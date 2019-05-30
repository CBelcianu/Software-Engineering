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
using System.Windows.Shapes;
using ConferenceManagementSystem.Controller;
using ConferenceManagementSystem.Entities;

namespace ConferenceManagementSystem
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private CMSController controller = new CMSController();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWin = new RegisterWindow(controller);
            regWin.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //if (usernameTextBox.Text == "")
            //    MessageBox.Show("Invalid username or password!", "Login");
            String username = usernameTextBox.Text;
            String passwd = passwordBox.Password.ToString();
            try
            {
                User user = controller.LogIN(username, passwd);
                if (user.RoleID == 6)
                {
                    SuperUserWindow SuperUserWin = new SuperUserWindow(controller, user);
                    this.Hide();
                    SuperUserWin.ShowDialog();
                    this.Show();
                }
                else
                {
                    UserMainWindow userMainWin = new UserMainWindow(controller, user);
                    this.Hide();
                    userMainWin.ShowDialog();
                    this.Show();
                }

                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
