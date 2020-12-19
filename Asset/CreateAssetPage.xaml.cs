﻿using System;
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
    /// Interaction logic for CreateAssetPage.xaml
    /// </summary>
    public partial class CreateAssetPage : MyPage
    {
        private BuilderButton builderButton;
        private BuilderTextBox builderTextBox;
        private IMyButton uploadImageButton;
        private IMyButton cancelButton;
        private IMyButton createButton;
        private IMyTextBox assetTextBox;
        private IMyTextBox quantityTextBox;
        private MyFile myFile;
        public CreateAssetPage()
        {
            InitializeComponent();
            setController(new AssetController(this));
            InitUIBuilders();
            InitUIElements();
        }

        private void InitUIBuilders()
        {
            builderButton = new BuilderButton();
            builderTextBox = new BuilderTextBox();
        }

        private void InitUIElements()
        {
            uploadImageButton = builderButton.activate(this, "upload_button").addOnClick(this, "SetAssetImage");
            cancelButton = builderButton.activate(this, "cancel_button").addOnClick(this, "RouteToAssetPage");
            createButton = builderButton.activate(this, "create_button").addOnClick(this, "OnClickCreateButton");
            assetTextBox = builderTextBox.activate(this, "name_txtBox");
            quantityTextBox = builderTextBox.activate(this, "quantity_txtBox");
        }

        public void OnClickCreateButton()
        {
            string assetName = assetTextBox.getText();
            string quantity = quantityTextBox.getText();
            getController().callMethod("CreateAsset", assetName, quantity, myFile);
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

        public void RouteToAssetPage()
        {
            this.NavigationService.Navigate(new AssetPage());
        }
    }
}