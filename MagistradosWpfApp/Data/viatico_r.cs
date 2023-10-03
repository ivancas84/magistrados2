using System;

namespace MagistradosWpfApp.Data
{
    public class Data_viatico_r : Data_viatico
    {
        private string? _departamento_judicial__id;
        public string? departamento_judicial__id
        {
            get { return _departamento_judicial__id; }
            set { _departamento_judicial__id = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial__codigo;
        public string? departamento_judicial__codigo
        {
            get { return _departamento_judicial__codigo; }
            set { _departamento_judicial__codigo = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial__nombre;
        public string? departamento_judicial__nombre
        {
            get { return _departamento_judicial__nombre; }
            set { _departamento_judicial__nombre = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial__organo;
        public string? departamento_judicial__organo
        {
            get { return _departamento_judicial__organo; }
            set { _departamento_judicial__organo = value; NotifyPropertyChanged(); }
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
