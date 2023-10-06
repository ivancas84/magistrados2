using MagistradosWpfApp.Values;
using Microsoft.Extensions.Caching.Memory;
using SqlOrganize;
using SqlOrganizeMy;

namespace MagistradosWpfApp
{
    internal class DbApp : DbMy
    {
        public DbApp(Config config, Model model, MemoryCache cache) : base(config, model, cache)
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
