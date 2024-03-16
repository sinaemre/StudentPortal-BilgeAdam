using FluentValidation;
using StudentPortal_Core.DTO_s.ClassroomDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.FluentValidators.ClassroomValidators
{
    public class UpdateClassroomValidator : AbstractValidator<UpdateClassroomDTO>
    {
        public UpdateClassroomValidator()
        {
            Regex regex = new Regex("^[a-zA-Z- ığüşöçİĞÜŞÖÇ0123456789.]*$");

            RuleFor(x => x.ClassroomName)
                .NotEmpty()
                .WithMessage("Sınıf adı boş geçilemez!")
                .MinimumLength(3)
                .WithMessage("Minimum 3 karakter girmelisiniz!")
                .MaximumLength(100)
                .WithMessage("Maximum 100 karakter girmelisiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf, rakam, boşluk, nokta ve '-' kullanabilirsiniz.");

            RuleFor(x => x.ClassroomDescription)
              .NotEmpty()
              .WithMessage("Sınıf açıklaması boş geçilemez!")
              .MinimumLength(3)
              .WithMessage("Minimum 3 karakter girmelisiniz!")
              .MaximumLength(200)
              .WithMessage("Maximum 200 karakter girmelisiniz!")
              .Matches(regex)
              .WithMessage("Sadece harf, rakam, boşluk, nokta ve '-' kullanabilirsiniz.");

            RuleFor(x => x.TeacherId)
                .NotEmpty()
                .WithMessage("Eğitmen boş geçilemez!");
        }
    }
}
