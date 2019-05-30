using System;
using System.Windows;
using System.Media;
using System.ComponentModel;

namespace BarneyErrorGenerator
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public int image = 0;
        Character myCharacter = new Character(Properties.Settings.Default.Character);
        public ErrorWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;



            //left out until settings implemented

            //if (Properties.Settings.Default.VirtualPowerButton == false)
            //    powerButton.Visibility = Visibility.Hidden;


            powerButton.Click += PowerButton_Click;
            this.ContentRendered += ErrorWindow_ContentRendered;
        }

        private void ErrorWindow_ContentRendered(object sender, EventArgs e)
        {
            myCharacter.SyncSpeak();

        }

        public Visibility PowerButtonVisibility
        {
            get { return powerButton.Visibility; }
            set { powerButton.Visibility = value; }
        }

        public Visibility ErrorButtonVisibility
        {
            get { return enterButton.Visibility; }
            set { enterButton.Visibility = value; }
        }
        public Visibility TextBoxVisibility
        {
            get { return textBox.Visibility; }
            set { textBox.Visibility = value; }
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Windows);
            simpleSound.Play();
            DarkWindow darkWindow = new DarkWindow();
            darkWindow.Show();
            PowerButtonVisibility = Visibility.Hidden;
            myCharacter.ChanceLoss(5);
            chanceBox.Text = myCharacter.GetChance().ToString();
        }


        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            if(myCharacter.GetChance()!=0)
                myCharacter.ChanceLoss();
            chanceBox.Text = myCharacter.GetChance().ToString();
            if (myCharacter.GetChance() <= 25)
            {
                ErrorButtonVisibility = Visibility.Hidden;
                TextBoxVisibility = Visibility.Hidden;
            }
            myCharacter.HideSpeak();
        }

        private void powerButton_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (powerButton.IsVisible == true && myCharacter.GetChance() < 30)
            {
                image++;
                if(myCharacter.GetChance() > 15)
                    myCharacter.PowerSpeak();
                myCharacter.Speak();
                if (myCharacter.GetChance() == 31)
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(DarkWindow))
                        {
                            (window as DarkWindow).Hide();
                        }
                    }
                    Hide();
                    PunishmentWindow punishmentWindow = new PunishmentWindow();
                }
                    
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {

            Application.Current.Shutdown();
            // Environment.Exit();
        }
    }
}

