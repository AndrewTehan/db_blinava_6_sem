use Trucking
alter database Trucking set trustworthy on
go

sp_configure 'show advanced options', 1;
go
reconfigure
go 

sp_configure 'clr enabled', 1;
go
reconfigure
go
 
sp_configure 'show advanced options', 0;
go

drop procedure AllGoods;
drop procedure send_email;
drop procedure CityArea;
drop procedure CityCoverage;
drop type dbo.[Route];
drop assembly StudentAssenbly;
create assembly StudentAssenbly
from 'C:\Users\Andrew\Desktop\6_sem\ад\3\lab3\bin\Debug\lab3.dll' 
WITH PERMISSION_SET = UNSAFE;
go

create or alter procedure AllGoods
as
	external name StudentAssenbly.[lab3.StoredProcedure].AllGoods
go

create or alter procedure send_email
as
	external name StudentAssenbly.[lab3.StoredProcedure].send_email
go

create or alter procedure CityArea(@city geometry)
as
	external name StudentAssenbly.[lab3.StoredProcedure].CityArea
go

create or alter procedure CityCoverage
as
	external name StudentAssenbly.[lab3.StoredProcedure].CityCoverage
go

exec AllGoods;
insert into Goods(shippingName, weightGoods) values ('to delete', 000);
delete from Goods where shippingName = 'to delete';

exec send_email;

declare @geom geometry = (select ogr_geometry from gadm40_blr_1 where ogr_fid = 3)
select @geom
exec CityArea @geom

exec CityCoverage

create trigger send_on_delete
on Goods
after delete
as exec send_email;
	
create TYPE dbo.[Route]
	external name StudentAssenbly.[Route]
go

declare @route route = 'Minsk,Moscow';
select @route.ToString();
go

