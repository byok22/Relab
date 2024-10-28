USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[UpdateGenericUpdate]
    @GenericUpdateId INT,
    @UpdatedAt DATETIME,
    @idUser INT = NULL,
    @Message NVARCHAR(MAX) = NULL,
    @Changes NVARCHAR(MAX) = NULL
AS
BEGIN
    DECLARE @ResponseMessage NVARCHAR(255);

    BEGIN TRY
        UPDATE [dbo].[AR_GenericUpdate]
        SET UpdatedAt = @UpdatedAt,
            idUser = @idUser,
            Message = @Message,
            Changes = @Changes
        WHERE Id = @GenericUpdateId;

        SET @ResponseMessage = 'Record updated successfully';
        SELECT @GenericUpdateId AS id, @ResponseMessage AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
