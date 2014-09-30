using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Helper_Methods
{

    /// <summary>
    /// Console extensions
    /// </summary>
    public static class ConsoleHelper
    {
        #region Constants

        public const ConsoleColor ErrorColor = ConsoleColor.Red;
        public const ConsoleColor WarningColor = ConsoleColor.Yellow;
        public const ConsoleColor SuccessColor = ConsoleColor.Green;
        public const ConsoleColor InfoColor = ConsoleColor.Gray;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void ErrorLine(string message, params object[] args)
        {
            ConsoleExtensions.WriteLine(ConsoleExtensions.ErrorColor, message, args);
        }

        public static void Error(string message, params object[] args)
        {
            ConsoleExtensions.Write(ConsoleExtensions.ErrorColor, message, args);
        }

        public static void WarnLine(string msg, params object[] args)
        {
            WriteLine(WarningColor, msg, args);
        }


        public static void Warn(string msg, params object[] args)
        {
            Write(WarningColor, msg, args);
        }

        public static void InfoLine(string msg, params object[] args)
        {
            WriteLine(InfoColor ?? Console.ForegroundColor, msg, args);
        }

        public static void Info(string msg, params object[] args)
        {
            Write(InfoColor ?? Console.ForegroundColor, msg, args);
        }

        public static void SuccessLine(string msg, params object[] args)
        {
            WriteLine(SuccessColor, msg, args);
        }


        public static void Success(string msg, params object[] args)
        {
            Write(SuccessColor, msg, args);
        }


        /// <summary>
        /// Clears the current line.
        /// </summary>
        public static void ClearLine()
        {
            var position = Console.CursorLeft;

            //overwrite with white space (backspace doesn't really clear the buffer,
            //would need a hacky combination of \b\b and single whitespace)
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("".PadRight(position));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        public static void Write(string msg, params object[] args)
        {
            Console.Write(msg, args);
        }


        public static void WriteLine(ConsoleColor color, string msg, params object[] args)
        {
            Write(color, msg, args);
            Console.Out.WriteLine();
        }


        public static void Write(ConsoleColor color, string msg, params object[] args)
        {
            try
            {
                Console.ForegroundColor = color;
                Console.Write(msg, args);
            }
            finally
            {
                Console.ResetColor();
            }
        }

    }

}
