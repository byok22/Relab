USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[UpdateEmployee]
    @EmployeeId INT,
    @EmployeeNumber INT,
    @Name NVARCHAR(255),
    @EmployeeType NVARCHAR(50)
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        UPDATE [dbo].[CT_Employee]
        SET EmployeeNumber = @EmployeeNumber,
            Name = @Name,
            EmployeeType = @EmployeeType
        WHERE Id = @EmployeeId;

        SET @Message = 'Employee updated successfully';
        SELECT @EmployeeId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
