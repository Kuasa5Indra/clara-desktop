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
using Velacro.Basic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.DataGrid;
using Velacro.UIElements.ListView;

namespace CLARA_Desktop.Reservation
{
    /// <summary>
    /// Interaction logic for ReservationPage.xaml
    /// </summary>
    public partial class ReservationPage : MyPage
    {

        private BuilderDataGrid builderDataGrid;
        private BuilderButton builderButton;
        private IMyDataGrid reservationDataGrid;
        private IMyButton previousButton;
        private IMyButton nextButton;
        private int currentPage = 1;

        public ReservationPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            InitUIBuilders();
            InitUIElements();
            setController(new ReservationController(this));
            GetReservation();
        }

        private void InitUIBuilders()
        {
            builderDataGrid = new BuilderDataGrid();
            builderButton = new BuilderButton();
        }
        private void InitUIElements()
        {
            reservationDataGrid = builderDataGrid.activate(this, "reservationsGrid");
            previousButton = builderButton.activate(this, "previous_page_button").addOnClick(this, "MoveToPreviousPage");
            nextButton = builderButton.activate(this, "next_page_button").addOnClick(this, "MoveToNextPage");
        }

        public void GetReservation()
        {
            getController().callMethod("GetReservationsList");
        }
        public void UpdateGrid(MyList<Model.Reservation> listReservation, int currentPage, int lastPage)
        {
            MyList<string> header = new MyList<string>() { "description", "begin", "end", "user", "asset", "status" };
            MyList<string> propertyNames = new MyList<string>() { "Description", "Date_begin", "Date_end", "User.Full_name", "Asset.Name", "Status" };
            this.Dispatcher.Invoke(() =>
            {
                reservationDataGrid.setColumnDataBinding<Model.Reservation>(header, propertyNames, listReservation);
                if (currentPage == 1)
                {
                    previous_page_button.Visibility = Visibility.Hidden;
                }
                else
                {
                    previous_page_button.Visibility = Visibility.Visible;
                }

                if (currentPage == lastPage)
                {
                    next_page_button.Visibility = Visibility.Hidden;
                }
                else
                {
                    next_page_button.Visibility = Visibility.Visible;
                }
            });
        }

        public void MoveToPreviousPage()
        {
            currentPage -= 1;
            reservationsGrid.UnselectAll();
            getController().callMethod("LoadReservationPage", currentPage);
        }

        public void MoveToNextPage()
        {
            currentPage += 1;
            reservationsGrid.UnselectAll();
            getController().callMethod("LoadReservationPage", currentPage);
        }

        private void reservationsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid Item = (DataGrid)sender;
            Model.Reservation reservation = (Model.Reservation)Item.SelectedItem;
            this.NavigationService.Navigate(new ReservationDetailPage(reservation));
        }
    }
}
