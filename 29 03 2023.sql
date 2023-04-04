USE [master]
GO
/****** Object:  Database [VzWeb]    Script Date: 3/29/2023 2:18:11 PM ******/
CREATE DATABASE [VzWeb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VzWeb', FILENAME = N'D:\Microsoft SQL Server\VzWeb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VzWeb_log', FILENAME = N'D:\Microsoft SQL Server\VzWeb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VzWeb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VzWeb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VzWeb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VzWeb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VzWeb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VzWeb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VzWeb] SET ARITHABORT OFF 
GO
ALTER DATABASE [VzWeb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VzWeb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VzWeb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VzWeb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VzWeb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VzWeb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VzWeb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VzWeb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VzWeb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VzWeb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [VzWeb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VzWeb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VzWeb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VzWeb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VzWeb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VzWeb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VzWeb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VzWeb] SET RECOVERY FULL 
GO
ALTER DATABASE [VzWeb] SET  MULTI_USER 
GO
ALTER DATABASE [VzWeb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VzWeb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VzWeb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VzWeb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VzWeb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VzWeb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'VzWeb', N'ON'
GO
ALTER DATABASE [VzWeb] SET QUERY_STORE = OFF
GO
USE [VzWeb]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 3/29/2023 2:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[MaSP] [char](10) NOT NULL,
	[SoluongMua] [int] NULL,
	[MaKH] [char](50) NOT NULL,
	[Gia] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC,
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHUC_VU]    Script Date: 3/29/2023 2:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHUC_VU](
	[MaCV] [char](10) NOT NULL,
	[ChucVu] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CT_HD]    Script Date: 3/29/2023 2:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CT_HD](
	[MaHÐ] [int] NOT NULL,
	[MaSP] [char](10) NOT NULL,
	[SoLuong] [int] NULL,
	[TongGia] [decimal](18, 0) NULL,
	[NgayMua] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHÐ] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOA_DON]    Script Date: 3/29/2023 2:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOA_DON](
	[MaHÐ] [int] IDENTITY(1,1) NOT NULL,
	[NgayDat] [date] NULL,
	[NgayGiao] [date] NULL,
	[MaNV] [char](50) NULL,
	[GiaoHang] [bit] NULL,
	[ThanhToan] [bit] NULL,
	[MaKH] [char](50) NOT NULL,
	[TongTienTT] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHÐ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHACH_HANG]    Script Date: 3/29/2023 2:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACH_HANG](
	[MaKH] [char](50) NOT NULL,
	[TenDangNhapTK] [char](30) NOT NULL,
	[HoTenKH] [nvarchar](50) NULL,
	[SDT] [char](10) NULL,
	[DiaChi] [nvarchar](250) NULL,
	[Email] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loai_SP]    Script Date: 3/29/2023 2:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loai_SP](
	[Ma_Loai] [int] IDENTITY(1,1) NOT NULL,
	[TenLoai] [nvarchar](50) NULL,
 CONSTRAINT [PK__Loai_SP__586312F9D449DE67] PRIMARY KEY CLUSTERED 
(
	[Ma_Loai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHAN_VIEN]    Script Date: 3/29/2023 2:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHAN_VIEN](
	[MaNV] [char](50) NOT NULL,
	[TenDangNhapTK] [char](30) NOT NULL,
	[MaCV] [char](10) NOT NULL,
	[HoTen] [nvarchar](50) NULL,
	[SDT] [char](10) NULL,
	[DiaChi] [nvarchar](250) NULL,
	[Email] [nvarchar](40) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhanLoai]    Script Date: 3/29/2023 2:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhanLoai](
	[MaPhanLoai] [int] IDENTITY(1,1) NOT NULL,
	[TenPhanLoai] [nvarchar](50) NULL,
 CONSTRAINT [PK__PhanLoai__E8C0182E5D2A4F07] PRIMARY KEY CLUSTERED 
(
	[MaPhanLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SAN_PHAM]    Script Date: 3/29/2023 2:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SAN_PHAM](
	[MaSP] [char](10) NOT NULL,
	[Ma_Loai] [int] NOT NULL,
	[MaPhanLoai] [int] NOT NULL,
	[TenSP] [nvarchar](70) NULL,
	[SoLuong] [int] NULL,
	[Gia] [decimal](18, 0) NULL,
	[Ma_TH] [int] NOT NULL,
	[GioiThieuSP] [nvarchar](500) NULL,
	[HinhAnhSP] [nvarchar](200) NULL,
	[HinhAnhCT1] [nvarchar](200) NULL,
	[HinhAnhCT2] [nvarchar](200) NULL,
	[HinhAnhCT3] [nvarchar](200) NULL,
	[HinhAnhCT4] [nvarchar](200) NULL,
	[SLTruyCap] [int] NULL,
 CONSTRAINT [PK__SAN_PHAM__2725081CAADB15DB] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAI_KHOAN]    Script Date: 3/29/2023 2:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAI_KHOAN](
	[TenDangNhapTK] [char](30) NOT NULL,
	[MatKhau] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TenDangNhapTK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[THUONG_HIEU]    Script Date: 3/29/2023 2:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[THUONG_HIEU](
	[Ma_TH] [int] IDENTITY(1,1) NOT NULL,
	[TenTH] [nvarchar](250) NULL,
 CONSTRAINT [PK__THUONG_H__2E62FB7FC1FA4068] PRIMARY KEY CLUSTERED 
(
	[Ma_TH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD FOREIGN KEY([MaKH])
REFERENCES [dbo].[KHACH_HANG] ([MaKH])
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [FK__cart__MaSP__38996AB5] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SAN_PHAM] ([MaSP])
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [FK__cart__MaSP__38996AB5]
GO
ALTER TABLE [dbo].[CT_HD]  WITH CHECK ADD FOREIGN KEY([MaHÐ])
REFERENCES [dbo].[HOA_DON] ([MaHÐ])
GO
ALTER TABLE [dbo].[CT_HD]  WITH CHECK ADD  CONSTRAINT [FK__CT_HD__MaSP__3A81B327] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SAN_PHAM] ([MaSP])
GO
ALTER TABLE [dbo].[CT_HD] CHECK CONSTRAINT [FK__CT_HD__MaSP__3A81B327]
GO
ALTER TABLE [dbo].[HOA_DON]  WITH CHECK ADD FOREIGN KEY([MaKH])
REFERENCES [dbo].[KHACH_HANG] ([MaKH])
GO
ALTER TABLE [dbo].[HOA_DON]  WITH CHECK ADD FOREIGN KEY([MaNV])
REFERENCES [dbo].[NHAN_VIEN] ([MaNV])
GO
ALTER TABLE [dbo].[KHACH_HANG]  WITH CHECK ADD FOREIGN KEY([TenDangNhapTK])
REFERENCES [dbo].[TAI_KHOAN] ([TenDangNhapTK])
GO
ALTER TABLE [dbo].[NHAN_VIEN]  WITH CHECK ADD FOREIGN KEY([MaCV])
REFERENCES [dbo].[CHUC_VU] ([MaCV])
GO
ALTER TABLE [dbo].[NHAN_VIEN]  WITH CHECK ADD FOREIGN KEY([TenDangNhapTK])
REFERENCES [dbo].[TAI_KHOAN] ([TenDangNhapTK])
GO
ALTER TABLE [dbo].[SAN_PHAM]  WITH CHECK ADD  CONSTRAINT [FK__SAN_PHAM__Ma_Loa__403A8C7D] FOREIGN KEY([Ma_Loai])
REFERENCES [dbo].[Loai_SP] ([Ma_Loai])
GO
ALTER TABLE [dbo].[SAN_PHAM] CHECK CONSTRAINT [FK__SAN_PHAM__Ma_Loa__403A8C7D]
GO
ALTER TABLE [dbo].[SAN_PHAM]  WITH CHECK ADD  CONSTRAINT [FK__SAN_PHAM__Ma_TH__412EB0B6] FOREIGN KEY([Ma_TH])
REFERENCES [dbo].[THUONG_HIEU] ([Ma_TH])
GO
ALTER TABLE [dbo].[SAN_PHAM] CHECK CONSTRAINT [FK__SAN_PHAM__Ma_TH__412EB0B6]
GO
ALTER TABLE [dbo].[SAN_PHAM]  WITH CHECK ADD  CONSTRAINT [FK_SAN_PHAM_PhanLoai] FOREIGN KEY([MaPhanLoai])
REFERENCES [dbo].[PhanLoai] ([MaPhanLoai])
GO
ALTER TABLE [dbo].[SAN_PHAM] CHECK CONSTRAINT [FK_SAN_PHAM_PhanLoai]
GO
USE [master]
GO
ALTER DATABASE [VzWeb] SET  READ_WRITE 
GO
