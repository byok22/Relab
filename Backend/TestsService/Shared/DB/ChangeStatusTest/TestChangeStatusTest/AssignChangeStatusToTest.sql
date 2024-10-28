USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[AssignChangeStatusToTest]
    @TestId INT,
    @ChangeStatusTestId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Verifica si la prueba existe
        IF NOT EXISTS (SELECT 1 FROM [dbo].[Tests] WHERE [Id] = @TestId)
        BEGIN
            SET @Message = 'Test not found';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Verifica si el cambio de estado existe
        IF NOT EXISTS (SELECT 1 FROM [dbo].[AR_ChangeStatusTest] WHERE [Id] = @ChangeStatusTestId)
        BEGIN
            SET @Message = 'Change status not found';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Inserta en la tabla IX_Test_ChangeStatusTest si ambos existen
        INSERT INTO [dbo].[IX_Test_ChangeStatusTest] ([TestId], [ChangeStatusTestID])
        VALUES (@TestId, @ChangeStatusTestId);

        SET @Message = 'Change status assigned to test successfully';
        SELECT @ChangeStatusTestId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
