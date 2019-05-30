using ConferenceManagementSystem.Controller;
using ConferenceManagementSystem.Domain;
using ConferenceManagementSystem.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Section = ConferenceManagementSystem.Domain.Section;

namespace ConferenceManagementSystem
{
    /// <summary>
    /// Interaction logic for PapersPage.xaml
    /// </summary>
    public partial class PapersPage : Page
    {
        CMSController controller;
        User user;
        public List<Conference> conferences { get; set; }
        public List<Section> sections { get; set; }
        public List<Paper> papers { get; set; }


        public PapersPage(CMSController controller, User user)
        {
            this.controller = controller;
            this.user = user;
            InitializeComponent();
            conferences = this.controller.getConferences();
            DataContext = this;
            if(user.RoleID==1 || user.RoleID == 5)
            {
                papersLabel.Visibility = Visibility.Hidden;
                papersListView.Visibility = Visibility.Hidden;
                btnViewAbstract.Visibility = Visibility.Hidden;
                btnViewPaper.Visibility = Visibility.Hidden;

            }
            else
            {
                if (user.RoleID != 4)
                {
                    btnViewPaper.Visibility = Visibility.Hidden;
                    btnViewAbstract.Visibility = Visibility.Hidden;
                }
                loadPapers();
            }
        }

        private void loadPapers()
        {
            papers = controller.getPapers();
            papersListView.ItemsSource = papers;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Conference conference = (Conference)conferencesListView.SelectedItems[0];
            sections = controller.getSectionsOfConference(conference);
            sectionsListView.ItemsSource = sections;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (user.RoleID == 1 || user.RoleID == 4)
            {   
                if(sectionsListView.SelectedItems.Count > 1 || sectionsListView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("You can select only one session");
                }

                Section section = (Section)sectionsListView.SelectedItems[0];
                try
                {
                    if (paperNameTextBox.Text != "" && topicTextBox.Text != "" && paperLocationTextBox.Text != "" && abstractLocationTextBox.Text != "")
                    {
                        DateTime dl = DateTime.Parse(section.PaperDeadline);
                        DateTime date = DateTime.Now;
                        if (dl > date)
                        {
                            controller.addPaper(paperNameTextBox.Text, topicTextBox.Text, paperLocationTextBox.Text, abstractLocationTextBox.Text, section.ID, user.ID, user.RoleID);
                            MessageBox.Show("Success!");
                        }
                        else
                        {
                            MessageBox.Show("Deadline passed!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("invalid paper name, topic, or locations!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("You can't upload papers");
            }
        }
        private void SectionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sectionsListView.SelectedItems.Count > 0)
            {
                Section section = (Section)sectionsListView.SelectedItems[0];
                papers = controller.getPapersOfSection(section);
                papersListView.ItemsSource = papers;
            }
        }

        private void BtnBroeseAbstract_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "PDF Files|*.pdf";
            fileDialog.DefaultExt = ".pdf";
            Nullable<bool> dialogOK = fileDialog.ShowDialog();

            if (dialogOK == true)
            {
                string fileName = fileDialog.FileName;
                abstractLocationTextBox.Text = fileName;

            }
        }

        private void BtnBrowsePaper_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "PDF Files|*.pdf";
            fileDialog.DefaultExt = ".pdf";
            Nullable<bool> dialogOK = fileDialog.ShowDialog();

            if (dialogOK == true)
            {
                string fileName = fileDialog.FileName;
                paperLocationTextBox.Text = fileName;

            }
        }

        private void BtnViewPaper_Click(object sender, RoutedEventArgs e)
        {
            if (papersListView.SelectedItems.Count > 0)
            {
                Paper paper = (Paper)papersListView.SelectedItems[0];
                PDFViewer viewer = new PDFViewer(paper.ContentLoc);
                viewer.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a paper!");
            }
        }

        private void BtnViewAbstract_Click(object sender, RoutedEventArgs e)
        {
            if (papersListView.SelectedItems.Count > 0)
            {
                Paper paper = (Paper)papersListView.SelectedItems[0];
                PDFViewer viewer = new PDFViewer(paper.AbstractLoc);
                viewer.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a paper!");
            }
        }
    }
}
