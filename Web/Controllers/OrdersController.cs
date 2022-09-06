using Framework.Common;
using Framework.Utils;
using Framework.Web;
using KO.Entities;
using KO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        public Task<JsonData> AddItem(byte orderId, byte itemId, byte quantity)
        {
            JsonData jsonData = new JsonData();

            try
            {

                OrderDetail orderDetail = new();
                orderDetail.MenuItemId = itemId;
                orderDetail.OrderId = orderId;
                orderDetail.CreationUser = UserUtils.GetId(User);
                orderDetail.CreationDate = DateTime.Now;
                orderDetail.Quantity = quantity;
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
    }
}

