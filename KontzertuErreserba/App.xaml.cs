namespace KontzertuErreserba
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

        }
        /// <summary>
        /// Metodo honek CreateWindow metodoa gaindegiten du, aplikazioaren leihoaren tamaina egokitzeko.
        /// </summary>
        /// <param name="activationState">Aplikazioaren aktibazio egoera.</param>
        /// <returns>Leihoa itzultzen du tamaina pertsonalizatua duela(Gure tamaina).</returns>
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 571;
            const int newHeight = 566;

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }
    }
}
