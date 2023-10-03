using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_log : INotifyPropertyChanged
    {

        public string? label { get; set; }

        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _type;
        public string? type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged(); }
        }
        private string? _description;
        public string? description
        {
            get { return _description; }
            set { _description = value; NotifyPropertyChanged(); }
        }
        private string? _user;
        public string? user
        {
            get { return _user; }
            set { _user = value; NotifyPropertyChanged(); }
        }
        private DateTime? _created;
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
