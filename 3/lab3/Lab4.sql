drop table ComplexesRegions;

create table ComplexesRegions(
	id int identity(0, 1) primary key,
	RegionName nvarchar(255),
	Coords GEOGRAPHY
);
	
CREATE SPATIAL INDEX [SpatialIndex-20220426-120003] ON [dbo].[ComplexesRegions]
(
	[Coords]
)USING  GEOGRAPHY_GRID 
WITH (GRIDS =(LEVEL_1 = MEDIUM,LEVEL_2 = MEDIUM,LEVEL_3 = MEDIUM,LEVEL_4 = MEDIUM), 
CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

select * from ComplexesRegions;

DROP INDEX [SpatialIndex-20220426-120003] ON [dbo].[ComplexesRegions]
GO

alter table City add foreign key (address_id) references ComplexesRegions(id);
alter table City add address_id int;

select * from City

go
declare @JSON nvarchar(max)
select @JSON = BulkColumn from openrowset (bulk '/complexes.json', single_clob) as json

insert into ComplexesRegions (RegionName, Coords)
select
    Name,
	geography::STPolyFromText(concat('POLYGON ((', Coordinates, '))'), 4326) as GEOGRAPHY
from openjson(@JSON, '$')
with
(
	Name nvarchar(255) '$.Name',
    Coordinates nvarchar(max) '$.Coordinates'
)

select * from ComplexesRegions


go
create procedure GetDistance (@from nvarchar(255), @to nvarchar(255), 
								@distance float output)
as
begin
	DECLARE @fromd geography;  
	DECLARE @tod geography;  
	SELECT @fromd = Coords FROM ComplexesRegions WHERE RegionName = @from;  
	SELECT @tod = Coords FROM ComplexesRegions WHERE RegionName = @to;  
	if (@fromd is null or @tod is null)
		return -1;
	SELECT @distance = @fromd.STDistance(@tod) / 1000;
	return;
end;

go

create or alter procedure GetUnion (@first nvarchar(255), @second nvarchar(255), 
								@union nvarchar(255) output)
as
begin
	DECLARE @firstd geography;  
	DECLARE @secondd geography;  
	DECLARE @result geography;  
	SELECT @firstd = Coords FROM ComplexesRegions WHERE RegionName = @first;  
	SELECT @secondd = Coords FROM ComplexesRegions WHERE RegionName = @second;
	if (@firstd is null or @secondd is null)
		return null;
	SELECT @result = @firstd.MakeValid().STUnion(@secondd.MakeValid()).MakeValid();
	SELECT @union = @result.STAsText();
	return;
end;

drop procedure GetUnion


create or alter procedure GetUniont (@first nvarchar(255), @second nvarchar(255), 
								@union geography output)
as
begin
	DECLARE @firstd geography;  
	DECLARE @secondd geography;  
	DECLARE @result geography;  
	SELECT @firstd = Coords FROM ComplexesRegions WHERE RegionName = @first;  
	SELECT @secondd = Coords FROM ComplexesRegions WHERE RegionName = @second;
	if (@firstd is null or @secondd is null)
		return null;
	SELECT @result = @firstd.MakeValid().STUnion(@secondd.MakeValid()).MakeValid();
	SELECT @union = @result;
	return;
end;

declare @unionOfMinskAndDinamo geography;

exec GetUniont 'Misnk arena', 'Dinamo arena', @unionOfMinskAndDinamo output;

select @unionOfMinskAndDinamo as [Union];

select * from ComplexesRegions where id = 1;


select Coords.STGeometryN(1).STAsText() from ComplexesRegions

declare @distanceMinskAndDinamo float;

exec GetDistance 'Misnk arena', 'Dvorets sporta', @distanceMinskAndDinamo output;

select @distanceMinskAndDinamo as [Distance (km)];

go
	create procedure UpdateArea (@name nvarchar(255), @coords nvarchar(255))
	as
	begin
	update ComplexesRegions set Coords =  geography::STPolyFromText(concat('POLYGON ((', @coords, '))'), 4326) where RegionName = @name;
	end
go	


select * from ComplexesRegions;

exec UpdateArea 'Dvorets sporta', '27.548877596855164 53.910253945071, 27.550610303878784 53.910253945071, 27.550610303878784 53.91113555339198, 27.548877596855164 53.91113555339198, 27.548877596855164 53.910253945071' 

select * from ComplexesRegions;

