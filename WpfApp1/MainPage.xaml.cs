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
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            UpdateData();
        }

        private void UpdateData()
        {
            var movies = Core.DB.Movies.ToList();

            // Поиск
            if (!string.IsNullOrWhiteSpace(SearchBox.Text))
                movies = movies.Where(m => m.Title.ToLower().Contains(SearchBox.Text.ToLower())).ToList();

            // Сортировка
            if (SortBox.SelectedIndex == 1) // По названию
                movies = movies.OrderBy(m => m.Title).ToList();
            else if (SortBox.SelectedIndex == 2) // По рейтингу
                movies = movies.OrderByDescending(m => m.Rating).ToList();

            MoviesList.ItemsSource = movies;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateData();
        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateData();

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            if (Core.CurrentUser == null)
                NavigationService.Navigate(new LoginPage());
            else
                NavigationService.Navigate(new ProfilePage());
        }

        private void MoviesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedMovie = MoviesList.SelectedItem as Movies;
            if (selectedMovie != null)
                NavigationService.Navigate(new MovieDetailsPage(selectedMovie));
        }
    }
}
