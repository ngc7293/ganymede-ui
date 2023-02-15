using Grpc.Net.Client;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using Ganymede.Services.Device;

namespace Ganymede.UI.Pages
{
    public sealed partial class ConfigListPage : Page
    {
        internal ViewModels.AppViewModel ViewModel => MainWindow.ViewModel;

        public ConfigListPage()
        {
            InitializeComponent();
        }

        private void ConfigList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConfigList.SelectedIndex >= 0)
            {
                var Config = (ViewModels.ConfigViewModel) ConfigList.SelectedItem;
                Frame.Navigate(typeof(ConfigPage), Config.Uid);
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

            var reply = await client.ListConfigAsync(new ListConfigRequest(), headers);

            ViewModel.Configs.Clear();
            foreach (var Config in reply.Configs) { ViewModel.Configs.Add(ViewModels.ConfigViewModel.FromProtobuf(Config)); }
        }
    }
}
