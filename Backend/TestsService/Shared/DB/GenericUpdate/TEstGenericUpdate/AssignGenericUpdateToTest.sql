USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[AssignGenericUpdateToTest]
    @TestId INT,
    @GenericUpdateId INT
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

        -- Verifica si la actualización genérica existe
        IF NOT EXISTS (SELECT 1 FROM [dbo].[AR_GenericUpdate] WHERE [Id] = @GenericUpdateId)
        BEGIN
            SET @Message = 'Generic update not found';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Inserta en la tabla IX_Test_GenericUpdate si ambos existen
        INSERT INTO [dbo].[IX_Test_GenericUpdate] ([TestId], [GenericUpdateID])
        VALUES (@TestId, @GenericUpdateId);

        SET @Message = 'Generic update assigned to test successfully';
        SELECT @GenericUpdateId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
