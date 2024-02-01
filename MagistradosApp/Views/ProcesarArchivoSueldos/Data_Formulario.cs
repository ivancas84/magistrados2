

namespace MagistradosApp.Views.ProcesarArchivoSueldos
{
    public class Data_Formulario : SqlOrganize.Data
    {

        protected int _periodo_mes = DateTime.Now.Month;

        public int periodo_mes
        {
            get { return _periodo_mes; }
            set { _periodo_mes = value; NotifyPropertyChanged(); }
        }

        protected int _periodo_anio = DateTime.Now.Year;

        public int periodo_anio
        {
            get { return _periodo_anio; }
            set { _periodo_anio = value; NotifyPropertyChanged(); }
        }

        protected string _tipo = "afiliacion";

        protected string _organo = "1";

        public string organo
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
