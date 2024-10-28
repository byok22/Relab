CREATE PROCEDURE CreateAttachment
    @Name NVARCHAR(255),
    @Location NVARCHAR(255),
    @Url NVARCHAR(255)
AS
BEGIN
    DECLARE @NewId INT;
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        INSERT INTO [dbo].[AR_Attachment] ([Name], [Location], [Url])
        VALUES (@Name, @Location, @Url);
        
        SET @NewId = SCOPE_IDENTITY();
        SET @Message = 'Attachment created successfully';

        -- Devuelve el ID del nuevo adjunto y el mensaje
        SELECT @NewId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
