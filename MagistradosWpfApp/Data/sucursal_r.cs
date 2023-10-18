using SqlOrganize;
using System;

namespace MagistradosWpfApp.Data
{
    public class Data_sucursal_r : Data_sucursal
    {

        public Data_sucursal_r () : base()
        {
            Initialize();
        }

        public Data_sucursal_r (DataInitMode mode = DataInitMode.Default) : base(mode)
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
