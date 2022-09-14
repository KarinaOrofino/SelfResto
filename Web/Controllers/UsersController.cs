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
using KO.Web.Models.Account;
using System.Linq;
using Framework.Common;

namespace Web.Controllers.Home
{
    [Route("[controller]/[action]")]
    public class UsersController : BaseController
    {
        private IUsersService IUsersService { get; set; }

        private IGenericService IGenericService { get; set; }

        public UsersController(IUsersService usersService, IGenericService GenericService)
        {
            this.IUsersService = usersService;
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
            UserViewModel userVM = new UserViewModel();
            return View(userVM);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Detail(int id)
        {
            UserViewModel userVM = new UserViewModel();

            try
            {
                User user = IGenericService.GetById<User>(id);

                this.LoadViewModel(userVM, user);
            }
            catch (Exception ex)
            {
                log.Error("$Error al buscar el usuario con id= " + id + ", Error:", ex);
                return Redirect("/Home/Error");
            }

            return View(userVM);
        }

        #endregion

        #region List View Methods

        [HttpGet]
        public JsonResult GetAll()
        {
            JsonData jsonData = new();

            try
            {
                List<User> usersList = IGenericService.GetAll<User>().ToList();

                List<UserViewModel> usersVMList = new();

                usersVMList = usersList.Select(user => new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    AccessTypeName = user.AccessType.Name,
                    Active = user.Active

                }).OrderBy(user => user.Surname).ToList();

                jsonData.content = usersVMList;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo obtener la lista de usuarios. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo obtener la lista de usuarios";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Inactivate(int id)
        {
            JsonData jsonData = new();

            try
            {

                IGenericService.Deactivate<User>(id);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo inactivar el usuario con id: " + id, ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo inactivar el usuario";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Activate(int id)
        {
            JsonData jsonData = new();

            try
            {

                IGenericService.Activate<User>(id);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo activar el usuario con id: " + id, ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo activar el usuario";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Export(string searchField, bool? state)
        {
            try
            {

                List<User> userList = IUsersService.GetAllFiltered(searchField, state).ToList();

                List<UserViewModel> userVMList = userList.Select(user => new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Password = user.Password,
                    AccessTypeName = user.AccessTypeName,
                    Active = user.Active

                }).OrderBy(user => user.Surname).ToList();

                var reducedList = userVMList.Select(user => new
                {
                    user.Name,
                    user.Surname,
                    user.AccessTypeName,
                    user.Email,
                    user.Password,
                    Estado = user.Active == true ? Global.ActiveMasc : Global.InactiveMasc,
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

        public JsonResult GetAccessTypes()
        {
            JsonData jsonData = new();
            List<AccessTypeViewModel> accessesVM = new();

            try
            {

                List<AccessType> accesses = IGenericService.GetAll<AccessType>().ToList();
                accessesVM = accesses.Select(acc => new AccessTypeViewModel() {
                    Id = acc.Id,
                    Name = acc.Name
                }).OrderBy(a=>a.Name).ToList();

                jsonData.content = accessesVM;
                jsonData.result = JsonData.Result.Ok;
            }


            catch (Exception ex)
            {
                log.Error("No se pudo obtener la lista de tipos de acceso. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        public JsonResult Add(UserViewModel userVM)
        {
            JsonData jsonData = new();
            User user = new();

            try
            {

                var ExistingUser = IGenericService.Get<User>(u => u.Email.Equals(userVM.Email));

                if (ExistingUser != null)
                {

                    Response.StatusCode = Constants.ERROR_HTTP;
                    jsonData.result = JsonData.Result.ModelValidation;
                    jsonData.errorUi = "Ya existe un usuario registrado con ese email";
                    return Json(jsonData);
                }

                user.Name = userVM.Name;
                user.Surname = userVM.Surname;
                user.Email = userVM.Email;
                user.Password = userVM.Password;
                user.Access_Type = userVM.AccessType;
                user.CreationDate = DateTime.Now;
                user.CreationUser = UserUtils.GetId(User);
                user.Active = true;

                IGenericService.Add(user);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo guardar el usuario. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.errorUi = "No se pudo guardar el usuario";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Update(UserViewModel userVM)
        {
            JsonData jsonData = new();
            User user = new();

            try
            {
                user.Id = userVM.Id;
                user.Name = userVM.Name;
                user.Surname = userVM.Surname;
                user.Email = userVM.Email;
                user.Password = userVM.Password;
                user.Access_Type = userVM.AccessType;
                user.UpdateDate = DateTime.Now;
                user.UpdateUser = UserUtils.GetId(User);
                user.Active = userVM.Active;

                IGenericService.Update<User>(user);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo actualizar el usuario: " + user.Surname + ", Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        #endregion

        #region Private Methods

        private UserViewModel LoadViewModel(UserViewModel userVM, User user) {

            userVM.Id = user.Id;
            userVM.Name = user.Name;
            userVM.Surname = user.Surname;
            userVM.Active = user.Active;
            userVM.Email = user.Email;
            userVM.Password = user.Password;
            userVM.AccessType = user.Access_Type;
            userVM.AccessTypeName = user.AccessType.Name;

            return userVM;
        }
        #endregion
    }
}
