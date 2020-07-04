using Antlr4.Runtime.Misc;
using EO.Internal;
using System;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using System.Drawing;
using System.IO;
using Windows.UI.Xaml.Controls;

namespace UWPgame.Class
{
     public class Enemy:Entity
    {
        public int speed;
        internal static object args;

        public static void Move(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
       

          
            for (int j = 0; j < MainPage.enemyXPOS.Count; ++j)
            {

                if (MainPage.enemySHIP[j] == 1) { MainPage.SHIP_IMG = MainPage.Enemy1; }
                if (MainPage.enemySHIP[j] == 2) { MainPage.SHIP_IMG = MainPage.Enemy2; }

                if (MainPage.enemyDIR[j] == "left")
                {
                    MainPage.enemyXPOS[j] -= 3;
                }
                else
                {
                    MainPage.enemyXPOS[j] += 3;

                }
                MainPage.enemyYPOS[j] += 3;

                args.DrawingSession.DrawImage(Scaling.img(MainPage.SHIP_IMG), MainPage.enemyXPOS[j], MainPage.enemyYPOS[j]);

            }



        }

        internal void Move()
        {
            throw new NotImplementedException();
        }

       
    }
}
