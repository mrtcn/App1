using App1.Interfaces;
using App1.Models;
using App1.ViewModels;
using Newtonsoft.Json;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.Views
{
    public class LoginViewModel
    {
        public ICommand OnFacebookLoginSuccessCmd { get; internal set; }
        public ICommand OnFacebookLoginErrorCmd { get; internal set; }
        public ICommand OnFacebookLoginCancelCmd { get; internal set; }

        public LoginViewModel()
        {
            OnFacebookLoginSuccessCmd = new Command<string>(
            async (authToken) =>
            {

                var httpHandler = DependencyService.Get<IHttpHandler>();
                var identityUrl = AppSettingsManager.Settings["identityUrl"];
                var facebookAuthViewModel = new FacebookAuthViewModel() { AccessToken = authToken };
                var jsonBody = JsonConvert.SerializeObject(facebookAuthViewModel);
                var tokenModel = await httpHandler.AuthPostAsync<TokenModel>(string.Empty, identityUrl, AppSettingsManager.Settings["facebookLogin"], jsonBody);

                if (!Application.Current.Properties.ContainsKey("AccessToken"))
                    Application.Current.Properties.Add("AccessToken", tokenModel.AccessToken);

                if (!Application.Current.Properties.ContainsKey("RefreshToken"))
                    Application.Current.Properties.Add("RefreshToken", tokenModel.RefreshToken);

                Application.Current.MainPage = new WizardPage();

            });

            OnFacebookLoginErrorCmd = new Command<string>(
                (err) => DisplayAlert("Error", $"Authentication failed: { err }"));

            OnFacebookLoginCancelCmd = new Command(
                () => DisplayAlert("Cancel", "Authentication cancelled by the user."));
        }

        void DisplayAlert(string title, string msg) =>
            (Application.Current as App).MainPage.DisplayAlert(title, msg, "OK");
    }
}
