using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Extension_Methods
{

    /// <summary>
    /// Extensions to the Exception class.
    /// </summary>
    public static class ExceptionExtensions
    {

        /// <summary>
        /// Generate a human readable string containing the exception error message.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerLineNumber"></param>
        /// <returns></returns>
        public static string CreateLogString(this Exception exception, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            return String.Format("{0} ({1}) [{2}]: {3}", Path.GetFileName(callerFilePath), callerMemberName, callerLineNumber, exception.Message);
        }

        /// <summary>
        /// Populates the exception with command parameter data.
        /// Usage: exception.PopulateExceptionWithCommandParameterData(exception, sqlCommand)
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="cmd">The sql command object.</param>
        public static void PopulateExceptionWithCommandParameterData(this Exception ex, DbCommand cmd)
        {
            if (cmd != null)
            {
                AddExceptionDataValue(ex, "CommandType", cmd.CommandType.ToString());
                if (cmd.Connection != null)
                {
                    AddExceptionDataValue(ex, "ConnectionString", cmd.Connection.ConnectionString);
                }
                AddExceptionDataValue(ex, "CommandText", cmd.CommandText);
                if (cmd.Parameters != null)
                {
                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        AddExceptionDataValue(ex, parameter.ParameterName, parameter.Value);
                    }
                }
            }
            else
            {
                AddExceptionDataValue(ex, "Additional Info: ", "Failed to create command object.");
            }
        }

        /// <summary>
        /// Adds data to an exception to help troubleshoot any problems, but first checks to make sure
        /// that the key does not already exist so a new exception won't get raised.
        /// </summary>
        /// <param name="ex">Exception to add data to</param>
        /// <param name="key">Data Key</param>
        /// <param name="value">Data value</param>
        public static void AddExceptionDataValue(this Exception ex, object key, object value)
        {

            if (!ex.Data.Contains(key))
            {
                ex.Data.Add(key, value.GetNullSafeString().Trim());
            }
        }
    }

}
