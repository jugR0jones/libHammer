using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// Convert a string into an array of bytes.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string content)
        {
            byte[] bytes = new byte[content.Length * sizeof(char)];
            System.Buffer.BlockCopy(content.ToCharArray(), 0, bytes, 0, bytes.Length);
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

    }
}
