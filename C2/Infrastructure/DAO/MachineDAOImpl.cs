using C2.Domain.DTO.CreateDTO;
using C2.Domain.IDAO;
using C2.Infrastructure.Connexions;
using LaverieEntities.Entities;
using MySql.Data.MySqlClient;

namespace C2.Infrastructure.DAO
{
    public class MachineDAOImpl : IMachineDAO
    {
        private readonly MySqlConnection _connection;

        public MachineDAOImpl(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        public List<Machine> GetAllMachines()
        {
            List<Machine> machines = new List<Machine>();

            try
            {
                _connection.Open();
                string query = "SELECT IdMachine, MarqueMachine, EtatMachine, IDLaverie FROM Machine";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var machine = new Machine
                        {
                            IdMachine = reader.GetInt32("IdMachine"),
                            MarqueMachine = reader.GetString("MarqueMachine"),
                            EtatMachine = reader.GetString("EtatMachine"),
                            IDLaverie = reader.GetInt32("IDLaverie")
                        };
                        machines.Add(machine);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des machines: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return machines;
        }

        public Machine GetMachineById(int id)
        {
            Machine machine = null;

            try
            {
                _connection.Open();
                string query = "SELECT IdMachine, MarqueMachine, EtatMachine, IDLaverie FROM Machine WHERE IdMachine = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            machine = new Machine
                            {
                                IdMachine = reader.GetInt32("IdMachine"),
                                MarqueMachine = reader.GetString("MarqueMachine"),
                                EtatMachine = reader.GetString("EtatMachine"),
                                IDLaverie = reader.GetInt32("IDLaverie")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération de la machine: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return machine;
        }

        public void CreateMachine(CreateMachineDTO machine)
        {
            try
            {
                _connection.Open();
                string query = "INSERT INTO Machine (MarqueMachine, EtatMachine, IDLaverie) VALUES (@marque, @etat, @idLaverie)";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@marque", machine.MarqueMachine);
                    cmd.Parameters.AddWithValue("@etat", machine.EtatMachine);
                    cmd.Parameters.AddWithValue("@idLaverie", machine.IDLaverie);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création de la machine: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void UpdateMachine(CreateMachineDTO machine)
        {
            try
            {
                _connection.Open();
                string query = "UPDATE Machine SET MarqueMachine = @marque, EtatMachine = @etat, IDLaverie = @idLaverie WHERE IdMachine = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@marque", machine.MarqueMachine);
                    cmd.Parameters.AddWithValue("@etat", machine.EtatMachine);
                    cmd.Parameters.AddWithValue("@idLaverie", machine.IDLaverie);
                    cmd.Parameters.AddWithValue("@id", machine.IdMachine);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la mise à jour de la machine: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void DeleteMachine(int id)
        {
            try
            {
                _connection.Open();
                string query = "DELETE FROM Machine WHERE IdMachine = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression de la machine: {ex.Message}");
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
