using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal_Core.DTO_s.AccountDTO;
using StudentPortal_Core.Entities.Concrete;
using StudentPortal_Core.Entities.UserEntites.Concrete;
using StudentPortal_DataAccess.Services.Interface;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using AutoMapper;

namespace StudentPortal_WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        //private readonly IStudentRepository _studentRepository;
        //private readonly ITeacherRepository _teacherRepository;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            //_studentRepository = studentRepository;
            //_teacherRepository = teacherRepository;
            //_roleManager = roleManager;
            //_mapper = mapper;
        }

        #region Register
        //[Authorize(Roles = "admin, hrPersonal")]
        //public async Task<IActionResult> Register()
        //{
        //    var roles = await _roleManager.Roles.ToListAsync();
        //    var model = new RegisterDTO() { Roles = roles };

        //    return View(model);
        //}

        //[HttpPost, ValidateAntiForgeryToken]
        //[Authorize(Roles = "admin, hrPersonal")]
        //public async Task<IActionResult> Register(RegisterDTO model)
        //{
        //    var roles = await _roleManager.Roles.ToListAsync();
        //    model.Roles = roles;
        //    if (ModelState.IsValid)
        //    {
        //        var appUser = new AppUser
        //        {
        //            UserName = String.Join("", model.FirstName.Normalize(NormalizationForm.FormD)
        //.Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).ToLower().Replace('ı', 'i') + "." + String.Join("", model.LastName.Normalize(NormalizationForm.FormD)
        //.Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).ToLower().Replace('ı', 'i'),
        //            Email = model.Email,
        //            FirstName = model.FirstName,
        //            LastName = model.LastName,
        //            BirthDate = model.BirthDate
        //        };

        //        appUser.PasswordHash = _passwordHasher.HashPassword(appUser, model.Password);

        //        IdentityResult result = await _userManager.CreateAsync(appUser);
        //        if (result.Succeeded)
        //        {
        //            var role = await _roleManager.FindByIdAsync(model.RoleId);

        //            if (role.Name == "student")
        //            {
        //                var student = new Student
        //                {
        //                    FirstName = model.FirstName,
        //                    LastName = model.LastName,
        //                    BirthDate = (DateTime)model.BirthDate,
        //                    Email = model.Email
        //                };
        //                await _studentRepository.AddAsync(student);

        //            }

        //            else if (role.Name == "teacher")
        //            {
        //                var teacher = new Teacher
        //                {
        //                    FirstName = model.FirstName,
        //                    LastName = model.LastName,
        //                    BirthDate = model.BirthDate,
        //                    Email = model.Email
        //                };
        //                await _teacherRepository.AddAsync(teacher);
        //            }

        //            TempData["Success"] = $"Hoşgeldiniz. Giriş yapma sayfasına yönlendiriliyorsunuz...";
        //            return RedirectToAction("Login");
        //        }
        //        else
        //        {
        //            TempData["Error"] = "Kayıt yapılamadı!";
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }
        //            return View(model);
        //        }
        //    }
        //    TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
        //    return View(model);
        //}
        #endregion

        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByNameAsync(model.UserName);
                
                if (appUser != null)
                {
                    SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        appUser.LoginCount++;
                        var result2 =  await _userManager.UpdateAsync(appUser);
                        if (result2.Succeeded)
                        {
                            if (appUser.LoginCount == 1 && !await _userManager.IsInRoleAsync(appUser, "admin"))
                            {
                                TempData["Success"] = "İlk kez giriş yaptığınız için şifrenizi değiştirmeniz gerekiyor. Lütfen yeni şifre giriniz!";
                                return RedirectToAction("ChangePassword", new { id = appUser.Id });
                            }
                            if (await _userManager.IsInRoleAsync(appUser, "admin"))
                            {
                                TempData["Success"] = "Sayın Admin Hoşgeldiniz!";
                                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                            }
                            TempData["Success"] = $"Hoşgeldiniz {appUser.FirstName} {appUser.LastName}";
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["Error"] = "Giriş yapılamadı!";
                        return View(model);
                    }
                }
                TempData["Error"] = "Kullanıcı adı veya şifre yanlış!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize(Roles = "admin, hrPersonal, student, teacher")]
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var model = new ChangePasswordDTO() { Id = id };
                return View(model);
            }
            TempData["Error"] = "Kullanıcı bulunamadı!";
            return RedirectToAction("Login");
        }

        [Authorize(Roles = "admin, hrPersonal, student, teacher")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model.OldPassword == model.Password)
                {
                    TempData["Error"] = "Yeni şifreniz eski şifrenizle aynı olamaz!";
                    return View(model);
                }
                if (model.Password == model.PasswordCheck)
                {
                    var appUser = await _userManager.FindByIdAsync(model.Id);
                    var result = await _userManager.ChangePasswordAsync(appUser, model.OldPassword, model.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignOutAsync();
                        TempData["Success"] = "Şifreniz kaydedildi. Yeni şifreniz ile giriş yapabilirsiniz!";
                        return RedirectToAction("Login");
                    }
                }
                TempData["Error"] = "Şifreleriniz uyuşmuyor!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }


        [Authorize(Roles = "admin, hrPersonal, student, teacher")]
        public async Task<IActionResult> EditUser()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if (userId is not null)
            {
                var appUser = await _userManager.FindByIdAsync(userId);
                var model = new EditUserDTO
                {
                    Id = appUser.Id,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    Email = appUser.Email,
                    UserName = appUser.UserName,
                    BirthDate = appUser.BirthDate.ToShortDateString(),
                    Password = appUser.PasswordHash,
                    CreatedDate = appUser.CreatedDate,
                    UpdatedDate = appUser.UpdatedDate
                };
                return View(model);
            }

            TempData["Error"] = "Bu sayfayı görüntülemek için giriş yapmanız gerekiyor!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, hrPersonal, student, teacher")]
        public async Task<IActionResult> EditUser(EditUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var appUser = await _userManager.FindByIdAsync(userId);
                if (appUser != null)
                {
                    appUser.Email = model.Email;
                    if (model.Password != null)
                    {
                        appUser.PasswordHash = _passwordHasher.HashPassword(appUser, model.Password);
                    }
                    appUser.UpdatedDate = DateTime.Now;
                    appUser.Status = StudentPortal_Core.Entities.Abstract.Status.Modified;

                    var result = await _userManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                    {
                        TempData["Success"] = "Profiliniz güncellendi!";
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            TempData["Error"] = error.Description;
                        }
                    }
                }
            }
            else
            {
                TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            }
            return View(model);
        }

        [Authorize(Roles = "admin, hrPersonal, student, teacher")]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
                TempData["Success"] = "Başarılı bir şekilde çıkış yaptınız!";
                return RedirectToAction("Login");
            }

            TempData["Error"] = "Önce giriş yapınız!";
            return RedirectToAction("Index", "Home");
        }
    }
}
