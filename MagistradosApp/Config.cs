using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagistradosApp
{
    public class Config : SqlOrganize.Config
    {
        public virtual string archivoSueldosPath { get; set; }
        public virtual string id { get; set; } = "id";


    }
}
