using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;


namespace CDA.DAL
{
    public class DataMapper
    {

        #region Properties

        private static Dictionary<string, IDictionary<string, DataInfo>> mMapPropertyInfo = new Dictionary<string, IDictionary<string, DataInfo>>();

        #endregion

        #region Methods

        #region Map

        private static bool CacheExists(string className)
        {
            return mMapPropertyInfo.ContainsKey(className);
        }

        public static IDictionary<string, DataInfo> Map<T>()
        {
            Type t = typeof(T);
            int idx;
            bool customAttribute = false;

            if (!CacheExists(t.Name))
            {
                mMapPropertyInfo.Add(t.Name, (new Dictionary<string, DataInfo>()));

                PropertyInfo[] properties = t.GetProperties();

                for (idx = 0; idx < properties.Length; idx++)
                {
                    customAttribute = false;

                    foreach (var attribute in properties[idx].GetCustomAttributes(true))
                    {
                        if (attribute is DataColumnAttribute)
                        {
                            DataInfo info = new DataInfo() { Type = properties[idx].PropertyType, Property = properties[idx], IsPrimaryKey = ((DataColumnAttribute)attribute).IsPrimaryKey };
                            mMapPropertyInfo[t.Name].Add(((DataColumnAttribute)attribute).Name, info);

                            customAttribute = true;
                        }
                    }

                    if (!customAttribute)
                        mMapPropertyInfo[t.Name].Add(properties[idx].Name, new DataInfo() { Type = properties[idx].PropertyType, Property = properties[idx] });

                }
            }

            return mMapPropertyInfo[t.Name];
        }

        #endregion

        #region To Enumerable using mapper

        public static IEnumerable<T> ToEnumerable<T>(IDataReader dr)
        {
            IDictionary<string, DataInfo> map = DataMapper.Map<T>();
            IList<T> list = new List<T>();
            DataInfo info;

            while (dr.Read())
            {
                T item = (T)Activator.CreateInstance(typeof(T));

                for (int j = 0; j < dr.FieldCount; j++)
                {
                    if (map.TryGetValue(dr.GetName(j), out info))
                        info.Property.SetValue(item, (dr[j].Equals(DBNull.Value) ? null : Convert.ChangeType(dr[j], info.Type)), null);
                }

                list.Add(item);
            }

            return list;
        }

        #endregion

        #region To Enumerable using dynamic code

        public static IEnumerable<T> ToEnumerable<T>(IDataReader dr, string methodName)
        {
            IList<T> list = new List<T>();

            while (dr.Read())
            {
                T obj = (T)Activator.CreateInstance(typeof(T));

                MethodInfo method = typeof(T).GetMethod(methodName);
                method.Invoke(obj, new object[] { dr });

                list.Add(obj);
            }

            return list;
        }

        #endregion

        #region To Enumerable using delegate function

        public static IEnumerable<T> ToEnumerable<T>(IDataReader dr, Method<T> method)
        {
            IList<T> list = new List<T>();

            while (dr.Read())
            {
                T obj = (T)Activator.CreateInstance(typeof(T));
                method(obj, dr);
                list.Add(obj);
            }

            return list;
        }

        #endregion

        #region To Object

        public static T ToObject<T>(IDataReader dr)
        {
            IDictionary<string, DataInfo> map = DataMapper.Map<T>();
            DataInfo info;

            T item = (T)Activator.CreateInstance(typeof(T));

            if (dr.Read())
            {
                for (int j = 0; j < dr.FieldCount; j++)
                {
                    if (map.TryGetValue(dr.GetName(j), out info))
                        info.Property.SetValue(item, (dr[j].Equals(DBNull.Value) ? null : Convert.ChangeType(dr[j], info.Type)), null);
                }
            }

            return item;
        }

        public static T ToObject<T>(IDataReader dr, Method<T> method)
        {
            T item = (T)Activator.CreateInstance(typeof(T));

            if (dr.Read())
                method(item, dr);

            return item;
        }

        public static void ToObject<T>(IDataReader dr, T item)
        {
            IDictionary<string, DataInfo> map = DataMapper.Map<T>();
            DataInfo info;

            if (dr.Read())
            {
                for (int j = 0; j < dr.FieldCount; j++)
                {
                    if (map.TryGetValue(dr.GetName(j), out info))
                        info.Property.SetValue(item, (dr[j].Equals(DBNull.Value) ? null : Convert.ChangeType(dr[j], info.Type)), null);
                }
            }
        }

        public static void ToObject<T>(IDataReader dr, T item, Method<T> method)
        {
            if (dr.Read())
                method(item, dr);
        }

        #endregion

        #endregion

    }

}
