CREATE PROCEDURE [dbo].[DeleteEquipment]
    @Id INT
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        DELETE FROM [dbo].[CT_Equipments]
        WHERE Id = @Id;

        SET @Message = 'Equipment deleted successfully';
        SELECT @Id AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
