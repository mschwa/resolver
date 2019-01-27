CREATE USER IdentityApp FOR LOGIN IdentityApp WITH DEFAULT_SCHEMA = dbo;
GO

GRANT CONNECT TO IdentityApp;
GO

EXEC sp_addrolemember @rolename = N'ResolverApp', @membername = N'IdentityApp';
GO