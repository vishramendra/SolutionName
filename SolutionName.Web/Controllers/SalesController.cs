using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolutionName.Model;
using SolutionName.DataLayer;
using SolutionName.Web.ViewModels;


namespace SolutionName.Web.Controllers
{
    public class SalesController : Controller
    {
        private SalesContext _salesContext;

        public SalesController()
        {
            _salesContext = new SalesContext();
        }


        public ActionResult Index()
        {
            return View(_salesContext.SalesOrders.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel salesOrderViewModel = Helpers.CreateSalesOrderViewModelFromSaleOrder(salesOrder);
            salesOrderViewModel.MessageToClient = "I originated from the viewmodel, rather than the model.";

            return View(salesOrderViewModel);
        }


        public ActionResult Create()
        {
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.ObjectState = ObjectState.Added;
            return View(salesOrderViewModel);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            var salesOrderViewModel = Helpers.CreateSalesOrderViewModelFromSaleOrder(salesOrder);
            salesOrderViewModel.MessageToClient = string.Format("I original value of the customer Name is {0}.", salesOrderViewModel.CustomerName);
            salesOrderViewModel.ObjectState = ObjectState.Unchanged;

            return View(salesOrderViewModel);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            var salesOrderViewModel = Helpers.CreateSalesOrderViewModelFromSaleOrder(salesOrder);            
            salesOrderViewModel.MessageToClient = string.Format("I original value of the customer Name is {0}.", salesOrderViewModel.CustomerName);
            salesOrderViewModel.ObjectState = ObjectState.Deleted;

            return View(salesOrderViewModel);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _salesContext.Dispose();
            }
            base.Dispose(disposing);
        }


        public JsonResult Save(SalesOrderViewModel salesOrderViewModel)
        {
           var salesOrder = Helpers.CreateSalesOrderFromSaleOrderViewModel(salesOrderViewModel);
           salesOrder.ObjectState = salesOrderViewModel.ObjectState;

            _salesContext.SalesOrders.Attach(salesOrder);
            _salesContext.ChangeTracker.Entries<IObjectWithState>().Single().State = DataLayer.Helper.ConvertState(salesOrder.ObjectState);
            _salesContext.SaveChanges();

            if (salesOrder.ObjectState == ObjectState.Deleted)
            {
                return Json(new { newLocation = "/Sales/Index" });
            }

            salesOrderViewModel.MessageToClient = Helpers.GetMessageToClient(salesOrder.ObjectState, salesOrder.CustomerName);          

            salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            salesOrderViewModel.ObjectState = ObjectState.Unchanged;

            return Json(new { salesOrderViewModel });
        }
    }
}
