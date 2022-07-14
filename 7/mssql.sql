use TechnicsParkService

--1.	������� Report
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

-- 2.	��������� XML
go
create procedure gXML
as
declare @gen XML
set @gen = (select u.nickname as [������������], c.name as [��������], p.capacity as [������ �����], GETDATE() as [����]
			from parks p inner join companies c on p.company_id = c.id
						 inner join users u
							on c.user_id = u.id
	FOR XML AUTO);
	SELECT @gen
go

execute gXML;

-- 3.	������� XML � ������� 
create procedure InsReport
as
DECLARE  @ins XML  
SET @ins = (select u.nickname as [������������], c.name as [��������], p.capacity as [������ �����], GETDATE() as [����]
			from parks p inner join companies c on p.company_id = c.id
						 inner join users u
							on c.user_id = u.id
for xml raw);
insert into Report values(@ins);
go
  
  execute InsReport
  select * from Report;

--4.	������ ��� XML-�������� 
create primary xml index IndexOnRep on Report(xml_column)

create xml index xmlIndexS on Report(xml_column) using xml index  IndexOnRep for path

--5.	���������� 
go
create procedure SelectRep
as
select xml_column.query('/row')
	as[xml_column]
	from Report
	for xml auto, type;
go

execute SelectRep