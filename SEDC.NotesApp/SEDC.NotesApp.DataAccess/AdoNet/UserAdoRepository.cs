using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SEDC.NotesApp.DataAccess.AdoNet
{
    public class UserAdoRepository : IRepository<User>
    {
        private string _connectionString;
        public UserAdoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(User entity)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $@"INSERT INTO dbo.Users(FirstName, LastName, Username) 
                                             VALUES(@firstNAme, @lastName, @username)";
            
            command.Parameters.AddWithValue("@firstName", entity.FirstName);
            command.Parameters.AddWithValue("@lastName", entity.LastName);
            command.Parameters.AddWithValue("@userName", entity.Username);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = $"DELETE FROM Users WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public List<User> GetAll()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM dbo.Users";

            List<User> userDb = new List<User>();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                userDb.Add(new User
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Username = (string)reader["UserName"],
                });
            }

            connection.Close();
            return userDb;
        }

        public User GetById(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM dbo.Users WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();

            List<User> userDb = new List<User>();

            while (reader.Read())
            {
                userDb.Add(new User
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Username = (string)reader["UserName"],
                });
            }

            connection.Close();
            return userDb.FirstOrDefault();
        }

        public void Update(User entity)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = $@"UPDATE dbo.Users set FirstName = @firstName, LastName = @lastName, Username = @userName,
                                              WHERE Id = @id";
            command.Parameters.AddWithValue("@firstName", entity.FirstName);
            command.Parameters.AddWithValue("@lastName", entity.LastName);
            command.Parameters.AddWithValue("@userName", entity.Username);
            command.Parameters.AddWithValue("@id", entity.Id);

            command.ExecuteNonQuery();

            connection.Close();

        }
    }
}
