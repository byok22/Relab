USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[RemoveGenericUpdateFromTest]
    @TestId INT,
    @GenericUpdateId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Verifica si la relación existe en IX_Test_GenericUpdate
        IF NOT EXISTS (SELECT 1 FROM [dbo].[IX_Test_GenericUpdate] WHERE [TestId] = @TestId AND [GenericUpdateID] = @GenericUpdateId)
        BEGIN
            SET @Message = 'Generic update not assigned to the specified test';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Elimina la relación en la tabla IX_Test_GenericUpdate
        DELETE FROM [dbo].[IX_Test_GenericUpdate]
        WHERE [TestId] = @TestId AND [GenericUpdateID] = @GenericUpdateId;

        SET @Message = 'Generic update removed from test successfully';
        SELECT @GenericUpdateId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
