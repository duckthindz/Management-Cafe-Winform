---TẠO DATABASE
CREATE DATABASE QL_QUANCAPHE
GO
USE QL_QUANCAPHE
GO

---TẠO TABLE
create table admin
(
	userName nchar(22) primary key not null,
	PassWord nchar(15),
	email nvarchar(100),
)
go
create table LOAISANPHAM
(
	maLSP char(12) not null primary key,
	tenL nvarchar(50),
	moTa nvarchar(200)
)
go
create table SANPHAM
(
	maSP char(12) not null primary key,
	maLSP char(12),
	tenSP nvarchar(50),
	phoBienSP nvarchar(100) ,
	hinhAnhSP varbinary(MAX),
	giaSP float,
	constraint FK_SANPHAM_LOAISANPHAM foreign key (maLSP) REFERENCES LOAISANPHAM(maLSP),
)
go

create table NHANVIEN
(
	maNV char(12) not null primary key,
	HoTenNV nvarchar(50),
	namSinhNV date,
	diaChiNV nvarchar(200),
	emailNV nvarchar(100),
	gioiTinhNV nvarchar(15),
	sdtNV nvarchar(16),
	chucVu nvarchar(50),
	ngayVaoLamNV  datetime,
	matKhauNV nvarchar(32),
	sumTGLV int,
	luongThuong int,
	thanhTien float,
	kyHanTraLuong nvarchar(10),
)

ALTER TABLE NHANVIEN
DROP COLUMN ngayVaoLamNV,sumTGLV,luongThuong,thanhTien,kyHanTraLuong;
Go
create table KHACHHANG
(
	maKH char(12) not null primary key,
	HoTenKH nvarchar(50),
	namSinhKH date,
	diaChiKH nvarchar(200),
	emailKH nvarchar(100),
	gioiTinhKH nvarchar(15),
	sdtKH nvarchar(16),
	diemTL int,
	LoaiKH bit not null
)
go
create table KHUVUC
(	
	maKV nchar(12) not null primary key,
	tenKV nvarchar(50),
	trangThai bit not null
)
go
create table BANCF
(
	maBan nchar(12) not null primary key,
	maKV nchar(12),
	tenBan nvarchar(50),
	thuocTinh bit not null,
	CONSTRAINT FK_BANCF_KHUVUC FOREIGN KEY (maKV) REFERENCES KHUVUC(maKV)
)
go
create table HOADON
(
	maHD char(12) not null primary key,
	maNV char(12),
	maKH char(12),
	maBan nchar(12),
	ngayLapHD datetime,
	diemTL int,
	giamGia float,
	chiPhiKhac float,
	tongTien float,
	CONSTRAINT FK_DONHANG_NHANVIEN FOREIGN KEY (maNV) REFERENCES NHANVIEN(maNV),
	CONSTRAINT FK_DONHANG_KHACHHANG FOREIGN KEY (maKH) REFERENCES KHACHHANG(maKH),
	CONSTRAINT FK_DONHANG_BANCF FOREIGN KEY (maBan) REFERENCES BANCF(maBan),
)
go
create table CHITIET_HOADON
(
  maHD char(12),
  maSP char(12),
  soLuong int,
  gopY nvarchar(50),
  CONSTRAINT PK_DIEM PRIMARY KEY(maHD,maSP),
  CONSTRAINT FK_CHITIET_HOADON_HOADON FOREIGN KEY (maHD) REFERENCES HOADON(maHD),
  CONSTRAINT FK_CHITIET_HOADON_SANPHAM FOREIGN KEY (maSP) REFERENCES SANPHAM(maSP),
)
go

---ĐIỀN DỮ LIỆU VÀO TABLE
set DATEFORMAT DMY
insert into NHANVIEN 
values('NV01',N'Thái Hưng Thịnh','16-12-2003',N'Quy Nhơn',N'hungthinh123@gmail.com',N'Nam','',N'Thu Ngân','@123'),
	('NV02',N'Trần Đức Thịnh','14-12-2003',N'Bình Thuận',N'ducthinh1412@gmail.com',N'Nam','',N'admin','sa123'),
	('NV03',N'Đinh Trường Giang','01-02-2003',N'Hòa Bình',N'truonggian11@gmail.com',N'Nam','',N'Phục Vụ','@123'),
	('NV04',N'Đặng Quốc Dũng','02-01-2003',N'TPHCM',N'quocdung22@gmail.com',N'Nam','',N'Pha Chế','@123')
go
insert into admin
values('HungThinh77','HThinh123','hungthinh123@gmail.com'),
	  ('DucThinh86','DThinh123','ducthinh1412@gmail.com'),
	  ('TruongGiang28','TGiang123','truonggian11@gmail.com'),
	  ('DangDung59','DDung123','quocdung22@gmail.com')
	  go
insert into KHUVUC
values('KV01',N'Couple',1),
	('KV02',N'Sân Vườn',1),
	('KV03',N'Phòng Lạnh',0),
	('KV04',N'Phòng VIP',0)
