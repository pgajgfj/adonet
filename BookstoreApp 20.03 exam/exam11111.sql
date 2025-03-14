CREATE DATABASE BookstoreDb;
USE BookstoreDb;
CREATE TABLE Authors (
    AuthorId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);


CREATE TABLE Publishers (
    PublisherId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

-
CREATE TABLE Genres (
    GenreId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE Books (
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    AuthorId INT FOREIGN KEY REFERENCES Authors(AuthorId),
    PublisherId INT FOREIGN KEY REFERENCES Publishers(PublisherId),
    GenreId INT FOREIGN KEY REFERENCES Genres(GenreId),
    Pages INT NOT NULL,
    Year INT NOT NULL,
    Cost DECIMAL(10, 2) NOT NULL,
    SalePrice DECIMAL(10, 2) NOT NULL,
    IsSequel BIT NOT NULL,
    SequelBookId INT NULL FOREIGN KEY REFERENCES Books(BookId)
);

 
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);


CREATE TABLE BookOperations (
    OperationId INT IDENTITY(1,1) PRIMARY KEY,
    BookId INT FOREIGN KEY REFERENCES Books(BookId),
    OperationType NVARCHAR(50) NOT NULL, 
    Date DATETIME NOT NULL,
    Quantity INT NOT NULL
);