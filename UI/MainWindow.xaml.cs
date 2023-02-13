using System;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Windows.Security.Credentials;

namespace Ganymede.UI
{
    public sealed partial class MainWindow : MicaWindow
    {
        internal static ViewModels.AppViewModel ViewModel { get; set; } = new ViewModels.AppViewModel();

        public MainWindow()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            var vault = new PasswordVault();

            try
            {
                var creds = vault.Retrieve("ganymede", "accesstoken");
                ViewModel.AccessToken = creds.Password;
                System.Diagnostics.Debug.WriteLine(String.Format("Token: {0}", ViewModel.AccessToken));
            }
            catch (Exception)
            {
                if (Content is FrameworkElement fe)
                {
                    fe.Loaded += (ss, ee) => PromptAuth();
                }
            }

        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)sender.SelectedItem;
            if ((string)item.Tag == "devices")
            {
                ContentFrame.Navigate(typeof(Pages.DeviceListPage));
            }
            else if ((string)item.Tag == "configs")
            {
                ContentFrame.Navigate(typeof(Pages.DeviceListPage));
            }
        }

        private async void PromptAuth()
        {
            var auth = new Dialogs.AuthDialog()
            {
                XamlRoot = Root.XamlRoot
            };

            var result = await auth.ShowAsync();
            ContentFrame.Navigate(typeof(Pages.DeviceListPage));
        }
    }
}
