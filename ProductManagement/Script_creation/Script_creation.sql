use master
go
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = 'Freelance'
OR name = 'Freelance')))
PRINT 'db exists'
drop table freelance
go
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
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='User') 
drop table [User]
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='typeprofil') 
drop table [typeprofil]
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Design') 
drop table [Design]
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='complaint') 
drop table [complaint]
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='state_complaint') 
drop table [state_complaint]
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='type_complaint') 
drop table [type_complaint]
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='stat') 
drop table [stat]
go
create table stat(
day nvarchar(10),
val int
)

create table complaint(
id int,
title nvarchar(50),
description nvarchar(2000),
type int,
product nvarchar(100),
Creation_date date, 
state int,
)
create table state_complaint(
id int,
[state] nvarchar(100)
)
create table type_complaint(
id int,
[type] nvarchar(100)
)
go
Create table Design(
id_user int,
design nvarchar(50),
datatable nvarchar(50),
btn nvarchar(50),
)
go
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](50) null,
	[password] [nvarchar](50) null,
	[typeprofil] [int] NULL,
	[firstname] [nvarchar](50) NULL,
	[lastname] [nvarchar](50) NULL,
	[birthdate] [date] NULL,
	[adresse] [nvarchar](100) NULL,
	[cite] [nvarchar](100) NULL,
	[countrie] [nvarchar](100) NULL,
	[codep] [int] NULL,
	[email] [nvarchar](100) NULL,
	[tel] [int] NULL,
	[prof] [nvarchar](100) NULL,
	primary key(id))
	go
-- création de la table customer
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

-- création de la table typeproduct
CREATE TABLE [dbo].[typeproduct](
	[id] [int] NOT NULL,
	[type] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL
	primary key (id)
	)

--Création de la table product
	CREATE TABLE [dbo].[product](
	[id] [int] NOT NULL,
	[ref] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](50) NULL,
	[datefab] [date] NULL,
	[type] [int] NULL,
	[price] [float] NULL,
	[qt] [int] NULL,
	primary key(id),
	foreign key(type) references typeproduct(id),)
	
--Création de la table commande
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
	-- création de la table typeproduct
CREATE TABLE [dbo].[typeprofil](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL
	primary key (id)
	)
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'selectuser')
DROP PROCEDURE selectuser
GO
CREATE procedure selectuser @login nvarchar(50), @password nvarchar(50)
as
select * from "user" where "login"=@login and "password"=@password
go
--Procédure stocké Addcustomer
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'addcustomer')
DROP PROCEDURE addcustomer
GO


CREATE procedure addcustomer @firstname nvarchar(50),@lastname nvarchar(50), @birthdate date, @adresse nvarchar(100),@cite nvarchar(100),
@countrie nvarchar(100),@codep int, @email nvarchar(100),@tel int,@prof nvarchar(100)
as
insert into customer(firstname,lastname,birthdate,adresse,cite,countrie,codep,email,tel,prof) 
values   (@firstname,@lastname,@birthdate,@adresse,@cite,@countrie,@codep,@email,@tel,@prof)

go
--Procédure stocké Adduser
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Adduser')
DROP PROCEDURE Adduser
GO


CREATE procedure Adduser @login nvarchar(50),@password nvarchar(50), @typeprofil int,  @firstname nvarchar(50),@lastname nvarchar(50), @birthdate date, @adresse nvarchar(100),@cite nvarchar(100),
@countrie nvarchar(100),@codep int, @email nvarchar(100),@tel int,@prof nvarchar(100)
as
insert into [User]("login",[password],typeprofil,firstname,lastname,birthdate,adresse,cite,countrie,codep,email,tel,prof) 
values   (@login,@password,@typeprofil,@firstname,@lastname,@birthdate,@adresse,@cite,@countrie,@codep,@email,@tel,@prof)

go
--Procédure stocké addproduct
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'addproduct')
DROP PROCEDURE addproduct
GO

CREATE procedure addproduct @ref nvarchar(50),@name nvarchar(50),@description nvarchar(100),@datefab date,
@type int, @price float,@qt int
as
insert into product
select isnull(max(id),0)+1, @ref,@name,@description,@datefab,@type,@price,@qt from product
go
--Procédure stocké addtype
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'addtype')
DROP PROCEDURE addtype
GO

