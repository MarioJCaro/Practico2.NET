using DataAccessLayer.IDALs;
using Microsoft.Data.SqlClient;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccessLayer.DALs
{
    public class DAL_PersonasADONET : IDAL_Personas
    {
        private string _connectionString = "Server=localhost,1433;Database=Practico2;User Id=sa;Password=Abc*123!;Encrypt=False;";

        private Dictionary<string, Persona> personas = new Dictionary<string, Persona>();

        public List<Persona> Get()
        {
            List<Persona> personas = new List<Persona>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Personas", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Persona persona = new Persona();

                            persona.Documento = reader["Documento"].ToString();
                            persona.Nombre = reader["Nombre"].ToString();

                            personas.Add(persona);
                        }
                    }
                }
            }

            return personas;
        }

        public Persona Get(string documento)
        {
            Persona persona = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Personas WHERE Documento = @Documento", connection))
                {
                    command.Parameters.AddWithValue("@Documento", documento);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new Persona();
                            persona.Documento = reader["Documento"].ToString();
                            persona.Nombre = reader["Nombre"].ToString();
                        }
                    }
                }
            }

            return persona;
        }

        public void Insert(Persona persona)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Personas (Documento, Nombre) VALUES (@Documento, @Nombre)", connection))
                {
                    command.Parameters.AddWithValue("@Documento", persona.Documento);
                    command.Parameters.AddWithValue("@Nombre", persona.Nombre);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Persona persona)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Personas SET Nombre = @Nombre WHERE Documento = @Documento", connection))
                {
                    command.Parameters.AddWithValue("@Documento", persona.Documento);
                    command.Parameters.AddWithValue("@Nombre", persona.Nombre);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string documento)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Personas WHERE Documento = @Documento", connection))
                {
                    command.Parameters.AddWithValue("@Documento", documento);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
