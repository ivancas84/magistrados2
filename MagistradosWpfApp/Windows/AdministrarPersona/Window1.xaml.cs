using MagistradosWpfApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utils;

namespace MagistradosWpfApp.Windows.AdministrarPersona
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private DAO.Persona dao = new ();
        private ObservableCollection<Data_persona> personaData = new();


        public Window1()
        {
            InitializeComponent();
            personaSearchList.Visibility = Visibility.Collapsed;
            personaData = new();
            personaSearchList.ItemsSource = personaData;

        }


        private void PersonaSearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.personaSearchList.Visibility = Visibility.Collapsed;

            if (this.personaSearchList.SelectedIndex > -1)
            {
                this.personaSearchTextBox.Text = ((Data_persona)this.personaSearchList.SelectedItem).Label;
            }
    
        }

        private void PersonaSearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (personaSearchList.SelectedIndex > -1)
                if (personaSearchTextBox.Text.Equals(((Data_persona)personaSearchList.SelectedItem).Label))
                    return;
                else
                    personaSearchList.SelectedIndex = -1;


            if (string.IsNullOrEmpty(personaSearchTextBox.Text) || personaSearchTextBox.Text.Length < 3) //restricciones locales para buscar, texto no nulo y mayor a 2 caracteres
            {
                personaSearchList.Visibility = Visibility.Collapsed;
                return;
            }

            personaSearchList.Visibility = Visibility.Visible;

            IEnumerable<Dictionary<string, object?>> list = dao.BuscarTexto(personaSearchTextBox.Text); //busqueda de valores a mostrar en funcion del texto

            personaData.Clear();
            foreach(Dictionary<string, object?> item in list)
            {
                var v = ContainerApp.db.Values("persona").Set(item);
                var o = item.Obj<Data_persona>();
                o.Label = v.ToString();
                personaData.Add(o);
            }

        }
    }
}
