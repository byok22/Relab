USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[AssignSpecificationToTest]
    @TestId INT,
    @SpecificationId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Verifica si la especificación existe
        IF NOT EXISTS (SELECT 1 FROM [dbo].[AR_Specifications] WHERE [Id] = @SpecificationId)
        BEGIN
            SET @Message = 'Specification not found';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Inserta en la tabla IX_Test_Specifications
        INSERT INTO [dbo].[IX_Test_Specifications] ([TestId], [SpecificationID])
        VALUES (@TestId, @SpecificationId);

        SET @Message = 'Specification assigned to test successfully';
        SELECT @SpecificationId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
