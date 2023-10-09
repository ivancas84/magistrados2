using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_viatico : INotifyPropertyChanged
    {

        public string? Label { get; set; }

        protected string? _id = (string?)ContainerApp.db.DefaultValue("viatico", "id");
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _creado = (DateTime?)ContainerApp.db.DefaultValue("viatico", "creado");
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        protected decimal? _valor = (decimal?)ContainerApp.db.DefaultValue("viatico", "valor");
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
        protected string? _departamento_judicial = null;
        public string? departamento_judicial
        {
            get { return _departamento_judicial; }
            set { _departamento_judicial = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
