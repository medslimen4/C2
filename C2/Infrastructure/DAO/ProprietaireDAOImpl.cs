using C2.Domain.DTO.CreateDTO;
using C2.Domain.IDAO;
using C2.Infrastructure.Connexions;
using LaverieEntities.Entities;
using MySql.Data.MySqlClient;

namespace C2.Infrastructure.DAO
{
    public class ProprietaireDAOImpl : IProprietaireDAO
    {
        private readonly MySqlConnection _connection;

        public ProprietaireDAOImpl(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        public List<CreateProprietaireDTO> GetAllProprietaires()
        {
            List<CreateProprietaireDTO> proprietaires = new List<CreateProprietaireDTO>();

            try
            {
                _connection.Open();
                string query = "SELECT _CIN, _Surname FROM Proprietaire";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CreateProprietaireDTO = new CreateProprietaireDTO
                        {
                            _CIN = reader.GetInt32("_CIN"),
                            _Surname = reader.GetString("_Surname")
                        };
                        proprietaires.Add(CreateProprietaireDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des propriétaires: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return proprietaires;
        }

        public Proprietaire GetProprietaireById(int cin)
        {
            Proprietaire proprietaire = null;

            try
            {
                _connection.Open();
                string query = "SELECT _CIN, _Surname FROM Proprietaire WHERE _CIN = @cin";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@cin", cin);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            proprietaire = new Proprietaire
                            {
                                _CIN = reader.GetInt32("_CIN"),
                                _Surname = reader.GetString("_Surname")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération du propriétaire: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return proprietaire;
        }

        public void CreateProprietaire(CreateProprietaireDTO proprietaire)
        {
            try
            {
                _connection.Open();
                string query = "INSERT INTO Proprietaire (_CIN, _Surname) VALUES (@cin, @surname)";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@cin", proprietaire._CIN);
                    cmd.Parameters.AddWithValue("@surname", proprietaire._Surname);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création du propriétaire: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void UpdateProprietaire(Proprietaire proprietaire)
        {
            try
            {
                _connection.Open();
                string query = "UPDATE Proprietaire SET _Surname = @surname WHERE _CIN = @cin";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@surname", proprietaire._Surname);
                    cmd.Parameters.AddWithValue("@cin", proprietaire._CIN);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la mise à jour du propriétaire: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void DeleteProprietaire(int cin)
        {
            try
            {
                _connection.Open();
                string query = "DELETE FROM Proprietaire WHERE _CIN = @cin";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@cin", cin);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression du propriétaire: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
    }
}
