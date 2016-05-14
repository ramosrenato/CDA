using System;
using System.Data.Common;
using System.Configuration;
using System.Data;

namespace CDA.DAL
{
    public delegate void Method<T>(T obj, IDataReader dr);

    
    public abstract class BaseDataAccess : IDisposable, IDataAccess
    {

        #region Attributes

        #region ConnectionString

        protected string mConnectionString;
        public string ConnectionString
        {
            get { return mConnectionString; }
        }

        #endregion

        #region ExistTransaction

        public bool ExistTransaction
        {
            get { return (mTransaction != null); }
        }

        #endregion

        #region Disposed

        protected bool mDisposed = false;

        #endregion

        #region Connection

        protected DbConnection mConnection;

        #endregion

        #region Command

        protected DbCommand mCommand;
        public DbCommand Command
        {
            get { return mCommand; }
        }

        #endregion

        #region Transaction

        protected DbTransaction mTransaction;

        #endregion

        #region DataAdapter

        protected DbDataAdapter mDataAdapter;

        #endregion

        #region Parameters

        protected BaseDataParameter mParameters;
        public BaseDataParameter Parameters
        {
            get { return mParameters; }
            set { mParameters = value; }
        }

        #endregion

        #endregion

        #region Constructor

        protected BaseDataAccess(string connectionString)
        {
            FillConnectionString(connectionString);
        }

        protected BaseDataAccess()
        {
            FillConnectionString();
        }

        #endregion

        #region Destructor

        ~BaseDataAccess()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!mDisposed)
            {
                if (mCommand.Parameters != null) 
                    mCommand.Parameters.Clear();

                if (disposing)
                {
                    if (mConnection.State != ConnectionState.Closed)
                        mConnection.Close();
                }

                mDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods Connection

        private void FillConnectionString(string connectionString)
        {
            mConnectionString = (ConfigurationManager.ConnectionStrings[connectionString] != null) ?
                ConfigurationManager.ConnectionStrings[connectionString].ConnectionString :
                connectionString;
        }

        private void FillConnectionString()
        {
            mConnectionString = (ConfigurationManager.ConnectionStrings[0] != null) ?
                ConfigurationManager.ConnectionStrings[0].ConnectionString :
                "";
        }

        public void Open()
        {
            if ((mConnection != null) && (mConnection.State == ConnectionState.Closed))
                mConnection.Open();
        }

        public void Close()
        {
            if (mConnection != null)
            {
                mConnection.Close();
                mConnection.Dispose();
            }
        }

        protected abstract void CreateConnection();

        #endregion

        #region Methods Prepare

        protected abstract void PrepareCommand();

        #endregion

        #region Interface IDataAccess

        public abstract void CreateCmd(CommandType type, string cmd);

        public int ExecuteNonQuery()
        {
            Open();

            PrepareCommand();

            return mCommand.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            Open();

            PrepareCommand();

            return mCommand.ExecuteScalar();
        }

        public abstract DataSet OpenDataSet();

        public abstract DataTable OpenDataTable();

        public abstract IDataReader OpenDataReader();

        public abstract IDataReader OpenDataReader(CommandBehavior behavior);

        #endregion

        #region Transaction

        public void BeginTransaction()
        {
            if (mConnection.State == ConnectionState.Closed)
                mConnection.Open();

            mTransaction = mConnection.BeginTransaction();
        }

        public void BeginTransaction(IsolationLevel level)
        {
            if (mConnection.State == ConnectionState.Closed)
                mConnection.Open();

            mTransaction = mConnection.BeginTransaction(level);
        }

        public void Commit()
        {
            if (mTransaction != null)
            {
                try
                {
                    mTransaction.Commit();
                }
                finally
                {
                    mTransaction.Dispose();
                    mTransaction = null;
                }
            }
        }

        public void Rollback()
        {
            if (mTransaction != null)
            {
                try
                {
                    mTransaction.Rollback();
                }
                finally
                {
                    mTransaction.Dispose();
                    mTransaction = null;
                }
            }
        }

        #endregion

    }
}
