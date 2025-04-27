CREATE DATABASE DynamicConfigDb;
GO
USE DynamicConfigDb;
GO
CREATE TABLE Configurations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Type NVARCHAR(50),
    Value NVARCHAR(MAX),
    IsActive BIT,
    ApplicationName NVARCHAR(100)
);
GO
INSERT INTO Configurations (Name, Type, Value, IsActive, ApplicationName)
VALUES ('ApiBaseUrl', 'string', 'https://service-b.example.com', 1, 'SERVICE-B');
GO
