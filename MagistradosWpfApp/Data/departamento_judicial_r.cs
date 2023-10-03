using System;

namespace MagistradosWpfApp.Data
{
    public class Data_departamento_judicial_r : Data_departamento_judicial
    {
        private string? _organo__id;
        public string? organo__id
        {
            get { return _organo__id; }
            set { _organo__id = value; NotifyPropertyChanged(); }
        }
        private string? _organo__descripcion;
        public string? organo__descripcion
        {
            get { return _organo__descripcion; }
            set { _organo__descripcion = value; NotifyPropertyChanged(); }
        }
    }
}
