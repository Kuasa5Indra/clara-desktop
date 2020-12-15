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
using Velacro.UIElements.TextBlock;
using CLARA_Desktop.Login;
using CLARA_Desktop.Home;
using CLARA_Desktop.Asset;
using CLARA_Desktop.Reservation;

namespace CLARA_Desktop.Dashboard
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : MyPage
    {
        private BuilderTextBlock builderTextBlock;
        private IMyTextBlock logoutTextBlock;
        private IMyTextBlock homeTextBlock;
        private IMyTextBlock assetTextBlock;
        private IMyTextBlock reservationTextBlock;

        public DashboardPage()
        {
            InitializeComponent();
            setController(new DashboardController(this));
            InitUIBuilders();
            InitUIElements();
            dashboardFrame.Navigate(new HomePage());
        }

        private void InitUIBuilders()
        {
            builderTextBlock = new BuilderTextBlock();
        }

        private void InitUIElements()
        {
            logoutTextBlock = builderTextBlock.activate(this, "logout_txtBlock").addOnPreviewMouseDown(getController(), "Logout");
            homeTextBlock = builderTextBlock.activate(this, "home_txtBlock").addOnPreviewMouseDown(this, "RouteToHomePage");
            assetTextBlock = builderTextBlock.activate(this, "asset_txtBlock").addOnPreviewMouseDown(this, "RouteToAssetPage");
            reservationTextBlock = builderTextBlock.activate(this, "reservation_txtBlock").addOnPreviewMouseDown(this, "RouteToReservationPage");
        }

        public void OnClickLogout()
        {
            this.NavigationService.Navigate(new LoginPage());
        }

        public void RouteToHomePage()
        {
            dashboardFrame.Navigate(new HomePage());
        }

        public void RouteToAssetPage()
        {
            dashboardFrame.Navigate(new AssetPage());
        }

        public void RouteToReservationPage()
        {
            dashboardFrame.Navigate(new ReservationPage());
        }
    }
}
