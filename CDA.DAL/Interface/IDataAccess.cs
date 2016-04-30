using System;
using System.Data;
using System.Data.Common;

namespace CDA.DAL
{
    public interface IDataAccess
    {

        DbCommand Command { get; }

        BaseDataParameter Parameters { get; set; }

        void CreateCmd(CommandType type, string cmd);

        int ExecuteNonQuery();

        object ExecuteScalar();

        DataSet OpenDataSet();

        DataTable OpenDataTable();

        IDataReader OpenDataReader();

        IDataReader OpenDataReader(CommandBehavior behavior);

    }
}
