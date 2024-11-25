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

namespace ISIP421_Rutkovskaya.Pages
{
	/// <summary>
	/// Логика взаимодействия для AdminPage.xaml
	/// </summary>
	public partial class AdminPage : Page
	{
		public AdminPage()
		{
			InitializeComponent();
			DataGridUser.ItemsSource = Entities.GetContext().User.ToList();
		}

		private void ButtonAdd_Click(object sender, RoutedEventArgs e)
		{
			NavigationService?.Navigate(new AddUserPage());
		}

		private void ButtonDel_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (Visibility == Visibility.Visible)
			{
				Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
				DataGridUser.ItemsSource = Entities.GetContext().User.ToList();
			}
        }
    }
}
