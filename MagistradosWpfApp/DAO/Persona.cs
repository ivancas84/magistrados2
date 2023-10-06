using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace MagistradosWpfApp.DAO
{
    class Persona
    {
        public IEnumerable<Dictionary<string, object?>> BuscarTexto(string search)
        {

            var q = ContainerApp.db.Query("persona")
                .Where("$nombres LIKE @0 OR $apellidos LIKE @0 OR $legajo LIKE @0 OR $numero_documento LIKE @0")
                .Order("$nombres ASC, $apellidos ASC")
                .Parameters("%"+search+"%");

            return q.ColOfDictCache();
        }
    }
}
