using SqlOrganize;
using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Model
{
    public class Data_tramite_excepcional : INotifyPropertyChanged
    {

        public Data_tramite_excepcional ()
        {
            Initialize();
        }

        public Data_tramite_excepcional(DataInitMode mode = DataInitMode.Default)
        {
            Initialize(mode);
        }

        protected virtual void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            switch(mode)
            {
                case DataInitMode.Default:
                case DataInitMode.DefaultMain:
                    _id = (string?)ContainerApp.db.Values("tramite_excepcional").Default("id").Get("id");
                    _creado = (DateTime?)ContainerApp.db.Values("tramite_excepcional").Default("creado").Get("creado");
                    _sucursal = (string?)ContainerApp.db.Values("tramite_excepcional").Default("sucursal").Get("sucursal");
                break;
            }
        }

        public string? Label { get; set; }

        protected string? _id = null;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        protected string? _motivo = null;
        public string? motivo
        {
            get { return _motivo; }
            set { _motivo = value; NotifyPropertyChanged(); }
        }
        protected string? _estado = null;
        public string? estado
        {
            get { return _estado; }
            set { _estado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _creado = null;
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _enviado = null;
        public DateTime? enviado
        {
            get { return _enviado; }
            set { _enviado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _evaluado = null;
        public DateTime? evaluado
        {
            get { return _evaluado; }
            set { _evaluado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _modificado = null;
        public DateTime? modificado
        {
            get { return _modificado; }
            set { _modificado = value; NotifyPropertyChanged(); }
        }
        protected string? _observaciones = null;
        public string? observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; NotifyPropertyChanged(); }
        }
        protected string? _persona = null;
        public string? persona
        {
            get { return _persona; }
            set { _persona = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _desde = null;
        public DateTime? desde
        {
            get { return _desde; }
            set { _desde = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _hasta = null;
        public DateTime? hasta
        {
            get { return _hasta; }
            set { _hasta = value; NotifyPropertyChanged(); }
        }
        protected decimal? _monto = null;
        public decimal? monto
        {
            get { return _monto; }
            set { _monto = value; NotifyPropertyChanged(); }
        }
        protected string? _sucursal = null;
        public string? sucursal
        {
            get { return _sucursal; }
            set { _sucursal = value; NotifyPropertyChanged(); }
        }
        protected int? _codigo = null;
        public int? codigo
        {
            get { return _codigo; }
            set { _codigo = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial = null;
        public string? departamento_judicial
        {
            get { return _departamento_judicial; }
            set { _departamento_judicial = value; NotifyPropertyChanged(); }
        }
        protected string? _organo = null;
        public string? organo
        {
            get { return _organo; }
            set { _organo = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial_informado = null;
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
