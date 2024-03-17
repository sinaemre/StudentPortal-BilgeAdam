using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal_Core.DTO_s.ClassroomDTO;
using StudentPortal_Core.DTO_s.StudentDTO;
using StudentPortal_Core.Entities.Concrete;
using StudentPortal_DataAccess.Services.Interface;
using StudentPortal_WEB.Models.ViewModels;
using StudentPortal_Core.Entities.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using StudentPortal_Core.Entities.UserEntites.Concrete;
using NuGet.DependencyResolver;
using System.Globalization;
using System.Text;

namespace StudentPortal_WEB.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IMapper _mapper;
        private readonly IClassroomRepository _classroomRepo;
        private readonly ITeacherRepository _teacherRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public StudentsController(IStudentRepository studentRepo, IMapper mapper, IClassroomRepository classroomRepo, ITeacherRepository teacherRepo, UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
            _classroomRepo = classroomRepo;
            _teacherRepo = teacherRepo;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        [Authorize(Roles = "admin, hrPersonal")]
        public async Task<IActionResult> Index()
        {
            var students = await _studentRepo.GetFilteredListAsync
                (
                    select: x => new GetStudentVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirthDate = x.BirthDate,
                        Email = x.Email,
                        ClassroomName = x.Classroom.ClassroomName,
                        Average = x.Average,
                        TeacherName = x.Classroom.Teacher.FirstName + " " + x.Classroom.Teacher.LastName,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Classroom).ThenInclude(z => z.Teacher)
                );

            return View(students);
        }

        public async Task<IActionResult> DetailStudent(int? id)
        {
            Student student = null;
            if (id is not null)
            {
                if (id > 0)
                {
                    student = await _studentRepo.GetByIdAsync((int)id);
                }
            }
            else
            {
                student = await _studentRepo.GetByDefaultAsync(x => x.AppUserID == _userManager.GetUserId(HttpContext.User));

            }
            if (student is not null)
            {
                var model = _mapper.Map<GetStudentDetailDTO>(student);
                if (student.ClassroomId is not null)
                {
                    var classroom = await _classroomRepo.GetByIdAsync((int)student.ClassroomId);
                    var teacher = await _teacherRepo.GetByIdAsync(classroom.TeacherId);
                    model.ClassroomName = classroom.ClassroomName;
                    model.TeacherName = teacher.FirstName + " " + teacher.LastName;
                }

                return View(model);
            }
            TempData["Error"] = "Öğrenci bulunamadı!";
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "admin, hrPersonal")]
        public async Task<IActionResult> CreateStudent()
        {
            var classrooms = await _classroomRepo.GetFilteredListAsync
                 (
                     select: x => new ShowClassroomDTO
                     {
                         Id = x.Id,
                         ClassroomName = x.ClassroomName,
                         ClassroomDescription = x.ClassroomDescription,
                         ClassroomSize = x.Students.Where(x => x.Status != Status.Passive).ToList().Count,
                         TeacherName = x.Teacher.FirstName + " " + x.Teacher.LastName
                     },
                     where: x => x.Status != Status.Passive,
                     join: x => x.Include(z => z.Students).Include(z => z.Teacher)
                 );

            var model = new CreateStudentDTO
            {
                Classrooms = classrooms
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, hrPersonal")]
        public async Task<IActionResult> CreateStudent(CreateStudentDTO model)
        {
            var classrooms = await _classroomRepo.GetFilteredListAsync
                (
                   select: x => new ShowClassroomDTO
                   {
                       Id = x.Id,
                       ClassroomName = x.ClassroomName,
                       ClassroomDescription = x.ClassroomDescription,
                       ClassroomSize = x.Students.Where(x => x.Status != Status.Passive).ToList().Count,
                       TeacherName = x.Teacher.FirstName + " " + x.Teacher.LastName
                   },
                     where: x => x.Status != Status.Passive,
                     join: x => x.Include(z => z.Students).Include(z => z.Teacher)
                );

            model.Classrooms = classrooms;


            if (ModelState.IsValid)
            {
                var student = _mapper.Map<Student>(model);
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
                    var result2 = await _userManager.AddToRoleAsync(appUser, "student");
                    if (result2.Succeeded)
                    {
                        student.AppUserID = appUser.Id;
                        await _studentRepo.AddAsync(student);
                        TempData["Success"] = $"{student.FirstName} {student.LastName} öğrencisi sisteme kaydedilmiştir.. Kullanıcı adı {appUser.UserName},\nŞifre: 1234";
                        return RedirectToAction("Index");
                    }
                    TempData["Error"] = "Öğrenci role eklenemedi!";
                    return View(model);
                }
                TempData["Error"] = "Öğrenci sisteme kayıt edilememiştir!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize(Roles = "admin, hrPersonal")]
        public async Task<IActionResult> UpdateStudent(int id)
        {
            if (id > 0)
            {
                var student = await _studentRepo.GetByIdAsync(id);
                if (student is not null)
                {
                    var model = _mapper.Map<UpdateStudentDTO>(student);
                    var classrooms = await _classroomRepo.GetFilteredListAsync
                                       (
                                          select: x => new ShowClassroomDTO
                                          {
                                              Id = x.Id,
                                              ClassroomName = x.ClassroomName,
                                              ClassroomDescription = x.ClassroomDescription,
                                              ClassroomSize = x.Students.Where(x => x.Status != Status.Passive).ToList().Count,
                                              TeacherName = x.Teacher.FirstName + " " + x.Teacher.LastName
                                          },
                                          where: x => x.Status != Status.Passive,
                                          join: x => x.Include(z => z.Students).Include(z => z.Teacher)
                                       );
                    model.Classrooms = classrooms;
                    return View(model);
                }
            }
            TempData["Error"] = "Öğrenci bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, hrPersonal")]
        public async Task<IActionResult> UpdateStudent(UpdateStudentDTO model)
        {
            var classrooms = await _classroomRepo.GetFilteredListAsync
                                       (
                                          select: x => new ShowClassroomDTO
                                          {
                                              Id = x.Id,
                                              ClassroomName = x.ClassroomName,
                                              ClassroomDescription = x.ClassroomDescription,
                                              ClassroomSize = x.Students.Where(x => x.Status != Status.Passive).ToList().Count,
                                              TeacherName = x.Teacher.FirstName + " " + x.Teacher.LastName
                                          },
                                          where: x => x.Status != Status.Passive,
                                          join: x => x.Include(z => z.Students).Include(z => z.Teacher)
                                       );
            model.Classrooms = classrooms;
            if (ModelState.IsValid)
            {
                var student = _mapper.Map<Student>(model);
                var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == student.AppUserID);
                if (appUser != null)
                {
                    appUser.FirstName = student.FirstName;
                    appUser.LastName = student.LastName;
                    appUser.BirthDate = student.BirthDate;
                    appUser.Email = student.Email;
                    await _userManager.UpdateAsync(appUser);
                }
                await _studentRepo.UpdateAsync(student);
                TempData["Success"] = $"{student.FirstName} {student.LastName} öğrencisi güncellenmiştir.";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize(Roles = "admin, hrPersonal")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (id > 0)
            {
                var student = await _studentRepo.GetByIdAsync(id);
                if (student is not null)
                {
                    await _studentRepo.DeleteAsync(student);
                    var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == student.AppUserID);
                    await _userManager.DeleteAsync(appUser);
                    TempData["Success"] = $"{student.FirstName} {student.LastName} öğrencisinin kaydı silinmiştir!";
                    return RedirectToAction("Index");
                }
            }
            TempData["Error"] = "Öğrenci bulunamadı!";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin, hrPersonal, teacher")]
        public async Task<IActionResult> GetStudentsByClassroomId(int? id)
        {
            if (id > 0)
            {
                var students = await _studentRepo.GetFilteredListAsync
                    (
                        select: x => new GetStudentsVM
                        {
                            Id = x.Id,
                            FullName = x.FirstName + " " + x.LastName,
                            BirthDate = x.BirthDate,
                            Exam1 = x.Exam1,
                            Exam2 = x.Exam2,
                            Average = x.Average,
                            ProjectExam = x.ProjectExam,
                            
                        },
                        where: x => x.ClassroomId == id && x.Status != Status.Passive
                    );
                return View(students);
            }
            TempData["Error"] = "Sınıf bulunamadı!";
            return RedirectToAction("Index", "Home");
        }
    }
}
