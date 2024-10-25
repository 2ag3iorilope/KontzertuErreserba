using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KontzertuErreserba
{
    internal class DatabaseService
    {
        // Especifica el puerto 3306 en la cadena de conexión
        private readonly string connectionString = "Server=localhost;Port=3306;Database=kontzertuerreserba;User Id=root;Password=mysql;";

        public async Task<string> ConectarBaseDatosAsync()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    // Extrae información de la conexión
                    string database = connection.Database;
                    string server = connection.DataSource;

                    return $"Konektatuta ondorengo datubasera: '{database}' ondorengo zerbitzarian '{server}'";
                }
                catch (Exception ex)
                {
                    return $"Errorea konektatzean: {ex.Message}";
                }
            }
        }
    }
}
