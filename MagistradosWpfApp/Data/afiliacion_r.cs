using System;

namespace MagistradosWpfApp.Data
{
    public class Data_afiliacion_r : Data_afiliacion
    {
        private string? _persona__id;
        public string? persona__id
        {
            get { return _persona__id; }
            set { _persona__id = value; NotifyPropertyChanged(); }
        }
        private string? _persona__nombres;
        public string? persona__nombres
        {
            get { return _persona__nombres; }
            set { _persona__nombres = value; NotifyPropertyChanged(); }
        }
        private string? _persona__apellidos;
        public string? persona__apellidos
        {
            get { return _persona__apellidos; }
            set { _persona__apellidos = value; NotifyPropertyChanged(); }
        }
        private string? _persona__legajo;
        public string? persona__legajo
        {
            get { return _persona__legajo; }
            set { _persona__legajo = value; NotifyPropertyChanged(); }
        }
        private string? _persona__numero_documento;
        public string? persona__numero_documento
        {
            get { return _persona__numero_documento; }
            set { _persona__numero_documento = value; NotifyPropertyChanged(); }
        }
        private string? _persona__telefono_laboral;
        public string? persona__telefono_laboral
        {
            get { return _persona__telefono_laboral; }
            set { _persona__telefono_laboral = value; NotifyPropertyChanged(); }
        }
        private string? _persona__telefono_particular;
        public string? persona__telefono_particular
        {
            get { return _persona__telefono_particular; }
            set { _persona__telefono_particular = value; NotifyPropertyChanged(); }
        }
        private DateTime? _persona__fecha_nacimiento;
        public DateTime? persona__fecha_nacimiento
        {
            get { return _persona__fecha_nacimiento; }
            set { _persona__fecha_nacimiento = value; NotifyPropertyChanged(); }
        }
        private string? _persona__email;
        public string? persona__email
        {
            get { return _persona__email; }
            set { _persona__email = value; NotifyPropertyChanged(); }
        }
        private string? _persona__tribunal;
        public string? persona__tribunal
        {
            get { return _persona__tribunal; }
            set { _persona__tribunal = value; NotifyPropertyChanged(); }
        }
        private DateTime? _persona__creado;
        public DateTime? persona__creado
        {
            get { return _persona__creado; }
            set { _persona__creado = value; NotifyPropertyChanged(); }
        }
        private DateTime? _persona__eliminado;
        public DateTime? persona__eliminado
        {
            get { return _persona__eliminado; }
            set { _persona__eliminado = value; NotifyPropertyChanged(); }
        }
        private string? _persona__cargo;
        public string? persona__cargo
        {
            get { return _persona__cargo; }
            set { _persona__cargo = value; NotifyPropertyChanged(); }
        }
        private string? _persona__tipo_documento;
        public string? persona__tipo_documento
        {
            get { return _persona__tipo_documento; }
            set { _persona__tipo_documento = value; NotifyPropertyChanged(); }
        }
        private string? _cargo__id;
        public string? cargo__id
        {
            get { return _cargo__id; }
            set { _cargo__id = value; NotifyPropertyChanged(); }
        }
        private string? _cargo__descripcion;
        public string? cargo__descripcion
        {
            get { return _cargo__descripcion; }
            set { _cargo__descripcion = value; NotifyPropertyChanged(); }
        }
        private string? _tipo_documento__id;
        public string? tipo_documento__id
        {
            get { return _tipo_documento__id; }
            set { _tipo_documento__id = value; NotifyPropertyChanged(); }
        }
        private string? _tipo_documento__descripcion;
        public string? tipo_documento__descripcion
        {
            get { return _tipo_documento__descripcion; }
            set { _tipo_documento__descripcion = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial__id;
        public string? departamento_judicial__id
        {
            get { return _departamento_judicial__id; }
            set { _departamento_judicial__id = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial__codigo;
        public string? departamento_judicial__codigo
        {
            get { return _departamento_judicial__codigo; }
            set { _departamento_judicial__codigo = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial__nombre;
        public string? departamento_judicial__nombre
        {
            get { return _departamento_judicial__nombre; }
            set { _departamento_judicial__nombre = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial__organo;
        public string? departamento_judicial__organo
        {
            get { return _departamento_judicial__organo; }
            set { _departamento_judicial__organo = value; NotifyPropertyChanged(); }
        }
        private string? _organo__id;
        public string? organo__id
        {
            get { return _organo__id; }
            set { _organo__id = value; NotifyPropertyChanged(); }
        }
        private string? _organo__descripcion;
        public string? organo__descripcion
        {
            get { return _organo__descripcion; }
            set { _organo__descripcion = value; NotifyPropertyChanged(); }
        }
        private string? _organo1__id;
        public string? organo1__id
        {
            get { return _organo1__id; }
            set { _organo1__id = value; NotifyPropertyChanged(); }
        }
        private string? _organo1__descripcion;
        public string? organo1__descripcion
        {
            get { return _organo1__descripcion; }
            set { _organo1__descripcion = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial_informado__id;
        public string? departamento_judicial_informado__id
        {
            get { return _departamento_judicial_informado__id; }
            set { _departamento_judicial_informado__id = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial_informado__codigo;
        public string? departamento_judicial_informado__codigo
        {
            get { return _departamento_judicial_informado__codigo; }
            set { _departamento_judicial_informado__codigo = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial_informado__nombre;
        public string? departamento_judicial_informado__nombre
        {
            get { return _departamento_judicial_informado__nombre; }
            set { _departamento_judicial_informado__nombre = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial_informado__organo;
        public string? departamento_judicial_informado__organo
        {
            get { return _departamento_judicial_informado__organo; }
            set { _departamento_judicial_informado__organo = value; NotifyPropertyChanged(); }
        }
        private string? _organo_de1__id;
        public string? organo_de1__id
        {
            get { return _organo_de1__id; }
            set { _organo_de1__id = value; NotifyPropertyChanged(); }
        }
        private string? _organo_de1__descripcion;
        public string? organo_de1__descripcion
        {
            get { return _organo_de1__descripcion; }
            set { _organo_de1__descripcion = value; NotifyPropertyChanged(); }
        }
    }
}
