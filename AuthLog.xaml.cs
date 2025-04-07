using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PIT_PR_6_Cheb_Akhm
{
    /// <summary>
    /// Логика взаимодействия для AuthLog.xaml
    /// </summary>
    public partial class AuthLog : Page
    {
        public AuthLog()
        {
            InitializeComponent();
        }

        private void Autho_Butt_Click(object sender, RoutedEventArgs e)
        {
            Auth(phoneText.Text, passwordText.Password);
            //if (string.IsNullOrEmpty(phoneText.Text) || string.IsNullOrEmpty(passwordText.Password))
            //{
            //    MessageBox.Show("Заполните все поля!");
            //    return;
            //}

            //try
            //{
            //    using (var db = new financeEntities())
            //    {
            //        var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Phone == phoneText.Text); // Пользователь в БД, у которого номер телефона совпадает

            //        if (user == null)
            //        {
            //            MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK);
            //            return;
            //        }

            //        string inputHash = RegLog.GetHash(passwordText.Password); // Хэшируем пароль

            //        //MessageBox.Show($"Введенный хэш: {inputHash}\nХэш из БД: {user.Password}");  (удалить после теста)

            //        if (user.Password == inputHash)
            //        {
            //            MessageBox.Show("Успешный вход!", "Добро пожаловать", MessageBoxButton.OK);
            //            NavigationService.Navigate(new MainPage());
            //        }
            //        else MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка системы", MessageBoxButton.OK);
            //}
        }

        public bool Auth(string login, string password)
        {
            //if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            //{
            //    MessageBox.Show("Заполните все поля!");
            //    return false;
            //}

            //using (var db = new financeEntities())
            //{
            //    var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Phone == login && u.Password == password);

            //    if (user == null)
            //    {
            //        MessageBox.Show("Пользователь с такими данными не найден!");
            //        return false;
            //    }
            //    MessageBox.Show("Пользователь успешно найден!");
            //    phoneText.Clear();
            //    passwordText.Clear();

            //    return true;
            //}
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!");
                return false;
            }

            try
            {
                using (var db = new Entities())
                {
                    var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Phone == login); // Пользователь в БД, у которого номер телефона совпадает

                    if (user == null)
                    {
                        MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK);
                        return false;
                    }

                    string inputHash = RegLog.GetHash(password); // Хэшируем пароль

                    //MessageBox.Show($"Введенный хэш: {inputHash}\nХэш из БД: {user.Password}");  (удалить после теста)

                    if (user.Password == inputHash)
                    {
                        MessageBox.Show("Успешный вход!", "Добро пожаловать", MessageBoxButton.OK);
                        NavigationService.Navigate(new MainPage());
                    }
                    else MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка системы", MessageBoxButton.OK);
            }
            return true;
        }

    }
}
