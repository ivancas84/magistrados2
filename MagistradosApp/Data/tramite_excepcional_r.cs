#nullable enable
using SqlOrganize;
using System;

namespace MagistradosApp.Data
{
    public class Data_tramite_excepcional_r : Data_tramite_excepcional
    {

        public Data_tramite_excepcional_r () : base()
        {
            Initialize();
        }

        public Data_tramite_excepcional_r (DataInitMode mode = DataInitMode.Default) : base(mode)
        {
            Initialize(mode);
        }

        protected override void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            base.Initialize(mode);
            switch(mode)
            {
                case DataInitMode.Default:
                    persona__id = (string?)ContainerApp.db.Values("persona").Default("id").Get("id");
                    persona__creado = (DateTime?)ContainerApp.db.Values("persona").Default("creado").Get("creado");
                    persona__tipo_documento = (string?)ContainerApp.db.Values("persona").Default("tipo_documento").Get("tipo_documento");
                    cargo__id = (string?)ContainerApp.db.Values("cargo").Default("id").Get("id");
                    tipo_documento__id = (string?)ContainerApp.db.Values("tipo_documento").Default("id").Get("id");
                    sucursal__id = (string?)ContainerApp.db.Values("sucursal").Default("id").Get("id");
                    departamento_judicial__id = (string?)ContainerApp.db.Values("departamento_judicial").Default("id").Get("id");
                    organo__id = (string?)ContainerApp.db.Values("organo").Default("id").Get("id");
                    departamento_judicial_informado__id = (string?)ContainerApp.db.Values("departamento_judicial").Default("id").Get("id");
                break;
            }
        }

        public string? persona__Label { get; set; }

        protected string? _persona__id = null;
        public string? persona__id
        {
            get { return _persona__id; }
            set { _persona__id = value; _persona = value; NotifyPropertyChanged(); }
        }
        protected string? _persona__nombres = null;
        public string? persona__nombres
        {
            get { return _persona__nombres; }
            set { _persona__nombres = value; NotifyPropertyChanged(); }
        }
        protected string? _persona__apellidos = null;
        public string? persona__apellidos
        {
            get { return _persona__apellidos; }
            set { _persona__apellidos = value; NotifyPropertyChanged(); }
        }
        protected string? _persona__legajo = null;
        public string? persona__legajo
        {
            get { return _persona__legajo; }
            set { _persona__legajo = value; NotifyPropertyChanged(); }
        }
        protected string? _persona__numero_documento = null;
        public string? persona__numero_documento
        {
            get { return _persona__numero_documento; }
            set { _persona__numero_documento = value; NotifyPropertyChanged(); }
        }
        protected string? _persona__telefono_laboral = null;
        public string? persona__telefono_laboral
        {
            get { return _persona__telefono_laboral; }
            set { _persona__telefono_laboral = value; NotifyPropertyChanged(); }
        }
        protected string? _persona__telefono_particular = null;
        public string? persona__telefono_particular
        {
            get { return _persona__telefono_particular; }
            set { _persona__telefono_particular = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _persona__fecha_nacimiento = null;
        public DateTime? persona__fecha_nacimiento
        {
            get { return _persona__fecha_nacimiento; }
            set { _persona__fecha_nacimiento = value; NotifyPropertyChanged(); }
        }
        protected string? _persona__email = null;
        public string? persona__email
        {
            get { return _persona__email; }
            set { _persona__email = value; NotifyPropertyChanged(); }
        }
        protected string? _persona__tribunal = null;
        public string? persona__tribunal
        {
            get { return _persona__tribunal; }
            set { _persona__tribunal = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _persona__creado = null;
        public DateTime? persona__creado
        {
            get { return _persona__creado; }
            set { _persona__creado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _persona__eliminado = null;
        public DateTime? persona__eliminado
        {
            get { return _persona__eliminado; }
            set { _persona__eliminado = value; NotifyPropertyChanged(); }
        }
        protected string? _persona__cargo = null;
        public string? persona__cargo
        {
            get { return _persona__cargo; }
            set { _persona__cargo = value; NotifyPropertyChanged(); }
        }
        protected string? _persona__tipo_documento = null;
        public string? persona__tipo_documento
        {
            get { return _persona__tipo_documento; }
            set { _persona__tipo_documento = value; NotifyPropertyChanged(); }
        }

        public string? cargo__Label { get; set; }

        protected string? _cargo__id = null;
        public string? cargo__id
        {
            get { return _cargo__id; }
            set { _cargo__id = value; _persona__cargo = value; NotifyPropertyChanged(); }
        }
        protected string? _cargo__descripcion = null;
        public string? cargo__descripcion
        {
            get { return _cargo__descripcion; }
            set { _cargo__descripcion = value; NotifyPropertyChanged(); }
        }

        public string? tipo_documento__Label { get; set; }

        protected string? _tipo_documento__id = null;
        public string? tipo_documento__id
        {
            get { return _tipo_documento__id; }
            set { _tipo_documento__id = value; _persona__tipo_documento = value; NotifyPropertyChanged(); }
        }
        protected string? _tipo_documento__descripcion = null;
        public string? tipo_documento__descripcion
        {
            get { return _tipo_documento__descripcion; }
            set { _tipo_documento__descripcion = value; NotifyPropertyChanged(); }
        }

        public string? sucursal__Label { get; set; }

        protected string? _sucursal__id = null;
        public string? sucursal__id
        {
            get { return _sucursal__id; }
            set { _sucursal__id = value; _sucursal = value; NotifyPropertyChanged(); }
        }
        protected string? _sucursal__descripcion = null;
        public string? sucursal__descripcion
        {
            get { return _sucursal__descripcion; }
            set { _sucursal__descripcion = value; NotifyPropertyChanged(); }
        }

        public string? departamento_judicial__Label { get; set; }

        protected string? _departamento_judicial__id = null;
        public string? departamento_judicial__id
        {
            get { return _departamento_judicial__id; }
            set { _departamento_judicial__id = value; _departamento_judicial = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial__codigo = null;
        public string? departamento_judicial__codigo
        {
            get { return _departamento_judicial__codigo; }
            set { _departamento_judicial__codigo = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial__nombre = null;
        public string? departamento_judicial__nombre
        {
            get { return _departamento_judicial__nombre; }
            set { _departamento_judicial__nombre = value; NotifyPropertyChanged(); }
        }

        public string? organo__Label { get; set; }

        protected string? _organo__id = null;
        public string? organo__id
        {
            get { return _organo__id; }
            set { _organo__id = value; _organo = value; NotifyPropertyChanged(); }
        }
        protected string? _organo__descripcion = null;
        public string? organo__descripcion
        {
            get { return _organo__descripcion; }
            set { _organo__descripcion = value; NotifyPropertyChanged(); }
        }

        public string? departamento_judicial_informado__Label { get; set; }

        protected string? _departamento_judicial_informado__id = null;
        public string? departamento_judicial_informado__id
        {
            get { return _departamento_judicial_informado__id; }
            set { _departamento_judicial_informado__id = value; _departamento_judicial_informado = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial_informado__codigo = null;
        public string? departamento_judicial_informado__codigo
        {
            get { return _departamento_judicial_informado__codigo; }
            set { _departamento_judicial_informado__codigo = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial_informado__nombre = null;
        public string? departamento_judicial_informado__nombre
        {
            get { return _departamento_judicial_informado__nombre; }
            set { _departamento_judicial_informado__nombre = value; NotifyPropertyChanged(); }
        }
    }
}
