using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace MagistradosApp.DAO
{
    class Departamento
    {
        public IEnumerable<Dictionary<string, object?>> Buscar(IDictionary<string, object?> search)
        {

            var q = ContainerApp.db.Query("afiliacion")
                .Search(search)
                .Order("$creado DESC, $persona-apellidos ASC");

            if (search.ContainsKey("modificado_set") && search["modificado_set"] != null) {
                string conc = (q.where.IsNullOrEmpty()) ? "" : " AND ";

                if ((bool)search["modificado_set"])
                    q.Where(conc + "$modificado IS NOT NULL");
                else
                    q.Where(conc + "$modificado IS NULL");
            }

            return q.ColOfDictCache();
        }
    }
}
