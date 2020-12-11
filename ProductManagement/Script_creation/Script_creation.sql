--use master
--go
--IF (EXISTS (SELECT name FROM master.dbo.sysdatabases 
--WHERE ('[' + name + ']' = 'Freelance'
--OR name = 'Freelance')))
--PRINT 'db exists'
--drop table freelance
--go
create database Freelance
go
 use freelance
 go
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Commande') 
drop table [commande]
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='product') 
drop table [product]
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='typeproduct') 
drop table [typeproduct]
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Customer') 
drop table [Customer]

go

CREATE TABLE [dbo].[customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [nvarchar](50) NULL,
	[lastname] [nvarchar](50) NULL,
	[birthdate] [date] NULL,
	[adresse] [nvarchar](100) NULL,
	[cite] [nvarchar](100) NULL,
	[countrie] [nvarchar](100) NULL,
	[codep] [int] NULL,
	[email] [nvarchar](100) NULL,
	[tel] [int] NULL,
	[prof] [nvarchar](100) NULL
	primary key(id))

	
CREATE TABLE [dbo].[typeproduct](
	[id] [int] NOT NULL,
	[type] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL
	primary key (id)
	)

	CREATE TABLE [dbo].[product](
	[id] [int] NOT NULL,
	[ref] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](50) NULL,
	[datefab] [date] NULL,
	[type] [int] NULL,
	[price] [float] NULL,
	[qt] [int] NULL,
	[available] [decimal](18, 0) NULL
	primary key(id),
	foreign key(type) references typeproduct(id),)
	
CREATE TABLE [dbo].[commande](
	[id] [int] NOT NULL,
	[idpdt] [int] NULL,
	[idclt] [int] NULL,
	[qt] [int] NULL,
	[datec] [date] NULL,
	primary key(id),
	foreign key(idpdt) references product(id),
	foreign key(idclt) references customer(id),
	)

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'addcustomer')
DROP PROCEDURE addcustomer
GO


CREATE procedure addcustomer @firstname nvarchar(50),@lastname nvarchar(50), @birthdate date, @adresse nvarchar(100),@cite nvarchar(100),
@countrie nvarchar(100),@codep int, @email nvarchar(100),@tel int,@prof nvarchar(100)
as
insert into customer(firstname,lastname,birthdate,adresse,cite,countrie,codep,email,tel,prof) 
values   (@firstname,@lastname,@birthdate,@adresse,@cite,@countrie,@codep,@email,@tel,@prof)

go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'addproduct')
DROP PROCEDURE addproduct
GO

CREATE procedure addproduct @ref nvarchar(50),@name nvarchar(50),@description nvarchar(100),@datefab date,
@type int, @price float,@qt int,@available decimal
as
insert into product
select isnull(max(id),0)+1, @ref,@name,@description,@datefab,@type,@price,@qt,@available from product
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'addtype')
DROP PROCEDURE addtype
GO

CREATE procedure addtype @type nvarchar(50), @description nvarchar(100)
as
insert into typeproduct 
select isnull(max(id),0)+1, @type,@description from typeproduct

go

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'addcommande')
DROP PROCEDURE addcommande
GO

CREATE procedure addcommande @idpdt int,@idclt int,@qt int,@datec date
as
insert into commande
select isnull(max(id),0)+1,@idpdt,@idclt,@qt,@datec from commande



