using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace CDA.DAL
{
    namespace MYSQL
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

            public MySqlParameter this[int i]
            {
                get { return (MySqlParameter)mParameters[i]; }
            }

            public MySqlParameter this[string name]
            {
                get
                {
                    int index;
                    return (TryGetIndexOfKey(name, out index)) ? this[index] : null;
                }
            }

            public List<MySqlParameter> ToList()
            {
                List<MySqlParameter> list = new List<MySqlParameter>();

                foreach (MySqlParameter item in this)
                    list.Add(item);

                return list;
            }

            public MySqlParameter[] ToArray()
            {
                return (MySqlParameter[])mParameters.ToArray();
            }

            #region Add Parameters

            private MySqlParameter Add(MySqlParameter p)
            {
                return this[mParameters.Add(p)];
            }

            public override void Add(DbParameter parameter)
            {
                Add((MySqlParameter)parameter);
            }

            public override void Add(string parameter, string value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.VarChar, value.Length)).Value = value;
            }

            public override void Add(string parameter, char value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Byte)).Value = value;
            }

            public override void Add(string parameter, DateTime value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.DateTime)).Value = value;
            }

            public override void Add(string parameter, long value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Int64)).Value = value;
            }

            public override void Add(string parameter, int value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Int32)).Value = value;
            }

            public override void Add(string parameter, short value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Int16)).Value = value;
            }

            public override void Add(string parameter, float value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Float)).Value = value;
            }

            public override void Add(string parameter, double value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Double)).Value = value;
            }

            public override void Add(string parameter, byte value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Byte)).Value = value;
            }

            public override void Add(string parameter, byte[] value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Blob)).Value = value;
            }

            public void Add(string parameter, bool value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Bit)).Value = value;
            }

            public void Add(string parameter, decimal value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Decimal)).Value = value;
            }

            public void Add(string parameter, TimeSpan value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Time)).Value = value;
            }

            public void Add(string parameter, Guid value)
            {
                Add(new MySqlParameter(parameter, MySqlDbType.Guid)).Value = value;
            }

            public override void Add(string parameter, DBNull value)
            {
                Add(new MySqlParameter(parameter, DBNull.Value)).Value = value;
            }

            #endregion

        }
    }
}
