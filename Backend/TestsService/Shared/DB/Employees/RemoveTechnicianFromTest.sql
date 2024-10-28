USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[RemoveTechnicianFromTest]
    @TestId INT,
    @EmployeeId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Verifica si la relación existe en IX_Test_Technicians
        IF NOT EXISTS (SELECT 1 FROM [dbo].[IX_Test_Technicians] WHERE [TestId] = @TestId AND [EmployeeId] = @EmployeeId)
        BEGIN
            SET @Message = 'Technician not assigned to the specified test';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Elimina la relación en la tabla IX_Test_Technicians
        DELETE FROM [dbo].[IX_Test_Technicians]
        WHERE [TestId] = @TestId AND [EmployeeId] = @EmployeeId;

        SET @Message = 'Technician removed from test successfully';
        SELECT @EmployeeId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
