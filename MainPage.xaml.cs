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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPgame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static  CanvasBitmap BG, StartScreen, Level1, ScoreScreen, photon, Enemy1, Enemy2, SHIP_IMG, Myship, Boom;
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        public static float DesighWidth = 2160;
        public static float DesignHeight = 2000;
        public static float scaleWidth, scaleHeight, pointX, pointY, photonX, photonY, Myscore = 0, boomX, boomY;
        public static int boomCount = 60;

        //HighScore
        public static string STRHighScore = "0";
        public static int HighScore;


        public static int countdown = 60; /*60 sec roundtime*/
        public static bool RoundEnded = false;

        public static int GameState = 0; // StartScreen

        public static DispatcherTimer RoundTimer = new DispatcherTimer();
        public static DispatcherTimer EnemyTimer = new DispatcherTimer();

        //list
        public static List<float> photonXPOS = new List<float>();

        //MediaPlayer
        MediaPlayer player;

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"ASSETS");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("8-bit Music Chiptune Mix RELOADED.mp3");

            player.AutoPlay = true;
            player.Source = MediaSource.CreateFromStorageFile(file);

            player.Play();
        }

        public static List<float> photonYPOS = new List<float>();
        public static List<float> percent = new List<float>();
        public static List<Player1> Players = new List<Player1>();

        // enemies
        public static List<Enemy> Enemies = new List<Enemy>();
        public static List<float> enemyXPOS = new List<float>();
        public static List<float> enemyYPOS = new List<float>();
        public static List<int> enemySHIP = new List<int>();
        public static List<string> enemyDIR = new List<string>();

        // radnom gen
        public Random EnemyShipRand = new Random();
        public Random EnemyGenRand = new Random();
        public Random EnemyXstart = new Random();
        internal static object args;

        public MainPage()
        {
            this.InitializeComponent();
            player = new MediaPlayer();
            Window.Current.SizeChanged += Current_SizeChanged; 
            Scaling.SetScale();
            photonX = (float)bounds.Width / 2;
            photonY = (float)bounds.Height;



            RoundTimer.Tick += RoundTimer_Tick;
            RoundTimer.Interval = new TimeSpan(0, 0, 1);

            EnemyTimer.Tick += EnemyTimer_Tick;
            EnemyTimer.Interval = new TimeSpan(0, 0, 0, 0, EnemyGenRand.Next(300, 3000));

            Storage.CreateFile();
            //Storage.ReadFile();
            
            HighScore = Convert.ToInt16(STRHighScore);

        }



        private void EnemyTimer_Tick(object sender, object e)
        {


            int ES = EnemyShipRand.Next(1, 3);
            int SP = EnemyXstart.Next(0, (int)bounds.Width); // staring position x
            if (SP > bounds.Width / 2)
            {
                enemyDIR.Add("left");
            }
            else
            {
                enemyDIR.Add("right");
            }


            enemyXPOS.Add(SP);
            enemyYPOS.Add(-50 * scaleHeight);
            enemySHIP.Add(ES);

            EnemyTimer.Interval = new TimeSpan(0, 0, 0, 0, EnemyGenRand.Next(500, 2000));


           

        }

        private void RoundTimer_Tick(object sender, object e)
        {

            countdown -= 1;

            if (countdown < 1)
            {
                RoundTimer.Stop();
                RoundEnded = true;
            }


        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            Scaling.SetScale();

            photonX = (float)bounds.Width / 2;
            photonY = (float)bounds.Height;
        }

        private void GameCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        async Task CreateResourcesAsync(CanvasControl sender)
        {
            StartScreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/startscreen.png"));
            Level1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/level1.jpg"));
            ScoreScreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/scorescreen.png"));
            Myship = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/Myship.png"));
            photon = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/photon.png"));
            Enemy1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/enemy1.png"));
            Enemy2 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/enemy2.png"));
            Boom = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/Boom.png"));
        }

        private void GameCanvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            gsm.GSM();
            args.DrawingSession.DrawImage(Scaling.img(BG));
            args.DrawingSession.DrawText(countdown.ToString(), 100, 100, Windows.UI.Colors.Yellow);

            if (RoundEnded == true)
            {

                if (Myscore > Convert.ToInt16(STRHighScore))
                {
                    Storage.UpdateScore();
                }

                CanvasTextLayout textLayout1 = new CanvasTextLayout(args.DrawingSession, Myscore.ToString(), new CanvasTextFormat() { FontSize = (120 * scaleHeight), WordWrapping = CanvasWordWrapping.NoWrap }, 0.0f, 0.0f);
                args.DrawingSession.DrawTextLayout(textLayout1, ((DesighWidth * scaleWidth) / 2) - ((float)textLayout1.DrawBounds.Width / 2), 350 * scaleHeight, Windows.UI.Colors.White);
                args.DrawingSession.DrawText("HighScore: " + Convert.ToInt16(STRHighScore), new Vector2(200, 200), Windows.UI.Color.FromArgb(255, 255, 255, 255));


            } else
            {
                if (GameState > 0)
                {


                    args.DrawingSession.DrawText("Score: " + Myscore.ToString(), (float)bounds.Width / 2, 10, Windows.UI.Color.FromArgb(255, 255, 255, 255));




                    if (boomX > 0 && boomY > 0 && boomCount > 0)
                    {
                        args.DrawingSession.DrawImage(Scaling.img(Boom), boomX, boomY);
                        boomCount -= 1;
                    }
                    else
                    {
                        boomCount = 60;
                        boomX = 0;
                        boomY = 0;
                    }

                    //enemy 


                    for (int j = 0; j < enemyXPOS.Count; ++j)
                    {

                        if (enemySHIP[j] == 1) { SHIP_IMG = Enemy1; }
                        if (enemySHIP[j] == 2) { SHIP_IMG = Enemy2; }

                        if (enemyDIR[j] == "left")
                        {
                            enemyXPOS[j] -= 3;
                        }
                        else
                        {
                            enemyXPOS[j] += 3;

                        }
                        enemyYPOS[j] += 3;
                        args.DrawingSession.DrawImage(Scaling.img(SHIP_IMG), enemyXPOS[j], enemyYPOS[j]);


                    }

                    // display

                    foreach (Player1 ship in Players)
                    {
                        ship.laser();
                    }

                    for (int i = 0; i < photonXPOS.Count; ++i)
                    {

                        pointX = (photonX + (photonXPOS[i] - photonX) * percent[i]);
                        pointY = (photonY + (photonYPOS[i] - photonY) * percent[i]);


                        args.DrawingSession.DrawImage(Scaling.img(photon), pointX - (145 * scaleWidth), pointY - (145 * scaleHeight));
                    

                    percent[i] += (0.050f * scaleHeight);
                        //    //Enemies
                        //    foreach (Enemy Mob in Enemies)
                        //    {

                        //        Mob.Move();

                        //    }

                        for (int h = 0; h < enemyXPOS.Count; ++h)
                        {
                            if (pointX >= enemyXPOS[h] && pointX <= enemyXPOS[h] + (302 * scaleWidth) && pointY >= enemyYPOS[h] && pointY <= enemyYPOS[h] + (167 * scaleHeight))
                            {
                                boomX = pointX - (151 * scaleWidth);
                                boomY = pointY - (83 * scaleHeight);

                                enemyXPOS.RemoveAt(h);
                                enemyYPOS.RemoveAt(h);
                                enemySHIP.RemoveAt(h);
                                enemyDIR.RemoveAt(h);

                                photonXPOS.RemoveAt(i);
                                photonYPOS.RemoveAt(i);
                                percent.RemoveAt(i);

                                Myscore = Myscore + 100;

                                break;


                            }


                        }

                        if (pointY < 0f)
                        {
                            photonXPOS.RemoveAt(i);
                            photonYPOS.RemoveAt(i);
                            percent.RemoveAt(i);
                        }
                    }
                    args.DrawingSession.DrawImage(Scaling.img(Myship), (float)bounds.Width / 2 - (150 * scaleWidth), (float)bounds.Height - (130 * scaleHeight));

                }
            }

            

            GameCanvas.Invalidate();
        }

        private void GameCanvas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(RoundEnded == true)
            {

                if ((float)e.GetPosition(GameCanvas).X > 555 * scaleWidth && (float)e.GetPosition(GameCanvas).X < 1331 * scaleWidth && (float)e.GetPosition(GameCanvas).Y > 321 * scaleHeight && (float)e.GetPosition(GameCanvas).Y < 1300 * scaleHeight)
                {

                   GameState = 0;
                   RoundEnded = false;
                   countdown = 60;
                   
                   // stop enemy timer
                   EnemyTimer.Stop();
                   enemyXPOS.Clear();
                   enemyYPOS.Clear();
                   enemySHIP.Clear();
                   enemyDIR.Clear();

                }


            }
            else
            {
                if (GameState == 0) 
                {
                    GameState += 1;
                    RoundTimer.Start();
                    EnemyTimer.Start();

                   
                }else if (GameState > 0)
                {
                    photonXPOS.Add((float)e.GetPosition(GameCanvas).X);
                    photonYPOS.Add((float)e.GetPosition(GameCanvas).Y);
                    percent.Add(0f);
                }
            }

            
        }
    }
}
