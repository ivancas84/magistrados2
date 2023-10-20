using SqlOrganize;
using System;

namespace MagistradosWpfApp.Model
{
    public class Data_cargo_r : Data_cargo
    {

        public Data_cargo_r () : base()
        {
            Initialize();
        }

        public Data_cargo_r (DataInitMode mode = DataInitMode.Default) : base(mode)
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
