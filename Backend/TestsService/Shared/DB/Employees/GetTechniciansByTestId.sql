USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[GetTechniciansByTestId]
    @TestId INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[IX_Test_Technicians] WHERE [TestId] = @TestId)
        BEGIN
            SELECT IX.Id, IX.TestId, IX.EmployeeId, E.Name, E.EmployeeNumber
            FROM [dbo].[IX_Test_Technicians] IX
            JOIN [dbo].[CT_Employee] E ON IX.EmployeeId = E.Id
            WHERE IX.TestId = @TestId AND E.EmployeeType = 'TECHNICIAN';
        END
        ELSE
        BEGIN
            SELECT NULL AS id, 'No technicians assigned to this test' AS message;
        END
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
