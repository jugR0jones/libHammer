using libHammer.Design_Patterns;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Dynamic;

namespace libHammer.Sql
{

    /// <summary>
    /// MS Sql DataReader Wrapper
    /// </summary>
    public class SqlWrapper
    {

        //USAGE: 
//        string commandText = "SELECT OrderID, CustomerID FROM dbo.Orders;";
//foreach (var row in SimpleQuery.Execute(Settings.Default.NorthwindConnectionString, commandText)) {
//    Console.WriteLine(String.Format("{0}, {1}", row.OrderID, row.CustomerID));
//}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="connString"></param>
    /// <param name="commandText"></param>
    /// <returns></returns>
    public static IEnumerable<dynamic> Execute(string connString, string commandText)
        {
            using (var connection = new SqlConnection(connString))
            {
                using (var command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        foreach (IDataRecord record in reader)
                        {
                            yield return new DataRecordDynamicWrapper(record);
                        }
                    }
                }
            }
        }

    }

    public class DataRecordDynamicWrapper : DynamicObject
    {
        private IDataRecord _dataRecord;
        public DataRecordDynamicWrapper(IDataRecord dataRecord) { _dataRecord = dataRecord; }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _dataRecord[binder.Name];
            return result != null;
        }
    }

}
