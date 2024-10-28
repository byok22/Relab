USE [TE_ReilabTest]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveTestFromRequest]
    @TestRequestID INT,
    @TestId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Verifica si la relación existe
        IF NOT EXISTS (SELECT 1 FROM [dbo].[IX_TestRequest_Test] WHERE [TestRequestID] = @TestRequestID AND [TestId] = @TestId)
        BEGIN
            SET @Message = 'Test not assigned to the specified request';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Elimina la relación
        DELETE FROM [dbo].[IX_TestRequest_Test]
        WHERE [TestRequestID] = @TestRequestID AND [TestId] = @TestId;

        SET @Message = 'Test removed from request successfully';
        SELECT @TestId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
