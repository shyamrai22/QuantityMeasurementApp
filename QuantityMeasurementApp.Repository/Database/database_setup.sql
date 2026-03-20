create database QuantityMeasurementDB;
go

use QuantityMeasurementDB;

create table Measurements
(
	Id int identity(1,1) primary key,
	Operation varchar(50), 
	Operand1 varchar(100),
	Operand2 varchar(100),
	Result varchar(100),
	MeasurementType varchar(50),
	HasError bit default 0,
	ErrorMessage varchar(100),
	CreatedAt datetime default getdate()
)

select * from Measurements;