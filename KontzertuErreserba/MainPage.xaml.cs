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


    }
}
