using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_importe_afiliacion : INotifyPropertyChanged
    {

        public string? Label { get; set; }

        protected string? _id = (string?)ContainerApp.db.DefaultValue("importe_afiliacion", "id");
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _creado = (DateTime?)ContainerApp.db.DefaultValue("importe_afiliacion", "creado");
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        protected string? _afiliacion = null;
        public string? afiliacion
        {
            get { return _afiliacion; }
            set { _afiliacion = value; NotifyPropertyChanged(); }
        }
        protected decimal? _valor = null;
        public decimal? valor
        {
            get { return _valor; }
            set { _valor = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _periodo = null;
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
