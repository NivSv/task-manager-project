CREATE DATABASE TaskManager;
GO
USE TaskManager;
GO
CREATE TABLE Users (
 UserID int NOT NULL IDENTITY(1,1),
 Username nvarchar(16),
 Password nvarchar(64),
 PRIMARY KEY (UserID)
 );
GO
CREATE TABLE Status (
 StatusID int NOT NULL IDENTITY(1,1),
 StatusName nvarchar(16),
 PRIMARY KEY (StatusID)
 );
GO
CREATE TABLE Priority (
 PriorityID int NOT NULL IDENTITY(1,1),
 PriorityName nvarchar(16),
 PRIMARY KEY (PriorityID)
 );
GO
CREATE TABLE Tasks (
	TaskID int NOT NULL IDENTITY(1,1),
	TaskTitle nvarchar(25),
	TaskDescription nvarchar(500),
	TaskPriority int NOT NULL,
	TaskStatus int DEFAULT 1,
	TaskCreatedDate DATE DEFAULT GETDATE(),
	TaskDeadline DATE,
	Assignee int NOT NULL,
	PRIMARY KEY(TaskID),
	FOREIGN KEY(Assignee) REFERENCES Users(UserID),
	FOREIGN KEY(TaskPriority) REFERENCES Priority(PriorityID),
	FOREIGN KEY(TaskStatus) REFERENCES Status(StatusID)
);
GO
INSERT INTO Priority (PriorityName)
VALUES 
	('Low'),
	('Medium'),
	('High');
GO
INSERT INTO Status (StatusName)
VALUES 
	('Pending'),
	('In Progress'),
	('Done'),
	('Canceled');
GO
INSERT INTO Users (Username,Password)
VALUES 
	('Niv','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
	('Niv2','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
	('Niv3','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
	('Niv4','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3');
GO
INSERT INTO Tasks (TaskTitle,TaskDescription,TaskPriority,TaskDeadline,Assignee)
VALUES 
	('KeyBoard Slam','SLAM YOUR HEAD ON YOUR KEYBOARD AND SEE WHAT GETS TYPED.',1,'2022-2-6',1),
	('Batman Signal','Go to a window and then back to your seat, say, I thought I saw the bat signal.',2,'2022-2-6',2),
	('Man On a Mission','Step on other peoples shadows.',2,'2022-2-6',3),
	('Hunger','Eat pizza. Then eat another. And then eat another.',3,'2022-2-6',4),
	('Do Not','Do not read the next sentence. You little rebel I like you.',3,'2022-2-6',3);
GO