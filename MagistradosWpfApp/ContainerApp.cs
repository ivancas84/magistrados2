using Microsoft.Extensions.Caching.Memory;
using System.Configuration;

namespace MagistradosWpfApp
{
    static class ContainerApp
    {
        public static DbApp db;

        public static SqlOrganize.DAO dao;

        public static Config config = new Config();
        


        static ContainerApp()
        {
            config.id = "id";
            config.fkId = true;
            config.connectionString = ConfigurationManager.AppSettings.Get("connectionString");
          
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

            Schema sch = new Schema();
            db = new DbApp(config, sch, cache);

            dao = new SqlOrganize.DAO(db);
        }

    }
}
