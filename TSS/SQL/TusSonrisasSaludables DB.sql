CREATE DATABASE TusSonrisasSaludables
GO

USE TusSonrisasSaludables
GO

CREATE TABLE Roles (
RoleID INT PRIMARY KEY IDENTITY NOT NULL,
RoleType VARCHAR(20) NOT NULL UNIQUE)
GO

INSERT INTO Roles VALUES ('Admin')
INSERT INTO Roles VALUES ('User')

CREATE TABLE IdentificationTypes (
TypeID INT PRIMARY KEY IDENTITY NOT NULL,
IDType VARCHAR(20) NOT NULL UNIQUE)
GO

INSERT INTO IdentificationTypes VALUES ('Nacional')
INSERT INTO IdentificationTypes VALUES ('Juridica')

CREATE TABLE Genres (
GenreID INT PRIMARY KEY IDENTITY,
GenreName VARCHAR(15) NOT NULL UNIQUE)

INSERT INTO Genres VALUES ('Femenino')
INSERT INTO Genres VALUES ('Masculino')

CREATE TABLE Provinces (
ProvinceID INT NOT NULL PRIMARY KEY IDENTITY,
ProvinceName VARCHAR(20) NOT NULL UNIQUE)

INSERT INTO Provinces VALUES ('San José'), ('Alajuela'), ('Cartago'), ('Heredia'), ('Guanacaste'), ('Puntarenas'), ('Limón')

CREATE TABLE Users (
UserID INT PRIMARY KEY IDENTITY NOT NULL,
RoleID INT NOT NULL,
TypeID INT NOT NULL,
GenreID INT NOT NULL,
ProvinceID INT NOT NULL,
IDNumber VARCHAR(11) NOT NULL UNIQUE,
UserName VARCHAR(100) NOT NULL,
FirstName VARCHAR(100) NOT NULL,
LastName VARCHAR(100) NOT NULL,
BirthDate DATE NOT NULL,
Email VARCHAR(100) NOT NULL UNIQUE,
PhoneNumber VARCHAR(9) NOT NULL, 
UserAddress VARCHAR(450) NOT NULL,
PasswordHash VARCHAR(100) NOT NULL,
FOREIGN KEY (RoleID) REFERENCES Roles, 
FOREIGN KEY (TypeID) REFERENCES IdentificationTypes,
FOREIGN KEY (GenreID) REFERENCES Genres,
FOREIGN KEY (ProvinceID) REFERENCES Provinces,
CHECK (IDNumber LIKE '[1-8]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]' AND Email LIKE '%_@_%.%'
AND PhoneNumber LIKE '[2,5,6,7,8][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]' AND UserName NOT LIKE '%[0-9%$#&<>?+-?%]%' 
AND FirstName NOT LIKE '%[0-9%$#&<>?+-?%]%' AND LastName NOT LIKE '%[0-9%$#&<>?+-?%]%')) 
GO 

CREATE TABLE ClinicProcedures (
ProcedureID INT PRIMARY KEY NOT NULL IDENTITY,
ProcedureName NVARCHAR (100) NOT NULL UNIQUE)

CREATE TABLE Specialties (
SpecialtyID INT PRIMARY KEY IDENTITY,
SpecialtyName VARCHAR(50) NOT NULL UNIQUE)

INSERT INTO Specialties VALUES ('Odontopediatría'), ('Ortodoncista')

CREATE TABLE Doctors (
DoctorID INT NOT NULL IDENTITY PRIMARY KEY,
TypeID INT NOT NULL,
--RoleID INT NOT NULL,
SpecialtyID INT NOT NULL,
GenreID INT NOT NULL,
IDNumber VARCHAR(15) NOT NULL UNIQUE,
DoctorName VARCHAR(20) NOT NULL,
FirstName VARCHAR(30) NOT NULL,
LastName VARCHAR(30) NOT NULL,
BirthDate DATE NOT NULL,
Email VARCHAR(50) NOT NULL UNIQUE,
PhoneNumber VARCHAR(9) NOT NULL UNIQUE,
DoctorPhoto IMAGE,
--FOREIGN KEY (RoleID) REFERENCES Roles,
FOREIGN KEY (TypeID) REFERENCES IdentificationTypes,
FOREIGN KEY (SpecialtyID) REFERENCES Specialties,
FOREIGN KEY (GenreID) REFERENCES Genres, 
CHECK (IDNumber LIKE '[1-8]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]' AND Email LIKE '%_@_%.%'
AND PhoneNumber LIKE '[2,5,6,7,8][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]' AND DoctorName NOT LIKE '%[0-9%$#&<>?+-?%]%' 
AND FirstName NOT LIKE '%[0-9%$#&<>?+-?%]%' AND LastName NOT LIKE '%[0-9%$#&<>?+-?%]%'))

