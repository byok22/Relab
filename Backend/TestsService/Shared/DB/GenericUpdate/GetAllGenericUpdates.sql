USE [TE_ReilabTest]
GO

CREATE PROCEDURE [dbo].[GetAllGenericUpdates]
AS
BEGIN
    SELECT Id, UpdatedAt, idUser, Message, Changes
    FROM [dbo].[AR_GenericUpdate];
END;
GO
