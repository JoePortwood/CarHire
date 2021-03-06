USE [master]
GO
/****** Object:  Database [CarHire]    Script Date: 26/04/2015 01:18:52 ******/
CREATE DATABASE [CarHire]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CarHire', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\CarHire.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CarHire_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\CarHire_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CarHire] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CarHire].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CarHire] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CarHire] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CarHire] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CarHire] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CarHire] SET ARITHABORT OFF 
GO
ALTER DATABASE [CarHire] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CarHire] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CarHire] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CarHire] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CarHire] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CarHire] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CarHire] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CarHire] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CarHire] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CarHire] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CarHire] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CarHire] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CarHire] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CarHire] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CarHire] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CarHire] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CarHire] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CarHire] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CarHire] SET  MULTI_USER 
GO
ALTER DATABASE [CarHire] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CarHire] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CarHire] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CarHire] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [CarHire]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressID] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressType] [int] NOT NULL,
	[Line1] [varchar](50) NULL,
	[Line2] [varchar](50) NULL,
	[City] [varchar](50) NOT NULL,
	[ZipOrPostcode] [varchar](50) NOT NULL,
	[CountyStateProvince] [varchar](50) NULL,
	[Country] [varchar](50) NOT NULL,
 CONSTRAINT [addresses_addressid_pk] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Companies](
	[CompanyID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[CompanyName] [varchar](50) NOT NULL,
	[CompanyDescription] [varchar](300) NULL,
	[LicensingDetails] [varchar](1000) NOT NULL,
	[PhoneNo] [varchar](20) NOT NULL,
	[EmailAddress] [varchar](150) NOT NULL,
 CONSTRAINT [companies_companyID_pk] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CountryMatrix]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CountryMatrix](
	[CountryID] [int] NOT NULL,
	[Country] [varchar](100) NOT NULL,
	[CountryCode] [varchar](2) NOT NULL,
 CONSTRAINT [countrymatrix_countryid_pk] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [bigint] NULL,
	[UserName] [varchar](50) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[Forename] [varchar](50) NOT NULL,
	[Title] [varchar](10) NOT NULL,
	[LicenseNo] [varchar](50) NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[PhoneNo] [varchar](20) NOT NULL,
	[MobileNo] [varchar](20) NULL,
	[EmailAddress] [varchar](150) NOT NULL,
 CONSTRAINT [customers_customerid_pk] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HolidayOpeningTimes]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HolidayOpeningTimes](
	[LocationID] [bigint] NOT NULL,
	[HolidayStartDate] [date] NOT NULL,
	[AltOpenTime] [datetime] NULL,
	[AltCloseTime] [datetime] NULL,
	[Closed] [bit] NOT NULL,
 CONSTRAINT [holidayopeningtimes_locationid__dayofweek_holidaystartdate_ck] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[HolidayStartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Locations]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Locations](
	[LocationID] [bigint] IDENTITY(1,1) NOT NULL,
	[LocationName] [varchar](100) NOT NULL,
	[OwnerName] [varchar](50) NOT NULL,
	[Capacity] [int] NULL,
	[AddressLine1] [varchar](50) NULL,
	[AddressLine2] [varchar](50) NULL,
	[City] [varchar](50) NOT NULL,
	[ZipOrPostcode] [varchar](50) NOT NULL,
	[CountyStateProvince] [varchar](50) NULL,
	[Country] [varchar](50) NOT NULL,
	[PhoneNo] [varchar](20) NOT NULL,
	[EmailAddress] [varchar](150) NOT NULL,
	[Longitude] [float] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [locations_locationid_pk] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OpeningTimes]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OpeningTimes](
	[LocationID] [bigint] NOT NULL,
	[DayOfTheWeek] [int] NOT NULL,
	[OpenTime] [datetime] NOT NULL,
	[CloseTime] [datetime] NOT NULL,
	[Closed] [bit] NOT NULL,
 CONSTRAINT [openingtimes_locationid_dayoftheweek_ck] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[DayOfTheWeek] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerID] [bigint] NOT NULL,
	[AddressID] [bigint] NULL,
	[HireStart] [datetime] NOT NULL,
	[HireEnd] [datetime] NOT NULL,
	[BookingCreated] [datetime] NOT NULL,
	[OrderStatus] [varchar](100) NOT NULL,
	[PayPalPayerID] [varchar](500) NOT NULL,
 CONSTRAINT [orders_carid_pk] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PasswordResetRequest]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PasswordResetRequest](
	[RequestID] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountType] [bigint] NOT NULL,
	[AccountID] [bigint] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [passwordresetrequest_requestid_pk] PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SIPPCodes]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SIPPCodes](
	[Type] [int] NOT NULL,
	[Letter] [char](1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [sippcodes_type_letter_description_ck] PRIMARY KEY CLUSTERED 
(
	[Type] ASC,
	[Letter] ASC,
	[Description] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserAccess]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAccess](
	[UserType] [bigint] NOT NULL,
	[TypeID] [bigint] NOT NULL,
	[Pwd] [varchar](max) NOT NULL,
 CONSTRAINT [useraccess_usertype_typeid_ck] PRIMARY KEY CLUSTERED 
(
	[UserType] ASC,
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserAction]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAction](
	[UserActionID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserType] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[ItemChangedID] [bigint] NOT NULL,
	[ActionType] [bigint] NOT NULL,
	[ActionTime] [datetime] NOT NULL,
	[ActionDescription] [varchar](1000) NULL,
 CONSTRAINT [useraction_useractionid_pk] PRIMARY KEY CLUSTERED 
(
	[UserActionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VehicleOrders]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleOrders](
	[VehiclesAvailableID] [bigint] NOT NULL,
	[OrderID] [bigint] NOT NULL,
 CONSTRAINT [vehicleorders_vehiclesavailableid_orderid_ck] PRIMARY KEY CLUSTERED 
(
	[VehiclesAvailableID] ASC,
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vehicles](
	[VehicleID] [bigint] IDENTITY(1,1) NOT NULL,
	[Manufacturer] [varchar](30) NOT NULL,
	[Model] [varchar](30) NOT NULL,
	[SIPPCode] [varchar](4) NOT NULL,
	[MPG] [float] NOT NULL,
	[ImageLoc] [varchar](100) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [vehicles_vehicleid_pk] PRIMARY KEY CLUSTERED 
(
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VehiclesAvailable]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VehiclesAvailable](
	[VehiclesAvailableID] [bigint] IDENTITY(1,1) NOT NULL,
	[VehicleID] [bigint] NOT NULL,
	[LocationID] [bigint] NOT NULL,
	[TotalVehicles] [int] NOT NULL,
	[Currency] [varchar](20) NOT NULL,
	[BasePrice] [float] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [vehiclesavailable_vehiclesavailableid_pk] PRIMARY KEY CLUSTERED 
(
	[VehiclesAvailableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[v_Companies]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[v_Companies] AS

	SELECT Companies.CompanyID, Companies.UserName, Companies.CompanyName, Companies.CompanyDescription,
	Companies.LicensingDetails, Companies.PhoneNo, Companies.EmailAddress,
	UserAccess.UserType, UserAccess.TypeID, UserAccess.Pwd
	FROM Companies
	JOIN UserAccess ON Companies.CompanyID = UserAccess.TypeID
		AND UserAccess.UserType = 2

GO
/****** Object:  View [dbo].[v_Customers]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[v_Customers] AS

	SELECT Customers.CustomerID, Customers.CompanyID, Customers.UserName, Customers.Surname, Customers.Forename, Companies.CompanyName, 
	Customers.Title, Customers.LicenseNo, Customers.IssueDate, Customers.ExpirationDate,
	Customers.DateOfBirth, Customers.PhoneNo, Customers.MobileNo, Customers.EmailAddress,
	UserAccess.UserType, UserAccess.TypeID, UserAccess.Pwd
	FROM Customers
	LEFT JOIN Companies ON Companies.CompanyID = Customers.CompanyID
	JOIN UserAccess ON UserAccess.TypeID = Customers.CustomerID
		AND UserAccess.UserType = 4

GO
/****** Object:  View [dbo].[v_OpeningTimes]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_OpeningTimes] AS
SELECT LocationID, DayOfTheWeek, CONVERT(VARCHAR(8),OpenTime,108) AS OpenTime,
CONVERT(VARCHAR(8),CloseTime,108) AS CloseTime, Closed
FROM OpeningTimes
GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

INSERT [dbo].[Addresses] ([AddressID], [AddressType], [Line1], [Line2], [City], [ZipOrPostcode], [CountyStateProvince], [Country]) VALUES (1, 1, N'3 Gordon Road', NULL, N'Wimborne', N'BH21 2AP', NULL, N'United Kingdom')
SET IDENTITY_INSERT [dbo].[Addresses] OFF
SET IDENTITY_INSERT [dbo].[Companies] ON 

INSERT [dbo].[Companies] ([CompanyID], [UserName], [CompanyName], [CompanyDescription], [LicensingDetails], [PhoneNo], [EmailAddress]) VALUES (1, N'Portwood', N'Portwood Consulting', N'Consultants', N'gsgdfg', N'+4446567652', N'joe.portwood@hotmail.com')
INSERT [dbo].[Companies] ([CompanyID], [UserName], [CompanyName], [CompanyDescription], [LicensingDetails], [PhoneNo], [EmailAddress]) VALUES (2, N'doctorw', N'Wright Doctors', N'Doctors', N'dfgfd', N'+44 324 366 45', N'doctors@doc.com')
INSERT [dbo].[Companies] ([CompanyID], [UserName], [CompanyName], [CompanyDescription], [LicensingDetails], [PhoneNo], [EmailAddress]) VALUES (3, N'enterpriseCompany', N'Enterprise', N'Provides hire cars', N'hgfhgf', N'+445069568650', N'joe.portwood@hotmail.com')
INSERT [dbo].[Companies] ([CompanyID], [UserName], [CompanyName], [CompanyDescription], [LicensingDetails], [PhoneNo], [EmailAddress]) VALUES (4, N'EuropcarCompany', N'Europcar', N'Hires cars to people', N'dhhfgh', N'+444557654567', N'joe.portwood@hotmail.com')
INSERT [dbo].[Companies] ([CompanyID], [UserName], [CompanyName], [CompanyDescription], [LicensingDetails], [PhoneNo], [EmailAddress]) VALUES (5, N'AvisCompany', N'Avis', N'Hires cars to people', N'fhgfhgf', N'+444367653345', N'joe.portwood@hotmail.com')
INSERT [dbo].[Companies] ([CompanyID], [UserName], [CompanyName], [CompanyDescription], [LicensingDetails], [PhoneNo], [EmailAddress]) VALUES (6, N'HertzCompany', N'Hertz', N'Hires cars to people', N'MIT', N'+44 7563 214454', N'hertz@gmail.com')
SET IDENTITY_INSERT [dbo].[Companies] OFF
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (1, N'Singapore', N'SG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (2, N'China', N'CN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (3, N'Malaysia', N'MY')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (4, N'United States', N'US')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (5, N'Canada', N'CA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (6, N'Albania', N'AL')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (7, N'Algeria', N'DZ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (8, N'Amerin Samoa', N'DS')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (9, N'Andorra', N'AD')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (10, N'Angola', N'AO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (11, N'Anguilla', N'AI')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (12, N'Antarctica', N'AQ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (13, N'Antigua and/or Barbuda', N'AG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (14, N'Argentina', N'AR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (15, N'Armenia', N'AM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (16, N'Aruba', N'AW')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (17, N'Austria', N'AT')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (18, N'Azerbaijan', N'AZ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (19, N'Bahamas', N'BS')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (20, N'Bahrain', N'BH')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (21, N'Bangladesh', N'BD')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (22, N'Barbados', N'BB')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (23, N'Belarus', N'BY')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (24, N'Belgium', N'BE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (25, N'Belize', N'BZ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (26, N'Benin', N'BJ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (27, N'Bermuda', N'BM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (28, N'Bhutan', N'BT')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (29, N'Bolivia', N'BO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (30, N'Bosnia and Herzegovina', N'BA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (31, N'Botswana', N'BW')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (32, N'Bouvet Island', N'BV')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (33, N'Brazil', N'BR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (34, N'British lndian Ocean Territory', N'IO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (35, N'Brunei Darussalam', N'BN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (36, N'Bulgaria', N'BG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (37, N'Burkina Faso', N'BF')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (38, N'Burundi', N'BI')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (39, N'Cambodia', N'KH')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (40, N'Cameroon', N'CM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (41, N'Cape Verde', N'CV')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (42, N'Cayman Islands', N'KY')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (43, N'Central African Republic', N'CF')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (44, N'Chad', N'TD')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (45, N'Chile', N'CL')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (46, N'Christmas Island', N'CX')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (47, N'Cocos (Keeling) Islands', N'CC')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (48, N'Colombia', N'CO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (49, N'Comoros', N'KM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (50, N'Congo', N'CG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (51, N'Cook Islands', N'CK')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (52, N'Costa Rica', N'CR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (53, N'Croatia (Hrvatska)', N'HR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (54, N'Cuba', N'CU')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (55, N'Cyprus', N'CY')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (56, N'Czech Republic', N'CZ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (57, N'Denmark', N'DK')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (58, N'Djibouti', N'DJ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (59, N'Dominica', N'DM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (60, N'Dominican Republic', N'DO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (61, N'East Timor', N'TP')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (62, N'Ecudaor', N'EC')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (63, N'Egypt', N'EG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (64, N'El Salvador', N'SV')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (65, N'Equatorial Guinea', N'GQ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (66, N'Eritrea', N'ER')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (67, N'Estonia', N'EE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (68, N'Ethiopia', N'ET')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (69, N'Falkland Islands (Malvinas)', N'FK')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (70, N'Faroe Islands', N'FO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (71, N'Fiji', N'FJ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (72, N'Finland', N'FI')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (73, N'France', N'FR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (74, N'France, Metropolitan', N'FX')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (75, N'French Guiana', N'GF')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (76, N'French Polynesia', N'PF')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (77, N'French Southern Territories', N'TF')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (78, N'Gabon', N'GA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (79, N'Gambia', N'GM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (80, N'Georgia', N'GE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (81, N'Germany', N'DE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (82, N'Ghana', N'GH')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (83, N'Gibraltar', N'GI')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (84, N'Greece', N'GR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (85, N'Greenland', N'GL')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (86, N'Grenada', N'GD')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (87, N'Guadeloupe', N'GP')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (88, N'Guam', N'GU')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (89, N'Guatemala', N'GT')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (90, N'Guinea', N'GN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (91, N'Guinea-Bissau', N'GW')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (92, N'Guyana', N'GY')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (93, N'Haiti', N'HT')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (94, N'Heard and Mc Donald Islands', N'HM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (95, N'Honduras', N'HN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (96, N'Hong Kong', N'HK')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (97, N'Hungary', N'HU')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (98, N'Iceland', N'IS')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (99, N'India', N'IN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (100, N'Indonesia', N'ID')
GO
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (101, N'Iran (Islamic Republic of)', N'IR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (102, N'Iraq', N'IQ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (103, N'Ireland', N'IE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (104, N'Israel', N'IL')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (105, N'Italy', N'IT')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (106, N'Ivory Coast', N'CI')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (107, N'Jamaica', N'JM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (108, N'Japan', N'JP')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (109, N'Jordan', N'JO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (110, N'Kazakhstan', N'KZ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (111, N'Kenya', N'KE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (112, N'Kiribati', N'KI')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (113, N'Korea, Democratic People''s Republic of', N'KP')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (114, N'Korea, Republic of', N'KR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (115, N'Kuwait', N'KW')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (116, N'Kyrgyzstan', N'KG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (117, N'Lao People''s Democratic Republic', N'LA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (118, N'Latvia', N'LV')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (119, N'Lebanon', N'LB')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (120, N'Lesotho', N'LS')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (121, N'Liberia', N'LR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (122, N'Libyan Arab Jamahiriya', N'LY')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (123, N'Liechtenstein', N'LI')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (124, N'Lithuania', N'LT')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (125, N'Luxembourg', N'LU')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (126, N'Macau', N'MO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (127, N'Macedonia', N'MK')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (128, N'Madagascar', N'MG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (129, N'Malawi', N'MW')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (130, N'New Zealand', N'NZ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (131, N'Maldives', N'MV')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (132, N'Mali', N'ML')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (133, N'Malta', N'MT')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (134, N'Marshall Islands', N'MH')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (135, N'Martinique', N'MQ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (136, N'Mauritania', N'MR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (137, N'Mauritius', N'MU')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (138, N'Mayotte', N'TY')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (139, N'Mexico', N'MX')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (140, N'Micronesia, Federated States of', N'FM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (141, N'Moldova, Republic of', N'MD')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (142, N'Monaco', N'MC')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (143, N'Mongolia', N'MN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (144, N'Montserrat', N'MS')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (145, N'Morocco', N'MA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (146, N'Mozambique', N'MZ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (147, N'Myanmar', N'MM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (148, N'Namibia', N'NA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (149, N'Nauru', N'NR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (150, N'Nepal', N'NP')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (151, N'Netherlands', N'NL')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (152, N'Netherlands Antilles', N'AN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (153, N'New Caledonia', N'NC')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (154, N'Nicaragua', N'NI')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (155, N'Niger', N'NE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (156, N'Nigeria', N'NG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (157, N'Niue', N'NU')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (158, N'Norfork Island', N'NF')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (159, N'Northern Mariana Islands', N'MP')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (160, N'Norway', N'NO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (161, N'Oman', N'OM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (162, N'Pakistan', N'PK')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (163, N'Palau', N'PW')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (164, N'Panama', N'PA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (165, N'Papua New Guinea', N'PG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (166, N'Paraguay', N'PY')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (167, N'Peru', N'PE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (168, N'Philippines', N'PH')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (169, N'Pitcairn', N'PN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (170, N'Poland', N'PL')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (171, N'Portugal', N'PT')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (172, N'Puerto Rico', N'PR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (173, N'Qatar', N'QA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (174, N'Reunion', N'RE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (175, N'Romania', N'RO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (176, N'Russian Federation', N'RU')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (177, N'Rwanda', N'RW')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (178, N'Saint Kitts and Nevis', N'KN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (179, N'Saint Lucia', N'LC')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (180, N'Saint Vincent and the Grenadines', N'VC')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (181, N'Samoa', N'WS')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (182, N'San Marino', N'SM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (183, N'Sao Tome and Principe', N'ST')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (184, N'Saudi Arabia', N'SA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (185, N'Senegal', N'SN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (186, N'Seychelles', N'SC')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (187, N'Sierra Leone', N'SL')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (188, N'Slovakia', N'SK')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (189, N'Slovenia', N'SI')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (190, N'Solomon Islands', N'SB')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (191, N'Somalia', N'SO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (192, N'South Africa', N'ZA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (193, N'South Georgia South Sandwich Islands', N'GS')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (194, N'Spain', N'ES')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (195, N'Sri Lanka', N'LK')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (196, N'St. Helena', N'SH')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (197, N'St. Pierre and Miquelon', N'PM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (198, N'Sudan', N'SD')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (199, N'Suriname', N'SR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (200, N'Svalbarn and Jan Mayen Islands', N'SJ')
GO
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (201, N'Swaziland', N'SZ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (202, N'Sweden', N'SE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (203, N'Switzerland', N'CH')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (204, N'Syrian Arab Republic', N'SY')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (205, N'Taiwan', N'TW')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (206, N'Tajikistan', N'TJ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (207, N'Tanzania, United Republic of', N'TZ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (208, N'Thailand', N'TH')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (209, N'Togo', N'TG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (210, N'Tokelau', N'TK')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (211, N'Tonga', N'TO')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (212, N'Trinidad and Tobago', N'TT')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (213, N'Tunisia', N'TN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (214, N'Turkey', N'TR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (215, N'Turkmenistan', N'TM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (216, N'Turks and Caicos Islands', N'TC')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (217, N'Tuvalu', N'TV')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (218, N'Uganda', N'UG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (219, N'Ukraine', N'UA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (220, N'United Arab Emirates', N'AE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (221, N'United Kingdom', N'GB')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (222, N'Australia', N'AU')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (223, N'Uruguay', N'UY')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (224, N'Uzbekistan', N'UZ')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (225, N'Vanuatu', N'VU')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (226, N'Vatican City State', N'VA')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (227, N'Venezuela', N'VE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (228, N'Vietnam', N'VN')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (229, N'Virigan Islands (British)', N'VG')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (230, N'Virgin Islands (U.S.)', N'VI')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (231, N'Wallis and Futuna Islands', N'WF')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (232, N'Western Sahara', N'EH')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (233, N'Yemen', N'YE')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (234, N'Yugoslavia', N'YU')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (235, N'Zaire', N'ZR')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (236, N'Zambia', N'ZM')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (237, N'Zimbabwe', N'ZW')
INSERT [dbo].[CountryMatrix] ([CountryID], [Country], [CountryCode]) VALUES (238, N'Afghanistan', N'AF')
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [CompanyID], [UserName], [Surname], [Forename], [Title], [LicenseNo], [IssueDate], [ExpirationDate], [DateOfBirth], [PhoneNo], [MobileNo], [EmailAddress]) VALUES (1, 6, N'SmithJ', N'Smith', N'John', N'Mr', N'56546456dff324', CAST(N'1921-07-16 00:00:00.000' AS DateTime), CAST(N'2043-06-16 00:00:00.000' AS DateTime), CAST(N'1986-04-16 00:00:00.000' AS DateTime), N'+44 345 356 5543', NULL, N'joe.portwood@hotmail.com')
INSERT [dbo].[Customers] ([CustomerID], [CompanyID], [UserName], [Surname], [Forename], [Title], [LicenseNo], [IssueDate], [ExpirationDate], [DateOfBirth], [PhoneNo], [MobileNo], [EmailAddress]) VALUES (2, NULL, N'PortwoodB', N'Portwood', N'Ben', N'Mr', N'2144YFFISDFY453FGD', CAST(N'1997-08-14 00:00:00.000' AS DateTime), CAST(N'2081-04-14 00:00:00.000' AS DateTime), CAST(N'1922-04-14 00:00:00.000' AS DateTime), N'+44 56 5686 456', NULL, N'f@foo.com')
INSERT [dbo].[Customers] ([CustomerID], [CompanyID], [UserName], [Surname], [Forename], [Title], [LicenseNo], [IssueDate], [ExpirationDate], [DateOfBirth], [PhoneNo], [MobileNo], [EmailAddress]) VALUES (3, NULL, N'test', N'test', N'foo', N'Mr', N'54665fghfgh456546', CAST(N'1917-02-05 00:00:00.000' AS DateTime), CAST(N'2028-03-15 00:00:00.000' AS DateTime), CAST(N'1931-04-17 00:00:00.000' AS DateTime), N'+44645654345', NULL, N'joe.portwood@hotmail.com')
INSERT [dbo].[Customers] ([CustomerID], [CompanyID], [UserName], [Surname], [Forename], [Title], [LicenseNo], [IssueDate], [ExpirationDate], [DateOfBirth], [PhoneNo], [MobileNo], [EmailAddress]) VALUES (4, 3, N'doeT', N'Doe', N'Tom', N'Rev', N'4305407443543543DFGFD', CAST(N'1938-06-08 00:00:00.000' AS DateTime), CAST(N'2055-06-10 00:00:00.000' AS DateTime), CAST(N'1990-07-15 00:00:00.000' AS DateTime), N'+4406708533455', NULL, N'joe.portwood@hotmail.com')
SET IDENTITY_INSERT [dbo].[Customers] OFF
INSERT [dbo].[HolidayOpeningTimes] ([LocationID], [HolidayStartDate], [AltOpenTime], [AltCloseTime], [Closed]) VALUES (3, CAST(N'2015-04-22' AS Date), CAST(N'2015-04-20 07:15:00.000' AS DateTime), CAST(N'2015-04-20 20:30:00.000' AS DateTime), 0)
INSERT [dbo].[HolidayOpeningTimes] ([LocationID], [HolidayStartDate], [AltOpenTime], [AltCloseTime], [Closed]) VALUES (3, CAST(N'2015-04-23' AS Date), CAST(N'2015-04-20 08:00:00.000' AS DateTime), CAST(N'2015-04-20 21:30:00.000' AS DateTime), 0)
INSERT [dbo].[HolidayOpeningTimes] ([LocationID], [HolidayStartDate], [AltOpenTime], [AltCloseTime], [Closed]) VALUES (5, CAST(N'2015-04-23' AS Date), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Locations] ON 

INSERT [dbo].[Locations] ([LocationID], [LocationName], [OwnerName], [Capacity], [AddressLine1], [AddressLine2], [City], [ZipOrPostcode], [CountyStateProvince], [Country], [PhoneNo], [EmailAddress], [Longitude], [Latitude], [Active]) VALUES (1, N'Avis Plymouth', N'Avis', 300, N'15 Kensington Road', NULL, N'Plymouth', N'PL4 7LU', NULL, N'United Kingdom', N'+44657658768', N'joe.portwood@hotmail.com', -4.13058739999997, 50.3799473, 1)
INSERT [dbo].[Locations] ([LocationID], [LocationName], [OwnerName], [Capacity], [AddressLine1], [AddressLine2], [City], [ZipOrPostcode], [CountyStateProvince], [Country], [PhoneNo], [EmailAddress], [Longitude], [Latitude], [Active]) VALUES (2, N'Europcar London', N'Europcar ', NULL, N'3 Queens Avenue', NULL, N'London', N'N20 0JB', NULL, N'United Kingdom', N'+44 56 5445 7651', N'europcarlondon@europcar.com', -0.166744200000039, 51.6276036, 1)
INSERT [dbo].[Locations] ([LocationID], [LocationName], [OwnerName], [Capacity], [AddressLine1], [AddressLine2], [City], [ZipOrPostcode], [CountyStateProvince], [Country], [PhoneNo], [EmailAddress], [Longitude], [Latitude], [Active]) VALUES (3, N'Enterprise Newcastle', N'Enterprise', NULL, N'New Bridge Street West', NULL, N'Newcastle', N'NE1 8BS', NULL, N'United Kingdom', N'+440404950594', N'enterprisenewcastle@enterprise.com', -1.6081724000000577, 54.9745338, 1)
INSERT [dbo].[Locations] ([LocationID], [LocationName], [OwnerName], [Capacity], [AddressLine1], [AddressLine2], [City], [ZipOrPostcode], [CountyStateProvince], [Country], [PhoneNo], [EmailAddress], [Longitude], [Latitude], [Active]) VALUES (4, N'Penguin Cherbourg', N'Penguin', NULL, N'4 Rue des Tanneries', NULL, N'Cherbourg-Octeville', N'50100', N'Normandie', N'France', N'+33 7 56 74 56 75', N'penguincherbourg@penguin.com', -1.6230674999999337, 49.6330321, 1)
INSERT [dbo].[Locations] ([LocationID], [LocationName], [OwnerName], [Capacity], [AddressLine1], [AddressLine2], [City], [ZipOrPostcode], [CountyStateProvince], [Country], [PhoneNo], [EmailAddress], [Longitude], [Latitude], [Active]) VALUES (5, N'Hertz Bristol', N'Hertz', NULL, N'Senate House', N'Tyndall Avenue', N'Bristol', N'BS8 1TH', N'Gloucestershire', N'United Kingdom', N'+44 7567 778456', N'hertzbristol@gmail.com', -2.60334250000005, 51.4590682, 1)
SET IDENTITY_INSERT [dbo].[Locations] OFF
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (1, 1, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (1, 2, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (1, 3, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (1, 4, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (1, 5, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (1, 6, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (1, 7, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (2, 1, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (2, 2, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (2, 3, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (2, 4, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (2, 5, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (2, 6, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (2, 7, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (3, 1, CAST(N'2015-04-06 07:15:00.000' AS DateTime), CAST(N'2015-04-06 10:45:00.000' AS DateTime), 0)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (3, 2, CAST(N'2015-04-06 07:15:00.000' AS DateTime), CAST(N'2015-04-06 10:45:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (3, 3, CAST(N'2015-04-06 07:15:00.000' AS DateTime), CAST(N'2015-04-06 10:45:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (3, 4, CAST(N'2015-04-06 07:15:00.000' AS DateTime), CAST(N'2015-04-06 10:45:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (3, 5, CAST(N'2015-04-06 07:15:00.000' AS DateTime), CAST(N'2015-04-06 10:45:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (3, 6, CAST(N'2015-04-06 07:15:00.000' AS DateTime), CAST(N'2015-04-06 10:45:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (3, 7, CAST(N'2015-04-06 07:15:00.000' AS DateTime), CAST(N'2015-04-06 10:45:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (4, 1, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (4, 2, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (4, 3, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (4, 4, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (4, 5, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (4, 6, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (4, 7, CAST(N'1900-01-01 00:00:00.000' AS DateTime), CAST(N'1900-01-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (5, 1, CAST(N'2015-04-23 06:15:00.000' AS DateTime), CAST(N'2015-04-23 06:30:00.000' AS DateTime), 0)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (5, 2, CAST(N'2015-04-23 02:00:00.000' AS DateTime), CAST(N'2015-04-23 14:30:00.000' AS DateTime), 0)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (5, 3, CAST(N'2015-04-23 02:15:00.000' AS DateTime), CAST(N'2015-04-23 17:30:00.000' AS DateTime), 0)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (5, 4, CAST(N'2015-04-23 01:15:00.000' AS DateTime), CAST(N'2015-04-23 14:30:00.000' AS DateTime), 0)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (5, 5, CAST(N'2015-04-23 01:15:00.000' AS DateTime), CAST(N'2015-04-23 19:30:00.000' AS DateTime), 0)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (5, 6, CAST(N'2015-04-23 02:15:00.000' AS DateTime), CAST(N'2015-04-23 15:30:00.000' AS DateTime), 0)
INSERT [dbo].[OpeningTimes] ([LocationID], [DayOfTheWeek], [OpenTime], [CloseTime], [Closed]) VALUES (5, 7, CAST(N'2015-04-23 01:15:00.000' AS DateTime), CAST(N'2015-04-23 15:30:00.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [CustomerID], [AddressID], [HireStart], [HireEnd], [BookingCreated], [OrderStatus], [PayPalPayerID]) VALUES (44, 1, NULL, CAST(N'2015-04-26 10:00:00.000' AS DateTime), CAST(N'2015-04-27 10:00:00.000' AS DateTime), CAST(N'2015-04-25 23:45:19.140' AS DateTime), N'Cancelled', N'JA2VYJP2US6X8')
SET IDENTITY_INSERT [dbo].[Orders] OFF
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'C', N'Compact')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'D', N'Compact Elite')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'E', N'Economy')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'F', N'Fullsize')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'G', N'Fullsize Elite')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'H', N'Economy Elite')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'I', N'Intermediate')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'J', N'Intermediate Elite')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'L', N'Luxury')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'M', N'Mini')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'N', N'Mini Elite')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'O', N'Oversize')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'P', N'Premium')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'R', N'Standard Elite')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'S', N'Standard')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'U', N'Premium Elite')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'W', N'Luxury Elite')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (1, N'X', N'Special')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'B', N'2/3 Door')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'C', N'2/4 Door')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'D', N'4/5 Door')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'E', N'Coupe')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'F', N'SUV')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'G', N'Crossover')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'H', N'Motor Home')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'J', N'Open Air All Terrain')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'K', N'Commercial Van/Truck')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'L', N'Limousine')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'M', N'Monospace')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'N', N'Roadster')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'P', N'Pickip Regular Cab')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'Q', N'Pickup Extended Cab')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'R', N'Recreational')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'S', N'Sport')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'T', N'Convertable')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'V', N'Passanger Van')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'W', N'Wagon/Estate')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'X', N'Special')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'Y', N'2 Wheel Vehicle')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (2, N'Z', N'Special Offer Car')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (3, N'A', N'Auto Drive')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (3, N'B', N'Auto, 4WD')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (3, N'C', N'Manual, AWD')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (3, N'D', N'Auto, AWD')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (3, N'M', N'Manual Drive')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (3, N'N', N'Manual, 4WD')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'A', N'Hydrogen, AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'B', N'Hydrogen, no AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'C', N'Electric, no AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'D', N'Diesel, AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'E', N'Electric, AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'F', N'Multi fuel, no AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'H', N'Hybrid, AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'I', N'Hybrid, no AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'L', N'LPG/Gas, AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'M', N'Multi fuel, AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'N', N'Unspecified fuel, no AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'Q', N'Diesel, no AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'R', N'Unspecified fuel, AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'S', N'LPG/Gas, no AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'U', N'Ethanol, AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'V', N'Petrol, AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'X', N'Ethanol, no AC')
INSERT [dbo].[SIPPCodes] ([Type], [Letter], [Description]) VALUES (4, N'Z', N'Petrol, no AC')
INSERT [dbo].[UserAccess] ([UserType], [TypeID], [Pwd]) VALUES (2, 1, N'1000:Qatna8Y6S8ElVEVIhE03fZFbRWgaBZwc:wSM/RBSKlnQOALDxnCrKTZT0sz5hqjoY')
INSERT [dbo].[UserAccess] ([UserType], [TypeID], [Pwd]) VALUES (2, 2, N'1000:BKGYIgAHmnBBnwLRhJKPpBtrhKyRaEYX:kJt2JR/MTRkd1gq9fLlq+fdR2cVba7Ch')
INSERT [dbo].[UserAccess] ([UserType], [TypeID], [Pwd]) VALUES (2, 3, N'1000:jAX9hGID5dJ3/jrjQz5gDm9C7/LUbgvb:UswcGRFQbTlMwToiApFR6SVULgQU8HQO')
INSERT [dbo].[UserAccess] ([UserType], [TypeID], [Pwd]) VALUES (2, 4, N'1000:mUW8J9wLqsGpK60h/Yy9sqR9YlF4nJ7s:UZJCH8Kql1gE14eKy9nqZHg/RpE6U6sK')
INSERT [dbo].[UserAccess] ([UserType], [TypeID], [Pwd]) VALUES (2, 5, N'1000:/uVk5XZfVN5cY8K94fC86upGtTt37LuO:HgEI96QI1P5U5VdGKU4/OmSbK+HJPUXo')
INSERT [dbo].[UserAccess] ([UserType], [TypeID], [Pwd]) VALUES (2, 6, N'1000:9oBcHZwbPj7KeVoFcJx+Z2EGeKT+QW7w:f2svZyJP2d/ed6uwIArxEAQAqOHyGkU9')
INSERT [dbo].[UserAccess] ([UserType], [TypeID], [Pwd]) VALUES (4, 1, N'1000:Gag9SJhJsBtGi91MALb7UFtC0+gelOEf:CiPSY6rQoZlGaymbw8OrylERxnDmCpNr')
INSERT [dbo].[UserAccess] ([UserType], [TypeID], [Pwd]) VALUES (4, 2, N'1000:HVPrwUWOOA7FQhGjYJiI43FTF+nXcu6r:F52rnqul7AF+eam6KMu22jlawR4VScBZ')
INSERT [dbo].[UserAccess] ([UserType], [TypeID], [Pwd]) VALUES (4, 3, N'1000:wPRhyY2Y+o6i56RYN9+HStYsbaV2ZeaF:AoKmF7pUuaRErJJ/IXc5Ge84A4MZK7ns')
INSERT [dbo].[UserAccess] ([UserType], [TypeID], [Pwd]) VALUES (4, 4, N'1000:jyxLEU7h15jNiRLMx/XeAU6GD6kmGjWw:GRfi2EYVkJBXFzM/Pcny5GfMoy4JhgkA')
SET IDENTITY_INSERT [dbo].[UserAction] ON 

INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (1, 2, 1, 1, 1, CAST(N'2015-04-03 20:12:38.320' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (2, 2, 1, 1, 1, CAST(N'2015-04-03 20:14:32.923' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (3, 2, 1, 1, 2, CAST(N'2015-04-03 22:00:33.817' AS DateTime), N'Location Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (4, 2, 2, 2, 1, CAST(N'2015-04-03 22:21:13.933' AS DateTime), N'Vehicle Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (5, 2, 2, 3, 1, CAST(N'2015-04-03 22:58:58.533' AS DateTime), N'Vehicle Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (6, 2, 2, 2, 2, CAST(N'2015-04-06 00:51:43.907' AS DateTime), N'Location Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (7, 2, 2, 3, 2, CAST(N'2015-04-06 03:18:11.233' AS DateTime), N'Location Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (8, 2, 2, 1, 3, CAST(N'2015-04-06 04:00:18.037' AS DateTime), N'Available Vehicle Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (9, 2, 2, 1, 3, CAST(N'2015-04-06 20:55:01.300' AS DateTime), N'Available Vehicle Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (10, 2, 1, 2, 3, CAST(N'2015-04-07 03:48:56.540' AS DateTime), N'Available Vehicle Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (11, 2, 2, 2, 2, CAST(N'2015-04-10 21:13:42.960' AS DateTime), N'Location Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (12, 2, 2, 4, 2, CAST(N'2015-04-10 21:22:26.710' AS DateTime), N'Location Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (13, 2, 2, 4, 2, CAST(N'2015-04-10 21:26:54.063' AS DateTime), N'Location Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (14, 2, 2, 4, 2, CAST(N'2015-04-10 21:38:24.707' AS DateTime), N'Location Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (15, 2, 2, 4, 2, CAST(N'2015-04-10 22:22:53.507' AS DateTime), N'Location Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (16, 2, 2, 4, 2, CAST(N'2015-04-10 22:37:30.917' AS DateTime), N'Location Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (19, 2, 2, 1, 3, CAST(N'2015-04-13 01:29:22.263' AS DateTime), N'Available Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (20, 2, 2, 2, 1, CAST(N'2015-04-13 02:52:32.087' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (21, 2, 2, 2, 1, CAST(N'2015-04-13 02:54:46.560' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (22, 2, 1, 1, 2, CAST(N'2015-04-15 00:23:23.380' AS DateTime), N'Location Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (23, 2, 1, 1, 1, CAST(N'2015-04-18 23:48:28.543' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (24, 2, 2, 3, 1, CAST(N'2015-04-19 23:20:25.807' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (25, 2, 2, 3, 1, CAST(N'2015-04-19 23:25:03.010' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (26, 2, 2, 3, 1, CAST(N'2015-04-20 00:20:27.767' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (27, 2, 2, 3, 1, CAST(N'2015-04-20 00:21:00.780' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (28, 2, 2, 3, 1, CAST(N'2015-04-20 00:32:01.987' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (29, 2, 2, 3, 1, CAST(N'2015-04-20 00:32:34.970' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (30, 2, 6, 5, 2, CAST(N'2015-04-23 11:17:07.147' AS DateTime), N'Location Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (31, 2, 6, 4, 1, CAST(N'2015-04-23 11:23:57.490' AS DateTime), N'Vehicle Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (32, 2, 6, 4, 1, CAST(N'2015-04-23 11:24:35.080' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (33, 2, 6, 3, 3, CAST(N'2015-04-23 11:27:07.010' AS DateTime), N'Available Vehicle Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (34, 2, 6, 3, 3, CAST(N'2015-04-23 12:02:01.417' AS DateTime), N'Available Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (35, 2, 6, 4, 1, CAST(N'2015-04-23 12:15:21.053' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (36, 2, 6, 4, 1, CAST(N'2015-04-23 12:16:41.740' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (37, 2, 6, 4, 1, CAST(N'2015-04-23 12:17:09.813' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (38, 2, 6, 4, 1, CAST(N'2015-04-25 20:31:33.367' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (39, 2, 6, 5, 2, CAST(N'2015-04-25 20:59:58.790' AS DateTime), N'Location Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (40, 2, 6, 5, 1, CAST(N'2015-04-25 21:27:34.120' AS DateTime), N'Vehicle Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (41, 2, 6, 4, 3, CAST(N'2015-04-25 21:28:06.657' AS DateTime), N'Available Vehicle Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (42, 2, 6, 3, 3, CAST(N'2015-04-25 21:29:00.423' AS DateTime), N'Available Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (43, 2, 6, 4, 1, CAST(N'2015-04-25 21:30:36.350' AS DateTime), N'Vehicle Updated')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (44, 2, 2, 5, 3, CAST(N'2015-04-25 23:16:42.220' AS DateTime), N'Available Vehicle Inserted')
INSERT [dbo].[UserAction] ([UserActionID], [UserType], [UserID], [ItemChangedID], [ActionType], [ActionTime], [ActionDescription]) VALUES (45, 2, 2, 6, 3, CAST(N'2015-04-25 23:23:14.937' AS DateTime), N'Available Vehicle Inserted')
SET IDENTITY_INSERT [dbo].[UserAction] OFF
INSERT [dbo].[VehicleOrders] ([VehiclesAvailableID], [OrderID]) VALUES (2, 44)
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([VehicleID], [Manufacturer], [Model], [SIPPCode], [MPG], [ImageLoc], [Active]) VALUES (1, N'Ford', N'Focus', N'IGME', 67, N'~/Images/ford focus.jpg', 1)
INSERT [dbo].[Vehicles] ([VehicleID], [Manufacturer], [Model], [SIPPCode], [MPG], [ImageLoc], [Active]) VALUES (2, N'Vauxhall', N'Corsa', N'ECMD', 65, N'~/Images/Vauxhall Corsa.jpg', 1)
INSERT [dbo].[Vehicles] ([VehicleID], [Manufacturer], [Model], [SIPPCode], [MPG], [ImageLoc], [Active]) VALUES (3, N'BMW', N'i8', N'CBAA', 45, N'~/Images/BMW i8.jpg', 1)
INSERT [dbo].[Vehicles] ([VehicleID], [Manufacturer], [Model], [SIPPCode], [MPG], [ImageLoc], [Active]) VALUES (4, N'Audi', N'A5', N'CDMR', 27, N'~/Images/Audi A5.jpg', 1)
INSERT [dbo].[Vehicles] ([VehicleID], [Manufacturer], [Model], [SIPPCode], [MPG], [ImageLoc], [Active]) VALUES (5, N'Ford', N'Ka', N'MBAD', 55, N'~/Images/Ford Ka.jpg', 1)
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
SET IDENTITY_INSERT [dbo].[VehiclesAvailable] ON 

INSERT [dbo].[VehiclesAvailable] ([VehiclesAvailableID], [VehicleID], [LocationID], [TotalVehicles], [Currency], [BasePrice], [Active]) VALUES (1, 2, 3, 300, N'GBP', 34.67, 1)
INSERT [dbo].[VehiclesAvailable] ([VehiclesAvailableID], [VehicleID], [LocationID], [TotalVehicles], [Currency], [BasePrice], [Active]) VALUES (2, 1, 1, 200, N'GBP', 23.67, 1)
INSERT [dbo].[VehiclesAvailable] ([VehiclesAvailableID], [VehicleID], [LocationID], [TotalVehicles], [Currency], [BasePrice], [Active]) VALUES (3, 4, 5, 5, N'AUD', 56, 1)
INSERT [dbo].[VehiclesAvailable] ([VehiclesAvailableID], [VehicleID], [LocationID], [TotalVehicles], [Currency], [BasePrice], [Active]) VALUES (4, 5, 5, 56, N'GBP', 90, 1)
INSERT [dbo].[VehiclesAvailable] ([VehiclesAvailableID], [VehicleID], [LocationID], [TotalVehicles], [Currency], [BasePrice], [Active]) VALUES (5, 3, 2, 56, N'USD', 106, 1)
INSERT [dbo].[VehiclesAvailable] ([VehiclesAvailableID], [VehicleID], [LocationID], [TotalVehicles], [Currency], [BasePrice], [Active]) VALUES (6, 2, 4, 34, N'CAD', 67, 1)
SET IDENTITY_INSERT [dbo].[VehiclesAvailable] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [companies_username_uni]    Script Date: 26/04/2015 01:18:52 ******/
ALTER TABLE [dbo].[Companies] ADD  CONSTRAINT [companies_username_uni] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [customers_username_uni]    Script Date: 26/04/2015 01:18:52 ******/
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [customers_username_uni] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [vehiclesavailable_vehicleid_locationid_uni]    Script Date: 26/04/2015 01:18:52 ******/
ALTER TABLE [dbo].[VehiclesAvailable] ADD  CONSTRAINT [vehiclesavailable_vehicleid_locationid_uni] UNIQUE NONCLUSTERED 
(
	[VehicleID] ASC,
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [customers_companyid_fk] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Companies] ([CompanyID])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [customers_companyid_fk]
GO
ALTER TABLE [dbo].[HolidayOpeningTimes]  WITH CHECK ADD  CONSTRAINT [holidayopeningtimes_locationid_fk] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO
ALTER TABLE [dbo].[HolidayOpeningTimes] CHECK CONSTRAINT [holidayopeningtimes_locationid_fk]
GO
ALTER TABLE [dbo].[OpeningTimes]  WITH CHECK ADD  CONSTRAINT [openingtimes_locationid_fk] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO
ALTER TABLE [dbo].[OpeningTimes] CHECK CONSTRAINT [openingtimes_locationid_fk]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [orders_addressid_fk] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Addresses] ([AddressID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [orders_addressid_fk]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [orders_customerid_fk] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [orders_customerid_fk]
GO
ALTER TABLE [dbo].[VehicleOrders]  WITH CHECK ADD  CONSTRAINT [vehicleorders_orderID_fk] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[VehicleOrders] CHECK CONSTRAINT [vehicleorders_orderID_fk]
GO
ALTER TABLE [dbo].[VehicleOrders]  WITH CHECK ADD  CONSTRAINT [vehicleorders_vehiclesavailableid_fk] FOREIGN KEY([VehiclesAvailableID])
REFERENCES [dbo].[VehiclesAvailable] ([VehiclesAvailableID])
GO
ALTER TABLE [dbo].[VehicleOrders] CHECK CONSTRAINT [vehicleorders_vehiclesavailableid_fk]
GO
ALTER TABLE [dbo].[VehiclesAvailable]  WITH CHECK ADD  CONSTRAINT [vehiclesavailable_locationid_fk] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO
ALTER TABLE [dbo].[VehiclesAvailable] CHECK CONSTRAINT [vehiclesavailable_locationid_fk]
GO
ALTER TABLE [dbo].[VehiclesAvailable]  WITH CHECK ADD  CONSTRAINT [vehiclesavailable_vehicleid_fk] FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([VehicleID])
GO
ALTER TABLE [dbo].[VehiclesAvailable] CHECK CONSTRAINT [vehiclesavailable_vehicleid_fk]
GO
/****** Object:  StoredProcedure [dbo].[INSERT_Address]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_Address]

@AddressType INT,
@AddressLine1 VARCHAR(50) = NULL,
@AddressLine2 VARCHAR(50) = NULL,
@City VARCHAR(50),
@ZipOrPostcode VARCHAR(50),
@CountyStateProvince VARCHAR(50) = NULL,
@Country VARCHAR(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO Addresses
	VALUES (@AddressType, @AddressLine1, @AddressLine2,
	@City, @ZipOrPostcode, @CountyStateProvince, @Country)

END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_AvailableVehicle]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_AvailableVehicle]

@VehicleID BIGINT,
@LocationID BIGINT,
@TotalVehicles INT,
@Currency VARCHAR(20),
@BasePrice FLOAT,
@Active BIT,
@UserID BIGINT,
@UserType BIGINT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @LastInsertedAvailableVehicle BIGINT

    INSERT INTO VehiclesAvailable
	VALUES (@VehicleID, @LocationID, @TotalVehicles, @Currency, @BasePrice, @Active)

	SET @LastInsertedAvailableVehicle =
	(SELECT TOP 1 VehiclesAvailableID
	FROM VehiclesAvailable
	ORDER BY VehiclesAvailableID DESC)

	INSERT INTO UserAction
	VALUES (@UserType, @UserID, @LastInsertedAvailableVehicle, 3, GETDATE(), 'Available Vehicle Inserted')
END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_Company]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_Company]

@UserName VARCHAR(50),
@CompanyName VARCHAR(50),
@CompanyDescription VARCHAR(300) = NULL,
@LicensingDetails VARCHAR(1000),
@PhoneNo VARCHAR(20),
@EmailAddress VARCHAR(150),
@UserType BIGINT,
@Password VARCHAR(max)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @LastInsertedCompany BIGINT

    INSERT INTO Companies
	VALUES (@UserName, @CompanyName, @CompanyDescription, @LicensingDetails, @PhoneNo, @EmailAddress)

	SET @LastInsertedCompany =
	(SELECT TOP 1 CompanyID
	FROM Companies
	ORDER BY CompanyID DESC)

	INSERT INTO UserAccess
	VALUES (@UserType, @LastInsertedCompany, @Password)

END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_CompanyAddress]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_CompanyAddress]

@CompanyID BIGINT,
@AddressLine1 VARCHAR(50) = NULL,
@AddressLine2 VARCHAR(50) = NULL,
@AddressLine3 VARCHAR(50) = NULL,
@AddressLine4 VARCHAR(50) = NULL,
@City VARCHAR(50),
@ZipOrPostcode VARCHAR(50),
@CountyStateProvince VARCHAR(50) = NULL,
@Country VARCHAR(50),
@OtherAddressDetails VARCHAR(200) = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO CompanyAddresses
	VALUES (@CompanyID, @AddressLine1, @AddressLine2, @AddressLine3, @AddressLine4,
	@City, @ZipOrPostcode, @CountyStateProvince, @Country, @OtherAddressDetails)

END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_Customer]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_Customer]

@CompanyID BIGINT = NULL,
@UserName VARCHAR(50),
@Surname VARCHAR(50),
@Forename VARCHAR(50),
@MobileNo VARCHAR(20) = NULL,
@PhoneNo VARCHAR(20),
@Title VARCHAR(10),
@LicenseNo VARCHAR(50),
@IssueDate DATETIME,
@ExpirationDate DATETIME,
@DateOfBirth DATETIME,
@EmailAddress VARCHAR(150),
@UserType BIGINT,
@Password VARCHAR(max)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @LastInsertedCustomer BIGINT

    INSERT INTO Customers
	VALUES (@CompanyID, @UserName, @Surname, @Forename, @Title, @LicenseNo, @IssueDate,
	@ExpirationDate, @DateOfBirth, @PhoneNo, @MobileNo, @EmailAddress)

	SET @LastInsertedCustomer =
	(SELECT TOP 1 CustomerID
	FROM Customers
	ORDER BY CustomerID DESC)

	INSERT INTO UserAccess
	VALUES (@UserType, @LastInsertedCustomer, @Password)

END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_DefaultOpeningTime]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_DefaultOpeningTime]
	
	@LocationID BIGINT,
	@DayOfTheWeek INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO OpeningTimes
	VALUES (@LocationID, @DayOfTheWeek, '0:00', '0:00', 1)
END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_HolidayOpeningTime]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_HolidayOpeningTime]
	
	@LocationID BIGINT,
	@HolidayStartDate DATE,
	@AltOpenTime DATETIME = NULL,
	@AltCloseTime DATETIME = NULL,
	@Closed BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO HolidayOpeningTimes
	VALUES (@LocationID, @HolidayStartDate, @AltOpenTime, @AltCloseTime, @Closed)
END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_Location]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_Location]
	-- Add the parameters for the stored procedure here
	@LocationName VARCHAR(50),
	@OwnerName VARCHAR(50),
	@Capacity INT = NULL,
	@AddressLine1 VARCHAR(50) = NULL,
	@AddressLine2 VARCHAR(50) = NULL,
	@City VARCHAR(50),
	@ZipOrPostcode VARCHAR(50),
	@CountyStateProvince VARCHAR(50) = NULL,
	@Country VARCHAR(50),
	@PhoneNo VARCHAR(20),
	@EmailAddress VARCHAR(50),
	@Longitude FLOAT,
	@Latitude FLOAT,
	@Active BIT,
	@UserID BIGINT,
	@UserType BIGINT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @LastInsertedLocation BIGINT

    INSERT INTO Locations
	VALUES (@LocationName, @OwnerName, @Capacity, @AddressLine1, @AddressLine2,
	@City, @ZipOrPostcode, @CountyStateProvince, @Country, @PhoneNo, @EmailAddress,
	@Longitude, @Latitude, @Active)

	SET @LastInsertedLocation =
	(SELECT TOP 1 LocationID
	FROM Locations
	ORDER BY LocationID DESC)

	INSERT INTO UserAction
	VALUES (@UserType, @UserID, @LastInsertedLocation, 2, GETDATE(), 'Location Inserted')
END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_Order]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_Order]

@CustomerID BIGINT,
@AddressID BIGINT = NULL,
@HireStart DATETIME,
@HireEnd DATETIME,
@VehicleAvailableID BIGINT,
@PayPalPayerID VARCHAR(500)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @LastInsertedOrder BIGINT

    INSERT INTO Orders
	VALUES (@CustomerID, @AddressID, @HireStart, @HireEnd, GETDATE(), 'Pending', @PayPalPayerID)

	SET @LastInsertedOrder =
	(SELECT TOP 1 OrderID
	FROM Orders
	ORDER BY OrderID DESC)

	INSERT INTO VehicleOrders
	VALUES (@VehicleAvailableID, @LastInsertedOrder)

END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_PasswordResetRequest]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_PasswordResetRequest]

@AccountType BIGINT,
@AccountID BIGINT,
@UserName VARCHAR(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO PasswordResetRequest
	VALUES (@AccountType, @AccountID, @UserName, GETDATE())

END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_UserAccess]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_UserAccess]

@UserType BIGINT,
@TypeID BIGINT,
@Password VARCHAR(max)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO UserAccess
	VALUES (@UserType, @TypeID, @Password)

END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_Vehicle]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_Vehicle]

@Manufacturer VARCHAR(30),
@Model VARCHAR(30),
@SIPPCode VARCHAR(4),
@MPG FLOAT,
@ImageLoc VARCHAR(100) = NULL,
@Active BIT,
@UserID BIGINT,
@UserType BIGINT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @LastInsertedVehicle BIGINT

    INSERT INTO Vehicles
	VALUES (@Manufacturer, @Model, @SIPPCode, @MPG, @ImageLoc, @Active)

	SET @LastInsertedVehicle =
	(SELECT TOP 1 VehicleID
	FROM Vehicles
	ORDER BY VehicleID DESC)

	INSERT INTO UserAction
	VALUES (@UserType, @UserID, @LastInsertedVehicle, 1, GETDATE(), 'Vehicle Inserted')
END

GO
/****** Object:  StoredProcedure [dbo].[INSERT_VehicleOrder]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_VehicleOrder]

@VehicleAvailableID BIGINT,
@OrderID BIGINT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO VehicleOrders
	VALUES (@VehicleAvailableID, @OrderID)
END

GO
/****** Object:  StoredProcedure [dbo].[SELECT_AddressesByCustomer]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_AddressesByCustomer]
	
	@CustomerID BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT DISTINCT Addresses.*
	FROM Orders
	JOIN Addresses ON Addresses.AddressID = Orders.AddressID
	WHERE CustomerID = @CustomerID

END

GO
/****** Object:  StoredProcedure [dbo].[SELECT_AvailableVehicles]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_AvailableVehicles]

	@LocationID BIGINT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT DISTINCT Vehicles.[VehicleID]
      ,[Manufacturer]
      ,[Model]
      ,[SIPPCode]
      ,[MPG]
      ,[ImageLoc]
      , VehiclesAvailable.*, UserID
	FROM Vehicles
	JOIN VehiclesAvailable ON Vehicles.VehicleID = VehiclesAvailable.VehicleID
	JOIN UserAction ON VehiclesAvailable.VehiclesAvailableID = UserAction.ItemChangedID
	AND UserAction.ActionType = 3
	WHERE LocationID = @LocationID

END


GO
/****** Object:  StoredProcedure [dbo].[SELECT_CodeByCountry]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_CodeByCountry]
	-- Add the parameters for the stored procedure here
	@Country VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM CountryMatrix
	WHERE Country = @Country

END



GO
/****** Object:  StoredProcedure [dbo].[SELECT_Companies]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_Companies]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT *
	FROM Companies
	JOIN CompanyAddresses ON CompanyAddresses.CompanyID = Companies.CompanyID

END

GO
/****** Object:  StoredProcedure [dbo].[SELECT_CompanyAccessInfo]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_CompanyAccessInfo]
	-- Add the parameters for the stored procedure here
	@UserName VARCHAR(50),
	@EmailAddress VARCHAR(50) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @EmailAddress IS NULL
	BEGIN

    SELECT *
	FROM UserAccess
	JOIN Companies ON Companies.CompanyID = UserAccess.typeid
	WHERE UserName = @UserName

	END
	ELSE
	BEGIN

	SELECT *
	FROM UserAccess
	JOIN Companies ON Companies.CompanyID = UserAccess.typeid
	WHERE UserName = @UserName
	AND EmailAddress = @EmailAddress
	END

END



GO
/****** Object:  StoredProcedure [dbo].[SELECT_CompanyByLogin]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_CompanyByLogin]
	-- Add the parameters for the stored procedure here
	@Pwd VARCHAR(150),
	@CompanyName VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT *
	FROM UserAccess
	JOIN Companies ON Companies.CompanyID = UserAccess.typeid
	WHERE Pwd = @Pwd
	AND CompanyName = @CompanyName
END


GO
/****** Object:  StoredProcedure [dbo].[SELECT_CompanyEmailByLocation]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_CompanyEmailByLocation]

	@LocationID BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT MAX(ActionTime) ActionTime, Companies.EmailAddress
	FROM Companies
	JOIN UserAction ON UserID = Companies.CompanyID
	AND UserType = 2
	WHERE ItemChangedID = @LocationID
	AND ActionType = 2
	GROUP BY Companies.EmailAddress

END

GO
/****** Object:  StoredProcedure [dbo].[SELECT_CountryByCode]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_CountryByCode]
	-- Add the parameters for the stored procedure here
	@CountryCode VARCHAR(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM CountryMatrix
	WHERE CountryCode = @CountryCode

END



GO
/****** Object:  StoredProcedure [dbo].[SELECT_HolidayOpeningTimesByLocation]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_HolidayOpeningTimesByLocation]
	-- Add the parameters for the stored procedure here
	@LocationID BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT *
	FROM HolidayOpeningTimes
	WHERE LocationID = @LocationID
END

GO
/****** Object:  StoredProcedure [dbo].[SELECT_LastInsertedAddress]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_LastInsertedAddress]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT TOP 1 AddressID
	FROM Addresses
	ORDER BY AddressID DESC

END


GO
/****** Object:  StoredProcedure [dbo].[SELECT_LastInsertedCompany]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_LastInsertedCompany]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT TOP 1 CompanyID
	FROM Companies
	ORDER BY CompanyID DESC

END


GO
/****** Object:  StoredProcedure [dbo].[SELECT_LastInsertedLocation]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_LastInsertedLocation]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT TOP 1 LocationID
	FROM Locations
	ORDER BY LocationID DESC

END

GO
/****** Object:  StoredProcedure [dbo].[SELECT_LastInsertedOrder]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_LastInsertedOrder]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT TOP 1 *
	FROM Orders
	ORDER BY OrderID DESC

END


GO
/****** Object:  StoredProcedure [dbo].[SELECT_Locations]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_Locations]

	@UserID BIGINT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @UserID IS NULL
	BEGIN
    SELECT DISTINCT Locations.*, UserAction.UserID
	FROM Locations
	JOIN UserAction on Locations.LocationID = UserAction.ItemChangedID
	AND UserAction.ActionType = 2
	END
	ELSE
	BEGIN
	SELECT DISTINCT Locations.*, UserAction.UserID
	FROM Locations
	JOIN UserAction on Locations.LocationID = UserAction.ItemChangedID
	AND UserAction.ActionType = 2
	WHERE UserID = @UserID
	END

END

GO
/****** Object:  StoredProcedure [dbo].[SELECT_Models]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_Models]
	
	@Manufacturer VARCHAR(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT DISTINCT Model 
	FROM Vehicles
	WHERE Manufacturer = @Manufacturer

END

GO
/****** Object:  StoredProcedure [dbo].[SELECT_OpeningTimesByLocation]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_OpeningTimesByLocation]
	-- Add the parameters for the stored procedure here
	@LocationID BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT *
	FROM OpeningTimes
	WHERE LocationID = @LocationID
END

GO
/****** Object:  StoredProcedure [dbo].[SELECT_OrderInfo]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_OrderInfo]
	-- Add the parameters for the stored procedure here
	@CustomerID BIGINT = NULL,
	@CompanyID BIGINT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @CustomerID IS NULL
	BEGIN
    SELECT MAX(ActionTime) ActionTime
	  ,	Orders.[OrderID]
      , Orders.[CustomerID]
      , Orders.[AddressID]
      , Orders.[HireStart]
      , Orders.[HireEnd]
      , Orders.[BookingCreated]
      , Orders.[OrderStatus]
      , Orders.[PayPalPayerID]
	  , VehicleOrders.[VehiclesAvailableID]
      , VehicleOrders.[OrderID]
	  , VehiclesAvailable.[VehiclesAvailableID]
      , VehiclesAvailable.[VehicleID]
      , VehiclesAvailable.[LocationID]
      , VehiclesAvailable.[TotalVehicles]
      , VehiclesAvailable.[Currency]
      , VehiclesAvailable.[BasePrice]
      , VehiclesAvailable.[Active]
	  , Vehicles.[VehicleID]
      , Vehicles.[Manufacturer]
      , Vehicles.[Model]
      , Vehicles.[SIPPCode]
      , Vehicles.[MPG]
      , Vehicles.[ImageLoc]
      , Vehicles.[Active]
	  , Locations.[LocationID]
      , Locations.[LocationName]
      , Locations.[OwnerName]
      , Locations.[Capacity]
      , Locations.[AddressLine1] LocationAddressLine1
      , Locations.[AddressLine2] LocationAddressLine2
      , Locations.[City] LocationCity
      , Locations.[ZipOrPostcode] LocationZipOrPostcode
      , Locations.[CountyStateProvince] LocationCountyStateProvince
      , Locations.[Country] LocationCountry
      , Locations.[PhoneNo] LocationPhoneNo
      , Locations.[EmailAddress] LocationEmailAddress
      , Locations.[Longitude]
      , Locations.[Latitude]
      , Locations.[Active]
	  , Addresses.[AddressID]
      , Addresses.[AddressType]
      , Addresses.[Line1]
      , Addresses.[Line2]
      , Addresses.[City]
      , Addresses.[ZipOrPostcode]
      , Addresses.[CountyStateProvince]
      , Addresses.[Country]
	  , Customers.[CustomerID]
      , Customers.[CompanyID] PartOfCompanyID
      , Customers.[UserName] CustomerUserName
      , Customers.[Surname] 
      , Customers.[Forename]
      , Customers.[Title]
      , Customers.[LicenseNo]
      , Customers.[IssueDate]
      , Customers.[ExpirationDate]
      , Customers.[DateOfBirth]
      , Customers.[PhoneNo] CustomerPhoneNo
      , Customers.[MobileNo]
      , Customers.[EmailAddress] CustomerEmailAddress
	  , Companies.[CompanyID] LocationCompanyID
      , Companies.[UserName]
      , Companies.[CompanyName]
      , Companies.[CompanyDescription]
      , Companies.[LicensingDetails]
      , Companies.[PhoneNo]
      , Companies.[EmailAddress]
	  --, UserAction.[UserActionID]
      , UserAction.[UserType]
      , UserAction.[UserID]
      , UserAction.[ItemChangedID]
      , UserAction.[ActionType]
      --, UserAction.[ActionTime]
      --, UserAction.[ActionDescription]
	FROM Orders
	JOIN VehicleOrders ON VehicleOrders.OrderID = Orders.OrderID
	JOIN VehiclesAvailable ON VehiclesAvailable.VehiclesAvailableID = VehicleOrders.VehiclesAvailableID
	JOIN Vehicles ON VehiclesAvailable.VehicleID = Vehicles.VehicleID
	JOIN Locations ON VehiclesAvailable.LocationID = Locations.LocationID
	LEFT JOIN Addresses ON Orders.AddressID = Addresses.AddressID
	JOIN Customers ON Orders.CustomerID = Customers.CustomerID
	JOIN UserAction ON Locations.LocationID = ItemChangedID
		AND UserAction.ActionType = 2
	JOIN Companies ON UserAction.UserID = Companies.CompanyID
	WHERE Companies.CompanyID = @CompanyID
	GROUP BY Orders.[OrderID]
      , Orders.[CustomerID]
      , Orders.[AddressID]
      , Orders.[HireStart]
      , Orders.[HireEnd]
      , Orders.[BookingCreated]
      , Orders.[OrderStatus]
      , Orders.[PayPalPayerID]
	  , VehicleOrders.[VehiclesAvailableID]
      , VehicleOrders.[OrderID]
	  , VehiclesAvailable.[VehiclesAvailableID]
      , VehiclesAvailable.[VehicleID]
      , VehiclesAvailable.[LocationID]
      , VehiclesAvailable.[TotalVehicles]
      , VehiclesAvailable.[Currency]
      , VehiclesAvailable.[BasePrice]
      , VehiclesAvailable.[Active]
	  , Vehicles.[VehicleID]
      , Vehicles.[Manufacturer]
      , Vehicles.[Model]
      , Vehicles.[SIPPCode]
      , Vehicles.[MPG]
      , Vehicles.[ImageLoc]
      , Vehicles.[Active]
	  , Locations.[LocationID]
      , Locations.[LocationName]
      , Locations.[OwnerName]
      , Locations.[Capacity]
      , Locations.[AddressLine1]
      , Locations.[AddressLine2]
      , Locations.[City]
      , Locations.[ZipOrPostcode] 
      , Locations.[CountyStateProvince]
      , Locations.[Country]
      , Locations.[PhoneNo]
      , Locations.[EmailAddress]
      , Locations.[Longitude]
      , Locations.[Latitude]
      , Locations.[Active]
	  , Addresses.[AddressID]
      , Addresses.[AddressType]
      , Addresses.[Line1]
      , Addresses.[Line2]
      , Addresses.[City]
      , Addresses.[ZipOrPostcode]
      , Addresses.[CountyStateProvince]
      , Addresses.[Country]
	  , Customers.[CustomerID]
      , Customers.[CompanyID]
      , Customers.[UserName]
      , Customers.[Surname] 
      , Customers.[Forename]
      , Customers.[Title]
      , Customers.[LicenseNo]
      , Customers.[IssueDate]
      , Customers.[ExpirationDate]
      , Customers.[DateOfBirth]
      , Customers.[PhoneNo]
      , Customers.[MobileNo]
      , Customers.[EmailAddress]
	  , Companies.[CompanyID]
      , Companies.[UserName]
      , Companies.[CompanyName]
      , Companies.[CompanyDescription]
      , Companies.[LicensingDetails]
      , Companies.[PhoneNo]
      , Companies.[EmailAddress]
	  --, UserAction.[UserActionID]
      , UserAction.[UserType]
      , UserAction.[UserID]
      , UserAction.[ItemChangedID]
      , UserAction.[ActionType]
      --, UserAction.[ActionTime]
      --, UserAction.[ActionDescription]
	ORDER BY Orders.HireStart DESC
	END
	ELSE
	BEGIN

	SELECT MAX(ActionTime) ActionTime
	  ,	Orders.[OrderID]
      , Orders.[CustomerID]
      , Orders.[AddressID]
      , Orders.[HireStart]
      , Orders.[HireEnd]
      , Orders.[BookingCreated]
      , Orders.[OrderStatus]
      , Orders.[PayPalPayerID]
	  , VehicleOrders.[VehiclesAvailableID]
      , VehicleOrders.[OrderID]
	  , VehiclesAvailable.[VehiclesAvailableID]
      , VehiclesAvailable.[VehicleID]
      , VehiclesAvailable.[LocationID]
      , VehiclesAvailable.[TotalVehicles]
      , VehiclesAvailable.[Currency]
      , VehiclesAvailable.[BasePrice]
      , VehiclesAvailable.[Active]
	  , Vehicles.[VehicleID]
      , Vehicles.[Manufacturer]
      , Vehicles.[Model]
      , Vehicles.[SIPPCode]
      , Vehicles.[MPG]
      , Vehicles.[ImageLoc]
      , Vehicles.[Active]
	  , Locations.[LocationID]
      , Locations.[LocationName]
      , Locations.[OwnerName]
      , Locations.[Capacity]
      , Locations.[AddressLine1] LocationAddressLine1
      , Locations.[AddressLine2] LocationAddressLine2
      , Locations.[City] LocationCity
      , Locations.[ZipOrPostcode] LocationZipOrPostcode
      , Locations.[CountyStateProvince] LocationCountyStateProvince
      , Locations.[Country] LocationCountry
      , Locations.[PhoneNo] LocationPhoneNo
      , Locations.[EmailAddress] LocationEmailAddress
      , Locations.[Longitude]
      , Locations.[Latitude]
      , Locations.[Active]
	  , Addresses.[AddressID]
      , Addresses.[AddressType]
      , Addresses.[Line1]
      , Addresses.[Line2]
      , Addresses.[City]
      , Addresses.[ZipOrPostcode]
      , Addresses.[CountyStateProvince]
      , Addresses.[Country]
	  , Customers.[CustomerID]
      , Customers.[CompanyID] PartOfCompanyID
      , Customers.[UserName] CustomerUserName
      , Customers.[Surname] 
      , Customers.[Forename]
      , Customers.[Title]
      , Customers.[LicenseNo]
      , Customers.[IssueDate]
      , Customers.[ExpirationDate]
      , Customers.[DateOfBirth]
      , Customers.[PhoneNo] CustomerPhoneNo
      , Customers.[MobileNo]
      , Customers.[EmailAddress] CustomerEmailAddress
	  , Companies.[CompanyID] LocationCompanyID
      , Companies.[UserName]
      , Companies.[CompanyName]
      , Companies.[CompanyDescription]
      , Companies.[LicensingDetails]
      , Companies.[PhoneNo]
      , Companies.[EmailAddress]
	  --, UserAction.[UserActionID]
      , UserAction.[UserType]
      , UserAction.[UserID]
      , UserAction.[ItemChangedID]
      , UserAction.[ActionType]
      --, UserAction.[ActionTime]
      --, UserAction.[ActionDescription]
	FROM Orders
	JOIN VehicleOrders ON VehicleOrders.OrderID = Orders.OrderID
	JOIN VehiclesAvailable ON VehiclesAvailable.VehiclesAvailableID = VehicleOrders.VehiclesAvailableID
	JOIN Vehicles ON VehiclesAvailable.VehicleID = Vehicles.VehicleID
	JOIN Locations ON VehiclesAvailable.LocationID = Locations.LocationID
	LEFT JOIN Addresses ON Orders.AddressID = Addresses.AddressID
	JOIN Customers ON Orders.CustomerID = Customers.CustomerID
	JOIN UserAction ON Locations.LocationID = ItemChangedID
		AND UserAction.ActionType = 2
	JOIN Companies ON UserAction.UserID = Companies.CompanyID
	WHERE Customers.CustomerID = @CustomerID
	GROUP BY Orders.[OrderID]
      , Orders.[CustomerID]
      , Orders.[AddressID]
      , Orders.[HireStart]
      , Orders.[HireEnd]
      , Orders.[BookingCreated]
      , Orders.[OrderStatus]
      , Orders.[PayPalPayerID]
	  , VehicleOrders.[VehiclesAvailableID]
      , VehicleOrders.[OrderID]
	  , VehiclesAvailable.[VehiclesAvailableID]
      , VehiclesAvailable.[VehicleID]
      , VehiclesAvailable.[LocationID]
      , VehiclesAvailable.[TotalVehicles]
      , VehiclesAvailable.[Currency]
      , VehiclesAvailable.[BasePrice]
      , VehiclesAvailable.[Active]
	  , Vehicles.[VehicleID]
      , Vehicles.[Manufacturer]
      , Vehicles.[Model]
      , Vehicles.[SIPPCode]
      , Vehicles.[MPG]
      , Vehicles.[ImageLoc]
      , Vehicles.[Active]
	  , Locations.[LocationID]
      , Locations.[LocationName]
      , Locations.[OwnerName]
      , Locations.[Capacity]
      , Locations.[AddressLine1]
      , Locations.[AddressLine2]
      , Locations.[City]
      , Locations.[ZipOrPostcode] 
      , Locations.[CountyStateProvince]
      , Locations.[Country]
      , Locations.[PhoneNo]
      , Locations.[EmailAddress]
      , Locations.[Longitude]
      , Locations.[Latitude]
      , Locations.[Active]
	  , Addresses.[AddressID]
      , Addresses.[AddressType]
      , Addresses.[Line1]
      , Addresses.[Line2]
      , Addresses.[City]
      , Addresses.[ZipOrPostcode]
      , Addresses.[CountyStateProvince]
      , Addresses.[Country]
	  , Customers.[CustomerID]
      , Customers.[CompanyID]
      , Customers.[UserName]
      , Customers.[Surname] 
      , Customers.[Forename]
      , Customers.[Title]
      , Customers.[LicenseNo]
      , Customers.[IssueDate]
      , Customers.[ExpirationDate]
      , Customers.[DateOfBirth]
      , Customers.[PhoneNo]
      , Customers.[MobileNo]
      , Customers.[EmailAddress]
	  , Companies.[CompanyID]
      , Companies.[UserName]
      , Companies.[CompanyName]
      , Companies.[CompanyDescription]
      , Companies.[LicensingDetails]
      , Companies.[PhoneNo]
      , Companies.[EmailAddress]
	  --, UserAction.[UserActionID]
      , UserAction.[UserType]
      , UserAction.[UserID]
      , UserAction.[ItemChangedID]
      , UserAction.[ActionType]
      --, UserAction.[ActionTime]
      --, UserAction.[ActionDescription]
	ORDER BY Orders.HireStart DESC
	END
END

GO
/****** Object:  StoredProcedure [dbo].[SELECT_PasswordResetTime]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_PasswordResetTime]
	-- Add the parameters for the stored procedure here
	@AccountType BIGINT,
	@AccountID BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT TOP 1 Created
	FROM PasswordResetRequest
	WHERE Created between DATEADD(MINUTE, -15, GETDATE()) and GETDATE()
	AND AccountType = @AccountType
	AND AccountID = @AccountID
	ORDER BY RequestID DESC

END



GO
/****** Object:  StoredProcedure [dbo].[SELECT_SIPPCodeByTypeAndLetter]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_SIPPCodeByTypeAndLetter]
	-- Add the parameters for the stored procedure here
	@Type INT,
	@Letter CHAR(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT *
	FROM SIPPCodes
	WHERE Type = @Type
	AND Letter = @Letter
END

GO
/****** Object:  StoredProcedure [dbo].[SELECT_Vehicles]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SELECT_Vehicles]

	@UserID BIGINT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @UserID IS NULL
	BEGIN
    SELECT DISTINCT Vehicles.*, UserAction.UserID
	FROM Vehicles
	JOIN UserAction on Vehicles.VehicleID = UserAction.ItemChangedID
	AND UserAction.ActionType = 1
	END
	ELSE
	BEGIN
	SELECT DISTINCT Vehicles.*, UserAction.UserID
	FROM Vehicles
	JOIN UserAction on Vehicles.VehicleID = UserAction.ItemChangedID
	AND UserAction.ActionType = 1
	WHERE UserID = @UserID
	END

END

GO
/****** Object:  StoredProcedure [dbo].[UPDATE_AvailableVehicle]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPDATE_AvailableVehicle]

@VehicleID BIGINT,
@LocationID BIGINT,
@TotalVehicles INT,
@Currency VARCHAR(20),
@BasePrice FLOAT,
@Active BIT,
@UserID BIGINT,
@UserType BIGINT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE VehiclesAvailable
	SET VehicleID = @VehicleID,
	LocationID = @LocationID,
	TotalVehicles = @TotalVehicles,
	Currency = @Currency,
	BasePrice = @BasePrice,
	Active = @Active
    WHERE LocationID = @LocationID
	AND VehicleID = @VehicleID

	DECLARE @UpdatedAvailableVehicleID BIGINT

	SET @UpdatedAvailableVehicleID = 
	(SELECT VehiclesAvailableID
	FROM VehiclesAvailable
	WHERE LocationID = @LocationID
	AND VehicleID = @VehicleID)

	INSERT INTO UserAction
	VALUES (@UserType, @UserID, @UpdatedAvailableVehicleID, 3, GETDATE(), 'Available Vehicle Updated')
END

GO
/****** Object:  StoredProcedure [dbo].[UPDATE_Company]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPDATE_Company]

@CompanyID BIGINT,
@CompanyName VARCHAR(50),
@CompanyDescription VARCHAR(300) = NULL,
@LicensingDetails VARCHAR(1000),
@PhoneNo VARCHAR(20),
@EmailAddress VARCHAR(150)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE Companies
	SET CompanyName = @CompanyName,
	CompanyDescription = @CompanyDescription,
	LicensingDetails = @LicensingDetails,
	PhoneNo = @PhoneNo,
	EmailAddress = @EmailAddress
	WHERE CompanyID = @CompanyID

END

GO
/****** Object:  StoredProcedure [dbo].[UPDATE_Customer]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPDATE_Customer]

	@CustomerID BIGINT,
	@CompanyID BIGINT = NULL,
	@Surname VARCHAR(50),
	@Forename VARCHAR(50),
	@Title VARCHAR(10),
	@LicenseNo VARCHAR(50),
	@IssueDate DATETIME,
	@ExpirationDate DATETIME,
	@DateOfBirth DATETIME,
	@PhoneNo VARCHAR(20),
	@MobileNo VARCHAR(20) = NULL,
	@EmailAddress VARCHAR(150)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Customers
	SET CompanyID = @CompanyID,
	Surname = @Surname,
	Forename = @Forename,
	Title = @Title,
	LicenseNo = @LicenseNo,
	IssueDate = @IssueDate,
	ExpirationDate = @ExpirationDate,
	DateOfBirth = @DateOfBirth,
	PhoneNo = @PhoneNo,
	MobileNo = @MobileNo,
	EmailAddress = @EmailAddress
	WHERE CustomerID = @CustomerID

	--INSERT INTO UserAction
	--VALUES (@UserType, @UserID, @LocationID, 2, GETDATE(), 'Location Updated')
END


GO
/****** Object:  StoredProcedure [dbo].[UPDATE_Location]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPDATE_Location]

	@LocationID BIGINT,
	@LocationName VARCHAR(100),
	@OwnerName VARCHAR(50),
	@Capacity INT = NULL,
	@AddressLine1 VARCHAR(50) = NULL,
	@AddressLine2 VARCHAR(50) = NULL,
	@City VARCHAR(50),
	@ZipOrPostcode VARCHAR(50),
	@CountyStateProvince VARCHAR(50) = NULL,
	@Country VARCHAR(50),
	@PhoneNo VARCHAR(20),
	@EmailAddress VARCHAR(150),
	@Longitude FLOAT,
	@Latitude FLOAT,
	@Active BIT,
	@UserID BIGINT,
	@UserType BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Locations
	SET LocationName = @LocationName,
	OwnerName = @OwnerName,
	Capacity = @Capacity,
	AddressLine1 = @AddressLine1,
	AddressLine2 = @AddressLine2,
	City = @City,
	ZipOrPostcode = @ZipOrPostcode,
	CountyStateProvince = @CountyStateProvince,
	Country = @Country,
	PhoneNo = @PhoneNo,
	EmailAddress = @EmailAddress,
	Longitude = @Longitude,
	Latitude = @Latitude,
	Active = @Active
	WHERE LocationID = @LocationID

	INSERT INTO UserAction
	VALUES (@UserType, @UserID, @LocationID, 2, GETDATE(), 'Location Updated')
END


GO
/****** Object:  StoredProcedure [dbo].[UPDATE_OpeningTime]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPDATE_OpeningTime]
	-- Add the parameters for the stored procedure here
	@LocationID BIGINT,
	@DayOfTheWeek INT,
	@OpenTime DATETIME,
	@CloseTime DATETIME,
	@Closed BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE OpeningTimes
	SET OpenTime = @OpenTime,
	CloseTime = @CloseTime,
	Closed = @Closed
	WHERE LocationID = @LocationID
	AND DayOfTheWeek = @DayOfTheWeek
END

GO
/****** Object:  StoredProcedure [dbo].[UPDATE_OrderStatus]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPDATE_OrderStatus]
	-- Add the parameters for the stored procedure here
	@OrderID BIGINT,
	@OrderStatus VARCHAR(100)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Orders
	SET OrderStatus = @OrderStatus
	WHERE OrderID = @OrderID
END

GO
/****** Object:  StoredProcedure [dbo].[UPDATE_UserAccess]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPDATE_UserAccess]

@UserType BIGINT,
@TypeID BIGINT,
@Password VARCHAR(max)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE UserAccess
	SET Pwd = @Password
	WHERE UserType = @UserType
	AND TypeID = @TypeID
END

GO
/****** Object:  StoredProcedure [dbo].[UPDATE_Vehicle]    Script Date: 26/04/2015 01:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPDATE_Vehicle]

	@VehicleID BIGINT,
	@Manufacturer VARCHAR(30),
	@Model VARCHAR(30),
	@SIPPCode VARCHAR(4),
	@MPG FLOAT,
	@ImageLoc VARCHAR(100) = NULL,
	@Active BIT,
	@UserID BIGINT,
	@UserType BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Vehicles
	SET Manufacturer = @Manufacturer,
	Model = @Model,
	SIPPCode = @SIPPCode,
	MPG = @MPG,
	ImageLoc = @ImageLoc,
	Active = @Active
	WHERE VehicleID = @VehicleID

	INSERT INTO UserAction
	VALUES (@UserType, @UserID, @VehicleID, 1, GETDATE(), 'Vehicle Updated')
END


GO
USE [master]
GO
ALTER DATABASE [CarHire] SET  READ_WRITE 
GO
