-- @domain_name is one of
---- the domain name when using Active Directory
---- the computer name where the database is installed, when not using Active Directory
declare @domain_name nvarchar(max) = 'domain-name';
-- @account_name is one of
---- windows computer name, when using Active Directory
---- windows group name
---- windows user name
declare @account_name nvarchar(max) = 'GISA Users';

declare @loginname nvarchar(max) = @domain_name + N'\' + @account_name;

declare @query nvarchar(max);

if not exists (SELECT * FROM master.dbo.syslogins WHERE loginname = @loginname )
begin
	exec('CREATE LOGIN [' + @loginname + '] FROM WINDOWS WITH DEFAULT_LANGUAGE = [Português]');
end

if not exists (select * from GISA.sys.sysusers where name = @loginname)
begin
	exec('CREATE USER [' + @loginname + '] FOR LOGIN [' + @loginname +']');
end
exec('GRANT SELECT, INSERT, UPDATE, DELETE TO [' + @loginname + ']')
exec('GRANT EXECUTE TO [' + @loginname + ']')
exec('GRANT CONTROL TO [' + @loginname + ']')