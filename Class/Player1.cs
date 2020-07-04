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
    public class Player1  : Entity 
    {
        public int fast;

        public static CanvasBitmap BG, StartScreen, Level1, ScoreScreen, photon, Enemy1, Enemy2, SHIP_IMG, Myship, Boom;
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
      
        public static float scaleWidth, scaleHeight, pointX, pointY, photonX, photonY, Myscore = 0, boomX, boomY;
       

        public static List<float> photonYPOS = new List<float>();
        public static List<float> percent = new List<float>();

        internal static object args;


        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            Scaling.SetScale();

            photonX = (float)bounds.Width / 2;
            photonY = (float)bounds.Height;
        }

        public static void laser(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            for (int i = 0; i < MainPage.photonXPOS.Count; ++i)
            {

                pointX = (photonX + (MainPage.photonXPOS[i] - photonX) * percent[i]);
                pointY = (photonY + (MainPage.photonYPOS[i] - photonY) * percent[i]);


                args.DrawingSession.DrawImage(Scaling.img(photon), pointX - (145 * scaleWidth), pointY - (145 * scaleHeight));
                

                percent[i] += (0.050f * scaleHeight);
                //Enemies
                foreach (Enemy Mob in MainPage.Enemies)
                {

                    Mob.Move();

                }

                for (int h = 0; h < MainPage.enemyXPOS.Count; ++h)
                {
                    if (pointX >= MainPage.enemyXPOS[h] && pointX <= MainPage.enemyXPOS[h] + (302 * scaleWidth) && pointY >= MainPage.enemyYPOS[h] && pointY <= MainPage.enemyYPOS[h] + (167 * scaleHeight))
                    {
                        boomX = pointX - (151 * scaleWidth);
                        boomY = pointY - (83 * scaleHeight);

                        MainPage.enemyXPOS.RemoveAt(h);
                        MainPage.enemyYPOS.RemoveAt(h);
                        MainPage.enemySHIP.RemoveAt(h);
                        MainPage.enemyDIR.RemoveAt(h);

                        MainPage.photonXPOS.RemoveAt(i);
                        MainPage.photonYPOS.RemoveAt(i);
                        MainPage.percent.RemoveAt(i);

                        Myscore = Myscore + 100;

                        break;


                    }


                }

                if (pointY < 0f)
                {
                    MainPage.photonXPOS.RemoveAt(i);
                    MainPage.photonYPOS.RemoveAt(i);
                    MainPage.percent.RemoveAt(i);
                }
            }

            //for (int i = 0; i<MainPage.photonXPOS.Count; ++i) 
            //    {
            //                pointX = (photonX + (MainPage.photonXPOS[i] - photonX) * percent[i]);
            //                pointY = (photonY + (MainPage.photonYPOS[i] - photonY) * percent[i]);
            //    }

            //    for (int h = 0; h < MainPage.enemyXPOS.Count; ++h)
            //    {
            //        if (pointX >= MainPage.enemyXPOS[h] && pointX <= MainPage.enemyXPOS[h] + (302 * scaleWidth) && pointY >= enemyYPOS[h] && pointY <= enemyYPOS[h] + (167 * scaleHeight))
            //        {
            //            boomX = pointX - (151 * scaleWidth);
            //            boomY = pointY - (83 * scaleHeight);

            //            MainPage.enemyXPOS.RemoveAt(h);
            //            MainPage.enemyYPOS.RemoveAt(h);
            //            MainPage.enemySHIP.RemoveAt(h);
            //            MainPage.enemyDIR.RemoveAt(h);

            //            MainPage.photonXPOS.RemoveAt(i);
            //            MainPage.photonYPOS.RemoveAt(i);
            //            MainPage.percent.RemoveAt(i);

            //            Myscore = Myscore + 100;

            //            break;


            //        }


            //    }

        }

        internal void laser()
        {
            throw new NotImplementedException();
        }
    }
}
