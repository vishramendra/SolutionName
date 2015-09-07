using SolutionName.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionName.Web.ViewModels
{
    public static class Helpers
    {
        public static SalesOrderViewModel CreateSalesOrderViewModelFromSaleOrder(SalesOrder saleOrder)
        {
            return new SalesOrderViewModel
            {
                CustomerName = saleOrder.CustomerName,
                PONumber = saleOrder.PONumber,
                SalesOrderId = saleOrder.SalesOrderId
            };
        }

        public static SalesOrder CreateSalesOrderFromSaleOrderViewModel(SalesOrderViewModel saleOrderViewModel)
        {
            return new SalesOrder
            {
                CustomerName = saleOrderViewModel.CustomerName,
                PONumber = saleOrderViewModel.PONumber,
                SalesOrderId = saleOrderViewModel.SalesOrderId
            };
        }

        public static string GetMessageToClient(ObjectState objectState, string customerName)
        {
            var messageToclient = string.Empty;
            switch (objectState)
            {
                case ObjectState.Added:
                    messageToclient = string.Format("{0}’s sales order has been added to the database.", customerName);
                    break;
                case ObjectState.Modified:
                    messageToclient= string.Format("{0}’s sales order has been edited.", customerName);
                    break;
            }

            return messageToclient; 
        }
    }
}