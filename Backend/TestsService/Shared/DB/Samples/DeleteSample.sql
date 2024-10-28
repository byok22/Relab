USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[DeleteSample]
    @SampleId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        DELETE FROM [dbo].[AR_Samples]
        WHERE [Id] = @SampleId;

        SET @Message = 'Sample deleted successfully';
        SELECT @SampleId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
