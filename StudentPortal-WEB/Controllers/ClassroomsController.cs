using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal_Core.DTO_s.ClassroomDTO;
using StudentPortal_Core.Entities.Concrete;
using StudentPortal_DataAccess.Services.Interface;
using StudentPortal_WEB.Models.ViewModels;
using StudentPortal_Core.Entities.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using StudentPortal_Core.Entities.UserEntites.Concrete;

namespace StudentPortal_WEB.Controllers
{
    public class ClassroomsController : Controller
    {
        private readonly IClassroomRepository _classroomRepo;
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepo;
        private readonly UserManager<AppUser> _userManager;

        public ClassroomsController(IClassroomRepository classroomRepo, IMapper mapper, ITeacherRepository teacherRepo, UserManager<AppUser> userManager)
        {
            _classroomRepo = classroomRepo;
            _mapper = mapper;
            _teacherRepo = teacherRepo;
            _userManager = userManager;
        }

        [Authorize(Roles = "admin, hrPersonal, teacher")]
        public async Task<IActionResult> Index()
        {
            var classrooms = await _classroomRepo.GetFilteredListAsync
                (
                    select: x => new GetClassroomVM
                    {
                        Id = x.Id,
                        ClassroomName = x.ClassroomName,
                        ClassroomDescription = x.ClassroomDescription,
                        TeacherName = x.Teacher.FirstName + " " + x.Teacher.LastName,
                        ClassroomSize = x.Students.Where(x => x.Status != Status.Passive).ToList().Count,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Teacher).Include(z => z.Students)
                );


            return View(classrooms);
        }

        [Authorize(Roles = "admin, hrPersonal")]
        public async Task<IActionResult> CreateClassroom()
        {
            var model = new CreateClassroomDTO
            {
                Teachers = await _teacherRepo.GetByDefaultsAsync(x => x.Status != Status.Passive)
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, hrPersonal")]
        public async Task<IActionResult> CreateClassroom(CreateClassroomDTO model)
        {
            model.Teachers = await _teacherRepo.GetByDefaultsAsync(x => x.Status != Status.Passive);

            if (ModelState.IsValid)
            {
                if (await _classroomRepo.AnyAsync(x =>
                                                    x.Status != Status.Passive &&
                                                    x.ClassroomName == model.ClassroomName))
                {
                    TempData["Error"] = "Başka bir sınıf adı seçiniz!";
                    return View(model);
                }
                else
                {
                    var classroom = _mapper.Map<Classroom>(model);
                    await _classroomRepo.AddAsync(classroom);
                    TempData["Success"] = $"{classroom.ClassroomName} sınıfı kaydedilmiştir.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize(Roles = "admin, hrPersonal")]
        public async Task<IActionResult> UpdateClassroom(int id)
        {
            if (id > 0)
            {
                var classroom = await _classroomRepo.GetByIdAsync(id);

                if (classroom != null)
                {
                    var model = _mapper.Map<UpdateClassroomDTO>(classroom);
                    model.Teachers = await _teacherRepo.GetByDefaultsAsync(x => x.Status != Status.Passive);
                    return View(model);
                }
            }
            TempData["Error"] = "Sınıf bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, hrPersonal")]
        public async Task<IActionResult> UpdateClassroom(UpdateClassroomDTO model)
        {
            model.Teachers = await _teacherRepo.GetByDefaultsAsync(x => x.Status != Status.Passive);

            if (ModelState.IsValid)
            {
                var classroom = await _classroomRepo.GetByIdAsync(model.Id);
                if (classroom != null)
                {
                    if (await _classroomRepo.AnyAsync(x =>
                                                    x.Status != Status.Passive &&
                                                    (x.Id != model.Id && x.ClassroomName == model.ClassroomName)))
                    {
                        TempData["Error"] = "Lütfen başka bir sınıf adı seçiniz!";
                        return View(model);
                    }
                    else
                    {
                        var classroomUpdated = _mapper.Map<Classroom>(model);
                        await _classroomRepo.UpdateAsync(classroomUpdated);
                        TempData["Success"] = $"{classroomUpdated.ClassroomName} sınıfı güncellendi!";
                        return RedirectToAction("Index");
                    }
                }
                TempData["Error"] = "Sınıf bulunamadı!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize(Roles = "admin, hrPersonal")]
        public async Task<IActionResult> DeleteClassroom(int id)
        {
            if (id > 0)
            {
                var classroom = await _classroomRepo.GetByIdAsync(id);
                if (classroom != null)
                {
                    await _classroomRepo.DeleteAsync(classroom);
                    TempData["Success"] = $"{classroom.ClassroomName} sınıfı silinmiştir!";
                    return RedirectToAction("Index");
                }
            }
            TempData["Error"] = "Sınıf bulunamadı!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetClassroomsByTeacherId()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var teacher = await _teacherRepo.GetByDefaultAsync(x => x.AppUserID == userId);

            if (teacher != null)
            {
                var classrooms = await _classroomRepo.GetFilteredListAsync
                  (
                      select: x => new GetClassroomsVM
                      {
                          Id = x.Id,
                          ClassName = x.ClassroomName,
                          ClassDescription = x.ClassroomDescription,
                          ClassSize = x.Students.Count()
                      },
                      where: x => x.TeacherId == teacher.Id && x.Status != Status.Passive,
                      join: x => x.Include(z => z.Students)
                  );

                return View(classrooms);
            }

            TempData["Error"] = "Sınıflar bulunamadı!";
            return RedirectToAction("Index", "Home");
        }
    }
}
