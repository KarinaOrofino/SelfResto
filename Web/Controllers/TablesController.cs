using Framework.Common;
using Framework.Utils;
using Framework.Web;
using KO.Entities;
using KO.Resources;
using KO.Services.Interfaces;
using KO.Web.Models.Account;
using KO.Web.Models.Table;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Controllers.Tables
{
    [Route("[controller]/[action]")]
    public class TablesController : BaseController
    {
        private ITablesService ITablesService { get; set; }

        private IGenericService IGenericService { get; set; }

        public TablesController(ITablesService tablesService, IGenericService GenericService)
        {
            this.ITablesService = tablesService;
            this.IGenericService = GenericService;
        }

        #region VIEWS
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Detail()
        {
            TableViewModel tableVM = new();
            return View(tableVM);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Detail(int id)
        {
            TableViewModel tableVM = new TableViewModel();

            try
            {
                Table table = IGenericService.GetById<Table>(id);

                this.LoadViewModel(tableVM, table);
            }
            catch (Exception ex)
            {
                log.Error("$Error al buscar la mesa con id= " + id + ", Error:", ex);
                return Redirect("/Home/Error");
            }

            return View(tableVM);
        }

        #endregion

        #region LISTVIEW

        [HttpGet]
        public JsonResult GetAll()
        {
            JsonData jsonData = new();

            try
            {
                List<Table> tablesList = IGenericService.GetAll<Table>().ToList();

                List<TableViewModel> tablesVMList = new();

                tablesVMList = tablesList.Select(table => new TableViewModel()
                {
                    Id = table.Id,
                    Number = table.Number,
                    Name = table.Name,
                    Description = table.Description,
                    WaiterId = table.WaiterId,
                    WaiterName = table.WaiterId != null ? table.WaiterUser.Name + " " + table.WaiterUser.Surname : "",
                    WaiterBackUpId = table.WaiterBackUpId,
                    WaiterBackUpName = table.WaiterId != null ? table.WaiterBackUpUser.Name + " " + table.WaiterBackUpUser.Surname : "",
                    Active = table.Active

                }).OrderBy(table => table.Number).ToList();

                jsonData.content = tablesVMList;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo obtener la lista de mesas. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo obtener la lista de mesas";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Inactivate(int id)
        {
            JsonData jsonData = new();

            try
            {
                Order order = IGenericService.Get<Order>(o => o.TableId == id && o.Active);

                if (order != null)
                {
                    Response.StatusCode = Constants.ERROR_HTTP;
                    jsonData.result = JsonData.Result.Error;
                    jsonData.errorUi = "No se puede inactivar la mesa porque tiene una orden activa";
                    return Json(jsonData);
                }

                Table table = IGenericService.GetById<Table>(id);
                table.Active = false;
                table.WaiterId = null;
                table.WaiterBackUpId = null;
                table.UpdateDate = DateTime.Now;
                table.UpdateUser = UserUtils.GetId(User);

                IGenericService.Update<Table>(table);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo inactivar la mesa con id: " + id, ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo inactivar la mesa";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        [HttpPost]
        public JsonResult Activate(TableViewModel tableVM)
        {
            JsonData jsonData = new();

            try
            {
                Table table = IGenericService.GetById<Table>(tableVM.Id);
                table.Active = true;
                table.WaiterId = tableVM.WaiterId;
                table.WaiterBackUpId = tableVM.WaiterBackUpId;
                table.UpdateDate = DateTime.Now;
                table.UpdateUser = UserUtils.GetId(User);

                IGenericService.Update<Table>(table);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo activar la mesa con id: " + tableVM.Id, ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo activar la mesa";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult GetAllWaiters()
        {
            JsonData jsonData = new();

            try
            {
                List<User> waiters = IGenericService.GetAll<User>(u => u.Access_Type == 11 && u.Active).ToList();

                List<UserViewModel> waitersVMList = new();

                waitersVMList = waiters.Select(user => new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,

                }).OrderBy(us => us.Surname).ToList();

                jsonData.content = waitersVMList;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo obtener la lista de mozos. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo obtener la lista de mozos";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Export(string searchField, bool? state)
        {
            try
            {
                List<Table> tableList = ITablesService.GetAllFiltered(searchField, state).ToList();

                List<TableViewModel> tableVMList = tableList.Select(table => new TableViewModel()
                {
                    Id = table.Id,
                    Number = table.Number,
                    Name = table.Name,
                    Description = table.Description,
                    WaiterName = table.WaiterId != null ? table.WaiterUser.Name + " " + table.WaiterUser.Surname : "",
                    WaiterBackUpName = table.WaiterId != null ? table.WaiterBackUpUser.Name + " " + table.WaiterBackUpUser.Surname : "",
                    Active = table.Active

                }).OrderBy(table => table.Number).ToList();

                var reducedList = tableVMList.Select(table => new
                {
                    table.Number,
                    table.Name,
                    table.Description,
                    table.WaiterName,
                    table.WaiterBackUpName,
                    Estado = table.Active == true ? Global.ActiveFem : Global.InactiveFem,
                }).ToList();

                var fileBytes = CreateExcelFile.CreateExcelDocumentAsByte(reducedList);
                log.Info("Método crear excel OK");
                this.HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                this.HttpContext.Response.Headers.Add("content-disposition", "attachment");
                return File(fileBytes, this.HttpContext.Response.ContentType);
            }
            catch (Exception ex)
            {
                this.HttpContext.Response.StatusCode = Constants.ERROR_HTTP;
                log.Error(ex);
            }

            return Content(string.Empty);
        }

        #endregion

        #region DETAIL VIEW

        public JsonResult Add(TableViewModel tableVM)
        {
            JsonData jsonData = new();
            Table table = new();

            try
            {
                var ExistingTable = IGenericService.Get<Table>(t => t.Number == tableVM.Number);

                if (ExistingTable != null)
                {

                    Response.StatusCode = Constants.ERROR_HTTP;
                    jsonData.result = JsonData.Result.ModelValidation;
                    jsonData.errorUi = "El NÚMERO de mesa ya existe.";
                    return Json(jsonData);
                }

                else
                {

                    table.Name = tableVM.Name;
                    table.Number = tableVM.Number;
                    table.Description = tableVM.Description;
                    table.WaiterId = tableVM.WaiterId;
                    table.WaiterBackUpId = tableVM.WaiterBackUpId;
                    table.CreationDate = DateTime.Now;
                    table.CreationUser = UserUtils.GetId(User);
                    table.Active = true;

                    IGenericService.Add(table);

                    jsonData.result = JsonData.Result.Ok;

                }
            }

            catch (Exception ex)
            {
                log.Error("No se pudo guardar la mesa. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo guardar la mesa";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Update(TableViewModel tableVM)
        {
            JsonData jsonData = new();
            Table table = new();

            try
            {
                table.Id = tableVM.Id;
                table.Number = tableVM.Number;
                table.Name = tableVM.Name;
                table.Description = tableVM.Description;
                table.WaiterId = tableVM.Active ? tableVM.WaiterId : null;
                table.WaiterBackUpId = tableVM.Active ? tableVM.WaiterBackUpId : null;
                table.UpdateDate = DateTime.Now;
                table.UpdateUser = UserUtils.GetId(User);
                table.Active = tableVM.Active;

                IGenericService.Update<Table>(table);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo actualizar la mesa: " + table.Number + ", Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        #endregion

        #region PRIVATE

        private TableViewModel LoadViewModel(TableViewModel tableVM, Table table)
        {
            tableVM.Id = table.Id;
            tableVM.Number = table.Number;
            tableVM.Name = table.Name;
            tableVM.WaiterId = table.WaiterId;
            tableVM.WaiterBackUpId = table.WaiterBackUpId;
            tableVM.Description = table.Description;
            tableVM.Active = table.Active;

            return tableVM;
        }
        #endregion
    }
}
