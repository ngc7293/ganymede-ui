using System;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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

            ((FrameworkElement)(Content)).Loaded += (ss, ee) => PromptAuth();

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
                ContentFrame.Navigate(typeof(Pages.ConfigListPage));
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
