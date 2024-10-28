USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[RemoveChangeStatusFromTest]
    @TestId INT,
    @ChangeStatusTestId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Verifica si la relación existe en IX_Test_ChangeStatusTest
        IF NOT EXISTS (SELECT 1 FROM [dbo].[IX_Test_ChangeStatusTest] WHERE [TestId] = @TestId AND [ChangeStatusTestID] = @ChangeStatusTestId)
        BEGIN
            SET @Message = 'Change status not assigned to the specified test';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Elimina la relación en la tabla IX_Test_ChangeStatusTest
        DELETE FROM [dbo].[IX_Test_ChangeStatusTest]
        WHERE [TestId] = @TestId AND [ChangeStatusTestID] = @ChangeStatusTestId;

        SET @Message = 'Change status removed from test successfully';
        SELECT @ChangeStatusTestId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
