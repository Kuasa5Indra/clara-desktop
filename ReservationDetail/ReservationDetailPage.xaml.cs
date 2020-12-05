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

namespace CLARA_Desktop.ReservationDetail
{
    /// <summary>
    /// Interaction logic for ReservationDetailPage.xaml
    /// </summary>
    public partial class ReservationDetailPage : MyPage
    {
        private BuilderTextBlock builderTextBlock;
        private IMyTextBlock beginTextBlock;
        private IMyTextBlock endTextBlock;
        private IMyTextBlock reserveeTextBlock;
        private IMyTextBlock classroomTextBlock;
        private IMyTextBlock nrpTextBlock;
        private IMyTextBlock assetNameTextBlock;
        private BuilderDataGrid builderDataGrid;
        private IMyDataGrid statusDataGrid;
        private Image assetImageImage;

        public ReservationDetailPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            initUIBuilders();
            initUIElements();
            setController(new ReservationDetailController(this));
            
        }

        private void initUIBuilders()
        {
            builderDataGrid = new BuilderDataGrid();
            builderTextBlock = new BuilderTextBlock();
        }
        private void initUIElements()
        {
            statusDataGrid = builderDataGrid.activate(this, "statusGrid");
            beginTextBlock = builderTextBlock.activate(this,"begin");
            endTextBlock = builderTextBlock.activate(this, "end");
            reserveeTextBlock = builderTextBlock.activate(this, "reservee");
            classroomTextBlock = builderTextBlock.activate(this, "classroom");
            nrpTextBlock = builderTextBlock.activate(this, "nrp");
            assetNameTextBlock = builderTextBlock.activate(this, "assetName");
            assetImageImage = (Image) this.FindName("imageAsset");

        }

        public void updateStatusGrid(Model.Reservation reservation)
        {
            MyList<string> header = new MyList<string>() { "status","datetime" };
            MyList<string> propertyNames = new MyList<string>() { "Status", "Datetime"};
            this.Dispatcher.Invoke(() =>
            {
                Debug.WriteLine(reservation);
                beginTextBlock.setText(reservation.Begin);
                endTextBlock.setText(reservation.End);
                reserveeTextBlock.setText(reservation.User.Full_Name);
                classroomTextBlock.setText(reservation.User.Class);
                nrpTextBlock.setText(reservation.User.Nrp);
                assetNameTextBlock.setText(reservation.Asset.Name);
                statusDataGrid.setColumnDataBinding<Model.History>(header, propertyNames, (reservation.History));
                assetImageImage.Source = new BitmapImage(new Uri("https://api.clara-app.tech/assets/"+reservation.Asset.Image));
            });
        }

    }
}