CREATE TABLE Brands (
BrandID INT PRIMARY KEY IDENTITY,
BrandName VARCHAR(50) NOT NULL UNIQUE)

CREATE TABLE Categories (
CategoryID INT PRIMARY KEY IDENTITY NOT NULL,
MainCategoryID INT,
CategoryName VARCHAR(20) NOT NULL UNIQUE,
FOREIGN KEY (MainCategoryID) REFERENCES Categories)

INSERT INTO Categories VALUES (NULL, 'Higiene Bucal'), (1, 'Pasta de Dientes')

CREATE TABLE Products (
ProductID INT PRIMARY KEY IDENTITY,
BrandID INT NOT NULL,
CategoryID INT NOT NULL,
ProductName VARCHAR(100) NOT NULL,
ProductDescription VARCHAR(400) NOT NULL,
UnitPrice DECIMAL(12,2) NOT NULL,
Stock INT NOT NULL,
FOREIGN KEY (BrandID) REFERENCES Brands,
FOREIGN KEY (CategoryID) REFERENCES Categories)

CREATE TABLE PatientRecords (
RecordID INT PRIMARY KEY IDENTITY,
UserID INT NOT NULL,
DoctorID INT NOT NULL,
ProcedureID INT NOT NULL,
Diagnoses VARCHAR(450) NOT NULL,
Symptoms VARCHAR(450) NOT NULL,
Treatment VARCHAR(450) NOT NULL,
RecordDate DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
FOREIGN KEY (UserID) REFERENCES Users,
FOREIGN KEY (DoctorID) REFERENCES Doctors,
FOREIGN KEY (ProcedureID) REFERENCES ClinicProcedures)

CREATE TABLE Appointments (
AppointmentID INT PRIMARY KEY IDENTITY,
UserID INT,
DoctorID INT,
SpecialtyID INT NOT NULL,
StartTime DATETIME NOT NULL UNIQUE,
EndTime DATETIME NOT NULL UNIQUE,
FOREIGN KEY (UserID) REFERENCES Users,
FOREIGN KEY (DoctorID) REFERENCES Doctors,
FOREIGN KEY (SpecialtyID) REFERENCES Specialties)

CREATE TABLE ShoppingCarts (
CartID INT PRIMARY KEY,
UserID INT,
CreationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
Total DECIMAL(12, 2),
FOREIGN KEY (UserID) REFERENCES Users)

CREATE TABLE ShoppingDetails (
DetailID INT PRIMARY KEY,
CartID INT,
ProductID INT,
Quantity INT,
UnitPrice DECIMAL(12, 2),
Taxes DECIMAL(3,2),
SubTotal DECIMAL(12,2),
FOREIGN KEY (CartID) REFERENCES ShoppingCarts,
FOREIGN KEY (ProductID) REFERENCES Products)
GO

--Vistas de la BD
CREATE OR ALTER VIEW VW_Roles AS
SELECT * FROM Roles
GO

CREATE OR ALTER VIEW VW_Identifications AS
SELECT * FROM IdentificationTypes
GO

CREATE OR ALTER VIEW VW_Provinces AS
SELECT * FROM Provinces
GO

CREATE OR ALTER VIEW VW_Users AS
SELECT UserID, IDNumber, ProvinceName, Username + ' ' + FirstName + ' ' + LastName AS 'Full Name', GenreName, BirthDate, Email, PhoneNumber FROM Users INNER JOIN
Provinces ON Users.ProvinceID = Provinces.ProvinceID INNER JOIN Genres ON Users.GenreID = Genres.GenreID
GO

CREATE OR ALTER VIEW VW_Brands AS
SELECT * FROM Brands
GO

CREATE OR ALTER VIEW VW_Categories AS
SELECT CategoryID, CategoryName FROM Categories WHERE MainCategoryID IS NULL
GO

CREATE OR ALTER VIEW VW_SubCategories AS
SELECT SC.CategoryID, MC.CategoryName AS 'MainCategory', SC.CategoryName AS 'SubCategory' FROM Categories SC
INNER JOIN Categories MC ON SC.MainCategoryID = MC.CategoryID WHERE SC.MainCategoryID IS NOT NULL
GO

