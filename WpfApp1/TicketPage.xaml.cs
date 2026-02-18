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
    /// Логика взаимодействия для TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        Sessions _session;
        int _seat;
        decimal _price = 300; 

        public TicketPage(Sessions session, int seat)
        {
            InitializeComponent();
            _session = session;
            _seat = seat;

        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Tickets newTicket = new Tickets
            {
                SessionId = _session.Id,
                UserId = Core.CurrentUser.Id,
                SeatNumber = _seat,
                Price = _price
            };

            Core.DB.Tickets.Add(newTicket);
            Core.DB.SaveChanges();

            MessageBox.Show("Билет куплен!");
            NavigationService.Navigate(new MainPage());
        }
    }
}
