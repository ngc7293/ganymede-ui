using Microsoft.UI.Xaml.Controls;
using Windows.Security.Credentials;

using Ganymede.Api.Auth0;

namespace Ganymede.UI.Dialogs
{
    public sealed partial class AuthDialog : ContentDialog
    {
        public AuthDialog()
        {
            InitializeComponent();
            LoadCredentials();
        }

        private void LoadCredentials()
        {
            var vault = new PasswordVault();

            try
            {
                var credentials = vault.FindAllByResource("Ganymede");

                if (credentials.Count > 0)
                {
                    var credential = vault.Retrieve("Ganymede", credentials[0].UserName);
                    UsernameInput.Text = credential.UserName;
                    PasswordInput.Password = credential.Password;
                    SaveCredsCheckbox.IsChecked = true;
                }
            }
            catch { }
        }

        private void SaveCredentials()
        {
            var vault = new PasswordVault();
            var credential = new PasswordCredential()
            {
                Resource = "Ganymede",
                UserName = UsernameInput.Text,
                Password = PasswordInput.Password,
            };

            vault.Add(credential);
        }

        private void ClearCredentials()
        {
            var vault = new PasswordVault();

            try {
                var credentials = vault.FindAllByResource("Ganymede");
                foreach (var credential in credentials) {
                    vault.Remove(credential);
                }
            } catch {}
        }

        private async void Login_Click(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var deferral = args.GetDeferral();
            var result = await (new AuthFacade()).LoginWithPassword(UsernameInput.Text, PasswordInput.Password);

            if (result.access_token != null)
            {
                MainWindow.ViewModel.AccessToken = result.access_token;

                if (SaveCredsCheckbox.IsChecked ?? false)
                {
                    SaveCredentials();
                } else {
                    ClearCredentials();
                }
            }
            else
            {
                ErrorText.Text = result.message;
                args.Cancel = true;
            }

            deferral.Complete();
        }
    }
}
