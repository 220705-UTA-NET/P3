/* 

DROP TABLE Contents;


DROP TABLE Tickets;
    


CREATE TABLE Tickets
(
    Ticket_ID INT IDENTITY NOT NULL UNIQUE,
    UserName NVARCHAR(30) NOT NULL ,
    PRIMARY KEY(Ticket_ID));
    


 CREATE TABLE Contents
 (
    Message_ID INT IDENTITY NOT NULL UNIQUE,
    Ticket_ID INT NOT NULL,
    Message Text NULL,
    Date_time Datetime NOT NULL,
    PRIMARY KEY (Message_ID),
    FOREIGN KEY(Ticket_ID) REFERENCES Tickets(Ticket_ID)
 );

*/


 
 --SELECT Message FROM Contents WHERE Ticket_ID=2


 

/* 
INSERT INTO Tickets VALUES('Roger'),('Jimmy'),('Kenneth'); 

INSERT INTO Contents VALUES
(1,'Go to the counter for help','08/08/2022'),
( 1,'Ask for a new ATM','08/09/2022'),
(1, 'Deposit more money','08/08/2022'),
(2,'Contact the nearest branch','08/08/2022'),
(3,'Your account is deactivated','08/08/2022');

*/




