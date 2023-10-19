using SqlOrganize;
using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_persona : INotifyPropertyChanged
    {

        public Data_persona ()
        {
            Initialize();
        }

        public Data_persona(DataInitMode mode = DataInitMode.Default)
        {
            Initialize(mode);
        }

        protected virtual void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            switch(mode)
            {
                case DataInitMode.Default:
                case DataInitMode.DefaultMain:
                    _id = (string?)ContainerApp.db.Values("persona").Default("id").Get("id");
                    _creado = (DateTime?)ContainerApp.db.Values("persona").Default("creado").Get("creado");
                break;
            }
        }

        public string? Label { get; set; }

        protected string? _id = null;
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
        protected DateTime? _creado = null;
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
