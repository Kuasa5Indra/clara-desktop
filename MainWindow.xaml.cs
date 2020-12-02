using System;
using System.IO;
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
using CLARA_Desktop.Login;
using CLARA_Desktop.Dashboard;

namespace CLARA_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : MyWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            CheckToken();
        }

        public void CheckToken()
        {
            if (!File.Exists("jwt.txt"))
            {
                mainframe.Navigate(new LoginPage());
            }
            else
            {
                mainframe.Navigate(new DashboardPage());
            }
        }
    }
}
