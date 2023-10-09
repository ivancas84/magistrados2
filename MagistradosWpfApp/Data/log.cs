using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_log : INotifyPropertyChanged
    {

        public string? Label { get; set; }

        protected string? _id = (string?)ContainerApp.db.DefaultValue("log", "id");
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        protected string? _type = null;
        public string? type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged(); }
        }
        protected string? _description = null;
        public string? description
        {
            get { return _description; }
            set { _description = value; NotifyPropertyChanged(); }
        }
        protected string? _user = null;
        public string? user
        {
            get { return _user; }
            set { _user = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _created = (DateTime?)ContainerApp.db.DefaultValue("log", "created");
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
