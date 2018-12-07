using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using test2.Filters;

namespace test2.Controllers
{
    public class Test2Controller : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return GetValue(id);
        }

        // POST api/values
        [HttpGet]
        [EnlistToDistributedTransactionActionFilter]
        public int Add([FromUri]string orderName,int payment)
        {
            return AddData(orderName, payment);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        private int AddData(string orderName, int payment)
        {
            string connectionString = "Data Source=localhost;Initial Catalog=test2;Persist Security Info=True;User ID=sa;Password=Password01!";
            int n = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string insertSql = String.Format("insert into tbl_test2(OrderName,Payment) values('{0}','{1}')", orderName, payment);
                SqlCommand cmd = new SqlCommand(insertSql, conn);
                n = cmd.ExecuteNonQuery();
            }

            return n;

        }

        private string GetValue(int id)
        {
            string connectionString = "Data Source=localhost;Initial Catalog=test2;Persist Security Info=True;User ID=sa;Password=Password01!";

            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT OrderName, Payment from tbl_test2 "
                    + "WHERE id = @id ";
            string value = "";

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        value = String.Format("OrderName - {0},Payment - {1}", reader[0].ToString(), reader[1].ToString());
                    }
                    else
                    {
                        value = "not found";
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    //           
                }
            }

            return value;
        }
    }
}
