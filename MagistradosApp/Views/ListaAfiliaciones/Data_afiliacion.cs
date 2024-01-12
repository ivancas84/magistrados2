using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagistradosApp.Views.ListaAfiliaciones
{
    public class Data_afiliacion : Data.Data_afiliacion_r
    {


        public Data_afiliacion() : base(DataInitMode.Null)
        {
            _esta_modificado = false;
            _motivo = "Alta";
            _estado = "Aprobado";

        }

        protected bool? _esta_modificado = null;

        public bool? esta_modificado
        {
            get { return _esta_modificado; }
            set { _esta_modificado = value; NotifyPropertyChanged(); }
        }

        protected Int32? _creado_anio = null;

        public Int32? creado_anio
        {
            get { return _creado_anio; }
            set { _creado_anio = value; NotifyPropertyChanged(); }
        }

        protected Int32? _enviado_anio = null;

        public Int32? enviado_anio
        {
            get { return _enviado_anio; }
            set { _enviado_anio = value; NotifyPropertyChanged(); }
        }

        protected Int32? _evaluado_anio = null;

        public Int32? evaluado_anio
        {
            get { return _evaluado_anio; }
            set { _evaluado_anio = value; NotifyPropertyChanged(); }
        }

        protected Int32? _modificado_anio = null;

        public Int32? modificado_anio
        {
            get { return _modificado_anio; }
            set { _modificado_anio = value; NotifyPropertyChanged(); }
        }
    }
}
