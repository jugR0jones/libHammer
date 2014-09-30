using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Extension_Methods
{

    /// <summary>
    /// Extension methods for the Object class.
    /// </summary>
    public static class ObjectExtensions
    {

        /// <summary>
        /// Generate an empty string if the object is null.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetNullSafeString(this Object obj)
        {
            if (obj == null)
            {
                return String.Empty;
            }

            return obj.ToString();
        }

    }

}
