USE [TE_Customers]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCustomer]
    @Id INT,
    @CustomerName NVARCHAR(255),
    @Division NVARCHAR(255),
    @BuildingID INT,
    @Building NVARCHAR(255),
    @Available BIT,
    @UpdatedBy NVARCHAR(255) = NULL,
    @CreatedBy NVARCHAR(255) = NULL
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        UPDATE [dbo].[CT_Customers]
        SET CustomerName = @CustomerName,
            Division = @Division,
            BuildingID = @BuildingID,
            Building = @Building,
            Available = @Available,
            UpdatedAt = GETDATE(),
            UpdatedBy = @UpdatedBy,
            CreatedBy = @CreatedBy
        WHERE Id = @Id;

        SET @Message = 'Customer updated successfully';
        SELECT @Id AS Id, @Message AS Message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS Id, ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO