﻿using Android.Content;
using App1.Droid.Views;
using App1.Views;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginButtonRenderer))]
namespace App1.Droid.Views
{
    public class FacebookLoginButtonRenderer : ViewRenderer
    {
        Context ctx;
        bool disposed;
        public FacebookLoginButtonRenderer(Context ctx) : base(ctx)
        {
            this.ctx = ctx;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            if (Control == null)
            {
                var fbLoginBtnView = e.NewElement as FacebookLoginButton;
                var fbLoginbBtnCtrl = new Xamarin.Facebook.Login.Widget.LoginButton(ctx)
                {
                    LoginBehavior = LoginBehavior.NativeOnly
                };

                fbLoginbBtnCtrl.SetReadPermissions(fbLoginBtnView.Permissions);
                fbLoginbBtnCtrl.RegisterCallback(MainActivity.CallbackManager, new MyFacebookCallback(this.Element as FacebookLoginButton));
                
                SetNativeControl(fbLoginbBtnCtrl);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                if (this.Control != null)
                {
                    (this.Control as Xamarin.Facebook.Login.Widget.LoginButton).UnregisterCallback(MainActivity.CallbackManager);
                    base.Dispose(disposing);
                    this.Control.Dispose();
                }
                this.disposed = true;
            }
            
        }

        class MyFacebookCallback : Java.Lang.Object, IFacebookCallback
        {
            FacebookLoginButton view;

            public MyFacebookCallback(FacebookLoginButton view)
            {
                this.view = view;
            }
            
            public void OnCancel() =>
                view.OnCancel?.Execute(null);

            public void OnError(FacebookException fbException) =>
                view.OnError?.Execute(fbException.Message);

            public void OnSuccess(Java.Lang.Object result) =>
                view.OnSuccess?.Execute(((LoginResult)result).AccessToken.Token);
        }
    }
}