CREATE OR ALTER VIEW VW_Appointmens AS
SELECT AppointmentID, DoctorName + ' ' + FirstName + ' ' + LastName AS 'Doctor', SpecialtyName, StartTime, EndTime FROM Appointments 
INNER JOIN Doctors ON Appointments.DoctorID = Doctors.DoctorID 
INNER JOIN Specialties ON Appointments.SpecialtyID = Specialties.SpecialtyID AND Specialties.SpecialtyID = Doctors.SpecialtyID
GO

--Procedimientos Almacenados de CRUD

--------------- SP ROLES ----------------
CREATE OR ALTER PROCEDURE SP_CreateRole 
@RoleName VARCHAR(20)
AS 
BEGIN
IF EXISTS (SELECT 1 FROM Roles WHERE RoleType = @RoleName)
BEGIN
RAISERROR('El tipo de rol ya existe.', 16, 1)
RETURN;
END

INSERT INTO Roles VALUES (@RoleName)
PRINT 'El rol ha sido agregado correctamente.'
END
GO

CREATE OR ALTER PROCEDURE SP_GetAllRoles
AS
SELECT * FROM VW_Roles
GO

CREATE OR ALTER PROCEDURE SP_GetRole
@RoleID INT 
AS
SELECT * FROM Roles WHERE RoleID = @RoleID
GO

CREATE OR ALTER PROCEDURE SP_EditRole
@RoleID INT,
@RoleName VARCHAR(20)
AS 
BEGIN
IF EXISTS (SELECT 1 FROM Roles WHERE RoleType = @RoleName AND RoleID != @RoleID)
BEGIN
RAISERROR('El tipo de rol ya existe.', 16, 1)
RETURN;
END

UPDATE Roles SET
RoleType = @RoleName WHERE RoleID = @RoleID
PRINT 'El rol ha sido actualizado correctamente.'
END
GO

CREATE OR ALTER PROCEDURE SP_DeleteRole 
@RoleID INT
AS
BEGIN
IF NOT EXISTS (SELECT RoleID FROM Roles WHERE RoleID = @RoleID)
BEGIN
RAISERROR('El tipo de rol no existe.', 16, 1)
RETURN;
END

DELETE FROM Roles WHERE RoleID = @RoleID
PRINT 'Se eliminó el rol correctamente.'
END
GO

------------------- SP Users -------------------
CREATE OR ALTER PROCEDURE SP_GetAllUsersView AS
SELECT * FROM VW_Users 
GO

CREATE OR ALTER PROCEDURE SP_GetAllUsers AS
SELECT * FROM Users 
GO

CREATE OR ALTER PROCEDURE SP_GetUserView 
@ID INT AS
BEGIN
IF NOT EXISTS (SELECT 1 FROM VW_Users WHERE UserID = @ID)
BEGIN
RAISERROR ('El usuario no existe.', 16, 1)
RETURN;
END 

SELECT * FROM VW_Users WHERE UserID = @ID
END
GO

CREATE OR ALTER PROCEDURE SP_GetUser
@ID INT AS
BEGIN
IF NOT EXISTS (SELECT 1 FROM Users WHERE UserID = @ID)
BEGIN
RAISERROR ('El usuario no existe.', 16, 1)
RETURN;
END 

SELECT * FROM Users WHERE UserID = @ID
END
GO

CREATE OR ALTER PROCEDURE SP_CreateUser
@TypeID INT, 
@GenreID INT, 
@ProvinceID INT,
@IDNumber VARCHAR(11),
@Username VARCHAR(100),
@FirstName VARCHAR(100),
@LastName VARCHAR(100),
@BirthDate DATE,
@Email VARCHAR(100),
@Phone VARCHAR(9), 
@UserAddress VARCHAR(450), 
@Password VARCHAR(128)
AS
BEGIN
IF NOT @IDNumber LIKE '[1-8]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'
BEGIN
RAISERROR ('El número de cédula no tiene el formato correcto.', 16, 1);
RETURN;
END

IF EXISTS (SELECT 1 FROM Users WHERE IDNumber = @IDNumber)
BEGIN
RAISERROR ('El número de cédula ya existe.', 16, 1);
RETURN;
END
ELSE 

