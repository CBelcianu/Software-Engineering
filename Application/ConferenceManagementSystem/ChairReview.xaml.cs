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
    /// Interaction logic for ChairReview.xaml
    /// </summary>
    public partial class ChairReview : Page
    {
        CMSController controller;
        User user;
        public List<Paper> papers { get; set; }
        public List<Review> reviews { get; set; }

        public ChairReview(CMSController con, User u)
        {
            this.controller = con;
            this.user = u;
            InitializeComponent();
            DataContext = this;
            loadPapers();
        }

        private void loadPapers()
        {
            papers = controller.getPapers();
            papersListView.ItemsSource = papers;
        }

        private void PapersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Paper paper = (Paper)this.papersListView.SelectedItems[0];
            reviews = controller.GetReviewsForPaper(paper);
            reviewListView.ItemsSource = reviews;
        }

        private void ReevalButton_Click(object sender, RoutedEventArgs e)
        {
            Paper paper = (Paper)this.papersListView.SelectedItems[0];
            controller.reevalPaper(paper);
            MessageBox.Show("Reevaluation requested!");
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            Paper paper = (Paper)this.papersListView.SelectedItems[0];
            controller.acceptPaper(paper);
            MessageBox.Show("Paper accepted!");
        }

        private void BtnShowAccepted_Click(object sender, RoutedEventArgs e)
        {
            papers = controller.getAcceptedPapers();
            papersListView.ItemsSource = papers;
        }

        private void BtnShowAll_Click(object sender, RoutedEventArgs e)
        {
            papers = controller.getPapers();
            papersListView.ItemsSource = papers;
        }
    }
}
