using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1;

namespace WpfApp1
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Теперь TxtLogin и TxtPass будут видны коду
            var user = Core.DB.Users.FirstOrDefault(u => u.Login == TxtLogin.Text && u.Password == TxtPass.Password);

            if (user != null)
            {
                Core.CurrentUser = user;
                MessageBox.Show($"Добро пожаловать, {user.Name}!");
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void BtnToReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}