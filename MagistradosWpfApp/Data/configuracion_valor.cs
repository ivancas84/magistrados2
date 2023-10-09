using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_configuracion_valor : INotifyPropertyChanged
    {

        public string? Label { get; set; }

        protected DateTime? _desde = null;
        public DateTime? desde
        {
            get { return _desde; }
            set { _desde = value; NotifyPropertyChanged(); }
        }
        protected decimal? _valor = null;
        public decimal? valor
        {
            get { return _valor; }
            set { _valor = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _creado = (DateTime?)ContainerApp.db.DefaultValue("configuracion_valor", "creado");
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        protected string? _id = (string?)ContainerApp.db.DefaultValue("configuracion_valor", "id");
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        protected string? _nombre = null;
        public string? nombre
        {
            get { return _nombre; }
            set { _nombre = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
