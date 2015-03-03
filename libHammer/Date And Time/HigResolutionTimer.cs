using System;
using System.Runtime.InteropServices;

namespace libHammer.Date_And_Time
{
    /// <summary>
    /// This is only Available on Windows 8+
    /// Usage HighResolutionDateTime.UtcNow
    /// </summary>
    public static class HighResolutionDateTime
    {
        public static bool IsAvailable { get; private set; }

        /* Load an external function. */
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.Winapi)]
        private static extern void GetSystemTimePreciseAsFileTime(out long filetime);
        public static DateTime UtcNow {
            get {
                if (!IsAvailable) {
                    throw new InvalidOperationException("High resolution clock isn't available.");
                }
                long filetime;
                GetSystemTimePreciseAsFileTime(out filetime);

                return DateTime.FromFileTimeUtc(filetime);
            }
        }

        /// <summary>
        ///
        /// </summary>
        static HighResolutionDateTime()
        {
            try
            {
                long filetime; GetSystemTimePreciseAsFileTime(out filetime); IsAvailable = true;
            }
            catch (EntryPointNotFoundException)
            {
                // Not running Windows 8 or higher. 
                IsAvailable = false;
            }
        }

    }
}