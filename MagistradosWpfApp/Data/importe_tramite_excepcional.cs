using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_importe_tramite_excepcional : INotifyPropertyChanged
    {

        public string? label { get; set; }

        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private DateTime? _creado;
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        private string? _tramite_excepcional;
        public string? tramite_excepcional
        {
            get { return _tramite_excepcional; }
            set { _tramite_excepcional = value; NotifyPropertyChanged(); }
        }
        private decimal? _valor;
        public decimal? valor
        {
            get { return _valor; }
            set { _valor = value; NotifyPropertyChanged(); }
        }
        private DateTime? _periodo;
        public DateTime? periodo
        {
            get { return _periodo; }
            set { _periodo = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
