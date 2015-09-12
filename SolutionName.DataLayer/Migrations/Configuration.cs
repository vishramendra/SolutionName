namespace SolutionName.DataLayer.Migrations
{
    using SolutionName.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<SolutionName.DataLayer.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SalesContext context)
        {
            context.SalesOrders.AddOrUpdate(
                 so => so.CustomerName,
                 new SalesOrder { CustomerName = "Ashutosh", PONumber = "678",
                     SalesOrderItems = new List<SalesOrderItem>
                     {
                         new SalesOrderItem
                         {
                             ProductCode = "Cd3450",
                             Quantity = 3,
                             UnitPrice = 19.0m
                         },
                         new SalesOrderItem
                         {
                             ProductCode = "TY8450",
                             Quantity = 2,
                             UnitPrice = 30m
                         },
                         new SalesOrderItem
                         {
                             ProductCode = "RE3450",
                             Quantity = 78,
                             UnitPrice = 100m
                         }
                     } 
                 },
                 new SalesOrder
                 {
                     CustomerName = "Ram",
                     PONumber = "679",
                     SalesOrderItems = new List<SalesOrderItem>
                     {
                         new SalesOrderItem
                         {
                             ProductCode = "MB6750",
                             Quantity = 10,
                             UnitPrice = 45m
                         },
                          new SalesOrderItem
                         {
                             ProductCode = "Cd3450",
                             Quantity = 3,
                             UnitPrice = 67m
                         },
                         new SalesOrderItem
                         {
                             ProductCode = "TY8450",
                             Quantity = 2,
                             UnitPrice = 30m
                         },
                         new SalesOrderItem
                         {
                             ProductCode = "RE3450",
                             Quantity = 78,
                             UnitPrice = 100m
                         }
                     }
                 },
                 new SalesOrder { CustomerName = "Rahul", PONumber = "680" ,
                     SalesOrderItems = new List<SalesOrderItem>
                     {
                         new SalesOrderItem
                         {
                             ProductCode = "BV3670",
                             Quantity = 20,
                             UnitPrice = 1000m
                         }
                     } 
                 });
        }
    }
}
