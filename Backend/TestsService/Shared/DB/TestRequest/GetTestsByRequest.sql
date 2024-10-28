USE [TE_ReilabTest]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTestsByRequest]
    @TestRequestID INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[AR_TestsRequests] WHERE [Id] = @TestRequestID)
        BEGIN
            SELECT t.[Id], t.[TestRequestID], t.[TestId]
            FROM [dbo].[IX_TestRequest_Test] t
            WHERE t.[TestRequestID] = @TestRequestID;
        END
        ELSE
        BEGIN
            SELECT NULL AS id, 'Test request not found' AS message;
        END
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
