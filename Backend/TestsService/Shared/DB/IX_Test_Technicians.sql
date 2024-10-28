USE [TE_ReilabTest]
GO

/****** Object:  Table [dbo].[IX_Test_Technicians]    Script Date: 18/09/2024 03:34:43 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IX_Test_Technicians](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestId] [int] NULL,
	[EmployeeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


