namespace SolutionName.DataLayer.Migrations
{
    using SolutionName.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SolutionName.DataLayer.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SolutionName.DataLayer.SalesContext context)
        {
            context.SalesOrders.AddOrUpdate(
                 so => so.CustomerName, 
                 new SalesOrder {CustomerName = "Ashutosh", PONumber="678"},
                 new SalesOrder {CustomerName = "Ram", PONumber = "679"},
                 new SalesOrder {CustomerName = "Rahul", PONumber = "680"});


        }
    }
}
