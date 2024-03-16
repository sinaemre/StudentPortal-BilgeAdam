using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal_Core.DTO_s.RoleDTO;
using StudentPortal_Core.Entities.UserEntites.Concrete;

namespace StudentPortal_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult CreateRole() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
                if (result.Succeeded)
                {
                    TempData["Success"] = "Rol başarılı bir şekilde eklendi!";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        TempData["Error"] = error.Description;
                    }
                    return View(model);
                }
            }

            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> UpdateRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var model = new UpdateRoleDTO { Id = role.Id, RoleName = role.Name };
                return View(model);
            }
            TempData["Error"] = "Rol bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRole(UpdateRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Name = model.RoleName;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        TempData["Success"] = "Rol güncellendi!";
                        return RedirectToAction("Index");
                    }
                    TempData["Error"] = "Rol güncellenemedi!";
                    return View(model);
                }
                TempData["Error"] = "Rol bulunamadı!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> AssignedUser(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                List<AppUser> hasRole = new List<AppUser>();
                List<AppUser> hasNotRole = new List<AppUser>();

                var users = await _userManager.Users.ToListAsync();

                foreach (var user in users)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        hasRole.Add(user);
                    }
                    else
                    {
                        hasNotRole.Add(user);
                    }
                }

                var model = new AssignedRoleDTO
                {
                    Role = role,
                    HasRole = hasRole,
                    HasNotRole = hasNotRole,
                    RoleName = role.Name
                };

                return View(model);
            }
            TempData["Error"] = "Rol bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignedUser(AssignedRoleDTO model)
        {
            IdentityResult result = new IdentityResult();

            foreach (var userId in model.AddIds ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                result = await _userManager.AddToRoleAsync(user, model.RoleName);
            }

            foreach (var userId in model.DeleteIds ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            }

            if (result.Succeeded)
            {
                TempData["Success"] = "Rol işlemleri başarılı bir şekilde yapıldı!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Rol işlemleri yapılamadı!";
            return View(model);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var hasRole = new List<AppUser>();
                var users = await _userManager.Users.ToListAsync();

                foreach (var user in users)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        hasRole.Add(user);
                    }
                }

                if (hasRole.Count() == 0)
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        TempData["Success"] = "Rol silindi!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Error"] = "Rol silinemedi!";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["Error"] = "Bu rolde kullanıcılar var. Silemezsiniz! Eğer silmek istiyosanız önce kullanıcıları bu rolden kaldırın!";
                    return RedirectToAction("AssignedUser", new { id = role.Id });
                }
            }
            TempData["Error"] = "Rol bulunamadı!";
            return RedirectToAction("Index");

        }
    }
}
