USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[GetSampleById]
    @SampleId INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[AR_Samples] WHERE [Id] = @SampleId)
        BEGIN
            SELECT * 
            FROM [dbo].[AR_Samples]
            WHERE [Id] = @SampleId;
        END
        ELSE
        BEGIN
            SELECT NULL AS id, 'Sample not found' AS message;
        END
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
