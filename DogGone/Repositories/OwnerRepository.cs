using DogGone.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGone.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IConfiguration _config;

        public OwnerRepository(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }

        }
        public List<Owner> GetAllOwners()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT Owner.Id, Owner.Email, Owner.Name, 
                                        Owner.Address, Owner.NeighborhoodId, Owner.Phone, 
                                        Neighborhood.Name as Neighborhood
                                        FROM Owner
                                        JOIN Neighborhood on Neighborhood.Id = NeighborhoodId
                                       ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Owner> owners = new List<Owner>();
                        while (reader.Read())
                        {
                            Owner owner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Neighborhood = reader.GetString(reader.GetOrdinal("Neighborhood"))
                            };
                            owners.Add(owner);

                        }
                        return owners;
                    }
                }

            }
        }
        public Owner GetOWnerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT Owner.Id, Owner.Email, Owner.Name, 
                                        Owner.Address, Owner.NeighborhoodId, Owner.Phone, 
                                        Neighborhood.Name as Neighborhood
                                        FROM Owner
                                        JOIN Neighborhood on Neighborhood.Id = NeighborhoodId
                                        WHERE Owner.Id = @id
                                       ";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            Owner owner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Neighborhood = reader.GetString(reader.GetOrdinal("Neighborhood"))
                            };

                            return owner;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }
}

