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
using Velacro.UIElements.Basic;
using Velacro.UIElements.TextBlock;

namespace CLARA_Desktop.Dashboard
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : MyPage
    {
        private BuilderTextBlock builderTextBlock;
        private IMyTextBlock logoutTextBlock;

        public DashboardPage()
        {
            InitializeComponent();
            setController(new DashboardController(this));
            InitUIBuilders();
            InitUIElements();
        }

        private void InitUIBuilders()
        {
            builderTextBlock = new BuilderTextBlock();
        }

        private void InitUIElements()
        {
            //logoutTextBlock = builderTextBlock.activate(this, "logout_txtBlock");
        }
    }
}
