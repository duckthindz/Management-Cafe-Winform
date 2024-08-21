select *from HOADON
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
update HOADON
set tongTien=10000
where maHD='HD02'
update HOADON
set tongTien=16500
where maHD='HD03'
update LOAISANPHAM
set moTa=N'Bánh ăn dậm đậm đà hương vị'
where maLSP='BN'
select tenSP,giaSP,maLSP from SANPHAM where maLSP = 'BN'
go
select *from KHACHHANG where ((HoTenKH LIKE '%H%') or sdtKH='sds') 
delete from KHACHHANG where maKH='KH11' or maKH='KH12'
go
select *from HOADON
insert into HOADON
values('HD05','NV04','...','BC02','08-11-2023','','','',12)
select maKH,HoTenKH,sdtKH,diaChiKH,diemTL from KHACHHANG
select HoTenNV from NHANVIEN where maNV='NV04'
select maHD,maNV,ngayLapHD,tongTien, HOADON.maKH from HOADON where maKH='KH03'
go
delete from HOADON where maHD='HD05'
ALTER TABLE HOADON
DROP CONSTRAINT FK_DONHANG_KHACHHANG;

alter table HOADON
add CONSTRAINT FK_DONHANG_KHACHHANG FOREIGN KEY (maKH) REFERENCES KHACHHANG(maKH),

set DATEFORMAT DMY
select ngayLapHD, CTHD.maHD,maNV,SUM(soLuong) as soLuong,tenSP,CTHD.maSP, tongTien,giamGia from CHITIET_HOADON as CTHD,HOADON as HD,SANPHAM as SP 
where CTHD.maHD=HD.maHD and CTHD.maSP=SP.maSP 
GROUP BY ngayLapHD, CTHD.maHD,maNV,soLuong,tenSP,CTHD.maSP, tongTien,giamGia
ORDER BY soLuong DESC

insert into CHITIET_HOADON
values('HD04','SPTU02',4,N'')
delete from CHITIET_HOADON where maHD='HD05'
go
select *from BANCF
update BANCF
set thuocTinh=0
where maBan='BC01'
go
select * from BANCF where thuocTinh='1' and maKV='KV03'
select * from BANCF where thuocTinh='1'
select * from NHANVIEN where maNV='NV02'

update NHANVIEN
set matKhauNV ='@123'
where maNV='NV01'

set DATEFORMAT DMY
select ngayLapHD, CTHD.maHD,maNV,soLuong,tenSP,CTHD.maSP, tongTien,giamGia from CHITIET_HOADON as CTHD,HOADON as HD,SANPHAM as SP
where CTHD.maHD=HD.maHD and CTHD.maSP=SP.maSP and '10-09-2023'<= ngayLapHD and ngayLapHD <= '12-09-2023'
select *from HOADON

select ngayLapHD,HD.maHD,tenBan,HoTenNV,HoTenKH,HD.diemTL,tenSP,soLuong,giaSP,giamGia,chiPhiKhac,tongTien from HOADON as HD,CHITIET_HOADON as CTHD,KHACHHANG as KH, SANPHAM as SP,BANCF as Ban, NHANVIEN as NV
where CTHD.maSP=SP.maSP and CTHD.maHD=HD.maHD and HD.maBan=Ban.maBan and HD.maKH=KH.maKH and HD.maNV=NV.maNV
go
set DATEFORMAT DMY
select ngayLapHD, CTHD.maHD,maNV,SUM(soLuong)as soLuong,tenSP,CTHD.maSP, tongTien,giamGia from CHITIET_HOADON as CTHD,HOADON as HD,SANPHAM as SP 
where CTHD.maHD=HD.maHD and CTHD.maSP=SP.maSP 
GROUP BY ngayLapHD, CTHD.maHD,maNV,soLuong,tenSP,CTHD.maSP, tongTien,giamGia
ORDER BY soLuong DESC
			/*Chỉnh lại thành diểm trừ tích luỹ ở Table hoá Đơn, update diểm tích luỹ cho khách hàng khi bấm vào btnINHoaDon*/
select tenSP,SUM(soLuong) as SL from CHITIET_HOADON,SANPHAM,HOADON
where CHITIET_HOADON.maSP=SANPHAM.maSP and CHITIET_HOADON.maHD=HOADON.maHD and '10-09-2023'<= ngayLapHD and ngayLapHD <= '12-09-2023'
group by tenSP
ORDER BY SUM(soLuong) DESC
select *from HOADON
go
select *from KHACHHANG
select *from CHITIET_HOADON
delete from CHITIET_HOADON
where maHD='HD07'
delete from HOADON
where maHD='HD07'

select *from HOADON