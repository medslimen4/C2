using C2.Domain.DTO.CreateDTO;
using C2.Domain.IDAO;
using C2.Infrastructure.Connexions;
using LaverieEntities.Entities;
using MySql.Data.MySqlClient;

namespace C2.Infrastructure.DAO
{
    public class LaveriesDAOImpl : ILaveriesDAO
    {
        private readonly MySqlConnection _connection;

        public LaveriesDAOImpl(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        public List<Laveries> GetAllLaveries()
        {
            List<Laveries> laveries = new List<Laveries>();

            try
            {
                _connection.Open();
                string query = "SELECT IdLaverie, CapaciteLaverie, AddresseLaverie, ProprietaireCIN FROM Laveries";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var laverie = new Laveries
                        {
                            IdLaverie = reader.GetInt32("IdLaverie"),
                            CapaciteLaverie = reader.GetString("CapaciteLaverie"),
                            AddresseLaverie = reader.GetString("AddresseLaverie"),
                            ProprietaireCIN = reader.GetInt32("ProprietaireCIN")
                        };
                        laveries.Add(laverie);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des laveries: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return laveries;
        }

        public Laveries GetLaverieById(int id)
        {
            Laveries laverie = null;

            try
            {
                _connection.Open();
                string query = "SELECT IdLaverie, CapaciteLaverie, AddresseLaverie, ProprietaireCIN FROM Laveries WHERE IdLaverie = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            laverie = new Laveries
                            {
                                IdLaverie = reader.GetInt32("IdLaverie"),
                                CapaciteLaverie = reader.GetString("CapaciteLaverie"),
                                AddresseLaverie = reader.GetString("AddresseLaverie"),
                                ProprietaireCIN = reader.GetInt32("ProprietaireCIN")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération de la laverie: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return laverie;
        }

        public void CreateLaverie(CreateLaverieDTO laverie)
        {
            try
            {
                _connection.Open();
                string query = "INSERT INTO Laveries (CapaciteLaverie, AddresseLaverie, ProprietaireCIN) VALUES (@capacite, @adresse, @proprietaireCIN)";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@capacite", laverie.CapaciteLaverie);
                    cmd.Parameters.AddWithValue("@adresse", laverie.AddresseLaverie);
                    cmd.Parameters.AddWithValue("@proprietaireCIN", laverie.ProprietaireCIN);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création de la laverie: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void UpdateLaverie(CreateLaverieDTO laverie)
        {
            try
            {
                _connection.Open();
                string query = "UPDATE Laveries SET CapaciteLaverie = @capacite, AddresseLaverie = @adresse, ProprietaireCIN = @proprietaireCIN WHERE IdLaverie = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@capacite", laverie.CapaciteLaverie);
                    cmd.Parameters.AddWithValue("@adresse", laverie.AddresseLaverie);
                    cmd.Parameters.AddWithValue("@proprietaireCIN", laverie.ProprietaireCIN);
                    cmd.Parameters.AddWithValue("@id", laverie.IdLaverie);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la mise à jour de la laverie: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void DeleteLaverie(int id)
        {
            try
            {
                _connection.Open();
                string query = "DELETE FROM Laveries WHERE IdLaverie = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression de la laverie: {ex.Message}");
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
