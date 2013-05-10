namespace aurora
{
    using System;

    using Microsoft.WindowsAzure.MobileServices;

    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    sealed partial class App
    {
        #region Static Fields

        public static MobileServiceClient MobileService = new MobileServiceClient(
            "https://aurora.azure-mobile.net/", "hEBLGXeZnzCroYtgsqCWaGcAPQYtUW81");

        #endregion

        #region Constructors and Destructors

        public App()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
            {
                Window.Current.Activate();
                return;
            }

            var rootFrame = new Frame();
            if (!rootFrame.Navigate(typeof(MainPage)))
            {
                throw new Exception("Failed to create initial page");
            }

            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        #endregion
    }
}