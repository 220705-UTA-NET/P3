CREATE SCHEMA project3;
GO

-- Drop Table Commands --
DROP TABLE [project3].[Customer];
DROP TABLE [project3].[Account];
DROP TABLE [project3].[Transaction];
DROP TABLE [project3].[Request];
DROP TABLE [project3].[Budget];
DROP TABLE [project3].[Ticket];
DROP TABLE [project3].[Message];
DROP TABLE [project3].[Support];

-- Create Table Commands
CREATE TABLE [project3].[Customer] (
    [customer_id]            INT           IDENTITY (1, 1) NOT NULL,
    [first_name]             NVARCHAR (50) NOT NULL,
    [last_name]              NVARCHAR (50) NOT NULL,
    [username]               NVARCHAR (50) NOT NULL UNIQUE,
    [email]                  NVARCHAR (50) NOT NULL UNIQUE,
    [phone]                  NVARCHAR (50) NOT NULL,
    [password]               NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([customer_id] ASC)
);

CREATE TABLE [project3].[Support] (
    [support_id]             INT           IDENTITY (1, 1) NOT NULL,
    [first_name]             NVARCHAR (50) NOT NULL,
    [last_name]              NVARCHAR (50) NOT NULL,
    [username]               NVARCHAR (50) NOT NULL UNIQUE,
    [email]                  NVARCHAR (50) NOT NULL UNIQUE,
    [phone]                  NVARCHAR (50) NOT NULL,
    [password]               NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Support] PRIMARY KEY CLUSTERED ([support_id] ASC)
);

