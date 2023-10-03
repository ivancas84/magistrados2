using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagistradosWpfApp.DAO
{
    class Afiliacion
    {
        public IEnumerable<Dictionary<string, object?>> Buscar(IDictionary<string, object?> search)
        {

            var q = ContainerApp.db.Query("afiliacion")
                .Search(search)
                .Order("$creado DESC, $persona-apellidos ASC");

            if (search.ContainsKey("modificado_set") && search["modificado_set"] != null) {
                if ((bool)search["modificado_set"])
                
                else
                    q.Where("$modificado IS NULL");
            }

            return q.ColOfDictCache();
        }
    }
}
