using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_Core.DTO_s.RoleDTO
{
    public class UpdateRoleDTO
    {
        public string Id { get; set; }

        [Display(Name = "Rol Adı")]
        public string? RoleName { get; set; }
    }
}
