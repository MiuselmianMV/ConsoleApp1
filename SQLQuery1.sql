create table [students]
(Id int identity(1,1) not null primary key,
FirstName nvarchar(50) not null,
LastName nvarchar(50) not null,
BirthDate date not null)