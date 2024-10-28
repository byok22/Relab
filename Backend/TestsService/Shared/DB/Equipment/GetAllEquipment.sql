CREATE PROCEDURE [dbo].[GetAllEquipments]
AS
BEGIN
    SELECT Id, Name, Description, CalibrationDate
    FROM [dbo].[CT_Equipments];
END;
GO
