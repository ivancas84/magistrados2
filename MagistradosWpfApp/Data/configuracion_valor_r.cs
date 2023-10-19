using SqlOrganize;
using System;

namespace MagistradosWpfApp.Data
{
    public class Data_configuracion_valor_r : Data_configuracion_valor
    {

        public Data_configuracion_valor_r () : base()
        {
            Initialize();
        }

        public Data_configuracion_valor_r (DataInitMode mode = DataInitMode.Default) : base(mode)
        {
            Initialize(mode);
        }

        protected override void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            base.Initialize(mode);
            switch(mode)
            {
                case DataInitMode.Default:
                break;
            }
        }
    }
}
