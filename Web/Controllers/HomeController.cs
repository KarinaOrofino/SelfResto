using Framework.Common;
using Framework.Utils;
using Framework.Web;
using KO.Entities;
using KO.Resources;
using KO.Services.Interfaces;
using KO.Web.Models.Order;
using KO.Web.Models.Table;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace Web.Controllers.Home
{
    [Route("[controller]/[action]")]
    public class HomeController : BaseController
    {
        private IConfiguration Configuration { get; set; }

        private IUsersService IUsersService { get; set; }

        private IGenericService IGenericService { get; set; }

        private IPaymentsService IPaymentsService { get; set; }

        private readonly IAppInfo _appInfo;

        public HomeController(IConfiguration configuration, IUsersService usersService, IAppInfo appInfo, IGenericService GenericService, IPaymentsService paymentsService)
        {
            _appInfo = appInfo;
            this.Configuration = configuration;
            this.IUsersService = usersService;
            this.IGenericService = GenericService;
            this.IPaymentsService = paymentsService;
        }

        #region GENERAL
        public Task<JsonData> JSGlobales(/*string cultura*/)
        {
            JsonData jsonData = new();

            try
            {
                ResourceManager MyResourceClass = new(typeof(Global));
                ResourceSet resourceSet = MyResourceClass.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

                List<KeyValuePair<string, string>> data = new();
                foreach (DictionaryEntry resourceItem in resourceSet)
                {
                    data.Add(new KeyValuePair<string, string>(resourceItem.Key.ToString(), resourceItem.Value.ToString()));
                }

                jsonData.content = data;
                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                jsonData.content = new { mensaje = ex };
                jsonData.result = JsonData.Result.Ok;
            }

            return Task.FromResult(jsonData);
        }
        public Task<JsonData> ObtenerVersion()
        {
            JsonData jsonData = new();

            try
            {
                jsonData.content = "build: " + _appInfo.GetVersion() + " - " + _appInfo.GetBuildDate();
                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error(Global.GenericError, ex);
                jsonData.result = JsonData.Result.Error;
                jsonData.error = ex.ToString();
            }

            return Task.FromResult(jsonData);
        }

        [HttpGet]
        public IActionResult Error()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }
            return View();
        }

        #endregion

        #region VIEWS
        [Route("{id}")]
        [HttpGet]
        public IActionResult IndexClient(int id)
        {
            Order order = IGenericService.GetById<Order>(id);

            OrderViewModel orderVM = new();
            orderVM.Id = order.Id;
            orderVM.Call = order.Call;
            orderVM.PaymentRequest = order.PaymentRequest;
            orderVM.TableId = order.TableId;
            orderVM.TableNumber = order.Table.Number;
            orderVM.ItemsTotalQuantity = order.OrderDetails.Count;

            return View(orderVM);
        }

        [HttpGet]
        public IActionResult IndexEmployee()
        {
            OrderViewModel ovm = new();
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }
            return View(ovm);
        }

        #endregion

        #region ADMIN
        [HttpGet]
        public JsonResult GetAllTables()
        {
            JsonData jsonData = new JsonData();
            List<TableViewModel> tablesVM = new();

            try
            {
                List<Table> tables = IGenericService.GetAll<Table>().ToList();
                List<Order> orders = IGenericService.GetAll<Order>().ToList();

                var query =
                   from table in tables
                   join order in orders on table.Id equals order.TableId into gj
                   from subset in gj.DefaultIfEmpty()
                   select new TableViewModel
                   {
                       Id = table.Id,
                       Number = table.Number,
                       WaiterId = table.WaiterId,
                       WaiterName = table.WaiterId != null ? string.Concat(table.WaiterUser.Name.Substring(0, 1), ". ", table.WaiterUser?.Surname) : "",
                       WaiterBackUpId = table.WaiterBackUpId,
                       WaiterBackUpName = table.WaiterBackUpId != null ? string.Concat(table.WaiterBackUpUser.Name.Substring(0, 1), ". ", table.WaiterBackUpUser?.Surname) : "",
                       Active = table.Active,
                       Closed = (subset?.Id == null || !subset.Active)
                   };

                List<TableViewModel> allTables = query.GroupBy(tb => new { tb.Number }).
                   Select(x =>
                   {
                       var result = x.Last();
                       return result;
                   }).ToList();

                jsonData.content = allTables;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudieron obtener las mesas. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudieron obtener las mesas.";
            }

            return Json(jsonData);

        }
        #endregion

        #region KITCHEN / WAITER
        [HttpGet]
        public Task<JsonData> GetActiveOrders()
        {
            JsonData jsonData = new();
            List<OrderViewModel> OrderViewModels = new();

            try
            {
                List<Order> Orders = IGenericService.GetAll<Order>(o => o.Active == true).ToList();

                OrderViewModels = Orders.Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    Call = o.Call,
                    PaymentRequest = o.PaymentRequest,
                    TableId = o.Table.Id,
                    TableNumber = o.Table.Number,
                    RequestedTime = o.CreationDate,
                    RequestedTimeString = o.CreationDate.ToString("HH:mm"),
                    WaiterName = o.Table.WaiterId != null ? o.Table.WaiterUser.Name + " " + o.Table.WaiterUser.Surname : "",
                    WaiterBackUpName = o.Table.WaiterBackUpId != null ? o.Table.WaiterBackUpUser.Name + " " + o.Table.WaiterBackUpUser.Surname : "",
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailViewModel()
                    {
                        Id = od.Id,
                        MenuItemCategoryId = od.MenuItem.CategoryId,
                        MenuItemId = od.MenuItemId,
                        MenuItemName = od.MenuItem.Name,
                        RelatedMenuItemId = od.RelatedMenuItemId,
                        RelatedMenuItemName = od.RelatedMenuItemId != null ? od.RelatedMenuItem.Name : "",
                        Quantity = od.Quantity,
                        OrderDetailStatusId = od.OrderDetailStatusId,
                    }).OrderBy(od => od.OrderDetailStatusId).ToList(),
                }).OrderBy(o => o.RequestedTime).ToList();

                jsonData.result = JsonData.Result.Ok;
                jsonData.content = OrderViewModels;
            }
            catch (Exception ex)
            {
                log.Error("No se pudieron obtener las órdenes. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudieron obtener las órdenes";
            }

            return Task.FromResult(jsonData);
        }

        [HttpPost]
        public JsonData ChangeItemState(int itemId, int itemState)
        {
            JsonData jsonData = new();

            try
            {
                OrderDetail od = IGenericService.GetById<OrderDetail>(itemId);
                od.OrderDetailStatusId = itemState;
                IGenericService.Update<OrderDetail>(od);

                jsonData.result = JsonData.Result.Ok;

            }
            catch (Exception ex)
            {
                log.Error("No se pudo cambiar el estado del item. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudo cambiar el estado del item";
            }

            return jsonData;
        }
        #endregion

        #region CLIENT
        [HttpPost]
        public JsonData CallWaiter(int orderId)
        {
            JsonData jsonData = new();

            try
            {
                Order order = IGenericService.GetById<Order>(orderId);
                order.Call = true;
                order.UpdateUser = UserUtils.GetId(User);
                order.UpdateDate = DateTime.Now;

                IGenericService.Update<Order>(order);

                jsonData.result = JsonData.Result.Ok;

            }
            catch (Exception ex)
            {
                log.Error("No se pudo llamar al mozo. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudo llamar al mozo";
            }

            return jsonData;
        }

        [HttpPost]
        public JsonResult RequestBill(int orderId)
        {
            JsonData jsonData = new();

            try
            {
                Order order = IGenericService.GetById<Order>(orderId);

                if (order.OrderDetails.Any(od => od.OrderDetailStatusId < 4))
                {

                    jsonData.result = JsonData.Result.Error;
                    Response.StatusCode = Constants.ERROR_HTTP;
                    jsonData.errorUi = "No se puede pedir la cuenta porque tiene pedidos pendientes";
                    return Json(jsonData);
                }
                else
                {
                    order.PaymentRequest = true;
                    order.UpdateUser = UserUtils.GetId(User);
                    order.UpdateDate = DateTime.Now;

                    foreach (OrderDetail od in order.OrderDetails)
                    {
                        od.Active = false;
                        od.UpdateUser = UserUtils.GetId(User);
                        od.UpdateDate = DateTime.Now;
                    }

                    Payment payment = new();
                    payment.OrderId = orderId;
                    payment.CreationUser = UserUtils.GetId(User);
                    payment.CreationDate = DateTime.Now;
                    payment.TotalAmount = order.OrderDetails.Sum(od => (od.Quantity * od.MenuItem.Price)) + order.OrderDetails.Where(od => od.RelatedMenuItem != null).Sum(od => (od.Quantity * od.RelatedMenuItem.Price));

                    IPaymentsService.RequestBill(order, payment);

                    jsonData.result = JsonData.Result.Ok;
                }
            }
            catch (Exception ex)
            {
                log.Error("No se pudo pedir la cuenta. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudo pedir la cuenta";
            }

            return Json(jsonData);
        }

        #endregion

        #region WAITER
        [HttpPost]
        public JsonData CloseCall(int orderId)
        {
            JsonData jsonData = new();

            try
            {
                Order order = IGenericService.GetById<Order>(orderId);
                order.Call = false;
                order.UpdateUser = UserUtils.GetId(User);
                order.UpdateDate = DateTime.Now;

                IGenericService.Update<Order>(order);

                jsonData.result = JsonData.Result.Ok;

            }
            catch (Exception ex)
            {
                log.Error("No se pudo cancelar la llamada. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudo cancelar la llamada";
            }

            return jsonData;
        }

        [HttpPost]
        public JsonData CloseOrder(int orderId)
        {
            JsonData jsonData = new();

            try
            {
                Order order = IGenericService.GetById<Order>(orderId);
                order.PaymentRequest = false;
                order.Active = false;
                order.UpdateUser = UserUtils.GetId(User);
                order.UpdateDate = DateTime.Now;

                IGenericService.Update<Order>(order);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo cerrar la orden. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudo cerrar la orden";
            }

            return jsonData;
        }

        #endregion
    }
}




