using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using teachingtools.Data;

namespace teachingtools.Pages
{
    public class SubscriptionsModel : PageModel
    {
        private AppDbContext _db;

        public readonly UserManager<ApplicationUser> _userManager;
        public SubscriptionsModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _userManager = um;
        }
        public void OnGet()
        {
        }
    }
}
