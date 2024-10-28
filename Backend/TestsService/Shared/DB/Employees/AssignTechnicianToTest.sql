USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[AssignTechnicianToTest]
    @TestId INT,
    @EmployeeId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Verifica si la prueba existe
        IF NOT EXISTS (SELECT 1 FROM [dbo].[IX_Test_Technicians] WHERE [TestId] = @TestId)
        BEGIN
            SET @Message = 'Test not found';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Verifica si el empleado es un técnico
        IF NOT EXISTS (SELECT 1 FROM [dbo].[CT_Employee] WHERE [Id] = @EmployeeId AND [EmployeeType] = 'TECHNICIAN')
        BEGIN
            SET @Message = 'Technician not found';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Inserta en la tabla IX_Test_Technicians si ambos existen
        INSERT INTO [dbo].[IX_Test_Technicians] ([TestId], [EmployeeId])
        VALUES (@TestId, @EmployeeId);

        SET @Message = 'Technician assigned to test successfully';
        SELECT @EmployeeId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
