using ConferenceManagementSystem.Controller;
using ConferenceManagementSystem.Entities;
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

namespace ConferenceManagementSystem
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        CMSController controller;
        User user;

        public ProfilePage(CMSController controller, User user)
        {
            this.user = user;
            this.controller = controller;
            InitializeComponent();
            loadTextBoxes();
        }

        public void loadTextBoxes()
        {
            firstNameTextBox.Text = user.FirstName;
            lastNameTextBox.Text = user.LastName;
            if (user.RoleID == 5)
            {
                affiliationLabel.Visibility = Visibility.Hidden;
                affiliationTextBox.Visibility = Visibility.Hidden;
                websiteLabel.Visibility = Visibility.Hidden;
                websiteTextBox.Visibility = Visibility.Hidden;
            }
            else if (user.RoleID == 1)
            {
                websiteLabel.Visibility = Visibility.Hidden;
                websiteTextBox.Visibility = Visibility.Hidden; 
            }
        }

        private void BtnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            if (user.RoleID == 5)
            {
                if (firstNameTextBox.Text != "" && lastNameTextBox.Text != "")
                {
                    controller.updateListener(user.ID, firstNameTextBox.Text, lastNameTextBox.Text);
                    MessageBox.Show("Success!");
                }
                else
                {
                    MessageBox.Show("Invalid first name or last name!");
                }
            }
            else if (user.RoleID == 1)
            {
                if (firstNameTextBox.Text != "" && lastNameTextBox.Text != "" && affiliationTextBox.Text != "")
                {
                    controller.updateAuthor(user.ID, firstNameTextBox.Text, lastNameTextBox.Text, affiliationTextBox.Text);
                    MessageBox.Show("Success!");
                }
                else
                {
                    MessageBox.Show("Invalid first name,last name or affiliation name!");
                }
            }
            else
            {
                if (firstNameTextBox.Text != "" && lastNameTextBox.Text != "" && affiliationTextBox.Text != "" && websiteTextBox.Text!="")
                {
                    controller.updatePCMember(user.ID, firstNameTextBox.Text, lastNameTextBox.Text, affiliationTextBox.Text,websiteTextBox.Text);
                    MessageBox.Show("Success!");
                }
                else
                {
                    MessageBox.Show("Invalid first name,last name,affiliation or website name!");
                }
            }
            
        }
    }
}
