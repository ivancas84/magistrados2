using CommunityToolkit.WinUI.Notifications;
using ControlzEx.Standard;
using MagistradosApp.DAO;
using MagistradosApp.Data;
using SqlOrganize;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Utils;

namespace MagistradosApp.Views;

public partial class ListaAfiliacionesPage : Page, INotifyPropertyChanged
{

    private ObservableCollection<Data_organo> organoOC = new();
    private ObservableCollection<Data_departamento_judicial> departamentoOC = new();
    private ObservableCollection<Int32?> creadoAnioOC = new();
    private ObservableCollection<Int32?> enviadoAnioOC = new();
    private ObservableCollection<Int32?> evaluadoAnioOC = new();
    private ObservableCollection<Int32?> modificadoAnioOC = new();
    private ObservableCollection<Data_cargo> personaCargoOC = new();

    private ObservableCollection<Data_afiliacion_r> afiliacionOC = new();
    private ShellWindow sw;

    public ListaAfiliacionesPage(ShellWindow sw)
    {
        InitializeComponent();
        this.sw = sw;

        DataContext = this;
        searchGroupBox.DataContext = new ListaAfiliaciones.Data_afiliacion();

        motivoComboBox.SelectedValuePath = "Key";
        motivoComboBox.DisplayMemberPath = "Value";
        motivoComboBox.Items.Add(new KeyValuePair<string?, string>(null, "(Todos)")); //quitar esta linea si no permite valor null
        motivoComboBox.Items.Add(new KeyValuePair<string, string>("Alta", "Alta"));
        motivoComboBox.Items.Add(new KeyValuePair<string, string>("Baja", "Baja"));
        motivoComboBox.Items.Add(new KeyValuePair<string, string>("Pendiente", "Pendiente"));

        estadoComboBox.SelectedValuePath = "Key";
        estadoComboBox.DisplayMemberPath = "Value";
        estadoComboBox.Items.Add(new KeyValuePair<string?, string>(null, "(Todos)")); //quitar esta linea si no permite valor null
        estadoComboBox.Items.Add(new KeyValuePair<string, string>("Creado", "Creado"));
        estadoComboBox.Items.Add(new KeyValuePair<string, string>("Enviado", "Enviado"));
        estadoComboBox.Items.Add(new KeyValuePair<string, string>("Aprobado", "Aprobado"));
        estadoComboBox.Items.Add(new KeyValuePair<string, string>("Rechazado", "Rechazado"));

        codigoComboBox.SelectedValuePath = "Key";
        codigoComboBox.DisplayMemberPath = "Value";
        codigoComboBox.Items.Add(new KeyValuePair<int?, string>(null, "(Todos)")); //quitar esta linea si no permite valor null
        codigoComboBox.Items.Add(new KeyValuePair<int, string>(161, "161"));
        codigoComboBox.Items.Add(new KeyValuePair<int, string>(162, "162"));
        codigoComboBox.Items.Add(new KeyValuePair<int, string>(1621, "1621"));
        codigoComboBox.Items.Add(new KeyValuePair<int, string>(1622, "1622"));

        estaModificadoComboBox.SelectedValuePath = "Key";
        estaModificadoComboBox.DisplayMemberPath = "Value";
        estaModificadoComboBox.Items.Add(new KeyValuePair<bool?, string>(null, "(Todos)")); //quitar esta linea si no permite valor null
        estaModificadoComboBox.Items.Add(new KeyValuePair<bool, string>(true, "Sí"));
        estaModificadoComboBox.Items.Add(new KeyValuePair<bool, string>(false, "No"));

        organoComboBox.ItemsSource = organoOC;
        organoComboBox.DisplayMemberPath = "descripcion";
        organoComboBox.SelectedValuePath = "id";
        var data = ContainerApp.db.Query("organo").ColOfDictCache();
        organoOC.Clear();
        organoOC.AddRange(data);
        Data_organo organoNull = new (SqlOrganize.DataInitMode.Null);
        organoNull.descripcion = "(Todos)";
        organoOC.Add(organoNull);

        #region departamentoComboBox y departamentoInformadoComboBox
        departamentoComboBox.ItemsSource = departamentoOC;
        departamentoComboBox.DisplayMemberPath = "nombre";
        departamentoComboBox.SelectedValuePath = "id";

        departamentoInformadoComboBox.ItemsSource = departamentoOC;
        departamentoInformadoComboBox.DisplayMemberPath = "nombre";
        departamentoInformadoComboBox.SelectedValuePath = "id";

        var data2 = ContainerApp.db.Query("departamento_judicial").Order("$nombre ASC").ColOfDictCache();
        departamentoOC.Clear();
        departamentoOC.AddRange(data2);
        Data_departamento_judicial departamentoNull = new Data_departamento_judicial(SqlOrganize.DataInitMode.Null);
        departamentoNull.nombre = "(Todos)";
        departamentoOC.Add(departamentoNull);
        #endregion

        #region personaCargoComboBox
        personaCargoComboBox.ItemsSource = personaCargoOC;
        personaCargoComboBox.DisplayMemberPath = "descripcion";
        personaCargoComboBox.SelectedValuePath = "id";

        data2 = ContainerApp.db.Query("cargo").Order("$descripcion ASC").ColOfDictCache();
        personaCargoOC.Clear();
        personaCargoOC.AddRange(data2);
        Data_cargo cargoNull = new (SqlOrganize.DataInitMode.Null);
        cargoNull.descripcion = "(Todos)";
        personaCargoOC.Add(cargoNull);
        #endregion


        creadoAnioComboBox.ItemsSource = creadoAnioOC;
        var anios = ContainerApp.db.Query("afiliacion").
            Select("YEAR($creado) AS anio").
            Where("$creado IS NOT NULL").
            Order("anio ASC").
            Column<Int32?>(0);
        creadoAnioOC.Clear();
        creadoAnioOC.AddRange(anios);
        creadoAnioOC.Add(null);

        enviadoAnioComboBox.ItemsSource = enviadoAnioOC;
        anios = ContainerApp.db.Query("afiliacion").
            Select("YEAR($enviado) AS anio").
            Where("$enviado IS NOT NULL").
            Order("anio ASC").
            Column<Int32?>(0);
        enviadoAnioOC.Clear();
        enviadoAnioOC.AddRange(anios);
        enviadoAnioOC.Add(null);


        evaluadoAnioComboBox.ItemsSource = evaluadoAnioOC;
        anios = ContainerApp.db.Query("afiliacion").
            Select("YEAR($evaluado) AS anio").
            Where("$evaluado IS NOT NULL").
            Order("anio ASC").
            Column<Int32?>(0);
        evaluadoAnioOC.Clear();
        evaluadoAnioOC.AddRange(anios);
        evaluadoAnioOC.Add(null);

        modificadoAnioComboBox.ItemsSource = modificadoAnioOC;
        anios = ContainerApp.db.Query("afiliacion").
            Select("YEAR($modificado) AS anio").
            Where("$modificado IS NOT NULL").
            Order("anio ASC").
            Column<Int32?>(0);
        modificadoAnioOC.Clear();
        modificadoAnioOC.AddRange(anios);
        modificadoAnioOC.Add(null);

        afiliacionDataGrid.ItemsSource = afiliacionOC;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;
        }

