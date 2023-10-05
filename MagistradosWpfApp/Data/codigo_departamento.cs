using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_codigo_departamento : INotifyPropertyChanged
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
        private string? _departamento_judicial;
        public string? departamento_judicial
        {
            get { return _departamento_judicial; }
            set { _departamento_judicial = value; NotifyPropertyChanged(); }
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
