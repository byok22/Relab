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
-- Create date: 2024-09-2024
-- Description:	GetGenericUpdatesFromTest
-- =============================================
CREATE PROCEDURE GetGenericUpdatesFromTest 
	-- Add the parameters for the stored procedure here
	@TestId int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		A.[Id]
      ,[UpdatedAt]
      ,[idUser]
      ,[Message]
      ,[Changes]
  FROM [TE_ReilabTest].[dbo].[AR_GenericUpdate] A
  INNER JOIN dbo.IX_Test_GenericUpdate   I
  ON A.Id = I.GenericUpdateID
  WHERE I.TestId = @TestId
    
END
GO
