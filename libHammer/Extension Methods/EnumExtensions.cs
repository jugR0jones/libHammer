using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Extension_Methods
{
    public static class EnumExtensions
    {

        /// <summary>
        /// Parse a string and convert it into the appropriate enum value.
        /// Usage: StatusEnum MyStatus = EnumExtensions.ParseEnum<StatusEnum>("Active");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static T ParseEnum<T>(string value)
        //{
        //    return (T)Enum.Parse(typeof(T), value, true);
        //}

        //TODO: This needs to move to the StringExtensions class
        public static T Parse<T>(this Enum enumeratedType, string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

    }
}
