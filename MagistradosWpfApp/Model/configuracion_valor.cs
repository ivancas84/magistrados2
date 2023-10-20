using SqlOrganize;
using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Model
{
    public class Data_configuracion_valor : INotifyPropertyChanged
    {

        public Data_configuracion_valor ()
        {
            Initialize();
        }

        public Data_configuracion_valor(DataInitMode mode = DataInitMode.Default)
        {
            Initialize(mode);
        }

        protected virtual void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            switch(mode)
            {
                case DataInitMode.Default:
                case DataInitMode.DefaultMain:
                    _creado = (DateTime?)ContainerApp.db.Values("configuracion_valor").Default("creado").Get("creado");
                    _id = (string?)ContainerApp.db.Values("configuracion_valor").Default("id").Get("id");
                break;
            }
        }

        public string? Label { get; set; }

        protected DateTime? _desde = null;
        public DateTime? desde
        {
            get { return _desde; }
            set { _desde = value; NotifyPropertyChanged(); }
        }
        protected decimal? _valor = null;
        public decimal? valor
        {
            get { return _valor; }
            set { _valor = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _creado = null;
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        protected string? _id = null;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        protected string? _nombre = null;
        public string? nombre
        {
            get { return _nombre; }
            set { _nombre = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
