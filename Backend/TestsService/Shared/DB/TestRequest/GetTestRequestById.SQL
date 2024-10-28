CREATE PROCEDURE [dbo].[GetTestRequestById]
    @Id INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[AR_TestsRequests] WHERE [Id] = @Id)
        BEGIN
            SELECT [Id], [Status], [Description], [Start], [End], [Active], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]
            FROM [dbo].[AR_TestsRequests]
            WHERE [Id] = @Id;
        END
        ELSE
        BEGIN
            SELECT NULL AS id, 'Test request not found' AS message;
        END
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
