IF NOT EXISTS (SELECT * FROM sys.databases WHERE name='Prisninja')
CREATE DATABASE Prisninja

USE Prisninja

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Stores')
CREATE TABLE Stores
(
    ID         int PRIMARY KEY,
    Brand      smallint,
    Location_X float,
    Location_Y float,
    Address    varchar(60)
)

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Products')
CREATE TABLE Products
(
    EAN         int PRIMARY KEY,
    Name        varchar(60),
    Brand       varchar(60),
    Unit        float,
    Measurement varchar(30)
)

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Product_Stores')
CREATE TABLE Product_Stores
(
    Product int FOREIGN KEY REFERENCES Products (EAN),
    Store   int FOREIGN KEY REFERENCES Stores (ID),
    Price   float
)