go
insert into BANCF
values('BC01','KV01',N'Couple01',1),
	('BC02','KV01',N'Couple02',1),
	('BC03','KV01',N'Couple03',0),
	('BC04','KV01',N'Couple04',0),
	('BS01','KV02',N'Sân Vườn 1',1),
	('BS02','KV02',N'Sân Vườn 2',1),
	('BS03','KV02',N'Sân Vườn 3',0),
	('BL01','KV03',N'Phòng Lạnh 1',1),
	('BL02','KV03',N'Phòng Lạnh 2',1),
	('BL03','KV03',N'Phòng Lạnh 3',0),
	('BV01','KV04',N'Phòng VIP 1',1),
	('BV02','KV04',N'Phòng VIP 2',1),
	('BV03','KV04',N'Phòng VIP 3',0)
go
insert into LOAISANPHAM
values('TU',N'Thức Uống',N'Thức uống giải khác và thưởng thức'),
	('BN',N'Bánh Ngọt',N'Bánh để ăn dặm đậm đà')
go
insert into SANPHAM
	values('SPTU01','TU',N'Cafe Sửa',N'Cao',(select *from openrowset(bulk 'D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\CafeSua.jpg', single_blob) as image),25000),
	('SPTU02','TU',N'Cafe đen đá',N'Thấp',(select *from openrowset(bulk 'D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\CfDen.png', single_blob) as image),20000),
	('SPTU03','TU',N'Nước Cam',N'Cao',(select *from openrowset(bulk 'D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\nuocCam.png', single_blob) as image),30000),
	('SPTU04','TU',N'Trà Sửa',N'Cao',(select *from openrowset(bulk 'D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\traSua.png', single_blob) as image),35000),
	('SPTU05','TU',N'Matcha Đá Xây',N'TB',(select *from openrowset(bulk 'D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\matCha.png', single_blob) as image),35000),
	('SPBN01','BN',N'Bánh Donut ',N'TB',(select *from openrowset(bulk 'D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\banhDonut.png', single_blob) as image),25000),
	('SPBN02','BN',N'Bánh Tiramisu',N'cao',(select *from openrowset(bulk 'D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\banhTiramesu.png', single_blob) as image),30000),
	('SPBN03','BN',N'Bánh Mochi',N'Thấp',(select *from openrowset(bulk 'D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\banhMochi.png', single_blob) as image),2000)
	go
select *from SANPHAM
go
insert into KHACHHANG
values('KH01',N'Vũ Hoàng Phúc','18-9-2003',N'TP.HCM',N'hoangphucdz123@gmail.com',N'Nam',N'0977032612',50,0),
	   ('KH02',N'Vũ Ngọc My','21-7-2003',N'Hòa Bình',N'myngovu28@gmail.com',N'Nữ',N'0923432423',60,0),
	   ('KH03',N'Đinh Ngọc Hân','26-1-1999',N'Hà Nội',N'hanphepha88@gmail.com',N'Nam',N'0973242523',30,0),
	   ('KH04',N'Vũ Hoàng Đăng','27-9-2005',N'Hải PHòng',N'hoangdangcypher@gmail.com',N'Nam',N'0997997129',70,0),
	   ('KH05',N'Nguyễn Ánh Tuyết','28-11-2003',N'Vĩnh Phúc',N'nguyentuyet09@gmail.com',N'Nữ',N'0923242362',20,0),
	   ('KH06',N'Gdragon','18-9-2003',N'Hàn Quốc',N'gdragon88@gmail.com',N'Nam',N'098888888',100,1),
	   ('KH08',N'Leo Messi','24-6-1987',N'Argentina',N'leomessi10@gmail.com',N'Nam',N'0837297338',100,1),
	   ('KH09',N'Vũ Văn Vinh','1-1-2003',N'TP.HCM',N'vinhdc11@gmail.com',N'Nam',N'088739379',100,1),
	   ('KH10',N'Đinh Ánh Ngọc','23-3-2003',N'Thái Bình',N'anhngoc231@gmail.com',N'Nữ',N'0345523433',40,0)
	   go
insert into HOADON
values('HD01','NV01','KH01','BV01','15-09-2023',50,0,0,200000),
	   ('HD02','NV01','KH03','BS01','17-09-2023',30,0,0,100000),
	   ('HD03','NV01','KH10','BV03','21-09-2023',100,30000,20000,300000),
	   ('HD04','NV01','KH05','BC01','11-09-2023',20,0,15000,200000),
	   ('HD05','NV01','KH04','BS02','14-08-2023',70,0,30000,250000),
	   ('HD06','NV01','KH02','BS03','25-08-2023',60,0,0,100000),
	   ('HD07','NV01','KH06','BV02','15-09-2023',100,30000,50000,500000)
	go
select * from HOADON
go
insert into CHITIET_HOADON
values('HD01','SPTU01',2,N'ít sửa'),
	('HD01','SPBN02',2,N''),
	('HD02','SPTU02',1,N'ít đường'),
	('HD02','SPTU03',1,N'nhiều đường'),
	('HD02','SPBN01',2,N'cho muỗn'),
	('HD03','SPTU04',3,N'30% đá'),
	('HD03','SPBN03',4,N'ít sửa'),
	('HD04','SPTU05',2,N''),
	('HD04','SPTU01',1,N'nhiều sửa'),
	('HD04','SPBN02',5,'')
go
