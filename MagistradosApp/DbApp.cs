using Microsoft.Extensions.Caching.Memory;
using SqlOrganize;
using SqlOrganizeMy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagistradosApp
{
    internal class DbApp : DbMy
    {
        public DbApp(Config config, Schema sch, MemoryCache cache) : base(config, sch, cache)
        {
        }

       
    }
}
