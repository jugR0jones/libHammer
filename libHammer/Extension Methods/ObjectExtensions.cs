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

        /// <summary>
        /// Replaces multiple OR clauses in an IF statement with a single, easy to read class.
        /// Usage: if(reallyLongIntegerVariableName.In(1,6,9,11))....
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsAnyOf<T>(this T source, params T[] list)
        {
            if (null == source)
            {
                throw new ArgumentNullException("source");
            }
            return list.Contains(source);
        }

        /// <summary>
        /// Check if an object is between the lower and upper limit.
        /// Usage: if (myNumber.Between(3,7)) ...
        /// </summary>
        /// <author>CMS - http://stackoverflow.com/posts/271444/revisions</author>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) < 0;
        }
    }

}
