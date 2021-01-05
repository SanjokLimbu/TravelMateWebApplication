using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelMate.ModelFolder.IdentityModel;

namespace TravelMate.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found.";
                return View("Error");
            }
            var model = new EditRoleModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in await _userManager.GetUsersInRoleAsync(role.Name))
            {
                model.Users.Add(user.UserName);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found.";
                return View("Error");
            }
            var model = new List<UserRoleModel>();
            var allUser = _userManager.Users;
            var usersInThisRole = await _userManager.GetUsersInRoleAsync(role.Name);
            foreach(var user in allUser)
            {
                var userRoleModel = new UserRoleModel
                {
                    UserId = user.Id,
                    UserName = user.Name,
                    IsSelected = false
                };
                if (usersInThisRole.Any(userInThisRole => userInThisRole.Id == userRoleModel.UserId))
                    userRoleModel.IsSelected = true;
                model.Add(userRoleModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel roleModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = roleModel.RoleName
                };
                var result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(roleModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found.";
                return View("Error");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("Error");
            }
            for(int i =0; i < model.Count; i++)
            {
               var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"The role with Id = {id} cannot be found";
                return View("Error");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("ListRoles");
            }
        }
    }
}
