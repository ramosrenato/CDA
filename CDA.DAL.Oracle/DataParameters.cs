using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;


namespace CDA.DAL
{
    namespace ORACLE
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

            public OracleParameter this[int i]
            {
                get { return (OracleParameter)mParameters[i]; }
            }

            public OracleParameter this[string name]
            {
                get
                {
                    int index;
                    return (TryGetIndexOfKey(name, out index)) ? this[index] : null;
                }
            }

            public List<OracleParameter> ToList()
            {
                List<OracleParameter> list = new List<OracleParameter>();

                foreach (OracleParameter item in this)
                    list.Add(item);

                return list;
            }

            public OracleParameter[] ToArray()
            {
                return (OracleParameter[])mParameters.ToArray();
            }

            #region Add Parameters

            private OracleParameter Add(OracleParameter p)
            {
                return this[mParameters.Add(p)];
            }

            public override void Add(DbParameter parameter)
            {
                Add((OracleParameter)parameter);
            }

            public override void Add(string parameter, string value)
            {
                Add(new OracleParameter(parameter, OracleDbType.Varchar2, value.Length)).Value = value;
            }

            public override void Add(string parameter, char value)
            {
                Add(new OracleParameter(parameter, OracleDbType.Byte)).Value = value;
            }

            public override void Add(string parameter, DateTime value)
            {
                Add(new OracleParameter(parameter, OracleDbType.TimeStamp)).Value = value;
            }

            public override void Add(string parameter, long value)
            {
                Add(new OracleParameter(parameter, OracleDbType.Int64)).Value = value;
            }

            public override void Add(string parameter, int value)
            {
                Add(new OracleParameter(parameter, OracleDbType.Int32)).Value = value;
            }

            public override void Add(string parameter, short value)
            {
                Add(new OracleParameter(parameter, OracleDbType.Int16)).Value = value;
            }

            public override void Add(string parameter, float value)
            {
                Add(new OracleParameter(parameter, OracleDbType.Decimal)).Value = value;
            }

            public override void Add(string parameter, double value)
            {
                Add(new OracleParameter(parameter, OracleDbType.Double)).Value = value;
            }

            public override void Add(string parameter, byte value)
            {
                Add(new OracleParameter(parameter, OracleDbType.Byte)).Value = value;
            }

            public override void Add(string parameter, byte[] value)
            {
                Add(new OracleParameter(parameter, OracleDbType.Blob)).Value = value;
            }

            /*public void Add(string parameter, bool value)
            {
                Add(new OracleParameter(parameter, OracleDbType.)).Value = value;
            }*/

            public void Add(string parameter, decimal value)
            {
                Add(new OracleParameter(parameter, OracleDbType.Decimal)).Value = value;
            }

            public void Add(string parameter, TimeSpan value)
            {
                Add(new OracleParameter(parameter, OracleDbType.TimeStamp)).Value = value;
            }

            /*public void Add(string parameter, Guid value)
            {
                Add(new OracleParameter(parameter, OracleDbType.)).Value = value;
            }*/

            public override void Add(string parameter, DBNull value)
            {
                Add(new OracleParameter(parameter, DBNull.Value)).Value = value;
            }

            #endregion

        }
    }
}
