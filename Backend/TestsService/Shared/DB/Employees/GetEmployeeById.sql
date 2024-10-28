USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[GetEmployeeById]
    @EmployeeId INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[CT_Employee] WHERE [Id] = @EmployeeId)
        BEGIN
            SELECT * 
            FROM [dbo].[CT_Employee]
            WHERE [Id] = @EmployeeId;
        END
        ELSE
        BEGIN
            SELECT NULL AS id, 'Employee not found' AS message;
        END
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
