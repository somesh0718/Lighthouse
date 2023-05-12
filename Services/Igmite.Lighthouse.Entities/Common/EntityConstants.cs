using System;
using System.Runtime.InteropServices;

namespace Igmite.Lighthouse.Entities
{
    public class EntityConstants
    {
        public static string SQLConnectionString = string.Empty;
        public static string Version = string.Empty;
        public static string SqlDatabaseName = string.Empty;
        public static string SecretKey = string.Empty;
        public static string TestToEmail = string.Empty;
        public static string TestToMobile = string.Empty;
        public static string DefaultAppPwd = string.Empty;
        public static bool IsDeveloperMode = false;
        public static bool IsShowErrorDetails = false;
        public static bool LazyLoadingAllowed = false;
        public static string HttpMethods = string.Empty;
        public static string HttpHeaders = string.Empty;
        public static double HttpMaxAge = 0;
        public static string CorsServiceUrl = string.Empty;
        public static string CorsWebsiteUrl = string.Empty;
        public static string RootPath = string.Empty;
        public static string AssetsPath = string.Empty;
        public static string DocumentPath = string.Empty;
        public static string ServiceIPAddress = string.Empty;
        public static string ServiceIPPort = string.Empty;
        public static int PageSize = 0;
        public static string SupportEmail = string.Empty;
        public static int BackDatedReportingDays = 1;
        public static string DashboardCronExpression = string.Empty;
        public static string NotReportingVTCronExpression = string.Empty;
        public static string WeeklyAttendanceCronExpression = string.Empty;
        public static string WatermarkText = string.Empty;
        public static string CompressImageSize = string.Empty;

        public const string DateFormatViewOnly = "{0:dd/MMM/yyyy}";
        public const string DateTimeFormatViewOnly = "{0:yyyy/MM/dd hh:mm tt}";
        public const string DateFormatExportViewOnly = "dd-MM-yyyy";
        public const string DateTimeFormatMvcViewOnly = "yyyy/MM/dd hh:mm tt";
        public const string DateTimeFormatForService = "yyyy/MM/dd HH:mm:ss.fff";
        public const string DateTimeFormatAPIOnly = "yyyy/MM/dd hh:mm tt";

        public const string RegxMobilePattern = @"^\(?([0-9]{3})\)?([0-9]{3})([0-9]{4})$";
        public const string MobileErrorMessage = "Enter valid Mobile number";

        //Original Telephone Regx: @"^(?:\(\d{3}\)|\d{3}-)\d{4}-\d{4}$";
        public const string RegxTelephonePattern = @"^(?:\(\d{3}\)|\d{3})\d{4}\d{4}$";

        public const string TelephoneErrorMessage = "Enter valid Telephone number";

        public const string RegxPasswordPattern = @"^.*(?=.*[A-Z])(?=.*[@#$%^&+=]).*$";
        public const string PasswordErrorMessage = "Password has to be of min 6 chars and contain at least 1 capital and 1 special chars.";

        public const string RegxEmailPattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public const string EmailErrorMessage = "Enter valid Email Address";

        public const string RegxQPCodePattern = @"^([A-Z]{3})([\s/])?([A-Z]{1})([0-9]{4})$";
        public const string QPCodeErrorMessage = "Enter valid QP Code";

        // India Standard Time  Indian      Eastern Standard Time
        private static TimeZoneInfo IndianTimeZone = null;

        public static DateTime GetCurrentDateTime
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    IndianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    IndianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");

                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IndianTimeZone);
            }
        }

        public const string AadhaarNumberError = "Enter valid 12 digits Aadhaar Number";

        public const string RegxPANPattern = @"^([A-Za-z]{5}\d{4}[A-Za-z]{1})$";
        public const string PANErrorMessage = "Enter valid 10 digits PAN Number";

        public const string RequiredErrorMessage = "{0} is required";
        public const string MaxLengthErrorMessage = "{0} can't be more than {1} characters";
        public const string MinLengthErrorMessage = "{0} can't be less than {1} characters";

        public const string UploadFileExtension = "image/bmp,image/gif,image/jpeg,image/png,application/pdf,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string ImageFileExtension = "image/bmp,image/gif,image/jpeg,image/png,application/pdf";
        public const string DocumentFileExtensions = "application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document";
    }
}