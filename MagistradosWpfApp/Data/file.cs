using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_file : INotifyPropertyChanged
    {

        public string? Label { get; set; }

        protected string? _id = (string?)ContainerApp.db.DefaultValue("file", "id");
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        protected string? _name = null;
        public string? name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(); }
        }
        protected string? _type = null;
        public string? type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged(); }
        }
        protected string? _content = null;
        public string? content
        {
            get { return _content; }
            set { _content = value; NotifyPropertyChanged(); }
        }
        protected uint? _size = null;
        public uint? size
        {
            get { return _size; }
            set { _size = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _created = (DateTime?)ContainerApp.db.DefaultValue("file", "created");
        public DateTime? created
        {
            get { return _created; }
            set { _created = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
