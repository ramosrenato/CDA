using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace CDA.DAL
{
    namespace MSSQL
    {
        public class DataAccessLayer
        {

            #region CreateDataAccess

            public static DataAccess CreateDataAccess()
            {
                return new DataAccess(DataAccessLayer.GetConnectionString());
            }

            public static DataAccess CreateDataAccess(string connectionName)
            {
                return new DataAccess(DataAccessLayer.GetConnectionString(connectionName));
            }

            #endregion

            #region ExecuteNonQuery

            public static int ExecuteNonQuery(CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString()))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;

                    return da.ExecuteNonQuery();
                }
            }

            public static int ExecuteNonQuery(CommandType cmdType, string cmdText, IDataParameter cmdParms, string connectionName)
            {
                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString(connectionName)))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;

                    return da.ExecuteNonQuery();
                }
            }

            public static int ExecuteNonQuery(IDataAccess dataAccess, CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                dataAccess.CreateCmd(cmdType, cmdText);
                dataAccess.Parameters = (DataParameter)cmdParms;

                return dataAccess.ExecuteNonQuery();
            }

            #endregion

            #region ExecuteNonQueryCmd

            public static DbCommand ExecuteNonQueryCmd(CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                DbCommand cmd = null;

                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString()))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;
                    cmd = da.Command;

                    da.ExecuteNonQuery();
                }

                return cmd;
            }

            public static DbCommand ExecuteNonQueryCmd(CommandType cmdType, string cmdText, IDataParameter cmdParms, string connectionName)
            {
                DbCommand cmd = null;

                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString(connectionName)))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;
                    cmd = da.Command;

                    da.ExecuteNonQuery();
                }

                return cmd;
            }

            public static DbCommand ExecuteNonQueryCmd(IDataAccess dataAccess, CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                DbCommand cmd = null;

                dataAccess.CreateCmd(cmdType, cmdText);
                dataAccess.Parameters = (DataParameter)cmdParms;
                cmd = dataAccess.Command;

                dataAccess.ExecuteNonQuery();

                return cmd;
            }

            #endregion

            #region ExecuteReader
            
            public static IEnumerable<T> ExecuteReader<T>(CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString()))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;

                    using (IDataReader dr = da.OpenDataReader())
                    {
                        return DataMapper.ToEnumerable<T>(dr);
                    }
                }
            }

            public static IEnumerable<T> ExecuteReader<T>(CommandType cmdType, string cmdText, IDataParameter cmdParms, string connectionName)
            {
                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString(connectionName)))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;

                    using (IDataReader dr = da.OpenDataReader())
                    {
                        return DataMapper.ToEnumerable<T>(dr);
                    }
                }
            }

            public static IEnumerable<T> ExecuteReader<T>(CommandType cmdType, string cmdText, IDataParameter cmdParms, Method<T> method)
            {
                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString()))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;

                    using (IDataReader dr = da.OpenDataReader())
                    {
                        return DataMapper.ToEnumerable<T>(dr, method);
                    }
                }
            }

            public static IEnumerable<T> ExecuteReader<T>(CommandType cmdType, string cmdText, IDataParameter cmdParms, Method<T> method, string connectionName)
            {
                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString(connectionName)))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;

                    using (IDataReader dr = da.OpenDataReader())
                    {
                        return DataMapper.ToEnumerable<T>(dr, method);
                    }
                }
            }
            

            public static T ExecuteReaderObject<T>(CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                using (IDataReader dr = DataAccessLayer.OpenDataReader(cmdType, cmdText, cmdParms))
                {
                    return DataMapper.ToObject<T>(dr);
                }
            }

            public static T ExecuteReaderObject<T>(CommandType cmdType, string cmdText, IDataParameter cmdParms, string connectionName)
            {
                using (IDataReader dr = DataAccessLayer.OpenDataReader(cmdType, cmdText, cmdParms, connectionName))
                {
                    return DataMapper.ToObject<T>(dr);
                }
            }

            public static T ExecuteReaderObject<T>(CommandType cmdType, string cmdText, IDataParameter cmdParms, Method<T> method)
            {
                using (IDataReader dr = DataAccessLayer.OpenDataReader(cmdType, cmdText, cmdParms))
                {
                    return DataMapper.ToObject<T>(dr, method);
                }
            }

            public static T ExecuteReaderObject<T>(CommandType cmdType, string cmdText, IDataParameter cmdParms, Method<T> method, string connectionName)
            {
                using (IDataReader dr = DataAccessLayer.OpenDataReader(cmdType, cmdText, cmdParms, connectionName))
                {
                    return DataMapper.ToObject<T>(dr, method);
                }
            }

            #endregion

            #region ExecuteScalar

            public static object ExecuteScalar(CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString()))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;

                    return da.ExecuteScalar();
                }
            }

            public static object ExecuteScalar(CommandType cmdType, string cmdText, IDataParameter cmdParms, string connectionName)
            {
                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString(connectionName)))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;

                    return da.ExecuteScalar();
                }
            }

            public static object ExecuteScalar(IDataAccess dataAccess, CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                dataAccess.CreateCmd(cmdType, cmdText);
                dataAccess.Parameters = (DataParameter)cmdParms;

                return dataAccess.ExecuteScalar();
            }

            #endregion

            #region OpenDataReader

            public static IDataReader OpenDataReader(IDataAccess dataAccess, CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                dataAccess.CreateCmd(cmdType, cmdText);
                dataAccess.Parameters = (DataParameter)cmdParms;

                return dataAccess.OpenDataReader();
            }

            public static IDataReader OpenDataReader(CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString());

                da.CreateCmd(cmdType, cmdText);
                da.Parameters = (DataParameter)cmdParms;

                return da.OpenDataReader(CommandBehavior.CloseConnection);
            }

            public static IDataReader OpenDataReader(CommandType cmdType, string cmdText, IDataParameter cmdParms, string connectionName)
            {
                DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString(connectionName));

                da.CreateCmd(cmdType, cmdText);
                da.Parameters = (DataParameter)cmdParms;

                return da.OpenDataReader(CommandBehavior.CloseConnection);
            }

            #endregion

            #region OpenDataSet

            public static DataSet OpenDataSet(CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString()))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;

                    return da.OpenDataSet();
                }
            }

            public static DataSet OpenDataSet(IDataAccess dataAccess, CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                dataAccess.CreateCmd(cmdType, cmdText);
                dataAccess.Parameters = (DataParameter)cmdParms;

                return dataAccess.OpenDataSet();
            }

            #endregion

            #region OpenDataTable

            public static DataTable OpenDataTable(CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString()))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;

                    return da.OpenDataTable();
                }
            }

            public static DataTable OpenDataTable(CommandType cmdType, string cmdText, IDataParameter cmdParms, string connectionName)
            {
                using (DataAccess da = new DataAccess(DataAccessLayer.GetConnectionString(connectionName)))
                {
                    da.CreateCmd(cmdType, cmdText);
                    da.Parameters = (DataParameter)cmdParms;

                    return da.OpenDataTable();
                }
            }

            public static DataTable OpenDataTable(IDataAccess dataAccess, CommandType cmdType, string cmdText, IDataParameter cmdParms)
            {
                dataAccess.CreateCmd(cmdType, cmdText);
                dataAccess.Parameters = (DataParameter)cmdParms;

                return dataAccess.OpenDataTable();
            }

            #endregion

            #region GetConnectionString

            private static string GetNamespace()
            {
                return typeof(CDA.DAL.MSSQL.DataAccessLayer).Namespace.ToLower();
            }

            public static string GetConnectionString()
            {
                return ConfigurationManager.ConnectionStrings[GetNamespace()].ConnectionString;
            }

            public static string GetConnectionString(string name)
            {
                return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }

            #endregion

        }
    }
}
