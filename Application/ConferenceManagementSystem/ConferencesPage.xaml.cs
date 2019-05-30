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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConferenceManagementSystem
{
    /// <summary>
    /// Interaction logic for ConferencesPage.xaml
    /// </summary>
    /// 

    public partial class ConferencesPage : Page
    {
        private CMSController controller;
        private User user;
        public List<Conference> conferences { get; set; }

        public ConferencesPage(CMSController controller, User user)
        {
            this.user = user;
            this.controller = controller;
            InitializeComponent();
            conferences = this.controller.getConferences();
            DataContext = this;

        }

        private void BntAttend_Click(object sender, RoutedEventArgs e)
        {
            if (conferencesListView.SelectedItems.Count > 0)
            {
                try
                {
                    Conference conference = (Conference)conferencesListView.SelectedItems[0];
                    controller.attendConference(conference, user);
                    MessageBox.Show("Success!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No conference is selected!");
            }
            
        }
    }
}
