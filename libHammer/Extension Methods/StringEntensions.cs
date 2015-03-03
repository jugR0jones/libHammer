using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace libHammer.Extension_Methods
{

    /// <summary>
    /// Extension methods to the String class.
    /// </summary>
    public static class StringEntensions
    {
        /// <summary>
        /// Used to generate MD5 Hashes. Only initialised when its needed.
        /// </summary>
        private static MD5CryptoServiceProvider _md5CryptoServiceProvider = null;

        #region Conversions

        /// <summary>
        /// Converts a string to its enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ParseEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str, true);
        }

        /// <summary>
        /// Convert a string into an array of bytes.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Converts a string to a 16bit integer value.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int16 ToInt16(this string str, Int16 defaultValue=0)
        {
            Int16 result = defaultValue;
            if (!String.IsNullOrEmpty(str))
            {
                Int16.TryParse(str, out result);
            }

            return result;
        }

        /// <summary>
        /// Converts a string to a 32bit integer value.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int32 ToInt32(this string str, Int32 defaultValue = 0)
        {
            Int32 result = defaultValue;
            if (!String.IsNullOrEmpty(str))
            {
                Int32.TryParse(str, out result);
            }

            return result;
        }

        /// <summary>
        /// Converts a string to a 64bit integer value.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int64 ToInt64(this string str, Int64 defaultValue = 0)
        {
            Int64 result = defaultValue;
            if (!String.IsNullOrEmpty(str))
            {
                Int64.TryParse(str, out result);
            }

            return result;
        }

        /// <summary>
        /// Converts a string to a double value.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defaultValue = 0)
        {
            double result = defaultValue;
            if (!String.IsNullOrEmpty(str))
            {
                Double.TryParse(str, out result);
            }

            return result;
        }

        /// <summary>
        /// Convert the string to one of the supported data types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this string str)
        {
            T result = default(T);
            System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (tc.CanConvertFrom(str.GetType()))
                result = (T)tc.ConvertFrom(str);
            else
            {
                tc = System.ComponentModel.TypeDescriptor.GetConverter(str.GetType());
                if (tc.CanConvertTo(typeof(T)))
                    result = (T)tc.ConvertTo(str, typeof(T));
                else
                    throw new NotSupportedException();
            }
            return result;
        }


        #endregion

        #region String Manipulation

        /// Like linq take - takes the first x characters
        public static string Take(this string str, int count, bool ellipsis = false)
        {
            int lengthToTake = Math.Min(count, str.Length);
            var cutDownString = str.Substring(0, lengthToTake);

            if (ellipsis && lengthToTake < str.Length)
                cutDownString += "...";

            return cutDownString;
        }

        //like linq skip - skips the first x characters and returns the remaining string
        public static string Skip(this string str, int count)
        {
            int startIndex = Math.Min(count, str.Length);
            var cutDownString = str.Substring(startIndex - 1);

            return cutDownString;
        }

        //reverses the string... pretty obvious really
        public static string Reverse(this string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        /// <summary>
        /// Returns characters from right of specified length
        /// </summary>
        /// <author>Faraz Masood Khan</author>
        /// <param name="str">String value</param>
        /// <param name="length">Max number of charaters to return</param>
        /// <returns>Returns string from right</returns>
        public static string Right(this string str, int length)
        {
            return str != null && str.Length > length ? str.Substring(str.Length - length) : str;
        }

        /// <summary>
        /// Returns characters from left of specified length
        /// </summary>
        /// <author>Faraz Masood Khan</author>
        /// <param name="str">String value</param>
        /// <param name="length">Max number of charaters to return</param>
        /// <returns>Returns string from left</returns>
        public static string Left(this string str, int length)
        {
            return str != null && str.Length > length ? str.Substring(0, length) : str;
        }

        #endregion

        public static bool Match(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        #region HTML

        /// <summary>
        /// Strips HTML tags but leaves entities.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string StripHtml(this string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return Regex.Replace(html, @"<[^>]*>", string.Empty);
        }

        /// <summary>
        /// true, if is valid email address
        /// from http://www.davidhayden.com/blog/dave/
        /// archive/2006/11/30/ExtensionMethodsCSharp.aspx
        /// </summary>
        /// <param name="s">email address to test</param>
        /// <returns>true, if is valid email address</returns>
        public static bool IsValidEmailAddress(this string s)
        {
            return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$").IsMatch(s);
        }

        /// <summary>
        /// Checks if url is valid. 
        /// from http://www.osix.net/modules/article/?id=586
        /// and changed to match http://localhost
        /// 
        /// complete (not only http) url regex can be found 
        /// at http://internet.ls-la.net/folklore/url-regexpr.html
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidUrl(this string url)
        {
            string strRegex = "^(https?://)"
        + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@
        + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184
        + "|" // allows either IP or domain
        + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www.
        + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]" // second level domain
        + @"(\.[a-z]{2,6})?)" // first level domain- .com or .museum is optional
        + "(:[0-9]{1,5})?" // port number- :80
        + "((/?)|" // a slash isn't required if there is no file name
        + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
            return new Regex(strRegex).IsMatch(url);
        }

        /// <summary>
        /// Check if url (http) is available.
        /// </summary>
        /// <param name="httpUri">url to check</param>
        /// <example>
        /// string url = "www.codeproject.com;
        /// if( !url.UrlAvailable())
        ///     ...codeproject is not available
        /// </example>
        /// <returns>true if available</returns>
        public static bool UrlAvailable(this string str)
        {
            try
            {
                if (!str.StartsWith("http://") || !str.StartsWith("https://"))
                {
                    str = "http://" + str;
                }

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(str);
                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Replace \r\n or \n by <br />
        /// from http://weblogs.asp.net/gunnarpeipman/archive/2007/11/18/c-extension-methods.aspx
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Nl2Br(this string s)
        {
            return s.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }

        #endregion

        /// <summary>
        /// true, if the string can be parse as Double respective Int32
        /// Spaces are not considred.
        /// </summary>
        /// <param name="s">input string</param>
        /// <param name="floatpoint">true, if Double is considered,
        /// otherwhise Int32 is considered.</param>
        /// <returns>true, if the string contains only digits or float-point</returns>
        //public static bool IsNumber(this string s, bool floatpoint)
        //{
        //    int i;
        //    double d;
        //    string withoutWhiteSpace = s.RemoveSpaces();
        //    if (floatpoint)
        //        return double.TryParse(withoutWhiteSpace, NumberStyles.Any,
        //            Thread.CurrentThread.CurrentUICulture, out d);
        //    else
        //        return int.TryParse(withoutWhiteSpace, out i);
        //}

        /// <summary>
        /// Remove accent from strings 
        /// </summary>
        /// <example>
        ///  input:  "Příliš žluťoučký kůň úpěl ďábelské ódy."
        ///  result: "Prilis zlutoucky kun upel dabelske ody."
        /// </example>
        /// <param name="s"></param>
        /// <remarks>founded at http://stackoverflow.com/questions/249087/
        /// how-do-i-remove-diacritics-accents-from-a-string-in-net</remarks>
        /// <returns>string without accents</returns>
        public static string RemoveDiacritics(this string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        #region Cryptography

        /// <summary>
        /// 
        /// Taken from http://weblogs.asp.net/gunnarpeipman/archive/2007/11/18/c-extension-methods.aspx
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToMd5Hash(this string str)
        {
            if (_md5CryptoServiceProvider == null)
            {
                _md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            }

            Byte[] newdata = Encoding.Default.GetBytes(str);
            Byte[] encrypted = _md5CryptoServiceProvider.ComputeHash(newdata);
            return BitConverter.ToString(encrypted).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Encryptes a string using the supplied key. Encoding is done using RSA encryption.
        /// </summary>
        /// <author>Mark de Rover</author>
        /// <param name="str">String that must be encrypted.</param>
        /// <param name="key">Encryptionkey.</param>
        /// <returns>A string representing a byte array separated by a minus sign.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToEncrypt or key is null or empty.</exception>
        public static string EncryptWithRsa(this string str, string key)
        {
            try
            {

                if (string.IsNullOrEmpty(str))
                {
                    throw new ArgumentException("An empty string value cannot be encrypted.");
                }

                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");
                }

                CspParameters cspp = new CspParameters();
                cspp.KeyContainerName = key;

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;

                byte[] bytes = rsa.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(str), true);

                return BitConverter.ToString(bytes);
            }
            catch (Exception exception)
            {
                string errorMessage = exception.CreateLogString();
                return String.Empty;
            }
        }

        /// <summary>
        /// Decryptes a string using the supplied key. Decoding is done using RSA encryption.
        /// </summary>
        /// <author>Mark de Rover</author>
        /// <param name="str">String that must be decrypted.</param>
        /// <param name="key">Decryptionkey.</param>
        /// <returns>The decrypted string or null if decryption failed.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToDecrypt or key is null or empty.</exception>
        public static string DecryptWithRsa(this string str, string key)
        {
            try
            {
                //string result = null;

                if (string.IsNullOrEmpty(str))
                {
                    throw new ArgumentException("An empty string value cannot be encrypted.");
                }

                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");
                }

                CspParameters cspp = new CspParameters();
                cspp.KeyContainerName = key;

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;

                string[] decryptArray = str.Split(new string[] { "-" }, StringSplitOptions.None);
                byte[] decryptByteArray = Array.ConvertAll<string, byte>(decryptArray, (s => Convert.ToByte(byte.Parse(s, System.Globalization.NumberStyles.HexNumber))));


                byte[] bytes = rsa.Decrypt(decryptByteArray, true);

                return System.Text.UTF8Encoding.UTF8.GetString(bytes);
            }
            catch (Exception exception)
            {
                string errorMessage = exception.CreateLogString();

                return String.Empty;
            }

        }

        #endregion

    }
}
