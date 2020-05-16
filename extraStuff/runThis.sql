begin tran
use starrexam;
drop table if exists bookings;
drop table if exists users;
drop table if exists rooms;
create table users(
userName varchar(50) not null primary key, 
[password] varchar(50) not null, 
userType varchar(50) not null);

insert into users(userName, [password], userType)
values ('admin1','hello','admin'),
('jonny','hello','customer'),
('jacky', 'hello', 'customer')
;

create table rooms(
roomNumber smallint primary key,  
price smallint not null);

DECLARE @i int = 100
WHILE @i <= 199 
BEGIN
    insert into rooms (roomNumber, price)
	values (@i, 0);
	   SET @i = @i + 1;
END

set @i=200;
WHILE @i <= 299 
BEGIN
    insert into rooms (roomNumber, price)
	values (@i, @i-150);
	    SET @i = @i + 1;
END

set @i=300;
WHILE @i <= 399 
BEGIN
    insert into rooms (roomNumber, price)
	values (@i, @i+1000);
	    SET @i = @i + 1;
END


create table bookings(
bookingId varchar(20) not null primary key,
roomNumber smallint not null foreign key references rooms(roomNumber),
userName varchar(50) not null foreign key references users(userName),
starting date not null,
ending date not null);

insert into bookings (bookingId, roomNumber, userName, starting, ending)
values('NWQ5llKf1nzJweadJ2sm', 102, 'admin1', '20200101', '20200110'),
('zalTFvH7MWbsqcHaog4Q', 201, 'jonny', '20200101', '20200110'),
('0gND8x0PdLVcOhXm0tXr', 301, 'jacky', '20200101', '20200110'),
('FRxI2O561xZWJnY56FO3',102,  'admin1', '20201001', '20201011'),
('lTQPuUnUPhOhjzWzR3bb',201,'jonny', '20201001', '20201011'),
('oocy8FIAYzrLkkCZzB0p', 301, 'jacky','20201001', '20201011'); ;
commit