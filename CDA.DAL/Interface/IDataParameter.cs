using System;
using System.Data.Common;

namespace CDA.DAL
{
    public interface IDataParameter
    {

        #region string

        void Add(string parameter, string value);

        void Add(string parameter, char value);

        #endregion

        #region numeric

        void Add(string parameter, float value);

        void Add(string parameter, double value);

        void Add(string parameter, short value);

        void Add(string parameter, int value);

        void Add(string parameter, long value);

        #endregion

        #region datetime

        void Add(string parameter, DateTime value);

        #endregion

        #region binary

        void Add(string parameter, byte[] value);

        void Add(string parameter, byte value);

        #endregion

        #region generic

        void Add(DbParameter parameter);

        #endregion

        #region  null

        void Add(string parameter, DBNull value);

        #endregion

    }
}
