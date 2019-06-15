using App1.Interfaces;
using App1.Models;
using App1.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.Views
{
    public class LoginViewModel
    {
        public ICommand OnFacebookLoginSuccessCmd { get; }
        public ICommand OnFacebookLoginErrorCmd { get; }
        public ICommand OnFacebookLoginCancelCmd { get; }

        public LoginViewModel()
        {
            OnFacebookLoginSuccessCmd = new Command<string>(
                async (authToken) => {

                    var httpHandler = DependencyService.Get<IHttpHandler>();
                    var identityUrl = "https://app1.identity.upope.com";
                    var facebookAuthViewModel = new FacebookAuthViewModel() { AccessToken = authToken };
                    var jsonBody = JsonConvert.SerializeObject(facebookAuthViewModel);
                    var tokenModel = httpHandler.AuthPostAsync<TokenModel>(string.Empty, identityUrl, "api/account/anon/facebook", jsonBody);

                    //var httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    //var response = await httpClient.PostAsync("https://app1.identity.upope.com/api/account/anon/facebook", httpContent);

                    DisplayAlert("Success", $"Authentication succeed: { authToken }");
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
