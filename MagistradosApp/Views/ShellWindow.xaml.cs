using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using MagistradosApp.Contracts.Services;
using MagistradosApp.Contracts.Views;

using MahApps.Metro.Controls;

namespace MagistradosApp.Views;

public partial class ShellWindow : MetroWindow, IShellWindow, INotifyPropertyChanged
{
    private readonly INavigationService _navigationService;
    private readonly IRightPaneService _rightPaneService;
    private bool _canGoBack;

    #region atributos historico e instancia https://github.com/Pericial/GAP/issues/114#issuecomment-1905655520
    Dictionary<string, Page> pages = new();
    List<Page> history = new(); //historial de navegacion
    #endregion

    #region metodos historico e instancia https://github.com/Pericial/GAP/issues/114#issuecomment-1905655520
    /// <summary>
    /// Guardar la instancia de la pagina y asignar a historico
    /// </summary>
    public void SetPageContent(Page pageInstance)
    {
        var t = pageInstance.GetType();
        if (!pages.ContainsKey(t.Name))
            pages[t.Name] = pageInstance;
        SetPageHistory(pages[t.Name]);
    }

    /// <summary>
    /// Asignar pagina al historico sin guardar la instancia
    /// </summary>
    public void SetPageHistory(Page pageInstance)
    {
        shellFrame.Content = pageInstance;
        history.Add(pageInstance);
        if (history.Count > ContainerApp.historyLength)
            history.RemoveAt(0);
    }

    /// <summary>
    /// Asignar pagina sin guardar historico ni instancia
    /// </summary>
    public void SetPage(Page pageInstance)
    {
        shellFrame.Content = pageInstance;
    }

    /// <summary>
    /// Botón volver
    /// </summary>
    private void btnVolver_Click(object sender, RoutedEventArgs e)
    {

        if (history.Count < 1)
            return;

        history.RemoveAt(history.Count - 1);

        if (history.Count == 0)
            shellFrame.Content = new MainPage();
        else
            shellFrame.Content = history.Last();
    }
    #endregion

    public bool CanGoBack
    {
        get { return _canGoBack; }
        set { Set(ref _canGoBack, value); }
    }

    public ShellWindow(INavigationService navigationService, IRightPaneService rightPaneService)
    {
        _navigationService = navigationService;
        _navigationService.Navigated += OnNavigated;
        _rightPaneService = rightPaneService;
        InitializeComponent();
        DataContext = this;
    }

    public Frame GetNavigationFrame()
        => shellFrame;

    public Frame GetRightPaneFrame()
        => rightPaneFrame;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();

    public SplitView GetSplitView()
        => splitView;

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _rightPaneService.CleanUp();
    }

    private void OnGoBack(object sender, RoutedEventArgs e)
    {
        _navigationService.GoBack();
    }

    private void OnNavigated(object sender, Type pageType)
    {
        CanGoBack = _navigationService.CanGoBack;
    }

    private void OnMenuFileExit(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown();

    private void OnMenuViewsMain(object sender, RoutedEventArgs e)
        => SetPageContent(new MainPage());

    private void OnMenuFileSettings(object sender, RoutedEventArgs e)
        => _rightPaneService.OpenInRightPane(typeof(SettingsPage));

    private void OnMenuViewsListaAfiliaciones(object sender, RoutedEventArgs e)
        => SetPageContent(new ListaAfiliacionesPage(this));

    private void OnMenuViewsCrearArchivoSueldos(object sender, RoutedEventArgs e)
        => SetPageHistory(new CrearArchivoSueldosPage());

    public event PropertyChangedEventHandler PropertyChanged;

    private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
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
