using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ISIP421_Rutkovskaya
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            LiveTimeLabel.Content = DateTime.Now.ToString();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите закрыть приложение?", 
                "Подтверждение закрытия", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

		private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
		{
            if (!(e.Content is Page page)) return;
            this.Title = $"ProjectByRutBai - {page.Title}";

            if (page is Pages.AuthPage)
                ButtonBack.Visibility = Visibility.Hidden;
            else
                ButtonBack.Visibility = Visibility.Visible;

		}

		private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
		{
            if(MainFrame.CanGoBack) MainFrame.GoBack();
		}
	}
}
