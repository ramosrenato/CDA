using MySql.Data.MySqlClient;
using System.Data;

namespace CDA.DAL
{
    namespace MYSQL
    {
        public class DataAccess : BaseDataAccess
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
                mConnection = new MySqlConnection(mConnectionString);
            }

            protected override void PrepareCommand()
            {
                if (mParameters != null)
                {
                    mCommand.Parameters.Clear();

                    foreach (MySqlParameter param in mParameters)
                    {
                        if (((MySqlCommand)mCommand).Parameters.Contains(param))
                            ((MySqlCommand)mCommand).Parameters[param.ParameterName] = param;
                        else
                            ((MySqlCommand)mCommand).Parameters.Add(param);
                    }
                }
            }

            #region Interface IDataAccess

            public override IDataReader OpenDataReader()
            {
                MySqlDataReader dr = null;

                Open();

                PrepareCommand();

                dr = (MySqlDataReader)mCommand.ExecuteReader();

                mCommand.Dispose();

                return dr;
            }

            public override IDataReader OpenDataReader(CommandBehavior behavior)
            {
                MySqlDataReader dr = null;

                Open();

                PrepareCommand();

                dr = (MySqlDataReader)mCommand.ExecuteReader(behavior);

                mCommand.Dispose();

                return dr;
            }

            public override DataSet OpenDataSet()
            {
                DataSet ds = new DataSet();

                Open();

                PrepareCommand();

                mDataAdapter = new MySqlDataAdapter((MySqlCommand)mCommand);
                mDataAdapter.Fill(ds);

                return ds;
            }

            public override DataTable OpenDataTable()
            {
                DataTable dt = new DataTable();

                Open();

                PrepareCommand();

                mDataAdapter = new MySqlDataAdapter((MySqlCommand)mCommand);
                mDataAdapter.Fill(dt);

                return dt;
            }

            public override void CreateCmd(CommandType type, string cmd)
            {
                mCommand = new MySqlCommand(cmd, (MySqlConnection)mConnection);
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
