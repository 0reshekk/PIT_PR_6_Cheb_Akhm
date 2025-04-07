using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для RegLog.xaml
    /// </summary>
    public partial class RegLog : Page
    {
        public RegLog()
        {
            InitializeComponent();
        }

        public static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        private void Autho_Butt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthLog());
        }

        public bool Reg(string login, string password, string email, string name)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!");
                return false;
            }

            using (var db = new Entities())
            {
                var e_mail = db.Users.AsNoTracking().FirstOrDefault(u => u.E_mail == email);
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Phone == login);
                if (user != null || e_mail != null)
                {
                    MessageBox.Show("Пользователь с такими данными уже существует! (Почта/Телефон)");
                    return false;
                }

                bool number = false;
                for (int i = 0; i < password.Length; i++) if (password[i] >= '0' && password[i] <= '9') number = true;

                var regex = new Regex(@"^8[0-9]{10}$");

                StringBuilder errors = new StringBuilder();

                if (password.Length < 6) errors.AppendLine("Пароль должен быть больше 6 символов");
                if (!regex.IsMatch(login)) errors.AppendLine("Укажите номер телефона в формате 8XXXXXXXXXX");
                if (!number) errors.AppendLine("Пароль должен содержать хотя бы одну цифру");
                if (!isValidMail(email)) errors.AppendLine("Введите корректный e-mail");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return false;
                }

                Users userObject = new Users
                {
                    FullName = name,
                    Password = GetHash(password),
                    RoleID = 1,
                    E_mail = email,
                    Phone = login,
                    CreatedAt = DateTime.Now,
                };
                db.Users.Add(userObject);
                db.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались!", "Успешно!", MessageBoxButton.OK);
                NavigationService.Navigate(new AuthLog());

                return true;
            }
        }

        private void Reg_Butt_Click(object sender, RoutedEventArgs e)
        {
            Reg(phoneText.Text, passwordText.Password, emailText.Text, nameText.Text);
            //if (string.IsNullOrEmpty(phoneText.Text) || string.IsNullOrEmpty(nameText.Text) || string.IsNullOrEmpty(emailText.Text) || string.IsNullOrEmpty(passwordText.Password))
            //{
            //    MessageBox.Show("Заполните все поля!");
            //    return;
            //}

            //using (var db = new financeEntities())
            //{
            //    var email = db.Users.AsNoTracking().FirstOrDefault(u => u.E_mail == emailText.Text);
            //    var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Phone == phoneText.Text);
            //    if (user != null || email != null)
            //    {
            //        MessageBox.Show("Пользователь с такими данными уже существует! (Почта/Телефон)");
            //        return;
            //    }
            //}

            //bool number = false;
            //for (int i = 0; i < passwordText.Password.Length; i++)
            //{
            //    if (passwordText.Password[i] >= '0' && passwordText.Password[i] <= '9') number = true;
            //}
            //var regex = new Regex(@"^8[0-9]{10}$");

            //StringBuilder errors = new StringBuilder();

            //if (passwordText.Password.Length < 6) errors.AppendLine("Пароль должен быть больше 6 символов");
            //if (!regex.IsMatch(phoneText.Text)) errors.AppendLine("Укажите номер телефона в формате 8XXXXXXXXXX");
            //if (!number) errors.AppendLine("Пароль должен содержать хотя бы одну цифру");
            //if (!isValidMail(emailText.Text)) errors.AppendLine("Введите корректный e-mail");

            //if (errors.Length > 0)
            //{
            //    MessageBox.Show(errors.ToString());
            //    return;
            //}

            //try
            //{
            //    financeEntities db = new financeEntities();
            //    Users userObject = new Users
            //    {
            //        FullName = nameText.Text,
            //        Password = GetHash(passwordText.Password),
            //        RoleID = 1,
            //        E_mail = emailText.Text,
            //        Phone = phoneText.Text,
            //        CreatedAt = DateTime.Now,
            //    };
            //    db.Users.Add(userObject);
            //    db.SaveChanges();
            //    MessageBox.Show("Вы успешно зарегистрировались!", "Успешно!", MessageBoxButton.OK);
            //    NavigationService.Navigate(new AuthLog());
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Произошла ошибка при регистрации: " + ex.Message, "Ошибка", MessageBoxButton.OK);
            //}
        }

        private bool isValidMail(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith(".")) return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

    }
}