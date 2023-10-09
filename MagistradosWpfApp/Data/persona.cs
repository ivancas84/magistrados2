using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_persona : INotifyPropertyChanged
    {

        public string? Label { get; set; }

        protected string? _id = (string?)ContainerApp.db.DefaultValue("persona", "id");
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        protected string? _nombres = null;
        public string? nombres
        {
            get { return _nombres; }
            set { _nombres = value; NotifyPropertyChanged(); }
        }
        protected string? _apellidos = null;
        public string? apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value; NotifyPropertyChanged(); }
        }
        protected string? _legajo = null;
        public string? legajo
        {
            get { return _legajo; }
            set { _legajo = value; NotifyPropertyChanged(); }
        }
        protected string? _numero_documento = null;
        public string? numero_documento
        {
            get { return _numero_documento; }
            set { _numero_documento = value; NotifyPropertyChanged(); }
        }
        protected string? _telefono_laboral = null;
        public string? telefono_laboral
        {
            get { return _telefono_laboral; }
            set { _telefono_laboral = value; NotifyPropertyChanged(); }
        }
        protected string? _telefono_particular = null;
        public string? telefono_particular
        {
            get { return _telefono_particular; }
            set { _telefono_particular = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _fecha_nacimiento = null;
        public DateTime? fecha_nacimiento
        {
            get { return _fecha_nacimiento; }
            set { _fecha_nacimiento = value; NotifyPropertyChanged(); }
        }
        protected string? _email = null;
        public string? email
        {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged(); }
        }
        protected string? _tribunal = null;
        public string? tribunal
        {
            get { return _tribunal; }
            set { _tribunal = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _creado = (DateTime?)ContainerApp.db.DefaultValue("persona", "creado");
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _eliminado = null;
        public DateTime? eliminado
        {
            get { return _eliminado; }
            set { _eliminado = value; NotifyPropertyChanged(); }
        }
        protected string? _cargo = null;
        public string? cargo
        {
            get { return _cargo; }
            set { _cargo = value; NotifyPropertyChanged(); }
        }
        protected string? _tipo_documento = null;
        public string? tipo_documento
        {
            get { return _tipo_documento; }
            set { _tipo_documento = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
