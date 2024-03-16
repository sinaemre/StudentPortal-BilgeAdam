using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_Core.DTO_s.AccountDTO
{
    public class EditUserDTO
    {
        public string Id { get; set; }

        [Display(Name = "Ad")]
        public string? FirstName { get; set; }
        
        [Display(Name = "Soyad")]
        public string? LastName { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public string? BirthDate { get; set; }

        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; }

        [Display(Name = "Parola")]
        public string? Password { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