IF NOT @Phone LIKE '[2,5,6,7,8][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'
BEGIN
RAISERROR ('El número de teléfono no tiene el formato correcto.', 16, 1);
RETURN;
END

IF EXISTS (SELECT 1 FROM Users WHERE PhoneNumber = @Phone)
BEGIN
RAISERROR ('El número de teléfono ya existe.', 16, 1);
RETURN;
END
ELSE 

IF NOT @Email LIKE '%_@_%._%'
BEGIN
RAISERROR ('El correo electrónico no tiene el formato correcto.', 16, 1);
RETURN;
END
ELSE

IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
BEGIN
RAISERROR ('El correo electrónico ya existe.', 16, 1);
RETURN;
END

DECLARE @PassHash VARCHAR(128) = CONVERT(VARCHAR(128), HASHBYTES('SHA2_256', @Password), 2);
INSERT INTO Users VALUES (2, @TypeID, @GenreID, @ProvinceID, @IDNumber, @Username, @FirstName, @LastName, @BirthDate, 
@Email, @Phone, @UserAddress, @PassHash)
END
GO

EXEC SP_CreateUser 1, 2, 2, '1-1822-0346', 'Brayan', 'Rivas', 'López', '2002/09/24', 
'bryan@gmail.com', '8828-8888', 'Al frente del Estadio Nacional', '12345678'
GO

CREATE OR ALTER PROCEDURE SP_CreateAdminUser
@TypeID INT, 
@GenreID INT, 
@ProvinceID INT,
@IDNumber VARCHAR(11),
@Username VARCHAR(100),
@FirstName VARCHAR(100),
@LastName VARCHAR(100),
@BirthDate DATE,
@Email VARCHAR(100),
@Phone VARCHAR(9), 
@UserAddress VARCHAR(450), 
@Password VARCHAR(128)
AS
BEGIN
IF NOT @IDNumber LIKE '[1-8]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'
BEGIN
RAISERROR ('El número de cédula no tiene el formato correcto.', 16, 1);
RETURN;
END

IF EXISTS (SELECT 1 FROM Users WHERE IDNumber = @IDNumber)
BEGIN
RAISERROR ('El número de cédula ya existe.', 16, 1);
RETURN;
END
ELSE 

IF NOT @Phone LIKE '[2,5,6,7,8][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'
BEGIN
RAISERROR ('El número de teléfono no tiene el formato correcto.', 16, 1);
RETURN;
END

IF EXISTS (SELECT 1 FROM Users WHERE PhoneNumber = @Phone)
BEGIN
RAISERROR ('El número de teléfono ya existe.', 16, 1);
RETURN;
END
ELSE 

IF NOT @Email LIKE '%_@_%._%'
BEGIN
RAISERROR ('El correo electrónico no tiene el formato correcto.', 16, 1);
RETURN;
END
ELSE

IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
BEGIN
RAISERROR ('El correo electrónico ya existe.', 16, 1);
RETURN;
END

DECLARE @PassHash VARCHAR(128) = CONVERT(VARCHAR(128), HASHBYTES('SHA2_256', @Password), 2);
INSERT INTO Users VALUES (1, @TypeID, @GenreID, @ProvinceID, @IDNumber, @Username, @FirstName, 
@LastName, @BirthDate, @Email, @Phone, @UserAddress, @PassHash)
END
GO

EXEC SP_CreateAdminUser 1, 2, 2, '1-1855-0046', 'Brayan', 'Rivas', 'López', '2002/09/24', 
'brayan@gmail.com', '8888-8888', 'Al frente del Estadio Nacional', '12345678'
GO

CREATE OR ALTER PROCEDURE SP_EditUser
@ID INT,
@TypeID INT, 
@GenreID INT, 
@ProvinceID INT,
@IDNumber VARCHAR(11),
@Username VARCHAR(100),
@FirstName VARCHAR(100),
@LastName VARCHAR(100),
@BirthDate DATE,
@Email VARCHAR(100),
@Phone VARCHAR(9), 
@UserAddress VARCHAR(450), 
@Password VARCHAR(128)
AS
BEGIN

IF NOT @IDNumber LIKE '[1-8]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'
BEGIN
RAISERROR ('El número de cédula no tiene el formato correcto.', 16, 1);
RETURN;
END

IF EXISTS (SELECT 1 FROM Users WHERE IDNumber = @IDNumber AND UserID != @ID)
BEGIN
RAISERROR ('El número de cédula ya existe.', 16, 1);
RETURN;
END
ELSE 

