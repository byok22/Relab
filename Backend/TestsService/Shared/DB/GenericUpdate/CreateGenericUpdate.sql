USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[CreateGenericUpdate]
    @UpdatedAt DATETIME,
    @idUser INT = NULL,
    @Message NVARCHAR(MAX) = NULL,
    @Changes NVARCHAR(MAX) = NULL
AS
BEGIN
    DECLARE @NewId INT;
    DECLARE @ResponseMessage NVARCHAR(255);

    BEGIN TRY
        INSERT INTO [dbo].[AR_GenericUpdate] (UpdatedAt, idUser, Message, Changes)
        VALUES (@UpdatedAt, @idUser, @Message, @Changes);
        
        SET @NewId = SCOPE_IDENTITY();
        SET @ResponseMessage = 'Record created successfully';

        SELECT @NewId AS id, @ResponseMessage AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
