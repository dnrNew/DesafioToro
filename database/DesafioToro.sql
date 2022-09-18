#Desafio Toro - US TORO-004
CREATE DATABASE desafioToroDb;
USE desafioToroDb;

CREATE TABLE User (
  Id INT NOT NULL AUTO_INCREMENT,
  Name VARCHAR(50) NOT NULL,
  Cpf VARCHAR(11) NOT NULL, 
  Account VARCHAR(12) NOT NULL, 
  Balance DECIMAL(28, 8) NOT NULL DEFAULT 0, 
  PRIMARY KEY (Id))
ENGINE = INNODB;

CREATE TABLE Stock (
  Id INT NOT NULL AUTO_INCREMENT,
  Symbol VARCHAR(10) NOT NULL, 
  CurrentPrice DECIMAL(28, 8) NOT NULL, 
  PRIMARY KEY (Id))
ENGINE = INNODB;

CREATE TABLE UserAsset (
  Id INT NOT NULL AUTO_INCREMENT,
  Quantity INT NOT NULL, 
  UserId INT NOT NULL, 
  StockId INT NOT NULL, 
  PRIMARY KEY (Id),
  INDEX `fk_Asset_User_idx` (UserId ASC) VISIBLE,
  CONSTRAINT `fk_Asset_User` FOREIGN KEY (UserId) REFERENCES User (Id),
  INDEX `fk_Asset_Stock_idx` (StockId ASC) VISIBLE,
  CONSTRAINT `fk_Stock_User` FOREIGN KEY (StockId) REFERENCES Stock (Id)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION) 
ENGINE = INNODB;

##Inserts
#InsertUser
INSERT INTO User (Name, Cpf, Account, Balance) VALUES
('Shiryu de Dragão', '29247119081', '300123', 5000), 
('Ikki de Fénix', '26598973015', '300124', 1000), 
('Hyoga de Cisne', '87002973053', '300125', 2500); 

#InsertStock
INSERT INTO Stock (Symbol, CurrentPrice) VALUES 
('GOLL4', 10.21), 
('MGLU3', 4.46),
('VALE3', 68.25),
('PETR4', 30.78),
('BBSE3', 29.47),
('ITSA4', 9.28),
('WEGE3', 30.48),
('AERI3', 2.34),
('OIBR3', 0.53),
('ABEV3', 15.27);
