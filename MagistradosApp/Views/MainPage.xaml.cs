using CommunityToolkit.WinUI.Notifications;
using Google.Protobuf.WellKnownTypes;
using MagistradosApp.DAO;
using MagistradosApp.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Utils;

namespace MagistradosApp.Views;

public partial class MainPage : Page, INotifyPropertyChanged
{
    #region personaComboBox
    private DAO.Persona personaDAO = new();
    private ObservableCollection<Data_persona> personaOC = new(); //datos consultados de la base de datos
    private DispatcherTimer personaTypingTimer; //timer para buscar
    #endregion

    private ObservableCollection<Data_cargo> cargoOC = new();
    private ObservableCollection<Data_tipo_documento> tipoDocumentoOC = new();


    #region afiliacionDataGrid
    private DAO.Afiliacion afiliacionDAO = new();
    private ObservableCollection<Data_afiliacion_r> afiliacionOC = new();
    #endregion


    #region tramiteExcepcionalDataGrid
    //private DAO.Afiliacion afiliacionDAO = new();
    //private ObservableCollection<Data_afiliacion_r> afiliacionOC = new();
    #endregion

    public MainPage(Data_afiliacion? afiliacion = null)
    {
        InitializeComponent();
        DataContext = this;

        personaComboBox.ItemsSource = personaOC;
        personaComboBox.DisplayMemberPath = "Label";
        personaComboBox.SelectedValuePath = "id";

        #region cargoComboBox
        cargoComboBox.ItemsSource = cargoOC;
        cargoComboBox.DisplayMemberPath = "descripcion";
        cargoComboBox.SelectedValuePath = "id";

        IEnumerable<Dictionary<string, object?>> data = ContainerApp.db.Query("cargo").Order("$descripcion ASC").ColOfDictCache();
        cargoOC.Clear();
        cargoOC.AddRange(data);
        Data_cargo cargoNull = new(SqlOrganize.DataInitMode.Null);
        cargoNull.descripcion = "(No especificado)";
        cargoOC.Add(cargoNull);
        #endregion

        #region tipoDocumentoComboBox
        tipoDocumentoComboBox.ItemsSource = tipoDocumentoOC;
        tipoDocumentoComboBox.DisplayMemberPath = "descripcion";
        tipoDocumentoComboBox.SelectedValuePath = "id";

        IEnumerable<Dictionary<string, object?>> data2 = ContainerApp.db.Query("tipo_documento").Order("$descripcion ASC").ColOfDictCache();
        tipoDocumentoOC.Clear();
        tipoDocumentoOC.AddRange(data2);
        Data_tipo_documento tipoDocumentoNull = new(SqlOrganize.DataInitMode.Null);
        tipoDocumentoNull.descripcion = "(No especificado)";
        tipoDocumentoOC.Add(tipoDocumentoNull);
        #endregion

        afiliacionDataGrid.ItemsSource = afiliacionOC;

        if (afiliacion.IsNullOrEmpty() || afiliacion.persona.IsNullOrEmptyOrDbNull())
            formGroupBox.DataContext = new Data_persona(SqlOrganize.DataInitMode.DefaultMain);
        else
            SetData(ContainerApp.dao.Get("persona", afiliacion.persona).Obj<Data_persona>());
    }

    #region personaComboBox v2023.11
    /// <summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks>Autocomplete 2 - GotFocus - v2023.11<br/>
    /// https://github.com/Pericial/GAP/issues/54#issuecomment-1790534457</remarks>
    private void PersonaComboBox_GotFocus(object sender, RoutedEventArgs e)
    {
        (sender as ComboBox).IsDropDownOpen = true;
    }

    /// <summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks>Autocomplete 2 - TextChanged - v2023.11<br/>
    /// https://github.com/Pericial/GAP/issues/54#issuecomment-1790534457</remarks>
    private void PersonaComboBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ComboBox cb = (sender as ComboBox);
        DispatcherTimer timer = personaTypingTimer;

        if (cb.Text.IsNullOrEmpty())
            cb.IsDropDownOpen = true;
        if (cb.SelectedIndex > -1)
        {
            if (cb.Text.Equals(((Data_persona)cb.SelectedItem).Label))
                return;
            cb.Text = "";
        }

