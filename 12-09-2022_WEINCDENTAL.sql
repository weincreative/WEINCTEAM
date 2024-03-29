USE [master]
GO
/****** Object:  Database [WEINCDENTAL]    Script Date: 12.09.2022 09:14:59 ******/
CREATE DATABASE [WEINCDENTAL]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WEINCDENTAL', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\WEINCDENTAL.mdf' , SIZE = 6144KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WEINCDENTAL_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\WEINCDENTAL_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WEINCDENTAL] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WEINCDENTAL].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WEINCDENTAL] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET ARITHABORT OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [WEINCDENTAL] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WEINCDENTAL] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WEINCDENTAL] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WEINCDENTAL] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WEINCDENTAL] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WEINCDENTAL] SET  MULTI_USER 
GO
ALTER DATABASE [WEINCDENTAL] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WEINCDENTAL] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WEINCDENTAL] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WEINCDENTAL] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [WEINCDENTAL]
GO
/****** Object:  StoredProcedure [dbo].[sp$UVAktif]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp$UVAktif](
@bid INT
)

AS
set NOCOUNT ON;
update hst_vezne

set t_aktif=0

where t_bid=@bid

SET NOCOUNT OFF;



GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteHhareket]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_DeleteHhareket](

@hhid INT
)

as
set 
NOCOUNT ON;

declare @basid INT
declare @veznesay INT


select @basid=(select t_basvuruid from hst_his_hareket
where t_id=@hhid and t_aktif=1)


select @veznesay=(select COUNT(*) from hst_vezne v

where v.t_aktif=1 and v.t_bid=@basid and v.t_odenen!=0)




IF (@veznesay=0) -- silinmek istenen hizmet_hareketin vezne tarafında ödemesi yoksa işlem yap...

BEGIN

BEGIN TRY 
update hst_his_hareket set t_aktif=0,t_yapildi=0  where t_id=@hhid
END TRY
BEGIN CATCH

throw

END CATCH
END
ELSE BEGIN

	DECLARE @err_message nvarchar(255)

	set @err_message='Ödemesi yapılmış hizmet silinemez'
	RAISERROR (@err_message, 11,1)

END

set NOCOUNT OFF;




GO
/****** Object:  StoredProcedure [dbo].[sp_InsertVezne]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[sp_InsertVezne](
@hareketid INT,
@basid INT
)
AS
set NOCOUNT ON;
declare @say INT
declare @borc money
declare @aktifsay INT

select @say=(select Count(*) from hst_vezne where t_aktif=1 and t_bid=@basid)
select @borc=(select SUM(t_totalborc) from hst_his_hareket
where t_aktif=1 and t_basvuruid=@basid and t_yapildi=1)

select @aktifsay=(select COUNT(*) from hst_his_hareket where t_aktif=1 and t_basvuruid=@basid)

IF (@aktifsay!=0)   -- Hizmet hareket de aktif hizmet varsa işlem yap..
BEGIN

IF (@say=0)	--Girilen başvruya ait önceden vezne de kayıt var mı?
BEGIN

insert into hst_vezne (t_bid,t_odenen,t_kalan,t_total,t_indirim,t_odenecek,t_odemetipi,t_createuser,t_odemetarih,t_createdate,t_aktif)
select t_basvuruid,0,@borc,@borc,0,@borc,3,t_createuser,t_islemtarihi,t_createdate, 1 from hst_his_hareket hh where hh.t_id= @hareketid and t_aktif=1 and t_basvuruid=@basid and t_yapildi=1
END
ELSE IF EXISTS(select t_odenen from hst_vezne where t_aktif=1 and t_bid=@basid and t_odenen=0)
BEGIN

UPDATE hst_vezne SET t_total=@borc,t_kalan=@borc where t_bid=@basid and t_aktif=1

END

ELSE
BEGIN
UPDATE hst_vezne SET t_total=@borc where (t_bid=@basid and t_aktif=1) or (t_bid=@basid and t_odenen=0)
END
 END

 ELSE
BEGIN 
UPDATE hst_vezne SET t_aktif=0 where t_bid=@basid
END

SET NOCOUNT OFF;





GO
/****** Object:  StoredProcedure [dbo].[sp_LastPay]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_LastPay](
@vid INT,
@b_id INT
)

AS

set NOCOUNT ON;

declare @gelir money
declare @gider money

select @gelir=(Select v.t_total from hst_vezne v where v.t_aktif=1 and v.t_id=@vid)

select @gider=(
(Select SUM(v.t_odenen) from hst_vezne v where v.t_aktif=1 and v.t_id<=@vid and v.t_bid=@b_id )+
(Select SUM(v.t_indirim) from hst_vezne v where v.t_aktif=1 and v.t_id<=@vid and v.t_bid=@b_id)
)


IF (@gelir>=@gider)
BEGIN

Update hst_vezne set t_odenecek=(@gelir-@gider) where t_aktif=1 and t_id=@vid
END



 SET NOCOUNT OFF;



GO
/****** Object:  StoredProcedure [dbo].[sp_OdemeBitim]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_OdemeBitim](
@basid INT

)as 

set NOCOUNT OFF;
declare @total money
declare @odenen money


select @total=(select sum(t_totalborc) from hst_his_hareket
where t_aktif=1 and t_basvuruid=@basid and t_yapildi=1)

select @odenen=(select Sum(t_odenen)+Sum(t_indirim) from hst_vezne where t_aktif=1 and t_bid=@basid)

IF (@total=@odenen)
BEGIN
	
	update hst_basvuru set t_borc=0 where t_aktif=1 and t_id=@basid

END
ELSE
BEGIN

	update hst_basvuru set t_borc=1 where t_aktif=1 and t_id=@basid
END

set NOCOUNT ON;





GO
/****** Object:  StoredProcedure [dbo].[sp_VezneUpIlkKayit]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_VezneUpIlkKayit](

@bsvid INT
)
as
set NOCOUNT OFF;
declare @vid INT
declare @say INT

select @say=(select Count(*) from hst_vezne where t_aktif=1 and t_bid=@bsvid and t_odenen!=0)
select @vid=(select t_id from hst_vezne where  t_bid=@bsvid and t_odenen=0)

IF (@say=0)  -- başvuruya ait vezne kaydı yoksa 
BEGIN
	update hst_vezne set t_aktif=1 where t_id=@vid
END
ELSE
BEGIN
	update hst_vezne set t_aktif=0 where t_id=@vid
END

SET NOCOUNT ON;



GO
/****** Object:  Table [dbo].[adm_ControllerList]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[adm_ControllerList](
	[ControllerId] [int] IDENTITY(1,1) NOT NULL,
	[ControllerName] [varchar](250) NOT NULL,
 CONSTRAINT [PK_adm_ControllerList] PRIMARY KEY CLUSTERED 
(
	[ControllerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[adm_kullanicigrup]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adm_kullanicigrup](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_adi] [nvarchar](50) NOT NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_createdate] [date] NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_adm_kullanicigrup] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[adm_kullanicilar]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ARITHABORT ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[adm_kullanicilar](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_kid]  AS ([t_id]+(100)),
	[t_kodu] [nvarchar](50) NOT NULL,
	[t_adi] [nvarchar](50) NOT NULL,
	[t_sifre] [nvarchar](50) NOT NULL,
	[t_grup] [int] NULL,
	[t_yetki] [int] NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_createdate] [date] NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_adm_kullanicilar_1] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_adm_kullanicilar] UNIQUE NONCLUSTERED 
(
	[t_kodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_adm_kullanicilar_kid] UNIQUE NONCLUSTERED 
(
	[t_kid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[adm_MethodList]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adm_MethodList](
	[MethodId] [int] IDENTITY(1,1) NOT NULL,
	[MethodName] [nvarchar](250) NOT NULL,
	[GorunecekIsim] [nvarchar](250) NOT NULL,
	[ControllerId] [int] NOT NULL,
 CONSTRAINT [PK_adm_MethodList] PRIMARY KEY CLUSTERED 
(
	[MethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[adm_modulyetki]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adm_modulyetki](
	[t_id] [int] NOT NULL,
	[t_adi] [nvarchar](50) NOT NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_createdate] [date] NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_adm_modulyetki] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[adm_pacs]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[adm_pacs](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_pacspath] [nvarchar](500) NOT NULL,
	[t_resimad] [varchar](250) NOT NULL,
	[t_klasorad] [varchar](100) NOT NULL,
	[t_tc] [nvarchar](12) NOT NULL,
	[t_ip] [int] NOT NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_createdate] [date] NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_adm_pacs] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[adm_UserGroups]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adm_UserGroups](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[Aktif] [bit] NOT NULL,
 CONSTRAINT [PK_adm_UserGroups] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_adm_UserGroups] UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[adm_UserYetkis]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adm_UserYetkis](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[YetkiId] [bigint] NOT NULL,
	[Aktif] [bit] NOT NULL,
 CONSTRAINT [PK_adm_UserYetkis_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[YetkiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Table_1] UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[adm_Yetki]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adm_Yetki](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[YetkiAdi] [nvarchar](50) NOT NULL,
	[YetkiAciklama] [nvarchar](500) NOT NULL,
	[CreateDate] [date] NULL,
	[Aktif] [bit] NOT NULL,
 CONSTRAINT [PK_adm_Yetki] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[adm_YetkiGroups]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adm_YetkiGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[YetkiId] [bigint] NOT NULL,
	[GroupId] [int] NOT NULL,
	[Aktif] [bit] NOT NULL,
 CONSTRAINT [PK_adm_YetkiGroup] PRIMARY KEY CLUSTERED 
(
	[YetkiId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_adm_YetkiGroups] UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[adm_YetkiMethods]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adm_YetkiMethods](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[YetkiId] [bigint] NOT NULL,
	[MethodId] [int] NOT NULL,
	[Aktif] [bit] NOT NULL,
 CONSTRAINT [PK_adm_YetkiMethods] PRIMARY KEY CLUSTERED 
(
	[YetkiId] ASC,
	[MethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_adm_YetkiMethods] UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_basvuru]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_basvuru](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_basvuru] [nvarchar](50) NOT NULL CONSTRAINT [DF_hst_basvuru_t_basvuru]  DEFAULT ((0)),
	[t_tc] [nvarchar](12) NULL,
	[t_basvurutarihi] [datetime] NOT NULL,
	[t_bolumkodu] [int] NOT NULL,
	[t_cagriekraniistem] [int] NOT NULL,
	[t_basvurudr] [int] NULL,
	[t_taburcu] [bit] NOT NULL,
	[t_borc] [bit] NOT NULL,
	[t_createdate] [datetime] NOT NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_basvuru] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_bölüm]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_bölüm](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_adi] [nvarchar](50) NOT NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_createdate] [date] NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_bölüm] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_cene_uygunmu]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_cene_uygunmu](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_adi] [nvarchar](50) NOT NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_createdate] [date] NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_cene_uygunmu] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_cinsiyet]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_cinsiyet](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_adi] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_hst_cinsiyet] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_disno]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_disno](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_disno] [int] NOT NULL,
	[t_cene] [int] NOT NULL,
	[t_yetiskinmi] [bit] NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_disno] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_firma]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_firma](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_fad] [nvarchar](50) NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_firma] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_hastadurum]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_hastadurum](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_tc] [nvarchar](12) NOT NULL,
	[t_hdurumid] [int] NOT NULL,
	[t_aciklama] [nvarchar](100) NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_hastadurum2] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_hastakarti]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_hastakarti](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_tc] [nvarchar](12) NOT NULL,
	[t_adi] [nvarchar](50) NOT NULL,
	[t_soyadi] [nvarchar](50) NOT NULL,
	[t_cinsiyet] [int] NOT NULL,
	[t_medenidurum] [int] NOT NULL,
	[t_dogumtarihi] [date] NOT NULL,
	[t_dogumyeri] [nvarchar](50) NOT NULL,
	[t_tel1] [nvarchar](11) NOT NULL,
	[t_tel2] [nvarchar](11) NULL,
	[t_ulkeId] [int] NOT NULL,
	[t_ilId] [int] NULL,
	[t_ilceId] [int] NULL,
	[t_adres] [nvarchar](100) NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_createdate] [date] NOT NULL,
	[t_aktif] [bit] NOT NULL,
	[t_dosyano] [nvarchar](50) NULL,
 CONSTRAINT [PK_hst_hastakarti] PRIMARY KEY CLUSTERED 
(
	[t_tc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_hastalik]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_hastalik](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_adi] [nvarchar](50) NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_hastadurum] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_his_hareket]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_his_hareket](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_basvuruid] [int] NOT NULL,
	[t_hizmetkodu] [int] NOT NULL,
	[t_islemtarihi] [datetime] NOT NULL,
	[t_diskodu] [int] NOT NULL,
	[t_parca] [int] NOT NULL,
	[t_cene] [int] NOT NULL,
	[t_yetiskinmi] [bit] NOT NULL,
	[t_odemevarmi] [bit] NOT NULL,
	[t_totalborc] [money] NULL,
	[t_borcdurum] [bit] NOT NULL,
	[t_firmaid] [int] NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_createdate] [date] NOT NULL,
	[t_aktif] [bit] NOT NULL,
	[t_yapildi] [bit] NULL,
 CONSTRAINT [PK_hst_his_hareket] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_hizmet]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_hizmet](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_adi] [nvarchar](50) NOT NULL,
	[t_parcauygunmu] [int] NOT NULL,
	[t_ceneuygunmu] [int] NOT NULL,
	[t_fiyat] [money] NOT NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_createdate] [date] NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_hizmet] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_hizmet_parca]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_hizmet_parca](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_adi] [nvarchar](50) NOT NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_createdate] [date] NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_hizmet_parca] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_il]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_il](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_ulkeId] [int] NOT NULL,
	[t_adi] [nvarchar](100) NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_il] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_ilce]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_ilce](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_ilId] [int] NOT NULL,
	[t_adi] [nvarchar](100) NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_ilce] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_marka]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[hst_marka](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_fid] [int] NOT NULL,
	[t_mad] [varchar](50) NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_marka] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[hst_medenidurum]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_medenidurum](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_adi] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_hst_medenidurum] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_odemetip]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[hst_odemetip](
	[t_Id] [int] IDENTITY(1,1) NOT NULL,
	[t_adi] [varchar](15) NOT NULL,
 CONSTRAINT [PK_hst_OdemeTip] PRIMARY KEY CLUSTERED 
(
	[t_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[hst_randevu]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_randevu](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_basvuru] [int] NULL,
	[t_tc] [nvarchar](12) NOT NULL,
	[t_title] [nvarchar](100) NOT NULL,
	[t_aciklama] [nvarchar](200) NULL,
	[t_baslangicsaat] [nvarchar](50) NOT NULL,
	[t_bitissaat] [nvarchar](50) NOT NULL,
	[t_classname] [nvarchar](50) NULL,
	[t_icon] [nvarchar](50) NULL,
	[t_allday] [bit] NOT NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_createdate] [datetime] NOT NULL,
	[t_basvurudr] [int] NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_randevu] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_ulke]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_ulke](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[BinaryCode] [nvarchar](2) NOT NULL,
	[TripleCode] [nvarchar](3) NOT NULL,
	[CountryName] [nvarchar](100) NOT NULL,
	[PhoneCode] [nvarchar](6) NOT NULL,
	[Aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_ulke] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[hst_vezne]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hst_vezne](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_bid] [int] NOT NULL,
	[t_hizid] [int] NULL,
	[t_odenen] [money] NOT NULL CONSTRAINT [DF_hst_vezne_t_odenen]  DEFAULT ((0)),
	[t_kalan] [money] NOT NULL CONSTRAINT [DF_hst_vezne_t_kalan]  DEFAULT ((0)),
	[t_total] [money] NOT NULL,
	[t_indirim] [money] NOT NULL CONSTRAINT [DF_hst_vezne_t_indirim]  DEFAULT ((0)),
	[t_odenecek] [money] NOT NULL,
	[t_odemetipi] [int] NOT NULL,
	[t_createuser] [nvarchar](50) NOT NULL,
	[t_odemetarih] [datetime] NOT NULL,
	[t_createdate] [date] NOT NULL,
	[t_aktif] [bit] NOT NULL,
 CONSTRAINT [PK_hst_vezne] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[View_BsvrVezne]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_BsvrVezne]
AS
SELECT        dbo.hst_vezne.t_id AS v_id, dbo.hst_basvuru.t_id AS b_id, dbo.hst_basvuru.t_tc, dbo.hst_hastakarti.t_adi, dbo.hst_hastakarti.t_soyadi, dbo.hst_hastakarti.t_dogumtarihi, dbo.hst_basvuru.t_basvurutarihi, 
                         dbo.hst_vezne.t_odenen, dbo.hst_vezne.t_kalan, dbo.hst_vezne.t_total AS AsilTutar, dbo.hst_vezne.t_indirim, dbo.hst_vezne.t_odenecek AS OdenecekTutar, dbo.hst_odemetip.t_adi AS OdemeTip, dbo.hst_vezne.t_odemetarih, 
                         dbo.hst_basvuru.t_borc AS BorcDurum, dbo.hst_basvuru.t_aktif AS BasvuruAktif, dbo.hst_vezne.t_aktif AS VezneAktif, dbo.hst_hastakarti.t_aktif AS HastaAktif
FROM            dbo.hst_basvuru INNER JOIN
                         dbo.hst_vezne ON dbo.hst_basvuru.t_id = dbo.hst_vezne.t_bid INNER JOIN
                         dbo.hst_odemetip ON dbo.hst_vezne.t_odemetipi = dbo.hst_odemetip.t_Id INNER JOIN
                         dbo.hst_hastakarti ON dbo.hst_basvuru.t_tc = dbo.hst_hastakarti.t_tc


GO
/****** Object:  View [dbo].[View_CeneDis]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_CeneDis]
AS
SELECT        dbo.hst_cene_uygunmu.t_id AS CeneNo, dbo.hst_disno.t_disno AS DisNo, dbo.hst_cene_uygunmu.t_adi AS CeneAd, dbo.hst_disno.t_yetiskinmi AS Yetiskin, dbo.hst_disno.t_aktif AS DisAktif, 
                         dbo.hst_cene_uygunmu.t_aktif AS CeneAktif
FROM            dbo.hst_cene_uygunmu INNER JOIN
                         dbo.hst_disno ON dbo.hst_cene_uygunmu.t_id = dbo.hst_disno.t_cene






GO
/****** Object:  View [dbo].[View_GroupYetki]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_GroupYetki]
AS
SELECT        TOP (100) PERCENT dbo.adm_UserGroups.UserId, dbo.adm_kullanicilar.t_kid, dbo.adm_kullanicilar.t_kodu, dbo.adm_YetkiMethods.YetkiId AS YM_Id, dbo.adm_YetkiGroups.YetkiId, dbo.adm_YetkiMethods.MethodId, 
                         dbo.adm_kullanicigrup.t_id AS GroupId, dbo.adm_kullanicigrup.t_adi AS groupAd, dbo.adm_Yetki.YetkiAdi, dbo.adm_Yetki.YetkiAciklama, dbo.adm_MethodList.GorunecekIsim, dbo.adm_MethodList.MethodName, 
                         dbo.adm_ControllerList.ControllerName, dbo.adm_UserGroups.Aktif AS UserGroupAktif, dbo.adm_YetkiGroups.Aktif AS YetkiAktif, dbo.adm_kullanicilar.t_aktif AS UserAktif
FROM            dbo.adm_kullanicigrup INNER JOIN
                         dbo.adm_kullanicilar INNER JOIN
                         dbo.adm_UserGroups ON dbo.adm_kullanicilar.t_id = dbo.adm_UserGroups.UserId ON dbo.adm_kullanicigrup.t_id = dbo.adm_UserGroups.GroupId INNER JOIN
                         dbo.adm_YetkiGroups ON dbo.adm_kullanicigrup.t_id = dbo.adm_YetkiGroups.GroupId INNER JOIN
                         dbo.adm_Yetki ON dbo.adm_YetkiGroups.YetkiId = dbo.adm_Yetki.Id INNER JOIN
                         dbo.adm_YetkiMethods ON dbo.adm_Yetki.Id = dbo.adm_YetkiMethods.YetkiId INNER JOIN
                         dbo.adm_MethodList INNER JOIN
                         dbo.adm_ControllerList ON dbo.adm_MethodList.ControllerId = dbo.adm_ControllerList.ControllerId ON dbo.adm_YetkiMethods.MethodId = dbo.adm_MethodList.MethodId


GO
/****** Object:  View [dbo].[View_HastalikDurum]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_HastalikDurum]
AS
SELECT        dbo.hst_hastadurum.t_id, dbo.hst_hastadurum.t_tc, dbo.hst_hastakarti.t_adi, dbo.hst_hastakarti.t_soyadi, dbo.hst_hastalik.t_adi AS Hastalık, dbo.hst_hastadurum.t_aciklama AS Aciklama, dbo.hst_hastadurum.t_aktif
FROM            dbo.hst_hastadurum INNER JOIN
                         dbo.hst_hastakarti ON dbo.hst_hastadurum.t_tc = dbo.hst_hastakarti.t_tc INNER JOIN
                         dbo.hst_hastalik ON dbo.hst_hastadurum.t_hdurumid = dbo.hst_hastalik.t_id






GO
/****** Object:  View [dbo].[View_HizHareket]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_HizHareket]
AS
SELECT        dbo.hst_basvuru.t_id AS basvuruid, dbo.hst_hastakarti.t_tc, dbo.hst_hastakarti.t_adi, dbo.hst_hastakarti.t_soyadi, dbo.hst_basvuru.t_basvuru AS BasvuruNo, dbo.hst_basvuru.t_basvurutarihi, dbo.hst_bölüm.t_adi AS Doktoradi, 
                         dbo.hst_basvuru.t_basvurudr, dbo.hst_basvuru.t_taburcu, dbo.hst_his_hareket.t_id AS Hareketid, dbo.hst_his_hareket.t_hizmetkodu, dbo.hst_hizmet.t_adi AS Hizmetadi, dbo.hst_his_hareket.t_yetiskinmi, 
                         dbo.hst_his_hareket.t_diskodu, dbo.hst_hizmet.t_fiyat, dbo.hst_his_hareket.t_islemtarihi, dbo.hst_his_hareket.t_odemevarmi AS Odemeyapildimi, dbo.hst_his_hareket.t_cene AS CeneNo, 
                         dbo.hst_cene_uygunmu.t_adi AS CeneDurum, dbo.hst_his_hareket.t_aktif AS HHareketAkteif, dbo.hst_basvuru.t_aktif AS Baktif, dbo.hst_hastakarti.t_aktif AS HastaAktif, dbo.hst_his_hareket.t_yapildi
FROM            dbo.hst_basvuru INNER JOIN
                         dbo.hst_hastakarti ON dbo.hst_basvuru.t_tc = dbo.hst_hastakarti.t_tc INNER JOIN
                         dbo.hst_his_hareket ON dbo.hst_basvuru.t_id = dbo.hst_his_hareket.t_basvuruid INNER JOIN
                         dbo.hst_hizmet ON dbo.hst_his_hareket.t_hizmetkodu = dbo.hst_hizmet.t_id INNER JOIN
                         dbo.hst_bölüm ON dbo.hst_basvuru.t_bolumkodu = dbo.hst_bölüm.t_id INNER JOIN
                         dbo.hst_cene_uygunmu ON dbo.hst_his_hareket.t_cene = dbo.hst_cene_uygunmu.t_id



GO
/****** Object:  View [dbo].[View_HizmetDetay]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_HizmetDetay]
AS
SELECT        dbo.hst_his_hareket.t_id AS HizHareketId, dbo.hst_his_hareket.t_basvuruid AS BasvuruId, dbo.hst_his_hareket.t_hizmetkodu AS HizmetKod, dbo.hst_basvuru.t_tc AS TC, dbo.hst_hastakarti.t_adi AS Ad, 
                         dbo.hst_hastakarti.t_soyadi AS Soyad, dbo.hst_hastakarti.t_dogumtarihi AS DTarih, dbo.hst_hastakarti.t_tel1 AS Tel, dbo.hst_basvuru.t_basvurutarihi AS Basvuru_Tarih, dbo.hst_bölüm.t_adi AS DoktorAd, 
                         dbo.hst_basvuru.t_taburcu, dbo.hst_hizmet.t_adi AS HizmetAd, dbo.hst_his_hareket.t_diskodu AS DisNo, dbo.hst_his_hareket.t_cene AS CeneNo, dbo.hst_cene_uygunmu.t_adi AS CeneAdi, dbo.hst_hizmet.t_fiyat, 
                         dbo.hst_his_hareket.t_totalborc AS ToplamBorc, dbo.hst_his_hareket.t_odemevarmi AS Odemedurumu, dbo.hst_his_hareket.t_islemtarihi AS Hizmet_Tarih, dbo.hst_his_hareket.t_yetiskinmi AS YetiskinMi, dbo.hst_firma.t_fad, 
                         dbo.hst_marka.t_mad, dbo.hst_basvuru.t_borc AS BorcDurum, dbo.hst_bölüm.t_aktif AS DoktorAktif, dbo.hst_basvuru.t_aktif AS BasvuruAktif, dbo.hst_his_hareket.t_aktif AS HHareketAktif, dbo.hst_his_hareket.t_yapildi
FROM            dbo.hst_hastakarti INNER JOIN
                         dbo.hst_basvuru ON dbo.hst_hastakarti.t_tc = dbo.hst_basvuru.t_tc INNER JOIN
                         dbo.hst_his_hareket ON dbo.hst_basvuru.t_id = dbo.hst_his_hareket.t_basvuruid INNER JOIN
                         dbo.hst_hizmet ON dbo.hst_his_hareket.t_hizmetkodu = dbo.hst_hizmet.t_id INNER JOIN
                         dbo.hst_cene_uygunmu ON dbo.hst_his_hareket.t_cene = dbo.hst_cene_uygunmu.t_id INNER JOIN
                         dbo.hst_bölüm ON dbo.hst_basvuru.t_bolumkodu = dbo.hst_bölüm.t_id INNER JOIN
                         dbo.hst_firma ON dbo.hst_his_hareket.t_firmaid = dbo.hst_firma.t_id INNER JOIN
                         dbo.hst_marka ON dbo.hst_firma.t_id = dbo.hst_marka.t_fid



GO
/****** Object:  View [dbo].[View_Pacs]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Pacs]
AS
SELECT        dbo.adm_pacs.t_id, dbo.adm_pacs.t_pacspath, dbo.adm_pacs.t_resimad, dbo.adm_pacs.t_klasorad, dbo.adm_pacs.t_tc, dbo.hst_hastakarti.t_adi, dbo.hst_hastakarti.t_soyadi, dbo.hst_cinsiyet.t_adi AS Cinsiyet, 
                         dbo.adm_pacs.t_createdate, dbo.hst_hastakarti.t_dogumtarihi, dbo.adm_pacs.t_ip, dbo.hst_hastakarti.t_aktif AS HastaAktif, dbo.adm_pacs.t_aktif AS PacsAktif
FROM            dbo.adm_pacs INNER JOIN
                         dbo.hst_hastakarti ON dbo.adm_pacs.t_tc = dbo.hst_hastakarti.t_tc INNER JOIN
                         dbo.hst_cinsiyet ON dbo.hst_hastakarti.t_cinsiyet = dbo.hst_cinsiyet.t_id



GO
/****** Object:  View [dbo].[View_Users]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Users]
AS
SELECT        dbo.adm_kullanicilar.t_id, dbo.adm_kullanicilar.t_kodu, dbo.adm_kullanicilar.t_adi, dbo.adm_kullanicilar.t_sifre, dbo.adm_kullanicilar.t_createuser, dbo.adm_kullanicilar.t_createdate, dbo.adm_kullanicilar.t_aktif, 
                         dbo.adm_kullanicigrup.t_adi AS GroupAd, dbo.adm_kullanicilar.t_grup
FROM            dbo.adm_kullanicigrup INNER JOIN
                         dbo.adm_UserGroups ON dbo.adm_kullanicigrup.t_id = dbo.adm_UserGroups.GroupId INNER JOIN
                         dbo.adm_kullanicilar ON dbo.adm_UserGroups.UserId = dbo.adm_kullanicilar.t_id


GO
/****** Object:  View [dbo].[View_UserYetkis]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_UserYetkis]
AS
SELECT        dbo.adm_UserYetkis.UserId, dbo.adm_UserYetkis.YetkiId, dbo.adm_YetkiMethods.Id AS YMId, dbo.adm_kullanicilar.t_kid, dbo.adm_kullanicilar.t_kodu, dbo.adm_kullanicilar.t_adi, dbo.adm_Yetki.YetkiAdi, 
                         dbo.adm_Yetki.YetkiAciklama, dbo.adm_MethodList.GorunecekIsim, dbo.adm_MethodList.MethodName, dbo.adm_ControllerList.ControllerName, dbo.adm_UserYetkis.Aktif
FROM            dbo.adm_Yetki INNER JOIN
                         dbo.adm_MethodList INNER JOIN
                         dbo.adm_YetkiMethods ON dbo.adm_MethodList.MethodId = dbo.adm_YetkiMethods.MethodId INNER JOIN
                         dbo.adm_ControllerList ON dbo.adm_MethodList.ControllerId = dbo.adm_ControllerList.ControllerId ON dbo.adm_Yetki.Id = dbo.adm_YetkiMethods.YetkiId INNER JOIN
                         dbo.adm_UserYetkis INNER JOIN
                         dbo.adm_kullanicilar ON dbo.adm_UserYetkis.UserId = dbo.adm_kullanicilar.t_id ON dbo.adm_Yetki.Id = dbo.adm_UserYetkis.YetkiId


GO
/****** Object:  View [dbo].[View_Vezne]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Vezne]
AS
SELECT        dbo.hst_basvuru.t_id AS bid, dbo.hst_vezne.t_id AS Vezneid, dbo.hst_his_hareket.t_id AS hareket_id, dbo.hst_basvuru.t_tc, dbo.hst_hastakarti.t_adi, dbo.hst_hastakarti.t_soyadi, dbo.hst_basvuru.t_basvurutarihi, 
                         dbo.hst_vezne.t_odenen, dbo.hst_hizmet.t_fiyat AS BirimFiyat, dbo.hst_vezne.t_kalan, dbo.hst_vezne.t_total, dbo.hst_vezne.t_indirim, dbo.hst_vezne.t_odenecek AS OdenecekTutar, dbo.hst_odemetip.t_adi AS OdemeTipi, 
                         dbo.hst_his_hareket.t_hizmetkodu, dbo.hst_hizmet.t_adi AS Hizmet, dbo.hst_his_hareket.t_islemtarihi, dbo.hst_his_hareket.t_diskodu, dbo.hst_cene_uygunmu.t_adi AS CeneDurum, dbo.hst_his_hareket.t_parca, 
                         dbo.hst_his_hareket.t_cene, dbo.hst_his_hareket.t_yetiskinmi, dbo.hst_his_hareket.t_borcdurum, dbo.hst_vezne.t_aktif AS VezneAktif, dbo.hst_basvuru.t_aktif AS BasvuruAktif, dbo.hst_his_hareket.t_aktif AS HHareketAktif, 
                         dbo.hst_hastakarti.t_aktif AS HastaAktif
FROM            dbo.hst_hastakarti INNER JOIN
                         dbo.hst_basvuru ON dbo.hst_hastakarti.t_tc = dbo.hst_basvuru.t_tc INNER JOIN
                         dbo.hst_his_hareket ON dbo.hst_basvuru.t_id = dbo.hst_his_hareket.t_basvuruid INNER JOIN
                         dbo.hst_vezne ON dbo.hst_basvuru.t_id = dbo.hst_vezne.t_bid INNER JOIN
                         dbo.hst_odemetip ON dbo.hst_vezne.t_odemetipi = dbo.hst_odemetip.t_Id INNER JOIN
                         dbo.hst_hizmet ON dbo.hst_his_hareket.t_hizmetkodu = dbo.hst_hizmet.t_id INNER JOIN
                         dbo.hst_cene_uygunmu ON dbo.hst_his_hareket.t_cene = dbo.hst_cene_uygunmu.t_id



GO
/****** Object:  View [dbo].[View_YetkiMethods]    Script Date: 12.09.2022 09:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_YetkiMethods]
AS
SELECT        dbo.adm_YetkiMethods.YetkiId, dbo.adm_YetkiMethods.Id AS YMId, dbo.adm_Yetki.YetkiAdi, dbo.adm_Yetki.YetkiAciklama, dbo.adm_YetkiMethods.MethodId, dbo.adm_MethodList.MethodName, 
                         dbo.adm_MethodList.GorunecekIsim, dbo.adm_MethodList.ControllerId, dbo.adm_ControllerList.ControllerName, dbo.adm_Yetki.Aktif AS YetkiAktif, dbo.adm_YetkiMethods.Aktif AS YMAktif
FROM            dbo.adm_Yetki INNER JOIN
                         dbo.adm_YetkiMethods ON dbo.adm_Yetki.Id = dbo.adm_YetkiMethods.YetkiId INNER JOIN
                         dbo.adm_MethodList ON dbo.adm_YetkiMethods.MethodId = dbo.adm_MethodList.MethodId INNER JOIN
                         dbo.adm_ControllerList ON dbo.adm_MethodList.ControllerId = dbo.adm_ControllerList.ControllerId


GO
ALTER TABLE [dbo].[adm_MethodList]  WITH CHECK ADD  CONSTRAINT [FK_adm_MethodList_adm_ControllerList] FOREIGN KEY([ControllerId])
REFERENCES [dbo].[adm_ControllerList] ([ControllerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[adm_MethodList] CHECK CONSTRAINT [FK_adm_MethodList_adm_ControllerList]
GO
ALTER TABLE [dbo].[adm_pacs]  WITH CHECK ADD  CONSTRAINT [FK_adm_pacs_hst_hastakarti] FOREIGN KEY([t_tc])
REFERENCES [dbo].[hst_hastakarti] ([t_tc])
GO
ALTER TABLE [dbo].[adm_pacs] CHECK CONSTRAINT [FK_adm_pacs_hst_hastakarti]
GO
ALTER TABLE [dbo].[adm_UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_adm_UserGroups_adm_kullanicigrup1] FOREIGN KEY([GroupId])
REFERENCES [dbo].[adm_kullanicigrup] ([t_id])
GO
ALTER TABLE [dbo].[adm_UserGroups] CHECK CONSTRAINT [FK_adm_UserGroups_adm_kullanicigrup1]
GO
ALTER TABLE [dbo].[adm_UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_adm_UserGroups_adm_kullanicilar1] FOREIGN KEY([UserId])
REFERENCES [dbo].[adm_kullanicilar] ([t_id])
GO
ALTER TABLE [dbo].[adm_UserGroups] CHECK CONSTRAINT [FK_adm_UserGroups_adm_kullanicilar1]
GO
ALTER TABLE [dbo].[adm_UserYetkis]  WITH CHECK ADD  CONSTRAINT [FK_adm_UserYetkis_adm_Yetki] FOREIGN KEY([YetkiId])
REFERENCES [dbo].[adm_Yetki] ([Id])
GO
ALTER TABLE [dbo].[adm_UserYetkis] CHECK CONSTRAINT [FK_adm_UserYetkis_adm_Yetki]
GO
ALTER TABLE [dbo].[adm_UserYetkis]  WITH CHECK ADD  CONSTRAINT [FK_adm_Yetkiler_adm_kullanicilar] FOREIGN KEY([UserId])
REFERENCES [dbo].[adm_kullanicilar] ([t_id])
GO
ALTER TABLE [dbo].[adm_UserYetkis] CHECK CONSTRAINT [FK_adm_Yetkiler_adm_kullanicilar]
GO
ALTER TABLE [dbo].[adm_YetkiGroups]  WITH CHECK ADD  CONSTRAINT [FK_adm_YetkiGroups_adm_kullanicigrup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[adm_kullanicigrup] ([t_id])
GO
ALTER TABLE [dbo].[adm_YetkiGroups] CHECK CONSTRAINT [FK_adm_YetkiGroups_adm_kullanicigrup]
GO
ALTER TABLE [dbo].[adm_YetkiGroups]  WITH CHECK ADD  CONSTRAINT [FK_adm_YetkiGroups_adm_Yetki] FOREIGN KEY([YetkiId])
REFERENCES [dbo].[adm_Yetki] ([Id])
GO
ALTER TABLE [dbo].[adm_YetkiGroups] CHECK CONSTRAINT [FK_adm_YetkiGroups_adm_Yetki]
GO
ALTER TABLE [dbo].[adm_YetkiMethods]  WITH CHECK ADD  CONSTRAINT [FK_adm_YetkiMethods_adm_MethodList] FOREIGN KEY([MethodId])
REFERENCES [dbo].[adm_MethodList] ([MethodId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[adm_YetkiMethods] CHECK CONSTRAINT [FK_adm_YetkiMethods_adm_MethodList]
GO
ALTER TABLE [dbo].[adm_YetkiMethods]  WITH CHECK ADD  CONSTRAINT [FK_adm_YetkiMethods_adm_Yetki] FOREIGN KEY([YetkiId])
REFERENCES [dbo].[adm_Yetki] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[adm_YetkiMethods] CHECK CONSTRAINT [FK_adm_YetkiMethods_adm_Yetki]
GO
ALTER TABLE [dbo].[hst_basvuru]  WITH CHECK ADD  CONSTRAINT [FK_hst_basvuru_hst_bölüm] FOREIGN KEY([t_bolumkodu])
REFERENCES [dbo].[hst_bölüm] ([t_id])
GO
ALTER TABLE [dbo].[hst_basvuru] CHECK CONSTRAINT [FK_hst_basvuru_hst_bölüm]
GO
ALTER TABLE [dbo].[hst_basvuru]  WITH CHECK ADD  CONSTRAINT [FK_hst_basvuru_hst_hastakarti] FOREIGN KEY([t_tc])
REFERENCES [dbo].[hst_hastakarti] ([t_tc])
GO
ALTER TABLE [dbo].[hst_basvuru] CHECK CONSTRAINT [FK_hst_basvuru_hst_hastakarti]
GO
ALTER TABLE [dbo].[hst_disno]  WITH CHECK ADD  CONSTRAINT [FK_hst_disno_hst_cene_uygunmu] FOREIGN KEY([t_cene])
REFERENCES [dbo].[hst_cene_uygunmu] ([t_id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[hst_disno] CHECK CONSTRAINT [FK_hst_disno_hst_cene_uygunmu]
GO
ALTER TABLE [dbo].[hst_hastadurum]  WITH CHECK ADD  CONSTRAINT [FK_hst_hastadurum_hst_hastakarti] FOREIGN KEY([t_tc])
REFERENCES [dbo].[hst_hastakarti] ([t_tc])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[hst_hastadurum] CHECK CONSTRAINT [FK_hst_hastadurum_hst_hastakarti]
GO
ALTER TABLE [dbo].[hst_hastadurum]  WITH CHECK ADD  CONSTRAINT [FK_hst_hastadurum_hst_hastalik] FOREIGN KEY([t_hdurumid])
REFERENCES [dbo].[hst_hastalik] ([t_id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[hst_hastadurum] CHECK CONSTRAINT [FK_hst_hastadurum_hst_hastalik]
GO
ALTER TABLE [dbo].[hst_hastakarti]  WITH CHECK ADD  CONSTRAINT [FK_hst_hastakarti_hst_cinsiyet] FOREIGN KEY([t_cinsiyet])
REFERENCES [dbo].[hst_cinsiyet] ([t_id])
GO
ALTER TABLE [dbo].[hst_hastakarti] CHECK CONSTRAINT [FK_hst_hastakarti_hst_cinsiyet]
GO
ALTER TABLE [dbo].[hst_hastakarti]  WITH CHECK ADD  CONSTRAINT [FK_hst_hastakarti_hst_il] FOREIGN KEY([t_ilId])
REFERENCES [dbo].[hst_il] ([t_id])
GO
ALTER TABLE [dbo].[hst_hastakarti] CHECK CONSTRAINT [FK_hst_hastakarti_hst_il]
GO
ALTER TABLE [dbo].[hst_hastakarti]  WITH CHECK ADD  CONSTRAINT [FK_hst_hastakarti_hst_ilce] FOREIGN KEY([t_ilceId])
REFERENCES [dbo].[hst_ilce] ([t_id])
GO
ALTER TABLE [dbo].[hst_hastakarti] CHECK CONSTRAINT [FK_hst_hastakarti_hst_ilce]
GO
ALTER TABLE [dbo].[hst_hastakarti]  WITH CHECK ADD  CONSTRAINT [FK_hst_hastakarti_hst_medenidurum] FOREIGN KEY([t_medenidurum])
REFERENCES [dbo].[hst_medenidurum] ([t_id])
GO
ALTER TABLE [dbo].[hst_hastakarti] CHECK CONSTRAINT [FK_hst_hastakarti_hst_medenidurum]
GO
ALTER TABLE [dbo].[hst_hastakarti]  WITH CHECK ADD  CONSTRAINT [FK_hst_hastakarti_hst_ulke] FOREIGN KEY([t_ulkeId])
REFERENCES [dbo].[hst_ulke] ([CountryID])
GO
ALTER TABLE [dbo].[hst_hastakarti] CHECK CONSTRAINT [FK_hst_hastakarti_hst_ulke]
GO
ALTER TABLE [dbo].[hst_his_hareket]  WITH CHECK ADD  CONSTRAINT [FK_hst_his_hareket_hst_basvuru] FOREIGN KEY([t_basvuruid])
REFERENCES [dbo].[hst_basvuru] ([t_id])
GO
ALTER TABLE [dbo].[hst_his_hareket] CHECK CONSTRAINT [FK_hst_his_hareket_hst_basvuru]
GO
ALTER TABLE [dbo].[hst_his_hareket]  WITH CHECK ADD  CONSTRAINT [FK_hst_his_hareket_hst_cene_uygunmu] FOREIGN KEY([t_cene])
REFERENCES [dbo].[hst_cene_uygunmu] ([t_id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[hst_his_hareket] CHECK CONSTRAINT [FK_hst_his_hareket_hst_cene_uygunmu]
GO
ALTER TABLE [dbo].[hst_his_hareket]  WITH CHECK ADD  CONSTRAINT [FK_hst_his_hareket_hst_firma] FOREIGN KEY([t_firmaid])
REFERENCES [dbo].[hst_firma] ([t_id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[hst_his_hareket] CHECK CONSTRAINT [FK_hst_his_hareket_hst_firma]
GO
ALTER TABLE [dbo].[hst_his_hareket]  WITH CHECK ADD  CONSTRAINT [FK_hst_his_hareket_hst_hizmet] FOREIGN KEY([t_hizmetkodu])
REFERENCES [dbo].[hst_hizmet] ([t_id])
GO
ALTER TABLE [dbo].[hst_his_hareket] CHECK CONSTRAINT [FK_hst_his_hareket_hst_hizmet]
GO
ALTER TABLE [dbo].[hst_hizmet]  WITH CHECK ADD  CONSTRAINT [FK_hst_hizmet_hst_cene_uygunmu] FOREIGN KEY([t_ceneuygunmu])
REFERENCES [dbo].[hst_cene_uygunmu] ([t_id])
GO
ALTER TABLE [dbo].[hst_hizmet] CHECK CONSTRAINT [FK_hst_hizmet_hst_cene_uygunmu]
GO
ALTER TABLE [dbo].[hst_hizmet]  WITH CHECK ADD  CONSTRAINT [FK_hst_hizmet_hst_hizmet_parca] FOREIGN KEY([t_parcauygunmu])
REFERENCES [dbo].[hst_hizmet_parca] ([t_id])
GO
ALTER TABLE [dbo].[hst_hizmet] CHECK CONSTRAINT [FK_hst_hizmet_hst_hizmet_parca]
GO
ALTER TABLE [dbo].[hst_il]  WITH CHECK ADD  CONSTRAINT [FK_hst_il_hst_ulke] FOREIGN KEY([t_ulkeId])
REFERENCES [dbo].[hst_ulke] ([CountryID])
GO
ALTER TABLE [dbo].[hst_il] CHECK CONSTRAINT [FK_hst_il_hst_ulke]
GO
ALTER TABLE [dbo].[hst_ilce]  WITH CHECK ADD  CONSTRAINT [FK_hst_ilce_hst_il] FOREIGN KEY([t_ilId])
REFERENCES [dbo].[hst_il] ([t_id])
GO
ALTER TABLE [dbo].[hst_ilce] CHECK CONSTRAINT [FK_hst_ilce_hst_il]
GO
ALTER TABLE [dbo].[hst_marka]  WITH CHECK ADD  CONSTRAINT [FK_hst_marka_hst_firma] FOREIGN KEY([t_fid])
REFERENCES [dbo].[hst_firma] ([t_id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[hst_marka] CHECK CONSTRAINT [FK_hst_marka_hst_firma]
GO
ALTER TABLE [dbo].[hst_randevu]  WITH CHECK ADD  CONSTRAINT [FK_hst_randevu_hst_hastakarti] FOREIGN KEY([t_tc])
REFERENCES [dbo].[hst_hastakarti] ([t_tc])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[hst_randevu] CHECK CONSTRAINT [FK_hst_randevu_hst_hastakarti]
GO
ALTER TABLE [dbo].[hst_vezne]  WITH CHECK ADD  CONSTRAINT [FK_hst_vezne_hst_basvuru] FOREIGN KEY([t_bid])
REFERENCES [dbo].[hst_basvuru] ([t_id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[hst_vezne] CHECK CONSTRAINT [FK_hst_vezne_hst_basvuru]
GO
ALTER TABLE [dbo].[hst_vezne]  WITH CHECK ADD  CONSTRAINT [FK_hst_vezne_hst_OdemeTip] FOREIGN KEY([t_odemetipi])
REFERENCES [dbo].[hst_odemetip] ([t_Id])
GO
ALTER TABLE [dbo].[hst_vezne] CHECK CONSTRAINT [FK_hst_vezne_hst_OdemeTip]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:Doktor, 2: Sekreter, 3: Admin, 4: NormalKullanici' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'adm_kullanicilar', @level2type=N'COLUMN',@level2name=N't_yetki'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hastanın Çağrılıp Çağrılmadığını kontrol eder. 0=> Çağrılmadı 1=> Çağrıldı 2=>Gelmedi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hst_basvuru', @level2type=N'COLUMN',@level2name=N't_cagriekraniistem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Randevusu olduğu halde gelip gelmediğini kontrol eder True, false' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hst_randevu', @level2type=N'COLUMN',@level2name=N't_basvurudr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[26] 4[36] 2[10] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "hst_basvuru"
            Begin Extent = 
               Top = 10
               Left = 253
               Bottom = 262
               Right = 438
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_vezne"
            Begin Extent = 
               Top = 2
               Left = 519
               Bottom = 297
               Right = 689
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_odemetip"
            Begin Extent = 
               Top = 5
               Left = 761
               Bottom = 169
               Right = 931
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_hastakarti"
            Begin Extent = 
               Top = 36
               Left = 6
               Bottom = 342
               Right = 180
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 20
         Width = 284
         Width = 705
         Width = 555
         Width = 1305
         Width = 1335
         Width = 1125
         Width = 1155
         Width = 1125
         Width = 1170
         Width = 1455
         Width = 1230
         Width = 2025
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_BsvrVezne'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'      Table = 1170
         Output = 2085
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_BsvrVezne'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_BsvrVezne'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[31] 4[23] 2[6] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "hst_cene_uygunmu"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 165
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_disno"
            Begin Extent = 
               Top = 4
               Left = 563
               Bottom = 261
               Right = 733
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_CeneDis'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_CeneDis'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[48] 4[12] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "adm_kullanicigrup"
            Begin Extent = 
               Top = 12
               Left = 293
               Bottom = 183
               Right = 463
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_kullanicilar"
            Begin Extent = 
               Top = 204
               Left = 233
               Bottom = 362
               Right = 403
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_UserGroups"
            Begin Extent = 
               Top = 6
               Left = 39
               Bottom = 190
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_YetkiGroups"
            Begin Extent = 
               Top = 7
               Left = 517
               Bottom = 152
               Right = 687
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_Yetki"
            Begin Extent = 
               Top = 8
               Left = 744
               Bottom = 175
               Right = 927
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_YetkiMethods"
            Begin Extent = 
               Top = 6
               Left = 965
               Bottom = 136
               Right = 1135
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_MethodList"
            Begin Extent = 
               Top = 191
               Left = 755
               Bottom = 321
               Right = 925' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_GroupYetki'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_ControllerList"
            Begin Extent = 
               Top = 214
               Left = 1012
               Bottom = 310
               Right = 1186
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 19
         Width = 284
         Width = 1035
         Width = 1215
         Width = 1290
         Width = 1140
         Width = 1260
         Width = 1050
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1785
         Table = 2190
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_GroupYetki'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_GroupYetki'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[15] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "hst_hastadurum"
            Begin Extent = 
               Top = 9
               Left = 396
               Bottom = 273
               Right = 566
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_hastakarti"
            Begin Extent = 
               Top = 20
               Left = 97
               Bottom = 344
               Right = 271
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_hastalik"
            Begin Extent = 
               Top = 26
               Left = 727
               Bottom = 291
               Right = 897
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_HastalikDurum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_HastalikDurum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[54] 4[8] 2[7] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "hst_basvuru"
            Begin Extent = 
               Top = 0
               Left = 309
               Bottom = 284
               Right = 494
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_hastakarti"
            Begin Extent = 
               Top = 4
               Left = 19
               Bottom = 358
               Right = 199
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_his_hareket"
            Begin Extent = 
               Top = 7
               Left = 598
               Bottom = 272
               Right = 768
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "hst_hizmet"
            Begin Extent = 
               Top = 5
               Left = 884
               Bottom = 295
               Right = 1063
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_bölüm"
            Begin Extent = 
               Top = 283
               Left = 567
               Bottom = 439
               Right = 737
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_cene_uygunmu"
            Begin Extent = 
               Top = 252
               Left = 1062
               Bottom = 382
               Right = 1232
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 20
         Width = 284
         Width = 15' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_HizHareket'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'00
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2130
         Table = 1770
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_HizHareket'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_HizHareket'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[59] 4[16] 2[3] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "hst_hastakarti"
            Begin Extent = 
               Top = 7
               Left = 49
               Bottom = 358
               Right = 223
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_basvuru"
            Begin Extent = 
               Top = 6
               Left = 261
               Bottom = 263
               Right = 446
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "hst_his_hareket"
            Begin Extent = 
               Top = 8
               Left = 506
               Bottom = 291
               Right = 676
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "hst_hizmet"
            Begin Extent = 
               Top = 6
               Left = 772
               Bottom = 211
               Right = 951
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_cene_uygunmu"
            Begin Extent = 
               Top = 218
               Left = 724
               Bottom = 373
               Right = 894
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_bölüm"
            Begin Extent = 
               Top = 369
               Left = 60
               Bottom = 531
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_firma"
            Begin Extent = 
               Top = 339
               Left = 297
               Bottom = 509
               Right = 467
            End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_HizmetDetay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_marka"
            Begin Extent = 
               Top = 380
               Left = 610
               Bottom = 546
               Right = 780
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 27
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2145
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_HizmetDetay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_HizmetDetay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "adm_pacs"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 211
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_cinsiyet"
            Begin Extent = 
               Top = 27
               Left = 792
               Bottom = 123
               Right = 962
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_hastakarti"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 211
               Right = 628
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Pacs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Pacs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[15] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "adm_kullanicigrup"
            Begin Extent = 
               Top = 61
               Left = 857
               Bottom = 191
               Right = 1027
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_UserGroups"
            Begin Extent = 
               Top = 0
               Left = 439
               Bottom = 139
               Right = 610
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_kullanicilar"
            Begin Extent = 
               Top = 48
               Left = 22
               Bottom = 308
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Users'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Users'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[49] 4[16] 2[8] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "adm_Yetki"
            Begin Extent = 
               Top = 5
               Left = 981
               Bottom = 171
               Right = 1164
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_MethodList"
            Begin Extent = 
               Top = 16
               Left = 89
               Bottom = 161
               Right = 264
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_YetkiMethods"
            Begin Extent = 
               Top = 9
               Left = 438
               Bottom = 140
               Right = 608
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_ControllerList"
            Begin Extent = 
               Top = 212
               Left = 72
               Bottom = 324
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_UserYetkis"
            Begin Extent = 
               Top = 166
               Left = 745
               Bottom = 324
               Right = 915
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_kullanicilar"
            Begin Extent = 
               Top = 155
               Left = 434
               Bottom = 394
               Right = 604
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 17
         Width = 284
      ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_UserYetkis'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'   Width = 975
         Width = 735
         Width = 1065
         Width = 1500
         Width = 1500
         Width = 1740
         Width = 2505
         Width = 2325
         Width = 1590
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2625
         Alias = 900
         Table = 1725
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_UserYetkis'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_UserYetkis'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[46] 4[13] 2[9] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "hst_hastakarti"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 355
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_basvuru"
            Begin Extent = 
               Top = 0
               Left = 323
               Bottom = 260
               Right = 508
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_his_hareket"
            Begin Extent = 
               Top = 0
               Left = 747
               Bottom = 323
               Right = 917
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_vezne"
            Begin Extent = 
               Top = 124
               Left = 541
               Bottom = 373
               Right = 711
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_odemetip"
            Begin Extent = 
               Top = 266
               Left = 230
               Bottom = 371
               Right = 400
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_hizmet"
            Begin Extent = 
               Top = 7
               Left = 1056
               Bottom = 227
               Right = 1235
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "hst_cene_uygunmu"
            Begin Extent = 
               Top = 239
               Left = 953
               Bottom = 389
               Right = 1123
          ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Vezne'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 29
         Width = 284
         Width = 840
         Width = 750
         Width = 1155
         Width = 855
         Width = 1020
         Width = 1005
         Width = 945
         Width = 1095
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1485
         Width = 660
         Width = 1500
         Width = 1485
         Width = 1005
         Width = 660
         Width = 1245
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1380
         Table = 2025
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Vezne'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Vezne'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[36] 4[16] 2[8] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "adm_Yetki"
            Begin Extent = 
               Top = 16
               Left = 589
               Bottom = 187
               Right = 759
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_YetkiMethods"
            Begin Extent = 
               Top = 22
               Left = 250
               Bottom = 192
               Right = 420
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_MethodList"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 152
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "adm_ControllerList"
            Begin Extent = 
               Top = 216
               Left = 15
               Bottom = 333
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1500
         Width = 1950
         Width = 1920
         Width = 915
         Width = 2220
         Width = 3360
         Width = 1500
         Width = 1590
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_YetkiMethods'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_YetkiMethods'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_YetkiMethods'
GO
USE [master]
GO
ALTER DATABASE [WEINCDENTAL] SET  READ_WRITE 
GO
