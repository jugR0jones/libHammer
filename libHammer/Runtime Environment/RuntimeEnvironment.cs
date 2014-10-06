using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Runtime_Environment
{

    /// <summary>
    /// Holds common Application settings.
    /// </summary>
    public static class RuntimeEnviornment
    {
        /// <summary>
        /// Title of the current Product.
        /// </summary>
        public static readonly string ProductTitle;
        /// <summary>
        /// Product Company.
        /// </summary>
        public static readonly string CompanyName;

        static RuntimeEnviornment()
        {
            Assembly assembly = Assembly.GetEntryAssembly()
                                    ?? Assembly.GetCallingAssembly()
                                    ?? Assembly.GetExecutingAssembly();

            object[] attribs = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
            if (attribs.Length > 0)
                CompanyName = (attribs[0] as AssemblyCompanyAttribute).Company;

            attribs = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
            if (attribs.Length > 0)
                ProductTitle = (attribs[0] as AssemblyTitleAttribute).Title;

        }
    }

}
