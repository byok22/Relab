CREATE PROCEDURE [dbo].[GetAllTestRequests]
AS
BEGIN
    SELECT [Id], [Status], [Description], [Start], [End], [Active], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]
    FROM [dbo].[AR_TestsRequests];
END;
GO
