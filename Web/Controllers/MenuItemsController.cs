using Framework.Web;
using KO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Web.Controllers.MenuItems
{
    [Route("[controller]/[action]")]
    public class MenuItemsController : BaseController
    {
        private IConfiguration Configuration { get; set; }

        private IUsersService IUsersService { get; set; }

        public MenuItemsController(IConfiguration configuration, IUsersService usersService)
        {
            this.Configuration = configuration;
            this.IUsersService = usersService;
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
    }
}

