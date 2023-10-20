﻿using MagistradosWpfApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Utils;

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
            tramiteExcepcionalGrid.ItemsSource = tramiteExcepcionalData;
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
    }
}
