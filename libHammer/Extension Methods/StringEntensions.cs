using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Converts a string to its enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Parse<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        #region Conversions

        /// <summary>
        /// Convert a string into an array of bytes.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string value)
        {
            byte[] bytes = new byte[value.Length * sizeof(char)];
            System.Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Convert the string to one of the supported data types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this string text)
        {
            T result = default(T);
            System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (tc.CanConvertFrom(text.GetType()))
                result = (T)tc.ConvertFrom(text);
            else
            {
                tc = System.ComponentModel.TypeDescriptor.GetConverter(text.GetType());
                if (tc.CanConvertTo(typeof(T)))
                    result = (T)tc.ConvertTo(text, typeof(T));
                else
                    throw new NotSupportedException();
            }
            return result;
        }

        /// <summary>
        /// Converts a string to a 16bit integer value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int16 ToInt16(this string value, Int16 defaultValue=0)
        {
            Int16 result = defaultValue;
            if (!String.IsNullOrEmpty(value))
            {
                Int16.TryParse(value, out result);
            }

            return result;
        }

        /// <summary>
        /// Converts a string to a 32bit integer value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int32 ToInt32(this string value, Int32 defaultValue = 0)
        {
            Int32 result = defaultValue;
            if (!String.IsNullOrEmpty(value))
            {
                Int32.TryParse(value, out result);
            }

            return result;
        }

        /// <summary>
        /// Converts a string to a 64bit integer value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int64 ToInt16(this string value, Int64 defaultValue = 0)
        {
            Int64 result = defaultValue;
            if (!String.IsNullOrEmpty(value))
            {
                Int64.TryParse(value, out result);
            }

            return result;
        }

        /// <summary>
        /// Converts a string to a double value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToInt16(this string value, double defaultValue = 0)
        {
            double result = defaultValue;
            if (!String.IsNullOrEmpty(value))
            {
                Double.TryParse(value, out result);
            }

            return result;
        }

        #endregion

        /// Like linq take - takes the first x characters
        public static string Take(this string theString, int count, bool ellipsis = false)
        {
            int lengthToTake = Math.Min(count, theString.Length);
            var cutDownString = theString.Substring(0, lengthToTake);

            if (ellipsis && lengthToTake < theString.Length)
                cutDownString += "...";

            return cutDownString;
        }

        //like linq skip - skips the first x characters and returns the remaining string
        public static string Skip(this string theString, int count)
        {
            int startIndex = Math.Min(count, theString.Length);
            var cutDownString = theString.Substring(startIndex - 1);

            return cutDownString;
        }

        //reverses the string... pretty obvious really
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        //ditches html tags - note it doesnt get rid of things like &nbsp;
        public static string StripHtml(this string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return Regex.Replace(html, @"<[^>]*>", string.Empty);
        }

        public static bool Match(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
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
        public static bool UrlAvailable(this string httpUrl)
        {
            if (!httpUrl.StartsWith("http://") || !httpUrl.StartsWith("https://"))
                httpUrl = "http://" + httpUrl;
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse myHttpWebResponse =
                   (HttpWebResponse)myRequest.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
