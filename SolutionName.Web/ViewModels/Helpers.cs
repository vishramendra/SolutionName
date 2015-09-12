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
            var salesOrderViewModel = new SalesOrderViewModel
            {
                CustomerName = saleOrder.CustomerName,
                PONumber = saleOrder.PONumber,
                SalesOrderId = saleOrder.SalesOrderId,
                ObjectState = ObjectState.Unchanged
            };

            foreach (var salesOrderItem in saleOrder.SalesOrderItems)
            {
                var salesOrderItemViewModel = new SalesOrderItemViewModel
                {
                    SalesOrderItemId = salesOrderItem.SalesOrderItemId,
                    ProductCode = salesOrderItem.ProductCode,
                    Quantity = salesOrderItem.Quantity,
                    UnitPrice = salesOrderItem.UnitPrice,
                    ObjectState = ObjectState.Unchanged,
                    SalesOrderId = salesOrderItem.SalesOrderId
                };

                salesOrderViewModel.SalesOrderItems.Add(salesOrderItemViewModel);
            }

            return salesOrderViewModel;
        }

        public static SalesOrder CreateSalesOrderFromSaleOrderViewModel(SalesOrderViewModel saleOrderViewModel)
        {
            var salesOrder = new SalesOrder
            {
                CustomerName = saleOrderViewModel.CustomerName,
                PONumber = saleOrderViewModel.PONumber,
                SalesOrderId = saleOrderViewModel.SalesOrderId,
                ObjectState = saleOrderViewModel.ObjectState
            };

            var temporarySalesOrderItemId = -1;

            foreach (var salesOrderItemViewModel in saleOrderViewModel.SalesOrderItems)
            {
                var salesOrderItem = new SalesOrderItem
                {
                    ProductCode = salesOrderItemViewModel.ProductCode,
                    Quantity = salesOrderItemViewModel.Quantity,
                    UnitPrice = salesOrderItemViewModel.UnitPrice,
                    ObjectState = salesOrderItemViewModel.ObjectState,
                };

                if (salesOrderItemViewModel.ObjectState != ObjectState.Added)
                    salesOrderItem.SalesOrderItemId = salesOrderItemViewModel.SalesOrderItemId;
                else
                {
                    salesOrderItem.SalesOrderItemId = temporarySalesOrderItemId;
                    temporarySalesOrderItemId--;
                }


                salesOrderItem.SalesOrderId = saleOrderViewModel.SalesOrderId;


                salesOrder.SalesOrderItems.Add(salesOrderItem);
            }

            return salesOrder;
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
                    messageToclient = string.Format("{0}’s sales order has been edited.", customerName);
                    break;
            }

            return messageToclient;
        }
    }
}