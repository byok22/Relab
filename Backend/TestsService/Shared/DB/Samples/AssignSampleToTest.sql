USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[AssignSampleToTest]
    @TestId INT,
    @SampleId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Verifica si la prueba existe
        IF NOT EXISTS (SELECT 1 FROM [dbo].[IX_Test_Sample] WHERE [TestId] = @TestId)
        BEGIN
            SET @Message = 'Test not found';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Verifica si el sample existe
        IF NOT EXISTS (SELECT 1 FROM [dbo].[AR_Samples] WHERE [Id] = @SampleId)
        BEGIN
            SET @Message = 'Sample not found';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Inserta en la tabla IX_Test_Sample si ambos existen
        INSERT INTO [dbo].[IX_Test_Sample] ([TestId], [SampleID])
        VALUES (@TestId, @SampleId);

        SET @Message = 'Sample assigned to test successfully';
        SELECT @SampleId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
