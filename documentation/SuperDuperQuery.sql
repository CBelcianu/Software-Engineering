create database SuperDuperISSDataBase

use SuperDuperISSDataBase

create table Roles(
	ID int identity(1,1),
	RoleName varchar(30)
	constraint PK_Roles_ID primary key(ID)
)

create table Users(
	ID int identity(1,1),
	FirstName varchar(30),
	LastName varchar(30),
	Username varchar(30) unique,
	Passwd varchar(40),
	email varchar(30) unique,
	RoleID int,
	constraint PK_Users_ID primary key(ID),
	constraint FK_Users_Roles foreign key(RoleID) references Roles(ID)
)

create table Authors(
	ID int,
	Affiliation varchar(30),
	constraint PK_Authors_ID primary key(ID),
	constraint FK_Users_Authors foreign key(ID) references Users(ID)
)

create table PCMembers(
	ID int,
	Affiliation varchar(30),
	website varchar(50),
	isReviewer bit,
	constraint PK_RegularMembers_ID primary key(ID),
	constraint FK_Users_RegularMembers foreign key(ID) references Users(ID)
)

create table Conferences(
	ID int identity(1,1),
	ConferenceName varchar(40),
	ConferenceAddress varchar(40),
	ConferenceDate date,
	constraint PK_Conferences_ID primary key(ID)
)

create table Sections(
	ID int identity(1,1),
	SectionName varchar(30),
	RoomName varchar(30),
	PaperDeadline date,
	ChairID int,
	ConferenceID int,
	constraint PK_Sections_ID primary key(ID),
	constraint FK_Sections_PCMembers foreign key(ChairID) references PCMembers(ID),
	constraint FK_Sections_Conferences foreign key(ConferenceID) references Conferences(ID)
)

create table Papers(
	ID int identity(1,1),
	ContentLoc varchar(100),
	AbstractLoc varchar(100),
	Topic varchar(30),
	PaperName varchar(60),
	isAccepted bit,
	SectionID int,
	constraint PK_Papers_ID primary key(ID),
	constraint FK_Papers_Sections foreign key(SectionID) references Sections(ID)
)

create table Reviews(
	PaperID int,
	ReviewerID int,
	Qualifier varchar(20),
	Comments varchar(200),
	constraint FK_Reviews_Papers foreign key(PaperID) references Papers(ID),
	constraint FK_Reviews_PCMembers foreign key(ReviewerID) references PCMembers(ID)
)

create table ConferenceUsers(
	UserID int,
	ConferenceID int,
	constraint PK_ConferenceUsers_ID primary key(UserID,ConferenceID),
	constraint FK_Conferences_Users foreign key(ConferenceID) references Conferences(ID)
)

create table ChosenPC(
	email varchar(30),
	RoleID int,
	constraint FK_ChosenPC_Roles foreign key(RoleID) references Roles(ID)
)

create table AuthorPapers(
	AuthorID int,
	PaperID int,
	constraint PK_AuthorPapers_ID primary key(AuthorID,PaperID),
	constraint FK_AuthorPapers_Author foreign key(AuthorID) references Authors(ID),
	constraint FK_AuthorPapers_Papers foreign key(PaperID) references Papers(ID)
)