using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using CoffeeShop.Models;
using System.Data.SqlClient;
using CoffeeShopApi.Interfaces;

namespace CoffeeShop.Repositories
{
    // this represents a repository for working with Coffee records in the database.
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly string _connectionString;

        // constructs a new instance of the CoffeeRepository class with the specified configuration.
        public CoffeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }

        // Retrieves a list of all coffee records from the database, including their ID, title, and bean variety ID. Returns a list<Coffee> object containing the retrieved data.
        public List<Coffee> GetAllCoffee()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Title, BeanVarietyId FROM Coffee;";
                    var reader = cmd.ExecuteReader();
                    var CoffeeList = new List<Coffee>();
                    while (reader.Read())
                    {
                        var thisCoffee = new Coffee()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            BeanVarietyId = reader.GetInt32(reader.GetOrdinal("BeanVarietyId")),
                        };
                        if (!reader.IsDBNull(reader.GetOrdinal("Title")))
                        {
                            thisCoffee.Title = reader.GetString(reader.GetOrdinal("Title"));
                        }
                        CoffeeList.Add(thisCoffee);
                    }

                    reader.Close();

                    return CoffeeList;
                }
            }
        }
        // returns a specific coffee record from the database based on its integer id, returns a Coffee object.
        public Coffee GetCoffeeById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Title, BeanVarietyId FROM Coffee
                         WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    Coffee thisCoffee = null;

                    if (reader.Read())
                    {
                        thisCoffee = new Coffee()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            BeanVarietyId = reader.GetInt32(reader.GetOrdinal("BeanVarietyId"))
                        };
                        if (!reader.IsDBNull(reader.GetOrdinal("Title")))
                        {
                            thisCoffee.Title = reader.GetString(reader.GetOrdinal("Title"));
                        };


                    }

                    return thisCoffee;
                }

            }
        }

        // adds a Coffee object to the sql database
        public void AddCoffee(string title, int beanVarietyId)
        {
            Coffee thisNewCoffee = null;
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Coffee(Title, BeanVarietyId)
                                       VALUES (@title, @beanVarietyId);";
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@beanVarietyId", beanVarietyId);

                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        cmd.CommandText = "SELECT * FROM Coffee WHERE Id = SCOPE_IDENTITY();";
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                thisNewCoffee = new Coffee
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Title = reader.GetString(reader.GetOrdinal("Title")),
                                    BeanVarietyId = reader.GetInt32(reader.GetOrdinal("BeanVarietyId"))
                                };
                            }
                        }
                    }
                }
            }
        }

        // updates an existing Coffee record in the database
        public void UpdateCoffee(int id, Coffee coffee)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Coffee (Title, BeanVarietyId) VALUES (@title, @beanVarietyId)";
                    cmd.Parameters.AddWithValue("@title", coffee.Title);
                    cmd.Parameters.AddWithValue("@beanVarietyId", coffee.BeanVarietyId);
                    int affectedRows = cmd.ExecuteNonQuery();
                }

            }
        }

        // deletes a Coffee record from the database.
        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Coffee WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    int affectedRows = cmd.ExecuteNonQuery();
                }
            }
        }
    }
}