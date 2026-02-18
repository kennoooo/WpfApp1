using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using WpfApp1;

namespace WpfApp1
{
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            // Простейшая проверка на пустые поля
            if (string.IsNullOrEmpty(TxtLogin.Text) && string.IsNullOrEmpty(TxtPass.Text)  && string.IsNullOrEmpty(TxtName.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            var existingUser = Core.DB.Users.FirstOrDefault(u => u.Login == TxtLogin.Text);
            if (existingUser != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }

            try
            {
                // Создаем нового пользователя
                Users newUser = new Users
                {
                    Login = TxtLogin.Text,
                    Password = TxtPass.Text, // В реальных проектах пароли хэшируют!
                    Name = TxtName.Text
                };

                // Добавляем в таблицу и сохраняем изменения
                Core.DB.Users.Add(newUser);
                Core.DB.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно!");

                // Возвращаемся на страницу входа
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}