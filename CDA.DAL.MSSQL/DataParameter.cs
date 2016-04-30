using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace CDA.DAL
{
    namespace MSSQL
    {
        public class DataParameter : BaseDataParameter, IDataParameter
        {

            private bool TryGetIndexOfKey(object key, out int index)
            {
                for (index = 0; index < mParameters.Count; index++)
                    if ((this[index]).ParameterName.Equals(key)) return true;

                return false;
            }

            public bool Contains(string name)
            {
                int index;
                return TryGetIndexOfKey(name, out index);
            }

            public SqlParameter this[int i]
            {
                get { return (SqlParameter)mParameters[i]; }
            }

            public SqlParameter this[string name]
            {
                get
                {
                    int index;
                    return (TryGetIndexOfKey(name, out index)) ? this[index] : null;
                }
            }

            public List<SqlParameter> ToList()
            {
                List<SqlParameter> list = new List<SqlParameter>();

                foreach (SqlParameter item in this)
                    list.Add(item);

                return list;
            }

            public SqlParameter[] ToArray()
            {
                return (SqlParameter[])mParameters.ToArray();
            }

            #region Add Parameters

            private SqlParameter Add(SqlParameter p)
            {
                return this[mParameters.Add(p)];
            }

            public override void Add(DbParameter parameter)
            {
                Add((SqlParameter)parameter);
            }

            public override void Add(string parameter, string value)
            {
                Add(new SqlParameter(parameter, SqlDbType.VarChar, value.Length)).Value = value;
            }

            public override void Add(string parameter, int value)
            {
                Add(new SqlParameter(parameter, SqlDbType.Int)).Value = value;
            }

            public override void Add(string parameter, char value)
            {
                Add(new SqlParameter(parameter, SqlDbType.Char)).Value = value;
            }

            public override void Add(string parameter, DateTime value)
            {
                Add(new SqlParameter(parameter, SqlDbType.DateTime)).Value = value;
            }

            public override void Add(string parameter, long value)
            {
                Add(new SqlParameter(parameter, SqlDbType.BigInt)).Value = value;
            }

            public override void Add(string parameter, short value)
            {
                Add(new SqlParameter(parameter, SqlDbType.SmallInt)).Value = value;
            }

            public override void Add(string parameter, float value)
            {
                Add(new SqlParameter(parameter, SqlDbType.Float)).Value = value;
            }

            public override void Add(string parameter, double value)
            {
                Add(new SqlParameter(parameter, SqlDbType.Float)).Value = value;
            }

            public override void Add(string parameter, byte value)
            {
                Add(new SqlParameter(parameter, SqlDbType.TinyInt)).Value = value;
            }

            public override void Add(string parameter, byte[] value)
            {
                Add(new SqlParameter(parameter, SqlDbType.VarBinary)).Value = value;
            }

            public void Add(string parameter, bool value)
            {
                Add(new SqlParameter(parameter, SqlDbType.Bit)).Value = value;
            }

            public void Add(string parameter, decimal value)
            {
                Add(new SqlParameter(parameter, SqlDbType.Decimal)).Value = value;
            }

            public void Add(string parameter, TimeSpan value)
            {
                Add(new SqlParameter(parameter, SqlDbType.Time)).Value = value;
            }

            public void Add(string parameter, Guid value)
            {
                Add(new SqlParameter(parameter, SqlDbType.UniqueIdentifier)).Value = value;
            }

            public override void Add(string parameter, DBNull value)
            {
                Add(new SqlParameter(parameter, DBNull.Value)).Value = value;
            }

            #endregion

        }
    }
}
