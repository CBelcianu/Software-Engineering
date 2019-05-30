using ConferenceManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ConferenceManagementSystem.Domain;

namespace ConferenceManagementSystem.Controller
{
    public class CMSController
    {
        public void addSection(string name, string room, DateTime date, int confId, int chairId)
        {
            /*
             * adds a section in the Sections table
             * pre: conference name (string), room (string), date (DateTime), conference id (integer), chair id (integer)
             * post: -
             */

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                try
                {
                    String query = "INSERT INTO Sections VALUES ('" + name + "', '" + room + "', '" + date + "', " +chairId + ", " + confId + ")";
                    db.Execute(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void deleteSection(int id)
        {
            /*
             * deletes the section with a given id from the Sections table
             * pre: section id (integer)
             * post: -
             */
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                String query = "DELETE FROM Sections WHERE ID = " + id.ToString();
                db.Execute(query);
            }
        }

        public void addReview(int paperId, int reviewerId, string qualifier, string comments)
        {
            /*
             * adds a new review in the Reviews table
             * pre: the paper id (integer), the reviewer id (integer), the qualifier (string), the comments (string)
             * post: throws an exception if the given paper already has the maximum number of reviews (4)
             */
            List<String> papers;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {

                papers = db.Query<String>("SELECT Qualifier from Reviews WHERE PaperID=" + paperId).ToList();
                if( papers.Count > 4)
                {
                    throw new Exception("There are already 4 reviewers on this paper!");
                }
                String query = "INSERT INTO Reviews(PaperID,ReviewerID,Qualifier,Comments) VALUES (" + paperId + "," + reviewerId + ",'" + qualifier + "','" + comments + "')";
                db.Execute(query);

            }
        }
        public void AddConference(string ConferenceName, string ConferenceAddress, DateTime ConferenceDate)
        {
            /*
             * adds a new conference in the Conferences table
             * pre: conference name (string), conference address (string), conference date (DateTime)
             * post: -
             */
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                try
                {
                    String query = "INSERT INTO Conferences VALUES ('" + ConferenceName + "','" + ConferenceAddress + "','" + ConferenceDate.ToString() + "')";
                    db.Execute(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<ChosenPcMember> getChosen()
        {
            /*
             * gets all the chosen PC Members from the ChosenPCMembers table
             * pre: -
             * post: a list with all the chosen PC Members
             */
            List<ChosenPcMember> pcs;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                pcs = db.Query<ChosenPcMember>("SELECT email, RoleName from ChosenPC C INNER JOIN Roles R ON C.RoleID = R.ID ").ToList();
                return pcs;
            }
        }

        public void addChosen(string email, string role)
        {
            /*
             * adds a new chosen PC Member in the ChosenPC table
             * pre: email (string), role (string)
             * post: -
             */
            int roleId = getRoleId(role);
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                String query = "INSERT INTO ChosenPC VALUES ('" + email + "'," + roleId + ")";
                db.Execute(query);
            }
        }

        public void deleteChosen(string email)
        {
            /*
            deletes a chosen PC Member from the ChosenPC table
            pre: email (string)
            post: -
             */
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                String query = "DELETE FROM ChosenPC WHERE Email = '" + email + "'";
                db.Execute(query);
            }
        }

        private int getRoleId(string role)
        {
            /*
             * gets the id of the given role from the Roles table
             * pre: role (string)
             * post: the role id (integer)
             */
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                int roleId = db.QueryFirst<int>("SELECT ID from Roles WHERE RoleName = '" + role + "'");
                return roleId;
            }
        }

        public void addPaper(string PaperName, string Topic, string ContentLoc, string AbstractLoc, int SectionID, int AuthorID, int RoleID)
        {
            /*
             * adds a new paper in the Papers and AuthorPapers tables
             * pre: paper name (string), topic (string), the path in the disc of the paper (string), the content on the disc of the abstract (string), the id of the section for the paper (integer), the author id (integer)
             * post: -
             */
            List<String> pid;
            List<String> affiliations;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                try
                {
                    string aff = "regular Member";
                    String query = "INSERT INTO Papers(ContentLoc,AbstractLoc,Topic,PaperName,SectionID,isAccepted) VALUES ('" + ContentLoc + "','" + AbstractLoc + "','" + Topic + "','" + PaperName + "'," + SectionID + ",0)";
                    db.Execute(query);
                    pid = db.Query<String>("SELECT ID FROM Papers WHERE ContentLoc='" + ContentLoc + "'").ToList();
                    int pidd = Int32.Parse(pid[0]);
                    
                    if (RoleID == 4)
                    {
                        affiliations = db.Query<String>( "SELECT Affiliation from Authors WHERE ID=" + AuthorID).ToList();
                        if (affiliations.Count == 0)
                        {
                            String query4 = "INSERT INTO Authors(ID,Affiliation) VALUES (" + AuthorID + ",'" + aff + "')";
                            db.Execute(query4);
                        }
                    }
                    String query1 = "INSERT INTO AuthorPapers(AuthorID,PaperID) VALUES (" + AuthorID + "," + pidd + ")";
                    db.Execute(query1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void updateListener(int id, string firstName, string lastName)
        {
            /*
             * updates the listener with the given id in the Users table
             * pre: listener id(int), first name (string), last name (string)
             * post: -
             */
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                String query = "UPDATE Users SET FirstName='"+firstName+"',LastName='"+lastName+"' WHERE ID="+id;
                db.Execute(query);
            }
            
        }

        public void updateAuthor(int id, string firstName, string lastName, string affiliation)
        {
            /*
             * updates the author with the given id in the Authors and Users tables
             * pre: author id (int), first name (string), last name (string), affiliation (string)
             * post: -
             */
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                String query = "UPDATE Users SET FirstName='" + firstName + "',LastName='" + lastName + "' WHERE ID=" + id;
                String query1 = "UPDATE Authors SET Affiliation='" + affiliation + "' WHERE ID=" + id;
                db.Execute(query);
                db.Execute(query1);
            }
        }

        public void updatePCMember(int id, string firstName, string lastName, string affiliation, string website)
        {
            /*
             * updates the PC Member with the given id in the Users and PcMembers tables
             * pre: PC Memeber id (integer), first name (string), last name (string), affiliation (string), website (string)
             * post: -
             */
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                String query = "UPDATE Users SET FirstName='" + firstName + "',LastName='" + lastName + "' WHERE ID=" + id;
                String query1 = "UPDATE PCMembers SET Affiliation='" + affiliation + "', website='"+website+"' WHERE ID=" + id;
                db.Execute(query);
                db.Execute(query1);
            }
        }

        public void attendConference(Conference conference, User user)
        {
            /*
             * adds the user id and the conference id in the ConferenceUser table 
             * pre: conference and user
             * post: throws an exception if the user is already marked as attending the given conference
             */
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                try
                {
                    String query = "INSERT INTO ConferenceUsers(ConferenceID,UserID) VALUES(" + conference.ID + "," + user.ID + ")";
                    db.Execute(query);
                }
                catch (SqlException)
                {
                    throw new Exception("you already attend this conference");
                }
            }


        }

