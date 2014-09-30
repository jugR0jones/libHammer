using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Extension_Methods
{

    /// <summary>
    /// Extensions to the Console class.
    /// </summary>
    public static class ConsoleExtensions
    {

        public const ConsoleColor ErrorColor = ConsoleColor.Red;
        public const ConsoleColor WarningColor = ConsoleColor.Yellow;
        public const ConsoleColor SuccessColor = ConsoleColor.Green;
        public const ConsoleColor InfoColor = ConsoleColor.Gray;

        public static WriteErrorLine(string msg, params object[] args) {
            this.WriteLine(ErrorColor, msg, args);
        }

    }
}
