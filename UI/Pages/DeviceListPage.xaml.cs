using Grpc.Net.Client;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using Ganymede.Services.Device;

namespace Ganymede.UI.Pages
{
    public sealed partial class DeviceListPage : Page
    {
        internal ViewModels.AppViewModel ViewModel => MainWindow.ViewModel;

        public DeviceListPage()
        {
            InitializeComponent();
        }

        private void DeviceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DeviceList.SelectedIndex >= 0)
            {
                var device = (ViewModels.DeviceViewModel)DeviceList.SelectedItem;
                Frame.Navigate(typeof(DevicePage), device.Uid);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Refresh();
        }

        private async void Refresh()
        {
            if (ViewModel.AccessToken.Length == 0)
            {
                return;
            }

            var address = System.Environment.GetEnvironmentVariable("GANYMEDE_ADDRESS") ?? "https://ganymede.davidbourgault.ca:443";
            var channel = GrpcChannel.ForAddress(address);
            var client = new DeviceService.DeviceServiceClient(channel);

            var headers = new Grpc.Core.Metadata
            {
                { "authorization", ViewModel.AccessToken }
            };

            var reply = await client.ListDeviceAsync(new ListDeviceRequest(), headers);

            ViewModel.Devices.Clear();
            foreach (var device in reply.Devices) { ViewModel.Devices.Add(ViewModels.DeviceViewModel.FromProtobuf(device)); }
        }
    }
}
