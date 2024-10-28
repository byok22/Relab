USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[GetAllSpecifications]
AS
BEGIN
    SELECT Id, SpecificationName, Details
    FROM [dbo].[AR_Specifications];
END
GO
