USE [TE_ReilabTest]
GO

/****** Object:  Table [dbo].[IX_Test_Attachment]    Script Date: 29/09/2024 07:06:23 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IX_TestRequest_Test](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestRequestID] [int] NULL,
	[TestId] [int] NULL,
	
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


