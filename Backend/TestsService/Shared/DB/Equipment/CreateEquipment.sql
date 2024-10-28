USE [TE_ReilabTest]
GO
/****** Object:  StoredProcedure [dbo].[CreateEquipment]    Script Date: 27/09/2024 01:14:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CreateEquipment]
    @Name NVARCHAR(255) = 'Default Name',
    @Description NVARCHAR(255) = 'Default Description',
    @CalibrationDate DATETIME = GETDATE()
AS
BEGIN
    DECLARE @NewId INT;
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        INSERT INTO [dbo].[CT_Equipments] ([Name], [Description], [CalibrationDate])
        VALUES (@Name, @Description, @CalibrationDate);
        
        SET @NewId = SCOPE_IDENTITY();
        SET @Message = 'Equipment created successfully';

        -- Return the new record
        SELECT [Id], [Name], [Description], [CalibrationDate]
        FROM [dbo].[CT_Equipments]
        WHERE [Id] = @NewId;
    END TRY
    BEGIN CATCH
        -- Error handling
        SELECT NULL AS Id, ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO
