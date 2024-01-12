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

        protected bool? _estaModificado = null;

        public Data_afiliacion(DataInitMode mode = DataInitMode.Default) : base(mode)
        {
        }

        public bool? estaModificado
        {
            get { return _estaModificado; }
            set { _estaModificado = value; NotifyPropertyChanged(); }
        }
    }
}
