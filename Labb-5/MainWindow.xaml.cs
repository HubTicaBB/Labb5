using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Labb_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<User> userList = new List<User>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnableAddButton()
        {
            addUserButton.IsEnabled = (nameBox.Text != "" && emailBox.Text != "");
        }

        private void EnableChangeButton()
        {
            changeUserButton.IsEnabled = userListBox.SelectedItem != null;
        }

        private void EnableDeleteButton()
        {
            deleteUserButton.IsEnabled = userListBox.SelectedItem != null;
        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userListBox.SelectedItem == null)
            {
                EnableAddButton();
            }
            else
            {
                EnableChangeButton();
            }
        }

        private void emailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userListBox.SelectedItem == null)
            {
                EnableAddButton();
            }
            else
            {
                EnableChangeButton();
            }
        }

        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(nameBox.Text, emailBox.Text);
            userList.Add(user);
            userListBox.ItemsSource = userList;
            userListBox.Items.Refresh();
            nameBox.Text = "";
            emailBox.Text = "";
        }

        private void userListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableChangeButton();
            EnableDeleteButton();
            foreach (var item in userList)
            {
                if (item == userListBox.SelectedItem)
                {
                    userInfoLabel.Content = $"User name\t {item.Name,30}\nE-Mail\t\t {item.Email,30}";
                }
            }
        }

        private void changeUserButton_Click(object sender, RoutedEventArgs e)
        {
            deleteUserButton.IsEnabled = false;
            nameBox.ToolTip = "Enter new name";
            emailBox.ToolTip = "Enter new email";

            if (nameBox.Text != "" && emailBox.Text != "")
            {                
                EnterNewUserData();
            }            
        }

        private void EnterNewUserData()
        {            
            foreach (var item in userList)
            {
                if (item == userListBox.SelectedItem)
                {
                    item.Name = nameBox.Text;                    
                    item.Email = emailBox.Text;
                    nameBox.Text = "";
                    emailBox.Text = "";                    
                    userListBox.ItemsSource = userList;
                    userListBox.Items.Refresh();
                }
            }
        }
    }
}
