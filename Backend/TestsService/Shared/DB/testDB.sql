CREATE TABLE AR_Tests (
    Id INT PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(255),
    [Description] NVARCHAR(MAX),
    [Start] DATETIME  NULL,
    [End] DATETIME  NULL,
    SamplesId INT,
    SpecialInstructions NVARCHAR(MAX),
    ProfileId INT,
	EnginnerId INT,
    [Status] NVARCHAR(50),
    LastUpdatedMessage NVARCHAR(MAX),
    IdRequest INT,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedBy NVARCHAR(255),
    CreatedBy NVARCHAR(255),
     --FOREIGN KEY (SamplesId) REFERENCES Samples(Id),
    --FOREIGN KEY (ProfileId) REFERENCES Attachment(Id)
);


CREATE TABLE AR_TestsRequests(
    Id INT PRIMARY KEY IDENTITY,
    [Status] NVARCHAR(50),
    [Description] NVARCHAR(MAX),
    [Start] DATETIME  NULL,
    [End] DATETIME  NULL,
    Active BIT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedBy NVARCHAR(255),
    CreatedBy NVARCHAR(255),

)




CREATE TABLE AR_Samples (
    Id INT PRIMARY KEY IDENTITY,
    Quantity INT ,
    Weight DECIMAL(18, 2),
    Size DECIMAL(18, 2)
);

CREATE TABLE [dbo].[AR_Attachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Location] [nvarchar](255) NULL,
	[Url] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__AR_Attac__3214EC07FCC4F96F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE CT_Employee (
    Id INT PRIMARY KEY IDENTITY,
    EmployeeNumber INT NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    EmployeeType NVARCHAR(50) NOT NULL
);

CREATE TABLE Equipment (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    CalibrationDate DATETIME NOT NULL
);

CREATE TABLE AR_Specification (
    Id INT PRIMARY KEY IDENTITY,
    SpecificationName NVARCHAR(255),
    Details NVARCHAR(MAX)
);

CREATE TABLE AR_ChangeStatusTest (
    Id INT PRIMARY KEY IDENTITY,
    Status NVARCHAR(50)  NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    AttachmentId INT,
    idUser INT NOT NULL,
   -- FOREIGN KEY (AttachmentId) REFERENCES Attachment(Id)
);

CREATE TABLE AR_ChangeStatusTestRequest (
    Id INT PRIMARY KEY IDENTITY,
    Status NVARCHAR(50)  NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    AttachmentId INT,
    idUser INT NOT NULL,
   -- FOREIGN KEY (AttachmentId) REFERENCES Attachment(Id)
);

CREATE TABLE AR_GenericUpdate (
    Id INT PRIMARY KEY IDENTITY,
    UpdatedAt DATETIME NOT NULL,
    idUser INT,
    Message NVARCHAR(MAX),
    Changes NVARCHAR(MAX),
    --FOREIGN KEY (UserId) REFERENCES [User](Id)
);
CREATE TABLE [User] (
    Id INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(255) NOT NULL,
    EmployeeAccount NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255)
);

CREATE TABLE Test_Employees (
    TestId INT,
    EmployeeId INT,
    FOREIGN KEY (TestId) REFERENCES Test(Id),
    FOREIGN KEY (EmployeeId) REFERENCES Employee(Id),
    PRIMARY KEY (TestId, EmployeeId)
);

CREATE TABLE Test_Equipments (
    TestId INT,
    EquipmentId INT,
    FOREIGN KEY (TestId) REFERENCES Test(Id),
    FOREIGN KEY (EquipmentId) REFERENCES Equipment(Id),
    PRIMARY KEY (TestId, EquipmentId)
);

CREATE TABLE Test_Specifications (
    TestId INT,
    SpecificationId INT,
    FOREIGN KEY (TestId) REFERENCES Test(Id),
    FOREIGN KEY (SpecificationId) REFERENCES Specification(Id),
    PRIMARY KEY (TestId, SpecificationId)
);

CREATE TABLE IX_Test_Attachment{
    Id INT PRIMARY KEY IDENTITY,
    TestId INT,
    AttachmentID INT,
}

CREATE TABLE IX_Test_Specifications(
	Id INT PRIMARY KEY IDENTITY,
    TestId INT,
    SpecificationID INT,
)

CREATE TABLE IX_Test_Equipments(
	Id INT PRIMARY KEY IDENTITY,
    TestId INT,
    EquipmentID INT,
)
CREATE TABLE IX_Test_GenericUpdate(
	Id INT PRIMARY KEY IDENTITY,
    TestId INT,
	GenericUpdateID INT,
)


CREATE TABLE IX_Test_ChangeStatusTest(
	Id INT PRIMARY KEY IDENTITY,
    TestId INT,
	ChangeStatusTestID INT,
)


CREATE TABLE IX_Test_GenericUpdateTestRequest(
	Id INT PRIMARY KEY IDENTITY,
    TestRequestId INT,
	GenericUpdateTestRequestID INT,
)


CREATE TABLE IX_Test_ChangeStatusTestRequest(
	Id INT PRIMARY KEY IDENTITY,
    TestRequestId INT,
	ChangeStatusTestRequestID INT,
)