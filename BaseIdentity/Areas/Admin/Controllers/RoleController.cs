using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BaseIdentity.PresentationLayer.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppRole appRole = new AppRole()
                {
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(appRole);
                if (result.Succeeded)
                {
                    var url = Url.RouteUrl("areas", new { controller = "Role", action = "Index", area = "Admin" });
                    return Redirect(url);
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                var url2 = Url.RouteUrl("areas", new { controller = "Role", action = "Index", area = "Admin" });
                return Redirect(url2);
            }
            var url = Url.RouteUrl("areas", new { controller = "Role", action = "Index", area = "Admin" });
            return Redirect(url);
        }


        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            return View(values);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(AppRole appRole)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == appRole.Id);
            values.Name = appRole.Name;
            //values.NormalizedName = appRole.NormalizedName;
            var result = await _roleManager.UpdateAsync(values);
            if (result.Succeeded)
            {
                var url = Url.RouteUrl("areas", new { controller = "Role", action = "Index", area = "Admin" });
                return Redirect(url);
            }
            return View();

        }

        public IActionResult UserList()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            var user =await _userManager.FindByIdAsync(id);
            var roles = _roleManager.Roles.ToList();
            TempData["UserID"] = user.Id;
            var userRoles = await _userManager.GetRolesAsync(user);

            List<RoleAssignViewModel> models = new List<RoleAssignViewModel>();

            foreach (var item in roles)
            {
                RoleAssignViewModel roleAssignViewModel = new RoleAssignViewModel();
                roleAssignViewModel.Name = item.Name;
                roleAssignViewModel.RoleID = item.Id;
                roleAssignViewModel.Exists = userRoles.Contains(item.Name);
                models.Add(roleAssignViewModel);

            }

            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> model)
        {
            var userid = (int)TempData["UserId"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);
            foreach (var item in model)
            {
                if (item.Exists)
                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }
            var url = Url.RouteUrl("areas", new { controller = "Role", action = "UserList", area = "Admin" });
            return Redirect(url);
        }
    }
}

