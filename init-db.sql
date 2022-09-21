-- create database Test;

use Test;

CREATE TABLE Users 
(
	ID int PRIMARY KEY AUTO_INCREMENT,
    Name nvarchar(100) NOT NULL,
    Age int NOT NULL,
    DateOfBirth DATE NOT NULL
);


CREATE TABLE Users_Hash 
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	Name nvarchar(100) NOT NULL,
    Age int NOT NULL,
    DateOfBirth DATE NOT NULL
) ENGINE = MEMORY;