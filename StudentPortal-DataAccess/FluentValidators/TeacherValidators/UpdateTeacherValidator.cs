using FluentValidation;
using StudentPortal_Core.DTO_s.TeacherDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.FluentValidators.TeacherValidators
{
    public class UpdateTeacherValidator : AbstractValidator<UpdateTeacherDTO>
    {
        public UpdateTeacherValidator()
        {
            Regex regex = new Regex("^[a-zA-Z- ığüşöçİĞÜŞÖÇ]*$");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Ad alanı zorunludur!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!")
                .MaximumLength(100)
                .WithMessage("En fazla 100 karakter girebilirsiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf girebilirsiniz!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Soyad alanı zorunludur!")
                .MinimumLength(2)
                .WithMessage("En az 2 karakter girmelisiniz!")
                .MaximumLength(200)
                .WithMessage("En fazla 200 karakter girebilirsiniz!")
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
        }
    }
}
