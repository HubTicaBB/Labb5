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

        private void EnableButtons()
        {
            addUserButton.IsEnabled = (nameBox.Text != "" && emailBox.Text != "");
            editUserButton.IsEnabled = (nameBox.Text != "" && emailBox.Text != "");
            deleteUserButton.IsEnabled = (nameBox.Text != "" && emailBox.Text != "");
        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButtons();
        }

        private void emailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButtons();
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
            foreach (var item in userList)
            {
                if (item == userListBox.SelectedItem)
                {
                    userInfoLabel.Content = $"User name\t {item.Name,30}\nE-Mail\t\t {item.Email,30}";
                }
            }
        }
    }
}
