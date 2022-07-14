use TechnicsParkService

--1.	таблица Report
create table Report (
id INTEGER primary key identity(1,1),
xml_column XML
);

select * from Report

---
select u.nickname, c.name, p.capacity, GETDATE()
from parks p inner join companies c 
				on p.company_id = c.id
			 inner join users u
				on c.user_id = u.id
---

-- 2.	Генерация XML
go
create procedure gXML
as
declare @gen XML
set @gen = (select u.nickname as [пользователь], c.name as [компания], p.capacity as [размер парка], GETDATE() as [дата]
			from parks p inner join companies c on p.company_id = c.id
						 inner join users u
							on c.user_id = u.id
	FOR XML AUTO);
	SELECT @gen
go

execute gXML;

-- 3.	вставки XML в таблицу 
create procedure InsReport
as
DECLARE  @ins XML  
SET @ins = (select u.nickname as [пользователь], c.name as [компания], p.capacity as [размер парка], GETDATE() as [дата]
			from parks p inner join companies c on p.company_id = c.id
						 inner join users u
							on c.user_id = u.id
for xml raw);
insert into Report values(@ins);
go
  
  execute InsReport
  select * from Report;

--4.	индекс над XML-столбцом 
create primary xml index IndexOnRep on Report(xml_column)

create xml index xmlIndexS on Report(xml_column) using xml index  IndexOnRep for path

--5.	извлечение 
go
create procedure SelectRep
as
select xml_column.query('/row')
	as[xml_column]
	from Report
	for xml auto, type;
go

execute SelectRep