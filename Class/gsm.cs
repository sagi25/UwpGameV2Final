using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPgame.Class
{
    public class gsm
    {
        public static void GSM()
        {
            if(MainPage.RoundEnded == true)
            {
                MainPage.BG = MainPage.ScoreScreen;
            }else
            {
                if (MainPage.GameState == 0)
                {

                    MainPage.BG = MainPage.StartScreen;

                }
                else if (MainPage.GameState == 1)
                {
                    MainPage.BG = MainPage.Level1;
                }
            }


         

        }


    }
}
