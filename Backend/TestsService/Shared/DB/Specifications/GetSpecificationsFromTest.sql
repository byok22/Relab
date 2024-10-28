-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Torruco
-- Create date: 2024-09-26
-- Description:	Get Specification From a Test
-- =============================================
CREATE PROCEDURE GetSpecificationsFromTest 
	-- Add the parameters for the stored procedure here
	@IdTest int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT s.[Id]
      ,[SpecificationName]
      ,[Details]
   FROM [TE_ReilabTest].[dbo].[AR_Specifications] s
   INNER JOIN  dbo.IX_Test_Specifications i 
   ON i.SpecificationID = s.Id
   WHERE  i.TestId = @IdTest

 
END
GO
