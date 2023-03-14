using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using teachingtools.Data;

namespace teachingtools.Pages
{
    [Authorize]
    public class AccountModel : PageModel
    {
        private AppDbContext _db;

        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountModel(AppDbContext db, SignInManager<ApplicationUser> sm)
        {
            _db = db;
            _signInManager = sm;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Index");
        }
    }
}
