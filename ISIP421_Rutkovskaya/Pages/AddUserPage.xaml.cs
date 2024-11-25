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
	/// Логика взаимодействия для AddUserPage.xaml
	/// </summary>
	public partial class AddUserPage : Page
	{
		private User _currentUser = new User();
		public AddUserPage(User selectedUser)
		{
			InitializeComponent();

			if (selectedUser != null)
			{
				_currentUser = selectedUser;
				cmbRole.Text = _currentUser.Role;
			}
				
			
			DataContext = _currentUser;
		}

		private void ButtonSave_Click(object sender, RoutedEventArgs e)
		{
			StringBuilder errors = new StringBuilder();
			if (string.IsNullOrWhiteSpace(_currentUser.Login)) errors.AppendLine("Укажите логин!");
			if (string.IsNullOrWhiteSpace(_currentUser.Password)) errors.AppendLine("Укажите пароль!");
			if ((_currentUser.Role == null) || (cmbRole.Text == "")) errors.AppendLine("Выберите роль!");
			else _currentUser.Role = cmbRole.Text;
			if (string.IsNullOrWhiteSpace(_currentUser.FIO)) errors.AppendLine("Укажите Ф.И.О.!");
			// Проверяем переменную errors на наличие ошибок
			if (errors.Length > 0)
			{
				MessageBox.Show(errors.ToString());
				return;
			}
			// Добавляем в объект User новую запись
			if (_currentUser.ID == 0)
				Entities.GetContext().User.Add(_currentUser);
			// Делаем попытку записи данных в БД о новом пользователе
			try
			{
				Entities.GetContext().SaveChanges();
				MessageBox.Show("Данные успешно сохранены!");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
        }

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			NavigationService?.Navigate(new AdminPage());
		}
    }
}
