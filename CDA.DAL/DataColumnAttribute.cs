using System;

namespace CDA.DAL
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class DataColumnAttribute : System.Attribute
    {

        #region Properties

        private string mName;
        public string Name
        {
            get { return mName; }
        }

        private bool mIsPrimaryKey;
        public bool IsPrimaryKey
        {
            get { return mIsPrimaryKey; }
        }

        #endregion

        #region Constructor

        public DataColumnAttribute(string name)
        {
            mName = name;
            mIsPrimaryKey = false;
        }

        public DataColumnAttribute(string name, bool isPrimaryKey)
        {
            mName = name;
            mIsPrimaryKey = isPrimaryKey;
        }

        #endregion
    }
}
