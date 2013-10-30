SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Vendors`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Vendors` (
  `idVendors` INT NOT NULL AUTO_INCREMENT ,
  `VendorName` VARCHAR(100) NULL ,
  PRIMARY KEY (`idVendors`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Measures`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Measures` (
  `idMeasures` INT NOT NULL AUTO_INCREMENT ,
  `MeasureName` VARCHAR(50) NULL ,
  PRIMARY KEY (`idMeasures`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Products`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Products` (
  `idProducts` INT NOT NULL AUTO_INCREMENT ,
  `VendorId` INT NULL ,
  `ProductName` VARCHAR(100) NULL ,
  `MeasureId` INT NULL ,
  `PriceId` INT NULL ,
  `Vendors_idVendors` INT NOT NULL ,
  `Measures_idMeasures` INT NOT NULL ,
  PRIMARY KEY (`idProducts`, `Vendors_idVendors`) ,
  INDEX `fk_Products_Vendors_idx` (`Vendors_idVendors` ASC) ,
  INDEX `fk_Products_Measures1_idx` (`Measures_idMeasures` ASC) ,
  CONSTRAINT `fk_Products_Vendors`
    FOREIGN KEY (`Vendors_idVendors` )
    REFERENCES `mydb`.`Vendors` (`idVendors` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Products_Measures1`
    FOREIGN KEY (`Measures_idMeasures` )
    REFERENCES `mydb`.`Measures` (`idMeasures` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Prices`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Prices` (
  `idPrices` INT NOT NULL AUTO_INCREMENT ,
  `Price` DECIMAL(10,2) NULL ,
  `Products_idProducts` INT NOT NULL ,
  `Products_Vendors_idVendors` INT NOT NULL ,
  PRIMARY KEY (`idPrices`, `Products_idProducts`, `Products_Vendors_idVendors`) ,
  INDEX `fk_Prices_Products1_idx` (`Products_idProducts` ASC, `Products_Vendors_idVendors` ASC) ,
  CONSTRAINT `fk_Prices_Products1`
    FOREIGN KEY (`Products_idProducts` , `Products_Vendors_idVendors` )
    REFERENCES `mydb`.`Products` (`idProducts` , `Vendors_idVendors` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `mydb` ;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
