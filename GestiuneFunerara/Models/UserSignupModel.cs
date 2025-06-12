using System.ComponentModel.DataAnnotations;

public class UserSignupModel
{
    [Required(ErrorMessage = "Numele complet este obligatoriu")]
    public string NumeComplet { get; set; }

    [Required(ErrorMessage = "Emailul este obligatoriu")]
    [EmailAddress(ErrorMessage = "Formatul emailului nu este valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Numărul de telefon este obligatoriu")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Numărul de telefon trebuie să aibă exact 10 cifre")]
    public string Telefon { get; set; }

    [Required(ErrorMessage = "Parola este obligatorie")]
    [MinLength(6, ErrorMessage = "Parola trebuie să aibă cel puțin 6 caractere")]
    public string Parola_hashuita { get; set; }
}
