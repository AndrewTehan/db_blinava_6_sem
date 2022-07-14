use test;

select * from world_countries;
select * from world_cities;

drop table region;
create table region(
	id int identity(0, 1) primary key,
	publName nvarchar(255),
	region int
);

alter table regionTest add foreign key (region) references world_countries(ogr_fid);

insert into region values('Belarus', 28);
insert into region values('Germany', 88);
select rT.publName, wc.ogr_geometry from region rT left join world_countries wc on rT.region = wc.ogr_fid;

--объединение
select * from world_cities where ogr_fid = 1;
select country, ogr_geometry from World_Countries where ogr_fid = 189;

declare @g3 geometry; 
select @g3 = ogr_geometry from world_cities where ogr_fid = 1;
declare @g4 geometry; 
select @g4 = ogr_geometry from world_countries where ogr_fid = 189;
select @g4.STContains(@g3) as [пересечение];
drop procedure checkContains;
go
create procedure checkContains(@country int, @city int, @result binary output)
as
begin
	declare @firstid geometry; 
	select @firstid = ogr_geometry from world_countries where ogr_fid = @country;
	declare @secondid geometry; 
	select @secondid = ogr_geometry from world_cities where ogr_fid = @city;
	select @result = @firstid.STContains(@secondid);
	return;
end;

--distance
select name, country from world_cities where ogr_fid = 16;
select name, country from world_cities where ogr_fid = 17;
select name, country from world_cities where ogr_fid = 18;

drop procedure checkDistance;
go
create procedure checkDistance(@cityN1 geometry, @cityN2 geometry, @cityN3 geometry)
as
begin
	declare @res1 float;
	select @res1 = @cityN1.STDistance(@cityN2); 
	declare @res2 float;
	select @res2 = @cityN1.STDistance(@cityN3);
	if(@res1 < @res2)
	 begin
		print @res1;
	 end;
	else print @res2;
end;

go
declare @city1 geometry; 
select @city1 = ogr_geometry from world_cities where ogr_fid = 16;
declare @city2 geometry; 
select @city2 = ogr_geometry from world_cities where ogr_fid = 17;
declare @city3 geometry;
select @city3 = ogr_geometry from world_cities where ogr_fid = 18;
select @city1.STDistance(@city3);
select @city1.STDistance(@city2);
exec checkDistance @city1, @city2, @city3;

--
declare @newGeometry geometry = 'POLYGON((73 55, 73 75, 100 100, 73 55))';
update world_cities set ogr_geometry = @newGeometry where name like 'Omsk';
select name, ogr_geometry from world_cities where name like 'Omsk';
--POINT (73.250289916992188 55.063304901123047)

drop procedure updateGeometry;
go
create or alter procedure updateGeometry(@newGeom nvarchar(255), @name nvarchar(255))
as
begin
	declare @newGeometry geometry;
	set @newGeometry = 'POLYGON((' +@newGeom+'))';
	update world_cities set ogr_geometry = @newGeometry where name like @name;
end;

exec updateGeometry '73 55, 93 85, 100 100, 73 55', 'Omsk';
select name, ogr_geometry from world_cities where name like 'Omsk';

--
create spatial index i_spatial_shape
on world_countries(ogr_geometry) using geometry_grid
with (bounding_box = (xmin=0, ymin=0, xmax=500, ymax=500),
	grids = (LEVEL_3= HIGH, LEVEL_2 = HIGH ));
