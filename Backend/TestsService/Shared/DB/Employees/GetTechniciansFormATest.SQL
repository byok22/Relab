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
-- Description:	Get Technicians From a test
-- =============================================
ALTER PROCEDURE GetTechniciansFormATest 
	-- Add the parameters for the stored procedure here
	@IdTest int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	/****** Script for SelectTopNRows command from SSMS  ******/
   SELECT  
		e.[Id]
      ,[EmployeeNumber]
      ,[Name]
      ,[EmployeeType]
  FROM [TE_ReilabTest].[dbo].[CT_Employee] e
  INNER JOIN dbo.IX_Test_Technicians ix on e.Id = ix.EmployeeId
  WHERE ix.TestId = @IdTest AND e.EmployeeType = 'TECHNICIAN';

  
END
GO
