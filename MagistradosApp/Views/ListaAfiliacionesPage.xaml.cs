using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MagistradosApp.Views;

public partial class ListaAfiliacionesPage : Page, INotifyPropertyChanged
{
    public ListaAfiliacionesPage()
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
