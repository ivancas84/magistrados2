using SqlOrganize;
using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Data
{
    public class Data_importe_tramite_excepcional : INotifyPropertyChanged
    {

        public Data_importe_tramite_excepcional ()
        {
            Initialize();
        }

        public Data_importe_tramite_excepcional(DataInitMode mode = DataInitMode.Default)
        {
            Initialize(mode);
        }

        protected virtual void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            switch(mode)
            {
                case DataInitMode.Default:
                case DataInitMode.DefaultMain:
                    _id = (string?)ContainerApp.db.Values("importe_tramite_excepcional").Default("id").Get("id");
                    _creado = (DateTime?)ContainerApp.db.Values("importe_tramite_excepcional").Default("creado").Get("creado");
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
        protected DateTime? _creado = null;
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        protected string? _tramite_excepcional = null;
        public string? tramite_excepcional
        {
            get { return _tramite_excepcional; }
            set { _tramite_excepcional = value; NotifyPropertyChanged(); }
        }
        protected decimal? _valor = null;
        public decimal? valor
        {
            get { return _valor; }
            set { _valor = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _periodo = null;
        public DateTime? periodo
        {
            get { return _periodo; }
            set { _periodo = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
