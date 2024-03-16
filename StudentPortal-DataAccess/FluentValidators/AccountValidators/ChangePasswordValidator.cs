using FluentValidation;
using StudentPortal_Core.DTO_s.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.FluentValidators.AccountValidators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage("Eski Şifre boş geçilemez!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Yeni şifre boş geçilemez!");

            RuleFor(x => x.PasswordCheck)
                .NotEmpty()
                .WithMessage("Yeni şifre tekarı boş geçilemez!")
                .Equal(x => x.Password)
                .WithMessage("Şifreler eşleşmiyor!!");
        }
    }
}
