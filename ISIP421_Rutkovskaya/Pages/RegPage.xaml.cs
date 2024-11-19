using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
        private void TBoxLogin_TextChanged(object sender, RoutedEventArgs e)
        {
			txtHintLogin.Visibility = Visibility.Visible;
			if (TBoxLogin.Text.Length > 0)
			{
				txtHintLogin.Visibility = Visibility.Hidden;
			}
        }
		private void TBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
		{
			txtHintPassword.Visibility = Visibility.Visible;
			if (TBoxPassword.Password.Length > 0)
			{
				txtHintPassword.Visibility = Visibility.Hidden;
			}
		}
        private void TBoxPasswordRepeat_PasswordChanged(object sender, RoutedEventArgs e)
        {
			txtHintPasswordRepeat.Visibility = Visibility.Visible;
			if (TBoxPasswordRepeat.Password.Length > 0)
			{
				txtHintPasswordRepeat.Visibility = Visibility.Hidden;
			}
        }
        private void TBoxName_TextChanged(object sender, RoutedEventArgs e)
		{
			txtHintName.Visibility = Visibility.Visible;
			if (TBoxName.Text.Length > 0)
			{
				txtHintName.Visibility = Visibility.Hidden;
			}
		}

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
			NavigationService?.Navigate(new AuthPage());
        }

		private void ButtonReg_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TBoxLogin.Text) || string.IsNullOrEmpty(TBoxPassword.Password) || string.IsNullOrEmpty(TBoxPasswordRepeat.Password) || string.IsNullOrEmpty(TBoxName.Text))
			{
				MessageBox.Show("Заполните все поля!");
				return;
			}

			using (var pr = new Entities())
			{
				var user = pr.User
					.AsNoTracking()
					.FirstOrDefault(u => u.Login == TBoxLogin.Text);
				if (user != null)
				{
					MessageBox.Show("Пользователь с таким логином уже существует!");
					return;
				}
			}

			if (TBoxPassword.Password.Length < 6)
			{
				MessageBox.Show("Пароль слишком короткий, минимум 6 символов");
				return;
			}
			
			bool en = true; // английская раскладка
			bool number = false; // цифра
			for (int i = 0; i < TBoxPassword.Password.Length; i++) // перебираем
			{
				if (TBoxPassword.Password[i] >= 'А' && TBoxPassword.Password[i] <= 'Я') en = false; // если русская раскладка
				if (TBoxPassword.Password[i] >= '0' && TBoxPassword.Password[i] <= '9') number = true; // если цифра
			}
			if (!en)
			{
				MessageBox.Show("Доступна только английская раскладка"); // выводим сообщение
				return;
			}
			if (!number)
			{
				MessageBox.Show("Добавьте хотя бы одну цифру"); // выводим сообщение
				return;
			}
			if (en && number)
			{
				MessageBox.Show("Пароль должен содержать анлийские буквы и цифры"); // выводим сообщение
				return;
			}
				
			if (TBoxPassword.Password != TBoxPasswordRepeat.Password) // проверка на совпадение
			{
				MessageBox.Show("Пароли не совпадают!");
				return;
			}
			Entities db = new Entities();
			User userObject = new User
			{
				FIO = TBoxName.Text,
				Login = TBoxLogin.Text,
				Password = GetHash(TBoxPassword.Password),
				Role = CBoxRole.Text
			};
			db.User.Add(userObject);
			db.SaveChanges();
			MessageBox.Show("Пользователь зарегистрирован!");

		}

		public static string GetHash(string password)
		{
			using (var hash = SHA1.Create())
			{
				return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
			}
		}
	}
	public class Role
	{
		public string Name { get; set; }
	}
}
