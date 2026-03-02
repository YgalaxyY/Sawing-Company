-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Aug 19, 2018 at 08:07 PM
-- Server version: 10.1.16-MariaDB
-- PHP Version: 7.0.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sewingfactory`
--

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `orderId` varchar(12) NOT NULL,
  `productId` varchar(11) NOT NULL,
  `cusId` varchar(11) NOT NULL,
  `workerId` varchar(50) NOT NULL,
  `orderDate` varchar(20) NOT NULL,
  `completionDate` varchar(20) NOT NULL,
  `basis` varchar(12) NOT NULL,
  `duration` int(12) NOT NULL,
  `TotalAmount` double NOT NULL,
  `workerAmount` double NOT NULL,
  `advancedAmount` double NOT NULL,
  `balance` double NOT NULL,
  `startQuantity` varchar(300) NOT NULL,
  `totalQuantity` varchar(20) NOT NULL,
  `perAmount` double NOT NULL,
  `DiscountAmount` double NOT NULL,
  `DiscountPer` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `product`
--

CREATE TABLE `product` (
  `productId` varchar(20) NOT NULL,
  `productModel` varchar(20) NOT NULL,
  `productBrand` varchar(25) NOT NULL,
  `productMakeYear` int(5) NOT NULL,
  `productColor` varchar(15) NOT NULL,
  `productDailyRate` double NOT NULL,
  `productMonthlyRate` double NOT NULL,
  `productStatus` text NOT NULL,
  `stock` varchar(300) NOT NULL,
  `unitmeter_type` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE `customer` (
  `customerId` varchar(100) NOT NULL,
  `customerFname` text NOT NULL,
  `customerLname` varchar(25) NOT NULL,
  `customerAddress` varchar(300) NOT NULL,
  `customerPhoneNo` int(25) NOT NULL,
  `customerIdentityCard` varchar(50) NOT NULL,
  `customerLicence` varchar(50) NOT NULL,
  `customerGender` char(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `worker`
--

CREATE TABLE `worker` (
  `workerId` varchar(100) NOT NULL,
  `workerFname` text NOT NULL,
  `workerLname` varchar(25) NOT NULL,
  `workerAddress` varchar(300) NOT NULL,
  `workerPhoneNo` int(25) NOT NULL,
  `workerIdentityCard` varchar(50) NOT NULL,
  `workerLicence` varchar(50) NOT NULL,
  `workerGender` char(2) NOT NULL,
  `workerStatus` varchar(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `oldorderdata`
--

CREATE TABLE `oldorderdata` (
  `orderId` varchar(20) NOT NULL,
  `productId` varchar(30) NOT NULL,
  `cusId` varchar(30) NOT NULL,
  `workerId` varchar(25) NOT NULL,
  `orderDate` varchar(20) NOT NULL,
  `completedDate` varchar(20) NOT NULL,
  `duration` int(11) NOT NULL,
  `workersalary` double NOT NULL,
  `advancedAmount` double NOT NULL,
  `balanceAmount` double NOT NULL,
  `extraDay` int(11) NOT NULL,
  `extraDayAmount` double NOT NULL,
  `extraworkerSalary` double NOT NULL,
  `extraQuantity` int(11) NOT NULL,
  `extraQuantityAmount` double NOT NULL,
  `TotalAmount` double NOT NULL,
  `basis` varchar(10) NOT NULL,
  `DiscountAmount` double NOT NULL,
  `DiscountPer` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `systemcontroller`
--

CREATE TABLE `systemcontroller` (
  `deleteCustomer` int(11) NOT NULL,
  `deleteWorker` int(11) NOT NULL,
  `deleteOrder` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`orderId`);

--
-- Indexes for table `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`productId`);

--
-- Indexes for table `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`customerId`);

--
-- Indexes for table `worker`
--
ALTER TABLE `worker`
  ADD PRIMARY KEY (`workerId`);

--
-- Indexes for table `oldorderdata`
--
ALTER TABLE `oldorderdata`
  ADD PRIMARY KEY (`orderId`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
