using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Extension_Methods
{

    /// <summary>
    /// Extensions to IEnumerable objects.
    /// </summary>
    
    public static class IEnumerableExtensions
    {

        /// <summary>
        /// OrderBy() is nice when you want a consistent & predictable ordering. This
        /// method is NOT THAT! Randomize() - Use this extension method when you want
        /// a different or random order every time! Useful when ordering a list of
        /// things for display to give each a fair chance of landing at the top or
        /// bottom on each hit. {customers, support techs, or even use as a randomizer
        /// for your lottery ;) }
        /// </summary>
        /// <author>Phil Campbell</author>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> target)
        {
            Random random = new Random();

            return target.OrderBy(x => random.Next());
        }

        /// <summary>
        /// Orders a list based on a sortexpression. Useful in object databinding scenarios
        /// where the objectdatasource generates a dynamic sortexpression (example: "Name desc")
        /// that specifies the property of the object sort on.
        ///
        /// Usage:
        /// class Customer
        /// {
        ///   public string Name{get;set;}
        /// }
        /// 
        /// var list = new List<Customer>();
        /// 
        /// list.OrderBy("Name desc");
        /// </summary>
        /// <author>C.F.Meijers</author>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> target, string sortExpression)
        {
            sortExpression += "";
            string[] parts = sortExpression.Split(' ');
            bool descending = false;
            string property = "";

            if (parts.Length > 0 && parts[0] != "")
            {
                property = parts[0];

                if (parts.Length > 1)
                {
                    descending = parts[1].ToLower().Contains("esc");
                }

                PropertyInfo prop = typeof(T).GetProperty(property);

                if (prop == null)
                {
                    throw new Exception("No property '" + property + "' in + " + typeof(T).Name + "'");
                }

                if (descending)
                    return target.OrderByDescending(x => prop.GetValue(x, null));
                else
                    return target.OrderBy(x => prop.GetValue(x, null));
            }

            return target;
        }

        /// <summary>
        /// Provides a Distinct method that takes a key selector lambda as parameter.
        /// The .net framework only provides a Distinct method that takes an instance
        /// of an implementation of IEqualityComparer<T> where the standard parameterless
        /// Distinct that uses the default equality comparer doesn't suffice.
        /// 
        /// Usage: var instrumentSet = _instrumentBag.Distinct(i => i.Name);
        /// 
        /// </summary>
        /// <author>Martin Rosén-Lidholm</author>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="this"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> @this, Func<T, TKey> keySelector)
        {
            return @this.GroupBy(keySelector).Select(grps => grps).Select(e => e.First());
        }
    }
}
