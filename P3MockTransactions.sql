CREATE SCHEMA [project3];

-- Create Table Commands
CREATE TABLE [project3].[Customer] (
    [customer_id] INT           IDENTITY (1, 1) NOT NULL,
    [first_name]  NVARCHAR (50) NOT NULL,
    [last_name]   NVARCHAR (50) NOT NULL,
    [email]       NVARCHAR (50) NOT NULL UNIQUE,
    [phone]       NVARCHAR (50) NOT NULL UNIQUE,
    [password]    NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([customer_id] ASC),
);

SELECT * FROM [project3].[Customer];

INSERT INTO [project3].[Customer] (first_name, last_name, email, phone, password)
VALUES ('BANK', 'BANK', 'Hithaeglir@MistyM.com', 7967961280, 'Hithaeglir');

INSERT INTO [project3].[Customer] (first_name, last_name, email, phone, password)
VALUES ('Arthur', 'Gao', 'arthurgao@MistyM.com', 1110001111, 'Yes');

INSERT INTO [project3].[Customer] (first_name, last_name, email, phone, password)
VALUES ('Gollum', 'Sméagol', 'TrueMasterOfTheRing@MistyM.com', 2221112222, '!RING!');

INSERT INTO [project3].[Customer] (first_name, last_name, email, phone, password)
VALUES ('Bilbo', 'Baggins', 'Quest@MistyM.com', 3332223333, 'AdventureAwaits');

CREATE TABLE [project3].[Account] (
    [account_id]          INT           IDENTITY (1, 1) NOT NULL,
    [account_type]        INT           NOT NULL,
    [balance]             FLOAT         NOT NULL,
    [customer_id]         INT           NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([account_id] ASC),
    CONSTRAINT [FK_Customer] FOREIGN KEY ([customer_id]) REFERENCES [project3].[Customer] ([customer_id]) ON DELETE CASCADE ON UPDATE CASCADE
);

SELECT * 
FROM [project3].[Account]

INSERT INTO [project3].[Account] (account_type, balance, customer_id)
VALUES ( 1 , 10000000, 1);

INSERT INTO [project3].[Account] (account_type, balance, customer_id)
VALUES (1 , 1000, 2);

INSERT INTO [project3].[Account] (account_type, balance, customer_id)
VALUES (2 , 1000, 2);

INSERT INTO [project3].[Account] (account_type, balance, customer_id)
VALUES (1 , 1000, 3);

INSERT INTO [project3].[Account] (account_type, balance, customer_id)
VALUES (1 , 1000, 4);

INSERT INTO [project3].[Account] (account_type, balance, customer_id)
VALUES (2 , 1000, 4);

CREATE TABLE [project3].[Transaction](
    transaction_id          INT         IDENTITY,
    account_id              INT,
    time                    DATETIME,
    amount                  FLOAT,
    transaction_notes       NVARCHAR(MAX),
    transaction_type        BIT,
    completion_status       BIT
);

CREATE TABLE [project3].[Request] (
    [request_id]             INT                IDENTITY (1, 1) NOT NULL,
    [reciever_from]          INT                NOT NULL,
    [org_acct]               INT                NOT NULL,
    [amount]                 FLOAT              NOT NULL,
    [req_DateTime]           DATETIME2          NOT NULL,
    [request_type]           BIT                NOT NULL,
    [request_notes]          NVARCHAR (MAX)     NULL,    
    CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED ([request_id] ASC)
);

SELECT * FROM [project3].[Request];

DROP TABLE [project3].[Transaction];

SELECT * FROM [project3].[Transaction];

INSERT [project3].[Transaction] (account_id, amount, time, transaction_notes, transaction_type, completion_status)
VALUES
(1, 450, GETDATE(),  '', 1, 1);

INSERT [project3].[Transaction] (account_id, amount, time, transaction_notes, transaction_type, completion_status)
VALUES
(2, 13945, GETDATE(), '', 1, 1);

INSERT [project3].[Transaction] (account_id, amount, time, transaction_notes, transaction_type, completion_status)
VALUES
(1, 465, GETDATE(), '', 1, 1);

INSERT [project3].[Transaction] (account_id, amount, time, transaction_notes, transaction_type, completion_status)
VALUES
(1, 3648, GETDATE(), '', 0, 1);

INSERT [project3].[Transaction] (account_id, amount, time, transaction_notes, transaction_type, completion_status)
VALUES
(2, 734, GETDATE(), '', 1, 1);

INSERT [project3].[Transaction] (account_id, amount, time, transaction_notes, transaction_type, completion_status)
VALUES
(3, 12124, GETDATE(), '', 1, 1);

INSERT [project3].[Transaction] (account_id, amount, time, transaction_notes, transaction_type, completion_status)
VALUES
(3, 22650, GETDATE(), '', 1, 1);

INSERT [project3].[Transaction] (account_id, amount, time, transaction_notes, transaction_type, completion_status)
VALUES
(2, 84, GETDATE(), '', 0, 1);

INSERT [project3].[Transaction] (account_id, amount, time, transaction_notes, transaction_type, completion_status)
VALUES
(1, 578, GETDATE(), '', 0, 1);

INSERT [project3].[Transaction] (account_id, amount, time, transaction_notes, transaction_type, completion_status)
VALUES
(3, 976, GETDATE(), '', 0, 1);


SELECT transaction_id, [project3].[Transaction].account_id, time, amount, transaction_notes, transaction_type, completion_status, [project3].[Account].Balance
FROM [project3].[Transaction] 
JOIN [project3].[Account] ON [project3].[Transaction].account_id = [project3].[Account].account_id
WHERE account_id = 1;