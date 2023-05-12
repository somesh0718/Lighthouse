using Igmite.Lighthouse.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Igmite.Lighthouse.Platform
{
    public static class StringUtility
    {
        /// <summary>
        /// Get unique new generated id
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Get unique new generated id
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public static string GetUniqueCharKey(int maxSize)
        {
            char[] chars = new char[26];
            chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            byte[] data = new byte[1];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Get unique new generated id
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public static string GetOTPToken(int maxSize)
        {
            maxSize = maxSize > 9 ? 9 : maxSize;

            char[] chars = new char[62];
            chars = "1234567890".ToCharArray();
            byte[] data = new byte[1];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }

        public static string GenerateAuthToken(string deviceId, string mobile, string loginId)
        {
            TimeZoneInfo IndianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IndianTimeZone);

            string tokenValue = string.Format("{0}~{1}~{2}~{3}", deviceId, mobile, loginId, currentDateTime.ToString("ddMMyyhhmm"));

            string encryptToken = CryptographyManager.Encrypt(tokenValue, true);

            return encryptToken;
        }

        public static string ReplaceWithMinus(this string textValue, string replaceValue = "-")
        {
            string result = string.IsNullOrEmpty(textValue) ? string.Empty : textValue;

            return Regex.Replace(result, @"\s+", replaceValue);
        }

        public static string GetCleanFileName(this string fileName, bool isTruncateFileName = false)
        {
            int fileNameLength = 20;
            fileName = string.IsNullOrEmpty(fileName) ? string.Empty : fileName;

            string result = Path.GetFileNameWithoutExtension(fileName);

            result = Regex.Replace(result, @"[^0-9a-zA-Z]+", "-").ToLower();

            if (isTruncateFileName)
                result = (result.Length > fileNameLength) ? result.Substring(0, fileNameLength) : result;

            return string.Format("{0}{1}", result, Path.GetExtension(fileName));
        }

        public static string GetFolderPath(string contentType, string basePath)
        {
            string folderPath = Path.Combine(basePath, contentType);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return folderPath;
        }

        public static string GetFileCleanName(Guid contentId, string displayType, string fileName)
        {
            return string.Format("{0}-V{1}V-{2}", contentId.ToString("N").Substring(0, 8).ToUpper(), displayType, fileName.GetCleanFileName(true));
        }

        public static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();

            return mimeType;
        }

        public static DateTime? ToDateTimeNullable(this string str)
        {
            DateTime result;
            if (DateTime.TryParse(str, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static DateTime? DateTimeOrDefault(this object obj, DateTime? defaultValue)
        {
            DateTime result;

            if (obj == null || obj == System.DBNull.Value)
                return defaultValue;
            else if (obj is string)
            {
                if (DateTime.TryParse((string)obj, out result))
                {
                    return result;
                }
                else
                {
                    return defaultValue;
                }
            }
            else
            {
                DateTime? val = obj as DateTime?;
                if (val.HasValue)
                {
                    return val.Value;
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        public static string IfNullOrEmpty(this string obj, string val)
        {
            if (obj.IsNullOrEmpty())
                return val;
            else
                return obj;
        }

        public static int IntValOrZero(this object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
                return 0;
            else if (obj is string)
            {
                int outParam = 0;
                if (int.TryParse((string)obj, out outParam))
                    return outParam;
                else
                    return 0;
            }
            else
            {
                int? o = obj as int?;
                if (o.HasValue)
                    return o.Value;
                else
                    return 0;
            }
        }

        public static long LongValOrZero(this object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
                return 0;
            else if (obj is string)
            {
                long outParam = 0;
                if (long.TryParse((string)obj, out outParam))
                    return outParam;
                else
                    return 0;
            }
            else
            {
                long? o = obj as long?;
                if (o.HasValue)
                    return o.Value;
                else
                    return 0;
            }
        }

        public static double DoubleValOrZero(this object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
                return 0;
            else if (obj is string)
            {
                double outParam = 0;
                if (double.TryParse((string)obj, out outParam))
                    return outParam;
                else
                    return 0;
            }
            else
            {
                double? o = obj as double?;
                if (o.HasValue)
                    return o.Value;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Determines if the string is an array of T objects separated by the delimiter specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="delimiter">The delimiter - e.g. "," or "|"</param>
        /// <param name="isTrueIfSingleElement">Whether to return TRUE if array is of single element</param>
        /// <returns></returns>
        public static bool IsArray<T>(this string str, string delimiter, bool isTrueIfSingleElement)
        {
            if (!string.IsNullOrEmpty(str) && str.Trim().Length > 0)
            {
                string[] elements = str.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                if (elements.Length == 1)
                {
                    if (isTrueIfSingleElement)
                    {
                        try
                        {
                            Convert.ChangeType(elements[0], typeof(T));
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (elements.Length > 1)
                {
                    for (int i = 0; i < elements.Length; i++)
                    {
                        try
                        {
                            Convert.ChangeType(elements[i], typeof(T));
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the string is an array of T objects separated by the delimiter specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="delimiter">The delimiter - e.g. "," or "|"</param>
        /// <remarks>Returns FALSE if array is of length = 1</remarks>
        public static bool IsArray<T>(this string str, string delimiter)
        {
            return IsArray<T>(str, delimiter, false);
        }

        /// <summary>
        /// Determines if the string is an array of T objects separated by the delimiter specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="delimiter">The delimiter - e.g. "," or "|"</param>
        /// <remarks>Returns TRUE if array is of length = 1 or greater</remarks>
        public static bool IsArrayOrSingleElement<T>(this string str, string delimiter)
        {
            return IsArray<T>(str, delimiter, true);
        }

        /// <summary>
        /// Converts the string into an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this string str, string delimiter)
        {
            if (!string.IsNullOrEmpty(str) && str.Trim().Length > 0)
            {
                string[] elements = str.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                if (elements.Length >= 1)
                {
                    T[] result = new T[elements.Length];
                    for (int i = 0; i < elements.Length; i++)
                    {
                        try
                        {
                            result[i] = (T)Convert.ChangeType(elements[i], typeof(T));
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    return result;
                }
            }

            return null;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotNullAndEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// If object is null, then an empty string is returned.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string StringVal(this object obj)
        {
            string strValue = string.Empty;

            if (obj == null || obj == System.DBNull.Value)
                strValue = string.Empty;
            else
                strValue = obj.ToString();

            Regex regex = new Regex("[ ]{2,}", RegexOptions.None);

            return regex.Replace(strValue.TrimStart().TrimEnd(), " ");
        }

        /// <summary>
        /// Used to convert strings containing chars such as _2D00_ or _2E00_ to space and periods.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HexToAscii(this object str)
        {
            string input = str.StringVal();
            Match match = System.Text.RegularExpressions.Regex.Match(input, "(_[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]_)");
            if (match.Captures.Count > 0)
            {
                foreach (var capture in match.Captures)
                {
                    byte hexChar = Convert.ToByte(capture.StringVal().Substring(1, 2), 16);
                    string asciiChar = System.Text.Encoding.ASCII.GetString(new byte[] { hexChar });
                    input = input.Replace(capture.StringVal(), asciiChar);
                }
            }

            return input;
        }

        public static bool BoolVal(this object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
                return false;
            else if (obj is string)
            {
                bool retVal = false;
                return bool.TryParse((string)obj, out retVal) ? retVal : false;
            }
            else
                return (bool?)obj ?? false;
        }

        public static bool BoolVal(this object obj, bool defaultVal)
        {
            if (obj == null || obj == System.DBNull.Value)
                return defaultVal;
            else if (obj is string)
            {
                bool retVal = false;
                return bool.TryParse((string)obj, out retVal) ? retVal : defaultVal;
            }
            else
                return (bool?)obj ?? defaultVal;
        }

        /// <summary>
        /// e.g. dictionary.DictionaryToString("&lt;br/&gt;", " = ")
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="dict"></param>
        /// <param name="rowDelimiter"></param>
        /// <param name="keyValSeparator"></param>
        /// <returns></returns>
        public static string DictionaryToString<TKey, TVal>(this IDictionary<TKey, TVal> dict, string rowDelimiter, string keyValSeparator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TKey key in dict.Keys)
            {
                sb.AppendFormat("{0}{1}{2}{3}", key.StringVal(), keyValSeparator, dict[key].StringVal(), rowDelimiter);
            }
            return sb.ToString();
        }

        public static string EnumerableToString<T>(this IEnumerable<T> enumerable, string delimiter, bool dropLastDelimiter)
        {
            if (enumerable == null || enumerable.Count() == 0)
                return string.Empty;

            if (delimiter == null)
                delimiter = string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (T val in enumerable)
            {
                sb.Append(val.ToString()).Append(delimiter);
            }

            return dropLastDelimiter ? sb.ToString(0, sb.Length - delimiter.Length) : sb.ToString();
        }

        public static string DictionaryToString(this System.Collections.IDictionary dict, string title, string delimiter, bool dropLastDelimiter)
        {
            if (dict == null || dict.Keys.Count == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(delimiter).Append("<b>").Append(title).Append("</b>").Append(delimiter);

            foreach (object key in dict.Keys)
            {
                sb.Append("<b>").Append(key.ToString()).Append("</b>").Append(" = ").Append(dict[key].ToString()).Append(delimiter);
            }

            return dropLastDelimiter ? sb.ToString(0, sb.Length - delimiter.Length) : sb.ToString();
        }

        private static Regex stripHtmlRegex = new Regex("<[^>]+>", RegexOptions.IgnoreCase);

        public static string StripHTML(this string htmlText)
        {
            return HttpUtility.HtmlDecode(stripHtmlRegex.Replace(htmlText, ""));
        }

        public static string ConvertHtmlToPlainText(this string html)
        {
            try
            {
                string result;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = html.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"•", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"‹", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"›", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"™", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"⁄", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @">", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"©", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"®", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // for testing
                //System.Text.RegularExpressions.Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch
            {
                // MessageBox.Show("Error");
                return string.Empty;
            }
        }

        public static string TrimSpaces(this string stringValue)
        {
            Regex regex = new Regex("[ ]{2,}", RegexOptions.None);

            return regex.Replace(stringValue, " ");
        }

        public static string ToTitleCase(this string stringValue)
        {
            Regex regex = new Regex("[ ]{2,}", RegexOptions.None);

            string results = regex.Replace(stringValue, " ");

            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(results);
        }

        // Convert the string to Pascal case.
        public static string ToPascalCase(this string inputValue)
        {
            // If there are 0 or 1 characters, just return the string.
            if (inputValue == null)
                return inputValue;
            if (inputValue.Length < 2 || inputValue.Contains("_"))
                return inputValue.ToUpper();

            // Split the string into words.
            string[] words = inputValue.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = "";
            foreach (string word in words)
            {
                int countUpperCase = Regex.Matches(word, @"\p{Lu}").Count;

                if (countUpperCase > 1)
                {
                    result += word.Substring(0, 1).ToUpper() + word.Substring(1);
                }
                else
                {
                    result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
                }
            }

            result = result.EndsWith("id") ? result.Replace("id", "Id") : result;

            return result;
        }

        // Convert the string to camel case.
        public static string ToCamelCase(this string inputValue)
        {
            // If there are 0 or 1 characters, just return the string.
            if (inputValue == null || inputValue.Length < 2)
                return inputValue;

            // Split the string into words.
            string[] splitWordWithSpaces = AddSpacingInName(inputValue).Split(' ');

            // Combine the words.

            string firstWord = splitWordWithSpaces[0];
            if (firstWord.Length > 2 && char.IsUpper(firstWord, 2))
            {
                string upperCasePattern = @"([A-Z]+)";
                var matchWords = Regex.Matches(firstWord, upperCasePattern).Cast<Match>().Select(m => m.Value);
                var withSpaces = string.Join("", matchWords);

                string replaceWord = withSpaces.Substring(0, withSpaces.Length - 1).ToLower() + withSpaces.Substring(withSpaces.Length - 1);
                splitWordWithSpaces[0] = firstWord.Replace(withSpaces, replaceWord);
            }
            else
            {
                splitWordWithSpaces[0] = splitWordWithSpaces[0].ToLower();
            }

            return string.Join("", splitWordWithSpaces);
        }

        public static string AddSpacingInName(this string inputValue, string separator = " ")
        {
            //inputValue = "ThisIsAnInputString.";

            //this will put a space before all capitals that are preceded by a lowercase character
            string resultString = Regex.Replace(inputValue, @"([a-z])([A-Z])", string.Format("$1{0}$2", separator));

            //Output: This Is An Input String.
            return resultString;
        }
    }
}