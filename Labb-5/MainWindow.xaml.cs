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

namespace Labb_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
    }
}
