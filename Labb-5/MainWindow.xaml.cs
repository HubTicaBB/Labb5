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
        static List<User> adminList = new List<User>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnableAddButton()
        {
            addUserButton.IsEnabled = (nameBox.Text != "" && emailBox.Text != "" && userListBox.SelectedItem == null);
        }

        private void EnableChangeButton()
        {
            changeUserButton.IsEnabled = userListBox.SelectedItem != null;
        }

        private void EnableDeleteButton()
        {
            deleteUserButton.IsEnabled = userListBox.SelectedItem != null;
        }

        private void EnableChangeStatusButton()
        {
            changeStatusButton.IsEnabled = userListBox.SelectedItem != null;
        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableAddOrChangeButton();
        }

        private void emailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableAddOrChangeButton();
        }

        private void EnableAddOrChangeButton()
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

        private void RefreshUserListBox()
        {
            userListBox.ItemsSource = userList;
            userListBox.Items.Refresh();
        }

        private void ClearTextBoxes()
        {
            nameBox.Text = "";
            emailBox.Text = "";
        }

        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(nameBox.Text, emailBox.Text);
            userList.Add(user);
            ClearTextBoxes();
            RefreshUserListBox();            
        }

        private void userListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableChangeButton();
            EnableDeleteButton();
            EnableChangeStatusButton();
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
            changeStatusButton.IsEnabled = false;
            nameBox.ToolTip = "Enter new name";
            emailBox.ToolTip = "Enter new email";
            if (nameBox.Text != "" && emailBox.Text != "")
            {
                EnterNewUserData();
                nameBox.ToolTip = null;
                emailBox.ToolTip = null;
                userListBox.SelectedItem = null;
                userInfoLabel.Content = null;
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
                    ClearTextBoxes();
                    RefreshUserListBox();
                }
            }
        }

        private void deleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i] == userListBox.SelectedItem)
                {
                    userList.Remove(userList[i]);
                    userInfoLabel.Content = null;
                    RefreshUserListBox();
                }
            }
        }
    }
}
