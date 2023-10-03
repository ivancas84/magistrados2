using MagistradosWpfApp.Data;
using System.Collections.ObjectModel;
using System.Windows;
using Utils;

namespace MagistradosWpfApp.Windows.ListaAfiliaciones
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private DAO.Afiliacion dao = new();
        private AfiliacionSearch search = new();
        private ObservableCollection<Data_afiliacion_r> afiliacionData = new();

        public Window1()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            DataContext = search;
            afiliacionGrid.ItemsSource = afiliacionData;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            var data = dao.Buscar(search.Dict());
            afiliacionData.Clear();
            afiliacionData.AddRange(data);
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }
    }
}
