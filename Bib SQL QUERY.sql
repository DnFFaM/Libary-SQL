/* Symbol /* .. */ is for explation and -- for sql commands */

/* To make a database with name Bib */
--CREATE DATABASE Bib; 

/* to Use Bib(database) instead of master, model, msdb, tempdb or the one we created early. */
--USE Bib;

/* to create an table with name author | and to give it some identitys. */
--CREATE TABLE Author(
--	Id INT IDENTITY(1,1) PRIMARY KEY,
--	FullName NVARCHAR(25)
--);

/* ^^ */
--CREATE TABLE Book(
--	Id INT IDENTITY(1,1) PRIMARY KEY,
--	Title NVARCHAR(50) UNIQUE NOT NULL, 
--	Author_Id INT FOREIGN KEY REFERENCES Author(Id)
--);

/* ^^^ */
--CREATE TABLE Borrower(
--	Id INT IDENTITY(1,1) PRIMARY KEY,
--	FullName NVARCHAR(25) NOT NULL,
--	Book_Id INT FOREIGN KEY REFERENCES Book(Id)
--);

/* to insert values/data inside the table(author) */
--INSERT INTO Author (FullName) VALUES ('J. K. Rowling');
--INSERT INTO Author (FullName) VALUES ('Harper Lee');
--INSERT INTO Author (FullName) VALUES ('F. Scott Fitzgerald');
--INSERT INTO Author (FullName) VALUES ('Jane Austen');
--INSERT INTO Author (FullName) VALUES ('J.D. Salinger');

/* ^^ */
--INSERT INTO Book (Title, Author_Id) VALUES ('Harry Potter and the Philosophers Stone', 1);
--INSERT INTO Book (Title, Author_Id) VALUES ('To Kill a Mockingbird', 2);
--INSERT INTO Book (Title, Author_Id) VALUES ('The Great Gatsby', 3);
--INSERT INTO Book (Title, Author_Id) VALUES ('Pride and Prejudice', 5);
--INSERT INTO Book (Title, Author_Id) VALUES ('The Cather in the Rye', 4);

/* ^^^ */
--INSERT INTO Borrower (FullName, Book_Id) VALUES ('Abdul Hansen', 1);
--INSERT INTO Borrower (FullName, Book_Id) VALUES ('Mad Maalgaard', 4);
--INSERT INTO Borrower (FullName, Book_Id) VALUES ('Iheb Malaki', 3);
--INSERT INTO Borrower (FullName, Book_Id) VALUES ('Adel Ajak', 2);
--INSERT INTO Borrower (FullName, Book_Id) VALUES ('Søren Sørensen', 5);

/* To create a view, and a view is the result set of a stored query */
 --CREATE VIEW RentedBooks AS
	--SELECT Author.FullName AS Auth, Book.Title AS Title, Borrower.FullName AS Borrower FROM Author
	--JOIN Book ON Book.Author_Id = Author.Id
	--JOIN Borrower ON Book.Id = Borrower.Book_Id;

/* ^^ */
--CREATE VIEW Tal AS
--	SELECT 
--	(SELECT COUNT(Id) FROM Book) AS Books,
--	(SELECT COUNT(Id) FROM Author) AS Authors,
--	(SELECT COUNT(Id) FROM Borrower) AS Borrowers;

--GO

/* To view our table */
--SELECT * FROM Tal;

/* delete an existing table */
--DROP PROCEDURE IF EXISTS Test;

--GO

/* to create an procedure, and that is a prepared sql code that you can save, and use it later. like in c# */
--CREATE PROCEDURE Test @navn NVARCHAR(25)
--AS
--	UPDATE Author SET FullName = @navn;

--GO
/* change in table TEST that include navn to Adel */
--EXEC Test @navn = 'Adel';

--SELECT FullName FROM Author;


SELECT * FROM RentedBooks;