        storage = value;
        OnPropertyChanged(propertyName);
    }


    private void BuscarButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            afiliacionOC.Clear();
            var request = (ListaAfiliaciones.Data_afiliacion)searchGroupBox.DataContext;
            EntityQuery query = ContainerApp.db.Query("afiliacion");

            Type type = request.GetType();
            IEnumerable<string> propertyNames = type.GetProperties().ColOfProp<string, PropertyInfo>("Name");
            IDictionary<string, object?> d = request.Dict();
            var count = query.parameters.Count;

            foreach (var (key, value) in d)
            {
                if ((propertyNames.Contains(key) || ContainerApp.db.FieldNamesRel("afiliacion").Contains(key)) && !value.IsNullOrEmpty())
                {
                    if (!query.where.IsNullOrEmpty())
                        query.Where(" AND ");

                    switch (key)
                    {
                        case "esta_modificado":
                            if((bool)value)
                                query.Where("$modificado IS NOT NULL");
                            else
                                query.Where("$modificado IS NULL");

                            continue; //evitar la carga de parametros!

                        case "creado_mes":
                            query.Where("MONTH($creado) = @" + count.ToString());
                            break;

                        case "creado_anio":
                            query.Where("YEAR($creado) = @" + count.ToString());
                            break;

                        case "enviado_mes":
                            query.Where("MONTH($enviado) = @" + count.ToString());
                            break;

                        case "enviado_anio":
                            query.Where("YEAR($enviado) = @" + count.ToString());
                            break;

                        case "evaluado_mes":
                            query.Where("MONTH($evaluado) = @" + count.ToString());
                            break;

                        case "evaluado_anio":
                            query.Where("YEAR($evaluado) = @" + count.ToString());
                            break;

                        case "modificado_mes":
                            query.Where("MONTH($modificado) = @" + count.ToString());
                            break;

                        case "modificado_anio":
                            query.Where("YEAR($modificado) = @" + count.ToString());
                            break;
                        default:
                            query.Where("$" + key + " = @" + count.ToString());
                            break;

                    }

                    query.Parameters(value!);
                    count++;
                }
            }

            IEnumerable<Dictionary<string, object?>> data = query.ColOfDictCache();

            if (data.IsNullOrEmptyOrDbNull())
                new ToastContentBuilder()
                    .AddText("Búsqueda de Afiliaciones")
                    .AddText("La consulta no arrojó resultados")
                    .Show();
            else
                new ToastContentBuilder()
                    .AddText("Búsqueda de Afiliaciones")
                    .AddText("La consulta devolvió " + data.Count() + " registros.")
                    .Show();

            afiliacionOC.AddRange(data);
        }
        catch (Exception ex)
        {
            new ToastContentBuilder()
                .AddText("Búsqueda de Afiliaciones")
                .AddText(ex.Message)
                .Show();
        }
    }

    void OnApellidosClick(object sender, RoutedEventArgs e)
    {

        var data = ((Hyperlink)e.OriginalSource).DataContext as Data_afiliacion_r;
        sw.SetPageHistory(new MainPage(data));
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

  
}
