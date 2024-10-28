USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[CreateEmployee]
    @EmployeeNumber INT,
    @Name NVARCHAR(255),
    @EmployeeType NVARCHAR(50)
AS
BEGIN
    DECLARE @NewId INT;
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        INSERT INTO [dbo].[CT_Employee] (EmployeeNumber, Name, EmployeeType)
        VALUES (@EmployeeNumber, @Name, @EmployeeType);
        
        SET @NewId = SCOPE_IDENTITY();
        SET @Message = 'Employee created successfully';

        SELECT @NewId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
