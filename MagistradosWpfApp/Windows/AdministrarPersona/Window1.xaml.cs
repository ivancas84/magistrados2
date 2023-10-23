using MagistradosWpfApp.Model;
using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Utils;
using WpfUtils;

namespace MagistradosWpfApp.Windows.AdministrarPersona
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private DAO.Persona personaDAO = new ();
        private ObservableCollection<Data_persona> personaData = new();
        Data_persona persona = new();

        private ObservableCollection<Data_afiliacion_r> afiliacionData = new();
        private ObservableCollection<Data_tramite_excepcional_r> tramiteExcepcionalData = new();

        public Window1()
        {
            InitializeComponent();
            personaSearchList.Visibility = Visibility.Collapsed;
            personaData = new();
            personaSearchList.ItemsSource = personaData;
            DataContext = persona;

            afiliacionGrid.ItemsSource = afiliacionData;
            afiliacionGrid.CellEditEnding += AfiliacionGrid_CellEditEnding;

            tramiteExcepcionalGrid.ItemsSource = tramiteExcepcionalData;
        }

        private void AfiliacionGrid_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit)
                return;

            var result = e.GetKeyAndValue();
            string key = result.key;
            object? value = result.value;
            IDictionary<string, object?> source = e.Row.DataContext.Dict();
            string? fieldId = null;
            string entityName = "afiliacion";
            string fieldName = key;

            if (key.Contains("__"))
                (fieldId, fieldName, entityName) = ContainerApp.db.KeyDeconstruction(entityName, key);

            EntityValues v = ContainerApp.db.Values(entityName, fieldId).Set(source);
            if (!v.GetOrNull(fieldName).IsNullOrEmptyOrDbNull() && v.values[fieldName]!.Equals(value))
                return;

            v.Sset(fieldName, value);
            IDictionary<string, object>? row = ContainerApp.dao.RowByUniqueFieldOrValues(fieldName, v);
            if (!row.IsNullOrEmpty()) //con el nuevo valor ingresados se obtuvo un nuevo campo unico, no se realiza persistencia y se cambian los valores para reflejar el nuevo valor consultado
            {
                v.Set(row);
                (e.Row.Item as Data_afiliacion_r).CopyValues<Data_afiliacion_r>(v.Get().Obj<Data_afiliacion_r>());
            }
            else
            {
                e.Row.Item.SetPropertyValue(fieldName, v.Get(fieldName));
            }
        }

        private void PersonaSearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.personaSearchList.Visibility = Visibility.Collapsed;

            if (this.personaSearchList.SelectedIndex > -1)
            {
                persona = (Data_persona)this.personaSearchList.SelectedItem;
                DataContext = persona;
                personaSearchTextBox.Text = persona.Label;

                var data = ContainerApp.db.Query("afiliacion").Size(0).Where("persona = @0").Parameters(persona.id!).ColOfDictCache();
                afiliacionData.Clear();
                foreach(var item in data)
                {
                    var o = item.Obj<Data_afiliacion_r>();
                    var v = ContainerApp.db.Values("afiliacion").Set(item);
                    o.persona__Label = v.ValuesTree("persona")?.ToString();
                    afiliacionData.Add(o);
                }

                data = ContainerApp.db.Query("tramite_excepcional").Size(0).Where("persona = @0").Parameters(persona.id!).ColOfDictCache();
                tramiteExcepcionalData.Clear();
                tramiteExcepcionalData.AddRange(data);
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

            IEnumerable<Dictionary<string, object?>> list = personaDAO.BuscarTexto(personaSearchTextBox.Text); //busqueda de valores a mostrar en funcion del texto

            personaData.Clear();
            foreach(Dictionary<string, object?> item in list)
            {
                var v = ContainerApp.db.Values("persona").Set(item);
                var o = item.Obj<Data_persona>();
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

        private void EliminarAfiliacion_Click(object sender, RoutedEventArgs e)
        {
            var button = (e.OriginalSource as Button);
            var a = (Data_afiliacion_r)button.DataContext;
            try {
                if (!a.id.IsNullOrEmpty())
                    ContainerApp.db.Persist("afiliacion").DeleteIds(new object[]{ a.id! }).Exec().RemoveCache();
                afiliacionData.Remove(a);

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EliminarTramiteExcepcional_Click(object sender, RoutedEventArgs e)
        {
            var button = (e.OriginalSource as Button);
            var te = (Data_tramite_excepcional_r)button.DataContext;
            try
            {
                if(!te.id.IsNullOrEmpty())
                    ContainerApp.db.Persist("tramite_excepcional").DeleteIds(new object[] { te.id! }).Exec().RemoveCache();
                tramiteExcepcionalData.Remove(te);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AgregarAfiliacion_Click(object sender, RoutedEventArgs e)
        {
            var a = new Data_afiliacion_r();
            a.persona = persona.id;
            afiliacionData.Add(a);
        }

        private void GuardarAfiliacion_Click(object sender, RoutedEventArgs e)
        {
            var button = (e.OriginalSource as Button);
            var afiliacion = (Data_afiliacion_r)button.DataContext;
            var p = ContainerApp.db.Persist("afiliacion");
            try
            {
                p.Persist(afiliacion.Dict()).Exec().RemoveCache();
                MessageBox.Show("Registro realizado");
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class DepartamentoData
    {
        public ObservableCollection<Data_departamento_judicial> Departamentos()
        {
            ObservableCollection<Data_departamento_judicial> response = new ();
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




}
