using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Windows.Forms;

namespace DapperDataAccessDemo
{
    public class DataAccess
    {
        public List<Person> GetPeople(string lastName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionValue("Sample")))
            {
                //return connection.Query<Person>($"SELECT * FROM People WHERE LastName = '{ lastName }'").ToList();
                var output = connection.Query<Person>("dbo.People_GetByLastName @LastName", new { LastName = lastName }).ToList();

                if(output.Count == 0)
                {
                    MessageBox.Show($"Last name: '{lastName}' was not found.");
                }

                return output;
            }
        }

        public void InsertPerson(string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionValue("Sample")))
            {
                //Person newPerson = new Person
                //{
                //    FirstName = firstName,
                //    LastName = lastName,
                //    EmailAddress = emailAddress,
                //    PhoneNumber = phoneNumber
                //};

                List<Person> people = new List<Person>();

                people.Add(new Person { FirstName = firstName, LastName = lastName, EmailAddress = emailAddress, PhoneNumber = phoneNumber });

                connection.Execute("dbo.People_Insert @FirstName, @LastName, @EmailAddress, @PhoneNumber", people);
            }
        }

        public void DeletePerson(string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnectionValue("Sample")))
            {
                connection.Execute($"dbo.People_Remove @FirstName = {firstName}, @LastName = {lastName}, @EmailAddress = {emailAddress}, @PhoneNumber = {phoneNumber}");
            }
        }
    }
}