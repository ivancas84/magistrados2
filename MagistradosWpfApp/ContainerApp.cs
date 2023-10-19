using Microsoft.Extensions.Caching.Memory;
using SqlOrganize;
using System.Configuration;

namespace MagistradosWpfApp
{
    static class ContainerApp
    {
        public static Db db;

        public static SqlOrganize.DAO dao;

        public static Config config = new Config
        {
            id = "id",
            fkId = true,
            connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
        };


        static ContainerApp()
        {

            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

            Model model = new Model();
            db = new DbApp(config, model, cache);

            dao = new SqlOrganize.DAO(db);
        }

    }
}
