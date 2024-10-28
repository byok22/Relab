USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[GetGenericUpdateById]
    @GenericUpdateId INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[AR_GenericUpdate] WHERE [Id] = @GenericUpdateId)
        BEGIN
            SELECT * 
            FROM [dbo].[AR_GenericUpdate]
            WHERE [Id] = @GenericUpdateId;
        END
        ELSE
        BEGIN
            SELECT NULL AS id, 'Record not found' AS message;
        END
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
