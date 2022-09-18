using Framework.Common;
using Framework.Utils;
using Framework.Web;
using KO.Entities;
using KO.Services.Interfaces;
using KO.Web.Models.Order;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.Orders
{
    [Route("[controller]/[action]")]
    public class OrdersController : BaseController
    {

        private IOrdersService IOrdersService { get; set; }

        private IGenericService IGenericService { get; set; }

        public OrdersController (IOrdersService OrdersService, IGenericService GenericService)
        {

            this.IOrdersService = OrdersService;
            this.IGenericService = GenericService;

        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Detail(int id) 
        {
            Order order = IGenericService.GetById<Order>(id);
            OrderViewModel ovm = new OrderViewModel();
            ovm.Id = order.Id;
            ovm.TableId = order.TableId;

            ovm.OrderDetails = order.OrderDetails.Select(od => new OrderDetailViewModel()
            {
                Id = od.Id,
                MenuItemCategoryId = od.MenuItem.CategoryId,
                MenuItemId = od.MenuItemId,
                MenuItemName = od.MenuItem.Name,
                MenuItemPicture = od.MenuItem.ImageUrl,
                RelatedMenuItemId = od.RelatedMenuItemId,
                RelatedMenuItemName = od.RelatedMenuItemId != null ? od.RelatedMenuItem.Name : "",
                Quantity = od.Quantity,
                UnitPrice = od.RelatedMenuItemId == null ? IGenericService.GetById<MenuItem>(od.MenuItemId).Price : (IGenericService.GetById<MenuItem>(od.MenuItemId).Price + IGenericService.GetById<MenuItem>(od.RelatedMenuItemId).Price),
                StateTypeName = od.StateType.Name,

            }).ToList();

            ovm.Active = order.Active;

            return View(ovm);
        }

        [HttpPost]
        public Task<JsonData> AddItem(int orderId, int itemId, byte quantity, int? idSalsa)
        {
            JsonData jsonData = new JsonData();

            try
            {

                OrderDetail orderDetail = new();
                orderDetail.OrderId = orderId;
                orderDetail.MenuItemId = itemId;
                orderDetail.Quantity = quantity;
                orderDetail.StateTypeId = 1;
                orderDetail.RelatedMenuItemId = idSalsa == null ? null : idSalsa;
                orderDetail.CreationUser = UserUtils.GetId(User);
                orderDetail.CreationDate = DateTime.Now;
                orderDetail.Active = true;

                IGenericService.Add(orderDetail);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo agregar el item a la orden. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudo agregar el item a la orden";
                return Task.FromResult(jsonData);
            }

            return Task.FromResult(jsonData);

        }

        private OrderViewModel LoadViewModel(OrderViewModel OrderVM, Order order)
        {
           


            return OrderVM;
        }
    }
}