CREATE TABLE [project3].[Account] (
    [account_id]             INT           IDENTITY (1, 1) NOT NULL,
    [account_type]           INT           NOT NULL,
    [balance]                FLOAT         NOT NULL,
    [customer_id]            INT           NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([account_id] ASC),
    CONSTRAINT [FK_Customer] FOREIGN KEY ([customer_id]) REFERENCES [project3].[Customer] ([customer_id]) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [project3].[Transaction] (
    [transaction_id]         INT                IDENTITY (1, 1) NOT NULL,
    [account_id]             INT                NOT NULL,
    [time]                   DATETIME2          NOT NULL,
    [amount]                 FLOAT              NOT NULL,
    [transaction_notes]      NVARCHAR (255)     NULL,
    [transaction_type]       BIT                NOT NULL,
    [completion_status]      BIT                NOT NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([transaction_id] ASC),
    CONSTRAINT [FK_Account] FOREIGN KEY ([account_id]) REFERENCES [project3].[Account] ([account_id]) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [project3].[Request] (
    [request_id]             INT                IDENTITY (1, 1) NOT NULL,
    [request_from]           INT                NOT NULL,
    [org_acct]               INT                NOT NULL,
    [amount]                 FLOAT              NOT NULL,
    [time]                   DATETIME2          NOT NULL,
    [request_type]           BIT                NOT NULL,
    [request_notes]          NVARCHAR (255)     NULL,    
    CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED ([request_id] ASC)
);

CREATE TABLE [project3].[Budget] (
    [budget_id]              INT  IDENTITY (1, 1) NOT NULL,
    [customer_id]            INT                  NOT NULL,
    [account_id]             INT                  NOT NULL,
    [budget_DateTime]        DATETIME2            NULL,
    [monthly_amount]         FLOAT                NOT NULL,
    [warning_amount]         FLOAT                NULL,
    CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED ([budget_id] ASC),
    CONSTRAINT [FK_Customer_Budget] FOREIGN KEY ([customer_id]) REFERENCES [project3].[Customer] ([customer_id]),
    CONSTRAINT [FK_Account_Budget] FOREIGN KEY ([account_id]) REFERENCES [project3].[Account] ([account_id])
);

CREATE TABLE [project3].[Ticket] (
    [ticket_id]              NVARCHAR (50)        NOT NULL,
    [customer_id]            INT                  NOT NULL,
    [initial_message]        TEXT                 NULL, 
    [ticket_status]          BIT                  NOT NULL,
    CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED ([ticket_id] ASC),
    CONSTRAINT [FK_Customer_Ticket] FOREIGN KEY ([customer_id]) REFERENCES [project3].[Customer] ([customer_id])
);

CREATE TABLE [project3].[Message] (
    [message_id]       INT           IDENTITY (1, 1) NOT NULL,
    [ticket_id]              NVARCHAR (50)        NOT NULL,
    [message_content]  TEXT          NULL,
    [message_DateTime] DATETIME2 (7) NOT NULL,
    [message_user]     NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED ([message_id] ASC),
    CONSTRAINT [FK_Tickets_Message] FOREIGN KEY ([ticket_id]) REFERENCES [project3].[Ticket] ([ticket_id])
);

-- Select Commands --
SELECT * FROM [project3].[Account];
SELECT * FROM [project3].[Budget];
SELECT * FROM [project3].[Customer];
SELECT * FROM [project3].[Transaction];
SELECT * FROM [project3].[Request];

SELECT CUSTOMER
SELECT customer_id, first_name, last_name, email, username, phone
FROM [project3].[Customer]
WHERE customer_id = 1;

SELECT customer_id, username, password
FROM [project3].[Customer]
WHERE username = 'john' AND password = 'password';

-- Insert Commands --

-- Insert Customer --
INSERT INTO [project3].[Customer] (first_name, last_name, email, username, phone, password)
VALUES
('Bank', 'Bank', 'Bank@Bank.Bank', 'Bank', '0000000000', 'password'),
('Jeff', 'Jefferson', 'jeffy@email.com', 'kingjeffy', '8082345678', 'JeffDomination'),
('Ian', 'ScrumPrentus', 'BossIan@email.com', 'scrumwizard2', '8082348765', 'PatricideIsFroundUpon'),
('Jonathan', 'ScrumMaster', 'TheRealBoss@email.com', 'scrumwizard', '8082359876', 'HailToTheKing'),
('James', 'DevOpTroll', 'PleaseWork@Github.Broke', 'notagain', '8085555555', 'YouMergedWhat'),
('Kadin', 'Democratic', 'IDodgedLeadership@Relaxation.net', 'relaxed', '8086588521', 'Got99ProblemsButLeadAint1');


INSERT INTO [project3].[Transaction] (account_id, time, amount, transaction_type, completion_status, transaction_notes)
VALUES
('5', '1984-07-16 12:21:12', '1337.00', '1', '1', ''),
('2', '1984-07-16 12:21:12', '1337.00', '1', '1', ''),
('4', '1984-07-16 12:21:12', '1337.00', '1', '1', ''),
('7', '1984-07-16 12:21:12', '1337.00', '0', '1', ''),
('1', '1984-07-16 12:21:12', '1337.00', '0', '1', ''),
('3', '1984-07-16 12:21:12', '1337.00', '1', '1', ''),
('6', '1984-07-16 12:21:12', '1337.00', '0', '1', ''),
('5', '1984-07-16 12:21:12', '1337.00', '1', '1', ''),
('7', '1984-07-16 12:21:12', '1337.00', '1', '1', ''),
('3', '1984-07-16 12:21:12', '1337.00', '0', '1', ''),
('2', '1984-07-16 12:21:12', '1337.00', '1', '1', ''),
('4', '1984-07-16 12:21:12', '1337.00', '1', '1', ''),
('1', '1984-07-16 12:21:12', '1337.00', '0', '1', ''),
('6', '1984-07-16 12:21:12', '1337.00', '1', '1', ''),
('7', '1984-07-16 12:21:12', '1337.00', '1', '1', '');

-- Insert Account --
INSERT INTO [project3].[Account] (account_type, balance, customer_id)
VALUES 
(1, 1000000000000.00, 1),
(2, 1000.00, 3),
(1, 100000.00, 2),
(1, 10.00, 3),
(1, 100000.00, 4),
(1, 0.00, 5),
(1, 1337.00, 6);

Calling procedure 'sp_rename' to rename a column name
EXEC sp_rename 'sekidb.project3.type', 'account_type', 'COLUMN';

INSERT INTO [project3].[Customer] (first_name, last_name, email, username, phone, password)
VALUES
('Rick', 'Astley', 'Rolling@everyone.net', 'nostranger', '6383746662', 'GiveYouUp');

INSERT INTO [project3].[Account] (account_type, balance, customer_id)
VALUES
(1, 1337.00, 6);

INSERT INTO [project3].[Transaction] (account_id, time, amount, transaction_type, completion_status, transaction_notes)
VALUES
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna say goodbye'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna make you cry'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna run around and desert you'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna let you down'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna give you up'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Gotta make you understand'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'I just wanna tell you how Im feeling'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'We know the game and were gonna play it'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Inside, we both know whats been going on'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Your hearts been aching, but youre too shy to say it'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Weve known each other for so long'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna tell a lie and hurt you'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna say goodbye'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna make you cry'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna run around and desert you'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna let you down'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna give you up'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna tell a lie and hurt you'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna say goodbye'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna make you cry'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna run around and desert you'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna let you down'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna give you up'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Dont tell me youre too blind to see'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'And if you ask me how Im feeling'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'We know the game and were gonna play it'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Inside, we both know whats been going on'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Your hearts been aching, but youre too shy to say it'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Weve known each other for so long'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna tell a lie and hurt you'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna say goodbye'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna make you cry'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna run around and desert you'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna let you down'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Never gonna give you up'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Gotta make you understand'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'I just wanna tell you how Im feeling'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'You wouldnt get this from any other guy'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'A full commitments what Im thinking of'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'You know the rules and so do I'),
('8', '1987-07-27 12:21:12', '1337.00', '1', '1', 'Were no strangers to love');

UPDATE [project3].[Account] SET customer_id = 7 WHERE account_id = 8; 