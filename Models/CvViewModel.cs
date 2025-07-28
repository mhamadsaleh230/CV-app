using System.ComponentModel.DataAnnotations;

namespace Hw5forExam.Models;

public class CvViewModel
{
    [Required]
    [Display(Name = "First Name")]
    [StringLength(20, ErrorMessage = "nah bro")]
    public string FName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    [MaxLength(20)]
    public string LName { get; set; }

    [Required]
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    public DateTime? BDay { get; set; }

    [Required]
    public string Nationality { get; set; }

    [Required]
    public string Sex { get; set; }
    [Required]

    public List<string> Skills { get; set; } = new List<string>();

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, EmailAddress]
    [Compare("Email", ErrorMessage = "Email confirmation must match.")]
    public string EmailConfirmation { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
    ErrorMessage = "Password must contain at least one uppercase, one lowercase, one number, and one special character")]
  
    public string Password { get; set; }

    [Required]
    public int Verification1 { get; set; }

    [Required]
    public int Verification2 { get; set; }

    [Required]
    public int SumAnswer { get; set; }
    [Display(Name = "Photo: ")]
    [Required(ErrorMessage = "Photo is required.")]
    public IFormFile Photo { get; set; }

}
