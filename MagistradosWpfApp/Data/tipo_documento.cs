using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_tipo_documento : INotifyPropertyChanged
    {

        public string? Label { get; set; }

        protected string? _id = (string?)ContainerApp.db.DefaultValue("tipo_documento", "id");
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        protected string? _descripcion = null;
        public string? descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
