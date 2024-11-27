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
	/// Логика взаимодействия для UserPage.xaml
	/// </summary>
	public partial class UserPage : Page
	{
		public UserPage()
		{
			InitializeComponent();
			SortComboBox.SelectedIndex = 0;
			RoleCheckBox.IsChecked = false;

			var currentUsers = Entities.GetContext().User.ToList();
			ListUser.ItemsSource = currentUsers;
		}

		private void UpdateUsers()
		{
			//загружаем всех пользователей в список
			var currentUsers = Entities.GetContext().User.ToList();
			//осуществляем поиск по Ф.И.О. без учета регистра букв
			currentUsers = currentUsers.Where(x => x.FIO.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
			//обрабатываем фильтр по одной роли пользователей
			if (RoleCheckBox.IsChecked.Value)
				currentUsers = currentUsers.Where(x => x.Role.Contains("Пользователь")).ToList();
			//осуществляем сортировку в зависимости от выбора пользователя
			if (SortComboBox.SelectedIndex == 0)
				ListUser.ItemsSource = currentUsers.OrderBy(x => x.FIO).ToList();
			else
				ListUser.ItemsSource = currentUsers.OrderByDescending(x => x.FIO).ToList();
		}

		private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			UpdateUsers();
		}

		private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateUsers();
		}

		private void RoleCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			UpdateUsers();
		}

		private void RoleCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			UpdateUsers();
		}

		private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
		{
			SearchTextBox.Text = string.Empty;
			SortComboBox.SelectedIndex = 0;
			RoleCheckBox.IsChecked = false;
			UpdateUsers();
		}
	}
}
