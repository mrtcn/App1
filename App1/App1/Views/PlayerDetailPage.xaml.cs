using Xamarin.Forms;

using App1.ViewModels;
using Plugin.Media;
using DLToolkit.Forms.Controls;

namespace App1.Views
{
    public partial class PlayerDetailPage : ContentPage
    {
        public PlayerDetailPage(PlayerDetailViewModel viewModel)
        {
            InitializeComponent();
            FlowListView.Init();
            BindingContext = viewModel;
        }

        private async void Camera_Clicked(object sender, System.EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }
    }
}