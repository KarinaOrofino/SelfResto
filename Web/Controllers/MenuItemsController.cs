using Framework.Common;
using Framework.Utils;
using Framework.Web;
using KO.Entities;
using KO.Resources;
using KO.Services.Interfaces;
using KO.Web.Models.MenuItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.MenuItems
{
    [Route("[controller]/[action]")]
    public class MenuItemsController : BaseController
    {
        private IConfiguration Configuration { get; set; }

        private IUsersService IUsersService { get; set; }

        private IGenericService IGenericService { get; set; }

        private IMenuItemsService IMenuItemsService { get; set; }

        public MenuItemsController(IConfiguration configuration, IUsersService usersService, IMenuItemsService menuItemsService, IGenericService genericService)
        {
            this.Configuration = configuration;
            this.IUsersService = usersService;
            this.IMenuItemsService = menuItemsService;
            this.IGenericService = genericService;
        }

        #region Views
        [HttpGet]
        public IActionResult ListToOrderMenu()
        {
            MenuItemViewModel MIVM = new();
            return View(MIVM);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult ListToOrder(int id)
        {

            MenuItemViewModel MIVM = new();
            Order order= IGenericService.GetById<Order>(id);
            MIVM.OrderId = order.Id;
            MIVM.TableId = order.TableId;

            return View(MIVM);
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Detail()
        {
            MenuItemViewModel MIVM = new MenuItemViewModel();
            return View(MIVM);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Detail(int id)
        {
            MenuItemViewModel MIVM = new MenuItemViewModel();

            try
            {
                MenuItem mItem = IGenericService.GetById<MenuItem>(id);

                this.LoadViewModel(MIVM, mItem);
            }
            catch (Exception ex)
            {
                log.Error("$Error al buscar el item con id= " + id + ", Error:", ex);
                return Redirect("/Home/Error");
            }

            return View(MIVM);
        }
        #endregion

        #region Items to Order

        [HttpGet]
        public JsonResult GetAllCategories()
        {
            JsonData jsonData = new JsonData();

            try
            {
                List<Category> categories = IMenuItemsService.GetAllCategories();

                jsonData.content = categories;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudieron obtener las categorias. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudieron obtener las categorias";
            }

            return Json(jsonData);

        }

        [HttpGet]
        public JsonResult GetAll()
        {
            JsonData jsonData = new JsonData();
            List<MenuItemViewModel> MIVMList = new();

            try
            {
                List<MenuItem> menuItems = IGenericService.GetAll<MenuItem>().ToList();

                MIVMList = menuItems.Select(mi => new MenuItemViewModel()
                {
                    Id = mi.Id,
                    VisualizationOrder = mi.VisualizationOrder,
                    CategoryId = mi.CategoryId,
                    CategoryName = mi.Category.Name,
                    Name = mi.Name,
                    Description = mi.Description,
                    Price = mi.Price,
                    ImageUrl = mi.ImageUrl,
                    Active = mi.Active,

                }).OrderBy(mi => mi.CategoryId).ThenBy(mi=>mi.VisualizationOrder).ToList();

                jsonData.content = MIVMList;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudieron obtener los items de menú. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudieron obtener los items de menú";
            }

            return Json(jsonData);

        }

        [HttpPost]
        public JsonResult GetAllMenuItemsFilteredByCatId(int catId, bool active)
        {
            JsonData jsonData = new JsonData();
            List<MenuItemViewModel> MIVMList = new();

            try
            {
                List<MenuItem> menuItems = IMenuItemsService.GetAllFilteredByCatId(catId, active);

                MIVMList = menuItems.Select(mi => new MenuItemViewModel()
                {
                    Id = mi.Id,
                    VisualizationOrder = mi.VisualizationOrder,
                    CategoryId = mi.CategoryId,
                    Name = mi.Name,
                    Description = mi.Description,
                    Price = mi.Price,
                    ImageUrl = mi.ImageUrl,
                    Active = mi.Active,

                }).ToList();

                jsonData.content = MIVMList;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudieron obtener los items de menú. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudieron obtener los items de menú";
            }

            return Json(jsonData);

        }

        [HttpPost]
        public JsonResult GetAllCatAndMenuItems()
        {
            JsonData jsonData = new JsonData();
            List<CategoryViewModel> CategoriesVMList = new();


            try
            {
                List<Category> CategoriesList = IMenuItemsService.GetAllCategories();

                CategoriesVMList = CategoriesList.Select(mi => new CategoryViewModel()
                {
                    Id = mi.Id,
                    Name = mi.Name,
                    CategoryImageUrl = mi.CategoryImageUrl,
                    MenuItems = IMenuItemsService.GetAllFilteredByCatId(mi.Id,true).Select(mi => new MenuItemViewModel()
                    {
                        Id = mi.Id,
                        VisualizationOrder = mi.VisualizationOrder,
                        CategoryId = mi.CategoryId,
                        Name = mi.Name,
                        Description = mi.Description,
                        Price = mi.Price,
                        ImageUrl = mi.ImageUrl,
                        Active = mi.Active,
                        Quantity = 0
                    }).OrderBy(mi=>mi.VisualizationOrder).ToList(),
                        Active = mi.Active,
                }).ToList();

                jsonData.content = CategoriesVMList;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudieron obtener los items de menú. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudieron obtener los items de menú";
            }

            return Json(jsonData);

        }

        #endregion

        #region List View Methods

        public JsonResult Inactivate(int id)
        {
            JsonData jsonData = new();

            try
            {

                IGenericService.Deactivate<MenuItem>(id);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo inactivar el item con id: " + id, ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo inactivar el item";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Activate(int id)
        {
            JsonData jsonData = new();

            try
            {

                IGenericService.Activate<MenuItem>(id);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo activar el item con id: " + id, ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo activar el item";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Export(string searchField, bool? state)
        {
            try
            {

                List<MenuItem> menuItemList = IMenuItemsService.GetAllFiltered(searchField, state).ToList();

                List<MenuItemViewModel> MIVMList = menuItemList.Select(mi => new MenuItemViewModel()
                {
                    Id = mi.Id,
                    CategoryId = mi.CategoryId,
                    CategoryName = mi.CategoryName,
                    VisualizationOrder = mi.VisualizationOrder,
                    Name = mi.Name,
                    Description = mi.Description,
                    ImageUrl = mi.ImageUrl,
                    Price = mi.Price,
                    Active = mi.Active

                }).OrderBy(mi => mi.CategoryId).ToList();

                var reducedList = MIVMList.Select(mi => new
                {
                    mi.VisualizationOrder,
                    mi.CategoryName,
                    mi.Name,
                    mi.Description,
                    mi.ImageUrl,
                    mi.Price,
                    Estado = mi.Active == true ? Global.ActiveMasc : Global.InactiveMasc,
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

        public JsonResult Add(MenuItemViewModel MIVM)
        {
            JsonData jsonData = new();
            MenuItem mItem = new();

            try
            {
                var ExistingMenuItem = IGenericService.Get<MenuItem>(t => t.Name.Equals(MIVM.Name));

                if (ExistingMenuItem != null)
                {

                    Response.StatusCode = Constants.ERROR_HTTP;
                    jsonData.result = JsonData.Result.ModelValidation;
                    jsonData.errorUi = "Ya existe un ítem con ese nombre";
                    return Json(jsonData);
                }

                else
                {
                    mItem.VisualizationOrder = MIVM.VisualizationOrder;
                    mItem.CategoryId = MIVM.CategoryId;
                    mItem.Name = MIVM.Name;
                    mItem.Description = MIVM.Description;
                    mItem.Price = MIVM.Price;
                    mItem.ImageUrl = MIVM.ImageUrl;
                    mItem.CreationDate = DateTime.Now;
                    mItem.CreationUser = UserUtils.GetId(User);
                    mItem.Active = true;

                    IGenericService.Add(mItem);

                    jsonData.result = JsonData.Result.Ok;

                }
            }

            catch (Exception ex)
            {
                log.Error("No se pudo guardar el item: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo guardar el item";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Update(MenuItemViewModel MIVM)
        {
            JsonData jsonData = new();
            MenuItem mItem = new();

            try
            {
                mItem.Id = MIVM.Id;
                mItem.VisualizationOrder = MIVM.VisualizationOrder;
                mItem.CategoryId = MIVM.CategoryId;
                mItem.Name = MIVM.Name;
                mItem.Description = MIVM.Description;
                mItem.Price = MIVM.Price;
                mItem.ImageUrl = MIVM.ImageUrl;
                mItem.CreationDate = MIVM.CreationDate;
                mItem.CreationUser = MIVM.CreationUser;
                mItem.UpdateDate = DateTime.Now;
                mItem.UpdateUser = UserUtils.GetId(User);
                mItem.Active = MIVM.Active;

                IGenericService.Update<MenuItem>(mItem);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo actualizar el item: " + mItem.Name + ", Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        #endregion

        #region Private Methods

        private MenuItemViewModel LoadViewModel(MenuItemViewModel MIVM, MenuItem mItem)
        {
            MIVM.VisualizationOrder = mItem.VisualizationOrder;
            MIVM.CategoryId = mItem.CategoryId;
            MIVM.Id = mItem.Id;
            MIVM.Name = mItem.Name;
            MIVM.Description = mItem.Description;
            MIVM.Price = mItem.Price;
            MIVM.ImageUrl = mItem.ImageUrl;
            MIVM.Active = mItem.Active;
            MIVM.CreationDate = mItem.CreationDate;
            MIVM.CreationUser = mItem.CreationUser;

            return MIVM;
        }

        public async Task<Task> AddPicture()
        {

            try
            {
                var file = Request.Form.Files[0];

                var filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "")+ Configuration["ImagesPath"] + file.Name);
                var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
                fileStream.Close();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message + " StackTrace: " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}


