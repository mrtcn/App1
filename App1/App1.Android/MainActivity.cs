using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Facebook;
using Android.Content;
using Java.Security;
using Android.Util;
using static Android.Content.PM.PackageManager;
using Java.Lang;
using DLToolkit.Forms.Controls;
using FFImageLoading.Forms.Platform;
using App1.Droid.Services;

namespace App1.Droid
{
    [Activity(Label = "App1", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static ICallbackManager CallbackManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // Create callback manager using CallbackManagerFactory
            CallbackManager = CallbackManagerFactory.Create();
            CachedImageRenderer.Init(enableFastRenderer: true);

            base.OnCreate(savedInstanceState);
            CachedImageRenderer.Init(enableFastRenderer: true);

            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            //LoadApplication(new App(Services.SharedInstance));

            try
            {

                PackageInfo info = PackageManager.GetPackageInfo(
                            "com.companyname.App1",
                            PackageInfoFlags.Signatures);
                foreach (var signature in info.Signatures)
                {
                    MessageDigest md = MessageDigest.GetInstance("SHA");
                    md.Update(signature.ToByteArray());
                    Log.Debug("KeyHash:", Base64.EncodeToString(md.Digest(), Base64.Default));
                    var s = Base64.EncodeToString(md.Digest(), Base64.Default);
                }
            }
            catch (NameNotFoundException e)
            {

            }
            catch (NoSuchAlgorithmException e)
            {

            }


            LoadApplication(new App());
    }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(
            int requestCode,
            Result resultCode,
            Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            MultiMediaPickerService.SharedInstance.OnActivityResult(requestCode, resultCode, data);
            CallbackManager.OnActivityResult(requestCode, Convert.ToInt32(resultCode), data);
        }
    }
}