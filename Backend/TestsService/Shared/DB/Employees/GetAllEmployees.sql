USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[GetAllEmployees]
AS
BEGIN
    SELECT Id, EmployeeNumber, Name, EmployeeType
    FROM [dbo].[CT_Employee];
END
GO
