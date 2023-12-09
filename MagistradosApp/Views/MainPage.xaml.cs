using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MagistradosApp.Views;

public partial class MainPage : Page, INotifyPropertyChanged
{
    private ObservableCollection<Data_persona> personaOC = new(); //datos consultados de la base de datos
    private DispatcherTimer personaTypingTimer; //timer para buscar

    public Window1()
    {
        InitializeComponent();

        personaComboBox.ItemsSource = personaOC;
        personaComboBox.DisplayMemberPath = "Label";
        personaComboBox.SelectedValuePath = "id";
    }

    public MainPage()
    {
        InitializeComponent();
        DataContext = this;
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
