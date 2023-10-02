using System;

namespace MagistradosWpfApp.Data
{
    public class Data_persona_r : Data_persona
    {
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
    }
}
