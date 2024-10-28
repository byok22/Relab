CREATE PROCEDURE [dbo].[DeleteTestRequest]
    @Id INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        DELETE FROM [dbo].[AR_TestsRequests]
        WHERE [Id] = @Id;

        SET @Message = 'Test request deleted successfully';
        SELECT @Id AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
