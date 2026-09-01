using Microsoft.Extensions.DependencyInjection;

namespace naidis_TARge25
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //loome esimese lehe (StartPage)

            var startPage = new StartPage();
            //Pakime selle NavgationPage sisse, et saaksime kasutada navigeerimist
            var navPage = new NavigationPage(startPage)
            {
                BarBackgroundColor = Colors.LightBlue,
                BarTextColor = Colors.White
            };

            return new Window(navPage);
        }
    }
}