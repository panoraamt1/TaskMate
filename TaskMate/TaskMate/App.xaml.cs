using TaskMate.Views;
using System.Globalization;

namespace TaskMate
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var culture = new CultureInfo("et-EE");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            MainPage = new NavigationPage(new HomePage());
        }
    }

}