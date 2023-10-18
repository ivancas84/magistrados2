using SqlOrganize;
using System;

namespace MagistradosWpfApp.Data
{
    public class Data_organo_r : Data_organo
    {

        public Data_organo_r () : base()
        {
            Initialize();
        }

        public Data_organo_r (DataInitMode mode = DataInitMode.Default) : base(mode)
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
