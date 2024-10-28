USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[GetChangeStatusTestById]
    @ChangeStatusId INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[AR_ChangeStatusTest] WHERE [Id] = @ChangeStatusId)
        BEGIN
            SELECT * 
            FROM [dbo].[AR_ChangeStatusTest]
            WHERE [Id] = @ChangeStatusId;
        END
        ELSE
        BEGIN
            SELECT NULL AS id, 'Change status not found' AS message;
        END
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
