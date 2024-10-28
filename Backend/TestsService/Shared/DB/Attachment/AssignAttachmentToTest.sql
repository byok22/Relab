CREATE PROCEDURE AssignAttachmentToTest
    @TestId INT,
    @AttachmentId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        INSERT INTO [dbo].[IX_Test_Attachment] ([TestId], [AttachmentID])
        VALUES (@TestId, @AttachmentId);
        
        SET @Message = 'Attachment assigned to test successfully';

        -- Devuelve el ID del adjunto y un mensaje de éxito
        SELECT @AttachmentId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
