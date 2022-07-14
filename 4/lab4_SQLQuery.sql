use Trucking;

--add column
alter table city add region int;

select ogr_geometry from gadm40_blr_1 where ogr_fid = 2

insert into City (city, region)	values ('minsk', 1), ('gomel', 2), ('grodno', 5)

--intersection
DECLARE @Ig geometry = geometry::STGeomFromText('POLYGON((0 0, 0 2, 2 2, 2 0, 0 0))', 0),
		@Ih geometry = geometry::STGeomFromText('POLYGON((-5 -5, -5 5, 5 5, 5 -5, -5 -5))', 0);    

SELECT @Ig.STIntersection(@Ih).ToString();

--exclusion
DECLARE @Dg geometry = geometry::STGeomFromText('POLYGON((0 0, 2 2, 2 2, 2 0, 0 0))', 0);;  
DECLARE @Dh geometry = geometry::STGeomFromText('POLYGON((1 1, 3 1, 3 3, 3 3, 1 1))', 0);;    

SELECT @Dg.STDifference(@Dh).ToString();

--
declare @g1 geometry = (select ogr_geometry from gadm40_blr_1 where ogr_fid = 1); 
declare @g2 geometry = (select ogr_geometry from gadm40_blr_1 where ogr_fid = 2); 
select @g1.STIntersects(@g2) as [Пересеклось], @g1.STCrosses(@g2) as [Объединилось], @g1.STContains(@g2) as [Исключилось];
go

--distance between 2 object
drop view distance;
create view distance as
	select ogr_fid, ogr_geometry, c.id as city_id from City c inner join gadm40_blr_1 g on c.region = g.ogr_fid; 
go
--select * from distance;

declare @org1 geometry = (select ogr_geometry.STAsText() from distance where ogr_fid = 1);
declare @org2 geometry = (select ogr_geometry.STAsText() from distance where ogr_fid = 5);;
declare @dist float = @org1.STDistance(@org2);

select 
	@dist as 'Distance', 
	(select city_id from distance where ogr_fid=1) as 'city1',
	city_id as 'city2'
	from distance where ogr_fid=5;

--nearest city 
 select ps.id 'city1', _ps.id 'city2'
		from city ps
			inner join city _ps on ps.id = 1
			inner join gadm40_blr_1 gb on _ps.region = gb.ogr_fid where gb.ogr_fid between 0 and 1; 

-- The area covered by the city
select region as 'region', id as 'city id', (select ogr_geometry from gadm40_blr_1 where ogr_fid = ps.region).STArea() 'area'
	from city ps join gadm40_blr_1 gb on ps.region = gb.ogr_fid
	where ps.region = 1;

-- City coverage map
select * from city ps inner join gadm40_blr_1 gb on ps.region = gb.ogr_fid


-- spatial index
drop index  SIndx_SpatialTable_geometry_col1 on gadm40_blr_1;

create spatial index SIndx_SpatialTable_geometry_col1
   on gadm40_blr_1(ogr_geometry)
   with ( BOUNDING_BOX = ( 0, 0, 50, 50 ) );

select ogr_geometry from gadm40_blr_1;

