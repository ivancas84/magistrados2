

using SqlOrganize;

namespace MagistradosApp.Views.ProcesarArchivoSueldos
{
    public class Data_Longitud
    {
        public Data_Longitud(string organo)
        {
            if (organo.Equals("2"))
            {
                longitudFilaMaxima = 92; //aplicando trim posicion inicial = 0
                longitudFilaMinima = 81; //aplicando trim posicion inicial = 0

                inicioCodigoDepartamento = 0;
                longitudCodigoDepartamento = 2;

                inicioCodigoAfiliacion = 3;
                longitudCodigoAfiliacion = 4;

                inicioDescripcionAfiliacion = 8;
                longitudDescripcionAfiliacion = 12;

                inicioLegajo = 37;
                longitudLegajo = 6;

                inicioNombre = 44;
                longitudNombre = 29;

                inicioMonto = 73;
                longitudMonto = 8;

                inicioNumero = 82; //actualmente el numero no es utilizado
                longitudNumero = 9; //actualmente el numero no es utilizado
            }
        }

        #region atributos de longitud (los valores por defecto definidos corresponden a administracion de justicia)
        public int longitudFilaMaxima = 85; //aplicando trim posicion inicial = 0
        public int longitudFilaMinima = 85; //aplicando trim posicion inicial = 0

        public int inicioCodigoDepartamento = 0;
        public int longitudCodigoDepartamento = 2;

        public int inicioCodigoAfiliacion = 3;
        public int longitudCodigoAfiliacion = 4;

        public int inicioDescripcionAfiliacion = 8;
        public int longitudDescripcionAfiliacion = 12;

        public int inicioLegajo = 37;
        public int longitudLegajo = 6;

        public int inicioMonto = 69;
        public int longitudMonto = 16;

        public int inicioNombre = 44;
        public int longitudNombre = 25;

        public int inicioNumero = 81; //actualmente el numero no es utilizado
        public int longitudNumero = 3; //actualmente el numero no es utilizado
        #endregion

    }
}
