CREATE PROCEDURE [dbo].[CreateTestRequest]
    @Status NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @Start DATETIME,
    @End DATETIME,
    @Active BIT,
    @CreatedBy NVARCHAR(255),
    @UpdatedBy NVARCHAR(255)
AS
BEGIN
    DECLARE @NewId INT;
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        INSERT INTO [dbo].[AR_TestsRequests] ([Status], [Description], [Start], [End], [Active], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy])
        VALUES (@Status, @Description, @Start, @End, @Active, GETDATE(), GETDATE(), @CreatedBy, @UpdatedBy);
        
        SET @NewId = SCOPE_IDENTITY();
        SET @Message = 'Test request created successfully';

        -- Return the new record
        SELECT [Id], [Status], [Description], [Start], [End], [Active], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]
        FROM [dbo].[AR_TestsRequests]
        WHERE [Id] = @NewId;
    END TRY
    BEGIN CATCH
        -- Error handling
        SELECT NULL AS Id, ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO
