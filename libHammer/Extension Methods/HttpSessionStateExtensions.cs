using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace libHammer.Extension_Methods
{
    
    /// <summary>
    /// 
    /// </summary>
    public static class HttpSessionStateExtensions
    {

        /// <summary>
        /// Return a value of the session state variable.
        /// Usage: String value1 = Session.GetValue<String>("key1");
        /// </summary>
        /// <author>Mark Wiseman</author>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(this HttpSessionStateBase session, string key)
        {
            return session.GetValue<T>(key, default(T));
        }

        /// <summary>
        /// Return the value of the session state variable, or the default value if there was a problem.
        /// Usage: String value2 = Session.GetValue<String>("key2", "default text");
        /// </summary>
        /// /// <author>Mark Wiseman</author>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetValue<T>(this HttpSessionStateBase session, string key, T defaultValue)
        {
            if (session[key] != null)
            {
                return (T)Convert.ChangeType(session[key], typeof(T));
            }

            return defaultValue;
        }

    }
}
