using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using teachingtools.Data;

namespace teachingtools.Pages
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public LoginUser Input { get; set; }
        [BindProperty]
        public Registration RegisterInput { get; set; }

        private readonly SignInManager<ApplicationUser> _logInManager;
        private readonly SignInManager<ApplicationUser> _registerManager;
        private AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userInManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, SignInManager<ApplicationUser> sm, UserManager<ApplicationUser> um, AppDbContext db, SignInManager<ApplicationUser> rm)
        {
            _logger = logger;
            _logInManager = sm;
            _registerManager = rm;
            _userInManager = um;
            _db = db;
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _logInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRegisterAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = RegisterInput.Email, Email = RegisterInput.Email };
                var result = await _userInManager.CreateAsync(user, RegisterInput.Password);
                if (result.Succeeded)
                {
                    await _registerManager.SignInAsync(user, isPersistent: false);
                    await _userInManager.AddToRoleAsync(user, "Member");
                    await _db.SaveChangesAsync();
                    return RedirectToPage("/Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}