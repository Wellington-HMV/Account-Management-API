**Comandos executados no Banco de dados:

CREATE TABLE [dbo].[extract_account](
	id int IDENTITY(1,1) PRIMARY KEY ,
	[id_account] [int] NOT NULL,
	[type] [varchar](10) NOT NULL,
	[value] [float] NOT NULL,
	[register] [datetime] NOT NULL,
	)

	ALTER TABLE [dbo].[extract_account] ADD	CONSTRAINT FK_ACCOUNT_EXTRACT FOREIGN KEY (id_account)
    	REFERENCES dbo.accounts(id)

CREATE TABLE dbo.accounts(
	id int IDENTITY(1,1) PRIMARY KEY ,
	name varchar(50) NOT NULL,
	accountNumber int NOT NULL,
	balance float NOT NULL,
	email varchar(50) NOT NULL,)


CREATE TABLE [dbo].[login_account](
	id int IDENTITY(1,1) PRIMARY KEY ,
	[id_account] [int] NOT NULL,
	[email] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	)

	ALTER TABLE [dbo].[login_account] ADD	CONSTRAINT FK_ACCOUNT_LOGIN FOREIGN KEY (id_account)
    	REFERENCES dbo.accounts(id)