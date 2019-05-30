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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConferenceManagementSystem
{
    /// <summary>
    /// Interaction logic for SectionsPage.xaml
    /// </summary>
    public partial class SectionsPage : Page
    {
        CMSController controller;
        User user;
        List<Section> sections { get; set; }
        Conference conf;

        public SectionsPage(Conference conf, CMSController controller, User user)
        {
            this.conf = conf;
            this.user = user;
            this.controller = controller;
            InitializeComponent();
            if (conf == null)
            {
                throw new Exception("No conference selected!");
            } 
            sections = this.controller.getSections(conf.ID);
            DataContext = this;
            sectionsListView.ItemsSource = sections;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string room = roomTextBox.Text;
            DateTime date = Convert.ToDateTime(confDate.Text);
            int chairId = Int32.Parse(chairTextBox.Text);
            controller.addSection(name, room, date, conf.ID, chairId);
            sections = controller.getSections(conf.ID);
            sectionsListView.ItemsSource = sections;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Section s = (Section)sectionsListView.SelectedItems[0];
            controller.deleteSection(s.ID);
            sections = controller.getSections(conf.ID);
            sectionsListView.ItemsSource = sections;
        }
    }
}
