using Hw5forExam.Data;
using Hw5forExam.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hw5forExam.Pages
{
    public class SummaryModel : PageModel
    {
        [FromRoute]
        public int randomNum { get; set; }
        public CvData cv { get; set; }

        public ICvService CvService { get; set; }
        public SummaryModel(ICvService cvService)
        {
           this.CvService = cvService;
        }
        public async Task OnGetAsync(int id)
        {
           cv = await CvService.GetCVAsync(id);
        }
    }
}
