using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LogovanjeModel
    {
        [Required]
        [Display(Name = "Korisnicko ime")]

        public string KorisnickoIme { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Lozinka { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool ZapamtiMe { get; set; }
    }

    public class RegistracijaModel
    {
        [Required]
        [StringLength(25, ErrorMessage = "Korisnicko ime mora da ima 25 karaktera")]
        [Display(Name = "Korisnicko ime")]
        public string KorisnickoIme { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Ime mora da ima 25 karaktera")]
        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Prezime mora da ima 25 karaktera")]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Lozinka {0} mora da ime najmanje {2} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Lozinka { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi password")]
        [Compare("Lozinka", ErrorMessage = "Ne odgovara sifra.")]
        public string PotvrdiLozinku { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
