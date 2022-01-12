using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace DapperDataAccessDemo
{
    public class DataAccess
    {
        public List<Person> GetPeople(string lastName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionValue("Sample")))
            {
                //return connection.Query<Person>($"SELECT * FROM People WHERE LastName = '{ lastName }'").ToList();
                return connection.Query<Person>("dbo.People_GetByLastName @LastName", new { LastName = lastName }).ToList();
            }
        }
    }
}