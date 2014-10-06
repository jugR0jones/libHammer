using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Sql
{

    /// <summary>
    /// Interface for a SQL Database wrapper
    /// </summary>
    public interface IDatabase : IDisposable
    {
        /// <summary>Gets the Connection object associated with the current instance.</summary>
        DbConnection Connection { get; }

        /// <summary>Gets or sets the connection string to the database.</summary>
        /// <value>The connection string.</value>
        string ConnectionString { get; set; }

        /// <summary>Begins a transaction.</summary>
        /// <returns>Created transaction.</returns>
        DbTransaction BeginTransaction();

        /// <summary>
        /// Gets a DbCommand object with the specified <see cref="DbCommand.CommandText"/> and <see cref="DbCommand.CommandType"/>
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A DbCommand object with the specified <see cref="DbCommand.CommandText"/> and <see cref="DbCommand.CommandType"/></returns>
        DbCommand GetCommand(string commandText, CommandType commandType);
        /// <summary>Gets a DbCommand object with the specified <see cref="DbCommand.CommandText"/>.</summary>
        /// <param name="sqlString">The SQL string.</param>
        /// <returns>A DbCommand object with the specified <see cref="DbCommand.CommandText"/>.</returns>
        DbCommand GetSqlStringCommand(string sqlString);
        /// <summary>Gets a DbCommand object with the specified <see cref="DbCommand.CommandText"/>.</summary>
        /// <param name="sqlStringFormat">The SQL string format.</param>
        /// <param name="args">The format arguments.</param>
        /// <returns>A DbCommand object with the specified <see cref="DbCommand.CommandText"/>.</returns>
        DbCommand GetSqlStringCommand(string sqlStringFormat, params object[] args);
        /// <summary>Gets a DbCommand object for the specified Stored Procedure.</summary>
        /// <param name="storedProcName">The name of the stored procedure.</param>
        /// <returns>A DbCommand object for the specified Stored Procedure.</returns>
        DbCommand GetStoredProcedureCommand(string storedProcName);
        /// <summary>Adds an input parameter to the given <see cref="DbCommand"/>.</summary>
        /// <param name="cmd">The command object the parameter should be added to.</param>
        /// <param name="paramName">The identifier of the parameter.</param>
        /// <param name="paramType">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>The <see cref="DbParameter"/> that was created.</returns>
        DbParameter AddInParameter(DbCommand cmd, string paramName, DbType paramType, object value);
        /// <summary>Adds an input parameter to the given <see cref="DbCommand"/>.</summary>
        /// <param name="cmd">The command object the parameter should be added to.</param>
        /// <param name="paramName">The identifier of the parameter.</param>
        /// <param name="paramType">The type of the parameter.</param>
        /// <param name="size">The maximum size in bytes, of the data table column to be affected.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>The <see cref="DbParameter"/> that was created.</returns>
        DbParameter AddInParameter(DbCommand cmd, string paramName, DbType paramType, int size, object value);
        /// <summary>Adds an input parameter to the given <see cref="DbCommand"/>.</summary>
        /// <param name="cmd">The command object the parameter should be added to.</param>
        /// <param name="paramName">The identifier of the parameter.</param>
        /// <param name="paramType">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>The <see cref="DbParameter"/> that was created.</returns>
        DbParameter AddOutParameter(DbCommand cmd, string paramName, DbType paramType, object value);
        /// <summary>Adds an output parameter to the given <see cref="DbCommand"/>.</summary>
        /// <param name="cmd">The command object the parameter should be added to.</param>
        /// <param name="paramName">The identifier of the parameter.</param>
        /// <param name="paramType">The type of the parameter.</param>
        /// <param name="size">The maximum size in bytes, of the data table column to be affected.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>The <see cref="DbParameter"/> that was created.</returns>
        DbParameter AddOutParameter(DbCommand cmd, string paramName, DbType paramType, int size, object value);
        /// <summary>Adds an parameter to the given <see cref="DbCommand"/>.</summary>
        /// <param name="cmd">The command object the parameter should be added to.</param>
        /// <param name="paramName">The identifier of the parameter.</param>
        /// <param name="paramType">The type of the parameter.</param>
        /// <param name="direction"><see cref="ParameterDirection"/> of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>The <see cref="DbParameter"/> that was created.</returns>
        DbParameter AddParameter(DbCommand cmd, string paramName, DbType paramType, ParameterDirection direction, object value);
        /// <summary>Adds an parameter to the given <see cref="DbCommand"/>.</summary>
        /// <param name="cmd">The command object the parameter should be added to.</param>
        /// <param name="paramName">The identifier of the parameter.</param>
        /// <param name="paramType">The type of the parameter.</param>
        /// <param name="direction"><see cref="ParameterDirection"/> of the parameter.</param>
        /// <param name="size">The maximum size in bytes, of the data table column to be affected.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>The <see cref="DbParameter"/> that was created.</returns>
        DbParameter AddParameter(DbCommand cmd, string paramName, DbType paramType, ParameterDirection direction, int size, object value);

        /// <summary>Executes the specified command against the current connection.</summary>
        /// <param name="cmd">The command to be executed.</param>
        /// <returns>Result returned by the database engine.</returns>
        int ExecuteNonQuery(DbCommand cmd);
        /// <summary>Executes the specified command against the current connection.</summary>
        /// <param name="cmd">The command to be executed.</param>
        /// <param name="txn">The database transaction inside which the command should be executed.</param>
        /// <returns>Result returned by the database engine.</returns>
        int ExecuteNonQuery(DbCommand cmd, DbTransaction txn);
        /// <summary>Executes the specified command against the current connection.</summary>
        /// <param name="cmd">The command to be executed.</param>
        /// <returns>Result returned by the database engine.</returns>
        DbDataReader ExecuteReader(DbCommand cmd);
        /// <summary>Executes the specified command against the current connection.</summary>
        /// <param name="cmd">The command to be executed.</param>
        /// <param name="behavior">One of the <see cref="System.Data.CommandBehavior"/> values.</param>
        /// <returns>Result returned by the database engine.</returns>
        DbDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior);
        /// <summary>Executes the specified command against the current connection.</summary>
        /// <param name="cmd">The command to be executed.</param>
        /// <returns>Result returned by the database engine.</returns>
        T ExecuteScalar<T>(DbCommand cmd, T defaultValue);
        /// <summary>Executes the specified command against the current connection.</summary>
        /// <param name="cmd">The command to be executed.</param>
        /// <returns>Result returned by the database engine.</returns>
        DataSet ExecuteDataSet(DbCommand cmd);
    }

}
