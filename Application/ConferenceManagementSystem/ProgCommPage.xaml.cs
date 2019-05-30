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
    /// Interaction logic for ProgCommPage.xaml
    /// </summary>
    public partial class ProgCommPage : Page
    {
        private CMSController controller;
        private User user;
        private List<ChosenPcMember> pcs { get; set; }

        public ProgCommPage(CMSController controller, User user)
        {
            this.user = user;
            this.controller = controller;
            InitializeComponent();
            pcs = this.controller.getChosen();
            DataContext = this;
            pcListView.ItemsSource = pcs;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            controller.addChosen(nameTextBox.Text, roleComboBox.Text);
            pcs = this.controller.getChosen();
            pcListView.ItemsSource = pcs;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ChosenPcMember cpm = (ChosenPcMember)pcListView.SelectedItems[0];
            controller.deleteChosen(cpm.email);
            pcs = this.controller.getChosen();
            pcListView.ItemsSource = pcs;
        }
    }
}
