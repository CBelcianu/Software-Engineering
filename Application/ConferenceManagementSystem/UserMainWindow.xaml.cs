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
using System.Windows.Shapes;

namespace ConferenceManagementSystem
{
    /// <summary>
    /// Interaction logic for UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        private CMSController controller;
        private User user;

        public UserMainWindow(CMSController controller, User user)
        {
            string r="";
            this.user = user;
            this.controller = controller;
            InitializeComponent();
            switch (user.RoleID)
            {
                case 1: { r = "Author"; break; }
                case 2: { r = "Chair"; break; }
                case 3: { r = "Co-Chair"; break; }
                case 4: { r = "Regular"; break; }
                case 5: { r = "Listener"; break; }

            }
            fullNameLabel.Content = user.LastName.ToUpper() + " " + user.FirstName.ToUpper() + ", " + r;

            if (user.RoleID == 5){
                this.papersItem.Visibility = Visibility.Collapsed;
                this.reviewsItem.Visibility = Visibility.Collapsed;
                this.deadlineItem.Visibility = Visibility.Collapsed;
            }
            if (user.RoleID == 1)
                this.reviewsItem.Visibility = Visibility.Collapsed;
                
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ProfileItem_Selected(object sender, RoutedEventArgs e)
        {
            contentFrame.Content = new ProfilePage(controller,user);
        }

        private void ConferencesItem_Selected(object sender, RoutedEventArgs e)
        {
            contentFrame.Content = new ConferencesPage(controller, user);
        }

        private void MyConferencesItem_Selected(object sender, RoutedEventArgs e)
        {
            contentFrame.Content = new MyConferencesPage(controller, user);
        }

        private void PapersItem_Selected(object sender, RoutedEventArgs e)
        {
            contentFrame.Content = new PapersPage(controller, user);   
        }

        private void ReviewsItem_Selected(object sender, RoutedEventArgs e)
        {
            if (user.RoleID == 4)
            {
                contentFrame.Content = new ReviewPage(controller, user);
            }
            else
            {
                contentFrame.Content = new ChairReview(controller, user);
            }
        }

        private void DeadlineItem_Selected(object sender, RoutedEventArgs e)
        {
            if (user.RoleID == 2 || user.RoleID == 3) {//is chair or co-chair
                contentFrame.Content = new DeadlinePage(controller, user, true);
            } else {
                contentFrame.Content = new DeadlinePage(controller, user, false);
            }
        }
    }
}
