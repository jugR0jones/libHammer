using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Extension_Methods
{

    /// <summary>
    /// Extension methods for dealing with an assembly
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Loads the configuration from assembly attributes
        /// </summary>
        /// <author>James Michael Hare (BlackRabbitCoder)</author>
        /// <typeparam name="T">The type of the custom attribute to find.</typeparam>
        /// <param name="callingAssembly">The calling assembly to search.</param>
        /// <returns>The custom attribute of type T, if found.</returns>
        //public static T GetAttribute<T>(this Assembly callingAssembly)
        //    where T : Attribute
        //{
        //    T result = null;

        //    // Try to find the configuration attribute for the default logger if it exists
        //    object[] configAttributes = Attribute.GetCustomAttributes(callingAssembly,
        //        typeof(T), false);

        //    // get just the first one
        //    if (!configAttributes.IsNullOrEmpty())
        //    {
        //        result = (T)configAttributes[0];
        //    }

        //    return result;
        //}
    }

}
