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
using Velacro.UIElements.Button;

namespace CLARA_Desktop.Asset
{
    /// <summary>
    /// Interaction logic for AssetPage.xaml
    /// </summary>
    public partial class AssetPage : MyPage
    {
        private BuilderButton builderButton;
        private IMyButton createAssetButton;
        private IMyButton previousPageButton;
        private IMyButton nextPageButton;
        private int currentPage;

        public AssetPage()
        {
            InitializeComponent();
            setController(new AssetController(this));
            InitUIBuilders();
            InitUIElements();
            GetAssets();
        }

        private void InitUIBuilders()
        {
            builderButton = new BuilderButton();
        }

        private void InitUIElements()
        {
            createAssetButton = builderButton.activate(this, "createAssetBtn").addOnClick(this, "RouteToCreateAssetPage");
            previousPageButton = builderButton.activate(this, "previous_page_button").addOnClick(this, "MoveToPreviousPage");
            nextPageButton = builderButton.activate(this, "next_page_button").addOnClick(this, "MoveToNextPage");
        }

        public void GetAssets()
        {
            getController().callMethod("LoadAsset");
        }

        public void SetAssetListView(List<Model.Asset> assets, int currentPage, int lastPage)
        {
            asset_listview.ItemsSource = assets;
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
        }

        public void MoveToPreviousPage()
        {
            currentPage -= 1;
            getController().callMethod("LoadAssetPage", currentPage);
        }

        public void MoveToNextPage()
        {
            currentPage += 1;
            getController().callMethod("LoadAssetPage", currentPage);
        }

        public void RouteToCreateAssetPage()
        {
            this.NavigationService.Navigate(new CreateAssetPage());
        }
    }
}
