#nullable enable
using SqlOrganize;
using System;

namespace MagistradosApp.Data
{
    public class Data_codigo_departamento_r : Data_codigo_departamento
    {

        public Data_codigo_departamento_r () : base()
        {
            Initialize();
        }

        public Data_codigo_departamento_r (DataInitMode mode = DataInitMode.Default) : base(mode)
        {
            Initialize(mode);
        }

        protected override void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            base.Initialize(mode);
            switch(mode)
            {
                case DataInitMode.Default:
                    departamento_judicial__id = (string?)ContainerApp.db.Values("departamento_judicial").Default("id").Get("id");
                    organo__id = (string?)ContainerApp.db.Values("organo").Default("id").Get("id");
                break;
            }
        }

        public string? departamento_judicial__Label { get; set; }

        protected string? _departamento_judicial__id = null;
        public string? departamento_judicial__id
        {
            get { return _departamento_judicial__id; }
            set { _departamento_judicial__id = value; _departamento_judicial = value; NotifyPropertyChanged(); }
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
    }
}
