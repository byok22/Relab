CREATE PROCEDURE [dbo].[UpdateTestRequest]
    @Id INT,
    @Status NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @Start DATETIME,
    @End DATETIME,
    @Active BIT,
    @UpdatedBy NVARCHAR(255)
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        UPDATE [dbo].[AR_TestsRequests]
        SET [Status] = @Status,
            [Description] = @Description,
            [Start] = @Start,
            [End] = @End,
            [Active] = @Active,
            [UpdatedAt] = GETDATE(),
            [UpdatedBy] = @UpdatedBy
        WHERE [Id] = @Id;

        SET @Message = 'Test request updated successfully';
        SELECT @Id AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
