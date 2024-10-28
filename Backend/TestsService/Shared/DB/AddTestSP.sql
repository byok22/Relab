CREATE PROCEDURE sp_AddTest
    @Name NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @Start DATETIME,
    @End DATETIME,
    @SamplesQuantity INT,
    @SamplesWeight DECIMAL(18, 2),
    @SamplesSize DECIMAL(18, 2),
    @SpecialInstructions NVARCHAR(MAX),
    @ProfileName NVARCHAR(255),
    @ProfileFile VARBINARY(MAX),
    @ProfileUrl NVARCHAR(MAX),
    @Status NVARCHAR(50), -- Ahora es string
    @LastUpdatedMessage NVARCHAR(MAX),
    @IdRequest INT,
    @CreatedBy NVARCHAR(255),
    @UpdatedBy NVARCHAR(255),
    @TechnicianIds dbo.IntList READONLY, 
    @EquipmentIds dbo.IntList READONLY,  
    @SpecificationIds dbo.IntList READONLY 
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Insertar en la tabla Samples
        DECLARE @SamplesId INT;
        IF @SamplesQuantity IS NOT NULL OR @SamplesWeight IS NOT NULL OR @SamplesSize IS NOT NULL
        BEGIN
            INSERT INTO Samples (Quantity, Weight, Size)
            VALUES (@SamplesQuantity, @SamplesWeight, @SamplesSize);
            SET @SamplesId = SCOPE_IDENTITY();
        END

        -- Insertar en la tabla Attachment para el Profile
        DECLARE @ProfileId INT;
        IF @ProfileName IS NOT NULL OR @ProfileFile IS NOT NULL OR @ProfileUrl IS NOT NULL
        BEGIN
            INSERT INTO Attachment (Name, File, Url)
            VALUES (@ProfileName, @ProfileFile, @ProfileUrl);
            SET @ProfileId = SCOPE_IDENTITY();
        END

        -- Insertar en la tabla Test
        DECLARE @TestId INT;
        INSERT INTO Test (Name, Description, Start, [End], SamplesId, SpecialInstructions, ProfileId, Status, LastUpdatedMessage, idRequest, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
        VALUES (@Name, @Description, @Start, @End, @SamplesId, @SpecialInstructions, @ProfileId, @Status, @LastUpdatedMessage, @IdRequest, GETDATE(), GETDATE(), @CreatedBy, @UpdatedBy);
        SET @TestId = SCOPE_IDENTITY();

        -- Insertar técnicos relacionados (si existen)
        IF EXISTS (SELECT 1 FROM @TechnicianIds)
        BEGIN
            INSERT INTO Test_Employees (TestId, EmployeeId)
            SELECT @TestId, Id FROM @TechnicianIds;
        END

        -- Insertar equipos relacionados (si existen)
        IF EXISTS (SELECT 1 FROM @EquipmentIds)
        BEGIN
            INSERT INTO Test_Equipments (TestId, EquipmentId)
            SELECT @TestId, Id FROM @EquipmentIds;
        END

        -- Insertar especificaciones relacionadas (si existen)
        IF EXISTS (SELECT 1 FROM @SpecificationIds)
        BEGIN
            INSERT INTO Test_Specifications (TestId, SpecificationId)
            SELECT @TestId, Id FROM @SpecificationIds;
        END

        -- Si todo fue exitoso, retornar el ID del nuevo Test
        SELECT @TestId AS NewTestId;

    END TRY
    BEGIN CATCH
        -- Manejar errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END


-----
-- Definir tipos de tabla para pasar listas de IDs
CREATE TYPE dbo.IntList AS TABLE
(
    Id INT
);
-----------

-- Declarar las listas de IDs
DECLARE @TechnicianIds dbo.IntList, @EquipmentIds dbo.IntList, @SpecificationIds dbo.IntList;

-- Insertar IDs de ejemplo
INSERT INTO @TechnicianIds (Id) VALUES (1), (2);
INSERT INTO @EquipmentIds (Id) VALUES (3), (4);
INSERT INTO @SpecificationIds (Id) VALUES (5), (6);

-- Llamar al procedimiento almacenado
EXEC sp_AddTest
    @Name = 'Test 1',
    @Description = 'Descripción del test',
    @Start = '2024-09-05 08:00',
    @End = '2024-09-05 18:00',
    @SamplesQuantity = 10,
    @SamplesWeight = 2.5,
    @SamplesSize = 5.0,
    @SpecialInstructions = 'Instrucciones especiales',
    @ProfileName = 'Perfil 1',
    @ProfileFile = NULL,  -- Asumimos que no hay archivo en este caso
    @ProfileUrl = 'https://example.com/perfil1',
    @Status = 1,  -- Representa el valor del enum TestStatusEnum
    @LastUpdatedMessage = 'Última actualización',
    @IdRequest = 1001,
    @CreatedBy = 'Usuario1',
    @UpdatedBy = 'Usuario1',
    @TechnicianIds = @TechnicianIds,
    @EquipmentIds = @EquipmentIds,
    @SpecificationIds = @SpecificationIds;
