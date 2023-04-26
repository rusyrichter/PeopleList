using System;
using System.Data.SqlClient;

namespace PeopleData
{
    public class PeopleRepo
    {
        private string _connectionString;

        public PeopleRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            var list = new List<Person>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]

                });
            }

            return list;
        }

        public void Add(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                "VALUES (@first, @last, @age)";
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM People  " +
                "WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
          
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public void EditPerson(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
           
            cmd.CommandText = @"Update People SET FirstName = @first, LastName = @last, Age = @age where id = @id";
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.Parameters.AddWithValue("@id", person.Id);

            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public Person GetSpecificPerson(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People WHERE Id = @id";
            connection.Open();
            cmd.Parameters.AddWithValue("@id", id);

            Person person = new();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               person = new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]

                };
            }

            return person;
        }
    }
}
