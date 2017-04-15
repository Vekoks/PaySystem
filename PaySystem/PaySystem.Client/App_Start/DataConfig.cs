using PaySystem.Data;
using PaySystem.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PaySystem.Client.App_Start
{
    public class DataConfig
    {
        public static void Init()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PaySystemDbContext, Configuration>());
            PaySystemDbContext.Create().Database.Initialize(true);
        }
    }
}