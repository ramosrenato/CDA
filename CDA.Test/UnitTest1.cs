using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CDA.DAL;
using CDA.DAL.MSSQL;

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

            DataParameter param1 = new DataParameter
            {
                {"p_id", 1 }
            };

            //UPDATE
            string query1 = "UPDATE [User] SET Name = 'TestUser' WHERE Id = @p_id";
            DataAccessLayer.ExecuteNonQuery(CommandType.Text, query1, param1);


            // VERIFY
            User user = null;
            DataParameter param2 = new DataParameter
            {
                {"p_id", 1 }
            };

            string query2 = "SELECT * FROM [User] WHERE Id = @p_id";
            using (IDataReader dr = DataAccessLayer.OpenDataReader(CommandType.Text, query2, param2))
            {
               user = DataMapper.ToInstance<User>(dr);
            }

            Assert.IsTrue(user.Name.Equals("TestUser"));
        }

        
    }
}
