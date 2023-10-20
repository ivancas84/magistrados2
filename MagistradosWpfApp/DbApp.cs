using MagistradosWpfApp.Values;
using Microsoft.Extensions.Caching.Memory;
using SqlOrganize;
using SqlOrganizeMy;

namespace MagistradosWpfApp
{
    internal class DbApp : DbMy
    {
        public DbApp(Config config, Schema sch, MemoryCache cache) : base(config, sch, cache)
        {
        }

        public override EntityValues Values(string entityName, string? fieldId = null)
        {
            switch (entityName)
            {
                case "persona":
                    return new Persona(this, entityName, fieldId);
            }

            return new EntityValues(this, entityName, fieldId);

        }
    }
}