CREATE procedure addtype @type nvarchar(50), @description nvarchar(100)
as
insert into typeproduct 
select isnull(max(id),0)+1, @type,@description from typeproduct

go
--Procédure stocké addtype
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'addtypeprofil')
DROP PROCEDURE addtypeprofil
GO

CREATE procedure addtypeprofil @name nvarchar(50), @description nvarchar(100)
as
insert into typeprofil
select isnull(max(id),0)+1, @name,@description from typeprofil

go
--procédure stocké addcommande
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'addcommande')
DROP PROCEDURE addcommande
GO

CREATE procedure addcommande @idpdt int,@idclt int,@qt int,@datec date
as
insert into commande
select isnull(max(id),0)+1,@idpdt,@idclt,@qt,@datec from commande
go

--procédure stocké selectcustomerbyid
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'selectcustomerbyid')
DROP PROCEDURE selectcustomerbyid
GO

create procedure selectcustomerbyid @id int as
select * from customer where id=@id
go

--procédure stocké countdatatablecustomers
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'countdatatablecustomers')
DROP PROCEDURE countdatatablecustomers
GO
create procedure countdatatablecustomers @number int,@start int, @sortcolumn nvarchar(30), @tri nvarchar(10),@searchval nvarchar(100) as

DECLARE @cmd AS NVARCHAR(max)

declare @formatdate as varchar(20)

SET @searchVal = ''''+'%' +@searchval+'%'+''''

set @formatdate='''dd/mm/yyyy'''

SET @cmd = N'select count(*) nbr 

from (

select clt.*, row_number() over(order by id) as [row_number] 

from Customer clt) clt 

where row_number >0 and  Firstname like '+@searchval+' or Lastname like '+@searchval+'  or Adresse like '+@searchval+'

or Cite like '+@searchval+' or Countrie like  ' + @searchval +' or Codep like ' +@searchval+' or Email like '

+@searchval+' or Tel like '+@searchval+'or Prof like ' +@searchval+'or FORMAT(birthdate, '+ @formatdate +') like '+@searchval 

print @cmd

exec(@cmd)
go

-- procédure stocké selectcustomers
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'selectcustomers')
DROP PROCEDURE selectcustomers
GO

CREATE procedure selectcustomers @number int,@start int, @sortcolumn nvarchar(30), @tri nvarchar(10),@searchval nvarchar(100) as

DECLARE @cmd AS NVARCHAR(max)

declare @formatdate as varchar(20)

SET @searchVal = ''''+'%' +@searchval+'%'+''''

set @formatdate='''dd/mm/yyyy'''

SET @cmd = N'select top(10) * 

from (

select clt.*, row_number() over(order by ' + @sortcolumn + ' ' + @tri + ') as [row_number] 

from Customer clt) clt 

where row_number >0 and  Firstname like '+@searchval+' or Lastname like '+@searchval+'  or Adresse like '+@searchval+'

or Cite like '+@searchval+' or Countrie like  ' + @searchval +' or Codep like ' +@searchval+' or Email like '

+@searchval+' or Tel like '+@searchval+'or Prof like ' +@searchval+'or FORMAT(birthdate, '+ @formatdate +') like '+@searchval 

print @cmd

exec(@cmd)
go

--procédure stocké Deletecustomer
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Deletecustomer')
DROP PROCEDURE Deletecustomer
GO

create procedure Deletecustomer @id int as

Delete from customer where id = @id
go

--Procédure stocké updatecustomer
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'updatecustomer')
DROP PROCEDURE updatecustomer
GO

create procedure updatecustomer @id int, @firstname nvarchar(50),@lastname nvarchar(50),@birthdate date, @adresse nvarchar(100)

,@cite nvarchar(50),@countrie nvarchar(50),@codep int,@email nvarchar(50),@tel nvarchar(50),@prof nvarchar(50)

 as

update customer 

set Firstname=@firstname, Lastname = @lastname, 

