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
    /// Логика взаимодействия для MovieDetailsPage.xaml
    /// </summary>
    public partial class MovieDetailsPage : Page
    {
        Movies _movie;
        public MovieDetailsPage(Movies movie)
        {
            InitializeComponent();
            _movie = movie;
            DataContext = _movie; // Привязка к полям (упрощенная)

            TxtTitle.Text = _movie.Title;
            TxtDesc.Text = _movie.Description;
            TxtRating.Text = _movie.Rating.ToString();

            // Загружаем сеансы только этого фильма
            SessionsList.ItemsSource = Core.DB.Sessions.Where(s => s.MovieId == _movie.Id).ToList();
        }

        private void SessionsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Core.CurrentUser == null)
            {
                MessageBox.Show("Для выбора места войдите в систему!");
                NavigationService.Navigate(new LoginPage());
                return;
            }

            var session = SessionsList.SelectedItem as Sessions;
            if (session != null)
                NavigationService.Navigate(new SessionPage(session));
        }

        private void Back_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
    }
}
