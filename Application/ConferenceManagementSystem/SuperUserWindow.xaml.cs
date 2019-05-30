using ConferenceManagementSystem.Controller;
using ConferenceManagementSystem.Domain;
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
using System.Windows.Shapes;

namespace ConferenceManagementSystem
{
    /// <summary>
    /// Interaction logic for SuperUserWindow.xaml
    /// </summary>
    public partial class SuperUserWindow : Window
    {
        private CMSController controller;
        private User user;
        private Conference selectedConference = null;

        public SuperUserWindow(CMSController controller, User user)
        {
            this.user = user;
            this.controller = controller;
            InitializeComponent();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public Conference getConference()
        {
            return this.selectedConference;
        }

        public void setConference(Conference conf)
        {
            this.selectedConference = conf;
            conferenceLabel.Content = conf.ConferenceName;
        }

        private void ProfileItem_Selected(object sender, RoutedEventArgs e)
        {
            contentFrame.Content = new ProfilePage(controller, user);
        }

        private void CommitteeItem_Selected(object sender, RoutedEventArgs e)
        {
            contentFrame.Content = new ProgCommPage(controller, user);
        }

        private void SectionsItem_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                contentFrame.Content = new SectionsPage(this.selectedConference, controller, user);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void ConferencesItem_Selected(object sender, RoutedEventArgs e)
        {
            contentFrame.Content = new ConfManagementPage(this, controller, user);
        }

    }
}
