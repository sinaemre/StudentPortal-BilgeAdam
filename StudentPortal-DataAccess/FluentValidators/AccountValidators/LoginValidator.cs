using FluentValidation;
using StudentPortal_Core.DTO_s.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.FluentValidators.AccountValidators
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş geçilemez!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Parola boş geçilemez!");
        }
    }
}
