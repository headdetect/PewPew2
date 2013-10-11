using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227
using Windows.UI.Xaml.Controls;
using PewPew2.Pages;

namespace PewPew2
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {

        private static PewPew2Game _pewpew2Game;

        public static PewPew2Game PewPew2Game
        {
            get { return _pewpew2Game; }
            set
            {
                if (_pewpew2Game != null) return;
                _pewpew2Game = value;
            }
        }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            //TODO: Change to logo page

            var gamePage = Window.Current.Content as GamePage;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (gamePage == null)
            {
                // Create a main GamePage
                gamePage = new GamePage(null);
                gamePage.ActivateGame<MainPage>();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the GamePage in the current Window
                Window.Current.Content = gamePage;
            }


            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity

            deferral.Complete();
        }

        public static void Navigate<T>(Dictionary<string, object> bundle) where T : PewPew2Game, new()
        {
            var gamePage = new GamePage(bundle);
            gamePage.ActivateGame<T>();
            Window.Current.Content = gamePage;
            Window.Current.Activate();
        }
    }
}
