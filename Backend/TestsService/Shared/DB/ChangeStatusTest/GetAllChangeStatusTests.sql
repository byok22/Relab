USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[GetAllChangeStatusTests]
AS
BEGIN
    SELECT Id, Status, Message, AttachmentId, idUser
    FROM [dbo].[AR_ChangeStatusTest];
END;
GO
