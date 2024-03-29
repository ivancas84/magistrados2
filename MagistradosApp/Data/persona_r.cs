#nullable enable
using SqlOrganize;
using System;

namespace MagistradosApp.Data
{
    public class Data_persona_r : Data_persona
    {

        public Data_persona_r () : base()
        {
            Initialize();
        }

        public Data_persona_r (DataInitMode mode) : base(mode)
        {
            Initialize(mode);
        }

        protected override void Initialize(DataInitMode mode = DataInitMode.Null)
        {
            base.Initialize(mode);
            switch(mode)
            {
                case DataInitMode.Default:
                    cargo__id = (string?)ContainerApp.db.Values("cargo").Default("id").Get("id");
                    tipo_documento__id = (string?)ContainerApp.db.Values("tipo_documento").Default("id").Get("id");
                break;
            }
        }

        public string? cargo__Label { get; set; }

        protected string? _cargo__id = null;
        public string? cargo__id
        {
            get { return _cargo__id; }
            set { _cargo__id = value; _cargo = value; NotifyPropertyChanged(); }
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
            set { _tipo_documento__id = value; _tipo_documento = value; NotifyPropertyChanged(); }
        }
        protected string? _tipo_documento__descripcion = null;
        public string? tipo_documento__descripcion
        {
            get { return _tipo_documento__descripcion; }
            set { _tipo_documento__descripcion = value; NotifyPropertyChanged(); }
        }
    }
}
