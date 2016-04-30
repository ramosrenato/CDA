using System.Data.SqlClient;
using System.Data;

namespace CDA.DAL
{
    namespace MSSQL
    {

        public sealed class DataAccess : BaseDataAccess
        {

            #region Attributes

            #region Parameters

            new public DataParameter Parameters
            {
                get { return (DataParameter)mParameters; }
                set { mParameters = value; }
            }

            #endregion

            #endregion

            #region Constructor

            public DataAccess(string connectionString) :
                base(connectionString)
            {
                CreateConnection();
            }

            public DataAccess() :
                base()
            {
                CreateConnection();
            }

            #endregion

            #region Methods Override

            protected override void CreateConnection()
            {
                mConnection = new SqlConnection(mConnectionString);
            }

            protected override void PrepareCommand()
            {
                if (mParameters != null)
                {
                    mCommand.Parameters.Clear();

                    foreach (SqlParameter param in mParameters)
                    {
                        if (((SqlCommand)mCommand).Parameters.Contains(param))
                            ((SqlCommand)mCommand).Parameters[param.ParameterName] = param;
                        else
                            ((SqlCommand)mCommand).Parameters.Add(param);
                    }
                }
            }

            #region Interface IDataAccess

            public override IDataReader OpenDataReader()
            {
                Open();

                PrepareCommand();

                SqlDataReader dr = (SqlDataReader)mCommand.ExecuteReader();

                mCommand.Dispose();

                return dr;
            }

            public override IDataReader OpenDataReader(CommandBehavior behavior)
            {

                Open();

                PrepareCommand();

                SqlDataReader dr = (SqlDataReader)mCommand.ExecuteReader(behavior);

                mCommand.Dispose();

                return dr;
            }

            public override DataSet OpenDataSet()
            {
                DataSet ds = new DataSet();

                Open();

                PrepareCommand();

                mDataAdapter = new SqlDataAdapter((SqlCommand)mCommand);
                mDataAdapter.Fill(ds);

                return ds;
            }

            public override DataTable OpenDataTable()
            {
                DataTable dt = new DataTable();

                Open();

                PrepareCommand();

                mDataAdapter = new SqlDataAdapter((SqlCommand)mCommand);
                mDataAdapter.Fill(dt);

                return dt;
            }

            public override void CreateCmd(CommandType type, string cmd)
            {
                mCommand = new SqlCommand(cmd, (SqlConnection)mConnection);
                mCommand.CommandType = type;
                mCommand.CommandTimeout = 500;

                if (ExistTransaction)
                    mCommand.Transaction = mTransaction;
            }

            #endregion

            #endregion

        }

    }
}
