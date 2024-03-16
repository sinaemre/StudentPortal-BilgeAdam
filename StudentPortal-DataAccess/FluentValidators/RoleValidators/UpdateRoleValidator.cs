using FluentValidation;
using StudentPortal_Core.DTO_s.RoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.FluentValidators.RoleValidators
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleDTO>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.RoleName)
              .NotEmpty()
              .WithMessage("Rol adı zorunludur!")
              .MinimumLength(3)
              .WithMessage("En az 3 karakter girmelisiniz!");
        }
    }
}
