using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace MagistradosWpfApp.Values
{
    internal class Persona : EntityValues
    {
        public Persona(Db _db, string _entityName, string? _fieldId = null) : base(_db, _entityName, _fieldId)
        {
        }

        public override string ToString()
        {
            string r = GetOrNull("nombres")?.ToString() ?? "?";
            r += " ";
            r += GetOrNull("apellidos")?.ToString() ?? "?";
            r += " ";
            r += GetOrNull("legajo")?.ToString() ?? "?";
            return r;
        }
    }

}
