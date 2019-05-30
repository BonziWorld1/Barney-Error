using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BarneyErrorGenerator
{
    /// <summary>
    /// Interaction logic for PunishmentWindow.xaml
    /// </summary>
    public partial class PunishmentWindow : Window
    {
        int count = 0;


        public PunishmentWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            Show();
            Update();
        }

        private async Task Update()
        {
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Images/CaillouPunishment.jpg"));
            await Task.Run(() =>
            {
                play();
            });

            image.Source = new BitmapImage(new Uri("pack://application:,,,/Images/FacesThatIPunish.jpg"));
            await Task.Run(() =>
            {
                play();
            });

            image.Source = new BitmapImage(new Uri("pack://application:,,,/Images/MotherGoosePunishment.jpg")); 
;
            await Task.Run(() =>
            {
                play();
            });

            Application.Current.Shutdown();

        }

        private void play()
        {
            if (count == 2)
            {
                SoundPlayer simplestSound = new SoundPlayer(Properties.Resources.Mother_Goose_Creepy);
                simplestSound.Play();
                System.Threading.Thread.Sleep(7000);

            }

            if (count == 1)
            {
                SoundPlayer simplerSound = new SoundPlayer(Properties.Resources.Faces_Creepy);
                simplerSound.Play();
                System.Threading.Thread.Sleep(7000);

                count++;
            }

            if (count == 0)
            {
                SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Cailou_creepy);
                simpleSound.Play();
                System.Threading.Thread.Sleep(7000);

                count++;
            }




        }
    }
}
