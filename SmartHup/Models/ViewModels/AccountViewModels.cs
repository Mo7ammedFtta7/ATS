using SmartHup.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartHup.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(ResourceType = typeof(Labels), Name = "Email")]
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
        [Display(ResourceType = typeof(Labels), Name = "Email")]

        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(ResourceType = typeof(Labels), Name = "Username")]
        public string UserName { get; set; }


        [Display(ResourceType = typeof(Labels), Name = "Email")]

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Labels), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public long systemId { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "status")]
        public int? status { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "version")]
        public int version { get; set; }

        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof(Labels), Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(ResourceType = typeof(Labels), Name = "Username")]
        public string Username { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Labels), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Labels), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Range(999, UInt64.MaxValue)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        [DataType(DataType.PhoneNumber)]
        [Display(ResourceType = typeof(Labels), Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "serviceProviderId")]
        public long serviceProviderId { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "RoleId")]
        public long RoleId { get; set; }


    }

    public class ResetPasswordViewModel
    {
        public long systemId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Labels), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Labels), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof(Labels), Name = "Email")]
        public string Email { get; set; }
    }
}
