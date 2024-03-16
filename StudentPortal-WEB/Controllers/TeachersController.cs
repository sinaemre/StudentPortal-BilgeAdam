﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentPortal_Core.DTO_s.TeacherDTO;
using StudentPortal_Core.Entities.Concrete;
using StudentPortal_DataAccess.Services.Interface;
using StudentPortal_WEB.Models.ViewModels;
using StudentPortal_Core.Entities.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using StudentPortal_Core.Entities.UserEntites.Concrete;
using System.Text;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace StudentPortal_WEB.Controllers
{
    [Authorize(Roles = "admin, hrPersonal")]
    public class TeachersController : Controller
    {
        private readonly ITeacherRepository _teacherRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public TeachersController(ITeacherRepository teacherRepo, IMapper mapper, UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _teacherRepo = teacherRepo;
            _mapper = mapper;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherRepo.GetFilteredListAsync
                (
                    select: x => new GetTeacherVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        BirthDate = x.BirthDate,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );

            return View(teachers);
        }

        public IActionResult CreateTeacher() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeacher(CreateTeacherDTO model)
        {
            if (ModelState.IsValid)
            {
                var teacher = _mapper.Map<Teacher>(model);
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
                    var result2 = await _userManager.AddToRoleAsync(appUser, "teacher");
                    if (result2.Succeeded)
                    {
                        teacher.AppUserID = appUser.Id;
                        await _teacherRepo.AddAsync(teacher);
                        TempData["Success"] = $"{teacher.FirstName} {teacher.LastName} öğretmeni sisteme kaydedilmiştir.. Kullanıcı adı {appUser.UserName},\nŞifre: 1234";
                        return RedirectToAction("Index");
                    }
                    TempData["Error"] = "Öğretmen role eklenemedi!";
                    return View(model);
                }
                TempData["Error"] = "Öğretmen sisteme kayıt edilememiştir!";
                return View(model);

            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> UpdateTeacher(int id)
        {
            if (id > 0)
            {
                var teacher = await _teacherRepo.GetByIdAsync(id);

                if (teacher is not null)
                {
                    var model = _mapper.Map<UpdateTeacherDTO>(teacher);
                    return View(model);
                }
            }
            TempData["Error"] = "Öğretmen bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherDTO model)
        {
            if (ModelState.IsValid)
            {
                var teacher = _mapper.Map<Teacher>(model);
                var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == teacher.AppUserID);
                if (appUser != null)
                {
                    appUser.FirstName = teacher.FirstName;
                    appUser.LastName = teacher.LastName;
                    appUser.Email = teacher.Email;
                    appUser.BirthDate = teacher.BirthDate;
                    await _userManager.UpdateAsync(appUser);
                }

                await _teacherRepo.UpdateAsync(teacher);
                TempData["Success"] = $"{teacher.FirstName} {teacher.LastName} kişisi güncellendi!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (id > 0)
            {
                var teacher = await _teacherRepo.GetByIdAsync(id);
                if (teacher is not null)
                {
                    if (!await _teacherRepo.HasClassroom(id))
                    {
                        await _teacherRepo.DeleteAsync(teacher);
                        var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == teacher.AppUserID);
                        await _userManager.DeleteAsync(appUser);
                        TempData["Success"] = $"{teacher.FirstName} {teacher.LastName} hocamıza hizmetleri için teşekkür ederiz. Hocamız silinmiştir.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var classroomList = await _teacherRepo.ClassroomNames(teacher.Id);
                        TempData["Error"] = $"{teacher.FirstName} {teacher.LastName} hoca şu sınıflarda kayıtlıdır => {classroomList}";
                        return RedirectToAction("Index");
                    }
                }
            }
            TempData["Error"] = "Hoca bulunamadı!";
            return RedirectToAction("Index");
        }


    }
}
