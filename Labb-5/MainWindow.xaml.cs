using System;
using System.Collections.Generic;
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
            changeUserButton.IsEnabled = userListBox.SelectedItem != null || adminListBox.SelectedItem != null; /////
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
            if (userListBox.SelectedItem == null && adminListBox.SelectedItem == null) /////
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
            if (userListBox.SelectedItem == null && adminListBox.SelectedItem == null) //////
            {
                EnableAddButton();
            }
            else
            {
                EnableChangeButton();
            }
        }

        private bool IsNameValid(string text)
        {
            if (Regex.IsMatch(text, @"^([a-zA-ZÄäÖöÅå]{1,}\s[a-zA-zÄäÖöÅå]{1,}'?-?[a-zA-ZÄäÖöÅå]{1,}\s?([a-zA-ZÄäÖöÅå]{1,})?)"))
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

        private bool IsStringValidEmail(string text)
        {
            if (Regex.IsMatch(text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"))
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

        private bool EmailIsUnique()
        {
            List<User> allUsers = new List<User>();
            allUsers.AddRange(userList);
            allUsers.AddRange(adminList);

            foreach (var user in allUsers)
            {
                if (user.Email == emailBox.Text)
                {
                    MessageBox.Show($"E-Mail address \"{emailBox.Text}\" already registered.\nChoose another E-Mail address.", "E-Mail already in use",
                                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    emailBox.Text = "";
                    emailBox.Focus();
                    return false;
                }
            }
            return true;
        }

        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsNameValid(nameBox.Text) && IsStringValidEmail(emailBox.Text) && EmailIsUnique())
            {
                MessageBoxResult answer = MessageBox.Show($"Are you sure you want to add the following user to the User List?\n\n" +
                           $"    {"Name: ",-10} {nameBox.Text}\n" +
                           $"    {"E-Mail: ",-10}{emailBox.Text}\n",
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
            addUserButton.IsEnabled = true;
            
            foreach (var user in userList)
            {
                if (user == userListBox.SelectedItem)
                {
                    userInfoLabel.Content = $"{"User name:",11}    {user.Name,-30}\n" +
                                            $"{"E-Mail:",15}    {user.Email,-30}\n" +
                                            $"{"Status:",15}    {"User",-30}";
                }
            }
        }

        private void changeUserButton_Click(object sender, RoutedEventArgs e)
        {
            deleteUserButton.IsEnabled = false;
            moveUserToAdmin.IsEnabled = false;
            moveAdminToUser.IsEnabled = false;
            addUserButton.IsEnabled = false;

            nameLabel.Content = "New First and Last Name";
            eMailLabel.Content = "New E-mail";
            nameBox.ToolTip = "Enter new name";
            emailBox.ToolTip = "Enter new email";
            if (nameBox.Text != "" && emailBox.Text != "" && IsStringValidEmail(emailBox.Text) && IsNameValid(nameBox.Text))
            {
                MessageBoxResult answer = MessageBox.Show($"Are you sure you want to change the following user?\n\n" +
                            $"{userInfoLabel.Content}",
                           "Change User Information",
                           MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (answer == MessageBoxResult.Yes)
                    if (userListBox.SelectedItem != null)
                    {
                        EnterNewUserData(userList, userListBox);

                    }
                    else if (adminListBox.SelectedItem != null)
                    {
                        EnterNewUserData(adminList, adminListBox);

                    }
                nameBox.ToolTip = null;
                emailBox.ToolTip = null;
                userListBox.SelectedItem = null;
                adminListBox.SelectedItem = null;
                userInfoLabel.Content = null;
                nameLabel.Content = "    Name";
                eMailLabel.Content = "  E-mail";
                nameBox.Text = "";
                emailBox.Text = "";
            }
        }

        private void EnterNewUserData(List<User> list, ListBox listBox)
        {
            foreach (var item in list)
            {
                if (item == listBox.SelectedItem)
                {
                    item.Name = nameBox.Text;
                    item.Email = emailBox.Text;
                    nameBox.Text = "";
                    emailBox.Text = "";
                    listBox.ItemsSource = list;
                    listBox.Items.Refresh();
                }
            }
        }

        private void deleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show($"Are you sure you want to delete the following user from  the list?\n\n" +
                                      $"{userInfoLabel.Content}",
                                      "Delete user",
                                      MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                if (userListBox.SelectedItem != null)
                {
                    DeleteUser(userList, userListBox);
                }
                else if (adminListBox.SelectedItem != null)
                {
                    DeleteUser(adminList, adminListBox);
                }
            }
        }

        private void DeleteUser(List<User> list, ListBox listBox)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == listBox.SelectedItem)
                {
                    list.Remove(list[i]);
                    userInfoLabel.Content = null;
                    listBox.ItemsSource = list;
                    listBox.Items.Refresh();
                }
            }
        }

        private void moveUserToAdmin_Click(object sender, RoutedEventArgs e)
        {
            MoveToTheOtherList(userListBox.SelectedItem, userList, adminList, adminListBox);
            userInfoLabel.Content = "";
        }

        private void MoveToTheOtherList(Object user, List<User> currentList, List<User> newList, ListBox newListBox)
        {
            MessageBoxResult answer = MessageBox.Show($"Are you sure you want to change status for the following user?\n\n" +
                                      $"{userInfoLabel.Content}",
                                      "Change status",
                                      MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                for (int i = 0; i < currentList.Count; i++)
                {
                    if (currentList[i] == user)
                    {
                        newList.Add(currentList[i]);
                        newListBox.ItemsSource = newList;
                        currentList.RemoveAt(i);
                        moveAdminToUser.IsEnabled = false;
                        moveUserToAdmin.IsEnabled = false;
                        userListBox.Items.Refresh();
                        adminListBox.Items.Refresh();
                    }
                }
            }
        }

        private void adminListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableMoveAdminToUserButton();
            deleteUserButton.IsEnabled = true;
            changeUserButton.IsEnabled = true;
            addUserButton.IsEnabled = true;
            foreach (var admin in adminList)
            {
                if (admin == adminListBox.SelectedItem)
                {
                    userInfoLabel.Content = $"{"User name:",11}    {admin.Name,-30}\n" +
                                            $"{"E-Mail:",15}    {admin.Email,-30}\n" +
                                            $"{"Status:",15}    {"Admin",-30}";
                }
            }
        }

        private void moveAdminToUser_Click(object sender, RoutedEventArgs e)
        {
            MoveToTheOtherList(adminListBox.SelectedItem, adminList, userList, userListBox);
            userInfoLabel.Content = "";
        }
    }
}
