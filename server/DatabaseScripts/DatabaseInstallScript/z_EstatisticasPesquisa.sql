if exists (select * from sys.objects where object_id = object_id(N'dbo.EstatisticaPesquisa'))
	drop table dbo.EstatisticaPesquisa
go
create table dbo.EstatisticaPesquisa (
  ID bigint not null,
  CatCode nchar(2) not null check (CatCode in ('OD', 'UI', 'UF')),

  UserID bigint null,

  AccessDateTime datetime not null,
  AccessMethod nchar(1) not null check (AccessMethod in ('W', 'D')),
  IPAddress nvarchar(max) null,
);
go
create clustered index EstatisticaPesquisa_IX on dbo.EstatisticaPesquisa (AccessDateTime)
go
if exists (select * from sys.objects where object_id = object_id(N'dbo.EstatisticaAcessosCount'))
	drop function dbo.EstatisticaAcessosCount
go
create function dbo.EstatisticaAcessosCount(@From datetime, @To datetime) returns table as
	return
	with A (YYYY, MM, DD, HH, ID, CatCode, UserID, AccessMethod, IPAddress) as
	(
	select distinct datepart(year, AccessDateTime) YYYY, datepart(month, AccessDateTime) MM, datepart(day, AccessDateTime) DD, datepart(hour, AccessDateTime) HH,
	ID, CatCode, UserID, AccessMethod, IPAddress
	from EstatisticaPesquisa
	where AccessDateTime between @From and @To
	)
	select count(*) N, CatCode, AccessMethod from A group by CatCode, AccessMethod;
go
if exists (select * from sys.objects where object_id = object_id(N'dbo.EstatisticaAcessosTopTen'))
	drop function dbo.EstatisticaAcessosTopTen
go
create function dbo.EstatisticaAcessosTopTen(@From datetime, @To datetime, @CatCode nchar(2)) returns table as
	return
	with A (YYYY, MM, DD, HH, ID, CatCode, UserID, AccessMethod, IPAddress) as
	(
	select distinct datepart(year, AccessDateTime) YYYY, datepart(month, AccessDateTime) MM, datepart(day, AccessDateTime) DD, datepart(hour, AccessDateTime) HH,
	ID, CatCode, UserID, AccessMethod, IPAddress
	from EstatisticaPesquisa
	where AccessDateTime between @From and @To
	)
	select top 10 count(*) N, ID from A where CatCode = @CatCode group by ID, CatCode order by 1 desc, ID
go
if exists (select * from sys.objects where object_id = object_id(N'dbo.EstatisticaAcessosTopTenParciais'))
	drop function dbo.EstatisticaAcessosTopTenParciais
go
create function dbo.EstatisticaAcessosTopTenParciais(@From datetime, @To datetime, @ID bigint, @CatCode nchar(2)) returns table as
	return
	with A (YYYY, MM, DD, HH, ID, CatCode, UserID, AccessMethod, IPAddress) as
	(
	select distinct datepart(year, AccessDateTime) YYYY, datepart(month, AccessDateTime) MM, datepart(day, AccessDateTime) DD, datepart(hour, AccessDateTime) HH,
	ID, CatCode, UserID, AccessMethod, IPAddress
	from EstatisticaPesquisa
	where AccessDateTime between @From and @To
	)
	select count(*) N, AccessMethod from A where ID = @ID and CatCode = @CatCode group by AccessMethod
go
if exists (select * from sys.objects where object_id = object_id(N'dbo.EstatisticaPesquisaSimplificar'))
	drop procedure dbo.EstatisticaPesquisaSimplificar
go
create procedure dbo.EstatisticaPesquisaSimplificar as
	delete from A
	from EstatisticaPesquisa A
	inner join EstatisticaPesquisa B
	on A.ID = B.ID and A.CatCode = B.CatCode and A.UserID = B.UserID and A.AccessMethod = B.AccessMethod and A.IPAddress = B.IPAddress
	and datepart(year, A.AccessDateTime) = datepart(year, B.AccessDateTime) 
	and datepart(month, A.AccessDateTime) = datepart(month, B.AccessDateTime) 
	and datepart(day, A.AccessDateTime) = datepart(day, B.AccessDateTime) 
	and datepart(hour, A.AccessDateTime) = datepart(hour, B.AccessDateTime) 
	where A.AccessDateTime < B.AccessDateTime;
go