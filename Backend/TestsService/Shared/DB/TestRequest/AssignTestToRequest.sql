USE [TE_ReilabTest]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AssignTestToRequest]
    @TestRequestID INT,
    @TestId INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        -- Verifica si la solicitud de test existe
        IF NOT EXISTS (SELECT 1 FROM [dbo].[AR_TestsRequests] WHERE [Id] = @TestRequestID)
        BEGIN
            SET @Message = 'Test request not found';
            SELECT NULL AS id, @Message AS message;
            RETURN;
        END

        -- Inserta en la tabla IX_TestRequest_Test
        INSERT INTO [dbo].[IX_TestRequest_Test] ([TestRequestID], [TestId])
        VALUES (@TestRequestID, @TestId);

        SET @Message = 'Test assigned to request successfully';
        SELECT @TestId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
