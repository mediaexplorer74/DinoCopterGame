using steamworks.games.game.core;
using steamworks.games.game.opengl;
using Steamworks.Games.Game.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace GameManager
{
    public sealed partial class GamePage : Page
    {
        //readonly /*Game2*/Game1 _game;
        readonly Game1 _game;// = new Game1((IDataUtils)new OpenGlDataUtils(),
              //  (IPersister)new OpenGlPersister());

        public GamePage()
        {
            this.InitializeComponent();

			// Create the game.
			var launchArguments = string.Empty;
            
            _game = MonoGame.Framework.XamlGame<Game1>.Create(//<Game2>.Create(
                launchArguments, 
                Window.Current.CoreWindow, 
                swapChainPanel);
        }
    }
}

/*
 #nullable disable
namespace steamworks.games.game.opengl
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (Game1 sillyGhostGame = 
                new Game1((IDataUtils)new OpenGlDataUtils(), 
                (IPersister)new OpenGlPersister()))
                sillyGhostGame.Run();
        }
    }
}

 */