        public List<Paper> getPapers()
        {
            /*
             * gets all the papers from the DB
             * pre: -
             * post: returns a list with all the papers
             */
            List<Paper> papers;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                papers = db.Query<Paper>("SELECT * FROM Papers").ToList();
                return papers;
            }
            
        }

        public List<Paper> getPapersOfSection(Section section)
        {
            /*
             * gets all the papers from a given section from the DB
             * pre: a section (Section)
             * post: returns a list with all the papers from the given section
             */
            List<Paper> papers;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                papers = db.Query<Paper>("SELECT * FROM Papers WHERE SectionID="+section.ID).ToList();
                return papers;
            }
        }

        public List<Conference> getConferences()
        {
            /*
             * get all the conferences from the DB
             * pre: -
             * post: returns a list with all the conferences
             */
            List<Conference> conferences;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                conferences = db.Query<Conference>("SELECT * FROM Conferences").ToList();
                return conferences;
            }

        }

        public List<Section> getSections(int confId)
        {
            /*
             * gets all the sections with the given conference id from the Sections table
             * pre: conference id (integer)
             * post: a list with all the sections from the conference
             */
            List<Section> section;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                section = db.Query<Section>("SELECT * FROM Sections WHERE ConferenceID = " + confId.ToString()).ToList();
                return section;
            }

        }

        public List<Section> getSections() {
            /*
             * gets all the sections from the DB
             * pre: -
             * post: returns a list with all the sections
             */
            List<Section> sections;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString)) {
                sections = db.Query<Section>("SELECT * FROM Sections").ToList();
                return sections;
            }
        }

        public void updateSectionDeadline(int id, DateTime newDate) {
            /*
             * updates the PaperDeadline field for the sections with the given id from the table Sections
             * pre: the section id (int) and the new deadline (DateTime)
             * post: -
             */
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString)) {
                String query = "UPDATE Sections SET PaperDeadline='" + newDate + "' WHERE ID=" + id;
                db.Execute(query);
            }
        }

        public List<Section> getSectionsOfConference(Conference conference)
        {
            /*
             * gets all the sections from a given conference
             * pre: a conference (Conference)
             * post: a list with all the sections 
             */
            List<Section> sections;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                sections = db.Query<Section>("SELECT S.ID, S.SectionName, S.RoomName, S.PaperDeadline, S.ChairID, S.ConferenceID FROM Sections S INNER JOIN Conferences C ON S.ConferenceID = C.ID WHERE C.ID="+conference.ID).ToList();
                return sections;
            }
        }

        public List<Conference> getMyConferences(User user)
        {
            /*
             * gets all the conferences which a given user participates in
             * pre: a user (User)
             * post: a list with all the conferences
             */
            List<Conference> conferences;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                conferences = db.Query<Conference>("SELECT C.ID,C.ConferenceName,C.ConferenceAddress,C.ConferenceDate FROM Conferences C INNER JOIN ConferenceUsers U ON C.ID=U.ConferenceID WHERE U.UserID="+user.ID).ToList();
                return conferences;
            }
        }

        public User LogIN(string username, string password)
        {
            /*
             * gets the user with the given username and password, and returns the user based on the type it has
             * pre: the username (string) and the password(string) of the user
             * post: the user, if the id is found, or throws an exception otherwise
             */
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                User user = null;
                user = db.QueryFirst<User>("SELECT * FROM Users WHERE Username='" + username + "' AND Passwd='" + password + "'");

 
                if (user.RoleID == 1)
                {
                    Author author = db.QueryFirst<Author>("SELECT U.ID, FirstName, LastName, RoleID, email, Username, Passwd, Affiliation FROM Users U INNER JOIN Authors A on U.ID=A.ID WHERE Username='" + username + "' AND Passwd='" + password + "'");
                    return author;
                }
                else if(user.RoleID == 2 || user.RoleID == 3 || user.RoleID == 4)
                {
                    PCMember pc = db.QueryFirst<PCMember>("SELECT U.ID, FirstName, LastName, RoleID, email, Username, Passwd, Affiliation, website FROM Users U INNER JOIN PCMembers A on U.ID=A.ID WHERE Username='" + username + "' AND Passwd='" + password + "'");
                    return pc;
                }
                return user;
            }
        }

        public void registerListener(string username, string passwd, string fname, string lname, string email)
        {
            /*
             * adds a new Listener in the Users table
             * pre: user username (string), password (string), first name (string), last name (string), email (string)
             * post: throws an exception if the username or the email are already used
             */
            List<String> res;
            List<String> res1;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                res = db.Query<String>("SELECT FirstName FROM Users WHERE Username='" + username + "'").ToList();
                res1 = db.Query<String>("SELECT FirstName FROM Users WHERE email='" + email + "'").ToList();
                if (res.Capacity > 0 || res1.Capacity > 0)
                    throw new Exception("Username/Email already in use");
                else
                {
                    String query = "INSERT INTO Users(FirstName,LastName,Username,Passwd,email,RoleID) values('" +fname+"','"+lname+"','"+username+"','"+passwd+"','"+email+"',5)";
                    db.Execute(query);
                }
            }
        }

        public void registerAuthor(string username, string passwd, string fname, string lname, string email, string affiliation)
        {
            /*
             * adds a new author in the Users and Authors tables
             * pre: authors' username (string), password (string), first name (string), last name (string), email (string) and affiliation (string)
             * post: throws an exception if the username or the email are already used
             */
            List<String> res;
            List<String> res1;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                res = db.Query<String>("SELECT FirstName FROM Users WHERE Username='" + username + "'").ToList();
                res1 = db.Query<String>("SELECT FirstName FROM Users WHERE email='" + email + "'").ToList();
                if (res.Capacity > 0 || res1.Capacity > 0)
                    throw new Exception("Username/Email already in use");
                else
                { 
                    String query = "INSERT INTO Users(FirstName,LastName,Username,Passwd,email,RoleID) values('" + fname + "','" + lname + "','" + username + "','" + passwd + "','" + email + "',1)";
                    db.Execute(query);
                    User user = db.QueryFirst<User>("SELECT * FROM Users WHERE Username='" + username + "' AND Passwd='" + passwd + "'");
                    String query1 = "INSERT INTO Authors(ID,Affiliation) values('" +user.ID+ "','" + affiliation + ")";
                    db.Execute(query1);
                }
            }
        }

        public List<Review> GetReviewsForPaper(Paper paper)
        {
            List<Review> reviews;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                reviews = db.Query<Review>("SELECT * FROM Reviews WHERE PaperId = " + paper.ID).ToList();
                return reviews;
            }
        }

        public List<Review> getReeval(Paper paper)
        {
            List<Review> papers;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                papers = db.Query<Review>("SELECT * FROM Reviews WHERE ReevalRequest=1 AND PaperID="+paper.ID).ToList();
                return papers;
            }


        }

        public List<Paper> getAcceptedPapers()
        {
            List<Paper> papers;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                papers = db.Query<Paper>("SELECT * FROM Papers WHERE isAccepted=1").ToList();
                return papers;
            }


        }

        public void acceptPaper(Paper paper)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                db.Execute("UPDATE Papers SET isAccepted=" + 1 + " WHERE ID=" + paper.ID);
            }


        }

        public void reevalPaper(Paper paper)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                db.Execute("UPDATE Reviews SET ReevalRequest = 1 WHERE PaperID=" + paper.ID);
            }
        }

        public void registerPCMember(string username, string passwd, string fname, string lname, string email, string affiliation, string website)
        {
            /*
             * adds a new PCMember to the Users and the PCMembers tables
             * pre: username (string), password (string), first name (string), last name (string), email (string), affiliation (string), website(string)
             * post: throws exception if the username or email are already used, or if the user does not have the right to register as a PCMember
             */
            List<String> res;
            List<String> res1;
            List<String> res2;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cmsDatabase"].ConnectionString))
            {
                res = db.Query<String>("SELECT FirstName FROM Users WHERE Username='" + username + "'").ToList();
                res1 = db.Query<String>("SELECT FirstName FROM Users WHERE email='" + email + "'").ToList();
                res2 = db.Query<String>("SELECT RoleID FROM ChosenPC WHERE email='" + email + "'").ToList();
                if (res.Capacity > 0 || res1.Capacity > 0)
                    throw new Exception("Username/Email already in use");
                if (res2.Capacity == 0)
                    throw new Exception("You don't have the right to register as a PC Member");
                else
                {
                    int xv = Int32.Parse(res2[0]);
                    String query = "INSERT INTO Users(FirstName,LastName,Username,Passwd,email,RoleID) values('" + fname + "','" + lname + "','" + username + "','" + passwd + "','" + email + "',"+xv+")";
                    db.Execute(query);
                    User user = db.QueryFirst<User>("SELECT * FROM Users WHERE Username='" + username + "' AND Passwd='" + passwd + "'");
                    String query1 = "INSERT INTO PCMembers(ID,Affiliation,website,isReviewer) values(" + user.ID + ",'" + affiliation + "','" + website + "',0)";
                    db.Execute(query1);
                }
            }
        }
    }
}
