using SqlOrganize;
using System;

namespace MagistradosWpfApp.Data
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
                    _departamento_judicial__id = (string?)ContainerApp.db.DefaultValue("departamento_judicial", "id");
                    _organo__id = (string?)ContainerApp.db.DefaultValue("organo", "id");
                break;
            }
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
    }
}
