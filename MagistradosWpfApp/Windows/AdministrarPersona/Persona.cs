using MagistradosWpfApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagistradosWpfApp.Windows.AdministrarPersona
{
    internal class Persona : Data_persona
    {

        private DateTime? _creado = DateTime.Now;

        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
    }
}
