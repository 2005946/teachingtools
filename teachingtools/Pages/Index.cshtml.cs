using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using teachingtools.Data;

namespace teachingtools.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public LoginUser Input { get; set; }

        private readonly SignInManager<ApplicationUser> _logInManager;

        public IndexModel(SignInManager<ApplicationUser> sm)
        {
            _logInManager = sm;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _logInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToPage("/Dashboard");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return Page();
        }
    }
}
