USE [TE_ReilabTest]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateTest]
    @Name NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @Start DATETIME,
    @End DATETIME,
    @SamplesId INT,
    @SpecialInstructions NVARCHAR(MAX),
    @ProfileId INT,
    @EnginnerId INT,
    @Status NVARCHAR(50),
    @LastUpdatedMessage NVARCHAR(MAX),
    @IdRequest INT,
    @CreatedBy NVARCHAR(255)
AS
BEGIN
    DECLARE @NewId INT;
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        INSERT INTO [dbo].[AR_Tests] (Name, Description, Start, [End], SamplesId, SpecialInstructions, ProfileId, EnginnerId, Status, LastUpdatedMessage, IdRequest, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
        VALUES (@Name, @Description, @Start, @End, @SamplesId, @SpecialInstructions, @ProfileId, @EnginnerId, @Status, @LastUpdatedMessage, @IdRequest, GETDATE(), GETDATE(), @CreatedBy, @CreatedBy);
        
        SET @NewId = SCOPE_IDENTITY();
        SET @Message = 'Test created successfully';

        SELECT @NewId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
