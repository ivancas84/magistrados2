using SqlOrganize;
using System;
using System.ComponentModel;

namespace MagistradosWpfApp.Model
{
    public class Data_viatico : INotifyPropertyChanged
    {

        public Data_viatico ()
        {
            Initialize();
        }

        public Data_viatico(DataInitMode mode = DataInitMode.Default)
        {
            Initialize(mode);
        }

        protected virtual void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            switch(mode)
            {
                case DataInitMode.Default:
                case DataInitMode.DefaultMain:
                    _id = (string?)ContainerApp.db.Values("viatico").Default("id").Get("id");
                    _creado = (DateTime?)ContainerApp.db.Values("viatico").Default("creado").Get("creado");
                    _valor = (decimal?)ContainerApp.db.Values("viatico").Default("valor").Get("valor");
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
        protected string? _departamento_judicial = null;
        public string? departamento_judicial
        {
            get { return _departamento_judicial; }
            set { _departamento_judicial = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
