USE [TE_Customers]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllCustomers]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id], [CustomerID], [CustomerName], [Division], [BuildingID], [Building], [Available], [CreatedAt], [UpdatedAt], [UpdatedBy], [CreatedBy]
    FROM [dbo].[CT_Customers];
END;
GO