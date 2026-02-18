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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            TxtName.Text = $"Привет, {Core.CurrentUser.Name}!";

            // Загружаем билеты текущего пользователя
            GridTickets.ItemsSource = Core.DB.Tickets
                                        .Where(t => t.UserId == Core.CurrentUser.Id)
                                        .ToList();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Core.CurrentUser = null;
            NavigationService.Navigate(new MainPage());
        }
    }
}
