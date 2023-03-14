using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace teachingtools.Pages
{
    [Authorize]
    public class LogicModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
