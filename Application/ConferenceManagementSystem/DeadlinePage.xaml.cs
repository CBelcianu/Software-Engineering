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

namespace ConferenceManagementSystem {
    /// <summary>
    /// Interaction logic for DeadlinePage.xaml
    /// </summary>
    public partial class DeadlinePage : Page {
        CMSController controller;
        User user;
        public List<Domain.Section> sections { get; set; }
        Domain.Section section;

        public DeadlinePage(CMSController controller, User user, Boolean canModify) {
            this.controller = controller;
            this.user = user;
            InitializeComponent();
            sections = controller.getSections();
            DataContext = this;
            if (!canModify) {
                dateDeadline.Visibility = Visibility.Hidden;
                submitButton.Visibility = Visibility.Hidden;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (sectionListView.SelectedItems.Count > 0) {
                section = (Domain.Section)sectionListView.SelectedItems[0];
                dateDeadline.Text = section.PaperDeadline;
            }
        }

        private void submitButton_Click(object sender, RoutedEventArgs e) {
            if (section == null)
                MessageBox.Show("Please select a section");
            else {
                DateTime initialDate = Convert.ToDateTime(section.PaperDeadline);
                DateTime newDate = Convert.ToDateTime(dateDeadline.Text);
                if (initialDate.CompareTo(newDate) >= 0)
                    MessageBox.Show("New deadline must be after current deadline.");
                else {
                    controller.updateSectionDeadline(section.ID, newDate);
                    MessageBox.Show("Updated deadline");
                    sections = controller.getSections();
                    sectionListView.ItemsSource = sections;
                }
            }
        }
    }
}
