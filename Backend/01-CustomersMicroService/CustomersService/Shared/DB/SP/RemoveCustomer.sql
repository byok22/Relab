USE [TE_Customers]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveCustomer]
    @Id INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        DELETE FROM [dbo].[CT_Customers]
        WHERE Id = @Id;

        SET @Message = 'Customer deleted successfully';
        SELECT @Id AS Id, @Message AS Message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS Id, ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO