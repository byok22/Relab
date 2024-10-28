CREATE PROCEDURE DeleteAttachment
    @AttachmentId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Elimina el adjunto
        DELETE FROM [dbo].[AR_Attachment]
        WHERE [Id] = @AttachmentId;

        -- Elimina la relación con las pruebas
        DELETE FROM [dbo].[IX_Test_Attachment]
        WHERE [AttachmentID] = @AttachmentId;

        SET @Message = 'Attachment deleted successfully';

        -- Devuelve el ID del adjunto y el mensaje de éxito
        SELECT @AttachmentId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
