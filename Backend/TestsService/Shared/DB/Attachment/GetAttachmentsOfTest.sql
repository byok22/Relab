CREATE PROCEDURE GetAttachmentsOfTest
    @TestId INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[IX_Test_Attachment] WHERE [TestId] = @TestId)
        BEGIN
            SELECT A.* 
            FROM [dbo].[AR_Attachment] A
            INNER JOIN [dbo].[IX_Test_Attachment] TA ON A.[Id] = TA.[AttachmentID]
            WHERE TA.[TestId] = @TestId;
        END
        ELSE
        BEGIN
            -- Devuelve un mensaje indicando que no se encontraron adjuntos para la prueba
            SELECT NULL AS id, 'No attachments found for the specified test' AS message;
        END
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
