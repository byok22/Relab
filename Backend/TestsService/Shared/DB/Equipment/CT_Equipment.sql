CREATE TABLE CT_Equipments (
   	[Id] [int] IDENTITY(1,1)  PRIMARY KEY NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    CalibrationDate DATETIME NOT NULL
);