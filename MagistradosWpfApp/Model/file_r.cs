using SqlOrganize;
using System;

namespace MagistradosWpfApp.Model
{
    public class Data_file_r : Data_file
    {

        public Data_file_r () : base()
        {
            Initialize();
        }

        public Data_file_r (DataInitMode mode = DataInitMode.Default) : base(mode)
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
