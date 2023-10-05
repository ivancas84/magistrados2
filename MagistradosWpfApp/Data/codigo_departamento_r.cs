using System;

namespace MagistradosWpfApp.Data
{
    public class Data_codigo_departamento_r : Data_codigo_departamento
    {
        private string? _departamento_judicial__id;
        public string? departamento_judicial__id
        {
            get { return _departamento_judicial__id; }
            set { _departamento_judicial__id = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial__nombre;
        public string? departamento_judicial__nombre
        {
            get { return _departamento_judicial__nombre; }
            set { _departamento_judicial__nombre = value; NotifyPropertyChanged(); }
        }
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
