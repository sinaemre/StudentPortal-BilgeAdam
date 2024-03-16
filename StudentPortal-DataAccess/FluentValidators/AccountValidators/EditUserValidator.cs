using FluentValidation;
using StudentPortal_Core.DTO_s.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.FluentValidators.AccountValidators
{
    public class EditUserValidator : AbstractValidator<EditUserDTO>
    {
        public EditUserValidator()
        {

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-Mail alanı boş geçilemez!")
                .EmailAddress()
                .WithMessage("E-Mail formatında giriş yapınız!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Parola boş geçilemez!")
                .MinimumLength(1)
                .WithMessage("En az 1 karakter girmelisiniz!");

        
        }
    }
}
