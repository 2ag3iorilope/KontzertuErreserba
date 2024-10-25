using Microsoft.Maui.Controls;

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
            statusLabel.Text = "Conectando...";

            // Llama al método y muestra el resultado en el Label
            string resultado = await _databaseService.ConectarBaseDatosAsync();
            statusLabel.Text = resultado;
        }
    }
}
