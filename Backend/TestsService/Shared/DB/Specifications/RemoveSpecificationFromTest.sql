USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[RemoveSpecificationFromTest]
    @TestId INT,
    @SpecificationId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Verifica si la relación existe en IX_Test_Specifications
        IF NOT EXISTS (SELECT 1 FROM [dbo].[IX_Test_Specifications] WHERE [TestId] = @TestId AND [SpecificationID] = @SpecificationId)
        BEGIN
            SET @Message = 'Specification not assigned to the specified test';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Elimina la relación en la tabla IX_Test_Specifications
        DELETE FROM [dbo].[IX_Test_Specifications]
        WHERE [TestId] = @TestId AND [SpecificationID] = @SpecificationId;

        SET @Message = 'Specification removed from test successfully';
        SELECT @SpecificationId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
