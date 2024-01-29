
using Microsoft.Win32;

namespace MagistradosApp.Views.CrearArchivoSueldos
{
    public class Data_CrearArchivoSueldos : SqlOrganize.Data
    {

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
