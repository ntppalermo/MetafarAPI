

--1.- ======== SCRIPT CREACION DB - ESTRUCTURA ============

USE [master]
GO
/****** Object:  Database [MetafarDB]  ******/
CREATE DATABASE [MetafarDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MetafarDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MetafarDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MetafarDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MetafarDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MetafarDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MetafarDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MetafarDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MetafarDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MetafarDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MetafarDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MetafarDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MetafarDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MetafarDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MetafarDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MetafarDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MetafarDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MetafarDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MetafarDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MetafarDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MetafarDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MetafarDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MetafarDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MetafarDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MetafarDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MetafarDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MetafarDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MetafarDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MetafarDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MetafarDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MetafarDB] SET  MULTI_USER 
GO
ALTER DATABASE [MetafarDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MetafarDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MetafarDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MetafarDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MetafarDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MetafarDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MetafarDB] SET QUERY_STORE = OFF
GO
USE [MetafarDB]
GO
/****** Object:  Table [dbo].[Operaciones]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operaciones](
	[OperacionID] [int] IDENTITY(1,1) NOT NULL,
	[NumeroTarjeta] [int] NULL,
	[TipoOperacionID] [int] NULL,
	[MontoExtraido] [decimal](18, 2) NULL,
	[FechaOperacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[OperacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tarjetas]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarjetas](
	[NumeroTarjeta] [int] NOT NULL,
	[UsuarioID] [int] NULL,
	[Pin] [int] NOT NULL,
	[Saldo] [decimal](18, 2) NULL,
	[IntentosFallidos] [int] NULL,
	[FechaUltimaExtraccion] [datetime] NULL,
	[Bloqueada] [bit] NOT NULL,
 CONSTRAINT [PK__Tarjetas__BC163C0B4444BFF8] PRIMARY KEY CLUSTERED 
(
	[NumeroTarjeta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposOperaciones]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposOperaciones](
	[TipoOperacionID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TipoOperacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[NumeroCuenta] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_NumeroCuenta] UNIQUE NONCLUSTERED 
(
	[NumeroCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Operaciones] ADD  DEFAULT (getdate()) FOR [FechaOperacion]
GO
ALTER TABLE [dbo].[Tarjetas] ADD  CONSTRAINT [DF__Tarjetas__Saldo__286302EC]  DEFAULT ((0)) FOR [Saldo]
GO
ALTER TABLE [dbo].[Tarjetas] ADD  CONSTRAINT [DF__Tarjetas__Intent__29572725]  DEFAULT ((0)) FOR [IntentosFallidos]
GO
ALTER TABLE [dbo].[Tarjetas] ADD  CONSTRAINT [DF__Tarjetas__Bloque__2A4B4B5E]  DEFAULT ((0)) FOR [Bloqueada]
GO
ALTER TABLE [dbo].[Operaciones]  WITH CHECK ADD  CONSTRAINT [FK__Operacion__Numer__2F10007B] FOREIGN KEY([NumeroTarjeta])
REFERENCES [dbo].[Tarjetas] ([NumeroTarjeta])
GO
ALTER TABLE [dbo].[Operaciones] CHECK CONSTRAINT [FK__Operacion__Numer__2F10007B]
GO
ALTER TABLE [dbo].[Operaciones]  WITH CHECK ADD FOREIGN KEY([TipoOperacionID])
REFERENCES [dbo].[TiposOperaciones] ([TipoOperacionID])
GO
ALTER TABLE [dbo].[Tarjetas]  WITH CHECK ADD  CONSTRAINT [FK__Tarjetas__Usuari__276EDEB3] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[Tarjetas] CHECK CONSTRAINT [FK__Tarjetas__Usuari__276EDEB3]
GO
USE [master]
GO
ALTER DATABASE [MetafarDB] SET  READ_WRITE 
GO



--2. ========== SCRIPT INSERT DATOS DE EJEMPLO  ============

USE [MetafarDB]
GO
SET IDENTITY_INSERT [dbo].[TiposOperaciones] ON 
GO
INSERT [dbo].[TiposOperaciones] ([TipoOperacionID], [Nombre]) VALUES (1, N'Consulta de Saldo')
GO
INSERT [dbo].[TiposOperaciones] ([TipoOperacionID], [Nombre]) VALUES (2, N'Extraccion')
GO
SET IDENTITY_INSERT [dbo].[TiposOperaciones] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 
GO
INSERT [dbo].[Usuarios] ([UsuarioID], [Nombre], [NumeroCuenta]) VALUES (1, N'Pedro ', 2343)
GO
INSERT [dbo].[Usuarios] ([UsuarioID], [Nombre], [NumeroCuenta]) VALUES (2, N'Jose', 4444)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
INSERT [dbo].[Tarjetas] ([NumeroTarjeta], [UsuarioID], [Pin], [Saldo], [IntentosFallidos], [FechaUltimaExtraccion], [Bloqueada]) VALUES (123456, 1, 123, CAST(10000.00 AS Decimal(18, 2)), 0, CAST(N'2023-12-05T19:40:12.000' AS DateTime), 0)
GO
INSERT [dbo].[Tarjetas] ([NumeroTarjeta], [UsuarioID], [Pin], [Saldo], [IntentosFallidos], [FechaUltimaExtraccion], [Bloqueada]) VALUES (444444, 1, 444, CAST(339000.00 AS Decimal(18, 2)), 0, NULL, 0)
GO
INSERT [dbo].[Tarjetas] ([NumeroTarjeta], [UsuarioID], [Pin], [Saldo], [IntentosFallidos], [FechaUltimaExtraccion], [Bloqueada]) VALUES (654321, 2, 321, CAST(45000.00 AS Decimal(18, 2)), 0, CAST(N'2023-11-11T12:30:00.000' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[Operaciones] ON 
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (1, 654321, 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2023-12-13T00:59:11.377' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (2, 654321, 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2023-12-13T01:00:57.063' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (3, 654321, 1, NULL, CAST(N'2023-12-13T02:00:57.063' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4, 654321, 1, NULL, CAST(N'2023-12-13T01:07:25.967' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (5, 123456, 1, NULL, CAST(N'2023-12-13T19:24:30.640' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (6, 123456, 2, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2023-12-13T19:42:15.997' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (7, 123456, 1, NULL, CAST(N'2023-12-13T19:58:32.683' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (8, 123456, 1, NULL, CAST(N'2023-12-13T19:59:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (9, 123456, 1, NULL, CAST(N'2023-12-13T20:00:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (10, 123456, 1, NULL, CAST(N'2023-12-13T20:01:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (11, 123456, 1, NULL, CAST(N'2023-12-13T20:02:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (12, 123456, 1, NULL, CAST(N'2023-12-13T20:03:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (13, 123456, 1, NULL, CAST(N'2023-12-13T20:04:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (14, 123456, 1, NULL, CAST(N'2023-12-13T20:05:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (15, 123456, 1, NULL, CAST(N'2023-12-13T20:06:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (16, 123456, 1, NULL, CAST(N'2023-12-13T20:07:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (17, 123456, 1, NULL, CAST(N'2023-12-13T20:08:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (18, 123456, 1, NULL, CAST(N'2023-12-13T20:09:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (19, 123456, 1, NULL, CAST(N'2023-12-13T20:19:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (20, 123456, 1, NULL, CAST(N'2023-12-13T20:22:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (21, 123456, 1, NULL, CAST(N'2023-12-13T20:28:29.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (22, 123456, 1, NULL, CAST(N'2023-12-13T19:59:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (23, 123456, 1, NULL, CAST(N'2023-12-13T20:00:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (24, 123456, 1, NULL, CAST(N'2023-12-13T20:01:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (25, 123456, 1, NULL, CAST(N'2023-12-13T20:02:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (26, 123456, 1, NULL, CAST(N'2023-12-13T20:03:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (27, 123456, 1, NULL, CAST(N'2023-12-13T20:04:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (28, 123456, 1, NULL, CAST(N'2023-12-13T20:05:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (29, 123456, 1, NULL, CAST(N'2023-12-13T20:06:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (30, 123456, 1, NULL, CAST(N'2023-12-13T20:07:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (31, 123456, 1, NULL, CAST(N'2023-12-13T20:08:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (32, 123456, 1, NULL, CAST(N'2023-12-13T20:09:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (33, 123456, 1, NULL, CAST(N'2023-12-13T20:19:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (34, 123456, 1, NULL, CAST(N'2023-12-13T20:22:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (35, 123456, 1, NULL, CAST(N'2023-12-13T20:28:45.713' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (36, 123456, 1, NULL, CAST(N'2023-12-13T14:33:10.240' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (37, 444444, 1, NULL, CAST(N'2023-12-13T14:33:10.240' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (38, 123456, 1, NULL, CAST(N'2023-12-13T14:34:58.040' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (39, 444444, 1, NULL, CAST(N'2023-12-13T14:34:58.040' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (40, 123456, 1, NULL, CAST(N'2023-12-13T14:35:28.247' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (41, 444444, 1, NULL, CAST(N'2023-12-13T14:35:28.247' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (42, 123456, 1, NULL, CAST(N'2023-12-13T14:36:59.700' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (43, 444444, 1, NULL, CAST(N'2023-12-13T14:36:59.700' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (44, 123456, 1, NULL, CAST(N'2023-12-13T14:37:08.157' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (45, 444444, 1, NULL, CAST(N'2023-12-13T14:37:08.157' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (46, 123456, 1, NULL, CAST(N'2023-12-13T14:40:26.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (47, 444444, 1, NULL, CAST(N'2023-12-13T14:40:26.440' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (48, 123456, 1, NULL, CAST(N'2023-12-13T14:41:32.727' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (49, 444444, 1, NULL, CAST(N'2023-12-13T14:41:32.727' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (50, 123456, 1, NULL, CAST(N'2023-12-13T14:42:24.937' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (51, 444444, 1, NULL, CAST(N'2023-12-13T14:42:24.937' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (52, 123456, 1, NULL, CAST(N'2023-12-13T12:30:12.030' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (53, 123456, 1, NULL, CAST(N'2023-12-13T12:36:50.617' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (54, 123456, 1, NULL, CAST(N'2023-12-13T12:41:09.030' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (1036, 123456, 1, NULL, CAST(N'2023-12-16T17:16:55.323' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (1037, 123456, 1, NULL, CAST(N'2023-12-16T20:30:44.937' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (2036, 444444, 1, NULL, CAST(N'2023-12-13T16:22:26.270' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (2037, 123456, 2, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2023-12-13T16:14:58.127' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (2038, 123456, 2, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2023-12-13T16:17:41.930' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (2039, 123456, 2, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2023-12-13T16:23:30.127' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (2040, 123456, 2, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2023-12-13T16:25:30.760' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (3036, 123456, 2, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2023-12-14T10:43:42.153' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (3037, 123456, 1, NULL, CAST(N'2023-12-14T16:50:42.837' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (3038, 444444, 1, NULL, CAST(N'2023-12-14T16:51:44.630' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (3039, 654321, 1, NULL, CAST(N'2023-12-14T16:51:59.417' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (3040, 123456, 1, NULL, CAST(N'2023-12-14T20:58:52.073' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (3041, 444444, 1, NULL, CAST(N'2023-12-14T20:59:03.483' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (3042, 444444, 1, NULL, CAST(N'2023-12-14T21:14:51.527' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (3043, 444444, 1, NULL, CAST(N'2023-12-14T22:28:43.793' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4036, 123456, 2, CAST(1000.00 AS Decimal(18, 2)), CAST(N'2023-12-15T12:05:17.920' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4037, 123456, 2, CAST(1000.00 AS Decimal(18, 2)), CAST(N'2023-12-15T17:11:17.940' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4038, 123456, 2, CAST(1000.00 AS Decimal(18, 2)), CAST(N'2023-12-16T17:15:51.823' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4039, 444444, 1, NULL, CAST(N'2023-12-17T12:59:18.877' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4040, 654321, 1, NULL, CAST(N'2023-12-17T12:59:48.733' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4041, 654321, 1, NULL, CAST(N'2023-12-17T13:00:02.580' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4042, 654321, 1, NULL, CAST(N'2023-12-17T13:00:54.873' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4043, 654321, 1, NULL, CAST(N'2023-12-17T13:01:58.820' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4044, 123456, 1, NULL, CAST(N'2023-12-17T15:38:06.850' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4045, 123456, 1, NULL, CAST(N'2023-12-17T15:39:26.330' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4046, 123456, 1, NULL, CAST(N'2023-12-17T16:14:44.523' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4047, 444444, 1, NULL, CAST(N'2023-12-17T16:15:03.520' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4048, 444444, 1, NULL, CAST(N'2023-12-17T16:19:29.163' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4049, 444444, 1, NULL, CAST(N'2023-12-17T16:32:32.630' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4050, 123456, 1, NULL, CAST(N'2023-12-17T17:50:21.130' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4051, 444444, 1, NULL, CAST(N'2023-12-17T17:52:43.243' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4052, 444444, 1, NULL, CAST(N'2023-12-17T18:23:23.983' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4053, 444444, 1, NULL, CAST(N'2023-12-17T18:24:13.640' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4054, 444444, 2, CAST(1000.00 AS Decimal(18, 2)), CAST(N'2023-12-17T18:26:10.697' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4055, 123456, 1, NULL, CAST(N'2023-12-17T19:00:25.603' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4056, 123456, 1, NULL, CAST(N'2023-12-17T19:01:02.453' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4057, 123456, 2, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2023-12-17T19:05:54.027' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4058, 123456, 1, NULL, CAST(N'2023-12-17T19:08:37.220' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4059, 123456, 2, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2023-12-17T19:08:48.290' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4060, 123456, 1, NULL, CAST(N'2023-12-17T19:13:47.530' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4061, 123456, 2, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2023-12-17T19:14:00.427' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4062, 123456, 1, NULL, CAST(N'2023-12-17T19:29:45.623' AS DateTime))
GO
INSERT [dbo].[Operaciones] ([OperacionID], [NumeroTarjeta], [TipoOperacionID], [MontoExtraido], [FechaOperacion]) VALUES (4063, 123456, 2, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2023-12-17T19:30:03.020' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Operaciones] OFF
GO
