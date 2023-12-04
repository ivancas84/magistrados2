using Microsoft.Extensions.Caching.Memory;
using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagistradosApp
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

            Schema sch = new Schema();
            db = new DbApp(config, sch, cache);

            dao = new SqlOrganize.DAO(db);
        }
    }
}
