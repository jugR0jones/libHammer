using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Design_Patterns
{

    /// <summary>
    /// Represents a single, globally accessible instance of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// This could be considered a "dirty Singelton" as type <typeparamref name="T"/> breaks the rule of
    /// containing a non-public constructor.
    /// </remarks>
    public class SharedInstance<T> where T : new()
    {
        /// <summary>
        /// Gets the shared instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance { get { return SharedInstanceProvier._instance; } }

        class SharedInstanceProvier
        {
            static SharedInstanceProvier() { }

            internal static readonly T _instance = new T();
        }
    }

}
