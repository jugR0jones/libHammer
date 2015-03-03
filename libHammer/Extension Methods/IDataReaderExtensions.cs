using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Extension_Methods
{

    /// <summary>
    /// Extensions to IDataReader, affects SqlDataReader, etc...
    /// </summary>
    public static class IDataReaderExtensions
    {

        /// <summary>
        /// Export DataReader to CSV (List<String>). Basic example that to export
        /// data to csv from a datareader. Handle value if it contains the separator
        /// and/or double quotes but can be easily be expended to include culture
        /// (date, etc...) , max errors, and more.
        /// 
        /// Usage:
        /// List<string> rows = null;
        /// using (SqlDataReader dataReader = command.ExecuteReader())
        ///    {
        ///        rows = dataReader.ToCSV(includeHeadersAsFirstRow, separator);
        ///        dataReader.Close();
        ///    }
        /// </summary>
        /// <author>Thierry Fierens</author>
        /// <param name="dataReader"></param>
        /// <param name="includeHeaderAsFirstRow"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static List<string> ToCSV(this IDataReader dataReader, bool includeHeaderAsFirstRow, string separator)
        {
            try
            {
                List<string> csvRows = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();

                if (includeHeaderAsFirstRow)
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        if (dataReader.GetName(i) != null)
                        {
                            stringBuilder.Append(dataReader.GetName(i));
                        }
                        stringBuilder.Append(",");
                    }

                    stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
                    csvRows.Add(stringBuilder.ToString());
                }

                while (dataReader.Read())
                {
                    stringBuilder.Clear();

                    for (int i = 0; i < dataReader.FieldCount - 1; i++)
                    {
                        if (!dataReader.IsDBNull(i))
                        {
                            string value = dataReader.GetValue(i).ToString();
                            if (dataReader.GetFieldType(i) == typeof(String))
                            {
                                /* If double quotes are used in value, ensure each are replaced but 2. */
                                if (value.IndexOf("\"") >= 0)
                                {
                                    value = value.Replace("\"", "\"\"");
                                }

                                /* If separtor are is in value, ensure it is put in double quotes. */
                                if (value.IndexOf(separator) >= 0)
                                {
                                    value = "\"" + value + "\"";
                                }
                                    
                            }
                            stringBuilder.Append(value);
                        }

                        if (i < dataReader.FieldCount - 1)
                            stringBuilder.Append(separator);
                    }

                    if (!dataReader.IsDBNull(dataReader.FieldCount - 1))
                        stringBuilder.Append(dataReader.GetValue(dataReader.FieldCount - 1).ToString().Replace(separator, " "));

                    csvRows.Add(stringBuilder.ToString());
                }
                dataReader.Close();
                stringBuilder = null;
                return csvRows;
            }
            catch (Exception exception)
            {
                string errorMessage = exception.CreateLogString();
                return null;
            }
           

            
        }

    }
}
