using Framework.Utils;
using Framework.Web;
using Microsoft.AspNetCore.Mvc;
using KO.Resources;
using System;
using System.Collections.Generic;
using KO.Services.Interfaces;
using KO.Entities;
using System.Linq;
using Framework.Common;
using KO.Web.Models.Table;

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

        #region Views
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Detail()
        {
            TableViewModel tableVM = new TableViewModel();
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

        #region List View Methods

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

                IGenericService.Deactivate<Table>(id);

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

        public JsonResult Activate(int id)
        {
            JsonData jsonData = new();

            try
            {

                IGenericService.Activate<Table>(id);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo activar la mesa con id: " + id, ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo activar la mesa";
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
                    Active = table.Active

                }).OrderBy(table => table.Number).ToList();

                var reducedList = tableVMList.Select(table => new
                {
                    table.Number,
                    table.Name,
                    table.Description,
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

        #region Detail View Methods

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

                else { 

                table.Name = tableVM.Name;
                table.Number = tableVM.Number;
                table.Description = tableVM.Description;
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

        #region Private Methods

        private TableViewModel LoadViewModel(TableViewModel tableVM, Table table)
        {

            tableVM.Id = table.Id;
            tableVM.Number = table.Number;
            tableVM.Name = table.Name;
            tableVM.Description = table.Description;
            tableVM.Active = table.Active;

            return tableVM;
        }
        #endregion
    }
}
