using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Velacro.UIElements.TextBlock;
using CLARA_Desktop.Routes;
using System.Net;

namespace CLARA_Desktop.Reservation
{
    /// <summary>
    /// Interaction logic for ReservationDetailPage.xaml
    /// </summary>
    public partial class ReservationDetailPage : MyPage
    {
        private Model.Reservation reservation;
        private BuilderTextBlock builderTextBlock;
        private IMyTextBlock beginTextBlock;
        private IMyTextBlock endTextBlock;
        private IMyTextBlock reserveeTextBlock;
        private IMyTextBlock classroomTextBlock;
        private IMyTextBlock nrpTextBlock;
        private IMyTextBlock assetNameTextBlock;
        private BuilderDataGrid builderDataGrid;
        private IMyDataGrid statusDataGrid;
        private Image assetImage;

        public ReservationDetailPage(Model.Reservation reservation)
        {
            InitializeComponent();
            this.reservation = reservation;
            this.KeepAlive = true;
            InitUIBuilders();
            InitUIElements();
            setController(new ReservationController(this));
            UpdateStatusGrid();
        }

        private void InitUIBuilders()
        {
            builderDataGrid = new BuilderDataGrid();
            builderTextBlock = new BuilderTextBlock();
        }
        private void InitUIElements()
        {
            statusDataGrid = builderDataGrid.activate(this, "statusGrid");
            beginTextBlock = builderTextBlock.activate(this,"begin");
            endTextBlock = builderTextBlock.activate(this, "end");
            reserveeTextBlock = builderTextBlock.activate(this, "reservee");
            classroomTextBlock = builderTextBlock.activate(this, "classroom");
            nrpTextBlock = builderTextBlock.activate(this, "nrp");
            assetNameTextBlock = builderTextBlock.activate(this, "assetName");
            assetImage = (Image) this.FindName("imageAsset");

        }

        public void UpdateStatusGrid()
        {
            MyList<string> header = new MyList<string>() { "datetime","status","description" };
            MyList<string> propertyNames = new MyList<string>() { "Datetime", "Status",  "Description"};
            this.Dispatcher.Invoke(() =>
            {
                beginTextBlock.setText(reservation.Date_begin);
                endTextBlock.setText(reservation.Date_end);
                reserveeTextBlock.setText(reservation.User.Full_name);
                classroomTextBlock.setText(reservation.User.Grade);
                nrpTextBlock.setText(reservation.User.Nrp.ToString());
                assetNameTextBlock.setText(reservation.Asset.Name);
                statusDataGrid.setColumnDataBinding<Model.History>(header, propertyNames, (reservation.Histories));
                assetImage.Source = new BitmapImage(new Uri(String.Concat(API.image_path, reservation.Asset.Image)));
            });
        }

    }
}
