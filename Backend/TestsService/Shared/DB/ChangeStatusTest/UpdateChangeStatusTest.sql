USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[UpdateChangeStatusTest]
    @ChangeStatusId INT,
    @Status NVARCHAR(50),
    @Message NVARCHAR(MAX),
    @AttachmentId INT = NULL,
    @idUser INT
AS
BEGIN
    DECLARE @ResponseMessage NVARCHAR(255);

    BEGIN TRY
        UPDATE [dbo].[AR_ChangeStatusTest]
        SET Status = @Status,
            Message = @Message,
            AttachmentId = @AttachmentId,
            idUser = @idUser
        WHERE Id = @ChangeStatusId;

        SET @ResponseMessage = 'Change status updated successfully';
        SELECT @ChangeStatusId AS id, @ResponseMessage AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
