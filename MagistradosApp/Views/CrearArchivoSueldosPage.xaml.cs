using CommunityToolkit.WinUI.Notifications;
using MagistradosApp.Data;
using MagistradosApp.Views.CrearArchivoSueldos;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Utils;

namespace MagistradosApp.Views;

public partial class CrearArchivoSueldosPage : Page, INotifyPropertyChanged
{
    private ObservableCollection<Int32> periodoAnioOC = new();
    private ObservableCollection<Data_organo> organoOC = new();

    public CrearArchivoSueldosPage()
    {
        InitializeComponent();
        DataContext = this;

        tipoComboBox.SelectedValuePath = "Key";
        tipoComboBox.DisplayMemberPath = "Value";
        tipoComboBox.Items.Add(new KeyValuePair<string, string>("afiliacion", "Registro 40"));
        tipoComboBox.Items.Add(new KeyValuePair<string, string>("tramite_excepcional", "Registro 80"));


        periodoMesComboBox.SelectedValuePath = "Key";
        periodoMesComboBox.DisplayMemberPath = "Value";
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(1, "1"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(2, "2"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(3, "3"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(4, "4"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(5, "5"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(6, "6"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(7, "7"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(8, "8"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(9, "9"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(10, "10"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(11, "11"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(12, "12"));


        periodoAnioComboBox.ItemsSource = periodoAnioOC;
        IEnumerable<Int32> data = ContainerApp.db.Query("afiliacion").
            Select("YEAR($creado) AS creado_anio").
            Where("$creado IS NOT NULL").
            Order("creado_anio DESC").
            Column<Int32>(0);
        periodoAnioOC.Clear();
        periodoAnioOC.AddRange(data);


        organoComboBox.SelectedValuePath = "id";
        organoComboBox.DisplayMemberPath = "descripcion";
        organoComboBox.ItemsSource = organoOC;
        IEnumerable<Dictionary<string, object?>> data2 = ContainerApp.db.Query("organo").
            ColOfDictCache();
        organoOC.Clear();
        organoOC.AddRange(data2);


        formGroupBox.DataContext = new Data_CrearArchivoSueldos();
    }




    private void CrearButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var request = (Data_CrearArchivoSueldos)formGroupBox.DataContext;

            #region verificar si ya se encuentran procesados
            var cantidad = ContainerApp.db.Query("importe_"+request.tipo).
                Select("COUNT(*) as cantidad").
                Where("YEAR($periodo) = @0 AND MONTH($periodo) = @1 AND $" + request.tipo + "-organo = @2").
                Parameters(request.periodo_anio, request.periodo_mes, request.organo).
                Column<Int64>(0).ElementAt(0);

            if (cantidad > 0)
                new ToastContentBuilder()
                .AddText("Crear Archivo de Sueldos")
                .AddText("ERROR: Los datos ingresados, ya se encuentran procesados")
                .Show();
            #endregion

            /*causaOC.Clear();

            CausaDatos[] data = causaDAO.BuscarWS(request);
            if (data.IsNullOrEmptyOrDbNull())
                new ToastContentBuilder()
                    .AddText("Búsqueda de Causas del WS")
                    .AddText("La consulta no arrojó resultados")
                    .Show();
            else
                new ToastContentBuilder()
                    .AddText("Búsqueda de Causas del WS")
                    .AddText("La consulta devolvió " + data.Count() + " registros.")
                    .Show();

            causaOC.AddRange(data);*/
        }
        catch (Exception ex)
        {
            new ToastContentBuilder()
                .AddText("Crear Archivo de Sueldos")
                .AddText("ERROR: " + ex.Message)
                .Show();
        }
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

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
