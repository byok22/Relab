USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[DeleteSpecification]
    @SpecificationId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        DELETE FROM [dbo].[AR_Specifications]
        WHERE [Id] = @SpecificationId;

        SET @Message = 'Specification deleted successfully';
        SELECT @SpecificationId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
