using SqlOrganize;
using System;

namespace MagistradosWpfApp.Model
{
    public class Data_viatico_r : Data_viatico
    {

        public Data_viatico_r () : base()
        {
            Initialize();
        }

        public Data_viatico_r (DataInitMode mode = DataInitMode.Default) : base(mode)
        {
            Initialize(mode);
        }

        protected override void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            base.Initialize(mode);
            switch(mode)
            {
                case DataInitMode.Default:
                    _departamento_judicial__id = (string?)ContainerApp.db.Values("departamento_judicial").Default("id").Get("id");
                break;
            }
        }

        public string? departamento_judicial__Label { get; set; }

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
    }
}
