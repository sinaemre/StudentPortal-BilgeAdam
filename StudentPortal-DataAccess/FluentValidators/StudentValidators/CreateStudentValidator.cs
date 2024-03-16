using FluentValidation;
using StudentPortal_Core.DTO_s.StudentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.FluentValidators.StudentValidators
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentDTO>
    {
        public CreateStudentValidator()
        {
            Regex regex = new Regex("^[a-zA-Z- ığüşöçİĞÜŞÖÇ]*$");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Ad alanı boş geçilemez!")
                .MaximumLength(100)
                .WithMessage("100 karakter sınırını geçemezsiniz!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf girebilirsiniz!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Soyad alanı boş geçilemez!")
                .MaximumLength(200)
                .WithMessage("200 karakter sınırını geçemezsiniz!")
                .MinimumLength(2)
                .WithMessage("En az 2 karakter girmelisiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf girebilirsiniz!");

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("Email alanı zorunludur!")
               .EmailAddress()
               .WithMessage("Lütfen bir mail giriniz!");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("Doğum tarihi zorunludur!");

            RuleFor(x => x.ClassroomId)
                .NotEmpty()
                .WithMessage("Sınıf boş geçilemez!");

        }
    }
}
