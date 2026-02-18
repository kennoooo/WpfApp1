using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class SessionPage : Page
    {
        Sessions _session;
        int _selectedSeat = -1;

        public SessionPage(Sessions session)
        {
            InitializeComponent();
            _session = session;
            LoadSeats();
        }

        private void LoadSeats()
        {
            SeatsContainer.Children.Clear();
            // Получаем список занятых мест (номера)
            var takenSeats = Core.DB.Tickets.Where(t => t.SessionId == _session.Id).Select(t => t.SeatNumber).ToList();

            // Создаем 50 мест (5 рядов по 10)
            for (int i = 1; i <= 50; i++)
            {
                Button btn = new Button();
                btn.Content = i.ToString();
                btn.Margin = new Thickness(2);
                btn.Tag = i; // Храним номер места в Tag
                btn.Click += Seat_Click;

                if (takenSeats.Contains(i))
                {
                    btn.IsEnabled = false;
                    btn.Background = Brushes.Red; // Занято
                }
                else
                {
                    btn.Background = Brushes.LightGreen; // Свободно
                }

                SeatsContainer.Children.Add(btn);
            }
        }

        private void Seat_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            _selectedSeat = (int)btn.Tag;
            TxtSelectedPlace.Text = $"Выбрано место: {_selectedSeat}";
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            _selectedSeat = -1;
            TxtSelectedPlace.Text = "";
        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSeat == -1)
            {
                MessageBox.Show("Выберите место!");
                return;
            }
            // Переход на страницу оформления
            NavigationService.Navigate(new TicketPage(_session, _selectedSeat));
        }
    }
}