﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        static List<User> adminList = new List<User>();
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

        private void EnableMoveUserToAdminButton()
        {
            moveUserToAdmin.IsEnabled = userListBox.SelectedItem != null;
        }
        private void EnableMoveAdminToUserButton()
        {
            moveAdminToUser.IsEnabled = adminListBox.SelectedItem != null;
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

        private bool NameIsValid()
        {
            if (Regex.IsMatch(nameBox.Text, @"^([a-zA-ZÄäÖöÅå]{1,}\s[a-zA-zÄäÖöÅå]{1,}'?-?[a-zA-ZÄäÖöÅå]{1,}\s?([a-zA-ZÄäÖöÅå]{1,})?)"))
            {
                return true;
            }
            else
            {
                MessageBox.Show($"The user name you entered is not valid.\n" +
                                $"Please enter first and last name",
                                "Invalid user name", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                nameBox.Text = "";
                nameBox.Focus();
                return false;
            }
        }


        private bool EmailIsValid()
        {
            if (Regex.IsMatch(emailBox.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"))
            {
                return true;
            }
            else
            {
                MessageBox.Show($"The e-mail address you entered is not valid.\n" +
                                $"Please try again.",
                                "Invalid e-mail address", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                emailBox.Text = "";
                emailBox.Focus();
                return false;
            }
        }
        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {

            if (NameIsValid() && EmailIsValid())
            {
                MessageBoxResult answer = MessageBox.Show($"Are you sure you want to add the following user to the User List?\n\n" +
                           $"    {"Name: ",-10}{nameBox.Text}\n" +
                           $"    {"E-Mail: ",-10}  {emailBox.Text}\n",
                           "Add New User",
                           MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (answer == MessageBoxResult.Yes)
                {
                    User user = new User(nameBox.Text, emailBox.Text);
                    userList.Add(user);
                    userListBox.ItemsSource = userList;
                    userListBox.Items.Refresh();
                }
                nameBox.Text = "";
                emailBox.Text = "";
            }
        }

        private void userListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableChangeButton();
            EnableDeleteButton();
            EnableMoveUserToAdminButton();


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
            moveUserToAdmin.IsEnabled = false;
            nameLabel.Content = "New Name";
            eMailLabel.Content = "New E-mail";

            nameBox.ToolTip = "Enter new name";
            emailBox.ToolTip = "Enter new email";
            if (nameBox.Text != "" && emailBox.Text != "")
            {
                MessageBoxResult answer = MessageBox.Show($"Are you sure you want to change the following user in the User List ?\n\n" +
                            $"{userInfoLabel.Content}",
                           "Change User Information",
                           MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (answer == MessageBoxResult.Yes) EnterNewUserData();
                nameBox.ToolTip = null;
                emailBox.ToolTip = null;
                userListBox.SelectedItem = null;
                userInfoLabel.Content = null;
                nameLabel.Content = "    Name";
                eMailLabel.Content = "  E-mail";
                nameBox.Text = "";
                emailBox.Text = "";
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

        private void deleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show($"Are you sure you want to delete the following user from  the User List?\n\n" +
              $"{userInfoLabel.Content}",
              "Delete user",
              MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    if (userList[i] == userListBox.SelectedItem)
                    {
                        userList.Remove(userList[i]);
                        userInfoLabel.Content = null;
                        userListBox.ItemsSource = userList;
                        userListBox.Items.Refresh();
                    }
                }
            }
        }

        private void moveUserToAdmin_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i] == userListBox.SelectedItem)
                {
                    adminList.Add(userList[i]);
                    adminListBox.ItemsSource = adminList;
                    userList.RemoveAt(i);
                    moveAdminToUser.IsEnabled = false;
                    moveUserToAdmin.IsEnabled = false;
                    userListBox.Items.Refresh();
                    adminListBox.Items.Refresh();
                }
            }
        }

        private void adminListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableMoveAdminToUserButton();
            foreach (var user in adminList)
            {
                if (user == adminListBox.SelectedItem)
                {
                    userInfoLabel.Content = $"User name\t {user.Name,30}\nE-Mail\t\t {user.Email,30}";
                }
            }
        }

        private void moveAdminToUser_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < adminList.Count; i++)
            {
                if (adminList[i] == adminListBox.SelectedItem)
                {
                    userList.Add(adminList[i]);
                    userListBox.ItemsSource = userList;
                    adminList.RemoveAt(i);
                    moveAdminToUser.IsEnabled = false;
                    moveUserToAdmin.IsEnabled = false;
                    userListBox.Items.Refresh();
                    adminListBox.Items.Refresh();
                }
            }

        }
    }
}
