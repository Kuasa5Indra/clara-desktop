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
using Velacro.LocalFile;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;

namespace CLARA_Desktop.Asset
{
    /// <summary>
    /// Interaction logic for AssetDetailPage.xaml
    /// </summary>
    public partial class AssetDetailPage : MyPage
    {
        private Model.Asset asset;
        private BuilderButton builderButton;
        private BuilderTextBox builderTextBox;
        private IMyButton uploadImageButton;
        private IMyButton deleteButton;
        private IMyButton updateButton;
        private IMyTextBox assetTextBox;
        private IMyTextBox quantityTextBox;
        private MyFile myFile;
        public AssetDetailPage(Model.Asset asset)
        {
            InitializeComponent();
            this.asset = asset;
            setController(new AssetController(this));
            InitUIBuilders();
            InitUIElements();
            ShowAsset();
        }

        private void InitUIBuilders()
        {
            builderButton = new BuilderButton();
            builderTextBox = new BuilderTextBox();
        }

        private void InitUIElements()
        {
            uploadImageButton = builderButton.activate(this, "upload_button").addOnClick(this, "SetAssetImage");
            deleteButton = builderButton.activate(this, "delete_button").addOnClick(this, "OnClickDeleteButton");
            updateButton = builderButton.activate(this, "update_button").addOnClick(this, "OnClickUpdateButton");
            assetTextBox = builderTextBox.activate(this, "name_txtBox");
            quantityTextBox = builderTextBox.activate(this, "quantity_txtBox");
        }

        public void ShowAsset()
        {
            assetTextBox.setText(asset.Name);
            quantityTextBox.setText(asset.Quantity.ToString());
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(asset.Image);
            image.EndInit();
            asset_image.Source = image;
        }

        public void SetAssetImage()
        {
            OpenFile openFile = new OpenFile();
            myFile = openFile.openFile(false)[0];
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(myFile.fullPath);
            image.EndInit();
            asset_image.Source = image;
        }

        public void OnClickUpdateButton()
        {
            string name = assetTextBox.getText();
            string quantity = quantityTextBox.getText();
            Model.Asset newAsset = new Model.Asset();
            newAsset.Id = asset.Id;
            newAsset.Name = name;
            newAsset.Quantity = Int32.Parse(quantity);
            getController().callMethod("UpdateAsset", newAsset, myFile);
        }

        public void OnClickDeleteButton()
        {
            getController().callMethod("DeleteAsset", asset.Id);
        }

        public void RouteToAssetPage()
        {
            this.NavigationService.Navigate(new AssetPage());
        }
    }
}
