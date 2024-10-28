USE [TE_ReilabTest]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateTest]
    @TestId INT,
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
    @UpdatedBy NVARCHAR(255)
AS
BEGIN
    DECLARE @Message NVARCHAR(255);

    BEGIN TRY
        UPDATE [dbo].[AR_Tests]
        SET Name = @Name,
            Description = @Description,
            Start = @Start,
            [End] = @End,
            SamplesId = @SamplesId,
            SpecialInstructions = @SpecialInstructions,
            ProfileId = @ProfileId,
            EnginnerId = @EnginnerId,
            Status = @Status,
            LastUpdatedMessage = @LastUpdatedMessage,
            UpdatedAt = GETDATE(),
            UpdatedBy = @UpdatedBy
        WHERE Id = @TestId;

        SET @Message = 'Test updated successfully';
        SELECT @TestId AS id, @Message AS message;
    END TRY
    BEGIN CATCH
        SELECT NULL AS id, ERROR_MESSAGE() AS message;
    END CATCH
END;
