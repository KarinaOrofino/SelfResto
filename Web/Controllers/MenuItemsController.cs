using Framework.Common;
using Framework.Web;
using KO.Entities;
using KO.Services.Interfaces;
using KO.Web.Models.MenuItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Controllers.MenuItems
{
    [Route("[controller]/[action]")]
    public class MenuItemsController : BaseController
    {
        private IConfiguration Configuration { get; set; }

        private IUsersService IUsersService { get; set; }

        private IMenuItemsService IMenuItemsService { get; set; }

        public MenuItemsController(IConfiguration configuration, IUsersService usersService, IMenuItemsService menuItemsService)
        {
            this.Configuration = configuration;
            this.IUsersService = usersService;
            this.IMenuItemsService = menuItemsService;
        }

        [HttpGet]
        public IActionResult List()
        {
            MenuItemViewModel MIVM = new();
            return View(MIVM);
        }

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
                List<MenuItem> menuItems = IMenuItemsService.GetAll();

                MIVMList = menuItems.Select(mi => new MenuItemViewModel()
                {
                    Id = mi.Id,
                    Order = mi.Order,
                    CategoryId = mi.CategoryId,
                    Name = mi.Name,
                    Description = mi.Description,
                    Price = mi.Price,
                    ImageUrl = mi.ImageUrl,
                    Active = mi.Active,

                }).OrderBy(mi => mi.Order).ToList();

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
        public JsonResult GetAllMenuItemsFiltered(int category, bool active)
        {
            JsonData jsonData = new JsonData();
            List<MenuItemViewModel> MIVMList = new();

            try
            {
                List<MenuItem> menuItems = IMenuItemsService.GetAll();

                MIVMList = menuItems.Select(mi => new MenuItemViewModel()
                {
                    Id = mi.Id,
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
                    MenuItems = IMenuItemsService.GetAllFiltered(mi.Id.ToString(),true).Select(mi => new MenuItemViewModel()
                    {
                        Id = mi.Id,
                        Order = mi.Order,
                        CategoryId = mi.CategoryId,
                        Name = mi.Name,
                        Description = mi.Description,
                        Price = mi.Price,
                        ImageUrl = mi.ImageUrl,
                        Active = mi.Active,
                        Quantity = 0
                    }).OrderBy(mi=>mi.Order).ToList(),
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

    }
}

