using SqlOrganize;
using System;

namespace MagistradosWpfApp.Data
{
    public class Data_persona_r : Data_persona
    {

        public Data_persona_r () : base()
        {
            Initialize();
        }

        public Data_persona_r (DataInitMode mode = DataInitMode.Default) : base(mode)
        {
            Initialize(mode);
        }

        protected override void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            base.Initialize(mode);
            switch(mode)
            {
                case DataInitMode.Default:
                    _cargo__id = (string?)ContainerApp.db.DefaultValue("cargo", "id");
                    _tipo_documento__id = (string?)ContainerApp.db.DefaultValue("tipo_documento", "id");
                break;
            }
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
    }
}
