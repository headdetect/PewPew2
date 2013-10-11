using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MonoGame.Framework;


namespace PewPew2
{
    /// <summary>
    /// The root page used to display the game.
    /// </summary>
    public sealed partial class GamePage : SwapChainBackgroundPanel
    {
        private readonly Dictionary<string, object> _bundle;

        public GamePage(Dictionary<string, object> bundle)
        {
            this.InitializeComponent();
            _bundle = bundle;
        }

        public void ActivateGame<T>() where T : PewPew2Game, new()
        {

            // Create the game.
            App.PewPew2Game = XamlGame<T>.Create(string.Empty, Window.Current.CoreWindow, this);
            App.PewPew2Game.Bundle = _bundle;
        }
    }
}
