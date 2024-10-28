USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[GetSpecificationById]
    @SpecificationId INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[AR_Specifications] WHERE [Id] = @SpecificationId)
        BEGIN
            SELECT * 
            FROM [dbo].[AR_Specifications]
            WHERE [Id] = @SpecificationId;
        END
        ELSE
        BEGIN
            SELECT NULL AS id, 'Specification not found' AS message;
        END
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