Birthdate =@birthdate, adresse =@adresse, 

cite =@cite, countrie =@countrie, Codep =@codep, 

email =@email, tel =@tel, prof =@prof where id =@id

go
--Procédure stocké countcustomers
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'countcustomers')
DROP PROCEDURE countcustomers
GO
create procedure countcustomers as

select count(*) as nb from customer
go

--procédure stocké selectproductbyid
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'selectproductbyid')
DROP PROCEDURE selectproductbyid
GO

create procedure selectproductbyid @id int as

select * from product where id=@id
go
--procédure stocké Listcustomers
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Listcustomers')
DROP PROCEDURE Listcustomers
GO
create procedure Listcustomers as
select * from customer

go
--procédure stocké Listproducts
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Listproducts')
DROP PROCEDURE Listproducts
GO
create procedure Listproducts as
select p.id,ref,name,p.description,datefab,t.type as desctype,p.type,price,qt from product p 
inner join typeproduct t on t.id = p.type
go

--procédure stocké countdatatableproducts
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'countdatatableproducts')
DROP PROCEDURE countdatatableproducts
GO
CREATE procedure countdatatableproducts @number int,@start int, @sortcolumn nvarchar(30), @tri nvarchar(10),@searchval nvarchar(100) as

DECLARE @cmd AS NVARCHAR(max)

declare @formatdate as varchar(20)

SET @searchVal = ''''+'%' +@searchval+'%'+''''

set @formatdate='''dd/mm/yyyy'''



set @cmd= N'select count(*) nbr from (

select pdt.id,pdt.ref,pdt.name,pdt.description,pdt.datefab,pdt.[type],t.[type] 

as desctype,pdt.price,pdt.qt, row_number() over(order by pdt.id) as [row_number] 

from Product pdt inner join [typeproduct] t on t.id=pdt.[type]) pdt where row_number >0

and pdt.id like '+@searchVal+' or pdt.ref like'+@searchVal+' or pdt.name like '+@searchVal+' or pdt.description like '+@searchVal+'

or FORMAT(pdt.datefab, '+ @formatdate +') like '+@searchval+' or pdt.desctype like '+@searchVal+' or  pdt.price like '+@searchVal+' or pdt.qt like'+@searchVal

print @cmd

exec(@cmd)
go

--procédure stocké selectproducts
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'selectproducts')
DROP PROCEDURE selectproducts
GO

CREATE procedure selectproducts @number int,@start int, @sortcolumn nvarchar(30), @tri nvarchar(10),@searchval nvarchar(100) as

DECLARE @cmd AS NVARCHAR(max)

declare @formatdate as varchar(20)

SET @searchVal = ''''+'%' +@searchval+'%'+''''

set @formatdate='''dd/mm/yyyy'''

set @cmd= N'select top(10) * from (

select pdt.id,pdt.ref,pdt.name,pdt.description,pdt.datefab,pdt.[type],t.[type] 

as desctype,pdt.price,pdt.qt, row_number() over(order by '+@sortcolumn+' '+ @tri+') as [row_number] 

from Product pdt inner join [typeproduct] t on t.id=pdt.[type]) pdt where row_number >0

and pdt.id like '+@searchVal+' or pdt.ref like'+@searchVal+' or pdt.name like '+@searchVal+' or pdt.description like '+@searchVal+'

or FORMAT(pdt.datefab, '+ @formatdate +') like '+@searchval+' or pdt.desctype like '+@searchVal+' or  pdt.price like '+@searchVal+' or pdt.qt like'+@searchVal

print @cmd

exec(@cmd)

go

-- procédure stocké updateproduct
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'updateproduct')
DROP PROCEDURE updateproduct
GO

create procedure updateproduct @id int,@ref nvarchar(50), @Name nvarchar(50),@Description nvarchar(50),@datefab date, @Type int ,@Price float,@qt int

 as

update product 

set ref=@ref, Name=@name, description = @Description, 

datefab =@datefab, Type =@type, 

price =@Price, Qt =@qt where id =@id
go

--procédure stocké deleteproduct
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'deleteproduct')
DROP PROCEDURE deleteproduct
GO
create procedure deleteproduct @id int as
delete from product where id = @id
go

