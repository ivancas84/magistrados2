using SqlOrganize;
using System;

namespace MagistradosWpfApp.Data
{
    public class Data_importe_tramite_excepcional_r : Data_importe_tramite_excepcional
    {

        public Data_importe_tramite_excepcional_r () : base()
        {
            Initialize();
        }

        public Data_importe_tramite_excepcional_r (DataInitMode mode = DataInitMode.Default) : base(mode)
        {
            Initialize(mode);
        }

        protected override void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            base.Initialize(mode);
            switch(mode)
            {
                case DataInitMode.Default:
                    _tramite_excepcional__id = (string?)ContainerApp.db.DefaultValue("tramite_excepcional", "id");
                    _tramite_excepcional__creado = (DateTime?)ContainerApp.db.DefaultValue("tramite_excepcional", "creado");
                    _tramite_excepcional__sucursal = (string?)ContainerApp.db.DefaultValue("tramite_excepcional", "sucursal");
                    _persona__id = (string?)ContainerApp.db.DefaultValue("persona", "id");
                    _persona__creado = (DateTime?)ContainerApp.db.DefaultValue("persona", "creado");
                    _cargo__id = (string?)ContainerApp.db.DefaultValue("cargo", "id");
                    _tipo_documento__id = (string?)ContainerApp.db.DefaultValue("tipo_documento", "id");
                    _sucursal__id = (string?)ContainerApp.db.DefaultValue("sucursal", "id");
                    _departamento_judicial__id = (string?)ContainerApp.db.DefaultValue("departamento_judicial", "id");
                    _organo__id = (string?)ContainerApp.db.DefaultValue("organo", "id");
                    _departamento_judicial_informado__id = (string?)ContainerApp.db.DefaultValue("departamento_judicial", "id");
                break;
            }
        }

