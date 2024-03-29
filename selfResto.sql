USE [master]
GO
/****** Object:  Database [SelfResto]    Script Date: 24/10/2022 14:44:26 ******/
CREATE DATABASE [SelfResto]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SelfResto', FILENAME = N'C:\Users\Codes-1N5110\SelfResto.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SelfResto_log', FILENAME = N'C:\Users\Codes-1N5110\SelfResto_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SelfResto] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SelfResto].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SelfResto] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SelfResto] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SelfResto] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SelfResto] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SelfResto] SET ARITHABORT OFF 
GO
ALTER DATABASE [SelfResto] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SelfResto] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SelfResto] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SelfResto] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SelfResto] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SelfResto] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SelfResto] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SelfResto] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SelfResto] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SelfResto] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SelfResto] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SelfResto] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SelfResto] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SelfResto] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SelfResto] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SelfResto] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SelfResto] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SelfResto] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SelfResto] SET  MULTI_USER 
GO
ALTER DATABASE [SelfResto] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SelfResto] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SelfResto] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SelfResto] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SelfResto] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SelfResto] SET QUERY_STORE = OFF
GO
USE [SelfResto]
GO
/****** Object:  Table [dbo].[AccessTypes]    Script Date: 24/10/2022 14:44:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](50) NOT NULL,
	[ACTIVE] [bit] NOT NULL,
	[CREATION_DATE] [datetime2](7) NOT NULL,
	[CREATION_USER] [int] NOT NULL,
	[UPDATE_DATE] [datetime2](7) NULL,
	[UPDATE_USER] [int] NULL,
 CONSTRAINT [PK_TBL_ACCESS_TYPES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 24/10/2022 14:44:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](100) NOT NULL,
	[ACTIVE] [bit] NOT NULL,
	[IMAGEURL] [nvarchar](max) NULL,
	[CREATION_DATE] [datetime2](7) NOT NULL,
	[CREATION_USER] [int] NOT NULL,
	[UPDATE_DATE] [datetime2](7) NULL,
	[UPDATE_USER] [int] NULL,
 CONSTRAINT [PK_TBL_Categories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuItems]    Script Date: 24/10/2022 14:44:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VisualizationOrder] [int] NULL,
	[Category_Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Price] [float] NOT NULL,
	[Active] [bit] NOT NULL,
	[Creation_Date] [datetime2](7) NOT NULL,
	[Creation_User] [int] NOT NULL,
	[Update_Date] [datetime2](7) NULL,
	[Update_User] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 24/10/2022 14:44:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ORDER_ID] [int] NOT NULL,
	[MENU_ITEM_ID] [int] NOT NULL,
	[QUANTITY] [tinyint] NOT NULL,
	[RELATED_MENU_ITEM_ID] [int] NULL,
	[ORDER_DETAIL_STATUS_ID] [int] NOT NULL,
	[ACTIVE] [bit] NOT NULL,
	[CREATION_DATE] [datetime2](7) NOT NULL,
	[CREATION_USER] [int] NOT NULL,
	[UPDATE_DATE] [datetime2](7) NULL,
	[UPDATE_USER] [int] NULL,
 CONSTRAINT [PK_TBL_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetailStatus]    Script Date: 24/10/2022 14:44:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetailStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_StateTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 24/10/2022 14:44:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TABLE_ID] [int] NOT NULL,
	[CALL] [bit] NOT NULL,
	[PAYMENT_REQUEST] [bit] NULL,
	[ACTIVE] [bit] NOT NULL,
	[CREATION_DATE] [datetime2](7) NOT NULL,
	[CREATION_USER] [int] NOT NULL,
	[UPDATE_DATE] [datetime2](7) NULL,
	[UPDATE_USER] [int] NULL,
 CONSTRAINT [PK_TBL_Orders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 24/10/2022 14:44:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ORDER_ID] [int] NOT NULL,
	[TOTAL_AMOUNT] [float] NOT NULL,
	[CREATION_DATE] [datetime2](7) NOT NULL,
	[CREATION_USER] [int] NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tables]    Script Date: 24/10/2022 14:44:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tables](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NUMBER] [int] NOT NULL,
	[NAME] [varchar](50) NULL,
	[DESCRIPTION] [varchar](100) NULL,
	[WAITER_ID] [int] NULL,
	[WAITER_BACK_UP_ID] [int] NULL,
	[ACTIVE] [bit] NOT NULL,
	[CREATION_DATE] [datetime2](7) NOT NULL,
	[CREATION_USER] [int] NOT NULL,
	[UPDATE_DATE] [datetime2](7) NULL,
	[UPDATE_USER] [int] NULL,
 CONSTRAINT [PK_Tables] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 24/10/2022 14:44:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](80) NOT NULL,
	[SURNAME] [nvarchar](80) NULL,
	[EMAIL] [nvarchar](50) NOT NULL,
	[PASSWORD] [nvarchar](20) NOT NULL,
	[ACCESS_TYPE] [int] NOT NULL,
	[ACTIVE] [bit] NOT NULL,
	[CREATION_DATE] [datetime2](7) NOT NULL,
	[CREATION_USER] [int] NOT NULL,
	[UPDATE_DATE] [datetime2](7) NULL,
	[UPDATE_USER] [int] NULL,
 CONSTRAINT [PK_TBL_USERS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AccessTypes] ON 

INSERT [dbo].[AccessTypes] ([ID], [NAME], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (6, N'ADMINISTRADOR', 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[AccessTypes] ([ID], [NAME], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (10, N'CLIENTE', 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[AccessTypes] ([ID], [NAME], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (11, N'MOZO', 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[AccessTypes] ([ID], [NAME], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (13, N'COCINA', 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[AccessTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (1, N'Entradas', 1, NULL, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (2, N'Pastas', 1, NULL, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3, N'Salsas', 1, NULL, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (4, N'Platos Elaborados', 1, NULL, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (5, N'Minutas', 1, NULL, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (6, N'Guarniciones', 1, NULL, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (7, N'Postres', 1, NULL, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (8, N'Bebidas', 1, NULL, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (9, N'Bebidas Sin Alcohol', 1, N'/images/MenuItems/Bebidas/BebidasSinAlcohol.jpg', CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (10, N'Vinos Tintos', 1, N'/images/MenuItems/Bebidas/VinoTinto.jpg', CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (11, N'Vinos Blancos', 1, N'/images/MenuItems/Bebidas/VinoBlanco.jpg', CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Categories] ([ID], [NAME], [ACTIVE], [IMAGEURL], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (12, N'Cervezas', 1, N'/images/MenuItems/Bebidas/Cervezas.jpg', CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[MenuItems] ON 

INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (1, 1, 1, N'Rabas a la romana', NULL, N'/images/MenuItems/Entradas/Rabas.jpg', 1400, 1, CAST(N'2022-07-10T00:00:00.0000000' AS DateTime2), 1, CAST(N'2022-09-22T17:35:12.5345025' AS DateTime2), 1)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (2, 2, 1, N'Ensalada rusa ', NULL, N'/images/MenuItems/Entradas/Rusa.jpg', 700, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (3, 3, 1, N'Jamón crudo ', NULL, N'/images/MenuItems/Entradas/Crudo.jpg', 800, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (4, 4, 1, N'Bastoncitos de muzzarella ', NULL, N'/images/MenuItems/Entradas/Muzza.jpg', 1200, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (5, 5, 1, N'Picada ', NULL, N'/images/MenuItems/Entradas/Picada.jpg', 2100, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (6, 1, 2, N'Ñoquis de papa ', NULL, N'/images/MenuItems/Pastas/Ñoquis.jpg', 800, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (7, 2, 2, N'Ravioles de ricota ', NULL, N'/images/MenuItems/Pastas/Ravioles.jpg', 1000, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (8, 3, 2, N'Spaguetti ', NULL, N'/images/MenuItems/Pastas/Spaguetti.jpg', 900, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (9, 4, 2, N'Sorrentinos de jamón y muzzarella ', NULL, N'/images/MenuItems/Pastas/Sorrentinos.jpg', 1000, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (10, 5, 2, N'Canelones de pollo y espinaca ', NULL, N'/images/MenuItems/Pastas/Canelones.jpg', 1000, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (11, 6, 2, N'Lasagna ', NULL, N'/images/MenuItems/Pastas/Lasagna.jpg', 1000, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (12, 1, 3, N'Filetto ', NULL, N'/images/MenuItems/Pastas/Filetto.jpg', 200, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, CAST(N'2022-09-12T10:58:45.9300011' AS DateTime2), NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (13, 4, 3, N'Blanca ', NULL, N'/images/MenuItems/Pastas/SalsaBlanca.jpg', 200, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (14, 5, 3, N'Crema ', NULL, N'/images/MenuItems/Pastas/Crema.jpg', 200, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (15, 6, 3, N'Rosa (Filetto y Crema)', NULL, N'/images/MenuItems/Pastas/Rosa.jpg', 200, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (16, 7, 3, N'Mixta (Filetto y S.Blanca) ', NULL, N'/images/MenuItems/Pastas/Mixta.jpg', 200, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (17, 8, 3, N'Pesto ', NULL, N'/images/MenuItems/Pastas/Pesto.jpg', 300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (18, 2, 3, N'Bolognesa ', NULL, N'/images/MenuItems/Pastas/Bolognesa.jpg', 300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (19, 3, 3, N'Estofado ', NULL, N'/images/MenuItems/Pastas/Estofado.jpg', 400, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (20, 1, 4, N'Escalopes al marsala con papas noisette ', NULL, N'/images/MenuItems/PlatosElaborados/Escalopes.jpg', 1800, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (21, 2, 4, N'Lomo al champignon con milhojas de papa ', NULL, N'/images/MenuItems/PlatosElaborados/LomoChampignon.jpg', 2200, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (22, 3, 4, N'Pechuga de pollo rellena de jamón y queso con papas rústicas ', NULL, N'/images/MenuItems/PlatosElaborados/PechugaRellena.jpg', 1800, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (23, 4, 4, N'1/4 de pollo al champignon con papas noisette ', NULL, N'/images/MenuItems/PlatosElaborados/PolloChampignon.jpg', 1700, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (24, 5, 4, N'Abadejo al verdeo con verduras gratinadas ', NULL, N'/images/MenuItems/PlatosElaborados/Abadejo.jpg', 1900, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (25, 6, 4, N'Risotto de camarones ', NULL, N'/images/MenuItems/PlatosElaborados/Risotto.jpg', 1900, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (26, 1, 5, N'Milanesa de ternera ', NULL, N'/images/MenuItems/Minutas/MilanesaTernera.jpg', 1000, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (27, 4, 5, N'Milanesa napolitana', NULL, N'/images/MenuItems/Minutas/Napolitana.jpg', 1200, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (28, 3, 5, N'Suprema de pollo ', NULL, N'/images/MenuItems/Minutas/SupremaPollo.jpg', 1000, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (30, 2, 5, N'A caballo de ternera o pollo', NULL, N'/images/MenuItems/Minutas/Caballo.jpg', 1300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (32, 6, 5, N'Filet de merluza a la milanesa ', NULL, N'/images/MenuItems/Minutas/Filet.jpg', 1300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (33, 5, 5, N'1/4 pollo a la plancha ', NULL, N'/images/MenuItems/Minutas/PolloPlancha.jpg', 800, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (34, 7, 5, N'Revuelto Gramajo ', NULL, N'/images/MenuItems/Minutas/Gramajo.jpg', 800, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (35, 1, 6, N'Papas fritas ', NULL, N'/images/MenuItems/Guarniciones/PapasFritas.jpg', 450, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (36, 2, 6, N'Puré de papa', NULL, N'/images/MenuItems/Guarniciones/Pure.jpg', 450, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (37, 3, 6, N'Papas rústicas ', NULL, N'/images/MenuItems/Guarniciones/Rusticas.jpg', 400, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (38, 4, 6, N'Tortilla de papas ', NULL, N'/images/MenuItems/Guarniciones/TortillaPapa.jpg', 1000, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (39, 5, 6, N'Arroz Blanco', NULL, N'/images/MenuItems/Guarniciones/ArrozBlanco.jpg', 400, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (40, 6, 6, N'Espinaca a la crema ', NULL, N'/images/MenuItems/Guarniciones/EspinacaCrema.jpg', 500, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (41, 7, 6, N'Huevo frito ', NULL, N'/images/MenuItems/Guarniciones/HuevoFrito.jpg', 200, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (42, 8, 6, N'Ensalada Mixta', NULL, N'/images/MenuItems/Guarniciones/Ensaladas.jpg', 500, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (44, 1, 7, N'Flan casero ', NULL, N'/images/MenuItems/Postres/Flan.jpg', 500, 0, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, CAST(N'2022-09-12T10:58:50.0559786' AS DateTime2), NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (45, 2, 7, N'Budín de pan ', NULL, N'/images/MenuItems/Postres/Budin.jpg', 500, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (46, 3, 7, N'Vigilante de membrillo', NULL, N'/images/MenuItems/Postres/VigilanteMembrillo.jpg', 700, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (47, 5, 7, N'Ensalada de frutas ', NULL, N'/images/MenuItems/Postres/EnsaladaFrutas.jpg', 600, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (48, 7, 7, N'Panqueque con dulce de leche ', NULL, N'/images/MenuItems/Postres/PanquequeDulceLeche.jpg', 650, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (49, 9, 7, N'Mousse de chocolate ', NULL, N'/images/MenuItems/Postres/Mousse.jpg', 750, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (50, 8, 7, N'Tiramisú ', NULL, N'/images/MenuItems/Postres/Tiramisu.jpg', 800, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (51, 6, 7, N'Frutillas con crema ', NULL, N'/images/MenuItems/Postres/FrutillasConCrema.jpg', 750, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (52, 10, 7, N'3 bochas de helado ', NULL, N'/images/MenuItems/Postres/Helado.jpg', 750, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (53, 11, 7, N'Bombón Suizo ', NULL, N'/images/MenuItems/Postres/BombonSuizo.jpg', 700, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (54, 12, 7, N'Charlotte ', NULL, N'/images/MenuItems/Postres/Charlotte.jpg', 950, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (55, 13, 7, N'Extra crema / dulce de leche ', NULL, N'/images/MenuItems/Postres/Recargo.jpg', 200, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (58, 14, 7, N'Café - Té ', NULL, N'/images/MenuItems/Postres/CafeTe.jpg', 250, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (60, 2, 9, N'Agua sin gas', NULL, N'/images/MenuItems/Bebidas/AguaSinGas.png', 300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (62, 3, 9, N'Coca Cola', NULL, N'/images/MenuItems/Bebidas/CocaCola.png', 300, 0, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, CAST(N'2022-09-12T17:31:53.9195425' AS DateTime2), NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (63, 7, 9, N'Agua Saborizada Pomelo', NULL, N'/images/MenuItems/Bebidas/LevitePomelo.png', 300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (64, 9, 10, N'Malbec Portillo ', NULL, N'/images/MenuItems/Bebidas/VinoTinto.jpg', 1400, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (65, 10, 10, N'Malbec Trumpeter', NULL, N'/images/MenuItems/Bebidas/VinoTinto.jpg', 1400, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (66, 11, 10, N'Cabernet Sauvignon Puro ', NULL, N'/images/MenuItems/Bebidas/VinoTinto.jpg', 1500, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (67, 12, 10, N'Cabernet Sauvignon Trumpeter', NULL, N'/images/MenuItems/Bebidas/VinoTinto.jpg', 1500, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (68, 13, 11, N'Chardonnay Alamos ', NULL, N'/images/MenuItems/Bebidas/VinoBlanco.jpg', 1500, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (69, 14, 11, N'Sauvignon Blanc Trumpeter ', NULL, N'/images/MenuItems/Bebidas/VinoBlanco.jpg', 2000, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (70, 15, 11, N'Cosecha Tardía Norton  ', NULL, N'/images/MenuItems/Bebidas/VinoBlanco.jpg', 1500, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (71, 16, 12, N'Quilmes Porrón ', NULL, N'/images/MenuItems/Bebidas/QuilmesPorron.png', 450, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (72, 17, 12, N'Quilmes 1 litro', NULL, N'/images/MenuItems/Bebidas/QuilmesLitro.jpg', 750, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (73, 18, 12, N'Stella Artois Porrón', NULL, N'/images/MenuItems/Bebidas/StellaPorron.png', 550, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (74, 19, 12, N'Stella Artois 1 litro', NULL, N'/images/MenuItems/Bebidas/StellaLitro.png', 850, 0, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, CAST(N'2022-09-12T17:32:03.4992024' AS DateTime2), NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (75, 20, 12, N'Budweisser Lata', NULL, N'/images/MenuItems/Bebidas/BudweiserLata.png', 500, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (76, 21, 12, N'Budweisser 1 litro', NULL, N'/images/MenuItems/Bebidas/BudweiserLitro.jpg', 800, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (77, 22, 12, N'Corona Porrón ', NULL, N'/images/MenuItems/Bebidas/CoronaPorron.png', 600, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (85, 6, 1, N'Servicio de Mesa', NULL, N'/images/MenuItems/Entradas/ServicioMesa.jpg', 300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, CAST(N'2022-09-13T12:50:38.9132917' AS DateTime2), 1)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (79, 1, 9, N'Agua con gas', NULL, N'/images/MenuItems/Bebidas/AguaConGas.png', 300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (80, 4, 9, N'Fanta', NULL, N'/images/MenuItems/Bebidas/Fanta.png', 300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (81, 5, 9, N'Sprite', NULL, N'/images/MenuItems/Bebidas/Sprite.png', 300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (82, 6, 9, N'Agua Saborizada Naranja', NULL, N'/images/MenuItems/Bebidas/LeviteNaranja.png', 300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (83, 8, 9, N'Agua Saborizada Manzana', NULL, N'/images/MenuItems/Bebidas/LeviteManzana.png', 300, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[MenuItems] ([Id], [VisualizationOrder], [Category_Id], [Name], [Description], [ImageUrl], [Price], [Active], [Creation_Date], [Creation_User], [Update_Date], [Update_User]) VALUES (84, 4, 7, N'Vigilante de batata', NULL, N'/images/MenuItems/Postres/VigilanteBatata.jpg', 700, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MenuItems] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3052, 2029, 1, 1, NULL, 3, 1, CAST(N'2022-09-29T08:00:31.4283433' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3053, 2029, 4, 1, NULL, 3, 1, CAST(N'2022-09-29T08:00:35.3020645' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3054, 2029, 85, 1, NULL, 3, 1, CAST(N'2022-09-29T08:00:37.7384702' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3055, 2029, 7, 1, 18, 2, 1, CAST(N'2022-09-29T08:01:08.5458644' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3056, 2029, 30, 1, NULL, 2, 1, CAST(N'2022-09-29T08:01:14.2815261' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3057, 2029, 79, 1, NULL, 4, 1, CAST(N'2022-09-29T08:01:28.1133326' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3058, 2029, 64, 1, NULL, 4, 1, CAST(N'2022-09-29T08:01:36.2429501' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3059, 2030, 73, 1, NULL, 1, 1, CAST(N'2022-09-29T08:05:38.8843528' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3060, 2030, 75, 1, NULL, 1, 1, CAST(N'2022-09-29T08:05:38.8848767' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3061, 2030, 5, 1, NULL, 1, 1, CAST(N'2022-09-29T08:05:51.5316277' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3062, 2031, 21, 1, NULL, 4, 0, CAST(N'2022-09-29T08:07:51.6457666' AS DateTime2), 6, CAST(N'2022-09-29T08:11:11.3922022' AS DateTime2), 6)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3063, 2031, 25, 1, NULL, 4, 0, CAST(N'2022-09-29T08:07:54.0013844' AS DateTime2), 6, CAST(N'2022-09-29T08:11:11.3922047' AS DateTime2), 6)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3064, 2031, 68, 1, NULL, 4, 0, CAST(N'2022-09-29T08:08:00.5303945' AS DateTime2), 6, CAST(N'2022-09-29T08:11:11.3922068' AS DateTime2), 6)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3065, 2031, 60, 1, NULL, 4, 0, CAST(N'2022-09-29T08:08:05.7593107' AS DateTime2), 6, CAST(N'2022-09-29T08:11:11.3922386' AS DateTime2), 6)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3066, 2031, 80, 1, NULL, 4, 0, CAST(N'2022-09-29T08:08:10.5429466' AS DateTime2), 6, CAST(N'2022-09-29T08:11:11.3922437' AS DateTime2), 6)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3067, 2032, 32, 1, NULL, 4, 1, CAST(N'2022-09-29T09:07:07.3601140' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3068, 2032, 40, 1, NULL, 4, 1, CAST(N'2022-09-29T09:07:13.1329513' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3069, 2032, 49, 1, NULL, 2, 1, CAST(N'2022-09-29T09:07:28.8422061' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3070, 2032, 52, 1, NULL, 2, 1, CAST(N'2022-09-29T09:07:31.4406406' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3071, 2032, 58, 1, NULL, 2, 1, CAST(N'2022-09-29T09:07:34.4249409' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3072, 2032, 33, 1, NULL, 4, 1, CAST(N'2022-09-29T09:07:43.4388764' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3073, 2032, 37, 1, NULL, 4, 1, CAST(N'2022-09-29T09:07:50.2547352' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3074, 2032, 42, 1, NULL, 4, 1, CAST(N'2022-09-29T09:07:54.5953261' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3075, 2032, 62, 1, NULL, 4, 1, CAST(N'2022-09-29T09:08:12.5200655' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[OrderDetails] ([ID], [ORDER_ID], [MENU_ITEM_ID], [QUANTITY], [RELATED_MENU_ITEM_ID], [ORDER_DETAIL_STATUS_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (3076, 2032, 63, 1, NULL, 4, 1, CAST(N'2022-09-29T09:08:18.8636794' AS DateTime2), 6, NULL, NULL)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetailStatus] ON 

INSERT [dbo].[OrderDetailStatus] ([ID], [NAME]) VALUES (1, N'Pendiente')
INSERT [dbo].[OrderDetailStatus] ([ID], [NAME]) VALUES (2, N'Cocinando')
INSERT [dbo].[OrderDetailStatus] ([ID], [NAME]) VALUES (3, N'Listo')
INSERT [dbo].[OrderDetailStatus] ([ID], [NAME]) VALUES (4, N'Entregado')
SET IDENTITY_INSERT [dbo].[OrderDetailStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([ID], [TABLE_ID], [CALL], [PAYMENT_REQUEST], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (2029, 1, 0, 0, 1, CAST(N'2022-09-29T08:00:06.2019395' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[Orders] ([ID], [TABLE_ID], [CALL], [PAYMENT_REQUEST], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (2030, 2, 1, 0, 1, CAST(N'2022-09-29T08:05:25.7995884' AS DateTime2), 6, CAST(N'2022-09-29T08:06:05.8892325' AS DateTime2), 6)
INSERT [dbo].[Orders] ([ID], [TABLE_ID], [CALL], [PAYMENT_REQUEST], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (2031, 7, 0, 1, 1, CAST(N'2022-09-29T08:07:37.5476046' AS DateTime2), 6, CAST(N'2022-09-29T08:11:11.3921529' AS DateTime2), 6)
INSERT [dbo].[Orders] ([ID], [TABLE_ID], [CALL], [PAYMENT_REQUEST], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (2032, 1015, 0, 0, 1, CAST(N'2022-09-29T09:06:49.8336424' AS DateTime2), 6, NULL, NULL)
INSERT [dbo].[Orders] ([ID], [TABLE_ID], [CALL], [PAYMENT_REQUEST], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (2033, 1017, 1, 0, 1, CAST(N'2022-09-29T09:08:31.5420721' AS DateTime2), 6, CAST(N'2022-09-29T09:08:35.7291443' AS DateTime2), 6)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([ID], [ORDER_ID], [TOTAL_AMOUNT], [CREATION_DATE], [CREATION_USER]) VALUES (2002, 2031, 6200, CAST(N'2022-09-29T08:11:11.3932567' AS DateTime2), 6)
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[Tables] ON 

INSERT [dbo].[Tables] ([ID], [NUMBER], [NAME], [DESCRIPTION], [WAITER_ID], [WAITER_BACK_UP_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (1, 1, N'Ventana 1', N'2 comensales', 2, 7, 1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2022-09-21T11:04:51.8136979' AS DateTime2), 1)
INSERT [dbo].[Tables] ([ID], [NUMBER], [NAME], [DESCRIPTION], [WAITER_ID], [WAITER_BACK_UP_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (2, 2, N'Ventana 2', N'2 comensales', 2, 7, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Tables] ([ID], [NUMBER], [NAME], [DESCRIPTION], [WAITER_ID], [WAITER_BACK_UP_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (6, 3, N'Medio 1', N'4 comensales', 2, 7, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Tables] ([ID], [NUMBER], [NAME], [DESCRIPTION], [WAITER_ID], [WAITER_BACK_UP_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (7, 4, N'Medio 2', N'2 a 3 comensales', 2, 7, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Tables] ([ID], [NUMBER], [NAME], [DESCRIPTION], [WAITER_ID], [WAITER_BACK_UP_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (9, 5, N'Fondo 1', N'5 comensales', 2, 7, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Tables] ([ID], [NUMBER], [NAME], [DESCRIPTION], [WAITER_ID], [WAITER_BACK_UP_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (10, 6, N'Fondo 2', N'3 a 5 comensales', 4, 7, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Tables] ([ID], [NUMBER], [NAME], [DESCRIPTION], [WAITER_ID], [WAITER_BACK_UP_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (1015, 7, N'Bar 1', N'2 comensales', 4, 7, 1, CAST(N'2022-09-08T19:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Tables] ([ID], [NUMBER], [NAME], [DESCRIPTION], [WAITER_ID], [WAITER_BACK_UP_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (1016, 8, N'Bar 2', N'3 comensales', 4, 7, 1, CAST(N'2022-09-08T19:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Tables] ([ID], [NUMBER], [NAME], [DESCRIPTION], [WAITER_ID], [WAITER_BACK_UP_ID], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (1017, 9, N'Bar 3', N'2 comensales', 4, 7, 1, CAST(N'2022-09-21T14:07:57.4088764' AS DateTime2), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Tables] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [NAME], [SURNAME], [EMAIL], [PASSWORD], [ACCESS_TYPE], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (1, N'Marcela', N'Rodriguez', N'administrador@correo.com', N'12345', 6, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [NAME], [SURNAME], [EMAIL], [PASSWORD], [ACCESS_TYPE], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (2, N'Julián', N'García', N'mozo1@correo.com', N'12345', 11, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, CAST(N'2022-09-08T13:46:51.7807979' AS DateTime2), NULL)
INSERT [dbo].[Users] ([ID], [NAME], [SURNAME], [EMAIL], [PASSWORD], [ACCESS_TYPE], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (4, N'Victoria', N'Méndez', N'mozo2@correo.com', N'12345', 11, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [NAME], [SURNAME], [EMAIL], [PASSWORD], [ACCESS_TYPE], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (5, N'Camila', N'Benitez', N'cocina@correo.com', N'12345', 13, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, CAST(N'2022-09-08T13:46:28.2471040' AS DateTime2), NULL)
INSERT [dbo].[Users] ([ID], [NAME], [SURNAME], [EMAIL], [PASSWORD], [ACCESS_TYPE], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (6, N'Cliente', N'Cliente', N'cliente@correo.com', N'12345', 10, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, CAST(N'2022-09-08T13:46:45.0274695' AS DateTime2), NULL)
INSERT [dbo].[Users] ([ID], [NAME], [SURNAME], [EMAIL], [PASSWORD], [ACCESS_TYPE], [ACTIVE], [CREATION_DATE], [CREATION_USER], [UPDATE_DATE], [UPDATE_USER]) VALUES (7, N'Sebastián', N'Contreras', N'mozo3@correo.com', N'12345', 11, 1, CAST(N'2022-07-11T00:00:00.0000000' AS DateTime2), 1, CAST(N'2022-09-08T13:46:34.8805389' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[AccessTypes]  WITH CHECK ADD  CONSTRAINT [FK_TBL_ACCESS_TYPES_TBL_USERS_CREATION_USER] FOREIGN KEY([CREATION_USER])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[AccessTypes] CHECK CONSTRAINT [FK_TBL_ACCESS_TYPES_TBL_USERS_CREATION_USER]
GO
ALTER TABLE [dbo].[AccessTypes]  WITH CHECK ADD  CONSTRAINT [FK_TBL_ACCESS_TYPES_TBL_USERS_UPDATE_USER] FOREIGN KEY([UPDATE_USER])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[AccessTypes] CHECK CONSTRAINT [FK_TBL_ACCESS_TYPES_TBL_USERS_UPDATE_USER]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_TBL_CATEGORIES_TBL_USERS_CREATION_USER] FOREIGN KEY([CREATION_USER])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_TBL_CATEGORIES_TBL_USERS_CREATION_USER]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_TBL_CATEGORIES_TBL_USERS_UPDATE_USER] FOREIGN KEY([UPDATE_USER])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_TBL_CATEGORIES_TBL_USERS_UPDATE_USER]
GO
ALTER TABLE [dbo].[MenuItems]  WITH CHECK ADD  CONSTRAINT [FK_TBL_MENU_TBL_CATEGORIES_ID] FOREIGN KEY([Category_Id])
REFERENCES [dbo].[Categories] ([ID])
GO
ALTER TABLE [dbo].[MenuItems] CHECK CONSTRAINT [FK_TBL_MENU_TBL_CATEGORIES_ID]
GO
ALTER TABLE [dbo].[MenuItems]  WITH CHECK ADD  CONSTRAINT [FK_TBL_MENU_TBL_USERS_CREATION_USER] FOREIGN KEY([Creation_User])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[MenuItems] CHECK CONSTRAINT [FK_TBL_MENU_TBL_USERS_CREATION_USER]
GO
ALTER TABLE [dbo].[MenuItems]  WITH CHECK ADD  CONSTRAINT [FK_TBL_MENU_TBL_USERS_UPDATE_USER] FOREIGN KEY([Update_User])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[MenuItems] CHECK CONSTRAINT [FK_TBL_MENU_TBL_USERS_UPDATE_USER]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__ORDER__59904A2C] FOREIGN KEY([ORDER_ID])
REFERENCES [dbo].[Orders] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__ORDER__59904A2C]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([ORDER_DETAIL_STATUS_ID])
REFERENCES [dbo].[OrderDetailStatus] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_TBL_ORDER_DETAILS_TBL_USERS_CREATION_USER] FOREIGN KEY([CREATION_USER])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_TBL_ORDER_DETAILS_TBL_USERS_CREATION_USER]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_TBL_ORDER_DETAILS_TBL_USERS_UPDATE_USER] FOREIGN KEY([UPDATE_USER])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_TBL_ORDER_DETAILS_TBL_USERS_UPDATE_USER]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__TABLE_ID__57A801BA] FOREIGN KEY([TABLE_ID])
REFERENCES [dbo].[Tables] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__TABLE_ID__57A801BA]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_TBL_ORDERS_TBL_USERS_CREATION_USER] FOREIGN KEY([CREATION_USER])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_TBL_ORDERS_TBL_USERS_CREATION_USER]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_TBL_ORDERS_TBL_USERS_UPDATE_USER] FOREIGN KEY([UPDATE_USER])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_TBL_ORDERS_TBL_USERS_UPDATE_USER]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([ORDER_ID])
REFERENCES [dbo].[Orders] ([ID])
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_TBL_PAYMENTS_TBL_USERS_CREATION_USER] FOREIGN KEY([CREATION_USER])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_TBL_PAYMENTS_TBL_USERS_CREATION_USER]
GO
ALTER TABLE [dbo].[Tables]  WITH CHECK ADD  CONSTRAINT [FK__Tables__WAITER__5B78929E] FOREIGN KEY([WAITER_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Tables] CHECK CONSTRAINT [FK__Tables__WAITER__5B78929E]
GO
ALTER TABLE [dbo].[Tables]  WITH CHECK ADD  CONSTRAINT [FK__Tables__WAITER_B__5C6CB6D7] FOREIGN KEY([WAITER_BACK_UP_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Tables] CHECK CONSTRAINT [FK__Tables__WAITER_B__5C6CB6D7]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_TBL_USERS_TBL_ACCESS_TYPES_ID] FOREIGN KEY([ACCESS_TYPE])
REFERENCES [dbo].[AccessTypes] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_TBL_USERS_TBL_ACCESS_TYPES_ID]
GO
/****** Object:  StoredProcedure [dbo].[sp_categories_get_all]    Script Date: 24/10/2022 14:44:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_categories_get_all]

AS
BEGIN

SELECT Id, Name, ImageUrl, ACTIVE FROM Categories

END

GO
/****** Object:  StoredProcedure [dbo].[sp_menuItems_get_all_filtered]    Script Date: 24/10/2022 14:44:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_menuItems_get_all_filtered]
@SearchField nvarchar(50) = null,
@Active bit = null

AS
BEGIN

SELECT mi.Id, mi.Category_Id, mi.NAME as CategoryName, mi.VisualizationOrder, mi.Name, mi.Description, mi.Active, mi.ImageUrl, mi.Price  FROM MenuItems mi
INNER JOIN Categories cat on cat.Id = mi.Category_Id
where
((@SearchField IS NULL OR mi.Name LIKE '%' + @SearchField+ '%')
OR (@SearchField IS NULL OR mi.Description LIKE '%' + @SearchField + '%')
OR (@SearchField IS NULL OR CAST (mi.VisualizationOrder AS varchar) LIKE '%' + @SearchField + '%')
OR (@SearchField IS NULL OR CAST (mi.Price as varchar) LIKE '%' + @SearchField + '%'))
AND (@Active IS NULL OR mi.Active = @Active)
END

GO
/****** Object:  StoredProcedure [dbo].[sp_menuItems_get_all_filtered_by_catId]    Script Date: 24/10/2022 14:44:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_menuItems_get_all_filtered_by_catId]
@catId int,
@Active bit = null

AS
BEGIN

SELECT * FROM MenuItems
where
Category_Id = @catId
AND Active = @Active
END

GO
/****** Object:  StoredProcedure [dbo].[sp_tables_get_all_filtered]    Script Date: 24/10/2022 14:44:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tables_get_all_filtered]
@SearchField nvarchar(50) = null,
@Active bit = null

AS
BEGIN

SELECT tb.Id, 
tb.NUMBER,
tb.NAME, 
tb.DESCRIPTION, 
tb.CREATION_DATE, 
tb.CREATION_USER, 
tb.ACTIVE
FROM dbo.[Tables] tb

where
(@SearchField IS NULL OR tb.Name LIKE '%' + @SearchField+ '%'
OR (@SearchField IS NULL OR tb.Description LIKE '%' + @SearchField + '%')
OR (@SearchField IS NULL OR tb.Number = CAST(@SearchField AS int))
AND (@Active IS NULL OR Active = @Active))
END


GO
/****** Object:  StoredProcedure [dbo].[sp_users_get_all_filtered]    Script Date: 24/10/2022 14:44:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_users_get_all_filtered]
@SearchField nvarchar(50) = null,
@Active bit = null

AS
BEGIN

SELECT us.Id, us.Name, us.Surname, us.ACCESS_TYPE, us.EMAIL, us.CREATION_DATE, us.CREATION_USER, us.ACTIVE, us.Password, aty.Name as AccessTypeName FROM Users us
INNER JOIN AccessTypes aty ON us.ACCESS_TYPE = aty.ID
where
(@SearchField IS NULL OR 'Name' LIKE '%' + @SearchField+ '%'
OR (@SearchField IS NULL OR Email LIKE '%' + @SearchField + '%')
OR (@SearchField IS NULL OR ACCESS_TYPE = CAST(@SearchField AS int))
AND (@Active IS NULL OR us.Active = @Active))
END


GO
/****** Object:  StoredProcedure [dbo].[sp_users_get_by_email]    Script Date: 24/10/2022 14:44:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_users_get_by_email]
@Email nvarchar(80)

AS
BEGIN

SELECT us.Id, us.Name, us.Surname, us.ACCESS_TYPE, us.EMAIL, us.ACTIVE, us.Password, 
aty.Name as AccessTypeName, us.CREATION_DATE, us.CREATION_USER, us.UPDATE_DATE, us.UPDATE_USER
FROM Users us
INNER JOIN AccessTypes aty ON us.ACCESS_TYPE = aty.ID
where Email = @Email

END
	
GO
USE [master]
GO
ALTER DATABASE [SelfResto] SET  READ_WRITE 
GO
