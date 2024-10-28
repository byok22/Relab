USE [TE_Customers]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCustomerById]
    @Id INT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM [dbo].[CT_Customers] WHERE [Id] = @Id)
        BEGIN
            SELECT [Id], [CustomerID], [CustomerName], [Division], [BuildingID], [Building], [Available], [CreatedAt], [UpdatedAt], [UpdatedBy], [CreatedBy]
            FROM [dbo].[Customers]
            WHERE [Id] = @Id;
        END
        ELSE
        BEGIN
            SELECT NULL AS Id, 'Customer not found' AS Message;
        END
    END TRY
    BEGIN CATCH
        SELECT NULL AS Id, ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO