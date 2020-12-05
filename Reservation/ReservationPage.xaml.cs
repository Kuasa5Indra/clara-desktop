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
        private IMyDataGrid reservationDataGrid;

        public ReservationPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            initUIBuilders();
            initUIElements();
            setController(new ReservationController(this));
            //getController().callMethod("getReservationsList");
        }

        private void initUIBuilders()
        {
            builderDataGrid = new BuilderDataGrid();
        }
        private void initUIElements()
        {
            reservationDataGrid = builderDataGrid.activate(this, "reservationsGrid");
        }
        public void updateGrid(MyList<Model.Reservation> listReservation)
        {
            MyList<string> header = new MyList<string>() { "description", "begin", "end", "user", "asset", "status" };
            MyList<string> propertyNames = new MyList<string>() { "description", "begin", "end", "user.full_name", "asset.name", "status" };
            this.Dispatcher.Invoke(() =>
            {
                reservationDataGrid.setColumnDataBinding<Model.Reservation>(header, propertyNames, listReservation);
            });
            
        }
    }
}
