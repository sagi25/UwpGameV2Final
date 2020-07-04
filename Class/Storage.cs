using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace UWPgame.Class
{
    public static class Storage
    {

        
        public static StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        public static async void CreateFile()
        {
            try
            {
                await storageFolder.CreateFileAsync("UWPspaceShooter.txt", CreationCollisionOption.OpenIfExists);
            }
            catch
            {
                var messageDialog = new MessageDialog("Create File Failed.");
            }
        }

        public static async void ReadFile()
        {
            try
            {
                StorageFile DataFile = await storageFolder.GetFileAsync("UWPspaceShooter.txt");
                MainPage.STRHighScore = await FileIO.ReadTextAsync(DataFile);
            }
            catch
            {
                var messageDialog = new MessageDialog("Create File Failed.");
            }
        }
        public static async void UpdateScore()
        {

            if(MainPage.Myscore > Convert.ToInt16(MainPage.STRHighScore))
            {
                try
                {
                    StorageFile DataFile = await storageFolder.GetFileAsync("UWPspaceShooter.txt");
                    await FileIO.WriteTextAsync(DataFile, MainPage.Myscore.ToString());
                    ReadFile();
                }
                catch
                {
                    var messageDialog = new MessageDialog("Create File Failed."); 
                }
            }

            
        }

    }
}
