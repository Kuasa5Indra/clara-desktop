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
using Velacro.UIElements.Button;
using Velacro.UIElements.Basic;
using Velacro.UIElements.DataGrid;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;
using CLARA_Desktop.Routes;

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
        private BuilderButton builderButton;
        private IMyButton acceptButton;
        private IMyButton denyButton;
        private BuilderTextBox builderTextBox;
        private IMyTextBox descriptionTextBox;

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
            builderButton = new BuilderButton();
            builderTextBox = new BuilderTextBox();
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
            acceptButton = builderButton.activate(this, "accept_button").addOnClick(this, "OnClickAcceptButton");
            denyButton = builderButton.activate(this, "deny_button").addOnClick(this, "OnClickDenyButton");
            descriptionTextBox = builderTextBox.activate(this, "description_txtBox");
        }

        public void UpdateStatusGrid()
        {
            MyList<string> header = new MyList<string>() { "Datetime","Status","Description" };
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
                if (reservation.Status == "On Reservation")
                {
                    deny_button.Visibility = Visibility.Hidden;
                    acceptButton.setText("Finish");
                }
                else if (reservation.Status == "Returned" || reservation.Status == "Denied")
                {
                    accept_button.Visibility = Visibility.Hidden;
                    deny_button.Visibility = Visibility.Hidden;
                }
            });
        }

        public void OnClickAcceptButton()
        {
            string description = descriptionTextBox.getText();
            if (description == "")
            {
                MessageBox.Show("Description is required", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string condition = accept_button.Content.ToString().ToLower();
                MessageBoxResult result = MessageBox.Show("Are you sure you want to " + condition + " reservation ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (accept_button.Content.Equals("Accept"))
                        {
                            getController().callMethod("UpdateStatusReservation", reservation.Id, "On Reservation", description);
                        }
                        else
                        {
                            getController().callMethod("UpdateStatusReservation", reservation.Id, "Returned", description);
                        }
                        break;
                }
            }
        }

        public void OnClickDenyButton()
        {
            string description = descriptionTextBox.getText();
            if (description == "")
            {
                MessageBox.Show("Description is required", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to deny reservation ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        getController().callMethod("UpdateStatusReservation", reservation.Id, "Denied", description);
                        break;
                }
            }
        }

        public void GetUpdatedReservation(Model.Reservation reservation)
        {
            this.reservation = reservation;
            UpdateStatusGrid();
        }

    }
}
