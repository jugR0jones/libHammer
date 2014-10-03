using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Extension_Methods
{

    /// <summary>
    /// Methods that extend the Math.Random class/
    /// </summary>
    public static class RandomExtensions
    {

        /// <summary>
        /// Returns a random item from the array of things that supplied.
        /// Usage: rand.OneOf("John", "George", "Radio XBR74 ROCKS!");
        /// </summary>
        /// <author>XYZ - http://stackoverflow.com/posts/271884/revisions</author>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <param name="things"></param>
        /// <returns></returns>
        public static T OneOf<T>(this Random random, params T[] things)
        {
            return things[random.Next(things.Length)];
        }

    }

}
