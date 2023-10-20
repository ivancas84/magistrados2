using SqlOrganize;
using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Model
{
    public class Data_importe_afiliacion : INotifyPropertyChanged
    {

        public Data_importe_afiliacion ()
        {
            Initialize();
        }

        public Data_importe_afiliacion(DataInitMode mode = DataInitMode.Default)
        {
            Initialize(mode);
        }

        protected virtual void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            switch(mode)
            {
                case DataInitMode.Default:
                case DataInitMode.DefaultMain:
                    _id = (string?)ContainerApp.db.Values("importe_afiliacion").Default("id").Get("id");
                    _creado = (DateTime?)ContainerApp.db.Values("importe_afiliacion").Default("creado").Get("creado");
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
        protected string? _afiliacion = null;
        public string? afiliacion
        {
            get { return _afiliacion; }
            set { _afiliacion = value; NotifyPropertyChanged(); }
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
