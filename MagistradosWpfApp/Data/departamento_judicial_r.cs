using SqlOrganize;
using System;

namespace MagistradosWpfApp.Data
{
    public class Data_departamento_judicial_r : Data_departamento_judicial
    {

        public Data_departamento_judicial_r () : base()
        {
            Initialize();
        }

        public Data_departamento_judicial_r (DataInitMode mode = DataInitMode.Default) : base(mode)
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