--procédure stocké countproducts
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'countproducts')
DROP PROCEDURE countproducts
GO
create procedure countproducts as
select count(*) as nb from product

go
-- procédure stocké selectusers
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'selectusers')
DROP PROCEDURE selectusers
GO

CREATE procedure selectusers @number int,@start int, @sortcolumn nvarchar(30), @tri nvarchar(10),@searchval nvarchar(100) as

DECLARE @cmd AS NVARCHAR(max)

declare @formatdate as varchar(20)

SET @searchVal = ''''+'%' +@searchval+'%'+''''

set @formatdate='''dd/mm/yyyy'''

SET @cmd = N'select top(10) * 

from (

select clt.*, row_number() over(order by ' + @sortcolumn + ' ' + @tri + ') as [row_number] 

from [User] clt where id > 1) clt 

where row_number >0 and  Firstname like '+@searchval+' or Lastname like '+@searchval+'  or Adresse like '+@searchval+'

or Cite like '+@searchval+' or Countrie like  ' + @searchval +' or Codep like ' +@searchval+' or Email like '

+@searchval+' or Tel like '+@searchval+'or Prof like ' +@searchval+'or FORMAT(birthdate, '+ @formatdate +') like '+@searchval 

print @cmd

exec(@cmd)
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'countdatatableusers')
DROP PROCEDURE countdatatableusers
GO
CREATE procedure countdatatableusers @number int,@start int, @sortcolumn nvarchar(30), @tri nvarchar(10),@searchval nvarchar(100) as

DECLARE @cmd AS NVARCHAR(max)
declare @formatdate as varchar(20)
SET @searchVal = ''''+'%' +@searchval+'%'+''''
set @formatdate='''dd/mm/yyyy'''
SET @cmd = N'select count(*) as nbr
from (
select clt.*, row_number() over(order by ' + @sortcolumn + ' ' + @tri + ') as [row_number] 
from [User] clt where id > 1) clt 
where row_number >0 and  Firstname like '+@searchval+' or Lastname like '+@searchval+'  or Adresse like '+@searchval+'
or Cite like '+@searchval+' or Countrie like  ' + @searchval +' or Codep like ' +@searchval+' or Email like '

+@searchval+' or Tel like '+@searchval+'or Prof like ' +@searchval+'or FORMAT(birthdate, '+ @formatdate +') like '+@searchval 
print @cmd
exec(@cmd)
go
--procédure stocké Deleteuser
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Deleteuser')
DROP PROCEDURE Deleteuser
GO

create procedure Deleteuser @id int as

Delete from [User] where id = @id
go

--procédure stocké selectuserbyid
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'selectuserbyid')
DROP PROCEDURE selectuserbyid
GO

create procedure selectuserbyid @id int as
select * from [User] where id=@id
go


--Procédure stocké updateuser
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'updateuser')
DROP PROCEDURE updateuser
GO

create procedure updateuser @id int, @firstname nvarchar(50),@lastname nvarchar(50),@birthdate date, @adresse nvarchar(100)

,@cite nvarchar(50),@countrie nvarchar(50),@codep int,@email nvarchar(50),@tel nvarchar(50),@prof nvarchar(50),@login nvarchar(50)
,@password nvarchar(50),@typeprofil int
 as

update [User] 

set Firstname=@firstname, Lastname = @lastname, 

Birthdate =@birthdate, adresse =@adresse, 

cite =@cite, countrie =@countrie, Codep =@codep, 

email =@email, tel =@tel, prof =@prof
,[login] = @login,[password]=@password,typeprofil=@typeprofil where id =@id

go

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'updatedesign')
DROP PROCEDURE updatedesign
GO
CREATE procedure updatedesign @id int,@design nvarchar(50),@datatable nvarchar(50),@btn nvarchar(50)  as
update design set design=@design, datatable=@datatable,btn=@btn where id_user=@id
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'getdesign')
DROP PROCEDURE getdesign
GO
Create procedure getdesign @id int as
select * from design where id_user=@id
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Listtypecomplaint')
DROP PROCEDURE Listtypecomplaint
GO
create procedure Listtypecomplaint as
select * from type_complaint
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Liststatecomplaint')
DROP PROCEDURE Liststatecomplaint
GO
create procedure Liststatecomplaint as
select * from complaint

