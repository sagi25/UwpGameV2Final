using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UWPgame.Class;
using System.Drawing;
using Microsoft.Graphics.Canvas.Text;
using Windows.Storage;
using System.Numerics;
using Windows.Media.Playback;
using Windows.Media.Core;


namespace UWPgame.Class
{
    

    
   
    public class Entity
    {
        
        public CanvasBitmap sprite;
        public static CanvasBitmap BG, StartScreen, Level1, ScoreScreen, photon, Enemy1, Enemy2, SHIP_IMG, Myship, Boom;
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        internal static object args;

        private void GameCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        async Task CreateResourcesAsync(CanvasControl sender)
        {
            //StartScreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/startscreen.png"));
            //Level1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/level1.jpg"));
            //ScoreScreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/scorescreen.png"));
            Myship = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/Myship.png"));
            photon = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/photon.png"));
            Enemy1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/enemy1.png"));
            Enemy2 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/enemy2.png"));
            Boom = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/Boom.png"));
        }
    }
}
