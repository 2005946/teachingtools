using Azure.Identity;
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

        public readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public bool subType;
        public bool isPaid;
        public AccountModel(AppDbContext db, SignInManager<ApplicationUser> sm, UserManager<ApplicationUser> um)
        {
            _db = db;
            _signInManager = sm;
            _userManager = um;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var subUser = await _db.Subscriptions.FindAsync(user.UserName);
            if (subUser != null)
            {
                isPaid = true;
				subType = subUser.SubscriptionType;
            }
            else
            {
                isPaid = false;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Index");
        }
    }
}
