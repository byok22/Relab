USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[DeleteEmployee]
    @EmployeeId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        DELETE FROM [dbo].[CT_Employee]
        WHERE [Id] = @EmployeeId;

        SET @Message = 'Employee deleted successfully';
        SELECT @EmployeeId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
