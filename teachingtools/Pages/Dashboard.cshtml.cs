using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace teachingtools.Pages
{
    [Authorize]
    public class DashboardModel : PageModel
    { 
        public void OnGet()
        {
        }
    }
}
