using Hw5forExam.Data;
using Hw5forExam.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hw5forExam.Pages
{
    public class CvListModel : PageModel
    {
        private readonly ICvService _cvService;
        public CvListModel(ICvService cvService)
        {
            _cvService = cvService;
        }

        [BindProperty]
        public int Id { get; set; }
        public List<CvData> CvList { get; set; } = new();
        public async Task OnGetAsync()
        {
            CvList = await _cvService.GetAllCVsAsync();
        }


        public async Task<IActionResult> OnPostEditAsync()
        {
            return RedirectToPage("/CvForm", new { id = Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await _cvService.DeleteCvAsync(Id);
            return RedirectToPage();
        }
    }
}
