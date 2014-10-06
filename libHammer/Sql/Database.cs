using libHammer.Design_Patterns;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Sql
{

    /// <summary>
    /// Encapsulates provider independant database interactin logic.
    /// </summary>
    /// <typeparam name="CONNECTION_TYPE"><see cref="DbConnection"/> derived Connection type.</typeparam>
    /// <typeparam name="COMMAND_TYPE"><see cref="DbCommand"/> derived Command type.</typeparam>
    /// <typeparam name="ADAPTER_TYPE"><see cref="DbDataAdapater"/> derived Data Adapter type.</typeparam>
    public class Database<CONNECTION_TYPE, COMMAND_TYPE, ADAPTER_TYPE> : IDatabase, ISupportLazyInitialization
        where CONNECTION_TYPE : DbConnection, new()
        where COMMAND_TYPE : DbCommand
        where ADAPTER_TYPE : DbDataAdapter, new()
    {

        #region : Connection :

        /// <summary>
        /// Gets the Connection object associated with the current instance.
        /// </summary>
        /// <value></value>
        public virtual DbConnection Connection
        {
            get
            {
                if (internal_currentConnection == null)
                {
                    internal_currentConnection = new CONNECTION_TYPE();
                    internal_currentConnection.ConnectionString = ConnectionString;
                }
                return internal_currentConnection;
            }
        }
        private DbConnection internal_currentConnection;

        private string _connectionString;
        /// <summary>When overridden in derived classes returns the connection string for the database.</summary>
        /// <returns>The connection string for the database.</returns>
        public string ConnectionString
        {
            get
            {
                if (_connectionString == null && ConfigurationManager.ConnectionStrings.Count > 0)
                {
                    string providerNamespace = typeof(CONNECTION_TYPE).Namespace;
                    var connectionStrings = ConfigurationManager.ConnectionStrings[providerNamespace];
                    _connectionString = connectionStrings.ConnectionString;
                    return _connectionString;
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        #endregion

        #region : Commands :

        public DbCommand GetSqlStringCommand(string sqlString)
        {
            return GetCommand(sqlString, CommandType.Text);
        }

        public DbCommand GetSqlStringCommand(string sqlStringFormat, params object[] args)
        {
            return GetSqlStringCommand(string.Format(sqlStringFormat, args));
        }

        public DbCommand GetStoredProcedureCommand(string storedProcName)
        {
            return GetCommand(storedProcName, CommandType.StoredProcedure);
        }

        public DbCommand GetCommand(string commandText, CommandType commandType)
        {
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            DbCommand cmd = this.Connection.CreateCommand();
            cmd.CommandType = commandType;
            cmd.CommandText = commandText;
            return cmd;
        }

        #region : Parameters :

        public DbParameter AddInParameter(DbCommand cmd, string paramName, DbType paramType, object value)
        {
            return AddParameter(cmd, paramName, paramType, ParameterDirection.Input, value);
        }

        public DbParameter AddInParameter(DbCommand cmd, string paramName, DbType paramType, int size, object value)
        {
            return AddParameter(cmd, paramName, paramType, ParameterDirection.Input, size, value);
        }

        public DbParameter AddOutParameter(DbCommand cmd, string paramName, DbType paramType, object value)
        {
            return AddParameter(cmd, paramName, paramType, ParameterDirection.Output, value);
        }

        public DbParameter AddOutParameter(DbCommand cmd, string paramName, DbType paramType, int size, object value)
        {
            return AddParameter(cmd, paramName, paramType, ParameterDirection.Output, size, value);
        }

        public virtual DbParameter AddParameter(DbCommand cmd, string paramName, DbType paramType, ParameterDirection direction, object value)
        {
            DbParameter param = cmd.CreateParameter();
            param.DbType = paramType;
            param.ParameterName = paramName;
            param.Value = value;
            param.Direction = direction;
            cmd.Parameters.Add(param);
            return param;
        }

        public virtual DbParameter AddParameter(DbCommand cmd, string paramName, DbType paramType, ParameterDirection direction, int size, object value)
        {
            DbParameter param = cmd.CreateParameter();
            param.DbType = paramType;
            param.ParameterName = paramName;
            param.Size = size;
            param.Value = value;
            param.Direction = direction;
            cmd.Parameters.Add(param);
            return param;
        }

        #endregion

        #region : Executes :

        public int ExecuteNonQuery(DbCommand cmd)
        {
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            return cmd.ExecuteNonQuery();
        }

        public int ExecuteNonQuery(DbCommand cmd, DbTransaction txn)
        {
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            cmd.Transaction = txn;
            return cmd.ExecuteNonQuery();
        }

        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            return cmd.ExecuteReader();
        }

        public DbDataReader ExecuteReader(DbCommand cmd, System.Data.CommandBehavior behavior)
        {
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            return cmd.ExecuteReader(behavior);
        }

        public virtual T ExecuteScalar<T>(DbCommand cmd, T defaultValue)
        {
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            object retVal = cmd.ExecuteScalar();
            if (null == retVal || DBNull.Value == retVal)
                return defaultValue;
            else
                return (T)retVal;
        }

        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            ADAPTER_TYPE adapter = new ADAPTER_TYPE();
            adapter.SelectCommand = (COMMAND_TYPE)cmd;

            DataSet retVal = new DataSet();
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapter.Fill(retVal);
            return retVal;
        }

        #endregion

        #endregion

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <returns>Created transaction.</returns>
        public DbTransaction BeginTransaction()
        {
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();
            return Connection.BeginTransaction();
        }

        #region : Consturction / Destruction :

        private static string _loadedConnectionString;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {
            if (_loadedConnectionString == null)
                lock (typeof(Database<CONNECTION_TYPE, COMMAND_TYPE, ADAPTER_TYPE>))
                {
                    if (_loadedConnectionString == null)
                    {
                        _loadedConnectionString = string.Empty;

                        string connectionTypeName = typeof(CONNECTION_TYPE).FullName;
                        var connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings;
                        foreach (ConnectionStringSettings cnxSetting in connectionStrings)
                            if (connectionTypeName.StartsWith(cnxSetting.ProviderName))
                            {
                                _loadedConnectionString = cnxSetting.ConnectionString;
                                break;
                            }
                    }
                }
            ConnectionString = _loadedConnectionString;
        }

        /// <summary>Disposes the resources associated with the current database connection.</summary>
        ~Database()
        {
            Dispose();
        }

        #region IDisposable Members

        /// <summary>Disposes the resources associated with the current database connection.</summary>
        public virtual void Dispose()
        {
            if (null != internal_currentConnection)
            {
                internal_currentConnection.Dispose();
                internal_currentConnection = null;
            }
        }

        #endregion

        #endregion
    }

}
