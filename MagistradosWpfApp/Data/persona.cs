using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_persona : INotifyPropertyChanged
    {

        public string? Label { get; set; }

        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _nombres;
        public string? nombres
        {
            get { return _nombres; }
            set { _nombres = value; NotifyPropertyChanged(); }
        }
        private string? _apellidos;
        public string? apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value; NotifyPropertyChanged(); }
        }
        private string? _legajo;
        public string? legajo
        {
            get { return _legajo; }
            set { _legajo = value; NotifyPropertyChanged(); }
        }
        private string? _numero_documento;
        public string? numero_documento
        {
            get { return _numero_documento; }
            set { _numero_documento = value; NotifyPropertyChanged(); }
        }
        private string? _telefono_laboral;
        public string? telefono_laboral
        {
            get { return _telefono_laboral; }
            set { _telefono_laboral = value; NotifyPropertyChanged(); }
        }
        private string? _telefono_particular;
        public string? telefono_particular
        {
            get { return _telefono_particular; }
            set { _telefono_particular = value; NotifyPropertyChanged(); }
        }
        private DateTime? _fecha_nacimiento;
        public DateTime? fecha_nacimiento
        {
            get { return _fecha_nacimiento; }
            set { _fecha_nacimiento = value; NotifyPropertyChanged(); }
        }
        private string? _email;
        public string? email
        {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged(); }
        }
        private string? _tribunal;
        public string? tribunal
        {
            get { return _tribunal; }
            set { _tribunal = value; NotifyPropertyChanged(); }
        }
        private DateTime? _creado;
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        private DateTime? _eliminado;
        public DateTime? eliminado
        {
            get { return _eliminado; }
            set { _eliminado = value; NotifyPropertyChanged(); }
        }
        private string? _cargo;
        public string? cargo
        {
            get { return _cargo; }
            set { _cargo = value; NotifyPropertyChanged(); }
        }
        private string? _tipo_documento;
        public string? tipo_documento
        {
            get { return _tipo_documento; }
            set { _tipo_documento = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
