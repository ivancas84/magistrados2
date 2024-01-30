using CommunityToolkit.WinUI.Notifications;
using MagistradosApp.Data;
using MagistradosApp.Views.ProcesarArchivoSueldos;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Utils;

namespace MagistradosApp.Views;

public partial class ProcesarArchivoSueldosPage : Page, INotifyPropertyChanged
{

    private ObservableCollection<int> periodoAnioOC = new();
    private ObservableCollection<Data_organo> organoOC = new();


    public ProcesarArchivoSueldosPage()
    {
        InitializeComponent();
        DataContext = this;

        formGroupBox.DataContext = new Data_ProcesarArchivoSueldos();

        #region Inicializar periodoAnioComboBox
        periodoAnioComboBox.ItemsSource = periodoAnioOC;
        IEnumerable<int> data = ContainerApp.db.Query("afiliacion").
            Select("YEAR($enviado) AS enviado_anio").
            Where("$enviado IS NOT NULL").
            Order("$enviado DESC").
            Column<int>(0);

        int anioAdicional = data.ElementAt(0) + 1;

        periodoAnioOC.Clear();
        periodoAnioOC.AddRange(data);
        periodoAnioOC.Insert(0, anioAdicional);
        #endregion

        #region Inicializar periodoMesComboBox
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
        #endregion

        #region organo
        organoComboBox.SelectedValuePath = "id";
        organoComboBox.DisplayMemberPath = "descripcion";
        organoComboBox.ItemsSource = organoOC;
        IEnumerable<Dictionary<string, object?>> data2 = ContainerApp.db.Query("organo").Order("$descripcion ASC").
            ColOfDictCache();
        organoOC.Clear();
        organoOC.AddRange(data2);
        #endregion
    }

    private void BuscarArchivoButton_Click(object sender, RoutedEventArgs e)
    {
        // Create OpenFileDialog
        Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

        // Launch OpenFileDialog by calling ShowDialog method
        Nullable<bool> result = openFileDlg.ShowDialog();
        // Get the selected file name and display in a TextBox.
        // Load content of file in a TextBlock
        if (result == true)
        {
            archivoTextBox.Text = openFileDlg.FileName;
            //TextBlock1.Text = 
        }
    }

    private void ProcesarButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string fileContent = System.IO.File.ReadAllText(archivoTextBox.Text);
            /*causaOC.Clear();

            var request = (Requests.Causas_BuscarDatosRequest)searchGroupBox.DataContext;
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
                .AddText("Búsqueda de Causas del WS")
                .AddText(ex.Message)
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
