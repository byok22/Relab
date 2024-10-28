USE [TE_ReilabTest]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllTests]
AS
BEGIN
    SELECT Id, Name, Description, Start, [End], SamplesId, SpecialInstructions, ProfileId, EnginnerId, Status, LastUpdatedMessage, IdRequest, CreatedAt, UpdatedAt, UpdatedBy, CreatedBy
    FROM [dbo].[AR_Tests];
END;
