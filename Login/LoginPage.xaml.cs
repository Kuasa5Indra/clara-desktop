﻿using System;
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
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBox;
using Velacro.UIElements.PasswordBox;
using CLARA_Desktop.Dashboard;
using System.Text.RegularExpressions;

namespace CLARA_Desktop.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderPasswordBox passBoxBuilder;
        private IMyButton loginButton;
        private IMyTextBox emailTxtBox;
        private IMyPasswordBox passwordBox;
        public LoginPage()
        {
            InitializeComponent();
            setController(new LoginController(this));
            InitUIBuilders();
            InitUIElements();
        }

        private void InitUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            passBoxBuilder = new BuilderPasswordBox();
        }

        private void InitUIElements()
        {
            loginButton = buttonBuilder.activate(this, "login_button").addOnClick(this, "OnClickLoginButton");
            emailTxtBox = txtBoxBuilder.activate(this, "email_textBox");
            passwordBox = passBoxBuilder.activate(this, "password_box");
        }

        public void OnClickLoginButton()
        {
            string email = emailTxtBox.getText();
            string password = passwordBox.getPassword();
            if(email == "" || password == "")
            {
                MessageBox.Show("Please fill all the fields", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                getController().callMethod("Login", email, password);
            }
        }

        public void RouteToDashboard()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.NavigationService.Navigate(new DashboardPage());
            });
        }

        private void email_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[a-z0-9]+@[a-z.]+[.][a-z]{2,3}").IsMatch(e.Text);
        }
    }
}