IF NOT @Phone LIKE '[2,5,6,7,8][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'
BEGIN
RAISERROR ('El número de teléfono no tiene el formato correcto.', 16, 1);
RETURN;
END

IF EXISTS (SELECT 1 FROM Users WHERE PhoneNumber = @Phone AND UserID != @ID)
BEGIN
RAISERROR ('El número de teléfono ya existe.', 16, 1);
RETURN;
END
ELSE 

IF NOT @Email LIKE '%_@_%._%'
BEGIN
RAISERROR ('El correo electrónico no tiene el formato correcto.', 16, 1);
RETURN;
END
ELSE

IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email AND UserID != @ID)
BEGIN
RAISERROR ('El correo electrónico ya existe.', 16, 1);
RETURN;
END

DECLARE @PassHash VARCHAR(128) = CONVERT(VARCHAR(128), HASHBYTES('SHA2_256', @Password), 2);
UPDATE Users SET 
TypeID = @TypeID, GenreID = @GenreID, ProvinceID = @ProvinceID, IDNumber = @IDNumber, 
UserName = @Username, FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate, Email = @Email, 
PhoneNumber = @Phone, UserAddress = @UserAddress, PasswordHash = @PassHash WHERE UserID = @ID
PRINT 'Usuario actualizado con éxito.'
END
GO

EXEC SP_EditUser 3, 1, 2, 2, '1-1855-0046', 'Brayan', 'Rivas', 'López', '2002/09/24', 
'brayan@gmail.com', '8888-8888', 'Al frente del Estadio Nacional', '12345678'
GO

CREATE OR ALTER PROCEDURE SP_DeleteUser 
@ID INT AS
BEGIN
IF NOT EXISTS (SELECT 1 FROM Users WHERE UserID = @ID)
BEGIN
RAISERROR('El usuario no existe.', 16, 1)
RETURN;
END 

DELETE FROM Users WHERE UserID = @ID
END
GO
--------------------------------------------------------

------------------ SP IDENTIFICATIONS ------------------

CREATE OR ALTER PROCEDURE SP_GetAllIdentifications AS
SELECT * FROM IdentificationTypes
GO

CREATE OR ALTER PROCEDURE SP_GetAllIdentificationsView AS
SELECT * FROM VW_Identifications
GO

CREATE OR ALTER PROCEDURE SP_GetIdentification
@ID INT AS
BEGIN
IF NOT EXISTS (SELECT 1 FROM IdentificationTypes WHERE TypeID = @ID)
BEGIN
RAISERROR('El tipo de identificación no fue encontrado o no existe.', 16, 1)
RETURN;
END

SELECT * FROM IdentificationTypes WHERE TypeID = @ID
END 
GO

CREATE OR ALTER PROCEDURE SP_GetIdentificationView
@ID INT AS
BEGIN
IF NOT EXISTS (SELECT * FROM VW_Identifications WHERE TypeID = @ID)
BEGIN
RAISERROR('El tipo de identificación no fue encontrado o no existe.', 16, 1)
RETURN;
END

SELECT * FROM VW_Identifications WHERE TypeID = @ID
END 
GO

CREATE OR ALTER PROCEDURE SP_EditIdentification 
@ID INT, 
@Type VARCHAR(20) AS
BEGIN 
IF EXISTS (SELECT 1 FROM IdentificationTypes WHERE IDType = @Type AND TypeID != @ID)
BEGIN
RAISERROR('El tipo de identificación ya existe.', 16, 1)
RETURN;
END

UPDATE IdentificationTypes SET
IDType = @Type WHERE TypeID = @ID
PRINT 'Se ha modificado la identificación con éxito.'
END
GO

CREATE OR ALTER PROCEDURE SP_DeleteIdentification 
@ID INT
AS 
BEGIN 
IF NOT EXISTS (SELECT 1 FROM IdentificationTypes WHERE TypeID = @ID)
BEGIN
RAISERROR('El tipo de identificación no fue encontrado o no existe.', 16, 1)
RETURN;
END
ELSE 

IF EXISTS (SELECT * FROM IdentificationTypes WHERE TypeID = @ID)
BEGIN
RAISERROR('No se puede eliminar el tipo de identificación debido a que está en uso.', 16, 1)
RETURN;
END

DELETE FROM IdentificationTypes WHERE TypeID = @ID
PRINT 'Se ha eliminado el tipo de identificación con éxito.'
END
GO