        protected string? _tramite_excepcional__id = null;
        public string? tramite_excepcional__id
        {
            get { return _tramite_excepcional__id; }
            set { _tramite_excepcional__id = value; NotifyPropertyChanged(); }
        }
        protected string? _tramite_excepcional__motivo = null;
        public string? tramite_excepcional__motivo
        {
            get { return _tramite_excepcional__motivo; }
            set { _tramite_excepcional__motivo = value; NotifyPropertyChanged(); }
        }
        protected string? _tramite_excepcional__estado = null;
        public string? tramite_excepcional__estado
        {
            get { return _tramite_excepcional__estado; }
            set { _tramite_excepcional__estado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _tramite_excepcional__creado = null;
        public DateTime? tramite_excepcional__creado
        {
            get { return _tramite_excepcional__creado; }
            set { _tramite_excepcional__creado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _tramite_excepcional__enviado = null;
        public DateTime? tramite_excepcional__enviado
        {
            get { return _tramite_excepcional__enviado; }
            set { _tramite_excepcional__enviado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _tramite_excepcional__evaluado = null;
        public DateTime? tramite_excepcional__evaluado
        {
            get { return _tramite_excepcional__evaluado; }
            set { _tramite_excepcional__evaluado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _tramite_excepcional__modificado = null;
        public DateTime? tramite_excepcional__modificado
        {
            get { return _tramite_excepcional__modificado; }
            set { _tramite_excepcional__modificado = value; NotifyPropertyChanged(); }
        }
        protected string? _tramite_excepcional__observaciones = null;
        public string? tramite_excepcional__observaciones
        {
            get { return _tramite_excepcional__observaciones; }
            set { _tramite_excepcional__observaciones = value; NotifyPropertyChanged(); }
        }
        protected string? _tramite_excepcional__persona = null;
        public string? tramite_excepcional__persona
        {
            get { return _tramite_excepcional__persona; }
            set { _tramite_excepcional__persona = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _tramite_excepcional__desde = null;
        public DateTime? tramite_excepcional__desde
        {
            get { return _tramite_excepcional__desde; }
            set { _tramite_excepcional__desde = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _tramite_excepcional__hasta = null;
        public DateTime? tramite_excepcional__hasta
        {
            get { return _tramite_excepcional__hasta; }
            set { _tramite_excepcional__hasta = value; NotifyPropertyChanged(); }
        }
        protected decimal? _tramite_excepcional__monto = null;
        public decimal? tramite_excepcional__monto
        {
            get { return _tramite_excepcional__monto; }
            set { _tramite_excepcional__monto = value; NotifyPropertyChanged(); }
        }
        protected string? _tramite_excepcional__sucursal = null;
        public string? tramite_excepcional__sucursal
        {
            get { return _tramite_excepcional__sucursal; }
            set { _tramite_excepcional__sucursal = value; NotifyPropertyChanged(); }
        }
        protected int? _tramite_excepcional__codigo = null;
        public int? tramite_excepcional__codigo
        {
            get { return _tramite_excepcional__codigo; }
            set { _tramite_excepcional__codigo = value; NotifyPropertyChanged(); }
        }
        protected string? _tramite_excepcional__departamento_judicial = null;
        public string? tramite_excepcional__departamento_judicial
        {
            get { return _tramite_excepcional__departamento_judicial; }
            set { _tramite_excepcional__departamento_judicial = value; NotifyPropertyChanged(); }
        }
        protected string? _tramite_excepcional__organo = null;
        public string? tramite_excepcional__organo
        {
            get { return _tramite_excepcional__organo; }
            set { _tramite_excepcional__organo = value; NotifyPropertyChanged(); }
        }
        protected string? _tramite_excepcional__departamento_judicial_informado = null;
        public string? tramite_excepcional__departamento_judicial_informado
        {
            get { return _tramite_excepcional__departamento_judicial_informado; }
            set { _tramite_excepcional__departamento_judicial_informado = value; NotifyPropertyChanged(); }
        }
        protected string? _persona__id = null;
        public string? persona__id
        {
            get { return _persona__id; }
            set { _persona__id = value; NotifyPropertyChanged(); }
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
        protected string? _cargo__id = null;
        public string? cargo__id
        {
            get { return _cargo__id; }
            set { _cargo__id = value; NotifyPropertyChanged(); }
        }
        protected string? _cargo__descripcion = null;
        public string? cargo__descripcion
        {
            get { return _cargo__descripcion; }
            set { _cargo__descripcion = value; NotifyPropertyChanged(); }
        }
        protected string? _tipo_documento__id = null;
        public string? tipo_documento__id
        {
            get { return _tipo_documento__id; }
            set { _tipo_documento__id = value; NotifyPropertyChanged(); }
        }
        protected string? _tipo_documento__descripcion = null;
        public string? tipo_documento__descripcion
        {
            get { return _tipo_documento__descripcion; }
            set { _tipo_documento__descripcion = value; NotifyPropertyChanged(); }
        }
        protected string? _sucursal__id = null;
        public string? sucursal__id
        {
            get { return _sucursal__id; }
            set { _sucursal__id = value; NotifyPropertyChanged(); }
        }
        protected string? _sucursal__descripcion = null;
        public string? sucursal__descripcion
        {
            get { return _sucursal__descripcion; }
            set { _sucursal__descripcion = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial__id = null;
        public string? departamento_judicial__id
        {
            get { return _departamento_judicial__id; }
            set { _departamento_judicial__id = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial__nombre = null;
        public string? departamento_judicial__nombre
        {
            get { return _departamento_judicial__nombre; }
            set { _departamento_judicial__nombre = value; NotifyPropertyChanged(); }
        }
        protected string? _organo__id = null;
        public string? organo__id
        {
            get { return _organo__id; }
            set { _organo__id = value; NotifyPropertyChanged(); }
        }
        protected string? _organo__descripcion = null;
        public string? organo__descripcion
        {
            get { return _organo__descripcion; }
            set { _organo__descripcion = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial_informado__id = null;
        public string? departamento_judicial_informado__id
        {
            get { return _departamento_judicial_informado__id; }
            set { _departamento_judicial_informado__id = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial_informado__nombre = null;
        public string? departamento_judicial_informado__nombre
        {
            get { return _departamento_judicial_informado__nombre; }
            set { _departamento_judicial_informado__nombre = value; NotifyPropertyChanged(); }
        }
    }
}
