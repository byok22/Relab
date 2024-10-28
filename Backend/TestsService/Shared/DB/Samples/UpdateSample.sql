USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[UpdateSample]
    @SampleId INT,
    @Quantity INT,
    @Weight DECIMAL(18, 2),
    @Size DECIMAL(18, 2)
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        UPDATE [dbo].[AR_Samples]
        SET [Quantity] = @Quantity,
            [Weight] = @Weight,
            [Size] = @Size
        WHERE [Id] = @SampleId;

        SET @Message = 'Sample updated successfully';
        SELECT @SampleId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
