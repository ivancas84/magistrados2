using SqlOrganize;
using System;

namespace MagistradosWpfApp.Model
{
    public class Data_tipo_documento_r : Data_tipo_documento
    {

        public Data_tipo_documento_r () : base()
        {
            Initialize();
        }

        public Data_tipo_documento_r (DataInitMode mode = DataInitMode.Default) : base(mode)
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
