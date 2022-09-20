using Framework.Utils;
using Framework.Web;
using Microsoft.AspNetCore.Mvc;
using KO.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using KO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using KO.Entities;
using KO.Web.Models.Order;
using System.Linq;
using Framework.Common;
using KO.Web.Models.Table;

namespace Web.Controllers.Home
{
    [Route("[controller]/[action]")]
    public class HomeController : BaseController
    {
        private IConfiguration Configuration { get; set; }

        private IUsersService IUsersService { get; set; }

        private IGenericService IGenericService { get; set; }

        private readonly IAppInfo _appInfo;

        public HomeController(IConfiguration configuration, IUsersService usersService, IAppInfo appInfo, IGenericService GenericService)
        {
            _appInfo = appInfo;
            this.Configuration = configuration;
            this.IUsersService = usersService;
            this.IGenericService = GenericService;
        }


        [Route("{id}")]
        [HttpGet]
        public IActionResult IndexClient(int id)
        {
            Order order = IGenericService.GetById<Order>(id);

            OrderViewModel orderVM = new();
            orderVM.Id = order.Id;
            orderVM.TableId = order.TableId;
            orderVM.TableNumber = order.Table.Number;

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

        [HttpGet]
        public IActionResult Error()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }
            return View();
        }



        public Task<JsonData> JSGlobales(/*string cultura*/)
        {
            JsonData jsonData = new ();

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

            JsonData jsonData = new ();

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
        public JsonResult GetAllTables()
        {
            JsonData jsonData = new JsonData();
            List<TableViewModel> tablesVM = new();

            try
            {

                List<Table> tables = IGenericService.GetAll<Table>(t => t.Active).ToList();
                List<Order> orders = IGenericService.GetAll<Order>().ToList();

                 var query =
                    from table in tables
                    join order in orders on table.Id equals order.TableId into gj
                    from subset in gj.DefaultIfEmpty()
                    select new TableViewModel
                    {
                        Id = table.Id,
                        Number = table.Number,
                        WaiterName = string.Concat(table.WaiterUser.Name.Substring(0,1),". ", table.WaiterUser.Surname),
                        WaiterBackUpName = string.Concat(table.WaiterBackUpUser.Name.Substring(0, 1), ". ", table.WaiterBackUpUser.Surname),
                        Active = subset?.Active == null ? false : subset.Active,
                        Closed = subset?.Id == null ? true : false
                    };

                List<TableViewModel> allTables = query.Where(q=>(q.Active && !q.Closed) || q.Closed).ToList();

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

        [HttpGet]
        public Task<JsonData> GetActiveOrders()
        {
            JsonData jsonData = new();
            List<OrderViewModel> OrderViewModels = new();

            try
            {
                List<Order> Orders = IGenericService.GetAll<Order>(o => o.Active == true && o.OrderDetails.Count > 0 && !o.OrderDetails.All(od=>od.MenuItem.CategoryId >=8)).ToList();

                OrderViewModels = Orders.Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    TableNumber = o.Table.Number,
                    RequestedTime = o.CreationDate,
                    RequestedTimeString = o.CreationDate.ToString("HH:mm"),
                    WaiterName = o.Table.WaiterUser.Name +" "+ o.Table.WaiterUser.Surname,
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailViewModel() {
                        Id=od.Id,
                        MenuItemCategoryId = od.MenuItem.CategoryId,
                        MenuItemId = od.MenuItemId,
                        MenuItemName = od.MenuItem.Name,
                        RelatedMenuItemId = od.RelatedMenuItemId,
                        RelatedMenuItemName = od.RelatedMenuItemId != null ? od.RelatedMenuItem.Name : "",
                        Quantity = od.Quantity,
                        StateTypeId = od.StateTypeId,
                    }).OrderBy(OrderDetail=>OrderDetail.StateTypeId).ToList(),
                }).OrderBy(o=>o.RequestedTime).ToList();

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
                od.StateTypeId = itemState;
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

    }
}
