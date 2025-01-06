using C2.Domain.DTO.CreateDTO;
using C2.Domain.IDAO;
using C2.Infrastructure.Connexions;
using LaverieEntities.Entities;
using MySql.Data.MySqlClient;

namespace C2.Infrastructure.DAO
{
    public class CycleDAOImpl : ICycleDAO
    {
        private readonly MySqlConnection _connection;

        public CycleDAOImpl(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        public List<Cycle> GetAllCycles()
        {
            List<Cycle> cycles = new List<Cycle>();

            try
            {
                _connection.Open();
                string query = "SELECT IdCycle, NomCycle, DureeCycleHR, coutCycle, IdMachine FROM Cycle";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cycle = new Cycle
                        {
                            IdCycle = reader.GetInt32("IdCycle"),
                            NomCycle = reader.GetString("NomCycle"),
                            DureeCycleHR = reader.GetInt32("DureeCycleHR"),
                            coutCycle = reader.GetDouble("coutCycle"),
                            IdMachine = reader.GetInt32("IdMachine")
                        };
                        cycles.Add(cycle);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des cycles: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return cycles;
        }

        public Cycle GetCycleById(int id)
        {
            Cycle cycle = null;

            try
            {
                _connection.Open();
                string query = "SELECT IdCycle, NomCycle, DureeCycleHR, coutCycle, IdMachine FROM Cycle WHERE IdCycle = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cycle = new Cycle
                            {
                                IdCycle = reader.GetInt32("IdCycle"),
                                NomCycle = reader.GetString("NomCycle"),
                                DureeCycleHR = reader.GetInt32("DureeCycleHR"),
                                coutCycle = reader.GetDouble("coutCycle"),
                                IdMachine = reader.GetInt32("IdMachine")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération du cycle: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return cycle;
        }

        public void CreateCycle(CreateCycleDTO cycle)
        {
            try
            {
                _connection.Open();
                string query = "INSERT INTO Cycle (NomCycle, DureeCycleHR, coutCycle, IdMachine) VALUES (@nomCycle, @dureeCycle, @coutCycle, @idMachine)";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@nomCycle", cycle.NomCycle);
                    cmd.Parameters.AddWithValue("@dureeCycle", cycle.DureeCycleHR);
                    cmd.Parameters.AddWithValue("@coutCycle", cycle.coutCycle);
                    cmd.Parameters.AddWithValue("@idMachine", cycle.IdMachine);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création du cycle: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void UpdateCycle(Cycle cycle)
        {
            try
            {
                _connection.Open();
                string query = "UPDATE Cycle SET NomCycle = @nomCycle, DureeCycleHR = @dureeCycle, coutCycle = @coutCycle, IdMachine = @idMachine WHERE IdCycle = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@nomCycle", cycle.NomCycle);
                    cmd.Parameters.AddWithValue("@dureeCycle", cycle.DureeCycleHR);
                    cmd.Parameters.AddWithValue("@coutCycle", cycle.coutCycle);
                    cmd.Parameters.AddWithValue("@idMachine", cycle.IdMachine);
                    cmd.Parameters.AddWithValue("@id", cycle.IdCycle);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la mise à jour du cycle: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void DeleteCycle(int id)
        {
            try
            {
                _connection.Open();
                string query = "DELETE FROM Cycle WHERE IdCycle = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression du cycle: {ex.Message}");
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