go

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'addcomplaint')
DROP PROCEDURE addcomplaint
GO
Create procedure addcomplaint @title nvarchar(50),@description nvarchar(50),@creation_date date,

@type int,@state  int,@product nvarchar(50)

as

insert into complaint(id,title,description,type,product,Creation_date,state)

select isnull(max(id),0)+1, @title,@description,@type,@product ,@creation_date,1 from complaint
go
--procédure stocké countdatatablecustomers
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'countdatatablecomplaints')
DROP PROCEDURE countdatatablecomplaints
GO
create procedure countdatatablecomplaints @number int,@start int, @sortcolumn nvarchar(30), @tri nvarchar(10),@searchval nvarchar(100) as

DECLARE @cmd AS NVARCHAR(max)

declare @formatdate as varchar(20)

SET @searchVal = ''''+'%' +@searchval+'%'+''''

set @formatdate='''dd/mm/yyyy'''

SET @cmd = N'select count(*) nbr 

from (

select c.*, row_number() over(order by c.id) as [row_number] 

from Complaint c
inner join type_complaint t on t.id=c.type
inner join state_complaint s on s.id=c.state) clt 

where row_number >0 and  Title like '+@searchval+' or Description like '+@searchval+'  or type like '+@searchval+'

or product like '+@searchval+' or FORMAT(Creation_date, '+ @formatdate +') like  ' + @searchval +' or state like ' +@searchval

print @cmd

exec(@cmd)
go

-- procédure stocké selectcustomers
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'selectcomplaints')
DROP PROCEDURE selectcomplaints
GO

CREATE procedure selectcomplaints @number int,@start int, @sortcolumn nvarchar(30), @tri nvarchar(10),@searchval nvarchar(100) as

DECLARE @cmd AS NVARCHAR(max)

declare @formatdate as varchar(20)

SET @searchVal = ''''+'%' +@searchval+'%'+''''

set @formatdate='''dd/mm/yyyy'''

SET @cmd = N'select top(10) * 

from (

select c.*,t.type as desctype,s.state as descstate, row_number() over(order by c.' + @sortcolumn + ' ' + @tri + ') as [row_number] 

from Complaint c
inner join type_complaint t on t.id=c.type
inner join state_complaint s on s.id=c.state) clt 

where row_number >0 and  Title like '+@searchval+' or Description like '+@searchval+'  or type like '+@searchval+'

or product like '+@searchval+' or FORMAT(Creation_date, '+ @formatdate +') like  ' + @searchval +' or state like ' +@searchval
print @cmd

exec(@cmd)
go

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'updatecomplaint')
DROP PROCEDURE updatecomplaint
GO
create procedure updatecomplaint @id int, @title nvarchar(50),@description nvarchar(50),@creation_date date
,@type int,@product nvarchar(50),@state int

 as



update [Complaint] 



set title=@title, description = @description, 



creation_date =@creation_date, type =@type, 



product =@product, [State] =@State

 where id =@id


go


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'selectcomplaintbyid')
DROP PROCEDURE selectcomplaintbyid
GO

create procedure selectcomplaintbyid @id int as

select * from [Complaint] where id=@id
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'stateproduct')
DROP PROCEDURE stateproduct
GO
create procedure stateproduct as
select count(*) as nb,t.type from product p
inner join typeproduct t on p.type=t.id
group by t.type
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'TR' AND name = 'insertdesign')
DROP TRIGGER insertdesign
go
CREATE TRIGGER insertdesign ON "user"
AFTER INSERT
as
begin
insert into design
select max(id),'purple','card-header-primary','btn-primary' from "user"
end;
dbcc checkident("user",reseed,0)
go
insert into "user" 
values('admin','admin',1,'admin','','','','','','','','','')
go 
insert into typeprofil values(1,'admin','admin')
insert into typeprofil values(2,'user','user')

