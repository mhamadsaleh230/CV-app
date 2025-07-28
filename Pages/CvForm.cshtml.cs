using Hw5forExam.Models;
using Hw5forExam.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace Hw5forExam.Pages;

public class CvFormModel : PageModel
{ 
 public CvViewModel view { get; set; }
 [BindProperty]
 public CvBindingModel bind{ get; set; }
 public IEnumerable<SelectListItem> Nationality { get; set; } = new List<SelectListItem>
{
    new SelectListItem{Value="Lb",Text="Lebanese"},
    new SelectListItem{Value="Ch",Text="Chinese"},
    new SelectListItem{Value="Sy",Text="Syrian"},
    new SelectListItem{Value="Am",Text="American"},
};

    public List<string> SkillOptions { get; set; } = new List<string>() { "Java", "Python", "ASP", "C#", "ASP.NET", "SQL", "HTML", "CSS", "JavaScript" };
    public List<string> SexOptions { get; set; } = new List<string>() { "M", "F", "O" };


    [FromQuery]
    public int? id { get; set; }
    public string ResultMessage { get; set; }
    public ICvService _service { get; set; }
   
    public CvFormModel(ICvService service)
    {
        this._service = service;
        view = new CvViewModel();
    }





    public async Task OnGetAsync()
    {

        if (id.HasValue)
        {
            var cv = await _service.GetCVAsync(id.Value);
            if (cv != null)
            {
                view = new CvViewModel
                {
                    FName = cv.FName,
                    LName = cv.LName,
                    BDay = cv.BDay,
                    Nationality = cv.Nationality,
                    Sex = cv.Sex,
                    Skills = cv.Skills,
                    Email = cv.Email,
                    // Passwords and confirmations are left empty intentionally
                    Verification1 = 3,
                    Verification2 = 5
                };
                return;
            }
        }
        view = new CvViewModel
        {
            Verification1 = 3,
            Verification2 = 5
        };

    }
    public async Task<IActionResult> OnPostAsync( int randomNum) {

        if (bind.Verification1 + bind.Verification2 != bind.SumAnswer)
        {
            //Add to the Model State a Model Error
            ModelState.AddModelError("view.SumAnswer", "Sum of number 1 and number 2 should be equal to sum");
            Console.WriteLine(string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage)));
        }
        if (bind.Email != bind.EmailConfirmation)
        {
            ModelState.AddModelError("view.EmailConfirmation", "Emails do not match.");
        }

        if (!ModelState.IsValid)
        {
            //Populate again the form by the received values

            view = new CvViewModel
            {
                FName = bind.FName,
                LName = bind.LName,
                BDay = bind.BDay,
                Nationality = bind.Nationality,
                Sex = bind.Sex,
                Skills = bind.Skills,
                Email = bind.Email,
                EmailConfirmation = bind.EmailConfirmation,
                Password = bind.Password,
                Verification1 = bind.Verification1,
                Verification2 = bind.Verification1,
                SumAnswer = bind.SumAnswer,
                Photo = bind.Photo
            };
            return Page();
        }
        ResultMessage = "form submitted successfully";
        int id = await _service.SaveCvAsync(bind);

        view = new CvViewModel();
        ModelState.Clear();
       
        return RedirectToPage("/Summary", new { id, randomNum });
    }
}