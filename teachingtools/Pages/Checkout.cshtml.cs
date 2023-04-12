using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stripe;
using teachingtools.Data;

namespace teachingtools.Pages
{
    public class CheckoutModel : PageModel
    {

        public double Total = 0.99;
        public long AmountPayable = 0;

        private AppDbContext _db;
        [BindProperty]
        public Subscriptions sub { get; set; }

        public readonly UserManager<ApplicationUser> _userManager;
        public CheckoutModel(AppDbContext db, UserManager<ApplicationUser> um)
        {
            _db = db;
            _userManager = um;
        }
        public void OnGet()
        {
            Total = 0.99;
            AmountPayable = (long)(Total * 100);
        }

        public async Task AssignRole()
        {
            var user = await _userManager.GetUserAsync(User);
            
           
            if (user != null)
            {
                if (Total == 0.99)
                {
                    sub.UserName = user.UserName;
                    sub.SubscriptionType = false;
                    _db.Subscriptions.Add(sub);
                }
                else if (Total == 9.99)
                {
                    sub.UserName = user.UserName;
                    sub.SubscriptionType = true;
                    _db.Subscriptions.Add(sub);
                }
                await _userManager.AddToRoleAsync(user, "Subscriber");
            }
            await _db.SaveChangesAsync();
        }

        public IActionResult OnPostCharge(string stripeEmail, string stripeToken, long amount)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = amount,
                Description = "The Logo Cafe Charge",
                Currency = "GBP",
                Customer = customer.Id
            });
            AssignRole().Wait();
            return RedirectToPage("/Subscriptions");
        }
    }
}
