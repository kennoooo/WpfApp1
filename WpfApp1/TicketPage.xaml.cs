using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp1
{
    public partial class TicketPage : Page
    {

        private Sessions _currentSession;
        private int _selectedSeat;
        private decimal _price = 300; 

        public TicketPage(Sessions session, int seat)
        {
            InitializeComponent();

            _currentSession = session;
            _selectedSeat = seat;

            TxtMovieTitle.Text = _currentSession.Movies.Title;
            TxtHall.Text = _currentSession.Halls.Name + " (" + _currentSession.Halls.Category + ")";
            TxtDateTime.Text = _currentSession.DateTime.Value.ToString("dd.MM.yyyy");
            TxtSeat.Text = _selectedSeat.ToString();
            TxtPrice.Text = _price.ToString();
        }


        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Tickets newTicket = new Tickets
                {
                    SessionId = _currentSession.Id,
                    UserId = Core.CurrentUser.Id,
                    SeatNumber = _selectedSeat,
                    Price = _price
                };

                Core.DB.Tickets.Add(newTicket);
                Core.DB.SaveChanges();

                MessageBox.Show("Билет успешно куплен!");

                NavigationService.Navigate(new MainPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при оформлении: " + ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}