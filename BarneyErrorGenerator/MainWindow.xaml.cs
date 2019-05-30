using System;
using System.Windows;
using System.Windows.Controls;

namespace BarneyErrorGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool change = false;

        //Set dependency properties to track changes.
        public bool Startup
        {
            get { return (bool)GetValue(StartupProperty); }
            set { SetValue(StartupProperty, value); }
        }
        
        public static readonly DependencyProperty StartupProperty =
            DependencyProperty.Register("Startup", typeof(bool), typeof(MainWindow), new PropertyMetadata(new PropertyChangedCallback(OnStartupChanged)));

        private static void OnStartupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            change = true;
        }

        public bool Virtual
        {
            get { return (bool)GetValue(VirtualProperty); }
            set { SetValue(VirtualProperty, value); }
        }
        
        public static readonly DependencyProperty VirtualProperty =
            DependencyProperty.Register("Virtual", typeof(bool), typeof(MainWindow), new PropertyMetadata(new PropertyChangedCallback(OnVirtualChanged)));

        private static void OnVirtualChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            change = true;
        }

        public MainWindow()
        {
            InitializeComponent();
            ErrorWindow errorWindow = new ErrorWindow();
            errorWindow.Show();
            Hide();

            //settings functionality left out for now

            //    IdentityBox.Items.Add("Barney"); // put our available characters into the combo box
            //    IdentityBox.Items.Add("Riff");
            //    IdentityBox.Items.Add("Brandon");

            //    //Couldn't figure out how to do a line break in the XML so I'm doing it here as a string.
            //    StartupBox.ToolTip = "Turning this on will cause the program to function like a true Barney Error.\nThe error will begin upon next system boot and every boot until punishment has been dealt.";
            //    VirtualBox.ToolTip = "This enables a fake power button on screen.\nWhen clicked, it will make the computer appear to power on and off quickly.\nIt's actually just fading the screen in and out. Barney doesn't know the difference.";

            //    //Bind events
            //SaveButton.Click += SaveButton_Click;
            //    IdentityBox.SelectionChanged += IdentityBox_SelectionChanged;
            //    this.Closing += MainWindow_Closing;
            //    //let checkboxes know where to look for the binding properties
            //    VirtualBox.DataContext = this;
            //    StartupBox.DataContext = this;

        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (change == true)
            {
                string messageBoxText = "Do you want to save the changes you've made?";
                string caption = "Save Changes";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        saveSettings();
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void saveSettings()
        {
            Properties.Settings.Default.Character = (string)IdentityBox.SelectionBoxItem;
            Properties.Settings.Default.VirtualPowerButton = (bool)VirtualBox.IsChecked;
            Properties.Settings.Default.ContinueAtStartup = (bool)StartupBox.IsChecked;
            Properties.Settings.Default.Save();
            change = false;
        }

        private void IdentityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            change = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            saveSettings();
            SavedText.Text = "Saved " + DateTime.Now.ToShortTimeString();
            change = false;
        }

        private void SaveBeginButton_Click(object sender, RoutedEventArgs e)
        {
            saveSettings();
            change = false;
            ErrorWindow errorWindow = new ErrorWindow();
            errorWindow.Show();
            Hide();
        }
    }
    
}
