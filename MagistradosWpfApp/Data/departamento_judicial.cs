using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_departamento_judicial : INotifyPropertyChanged
    {

        public string? label { get; set; }

        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _codigo;
        public string? codigo
        {
            get { return _codigo; }
            set { _codigo = value; NotifyPropertyChanged(); }
        }
        private string? _nombre;
        public string? nombre
        {
            get { return _nombre; }
            set { _nombre = value; NotifyPropertyChanged(); }
        }
        private string? _organo;
        public string? organo
        {
            get { return _organo; }
            set { _organo = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
