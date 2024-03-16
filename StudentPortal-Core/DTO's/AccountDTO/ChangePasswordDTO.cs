using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_Core.DTO_s.AccountDTO
{
    public class ChangePasswordDTO
    {
        public string Id { get; set; }

        [Display(Name = "Eski Şifre")]
        public string? OldPassword { get; set; }

        [Display(Name = "Yeni Şifre")]
        public string? Password { get; set; }
        
        [Display(Name = "Yeni Şifre Tekrar")]
        public string? PasswordCheck { get; set; }
    }
}
