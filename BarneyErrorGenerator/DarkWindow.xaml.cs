using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace BarneyErrorGenerator
{
    /// <summary>
    /// Interaction logic for DarkWindow.xaml
    /// </summary>
    public partial class DarkWindow : Window
    {

        Storyboard FirstStoryboard = new Storyboard();
        Storyboard SecondStoryboard = new Storyboard();
        Duration myDuration = new Duration(TimeSpan.FromSeconds(2));
        public bool _BoardDark=false;



        public DarkWindow()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            this.IsVisibleChanged += DarkWindow_IsVisibleChanged;
            FirstStoryboard.Completed += FirstStoryboard_Completed;
            SecondStoryboard.Completed += SecondStoryboard_Completed;
         }

        private void SecondStoryboard_Completed(object sender, EventArgs e)
        {
            
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(ErrorWindow))
                {
                    (window as ErrorWindow).PowerButtonVisibility = Visibility.Visible;
                }
            }
            Hide();
        }

        private void DarkWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            DoubleAnimation upFader = new DoubleAnimation();
            upFader.Duration = myDuration;
            upFader.To = 1;
            FirstStoryboard.Children.Add(upFader);
            Storyboard.SetTarget(upFader, this);
            Storyboard.SetTargetProperty(upFader, new PropertyPath(OpacityProperty));
            FirstStoryboard.Begin();
        }
        private void FirstStoryboard_Completed(object sender, EventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(ErrorWindow))
                {
                    if((window as ErrorWindow).image == 1)
                        (window as ErrorWindow).characterImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/barney2.jpg"));
                    if ((window as ErrorWindow).image == 2)
                        (window as ErrorWindow).characterImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/barney3.jpg"));
                    if ((window as ErrorWindow).image == 3)
                        (window as ErrorWindow).characterImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/barney4.jpg"));
                }
            }
            DoubleAnimation downFader = new DoubleAnimation();
            downFader.Duration = myDuration;
            downFader.To = 0;
            SecondStoryboard.Children.Add(downFader);
            Storyboard.SetTarget(downFader, this);
            Storyboard.SetTargetProperty(downFader, new PropertyPath(OpacityProperty));
            SecondStoryboard.Begin();
        }


    }
}
