--create database Trucking;

use Trucking;

create or alter procedure create_tables
as
	create table Client(
	id int identity(1,1),
	clientName nvarchar(70) not null,
	constraint pk_client primary key(id)
	);
	------------------------
	create table Goods(
	id int identity(1,1),
	shippingName nvarchar(70) not null,
	weightGoods int not null,
	constraint pk_goods primary key(id)
	);
	------------------------
	create table City(
	id int identity(1,1),
	city nvarchar(30) not null,
	constraint pk_city primary key(id)
	);
	------------------------
	create table Driver(
	id int identity(1,1) unique,
	lastName nvarchar(30) not null,
	firstName nvarchar(30) not null,
	driversLicenseNumber int not null,
	category nvarchar(10) not null,
	salary money not null
	) on [primary];
	------------------------
	create table Transport(
	id int identity(1,1),
	transportNumber nchar(7) not null,
	capacity int not null,	
	idDriver int not null,
	constraint pk_trucks  primary key(id),
	constraint fk_driver foreign key (idDriver) references Driver(id) on delete cascade
	);
	------------------------
	create table Carriage(
	id int identity(1,1),
	idClient int not null,
	idGoods int not null,
	idTransport int not null,
	idCity int not null,
	dateOfDelivery datetime not null,
	typeOfService nvarchar(60) not null,
	constraint check_type check (typeofService = 'Автомобильная' or typeofService = 'Грузовая'),
	constraint pk_carriage primary key(id),
	constraint fk_client foreign key (idClient) references Client(id) on delete cascade,
	constraint fk_goods foreign key (idGoods) references Goods(id) on delete cascade,
	constraint fk_trucks foreign key (idTransport) references Transport(id) on delete cascade,
	constraint fk_city foreign key (idCity) references City(id) on delete cascade
	);
	------------------------
	create table History(
		id INT IDENTITY(1,1) PRIMARY KEY,
		carriageId INT NOT NULL,
		operation NVARCHAR(200) NOT NULL,
		createAt DATETIME NOT NULL DEFAULT GETDATE(),
	);
go

create or alter procedure seed
as
	insert into Client (clientName) values ('Andrew');
	insert into Client (clientName) values ('Anton');
	insert into Client (clientName) values ('Sergey');
	insert into Client (clientName) values ('Olga');
	insert into Client (clientName) values ('Uri');

	insert into Goods (shippingName, weightGoods) values ('Ace-creame', 2);
	insert into Goods (shippingName, weightGoods) values ('meat', 3);
	insert into Goods (shippingName, weightGoods) values ('eggs', 1);
	insert into Goods (shippingName, weightGoods) values ('water', 5);
	insert into Goods (shippingName, weightGoods) values ('Milk', 2);

	insert into City (city) values ('Minsk');
	insert into City (city) values ('Brest');
	insert into City (city) values ('Vitebsk');
	insert into City (city) values ('Grodno');
	insert into City (city) values ('Gomel');

	insert into Driver (lastName, firstName, driversLicenseNumber, category, salary) values ('Onill', 'Katy', 10, 'B', 1000.2);
	insert into Driver (lastName, firstName, driversLicenseNumber, category, salary) values ('McKey', 'Ylia', 20, 'B', 900.9);
	insert into Driver (lastName, firstName, driversLicenseNumber, category, salary) values ('Bush', 'Marina', 30, 'B', 1500.3);
	insert into Driver (lastName, firstName, driversLicenseNumber, category, salary) values ('Freeman', 'Kris', 40, 'B', 3000.44);
	insert into Driver (lastName, firstName, driversLicenseNumber, category, salary) values ('Bob', 'Poly', 50, 'B', 1020.1);

	insert into Transport (transportNumber, capacity, idDriver) values ('as123', 1000, 1);
	insert into Transport (transportNumber, capacity, idDriver) values ('gsf54', 1777, 2);
	insert into Transport (transportNumber, capacity, idDriver) values ('23fdb', 400, 3);
	insert into Transport (transportNumber, capacity, idDriver) values ('5hyj7', 1100, 4);
	insert into Transport (transportNumber, capacity, idDriver) values ('sg54d', 3339, 5);

	insert into Carriage (idCity, idClient, idGoods, idTransport, dateOfDelivery, typeOfService) values (1, 1, 1, 1, '12-21-16', 'Автомобильная');
	insert into Carriage (idCity, idClient, idGoods, idTransport, dateOfDelivery, typeOfService) values (2, 2, 2, 2, '12-21-17', 'Грузовая');
	insert into Carriage (idCity, idClient, idGoods, idTransport, dateOfDelivery, typeOfService) values (3, 3, 3, 3, '12-21-18', 'Грузовая');
	insert into Carriage (idCity, idClient, idGoods, idTransport, dateOfDelivery, typeOfService) values (4, 4, 4, 4, '12-21-19', 'Автомобильная');
	insert into Carriage (idCity, idClient, idGoods, idTransport, dateOfDelivery, typeOfService) values (5, 5, 5, 5, '12-21-20', 'Автомобильная');
