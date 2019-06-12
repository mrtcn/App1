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
                    var httpClient = new HttpClient();

                    var facebookAuthViewModel = new FacebookAuthViewModel() { AccessToken = authToken};
                    var jsonBody = JsonConvert.SerializeObject(facebookAuthViewModel);
                    var httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("https://app1.identity.upope.com/api/account/anon/facebook", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenModelJson = await response.Content.ReadAsStringAsync();
                        var tokenModel = JsonConvert.DeserializeObject<TokenModel>(tokenModelJson);
                    }

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
