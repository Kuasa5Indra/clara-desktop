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
using Velacro.UIElements.TextBox;

namespace CLARA_Desktop.Reservation
{
    /// <summary>
    /// Interaction logic for ReservationPage.xaml
    /// </summary>
    public partial class ReservationPage : MyPage
    {

        private BuilderDataGrid builderDataGrid;
        private BuilderButton builderButton;
        private BuilderTextBox builderTextBox;
        private IMyDataGrid reservationDataGrid;
        private IMyButton previousButton;
        private IMyButton nextButton;
        private IMyButton searchButton;
        private IMyTextBox searchTextBox;
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
            builderTextBox = new BuilderTextBox();
        }
        private void InitUIElements()
        {
            reservationDataGrid = builderDataGrid.activate(this, "reservationsGrid");
            previousButton = builderButton.activate(this, "previous_page_button").addOnClick(this, "MoveToPreviousPage");
            nextButton = builderButton.activate(this, "next_page_button").addOnClick(this, "MoveToNextPage");
            searchButton = builderButton.activate(this, "search_button").addOnClick(this, "GetReservationAsset");
            searchTextBox = builderTextBox.activate(this, "search_txtBox");
        }

        public void GetReservation()
        {
            getController().callMethod("GetReservationsList");
        }

        public void GetReservationAsset()
        {
            string name = searchTextBox.getText();
            getController().callMethod("SearchReservation", name);
        }
        public void UpdateGrid(List<Model.Reservation> listReservation, int currentPage, int lastPage)
        {
            /*MyList<string> header = new MyList<string>() { "Description", "Begin", "End", "User", "Asset", "Status" };
            MyList<string> propertyNames = new MyList<string>() { "Description", "Date_begin", "Date_end", "User.Full_name", "Asset.Name", "Status" };*/
            this.Dispatcher.Invoke(() =>
            {
                reservationsGrid.ItemsSource = listReservation;
                /*reservationDataGrid.setColumnDataBinding<Model.Reservation>(header, propertyNames, listReservation);*/
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

        private void previous_page_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