        if (timer == null)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(300);
            timer.Tick += new EventHandler(PersonaHandleTypingTimerTimeout);
        }

        timer.Stop(); // Resets the timer
        timer.Tag = cb.Text; // This should be done with EventArgs
        timer.Start();
    }

    /// <summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks>Autocomplete 2 - HandleTypingTimerTimeout - v2023.11<br/>
    /// https://github.com/Pericial/GAP/issues/54#issuecomment-1790603741</remarks>
    private void PersonaHandleTypingTimerTimeout(object sender, EventArgs e)
    {
        var timer = sender as DispatcherTimer; // WPF
        if (timer == null)
            return;

        _PersonaComboBox_TextChanged();

        // The timer must be stopped! We want to act only once per keystroke.
        timer.Stop();
    }

    /// <summary>
    /// Autocomplete 2 - _TextChanged
    /// </summary>
    /// <remarks>Autocomplete 2 - _TextChanged - v 2023.11<br/>
    /// https://github.com/Pericial/GAP/issues/54#issuecomment-1790603853</remarks>
    private void _PersonaComboBox_TextChanged()
    {

        personaOC.Clear();

        if (string.IsNullOrEmpty(personaComboBox.Text) || personaComboBox.Text.Length < 3) //restricciones para buscar, texto no nulo y mayor a 2 caracteres
        {
            return;
        }

        IEnumerable<Dictionary<string, object>> list = personaDAO.BuscarTexto(personaComboBox.Text); //busqueda de valores a mostrar en funcion del texto

        foreach (var item in list)
        {
            var o = new Data_persona();
            o.SetData(item);
            o.Label = o.nombres + " " + o.apellidos + " " + o.legajo;
            personaOC.Add(o);
        }
    }

    /// <summary>
    /// </summary>
    /// <remarks>Autocomplete 2 - SelectionChanged - v2023.11<br/>
    /// https://github.com/Pericial/GAP/issues/54#issuecomment-1790604099</remarks>
    private void PersonaComboBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
        var cb = (ComboBox)sender;

        if (cb.SelectedIndex < 0)
            cb.IsDropDownOpen = true;
        else
        {
            SetData((Data_persona)cb.SelectedItem);
        }
    }
    #endregion

    public event PropertyChangedEventHandler PropertyChanged;


    private void SetData(Data_persona persona)
    {
        formGroupBox.DataContext = persona;
        afiliacionOC.Clear();
        var data = ContainerApp.dao.SearchKeyValue("afiliacion", "persona", persona.id);
        afiliacionOC.AddRange(data);
    }

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        
        if (Equals(storage, value))
        {
            return;
        }

        storage = value;
        OnPropertyChanged(propertyName);
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void GuardarPersonaButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var persona = (Data_persona)formGroupBox.DataContext;
            ContainerApp.db.Persist("persona").PersistObj(persona).Exec().RemoveCache();
            new ToastContentBuilder()
                .AddText("Administración de Persona")
                .AddText("Los datos de la persona se han registrado correctamente.")
                .Show();
        }
        catch (Exception ex)
        {
            new ToastContentBuilder()
                .AddText("Administración de Persona")
                .AddText("Error: " + ex.Message)
                .Show();
        }
    }

    /// <summary>
    /// DataGrid Save Button v 2024-01
    /// </summary>
    /// <remarks>https://github.com/Pericial/GAP/issues/68#issuecomment-1878745457</remarks>
    private void GuardarAfiliacion_Click(object sender, RoutedEventArgs e)
    {
        var button = (e.OriginalSource as Button);
        var afiliacion = (Data_afiliacion_r)button.DataContext;
        var p = ContainerApp.db.Persist("afiliacion");
        try
        {
            p.PersistObj(afiliacion).Exec().RemoveCache();
            new ToastContentBuilder()
                .AddText("Administración de Afiliado")
                .AddText("Registro 40 guardado")
                .Show();
        }
        catch (Exception ex)
        {
            new ToastContentBuilder()
                .AddText("Administración de Afiliado")
                .AddText("ERROR al guardar registro 40: " + ex.Message)
                .Show();
        }
    }

    /// <summary>
    /// DataGrid Delete Button v 2023-10
    /// </summary>
    /// <remarks>https://github.com/Pericial/GAP/issues/68</remarks>
    private void EliminarAfiliacion_Click(object sender, RoutedEventArgs e)
    {
        var button = (e.OriginalSource as Button);
        var a = (Data_afiliacion_r)button.DataContext;
        try
        {
            if (!a.id.IsNullOrEmpty())
                ContainerApp.db.Persist("afiliacion").DeleteIds(new object[] { a.id! }).Exec().RemoveCache();
            afiliacionOC.Remove(a);
            new ToastContentBuilder()
                .AddText("Administración de Afiliado")
                .AddText("Registro 40 eliminado")
                .Show();

        }
        catch (Exception ex)
        {
            new ToastContentBuilder()
                .AddText("Administración de Afiliado")
                .AddText("Error al eliminar Registro 40: " + ex.Message)
                .Show();
        }
    }

    private void AgregarAfiliacion_Click(object sender, RoutedEventArgs e)
    {
        var a = new Data_afiliacion_r();
        var persona = (Data_persona)formGroupBox.DataContext;
        a.persona = persona.id;
        afiliacionOC.Add(a);
    }

    private void EliminarTramiteExcepcional_Click(object sender, RoutedEventArgs e)
    {

    }
}

/// <summary>
/// Local ObjectDataProvider v2024-01
/// </summary>
/// <remarks>https://github.com/Pericial/GAP/issues/61#issuecomment-1875302097</remarks>
public class DepartamentoData
{
    public ObservableCollection<Data_departamento_judicial> Departamentos()
    {
        ObservableCollection<Data_departamento_judicial> response = new();
        var data = ContainerApp.db.Query("departamento_judicial").Size(0).Order("$nombre ASC").ColOfDictCache();
        response.Clear();
        response.AddRange(data);
        return response;
    }
}

public class OrganoData
{
    public ObservableCollection<Data_organo> Organos()
    {
        ObservableCollection<Data_organo> response = new();
        var data = ContainerApp.db.Query("organo").Size(0).Order("descripcion ASC").ColOfDictCache();
        response.Clear();
        response.AddRange(data);
        return response;
    }
}

public class EstadoData
{
    public ObservableCollection<string> Estados()
    {
        return new()
            {
                "Creado",
                "Enviado",
                "Aprobado",
                "Rechazado",
            };
    }
}

public class CodigoData
{
    public ObservableCollection<Int32> Codigos()
    {
        return new()
            {
                161,
                162,
                1621,
                1622,
            };
    }
}

public class MotivoData
{
    public ObservableCollection<string> Motivos()
    {
        return new()
            {
                "Alta",
                "Baja",
                "Pendiente",
            };
    }
}




