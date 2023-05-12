using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace Igmite.Lighthouse.Models
{
    public class Constants : ModelConstants
    {
        public const string AntiForgeryTokenSalt = "gtmtech~02jan!2018";
        public static readonly Guid DefaultAccountId = Guid.Parse("70E30807-0BC4-4445-BEBF-B5A8C16BC703");
        public static readonly Guid UserTypeId = Guid.Parse("E7FE86B4-BCA6-4C0B-92D9-15330B520E08");
        public static readonly Guid UserRoleId = Guid.Parse("A55D561D-72FA-4313-9FAE-59F07A08BFCB");
        public static readonly Guid ManagerApprovalId = Guid.Parse("935940EC-B82B-4ACB-95D6-208260817EE5");

        public static string UploadedRootFolderPath = string.Empty;
        public static string UploadFilePath = string.Empty;
        public static string BaseUploadUrl = string.Empty;
        public static string BaseUploadPath = string.Empty;
        public static string DefaultEmailId = "ritesh.gtmcs@gmail.com";
        public static string DefaultMobile = "9322826712";

        public static int OTPExpireInMinutes = 20;
        public static int PasswordExpireInDays = 90;
        public static int NotifyPasswordExpireInDays = 15;
        public static int SessionTimeoutInMinutes = 30;

        public const string AccountType = "Employee";
        public const string Administrator = "Administrator";
        public const string CountryCode = "IN";
        public const string StateCode = "MH";
        public const string MasterRole = "ADM";
        public const string SumperAdmin = "SUR";
        public const string AuthUserId = "AuthUserId";

        public const string InvalidLoginMessage = "You have entered an invalid emailId or password. Please try again.";
        public const string InvalidResetPasswordMessage = "An email link would be sent to the  email address provided if the account exists.";

        public const string GetDataMessage = "Record retrived successfully";
        public const string CreatedMessage = "Record created successfully";
        public const string UpdatedMessage = "Record updated successfully";
        public const string DeletedMessage = "Record deleted successfully";
        public const string ExistMessage = "Record already exists";
        public const string ExcelTemplateErrorMessage = "Invalid template data, Please verify template data\nMore details : {0}";

        public class User
        {
            public const string Controller = "Home";
            public const string Login = "Login";
            public const string Logout = "Logout";
            public const string PasswordExpired = "PasswordExpired";
            public const string ForgotPassword = "ForgotPassword";
            public const string ChangePassword = "ChangePassword";
            public const string SecurityError = "SecurityError";
            public const string SessionExpired = "SessionExpired";
            public const string SendCode = "SendCode";
            public const string VerifyCode = "VerifyCode";
            public const string UserRegistration = "UserRegistration";
            public const string ExternalLoginConfirmation = "ExternalLoginConfirmation";
            public const string LoginUrl = "/Home/Login";
            public const string HomeIndex = "/Home/Index";
        }

        public class HttpMethod
        {
            public const string Get = "GET";
            public const string Post = "POST";
        }

        public class SaveImageType
        {
            public const string Thumbnail = "TH";
            public const string Small = "SM";
            public const string Medium = "MD";
            public const string Original = "OR";
            public const string Watermark = "WM";

            public static readonly Size ThumbnailSize = new Size(180, 150);
            public static readonly Size SmallSize = new Size(350, 325);
            public static readonly Size MediumSize = new Size(1500, 800); //500 X 480     1500 X 800
            public static readonly Size OriginalSize = new Size(1, 1);
        }

        public class RequestTypeText
        {
            public const string New = "New";
            public const string Create = "Create";
            public const string Add = "Add";
            public const string Save = "Save";
            public const string View = "View";
            public const string Edit = "Edit";
            public const string Update = "Update";
            public const string Delete = "Delete";
            public const string Cancel = "Cancel";
            public const string Exit = "Exit";
            public const string Amend = "Amend";
            public const string Approve = "Approve";
            public const string Reject = "Reject";
            public const string Inactive = "Inactive";
            public const string ApproveReject = "Approve/Reject";
            public const string Delegate = "Delegate";
            public const string Back = "Back";
        }

        public class DocumentType
        {
            public const string Lighthouse = "Lighthouse";
        }

        public class RecordStatusText
        {
            public const string Pending = "Pending";
            public const string Waiting = "Waiting";
            public const string Resolved = "Resolved";
            public const string Onhold = "Onhold";
            public const string Completed = "Completed";
            public const string Failed = "Failed";
            public const string Success = "Success";
        }

        public static IDictionary<bool, string> AnswerTypes
        {
            get
            {
                IDictionary<bool, string> answerTypes = new Dictionary<bool, string>();
                answerTypes.Add(new KeyValuePair<bool, string>(true, "Yes"));
                answerTypes.Add(new KeyValuePair<bool, string>(false, "No"));

                return answerTypes;
            }
        }

        public const string Comments = "Comments";

        public static DateTime? GetDateValue(string dateValue)
        {
            DateTime formattedDate = Convert.ToDateTime("2010/11/12");

            if (string.IsNullOrEmpty(dateValue))
                return null;

            string dateOnlyText = dateValue.Replace("-", "/").Replace("-", "/");

            if (!string.IsNullOrEmpty(dateValue) && dateValue.Length > 5)
            {
                try
                {
                    //Sample DateTime => "15/03/2018 04:28:28 PM"

                    string[] dateArray = dateOnlyText.Split('/');

                    string dateText = string.Empty, monthText = string.Empty, yearText = string.Empty;
                    if (dateArray[2].Length == 4)
                    {
                        dateText = dateArray[0];
                        monthText = dateArray[1];
                        yearText = dateArray[2];
                    }
                    else
                    {
                        dateText = dateArray[2];
                        monthText = dateArray[1];
                        yearText = dateArray[0];
                    }

                    DateTime.TryParse(string.Format("{0}/{1}/{2} {3}", yearText, monthText, dateText, GetCurrentDateTime.ToString("hh:mm:ss tt")), out formattedDate);
                }
                catch (Exception ex1)
                {
                    try
                    {
                        formattedDate = DateTime.Parse(dateValue);
                    }
                    catch (Exception ex2)
                    {
                        formattedDate = DateTime.ParseExact(dateValue, DateFormatExportViewOnly, CultureInfo.InvariantCulture);
                    }
                }
            }

            if (formattedDate < Convert.ToDateTime("1901/01/31"))
                return null;

            return formattedDate;
        }
    }
}