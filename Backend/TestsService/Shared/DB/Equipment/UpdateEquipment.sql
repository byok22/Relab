CREATE PROCEDURE [dbo].[UpdateEquipment]
    @Id INT,
    @Name NVARCHAR(255),
    @Description NVARCHAR(255),
    @CalibrationDate DATETIME
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        UPDATE [dbo].[CT_Equipments]
        SET Name = @Name,
            Description = @Description,
            CalibrationDate = @CalibrationDate
        WHERE Id = @Id;

        SET @Message = 'Equipment updated successfully';
        SELECT @Id AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
GO
