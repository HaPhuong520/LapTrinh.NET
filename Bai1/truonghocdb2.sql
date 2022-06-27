use master
go
if DB_ID('truonghocdb2') is not null
	drop database truonghocdb2
go
create database truonghocdb2
go
USE [truonghocdb2]
GO
/****** Object:  Table [dbo].[lophoc]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lophoc](
	[malop] [int] IDENTITY(1,1) NOT NULL,
	[tenlop] [nvarchar](30) NULL,
	[giangvien] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[malop] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[lophoc] ON
INSERT [dbo].[lophoc] ([malop], [tenlop], [giangvien]) VALUES (1, N'Khoa học máy tính', N'Hoàng Văn Long')
INSERT [dbo].[lophoc] ([malop], [tenlop], [giangvien]) VALUES (2, N'Công nghệ thông tin', N'Phan Tuyết Nga')
INSERT [dbo].[lophoc] ([malop], [tenlop], [giangvien]) VALUES (3, N'Kỹ thuật phần mềm', N'Nguyễn Thanh Tú')
INSERT [dbo].[lophoc] ([malop], [tenlop], [giangvien]) VALUES (4, N'Hệ thống thông tin', N'Trần Thị Mai')
SET IDENTITY_INSERT [dbo].[lophoc] OFF
/****** Object:  Table [dbo].[sinhvien]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sinhvien](
	[masv] [int] PRIMARY KEY NOT NULL,
	[hoten] [nvarchar](30) NULL,
	[diachi] [nvarchar](30) NULL,
	[diem] [tinyint] NULL,
	[malop] [int] NULL,
	[anh] [nvarchar](255) NULL
	)
GO
ALTER TABLE [dbo].[sinhvien]  WITH CHECK ADD  CONSTRAINT [FK_malop] FOREIGN KEY([malop])
REFERENCES [dbo].[lophoc] ([malop])
GO
ALTER TABLE [dbo].[sinhvien] CHECK CONSTRAINT [FK_malop]
GO

 
INSERT [dbo].[sinhvien] ([masv], [hoten], [diachi], [diem], [malop], [anh]) VALUES (1, N'Duy Quang', N'Hà nội', 8, 2, N'a1.jpg')
INSERT [dbo].[sinhvien] ([masv], [hoten], [diachi], [diem], [malop], [anh]) VALUES (2, N'Việt Phương', N'Thái Bình',7, 1, N'a2.jpg')
INSERT [dbo].[sinhvien] ([masv], [hoten], [diachi], [diem], [malop], [anh]) VALUES (3, N'Huyền Trang', N'Hải phòng',6, 2, N'a3.jpg')
INSERT [dbo].[sinhvien] ([masv], [hoten], [diachi], [diem], [malop], [anh]) VALUES (4, N'Đức Vinh', N'Quảng ninh', 8, 3, N'a4.jpg')
INSERT [dbo].[sinhvien] ([masv], [hoten], [diachi], [diem], [malop], [anh]) VALUES (5, N'Mai Hoa', N'Hà nội', 5, 4, N'a5.jpg')
INSERT [dbo].[sinhvien] ([masv], [hoten], [diachi], [diem], [malop], [anh]) VALUES (6, N'Văn Hoàng', N'Hà nội',9, 4, N'a6.jpg')
 
go
select * from sinhvien
select * from lophoc

