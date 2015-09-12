using System.Linq;
using System.Net;
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

            _salesContext.SalesOrders.Attach(salesOrder);

            foreach (var salesOrderItemViewModel in salesOrderViewModel.SalesOrderItems)
            {
                var salesOrderItem = _salesContext.SalesOrderItems.Find(salesOrderItemViewModel.SalesOrderItemId);

                if (salesOrderItem != null)
                    salesOrderItem.ObjectState = ObjectState.Deleted;

            }

            foreach (var salesOrderItemId in salesOrderViewModel.SalesOrderItemsToDelete)
            {
                var salesOrderItem = _salesContext.SalesOrderItems.Find(salesOrderItemId);

                if(salesOrderItem!=null)
                    salesOrderItem.ObjectState = ObjectState.Deleted;

            }
            _salesContext.ApplyStateChanges();
            _salesContext.SaveChanges();

            if (salesOrder.ObjectState == ObjectState.Deleted)
            {
                return Json(new { newLocation = "/Sales/Index" });
            }

            var messageToClient = Helpers.GetMessageToClient(salesOrder.ObjectState, salesOrder.CustomerName);
            salesOrderViewModel = ViewModels.Helpers.CreateSalesOrderViewModelFromSaleOrder(salesOrder);
            salesOrderViewModel.MessageToClient = messageToClient;

            return Json(new { salesOrderViewModel });
        }
    }
}
