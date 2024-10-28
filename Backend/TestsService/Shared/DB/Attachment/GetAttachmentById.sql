CREATE PROCEDURE GetAttachmentById
    @AttachmentId INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[AR_Attachment] WHERE [Id] = @AttachmentId)
        BEGIN
            SELECT * 
            FROM [dbo].[AR_Attachment]
            WHERE [Id] = @AttachmentId;
        END
        ELSE
        BEGIN
            -- Devuelve un mensaje indicando que no se encontró el adjunto
            SELECT NULL AS id, 'Attachment not found' AS message;
        END
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
