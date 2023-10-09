using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_afiliacion : INotifyPropertyChanged
    {

        public string? Label { get; set; }

        protected string? _id = (string?)ContainerApp.db.DefaultValue("afiliacion", "id");
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        protected string? _motivo = null;
        public string? motivo
        {
            get { return _motivo; }
            set { _motivo = value; NotifyPropertyChanged(); }
        }
        protected string? _estado = null;
        public string? estado
        {
            get { return _estado; }
            set { _estado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _creado = (DateTime?)ContainerApp.db.DefaultValue("afiliacion", "creado");
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _enviado = null;
        public DateTime? enviado
        {
            get { return _enviado; }
            set { _enviado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _evaluado = null;
        public DateTime? evaluado
        {
            get { return _evaluado; }
            set { _evaluado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _modificado = null;
        public DateTime? modificado
        {
            get { return _modificado; }
            set { _modificado = value; NotifyPropertyChanged(); }
        }
        protected string? _observaciones = null;
        public string? observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; NotifyPropertyChanged(); }
        }
        protected string? _persona = null;
        public string? persona
        {
            get { return _persona; }
            set { _persona = value; NotifyPropertyChanged(); }
        }
        protected int? _codigo = null;
        public int? codigo
        {
            get { return _codigo; }
            set { _codigo = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial = null;
        public string? departamento_judicial
        {
            get { return _departamento_judicial; }
            set { _departamento_judicial = value; NotifyPropertyChanged(); }
        }
        protected string? _organo = null;
        public string? organo
        {
            get { return _organo; }
            set { _organo = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial_informado = null;
        public string? departamento_judicial_informado
        {
            get { return _departamento_judicial_informado; }
            set { _departamento_judicial_informado = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
