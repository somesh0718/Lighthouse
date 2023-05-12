using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class LoginModel : RequestModel
    {
        [Display(Name = "User Id", Description = "User Id")]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(125, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public virtual string UserId { get; set; }

        [Display(Name = "Password", Description = "Password")]
        [RegularExpression(EntityConstants.RegxPasswordPattern, ErrorMessage = EntityConstants.PasswordErrorMessage)]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(40, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public virtual string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Display(Name = "Return Url")]
        public string ReturnUrl { get; set; }
    }

    [DataContract, Serializable]
    public class ChangePasswordModel
    {
        [DataMember]
        [Display(Name = "User Id", Description = "User Id")]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(125, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        public virtual string UserId { get; set; }

        [DataMember]
        [Display(Name = "Password", Description = "Password")]
        [RegularExpression(EntityConstants.RegxPasswordPattern, ErrorMessage = EntityConstants.PasswordErrorMessage)]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(40, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Password { get; set; }

        [DataMember]
        [Display(Name = "New Password", Description = "New Password")]
        [RegularExpression(EntityConstants.RegxPasswordPattern, ErrorMessage = EntityConstants.PasswordErrorMessage)]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(40, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string NewPassword { get; set; }

        [DataMember]
        [Display(Name = "Confirm Password", Description = "Confirm Password")]
        [RegularExpression(EntityConstants.RegxPasswordPattern, ErrorMessage = EntityConstants.PasswordErrorMessage)]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(40, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ConfirmPassword { get; set; }
    }

    [DataContract, Serializable]
    public class ForgotPasswordRequest
    {
        [DataMember]
        [Display(Name = "User Id", Description = "User Id")]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(125, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        public virtual string UserId { get; set; }

        [DataMember]
        [Display(Name = "Reset Url", Description = "Reset Url")]
        public virtual string ResetUrl { get; set; }

        [DataMember]
        [Display(Name = "Reset Token", Description = "Reset Token")]
        public virtual string ResetToken { get; set; }
    }

    [DataContract, Serializable]
    public class ResetPasswordRequest
    {
        [DataMember]
        [Display(Name = "User Id", Description = "User Id")]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(125, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        public virtual string UserId { get; set; }

        [DataMember]
        [Display(Name = "New Password", Description = "New Password")]
        [RegularExpression(EntityConstants.RegxPasswordPattern, ErrorMessage = EntityConstants.PasswordErrorMessage)]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(40, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string NewPassword { get; set; }

        [DataMember]
        [Display(Name = "Confirm Password", Description = "Confirm Password")]
        [RegularExpression(EntityConstants.RegxPasswordPattern, ErrorMessage = EntityConstants.PasswordErrorMessage)]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(40, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ConfirmPassword { get; set; }
    }

    [DataContract, Serializable]
    public class ChangeLoginModel
    {
        [DataMember]
        [Display(Name = "Role Id", Description = "Role Id")]
        [Required(ErrorMessage = "{0} is required")]
        public virtual Guid RoleId { get; set; }

        [DataMember]
        [Display(Name = "Account Id", Description = "Account Id")]
        [Required(ErrorMessage = "{0} is required")]
        public virtual Guid AccountId { get; set; }

        [DataMember]
        [Display(Name = "New Login Id", Description = "New Login Id")]
        [Required(ErrorMessage = "{0} is required")]
        public virtual string NewLoginId { get; set; }
    }

    [DataContract, Serializable]
    public class LoginRequest
    {
        [DataMember]
        [Display(Name = "User Id", Description = "User Id")]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(125, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public virtual string UserId { get; set; }

        [DataMember]
        [Display(Name = "Password", Description = "Password")]
        [RegularExpression(EntityConstants.RegxPasswordPattern, ErrorMessage = EntityConstants.PasswordErrorMessage)]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(40, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public virtual string Password { get; set; }

        [DataMember]
        [Display(Name = "User Name", Description = "User Name")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public bool RememberMe { get; set; }

        [DataMember]
        [Display(Name = "Auth Token", Description = "Auth Token")]
        public string AuthToken { get; set; }

        [DataMember]
        public bool? IsMobile { get; set; }
    }

    public class LogoutRequest
    {
        [DataMember]
        [Display(Name = "Login Unique Id", Description = "Login Unique Id")]
        public virtual string LoginUniqueId { get; set; }

        [DataMember]
        [Display(Name = "Auth Token", Description = "Auth Token")]
        public string AuthToken { get; set; }
    }

    [DataContract, Serializable]
    public class LoginResponce
    {
        public LoginResponce()
        {
            //this.RoleTransactions = new List<RoleTransactionResponce>();
        }

        [DataMember]
        [Display(Name = "Login Unique Id", Description = "Login Unique Id")]
        public virtual string LoginUniqueId { get; set; }

        [DataMember, Key]
        [Display(Name = "Login Id", Description = "Login Id")]
        public virtual string LoginId { get; set; }

        [DataMember]
        [Display(Name = "Password", Description = "Password")]
        public virtual string Password { get; set; }

        [DataMember]
        [Display(Name = "User Type Id", Description = "User Type Id")]
        public virtual Guid? UserTypeId { get; set; }

        [DataMember]
        [Display(Name = "Academic Year Id", Description = "Academic Year Id")]
        public virtual Guid? AcademicYearId { get; set; }

        [DataMember]
        [Display(Name = "User Id", Description = "User Id")]
        public virtual string UserId { get; set; }

        [DataMember]
        [Display(Name = "User Name", Description = "User Name")]
        public virtual string UserName { get; set; }

        [DataMember]
        [Display(Name = "First Name", Description = "First Name")]
        public virtual string FirstName { get; set; }

        [DataMember]
        [Display(Name = "Last Name", Description = "Last Name")]
        public virtual string LastName { get; set; }

        [DataMember]
        [Display(Name = "Designation", Description = "Designation")]
        public virtual string Designation { get; set; }

        [DataMember]
        [Display(Name = "Date Of Joining", Description = "Date Of Joining")]
        public virtual DateTime? DateOfJoining { get; set; }

        [DataMember]
        [Display(Name = "Date Of Allocation", Description = "Date Of Allocation")]
        public virtual DateTime? DateOfAllocation { get; set; }

        [DataMember]
        [Display(Name = "Email Id", Description = "Email Id")]
        public virtual string EmailId { get; set; }

        [DataMember]
        [Display(Name = "Mobile", Description = "Mobile")]
        public virtual string Mobile { get; set; }

        [DataMember]
        [Display(Name = "Role Code", Description = "Role Code")]
        public virtual string RoleCode { get; set; }

        [DataMember]
        [Display(Name = "Sector Id", Description = "Sector Id")]
        public virtual Guid? SectorId { get; set; }

        [DataMember]
        [Display(Name = "Default State Id", Description = "Default State Id")]
        public virtual string DefaultStateId { get; set; }

        [DataMember]
        [Display(Name = "Sector Id", Description = "Sector Id")]
        public virtual string StateId { get; set; }

        [DataMember]
        [Display(Name = "Division", Description = "Division")]
        public virtual Guid? DivisionId { get; set; }

        [DataMember]
        [Display(Name = "District", Description = "District")]
        public virtual string DistrictId { get; set; }

        [DataMember]
        [Display(Name = "Block", Description = "Block")]
        public virtual string BlockId { get; set; }

        [DataMember]
        [Display(Name = "Account Type", Description = "Account Type")]
        public virtual string AccountType { get; set; }

        [DataMember]
        [Display(Name = "Landing Page Url", Description = "Landing Page Url")]
        public virtual string LandingPageUrl { get; set; }

        [DataMember]
        [Display(Name = "Password Update Date", Description = "Password Update Date")]
        public virtual DateTime? PasswordUpdateDate { get; set; }

        [DataMember]
        [Display(Name = "Password Expired On", Description = "Password Expired On")]
        public virtual DateTime? PasswordExpiredOn { get; set; }

        [DataMember]
        [Display(Name = "Last Login Date", Description = "Last Login Date")]
        public virtual DateTime? LastLoginDate { get; set; }

        [DataMember]
        [Display(Name = "Invalid Attempt", Description = "Invalid Attempt")]
        public virtual int? InvalidAttempt { get; set; }

        [DataMember]
        [Display(Name = "Is Password Reset?", Description = "Is Password Reset?")]
        public virtual bool IsPasswordReset { get; set; }

        [DataMember]
        [Display(Name = "Password Reset Token", Description = "Password Reset Token")]
        public virtual string PasswordResetToken { get; set; }

        [DataMember]
        [Display(Name = "Auth Token", Description = "Auth Token")]
        public virtual string AuthToken { get; set; }

        [DataMember]
        [Display(Name = "Token Expired On", Description = "Token Expired On")]
        public virtual DateTime? TokenExpiredOn { get; set; }

        [DataMember]
        [Display(Name = "Is Locked?", Description = "Is Locked?")]
        public virtual bool IsLocked { get; set; }

        [DataMember]
        public virtual bool? IsMobile { get; set; }
    }

    [DataContract, Serializable]
    public class RoleTransactionResponce
    {
        [DataMember, Key]
        [Display(Name = "Sr No", Description = "Sr No")]
        public int SrNo { get; set; }

        [DataMember]
        [Display(Name = "Header Name", Description = "Header Name")]
        public virtual string HeaderName { get; set; }

        [DataMember]
        [Display(Name = "Header Order", Description = "Header Order")]
        public virtual int HeaderOrder { get; set; }

        [DataMember]
        [Display(Name = "Transaction Order", Description = "Transaction Order")]
        public virtual int TransactionOrder { get; set; }

        [DataMember]
        [Display(Name = "Is Header Menu?", Description = "Is Header Menu?")]
        public virtual bool IsHeaderMenu { get; set; }

        [Display(Name = "Transaction Id", Description = "Transaction Id")]
        public virtual Guid TransactionId { get; set; }

        [Display(Name = "Code", Description = "Code")]
        public virtual string Code { get; set; }

        [DataMember]
        [Display(Name = "Name", Description = "Name")]
        public virtual string Name { get; set; }

        [DataMember]
        [Display(Name = "Page Title", Description = "Page Title")]
        public virtual string PageTitle { get; set; }

        [DataMember]
        [Display(Name = "Page Description", Description = "Page Description")]
        public virtual string PageDescription { get; set; }

        [DataMember]
        [Display(Name = "Transaction Icon", Description = "Transaction Icon")]
        public string TransactionIcon { get; set; }

        [DataMember]
        [Display(Name = "Url Action", Description = "Url Action")]
        public virtual string UrlAction { get; set; }

        [DataMember]
        [Display(Name = "Url Controller", Description = "Url Controller")]
        public virtual string UrlController { get; set; }

        [DataMember]
        [Display(Name = "Url Para", Description = "Url Para")]
        public virtual string UrlPara { get; set; }

        [DataMember]
        [Display(Name = "Rights?", Description = "Rights?")]
        public bool Rights { get; set; }

        [DataMember]
        [Display(Name = "Is Add?", Description = "Is Add?")]
        public bool IsAdd { get; set; }

        [DataMember]
        [Display(Name = "Is Edit?", Description = "Is Edit?")]
        public bool IsEdit { get; set; }

        [DataMember]
        [Display(Name = "Is Delete?", Description = "Is Delete?")]
        public bool IsDelete { get; set; }

        [DataMember]
        [Display(Name = "Is View?", Description = "Is View?")]
        public bool IsView { get; set; }

        [DataMember]
        [Display(Name = "Is Export?", Description = "Is Export?")]
        public bool IsExport { get; set; }

        [DataMember]
        [Display(Name = "List View", Description = "List View")]
        public bool ListView { get; set; }

        [DataMember]
        [Display(Name = "Basic View", Description = "Basic View")]
        public bool BasicView { get; set; }

        [DataMember]
        [Display(Name = "Detailed View", Description = "Detailed View")]
        public bool DetailView { get; set; }

        [DataMember]
        [Display(Name = "Is Public?", Description = "Is Public?")]
        public bool IsPublic { get; set; }

        [DataMember]
        [Display(Name = "Route Url", Description = "Route Url")]
        public string RouteUrl { get; set; }
    }

    [DataContract, Serializable]
    public class UserTransactionResponce
    {
        [DataMember, Key]
        [Display(Name = "Sr No", Description = "Sr No")]
        public int SrNo { get; set; }

        [DataMember]
        [Display(Name = "Header Name", Description = "Header Name")]
        public virtual string HeaderName { get; set; }

        [DataMember]
        [Display(Name = "Header Order", Description = "Header Order")]
        public virtual int HeaderOrder { get; set; }

        [DataMember]
        [Display(Name = "Transaction Order", Description = "Transaction Order")]
        public virtual int TransactionOrder { get; set; }

        [DataMember]
        [Display(Name = "Is Header Menu?", Description = "Is Header Menu?")]
        public virtual bool IsHeaderMenu { get; set; }

        [Display(Name = "Transaction Id", Description = "Transaction Id")]
        public virtual Guid TransactionId { get; set; }

        [Display(Name = "Code", Description = "Code")]
        public virtual string Code { get; set; }

        [DataMember]
        [Display(Name = "Name", Description = "Name")]
        public virtual string Name { get; set; }

        [DataMember]
        [Display(Name = "Page Title", Description = "Page Title")]
        public virtual string PageTitle { get; set; }

        [DataMember]
        [Display(Name = "Page Description", Description = "Page Description")]
        public virtual string PageDescription { get; set; }

        [DataMember]
        [Display(Name = "Transaction Icon", Description = "Transaction Icon")]
        public string TransactionIcon { get; set; }

        [DataMember]
        [Display(Name = "Url Action", Description = "Url Action")]
        public virtual string UrlAction { get; set; }

        [DataMember]
        [Display(Name = "Url Controller", Description = "Url Controller")]
        public virtual string UrlController { get; set; }

        [DataMember]
        [Display(Name = "Url Para", Description = "Url Para")]
        public virtual string UrlPara { get; set; }

        [DataMember]
        [Display(Name = "Rights?", Description = "Rights?")]
        public bool Rights { get; set; }

        [DataMember]
        [Display(Name = "Is Add?", Description = "Is Add?")]
        public bool IsAdd { get; set; }

        [DataMember]
        [Display(Name = "Is Edit?", Description = "Is Edit?")]
        public bool IsEdit { get; set; }

        [DataMember]
        [Display(Name = "Is Delete?", Description = "Is Delete?")]
        public bool IsDelete { get; set; }

        [DataMember]
        [Display(Name = "Is View?", Description = "Is View?")]
        public bool IsView { get; set; }

        [DataMember]
        [Display(Name = "Is Export?", Description = "Is Export?")]
        public bool IsExport { get; set; }

        [DataMember]
        [Display(Name = "List View", Description = "List View")]
        public bool ListView { get; set; }

        [DataMember]
        [Display(Name = "Basic View", Description = "Basic View")]
        public bool BasicView { get; set; }

        [DataMember]
        [Display(Name = "Detailed View", Description = "Detailed View")]
        public bool DetailView { get; set; }

        [DataMember]
        [Display(Name = "Is Public?", Description = "Is Public?")]
        public bool IsPublic { get; set; }

        [DataMember]
        [Display(Name = "Route Url", Description = "Route Url")]
        public string RouteUrl { get; set; }
    }
}