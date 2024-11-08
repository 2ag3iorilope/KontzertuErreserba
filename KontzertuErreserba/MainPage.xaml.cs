using Microsoft.Data.SqlClient;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;

namespace KontzertuErreserba
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService = new DatabaseService();

        public MainPage()
        {
            InitializeComponent();
            //SetWindowSize();
        }

        private void OnEntrydniTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender == DNIEntry)
            {
                ValidateDNI(DNIEntry.Text);
            }
        }
        void SetWindowSize()
        {
            var mainWindow = Application.Current?.MainPage as Page;
            mainWindow.WidthRequest = 800;
            mainWindow.HeightRequest = 600;
        }

        private async void OnConnectButtonClicked(object sender, EventArgs e)
        {
            statusLabel.Text = "Konektatzen...";


            string resultado = await _databaseService.ConectarBaseDatosAsync();
            statusLabel.Text = resultado;
        }


        private async void OnCitySelected(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var selectedCity = (sender as RadioButton)?.Content.ToString();
                int reservas = await _databaseService.GetReservasCountAsync(selectedCity);
                reservasLabel.Text = $"Erreserbak: {reservas}";
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {

            var entry = sender as Entry;
            if (entry == IzenaEntry || entry == AbizenaEntry)
            {
                if (!string.IsNullOrEmpty(entry.Text) && !IsTextAlphabetic(entry.Text))
                {

                    entry.Text = e.OldTextValue;
                }

            }
            if (entry == KantitateaEntry)
            {
                if (!string.IsNullOrEmpty(entry.Text) && !IsTextNumeric(entry.Text))
                {

                    entry.Text = e.OldTextValue;
                }

            }


            bool isFormFilled = !string.IsNullOrWhiteSpace(IzenaEntry.Text) &&
                        !string.IsNullOrWhiteSpace(AbizenaEntry.Text) &&
                        !string.IsNullOrWhiteSpace(DNIEntry.Text) &&
                        !string.IsNullOrWhiteSpace(KantitateaEntry.Text);


            ErreserbaBotoia.IsEnabled = isFormFilled;
        }

        private bool IsTextAlphabetic(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsLetter(c))
                    return false;
            }
            return true;
        }

        private bool IsTextNumeric(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private void OnIrtenButtonClicked(object sender, EventArgs e)
        {
            Application.Current!.Quit();
        }
        private int GetCityId()
        {
            if (MadridRadioButton.IsChecked) return 1;
            if (BarcelonaRadioButton.IsChecked) return 2;
            if (BilbaoRadioButton.IsChecked) return 3;
            return 0; // Default in case no city is selected
        }

        private void ValidateDNI(string dni)
        {
            if (dni.Length < 9)
            {
                // El DNI debe tener 8 números y 1 letra, en total 9 caracteres
                DNIEntry.TextColor = Colors.Red;
                statusLabel.Text = "DNI ez da zuzena";
                return;
            }

            string dniNumberPart = dni.Substring(0, 8);
            char dniLetter = dni[8];

            if (int.TryParse(dniNumberPart, out _) && char.IsLetter(dniLetter))
            {
                // Verifica que la letra coincida con la calculada para el número
                string letters = "TRWAGMYFPDXBNJZSQVHLCKE";
                int index = int.Parse(dniNumberPart) % 23;
                char correctLetter = letters[index];

                if (char.ToUpper(dniLetter) == correctLetter)
                {
                    // DNI válido
                    DNIEntry.TextColor = Colors.Green;
                    statusLabel.Text = "DNI zuzena";
                }
                else
                {
                    // DNI inválido
                    DNIEntry.TextColor = Colors.Red;
                    statusLabel.Text = "DNI ez da zuzena";
                }
            }
            else
            {
                DNIEntry.TextColor = Colors.Red;
                statusLabel.Text = "DNI ez da zuzena";
            }
        }


        private async void OnErreserbaButtonClicked(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=3306;Database=kontzertuerreserba;User Id=root;Password=mysql;";
            int cityId = GetCityId();  // Esta función obtiene el ID del concierto según la ciudad seleccionada
            string izena = IzenaEntry.Text;
            string abizena = AbizenaEntry.Text;
            string dni = DNIEntry.Text;
            int kantitatea = int.Parse(KantitateaEntry.Text);

            // Primero obtenemos el último idErreserbak
            string getLastIdQuery = "SELECT MAX(idErreserbak) FROM erreserbak";

            try
            {
                int newId = 1;  // Si no hay registros, comenzamos desde 1
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Obtener el último idErreserbak
                    using (MySqlCommand getIdCommand = new MySqlCommand(getLastIdQuery, connection))
                    {
                        var result = await getIdCommand.ExecuteScalarAsync();
                        if (result != DBNull.Value)
                        {
                            newId = Convert.ToInt32(result) + 1;
                        }
                    }

                    // Ahora realizamos la inserción con el nuevo idErreserbak
                    string query = "INSERT INTO erreserbak (idErreserbak, DNI, Abizena, Izena, Kantitatea, Kontzertuak_idKontzertua) " +
                                   "VALUES (@idErreserbak, @dni, @abizena, @izena, @kantitatea, @concierto_id)";

                    using (MySqlCommand insertCommand = new MySqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idErreserbak", newId);
                        insertCommand.Parameters.AddWithValue("@dni", dni);
                        insertCommand.Parameters.AddWithValue("@abizena", abizena);
                        insertCommand.Parameters.AddWithValue("@izena", izena);
                        insertCommand.Parameters.AddWithValue("@kantitatea", kantitatea);
                        insertCommand.Parameters.AddWithValue("@concierto_id", cityId);

                        await insertCommand.ExecuteNonQueryAsync();
                        reservasLabel.Text = "Erreserba eginda!";  // Confirmación de reserva
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Errorea", $"Erreserba ezin izan da burutu: {ex.Message}", "Ados");
            }



        }


    }
}

