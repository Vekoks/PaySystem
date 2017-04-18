namespace PaySystem.Data.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<PaySystem.Data.PaySystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(PaySystem.Data.PaySystemDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            context.ReallyBills.AddOrUpdate(
                new ReallyBill { Balance = 1000, IBank = "1234"},
                new ReallyBill { Balance = 2000, IBank = "12345"},
                new ReallyBill { Balance = 5000, IBank = "123456"},
                new ReallyBill { Balance = 3000, IBank = "1234567"},
                new ReallyBill { Balance = 500, IBank = "12345678"}

                );
        }
    }
}
