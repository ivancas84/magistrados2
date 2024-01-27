
using Microsoft.Win32;

namespace MagistradosApp.Views.CrearArchivoSueldos
{
    public class Data_CrearArchivoSueldos : SqlOrganize.Data
    {



        protected Int32? _periodo_mes = DateTime.Now.Month;

        public Int32? periodo_mes
        {
            get { return _periodo_mes; }
            set { _periodo_mes = value; NotifyPropertyChanged(); }
        }

        protected Int32? _periodo_anio = DateTime.Now.Year;

        public Int32? periodo_anio
        {
            get { return _periodo_anio; }
            set { _periodo_anio = value; NotifyPropertyChanged(); }
        }


        protected string? _tipo = "afiliacion";

        public string? tipo
        {
            get { return _tipo; }
            set { _tipo = value; NotifyPropertyChanged(); }
        }


        protected string? _organo = "1";

        public string? organo
        {
            get { return _organo; }
            set { _organo = value; NotifyPropertyChanged(); }
        }

        protected override string ValidateField(string columnName)
        {
            throw new NotImplementedException();
        }
    }
}
