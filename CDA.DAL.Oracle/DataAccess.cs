using Oracle.DataAccess.Client;
using System.Data;


namespace CDA.DAL
{
    namespace ORACLE
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
                mConnection = new OracleConnection(mConnectionString);
            }

            protected override void PrepareCommand()
            {
                if (mParameters != null)
                {
                    mCommand.Parameters.Clear();

                    foreach (OracleParameter param in mParameters)
                    {
                        if (((OracleCommand)mCommand).Parameters.Contains(param))
                            ((OracleCommand)mCommand).Parameters[param.ParameterName] = param;
                        else
                            ((OracleCommand)mCommand).Parameters.Add(param);
                    }
                }
            }

            #region Interface IDataAccess

            public override IDataReader OpenDataReader()
            {
                OracleDataReader dr = null;

                Open();

                PrepareCommand();

                dr = (OracleDataReader)mCommand.ExecuteReader();

                mCommand.Dispose();

                return dr;
            }

            public override IDataReader OpenDataReader(CommandBehavior behavior)
            {
                OracleDataReader dr = null;

                Open();

                PrepareCommand();

                dr = (OracleDataReader)mCommand.ExecuteReader(behavior);

                mCommand.Dispose();

                return dr;
            }

            public override DataSet OpenDataSet()
            {
                DataSet ds = new DataSet();

                Open();

                PrepareCommand();

                mDataAdapter = new OracleDataAdapter((OracleCommand)mCommand);
                mDataAdapter.Fill(ds);

                return ds;
            }

            public override DataTable OpenDataTable()
            {
                DataTable dt = new DataTable();

                Open();

                PrepareCommand();

                mDataAdapter = new OracleDataAdapter((OracleCommand)mCommand);
                mDataAdapter.Fill(dt);

                return dt;
            }

            public override void CreateCmd(CommandType type, string cmd)
            {
                mCommand = new OracleCommand(cmd, (OracleConnection)mConnection);
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
