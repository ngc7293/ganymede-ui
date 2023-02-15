using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Ganymede.UI.Pages
{
    public sealed partial class ConfigPage : Page
    {
        internal ViewModels.ConfigViewModel ViewModel = new ViewModels.ConfigViewModel();

        public ConfigPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var uid = (string)e.Parameter;

            foreach (var Config in MainWindow.ViewModel.Configs)
            {
                if (Config.Uid == uid)
                {
                    ViewModel = Config;
                    break;
                }
            }
        }
    }
}
