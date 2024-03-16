using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentPortal_DataAccess.Services.Interface;
using StudentPortal_Core.Entities.Abstract;
using StudentPortal_WEB.Areas.Admin.Models;
using StudentPortal_Core.DTO_s.HumanResourcesDTO;
using AutoMapper;
using StudentPortal_Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using StudentPortal_Core.Entities.UserEntites.Concrete;
using NuGet.DependencyResolver;
using System.Globalization;
using System.Text;

namespace StudentPortal_WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class HumanResourcesController : Controller
    {
        private readonly IHumanResourcesRepository _hrRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public HumanResourcesController(IHumanResourcesRepository hrRepository, IMapper mapper, UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher )
        {
            _hrRepository = hrRepository;
            _mapper = mapper;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Index()
        {
            var hrList = await _hrRepository.GetFilteredListAsync
                (
                    select: x => new GetHRVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        BirthDate = x.BirthDate,
                        HireDate = x.HireDate,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );

            return View(hrList);
        }

        [HttpGet]
        public IActionResult CreateHR() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHR(CreateHRDTO model)
        {
            if (ModelState.IsValid)
            {
                var hr = _mapper.Map<HumanResources>(model);
                var appUser = new AppUser
                {
                    UserName = String.Join("", model.FirstName.Normalize(NormalizationForm.FormD)
    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).ToLower().Replace('ı', 'i').Replace(" ", "") + "." + String.Join("", model.LastName.Normalize(NormalizationForm.FormD)
    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).ToLower().Replace('ı', 'i').Replace(" ", ""),
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = (DateTime)model.BirthDate
                };

                appUser.PasswordHash = _passwordHasher.HashPassword(appUser, "1234");

                IdentityResult result = await _userManager.CreateAsync(appUser);
                if (result.Succeeded)
                {
                    var result2 = await _userManager.AddToRoleAsync(appUser, "hrPersonal");
                    if (result2.Succeeded)
                    {
                        await _hrRepository.AddAsync(hr);
                        TempData["Success"] = $"{hr.FirstName} {hr.LastName} personeli sisteme kaydedilmiştir.. Kullanıcı adı {appUser.UserName},\nŞifre: 1234";
                        return RedirectToAction("Index");
                    }
                    TempData["Error"] = "Personel role eklenemedi!";
                    return View(model);
                }
                TempData["Error"] = "Personel sisteme kayıt edilememiştir!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }
    }
}
