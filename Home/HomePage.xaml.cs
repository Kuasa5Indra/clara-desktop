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
using Velacro.UIElements.Basic;

namespace CLARA_Desktop.Home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : MyPage
    {
        public HomePage()
        {
            InitializeComponent();
            setController(new HomePageController(this));
            GetReservation();
        }

        public void GetReservation()
        {
            getController().callMethod("CountWaitingReservation");
            getController().callMethod("CountOnReserveReservation");
            getController().callMethod("CountReturnedReservation");
            getController().callMethod("LoadRecentReservation");
        }

        public void SetWaitingReservationLabel(int count)
        {
            waiting_txtCount.Content = count;
        }

        public void SetOnReserveReservationLabel(int count)
        {
            reserve_txtCount.Content = count;
        }
        public void SetReturnedReservationLabel(int count)
        {
            return_txtCount.Content = count;
        }

        public void SetRecentReservationListView(List<Model.Reservation> reservations)
        {
            recent_reservation_Lv.ItemsSource = reservations;
        }
    }
}
