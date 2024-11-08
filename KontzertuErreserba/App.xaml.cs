namespace KontzertuErreserba
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            //MainPage.HeightRequest = 500;
            //MainPage.WidthRequest = 500;
        }
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
