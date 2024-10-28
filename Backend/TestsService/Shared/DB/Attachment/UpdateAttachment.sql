CREATE PROCEDURE UpdateAttachment
    @AttachmentId INT,
    @Name NVARCHAR(255),
    @Location NVARCHAR(255),
    @Url NVARCHAR(255)
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        UPDATE [dbo].[AR_Attachment]
        SET [Name] = @Name,
            [Location] = @Location,
            [Url] = @Url
        WHERE [Id] = @AttachmentId;

        SET @Message = 'Attachment updated successfully';

        -- Devuelve el ID del adjunto y el mensaje
        SELECT @AttachmentId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
