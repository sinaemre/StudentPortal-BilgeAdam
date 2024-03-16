using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using StudentPortal_Core.Entities.UserEntites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "user-role")]
    public class RoleTagHelper : TagHelper
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleTagHelper(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HtmlAttributeName("user-role")]
        public string RoleId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> userNames = new List<string>();

            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role != null)
            {
                var users = await _userManager.Users.ToListAsync();

                foreach (var user in users)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userNames.Add(user.UserName);
                    }
                }
            }

            output.Content.SetContent(userNames.Count == 0 ? 
                "Bu rolde hiçbir kullanıcı yok!" : 
                userNames.Count > 3 ? 
                (string.Join(", ", userNames.Take(3)) + " ...") : 
                string.Join(", ", userNames));
        }
    }
}
