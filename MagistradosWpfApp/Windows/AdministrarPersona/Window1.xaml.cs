using MagistradosWpfApp.Data;
using SqlOrganize;
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
        private ObservableCollection<Persona> personaData = new();
        Persona persona = new();

        public Window1()
        {
            InitializeComponent();
            personaSearchList.Visibility = Visibility.Collapsed;
            personaData = new();
            personaSearchList.ItemsSource = personaData;
            DataContext = persona;
        }


        private void PersonaSearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.personaSearchList.Visibility = Visibility.Collapsed;

            if (this.personaSearchList.SelectedIndex > -1)
            {
                persona = (Persona)this.personaSearchList.SelectedItem;
                DataContext = persona;
                personaSearchTextBox.Text = persona.Label;
            }
    
        }

        private void PersonaSearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (personaSearchList.SelectedIndex > -1)
                if (personaSearchTextBox.Text.Equals(((Persona)personaSearchList.SelectedItem).Label))
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
                var o = item.Obj<Persona>();
                o.Label = v.ToString();
                personaData.Add(o);
            }

        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            try {

                ContainerApp.db.Persist("persona").Persist(persona.Dict()).Exec().RemoveCache();
                MessageBox.Show("Registro guardado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
