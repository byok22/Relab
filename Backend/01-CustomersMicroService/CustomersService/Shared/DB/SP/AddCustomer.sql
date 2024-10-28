USE [TE_Customers]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddCustomer]
    @CustomerName NVARCHAR(255),
    @Division NVARCHAR(255),
    @BuildingID INT,
    @Building NVARCHAR(255),
    @Available BIT,
    @UpdatedBy NVARCHAR(255) = NULL,
    @CreatedBy NVARCHAR(255) = NULL
AS
BEGIN
    DECLARE @NewId INT;
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        INSERT INTO [dbo].[CT_Customers] ([CustomerID], [CustomerName], [Division], [BuildingID], [Building], [Available], [CreatedAt], [UpdatedAt], [UpdatedBy], [CreatedBy])
        VALUES (NEWID(), @CustomerName, @Division, @BuildingID, @Building, @Available, GETDATE(), GETDATE(), @UpdatedBy, @CreatedBy);
        
        SET @NewId = SCOPE_IDENTITY();
        SET @Message = 'Customer created successfully';

        -- Return the new record
        SELECT [Id], [CustomerID], [CustomerName], [Division], [BuildingID], [Building], [Available], [CreatedAt], [UpdatedAt], [UpdatedBy], [CreatedBy]
        FROM [dbo].[CT_Customers]
        WHERE [Id] = @NewId;
    END TRY
    BEGIN CATCH
        -- Error handling
        SELECT NULL AS Id, ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO