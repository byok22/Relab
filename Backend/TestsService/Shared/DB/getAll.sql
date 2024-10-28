CREATE PROCEDURE sp_GetAllTests
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Consultar todas las pruebas (Tests)
        SELECT 
            t.Id,
            t.Name,
            t.Description,
            t.Start,
            t.[End],
            t.SpecialInstructions,
            t.Status,
            t.LastUpdatedMessage,
            t.idRequest,
            t.CreatedAt,
            t.UpdatedAt,
            t.CreatedBy,
            t.UpdatedBy,
            -- Información de Samples (si existe)
            s.Quantity AS SamplesQuantity,
            s.Weight AS SamplesWeight,
            s.Size AS SamplesSize,
            -- Información de Profile (si existe)
            p.Name AS ProfileName,
            p.Url AS ProfileUrl
        FROM 
            Test t
        LEFT JOIN 
            Samples s ON t.SamplesId = s.Id
        LEFT JOIN 
            Attachment p ON t.ProfileId = p.Id;

        -- Consultar los técnicos para todas las pruebas (si existen)
        SELECT 
            t.Id AS TestId,
            e.EmployeeNumber,
            e.Name,
            e.EmployeeType
        FROM 
            Test t
        LEFT JOIN 
            Test_Employees te ON t.Id = te.TestId
        LEFT JOIN 
            Employees e ON te.EmployeeId = e.EmployeeNumber;

        -- Consultar los equipos para todas las pruebas (si existen)
        SELECT 
            t.Id AS TestId,
            eq.Id AS EquipmentId,
            eq.Name,
            eq.Description,
            eq.CalibrationDate
        FROM 
            Test t
        LEFT JOIN 
            Test_Equipments teq ON t.Id = teq.TestId
        LEFT JOIN 
            Equipments eq ON teq.EquipmentId = eq.Id;

        -- Consultar las especificaciones para todas las pruebas (si existen)
        SELECT 
            t.Id AS TestId,
            sp.Id AS SpecificationId,
            sp.SpecificationName,
            sp.Details
        FROM 
            Test t
        LEFT JOIN 
            Test_Specifications ts ON t.Id = ts.TestId
        LEFT JOIN 
            Specifications sp ON ts.SpecificationId = sp.Id;

        -- Consultar las actualizaciones para todas las pruebas (si existen)
        SELECT 
            t.Id AS TestId,
            u.Id AS UpdateId,
            u.UpdatedAt,
            u.Message,
            u.Changes,
            usr.UserName,
            usr.EmployeeAccount,
            usr.Email
        FROM 
            Test t
        LEFT JOIN 
            Test_Updates tu ON t.Id = tu.TestId
        LEFT JOIN 
            GenericUpdate u ON tu.UpdateId = u.Id
        LEFT JOIN 
            Users usr ON u.UserId = usr.Id;

    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
