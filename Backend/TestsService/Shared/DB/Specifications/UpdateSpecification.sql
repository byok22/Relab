USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[UpdateSpecification]
    @SpecificationId INT,
    @SpecificationName NVARCHAR(255),
    @Details NVARCHAR(MAX)
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        UPDATE [dbo].[AR_Specifications]
        SET SpecificationName = @SpecificationName,
            Details = @Details
        WHERE Id = @SpecificationId;

        SET @Message = 'Specification updated successfully';
        SELECT @SpecificationId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
