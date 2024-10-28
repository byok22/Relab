CREATE PROCEDURE sp_GetTestById
    @TestId INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Consultar el Test principal
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
            Attachment p ON t.ProfileId = p.Id
        WHERE 
            t.Id = @TestId;

        -- Consultar los técnicos (si existen)
        SELECT 
            e.EmployeeNumber,
            e.Name,
            e.EmployeeType
        FROM 
            Test_Employees te
        INNER JOIN 
            Employees e ON te.EmployeeId = e.EmployeeNumber
        WHERE 
            te.TestId = @TestId;

        -- Consultar los equipos (si existen)
        SELECT 
            eq.Id,
            eq.Name,
            eq.Description,
            eq.CalibrationDate
        FROM 
            Test_Equipments teq
        INNER JOIN 
            Equipments eq ON teq.EquipmentId = eq.Id
        WHERE 
            teq.TestId = @TestId;

        -- Consultar las especificaciones (si existen)
        SELECT 
            sp.Id,
            sp.SpecificationName,
            sp.Details
        FROM 
            Test_Specifications ts
        INNER JOIN 
            Specifications sp ON ts.SpecificationId = sp.Id
        WHERE 
            ts.TestId = @TestId;

        -- Consultar las actualizaciones del test (si existen)
        SELECT 
            u.Id AS UpdateId,
            u.UpdatedAt,
            u.Message,
            u.Changes,
            usr.UserName,
            usr.EmployeeAccount,
            usr.Email
        FROM 
            Test_Updates tu
        INNER JOIN 
            GenericUpdate u ON tu.UpdateId = u.Id
        LEFT JOIN 
            Users usr ON u.UserId = usr.Id
        WHERE 
            tu.TestId = @TestId;

    END TRY
    BEGIN CATCH
        -- Manejar errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
