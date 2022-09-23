-- create database Test;

use Test;

CREATE TABLE Users 
(
	ID int PRIMARY KEY AUTO_INCREMENT,
    Name nvarchar(100) NOT NULL,
    Age int NOT NULL,
    DateOfBirth DATE NOT NULL
);

CREATE INDEX btree_index ON Users (DateOfBirth) USING HASH;

SET GLOBAL innodb_adaptive_hash_index=OFF;

CREATE TABLE Users_Hash 
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	Name nvarchar(100) NOT NULL,
    Age int NOT NULL,
    DateOfBirth DATE NOT NULL
) ENGINE = MEMORY;

CREATE INDEX hash_index ON Users_Hash (DateOfBirth) USING HASH;

SET GLOBAL innodb_flush_log_at_trx_commit=2;