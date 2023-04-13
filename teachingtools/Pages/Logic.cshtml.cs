using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using teachingtools.Data;

namespace teachingtools.Pages
{
    [Authorize]
    public class LogicModel : PageModel
    {
        private AppDbContext _db;

        public readonly UserManager<ApplicationUser> _userManager;
        public IList<string> userRole;
        public  LogicModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _userManager = um;
        }
        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var role = await _userManager.GetRolesAsync(user);
            if (role != null)
            {
                userRole = role;
            }
        }
    }
}
