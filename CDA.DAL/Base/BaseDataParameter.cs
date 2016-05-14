using System;
using System.Data.Common;
using System.Collections;

namespace CDA.DAL
{
    public abstract class BaseDataParameter : ICollection, IDataParameter
    {

        #region Attributes

        protected ArrayList mParameters;

        #endregion

        #region Constructors

        public BaseDataParameter()
        {
            mParameters = new ArrayList();
        }

        #endregion

        #region Interface ICollection

        public void CopyTo(Array array, int index)
        {
            mParameters.CopyTo(array, index);
        }

        public bool IsSynchronized
        {
            get { return mParameters.IsSynchronized; }
        }

        public int Count
        {
            get { return mParameters.Count; }
        }

        public object SyncRoot
        {
            get { return mParameters.SyncRoot; }
        }

        #region Interface IEnumerable

        public IEnumerator GetEnumerator()
        {
            return mParameters.GetEnumerator();
        }

        #endregion

        #endregion

        #region Interface IDisposable

        public void Dispose()
        {
            mParameters.Clear();
        }

        #endregion

        #region Interface IDataParameter

        public abstract void Add(string parameter, string value);
        public abstract void Add(string parameter, char value);
        public abstract void Add(string parameter, float value);
        public abstract void Add(string parameter, double value);
        public abstract void Add(string parameter, short value);
        public abstract void Add(string parameter, int value);
        public abstract void Add(string parameter, long value);
        public abstract void Add(string parameter, DateTime value);
        public abstract void Add(string parameter, byte[] value);
        public abstract void Add(string parameter, byte value);
        public abstract void Add(DbParameter parameter);
        public abstract void Add(string parameter, DBNull value);

        #endregion

    }
}
