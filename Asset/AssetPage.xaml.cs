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
        public AssetPage()
        {
            InitializeComponent();
            setController(new AssetController(this));
            InitUIBuilders();
            InitUIElements();
        }

        private void InitUIBuilders()
        {
            builderButton = new BuilderButton();
        }

        private void InitUIElements()
        {
            createAssetButton = builderButton.activate(this, "createAssetBtn").addOnClick(this, "RouteToCreateAssetPage");
        }

        public void RouteToCreateAssetPage()
        {
            this.NavigationService.Navigate(new CreateAssetPage());
        }
    }
}
