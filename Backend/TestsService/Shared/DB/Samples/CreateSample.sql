USE [TE_ReilabTest]
GO

ALTER PROCEDURE [dbo].[CreateSample]
    @Quantity INT,
    @Weight DECIMAL(18, 2),
    @Size DECIMAL(18, 2)
AS
BEGIN
    DECLARE @NewId INT;
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        INSERT INTO [dbo].[AR_Samples] ([Quantity], [Weight], [Size])
        VALUES (@Quantity, @Weight, @Size);
        
        SET @NewId = SCOPE_IDENTITY();
        SET @Message = 'Sample created successfully';

        -- Devuelve el ID del nuevo sample y el mensaje
        SELECT @NewId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
