USE SuperDuperISSDataBase

DELETE FROM PCMembers
DELETE FROM Users
DELETE FROM ChosenPC
DELETE FROM Roles

DBCC CHECKIDENT ('Roles', RESEED, 0)
INSERT INTO Roles VALUES
	('Author'),
	('Chair'),
	('Co-Chair'),
	('Regular'),
	('Listener'),
	('SteeringCommittee')
SELECT * FROM Roles

/*
	Some accounts for testing the app

    username	password	role
--------------+-----------+------------
	admin		admin		SteeringCommittee
	alex		test		Co-Chair
	ale			test		Chair
	cris		test		Author
	cata		test		Listener
	dia			test		Regular
---------------------------------------
*/

INSERT INTO ChosenPC VALUES
	('alex@email.com', 3),
	('ale@email.com', 2),
	('dia@email.com', 4),
	('pcm1@email.com', 3),
	('pcm2@email.com', 3),
	('pcm3@email.com', 4),
	('pcm4@email.com', 4),
	('pcm5@email.com', 4)
SELECT * FROM ChosenPC

DBCC CHECKIDENT ('Users', RESEED, 0)
INSERT INTO Users VALUES
	('n/a', 'n/a', 'admin', 'admin', 'stcomm@email.com', 6),
	('Alexandra', 'Ardelian', 'ale', 'test', 'ale@email.com', 2)
SELECT * FROM Users

INSERT INTO PCMembers VALUES
	(2, 'aff', 'web', 0)
SELECT * FROM PCMembers

SELECT email, RoleName from ChosenPC C INNER JOIN Roles R ON C.RoleID = R.ID 
SELECT * FROM Sections