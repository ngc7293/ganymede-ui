using Microsoft.UI.Xaml.Controls;

using Ganymede.Api.Auth0;

namespace Ganymede.UI.Dialogs
{
    public sealed partial class AuthDialog : ContentDialog
    {
        public AuthDialog()
        {
            InitializeComponent();
        }

        private async void Login_Click(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var deferral = args.GetDeferral();    

            var result = await (new AuthFacade()).LoginWithPassword(UsernameInput.Text, PasswordInput.Password);

            if (result.access_token != null) {
                MainWindow.ViewModel.AccessToken = result.access_token;
            } else {
                ErrorText.Text = result.message;
                args.Cancel = true;
            }

            deferral.Complete();
        }
    }
}