go

create or alter procedure select_data
as
	select * from Client;
	select * from Goods;
	select * from City;
	select * from Driver;
	select * from Transport;
	select * from Carriage;
	select * from History;
go

create or alter procedure drop_tables
as
	drop table Carriage;
	drop table Transport;
	drop table Driver;
	drop table Client;
	drop table Goods;
	drop table City;
	drop table History;
go

exec drop_tables;
exec create_tables;
exec seed;
exec select_data;


create or alter procedure GetInfoCarriage
as
select cl.clientName[Имя клиента], g.shippingName[Поставка], g.weightGoods[вес поставки], 
d.lastName[Фамилия доставщика], d.firstName [Иия доставщика], city.city[В город], dateOfDelivery[дата поставки] from 
Client cl join Carriage car on cl.id = car.idClient 
join Goods g on g.id = car.idGoods
join Transport t on t.id = car.idTransport 
join Driver d on d.id = t.idDriver
join City city on city.id = car.idCity
go
exec GetInfoCarriage

create or alter view InfoAboutCarrage
as
select cl.clientName[Имя клиента], g.shippingName[Поставка], g.weightGoods[вес поставки], 
d.lastName[Фамилия доставщика], d.firstName [Иия доставщика], city.city[В город], dateOfDelivery[дата поставки] from 
Client cl join Carriage car on cl.id = car.idClient 
join Goods g on g.id = car.idGoods
join Transport t on t.id = car.idTransport 
join Driver d on d.id = t.idDriver
join City city on city.id = car.idCity
go
select * from InfoAboutCarrage;

create nonclustered index IX_Carriage on Carriage (dateOfDelivery);

create or alter procedure [dbo].[AddCarriage]
	@idClient int,
	@idGoods int,
	@idTransport int,
	@idCity int,
	@dateOfDelivery datetime,
	@typeOfService nvarchar(60)
as
declare @weight int;
declare @capacity int;
select @weight = weightGoods, @capacity = capacity from Goods, Transport where @idGoods = Goods.id and @idTransport = Transport.id;
if(@weight < @capacity)
insert into Carriage (idClient, idGoods, idTransport,idCity, dateOfDelivery, typeOfService) 
select @idClient, @idGoods, @idTransport, @idCity, @dateOfDelivery, @typeOfService
if(@weight > @capacity)
print N'Вес для перевозки не может быть больше грузоподъемности грузовика';
select id, idClient, idGoods, idTransport, idCity, dateOfDelivery, typeOfService from Carriage where id = SCOPE_IDENTITY()
go
execute [dbo].[AddCarriage] @idClient = 1, @idGoods = 2,  @idTransport = 3, @idCity = 4, @dateOfDelivery = '2022-02-22', @typeOfService = 'Автомобильная'
--raise error
insert into Goods (shippingName, weightGoods) values ('Beton', 401);
execute [dbo].[AddCarriage] @idClient = 2, @idGoods = 6,  @idTransport = 3, @idCity = 5, @dateOfDelivery = '2022-02-22', @typeOfService = 'Грузовая'

create or alter procedure GetAllCarriage
as
select idClient, idGoods, idTransport, idCity, dateOfDelivery, typeOfService from Carriage
go
exec GetAllCarriage

create or alter procedure InsertIntoClient
@clientName nvarchar(70)
as 
insert into Client(clientName) values(@clientName);
go

execute InsertIntoClient @clientName = 'Dima'

create trigger carriage_insert on Carriage after insert
as 
insert into History (carriageId, operation) select id , 'Добавлена поставка' from inserted
go
insert into Carriage (idCity, idClient, idGoods, idTransport, dateOfDelivery, typeOfService) values (5, 4, 3, 2, '12-21-20', 'Автомобильная');

select * from History;
drop table History;