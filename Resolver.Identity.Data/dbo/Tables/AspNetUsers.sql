﻿/* Account */
CREATE TABLE dbo.AspNetUsers (
  Id NVARCHAR(450) NOT NULL
 ,AccessFailedCount INT NOT NULL
 --,CardHolderName NVARCHAR(MAX) NULL -- NOT NULL
 --,CardNumber NVARCHAR(MAX) NULL -- NOT NULL
 --,CardType INT NULL -- NOT NULL
 --,City NVARCHAR(MAX) NULL -- NOT NULL
 ,ConcurrencyStamp NVARCHAR(MAX) NULL
 --,Country NVARCHAR(MAX) NULL -- NOT NULL
 ,Email NVARCHAR(256) NULL
 ,EmailConfirmed BIT NOT NULL
 --,Expiration NVARCHAR(MAX) NOT NULL
 --,LastName NVARCHAR(MAX) NULL -- NOT NULL
 ,LockoutEnabled BIT NOT NULL
 ,LockoutEnd DATETIMEOFFSET(7) NULL
 --,Name NVARCHAR(MAX) NULL -- NOT NULL
 ,NormalizedEmail NVARCHAR(256) NULL
 ,NormalizedUserName NVARCHAR(256) NULL
 ,PasswordHash NVARCHAR(MAX) NULL
 ,PhoneNumber NVARCHAR(MAX) NULL
 ,PhoneNumberConfirmed BIT NULL -- NOT NULL
 --,SecurityNumber NVARCHAR(MAX) NOT NULL
 ,SecurityStamp NVARCHAR(MAX) NULL
 --,State NVARCHAR(MAX) NULL -- NOT NULL
 --,Street NVARCHAR(MAX) NULL -- NOT NULL
 ,TwoFactorEnabled BIT NOT NULL
 ,UserName NVARCHAR(256) NULL
 --,ZipCode NVARCHAR(MAX) NULL -- NOT NULL
 ,CONSTRAINT PK_AspNetUsers PRIMARY KEY CLUSTERED (Id ASC)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX UserNameIndex
ON dbo.AspNetUsers (NormalizedUserName ASC)
WHERE (NormalizedUserName IS NOT NULL);
GO

CREATE NONCLUSTERED INDEX EmailIndex ON dbo.AspNetUsers (NormalizedEmail ASC);
GO
