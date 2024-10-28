USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[DeleteGenericUpdate]
    @GenericUpdateId INT
AS
BEGIN
    DECLARE @ResponseMessage NVARCHAR(255);

    BEGIN TRY
        DELETE FROM [dbo].[AR_GenericUpdate]
        WHERE [Id] = @GenericUpdateId;

        SET @ResponseMessage = 'Record deleted successfully';
        SELECT @GenericUpdateId AS id, @ResponseMessage AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
