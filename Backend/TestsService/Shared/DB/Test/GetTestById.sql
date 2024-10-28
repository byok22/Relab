USE [TE_ReilabTest]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTestById]
    @TestId INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[AR_Tests] WHERE [Id] = @TestId)
        BEGIN
            SELECT * 
            FROM [dbo].[AR_Tests]
            WHERE [Id] = @TestId;
        END
        ELSE
        BEGIN
            SELECT NULL AS id, 'Test not found' AS message;
        END
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
