using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Ganymede.UI.Pages
{
    public sealed partial class DevicePage : Page
    {
        internal ViewModels.DeviceViewModel ViewModel = new ViewModels.DeviceViewModel();

        public DevicePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var uid = (string)e.Parameter;

            foreach (var device in MainWindow.ViewModel.Devices)
            {
                if (device.Uid == uid)
                {
                    ViewModel = device;
                    break;
                }
            }
        }
    }
}
