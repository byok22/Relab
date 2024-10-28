USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[DeleteChangeStatusTest]
    @ChangeStatusId INT
AS
BEGIN
    DECLARE @ResponseMessage NVARCHAR(255);

    BEGIN TRY
        DELETE FROM [dbo].[AR_ChangeStatusTest]
        WHERE [Id] = @ChangeStatusId;

        SET @ResponseMessage = 'Change status deleted successfully';
        SELECT @ChangeStatusId AS id, @ResponseMessage AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
