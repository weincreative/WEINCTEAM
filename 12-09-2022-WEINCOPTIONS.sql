USE [master]
GO
/****** Object:  Database [WEINCOPTIONS]    Script Date: 12.09.2022 09:14:04 ******/
CREATE DATABASE [WEINCOPTIONS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WEINCOPTIONS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\WEINCOPTIONS.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WEINCOPTIONS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\WEINCOPTIONS_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WEINCOPTIONS] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WEINCOPTIONS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WEINCOPTIONS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET ARITHABORT OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [WEINCOPTIONS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WEINCOPTIONS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WEINCOPTIONS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WEINCOPTIONS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WEINCOPTIONS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WEINCOPTIONS] SET  MULTI_USER 
GO
ALTER DATABASE [WEINCOPTIONS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WEINCOPTIONS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WEINCOPTIONS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WEINCOPTIONS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [WEINCOPTIONS]
GO
/****** Object:  Table [dbo].[hst_weincoptions]    Script Date: 12.09.2022 09:14:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_weincoptions](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_sure] [int] NULL,
	[t_kayittarihi] [nvarchar](10) NULL,
	[t_kullanici] [nvarchar](50) NULL,
	[t_sifre] [nvarchar](50) NULL,
	[t_serial] [nvarchar](100) NULL,
	[t_yetki] [nvarchar](100) NULL,
	[t_createdate] [date] NULL,
 CONSTRAINT [PK_hst_weincoptions] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [WEINCOPTIONS] SET  READ_WRITE 
GO
