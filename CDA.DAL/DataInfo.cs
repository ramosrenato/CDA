using System;
using System.Reflection;

namespace CDA.DAL
{
    public class DataInfo
    {
        public Type Type { get; set; }
        public PropertyInfo Property { get; set; }
        public bool IsPrimaryKey { get; set; }

    }
}
