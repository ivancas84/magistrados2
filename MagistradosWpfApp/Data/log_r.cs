using SqlOrganize;
using System;

namespace MagistradosWpfApp.Data
{
    public class Data_log_r : Data_log
    {

        public Data_log_r () : base()
        {
            Initialize();
        }

        public Data_log_r (DataInitMode mode = DataInitMode.Default) : base(mode)
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
