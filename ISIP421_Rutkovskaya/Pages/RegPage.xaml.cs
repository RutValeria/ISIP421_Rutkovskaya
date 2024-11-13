using System;
using System.Collections.Generic;
using System.Data;
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
	/// Логика взаимодействия для RegPage.xaml
	/// </summary>
	public partial class RegPage : Page
	{
		public RegPage()
		{
			InitializeComponent();
			CBoxRole.ItemsSource = new Role[]
			{
				new Role { Name = "Пользователь"},
				new Role { Name = "Администратор"}
			};
			CBoxRole.SelectedIndex = 0;
		}

		private void TBoxPasswordRepeat_PasswordChanged(object sender, RoutedEventArgs e)
		{

        }

		private void TBoxName_TextChanged(object sender, TextChangedEventArgs e)
		{

        }
    }
	public class Role
	{
		public string Name { get; set; }
	}
}
