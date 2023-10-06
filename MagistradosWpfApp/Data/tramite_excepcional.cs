using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_tramite_excepcional : INotifyPropertyChanged
    {

        public string? Label { get; set; }

        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _motivo;
        public string? motivo
        {
            get { return _motivo; }
            set { _motivo = value; NotifyPropertyChanged(); }
        }
        private string? _estado;
        public string? estado
        {
            get { return _estado; }
            set { _estado = value; NotifyPropertyChanged(); }
        }
        private DateTime? _creado;
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        private DateTime? _enviado;
        public DateTime? enviado
        {
            get { return _enviado; }
            set { _enviado = value; NotifyPropertyChanged(); }
        }
        private DateTime? _evaluado;
        public DateTime? evaluado
        {
            get { return _evaluado; }
            set { _evaluado = value; NotifyPropertyChanged(); }
        }
        private DateTime? _modificado;
        public DateTime? modificado
        {
            get { return _modificado; }
            set { _modificado = value; NotifyPropertyChanged(); }
        }
        private string? _observaciones;
        public string? observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; NotifyPropertyChanged(); }
        }
        private string? _persona;
        public string? persona
        {
            get { return _persona; }
            set { _persona = value; NotifyPropertyChanged(); }
        }
        private DateTime? _desde;
        public DateTime? desde
        {
            get { return _desde; }
            set { _desde = value; NotifyPropertyChanged(); }
        }
        private DateTime? _hasta;
        public DateTime? hasta
        {
            get { return _hasta; }
            set { _hasta = value; NotifyPropertyChanged(); }
        }
        private decimal? _monto;
        public decimal? monto
        {
            get { return _monto; }
            set { _monto = value; NotifyPropertyChanged(); }
        }
        private string? _sucursal;
        public string? sucursal
        {
            get { return _sucursal; }
            set { _sucursal = value; NotifyPropertyChanged(); }
        }
        private int? _codigo;
        public int? codigo
        {
            get { return _codigo; }
            set { _codigo = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial;
        public string? departamento_judicial
        {
            get { return _departamento_judicial; }
            set { _departamento_judicial = value; NotifyPropertyChanged(); }
        }
        private string? _organo;
        public string? organo
        {
            get { return _organo; }
            set { _organo = value; NotifyPropertyChanged(); }
        }
        private string? _departamento_judicial_informado;
        public string? departamento_judicial_informado
        {
            get { return _departamento_judicial_informado; }
            set { _departamento_judicial_informado = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
