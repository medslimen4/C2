using MySql.Data.MySqlClient;
namespace C2.Infrastructure.Connexions
{


  
        public class ConnectionDB
        {
            private static ConnectionDB _instance;
            private static MySqlConnection _con;



            private ConnectionDB(string connectionString)
            {
                _con = new MySqlConnection(connectionString);
            }

            public static ConnectionDB GetInstance(string connectionString)
            {
                if (_instance == null)
                {
                    _instance = new ConnectionDB(connectionString);
                }
                return _instance;

            }
            public MySqlConnection GetConnection()
            {
                return _con;
            }
        }
    

}
