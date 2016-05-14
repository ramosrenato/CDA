using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CDA.DAL;
using CDA.DAL.MSSQL;
using System;

namespace CDA.Test
{

    public class User
    {
        [DataColumn("Id", true)]
        public int Id { get; set; }

        [DataColumn("Name")]
        public string Name { get; set; }

        [DataColumn("Active")]
        public bool Active { get; set; }

        public User()
        {

        }

    }


    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestListUsers()
        {
            string query = "SELECT * FROM [User]";

            IList<User> users = (IList<User>)DataAccessLayer.ExecuteReader<User>(CommandType.Text, query, null);

            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            DataParameter param = new DataParameter
            {
                {"p_id", 1 }
            };

            //UPDATE
            string query1 = "UPDATE [User] SET Name = 'TestUser' WHERE Id = @p_id";
            DataAccessLayer.ExecuteNonQuery(CommandType.Text, query1, param);


            // VERIFY
            string query2 = "SELECT * FROM [User] WHERE Id = @p_id";
            User user = DataAccessLayer.ExecuteReaderObject<User>(CommandType.Text, query2, param);

            
            Assert.IsTrue(user.Name.Equals("TestUser"));
        }

        [TestMethod]
        public void TestSelect()
        {
            string query = "SELECT * FROM [User]";
            IList<User> users = (IList<User>)DataAccessLayer.ExecuteReader<User>(CommandType.Text, query, null, FillerTestSelect);

            Assert.IsTrue(users.Count > 0);
        }

        public static void FillerTestSelect(User obj, IDataReader dr)
        {
            obj.Id = Convert.ToInt32(dr["Id"]);
            obj.Name = dr["Name"].ToString();
            obj.Active = Convert.ToBoolean(dr["Active"]);
        }

    }
}
