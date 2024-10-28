USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[RemoveSampleFromTest]
    @TestId INT,
    @SampleId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Verifica si la relación existe en IX_Test_Sample
        IF NOT EXISTS (SELECT 1 FROM [dbo].[IX_Test_Sample] WHERE [TestId] = @TestId AND [SampleID] = @SampleId)
        BEGIN
            SET @Message = 'Sample not assigned to the specified test';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Elimina la relación en la tabla IX_Test_Sample
        DELETE FROM [dbo].[IX_Test_Sample]
        WHERE [TestId] = @TestId AND [SampleID] = @SampleId;

        SET @Message = 'Sample removed from test successfully';
        SELECT @SampleId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
