USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[CreateChangeStatusTest]
    @Status NVARCHAR(50),
    @Message NVARCHAR(MAX),
    @AttachmentId INT = NULL,
    @idUser INT
AS
BEGIN
    DECLARE @NewId INT;
    DECLARE @ResponseMessage NVARCHAR(255);

    BEGIN TRY
        INSERT INTO [dbo].[AR_ChangeStatusTest] (Status, Message, AttachmentId, idUser)
        VALUES (@Status, @Message, @AttachmentId, @idUser);
        
        SET @NewId = SCOPE_IDENTITY();
        SET @ResponseMessage = 'Change status created successfully';

        SELECT @NewId AS id, @ResponseMessage AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
