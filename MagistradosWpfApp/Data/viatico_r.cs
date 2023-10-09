using System;

namespace MagistradosWpfApp.Data
{
    public class Data_viatico_r : Data_viatico
    {
        protected string? _departamento_judicial__id = (string?)ContainerApp.db.DefaultValue("departamento_judicial", "id");
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
