USE [TE_ReilabTest]
GO
/****** Object:  StoredProcedure [dbo].[CreateSpecification]    Script Date: 28/09/2024 08:55:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CreateSpecification]
    @SpecificationName NVARCHAR(255),
    @Details NVARCHAR(MAX)
AS
BEGIN
    DECLARE @NewId INT;
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        INSERT INTO [dbo].[AR_Specifications] (SpecificationName, Details)
        VALUES (@SpecificationName, @Details);
        
        SET @NewId = SCOPE_IDENTITY();
        SET @Message = 'Specification created successfully';

        -- Return the new record
        SELECT [Id], [SpecificationName], [Details]
        FROM [dbo].[AR_Specifications]
        WHERE [Id] = @NewId;
    END TRY
    BEGIN CATCH
        -- Error handling
        SELECT NULL AS Id, ERROR_MESSAGE() AS Message;
    END CATCH
END;