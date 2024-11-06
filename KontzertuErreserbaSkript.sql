-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema kontzertuerreserba
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema kontzertuerreserba
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `kontzertuerreserba` DEFAULT CHARACTER SET utf8mb3 ;
USE `kontzertuerreserba` ;

-- -----------------------------------------------------
-- Table `kontzertuerreserba`.`kontzertuak`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kontzertuerreserba`.`kontzertuak` (
  `idKontzertua` INT NOT NULL AUTO_INCREMENT,
  `Hiria` VARCHAR(45) NOT NULL,
  `Data` DATE NOT NULL,
  `Aforoa` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`idKontzertua`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `kontzertuerreserba`.`erreserbak`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kontzertuerreserba`.`erreserbak` (
  `idErreserbak` INT NOT NULL AUTO_INCREMENT,
  `DNI` VARCHAR(9) NOT NULL,
  `Abizena` VARCHAR(25) NOT NULL,
  `Izena` VARCHAR(25) NOT NULL,
  `Kantitatea` INT UNSIGNED NOT NULL,
  `Kontzertuak_idKontzertua` INT NOT NULL,
  PRIMARY KEY (`idErreserbak`),
  INDEX `fk_Erreserbak_Kontzertuak_idx` (`Kontzertuak_idKontzertua` ASC) VISIBLE,
  CONSTRAINT `fk_Erreserbak_Kontzertuak`
    FOREIGN KEY (`Kontzertuak_idKontzertua`)
    REFERENCES `kontzertuerreserba`.`kontzertuak` (`idKontzertua`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
