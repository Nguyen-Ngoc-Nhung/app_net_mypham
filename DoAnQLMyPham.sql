USE master
IF EXISTS(SELECT * FROM sys.databases WHERE name='MYPHAM')
BEGIN
        DROP DATABASE MYPHAM
END
CREATE DATABASE MYPHAM
GO
USE MYPHAM
GO

CREATE TABLE QUYEN 
(
    MAQUYEN INT IDENTITY NOT NULL,
    TENQUYEN NVARCHAR(50), -- TÊN CÁC NHÓM QUYỀN(ADIMIN,NHÂN VIÊN)
    CONSTRAINT PK_QUYEN PRIMARY KEY (MAQUYEN) --KHÓA CHÍNH
)
CREATE TABLE TAIKHOAN 
(
    MATK CHAR(10) NOT NULL, 
    TENTK VARCHAR(50),
	MATKHAU CHAR(50),
    MAQUYEN INT ,
	--KHÓA CHÍNH
	CONSTRAINT PK_TAIKHOAN PRIMARY KEY (MATK),
	--KHÓA NGOẠI
	CONSTRAINT FK_TK_QUYEN FOREIGN KEY(MAQUYEN) REFERENCES QUYEN(MAQUYEN)
)
CREATE TABLE THONGTINTAIKHOAN 
(
	MATK CHAR(10) NOT NULL, 
    HOTEN NVARCHAR(50), -- HỌ TÊN
    NGSINH DATE, -- NGÀY SINH
    GTINH NVARCHAR(3),
    NGTAO DATETIME, -- NGÀY TẠO
    EMAIL VARCHAR(50), -- ĐỊA CHỈ EMAIL
    SDT VARCHAR(11), -- SDT
    DCHI NVARCHAR(50), -- ĐỊA CHỈ NHÀ
	TINHTRANG NVARCHAR(50),---ĐANG LÀM ,NGHỈ VIỆC
	--KHÓA CHÍNH
    CONSTRAINT PK_TTTK PRIMARY KEY (MATK),
	--KHÓA NGOẠI
	CONSTRAINT FK_TTTK_TAIKHOAN FOREIGN KEY(MATK) REFERENCES TAIKHOAN(MATK)
)
CREATE TABLE KHACHHANG 
(
    MAKH VARCHAR(11) NOT NULL, -- sdt 
    DIEMTICHLUY INT,
    CONSTRAINT PK_KH PRIMARY KEY (MAKH)
)
CREATE TABLE NHANVIEN 
(
    MANV VARCHAR(10) NOT NULL,
    MATK CHAR(10) NOT NULL, 
    CONSTRAINT PK_NV PRIMARY KEY (MANV),
    CONSTRAINT FK_NV_TK FOREIGN KEY (MATK) REFERENCES TAIKHOAN(MATK)
)
CREATE TABLE LOAISP 
(
    MALSP VARCHAR(6) NOT NULL,
    TENLOAI NVARCHAR(50),
    CONSTRAINT PK_LSP PRIMARY KEY (MALSP)
)
CREATE TABLE DANHMUC 
(
    MADM INT IDENTITY NOT NULL,
    TENDANHMUC NVARCHAR(50),
    CONSTRAINT PK_DMUC PRIMARY KEY (MADM)
)
CREATE TABLE CHITIETDANHMUC 
(
	MADM INT NOT NULL REFERENCES DANHMUC(MADM),
	MALSP VARCHAR(6) NOT NULL REFERENCES LOAISP(MALSP),
	CONSTRAINT PK_CTDM PRIMARY KEY (MADM, MALSP)
)
CREATE TABLE SANPHAM 
(
    MASP INT IDENTITY NOT NULL, -- CREATE AUTO
    TENSP NVARCHAR(MAX), -- TÊN SẢN PHẨM
    MOTA NVARCHAR(MAX), -- MÔ TẢ
    SOLUONG INT, -- SỐ LƯỢNG TỒN KHO
    DONGIA FLOAT, -- ĐƠN GIÁ
    NSX NVARCHAR(30), -- NHÀ SẢN XUẤT
    MALSP VARCHAR(6) REFERENCES LOAISP(MALSP),
    CONSTRAINT PK_SP PRIMARY KEY (MASP)
)
CREATE TABLE HOADON 
(
    MAHD INT IDENTITY NOT NULL, -- CREATE AUTO
    NGTAO DATETIME, -- NGÀY TẠO HÓA ĐƠN
    THANHTOAN FLOAT, -- TỔNG (SỐ LƯỢNG * ĐƠN GIÁ)
    TINHTRANG NVARCHAR(50), -- CHƯA THANHTOAN,DATT
    MAKH VARCHAR(11) REFERENCES KHACHHANG(MAKH),
    MANV VARCHAR(10) REFERENCES NHANVIEN(MANV),
    CONSTRAINT PK_HD PRIMARY KEY (MAHD)
)
CREATE TABLE CHITIETHD 
(
    MAHD INT REFERENCES HOADON(MAHD),
    MASP INT REFERENCES SANPHAM(MASP),
    SOLUONG INT, -- SỐ LƯỢNG > 0, số lượng bán
    CONSTRAINT PK_CTHD PRIMARY KEY (MAHD,MASP) 
)

----------------------------------------------------------------------------
--RÀNG BUỘC TOÀN VẸN
----------------------------------------------------------------------------
SET DATEFORMAT DMY

--TABLE TAIKHOAN
ALTER TABLE TAIKHOAN ADD CONSTRAINT uni_TenLoaiTK UNIQUE(TENTK)
GO

--TABLE THONGTINTAIKHOAN
ALTER TABLE THONGTINTAIKHOAN ADD CONSTRAINT chk_GioiTinh CHECK(GTINH IN (N'Nam', N'Nữ'))
ALTER TABLE THONGTINTAIKHOAN ADD CONSTRAINT chk_NgayVaoLam CHECK(NGTAO<=GETDATE())
ALTER TABLE THONGTINTAIKHOAN ADD CONSTRAINT DEF_DC DEFAULT N'TP.HCM' FOR DCHI
ALTER TABLE THONGTINTAIKHOAN ADD CONSTRAINT chk_SDT_NV CHECK(LEN(SDT)=10)
ALTER TABLE THONGTINTAIKHOAN ADD CONSTRAINT UNI_SDT UNIQUE(SDT)
ALTER TABLE THONGTINTAIKHOAN ADD CONSTRAINT UNI_EMAIL UNIQUE(EMAIL)
GO

--TABLE KHACHHANG
ALTER TABLE KHACHHANG ADD CONSTRAINT chk_SDT_KH CHECK(LEN(MAKH)=10)
ALTER TABLE KHACHHANG ADD CONSTRAINT chk_PhanTramGiam CHECK(DIEMTICHLUY>=0 AND DIEMTICHLUY<=50)
GO

--TABLE HOADON
ALTER TABLE CHITIETHD ADD CONSTRAINT chk_SoLuong_CTHD CHECK(SOLUONG>0)
ALTER TABLE HOADON ADD CONSTRAINT chk_ThanhTien_HD CHECK(THANHTOAN>0)
GO

--TABLE DANH MUC
ALTER TABLE DANHMUC ADD CONSTRAINT uni_TenDanhMuc UNIQUE(TENDANHMUC)
GO

--TABLE SANPHAM

ALTER TABLE SANPHAM ADD CONSTRAINT chk_GiaBan CHECK(DONGIA>0)
GO

--Ràng buộc trigger

--Cập nhật thanh toán trên hóa đơn
CREATE TRIGGER CAPNHATTHANHTOAN
ON CHITIETHD
FOR INSERT,UPDATE,DELETE
AS
	BEGIN
	DECLARE @GiamGia MONEY
	
	-- cập nhật số lượng tồn của sản phẩm khi thêm vào 1 CHITIETHD
		UPDATE SANPHAM
		SET SOLUONG = SOLUONG - (SELECT SOLUONG FROM inserted)
				WHERE MASP = (SELECT MASP FROM inserted)
	--cập nhật TỔNG TIỀN sau khi trừ đi ĐIỂM TÍCH LŨY cho KHACHHANG (1 điểm được 2000)
	IF((SELECT DIEMTICHLUY FROM KHACHHANG,HOADON WHERE HOADON.MAKH=KHACHHANG.MAKH AND MAHD =(SELECT MAHD FROM inserted))>0)
	BEGIN
		SET @GiamGia = (SELECT DIEMTICHLUY FROM KHACHHANG,HOADON WHERE HOADON.MAKH=KHACHHANG.MAKH AND MAHD =(SELECT MAHD FROM inserted))*2000
	-- cập nhật TỔNG TIỀN thu được của mỗi CHITIETHD
		UPDATE HOADON
		SET THANHTOAN = (SELECT SUM(CHITIETHD.SOLUONG*SANPHAM.DONGIA)
						FROM CHITIETHD,SANPHAM
						WHERE CHITIETHD.MASP = SANPHAM.MASP AND CHITIETHD.MAHD =(SELECT MAHD FROM inserted) GROUP BY CHITIETHD.MAHD) - @GiamGia
		WHERE MAHD =(SELECT MAHD FROM inserted)
	END
		ELSE
		-- cập nhật TỔNG TIỀN thu được của mỗi CHITIETHD
		UPDATE HOADON
		SET THANHTOAN = (SELECT SUM(CHITIETHD.SOLUONG*SANPHAM.DONGIA)
						FROM CHITIETHD,SANPHAM
						WHERE CHITIETHD.MASP = SANPHAM.MASP AND CHITIETHD.MAHD =(SELECT MAHD FROM inserted) GROUP BY CHITIETHD.MAHD)
		WHERE MAHD =(SELECT MAHD FROM inserted)
	END		
GO
--cập nhật điểm tích lũy khi khách hàng mua sản phẩm trên 500k

CREATE TRIGGER sp_UpdateDiemTL
ON HOADON
AFTER UPDATE
AS
	IF((SELECT THANHTOAN FROM HOADON WHERE MAHD =(SELECT MAHD FROM inserted)) > 500000 
		AND (SELECT TINHTRANG FROM HOADON WHERE MAHD =(SELECT MAHD FROM inserted)) = N'Đã thanh toán')
	BEGIN
		UPDATE KHACHHANG 
		SET DIEMTICHLUY = DIEMTICHLUY + 1
		WHERE MAKH =(SELECT MAKH FROM inserted)
	END
GO


----------------------------------------------------------------------------
--NHẬP DỮ LIỆU
----------------------------------------------------------------------------


--BẢNG QUYỀN
INSERT QUYEN VALUES(N'ADMIN')
INSERT QUYEN VALUES(N'NHÂN VIÊN')

--BẢNG TÀI KHOẢN
CREATE PROC sp_AddTK(
	@MATK CHAR(10) , 
	@TENTK VARCHAR(50),
	@MATKHAU CHAR(50),
    @MAQUYEN INT)

AS 
--KIỂM TRA TRÙNG KHÓA CHÍNH
	IF EXISTS(SELECT * FROM TAIKHOAN WHERE MATK=@MATK)
		RETURN 0
	--THÊM MỚI DỮ LIỆU
	INSERT INTO TAIKHOAN(MATK,TENTK,MATKHAU,MAQUYEN)
	VALUES(@MATK,@TENTK, @MATKHAU, @MAQUYEN)
GO

EXEC sp_AddTK '1','ADMIN1','202cb962ac5975b964b7152d234b70','1'
EXEC sp_AddTK '2','ADMIN2','202cb962ac5975b964b7152d234b70','1'
EXEC sp_AddTK '3','NHANVIEN1','202cb962ac5975b964b7152d234b70','2'
EXEC sp_AddTK '4','NHANVIEN2','202cb962ac5975b964b7152d234b70','2'
EXEC sp_AddTK '5','NHANVIEN3','202cb962ac5975b964b7152d234b70','2'
EXEC sp_AddTK '6','NHANVIEN4','202cb962ac5975b964b7152d234b70','2'

--BẢNG THÔNG TIN TÀI KHOẢN
CREATE PROC sp_AddTTTK(
	@MATK CHAR(10) , 
    @HOTEN NVARCHAR(50), -- HỌ TÊN
    @NGSINH DATE, -- NGÀY SINH
    @GTINH NVARCHAR(3),
    @NGTAO DATETIME, -- NGÀY TẠO
    @EMAIL VARCHAR(50), -- ĐỊA CHỈ EMAIL
    @SDT VARCHAR(11), -- SDT
    @DCHI NVARCHAR(50),
	@TINHTRANG NVARCHAR(50))

AS 
--KIỂM TRA TRÙNG KHÓA CHÍNH
	IF EXISTS(SELECT * FROM THONGTINTAIKHOAN WHERE MATK=@MATK)
		RETURN 0
	--THÊM MỚI DỮ LIỆU
	INSERT INTO THONGTINTAIKHOAN(MATK,HOTEN,NGSINH,GTINH,NGTAO,EMAIL,SDT,DCHI,TINHTRANG)
	VALUES(@MATK,@HOTEN,@NGSINH,@GTINH,@NGTAO,@EMAIL,@SDT,@DCHI,@TINHTRANG)
GO

EXEC sp_AddTTTK '1',N'NGUYỄN NGỌC NHUNG','10/01/2001',N'Nữ','1/1/2021 07:08:09','nhungafpla@gmail.com','0368627640',N'Đồng Nai',N'Đang Làm'
EXEC sp_AddTTTK '2',N'LÊ THÙY NA','07/07/2001',N'Nữ','1/1/2021 07:08:09','nalethuy@gmail.com','0903134249',N'Phú Yên',N'Đang Làm'
EXEC sp_AddTTTK '3',N'TRẦN ĐỨC THẮNG','09/07/2001',N'Nam','1/1/2021 07:08:09','bnguyenva@gmail.com','0345674123',N'Bình Dương',N'Đang Làm'
EXEC sp_AddTTTK '4',N'HUỲNH THU PHƯƠNG','08/07/1999',N'Nữ','1/1/2021 07:08:09','tunu@gmail.com','0312345678',N'Bình Dương',N'Đang Làm'
EXEC sp_AddTTTK '5',N'TẠ MINH TRỰC','05/07/2001',N'Nam','1/1/2021 07:08:09','minhtruc12@gmail.com','0383039079',N'Bình Dương',N'Đang Làm'
EXEC sp_AddTTTK '6',N'VÕ KIỀU MINH HẢI','01/07/1999',N'Nữ','1/1/2021 07:08:09','minhhai2@gmail.com','0961314900',N'Bình Dương',N'Đang Làm'

--BẢNG KHÁCH HÀNG
INSERT KHACHHANG VALUES('0934214565',0)
INSERT KHACHHANG VALUES('0948571923',1)
INSERT KHACHHANG VALUES('0968492918',2)
INSERT KHACHHANG VALUES('0989090761',3)

--BẢNG NHÂN VIÊN
INSERT NHANVIEN VALUES('NV01','3')
INSERT NHANVIEN VALUES('NV02','4')
INSERT NHANVIEN VALUES('NV03','5')
INSERT NHANVIEN VALUES('NV04','6')

-- BẢNG DANH MỤC
INSERT DANHMUC VALUES(N'Dưỡng da')
INSERT DANHMUC VALUES(N'Làm sạch da' )
INSERT DANHMUC VALUES(N'Phụ kiện' )
INSERT DANHMUC VALUES(N'Chăm sóc cơ thể' )
INSERT DANHMUC VALUES(N'Chăm sóc tóc' )
INSERT DANHMUC VALUES(N'Trang điểm' )

-- BẢNG LOẠI SP
-- dưỡng da
INSERT LOAISP VALUES('LSP01',N'Toner')
INSERT LOAISP VALUES('LSP02',N'Serum')
INSERT LOAISP VALUES('LSP03',N'Kem dưỡng')
INSERT LOAISP VALUES('LSP04',N'Kem mắt')
INSERT LOAISP VALUES('LSP05',N'Nước cân bằng')
INSERT LOAISP VALUES('LSP06',N'Xịt khoáng')
INSERT LOAISP VALUES('LSP07',N'Mặt nạ')
INSERT LOAISP VALUES('LSP08',N'Kem chống nắng')
-- chăm sóc cơ thể
INSERT LOAISP VALUES('LSP09',N'Sữa tắm')
INSERT LOAISP VALUES('LSP10',N'Tẩy tế bào chết cơ thể')
INSERT LOAISP VALUES('LSP11',N'Sữa dưỡng thể')
INSERT LOAISP VALUES('LSP12',N'Kem dưỡng da tay')
INSERT LOAISP VALUES('LSP13',N'Sản phẩm khử mùi')
INSERT LOAISP VALUES('LSP14',N'Nước hoa')
-- Chăm sóc tóc
INSERT LOAISP VALUES('LSP15',N'Dầu gội')
INSERT LOAISP VALUES('LSP16',N'Dầu xả')
INSERT LOAISP VALUES('LSP17',N'Kem ủ tóc')
INSERT LOAISP VALUES('LSP18',N'Dầu dưỡng tóc')
INSERT LOAISP VALUES('LSP19',N'Nhuộm tóc')

-- Trang điểm
INSERT LOAISP VALUES('LSP20',N'Kem lót')
INSERT LOAISP VALUES('LSP21',N'Kem nền')
INSERT LOAISP VALUES('LSP22',N'Kem che khuyết điểm')
INSERT LOAISP VALUES('LSP23',N'Phấn phủ')
INSERT LOAISP VALUES('LSP24',N'Tạo khối')
INSERT LOAISP VALUES('LSP25',N'Kẻ chân mày')
INSERT LOAISP VALUES('LSP26',N'Phấn mắt')
INSERT LOAISP VALUES('LSP27',N'Kẻ mắt')
INSERT LOAISP VALUES('LSP28',N'Mascara')
INSERT LOAISP VALUES('LSP29',N'Má hồng')
INSERT LOAISP VALUES('LSP30',N'Son thỏi')
INSERT LOAISP VALUES('LSP31',N'Son kem')
INSERT LOAISP VALUES('LSP32',N'Son dưỡng')
INSERT LOAISP VALUES('LSP33',N'Phấn nước cushion')
--Làm sạch da
INSERT LOAISP VALUES('LSP34',N'Sữa rửa mặt')
INSERT LOAISP VALUES('LSP35',N'Tẩy trang mặt')
INSERT LOAISP VALUES('LSP36',N'Tẩy trang mắt + môi')
INSERT LOAISP VALUES('LSP37',N'Tẩy da chết mặt')

--Phụ kiện
INSERT LOAISP VALUES('LSP38',N'Cọ trang điểm')
INSERT LOAISP VALUES('LSP39',N'TMút trang điểm')
INSERT LOAISP VALUES('LSP40',N'Dụng cụ bấm mi')
INSERT LOAISP VALUES('LSP41',N'Lông mi giả')
INSERT LOAISP VALUES('LSP42',N'Lens mắt')

CREATE PROC sp_AddSP(
	@TENSP NVARCHAR(MAX), -- TÊN SẢN PHẨM
    @MOTA NVARCHAR(MAX), -- MÔ TẢ
    @SOLUONG INT, -- SỐ LƯỢNG TỒN KHO
    @DONGIA FLOAT, -- ĐƠN GIÁ
    @NSX NVARCHAR(30), -- NHÀ SẢN XUẤT
    @MALSP VARCHAR(6))

AS 
--KIỂM TRA TRÙNG KHÓA CHÍNH
	IF EXISTS(SELECT * FROM SANPHAM WHERE TENSP=@TENSP)
		RETURN 0
	--THÊM MỚI DỮ LIỆU
	INSERT INTO SANPHAM(TENSP,MOTA,SOLUONG,DONGIA,NSX,MALSP)
	VALUES(@TENSP, @MOTA, @SOLUONG, @DONGIA, @NSX,@MALSP)
GO
--BẢNG SP
-- dưỡng da
-- Toner
EXEC sp_AddSP N'Nước cân bằng da Dr.Morita Tea Tree Pore Purifying Toner 150g',N'Thương hiệu: Dr. Morita
Dung tích: 150g
Dr. Morita Tea Tree Pore Purifying Toner với chiết xuất từ tràm trà giúp kháng viêm, làm dịu da và cân bằng độ PH cho da, hỗ trợ điều trị mụn.
Thành phần: Water/Aqua, dirpopylenne glycol, butylene glycol, ethoxydiglycol, phenoxyethanol, methyparaben, sodium PCA, mentha piperita, leaf water, melaleuca alternifoli (Tea Tree) leaf water, calendula offcinalis flower extract, PEG-40 hydrogenated castor oil, styrene/acrylate copolymer, dipotassium, glycyrrhizate, Zinc PCA, Fragrance, Salicylic Aicd, Saccharum offcinarum (sugar maple) extract, citrus aurantium dulcis (orange), hydrolyzed hyaluronic aicd.
Công dụng:
- Với chiết xuất từ tinh chất tràm trà giúp kháng viêm, kháng khuẩn, làm dịu làn da bị mụn.
- Giúp làm sạch bụi bẩn, bã nhờn còn sót lại và cân bằng độ PH cho không bị khô căng.
- Cấp ẩm dịu nhẹ cho da mềm mịn.
Hướng dẫn sử dụng:
- Sau khi rửa mặt, lấy một lượng nước hoa hồng vào bông hoặc lòng bàn tay.
- Thoa đều lên da và vỗ nhẹ nhàng để sản phẩm thẩm thấu vào da. Tránh tiếp xúc với mắt. Dùng buổi sáng và tối.','100','168000',N'Đài Loan','LSP01'
EXEC sp_AddSP N'Toner Cấp Nước Accoje Hydrating Aqua 130ml',N'Nước cân bằng cấp ẩm dịu nhẹ, giúp cân bằng và làm sạch da, cho làn da mềm mại và mịn màng
Công dụng:
- Củ cải đen sinh trưởng tại vùng đảo Jeju nguyên sơ: được tìm thấy từ vùng núi Seongsan Illchulbong nổi tiếng.
- Mạch nước khoáng tinh khiết được tìm thấy từ độ sâu 420m dưới hòn đảo Jeju: Được UNESCO công nhận là nguồn sống tạo nên Jeju thanh sạch, chứa hàm lượng khoáng chất cao cho da căng khỏe.
- Hyaluronic Acid: Nữ hoàng cấp ẩm cho da, với khả năng giữ và khóa nước lên tới 1,000 lần trọng lượng phân tử chính nó, giúp làm ẩm và se khít lỗ chân lông tối ưu.
- Phức hợp Jeju Accoje Complex: Chiết xuất từ tảo nâu, xương rồng, nha đam và rau sam đặc biệt tại đảo Jeju nguyên sơ, sinh trưởng nhờ nguồn nước trong lành của hòn đảo này. Phức hợp có khả năng tăng cường miễn dịch, dưỡng ẩm và ngăn ngừa lão hóa cho da căng khỏe hơn.
HDSD: Sử dụng sau bước làm sạch. Sử dụng bông tẩy trang để thoa đều sản phẩm lên da, nhẹ nhàng mát xa và vỗ nhẹ để dưỡng chất hấp thụ vào sâu bên trong da.
Thành phần: Water, Ecklonia Cava Extract , Portulaca Oleracea Extract , Aloe Barbadensis Leaf Extract, Opuntia Coccinellifera Fruit Extract, 1,2-Hexanediol, Glycerin, Butylene Glycol, Pentylene Glycol, Raphanus Sativus (Radish) Root Extract, Sophora Angustifolia
Điều kiện bảo quản: Bảo quản ở nhiệt độ phòng, tránh nhiệt độ cao và ánh nắng trực tiếp.','1011','490000',N'Hàn Quốc','LSP01'
EXEC sp_AddSP N'Nước Hoa Hồng Skin1004 Cấp Ẩm, Dưỡng Sáng Da 210ml - Madagascar Centella Hyalu-cica Brightening Toner',N'Nước Hoa Hồng Skin1004 Madagascar Centella Hyalu-cica Brightening Toner là sản phẩm đầu tiên của bộ sản phẩm Madagascar Centella Hyalu-cica mới ra mắt từ thương hiệu Skin1004, giúp cung cấp nước và độ ẩm vượt trội cho làn da tức thì, mang lại làn da căng mịn và sáng bóng. Công thức chứa 74% chiết xuất rau má giúp làm dịu làn da & hỗ trợ chữa lành vết thương, kết hợp cùng AHA và LHA giúp làm sạch sâu, tẩy tế bào chết nhẹ nhàng cho da nhạy cảm.
Thành phần cực kì lành tính, không cồn, không hương liệu & phẩm màu nhân tạo, an toàn cho ngay cả những làn da nhạy cảm.
Bảo quản:
Bảo quản nơi khô ráo thoáng mát, tránh ánh nắng trực tiếp.
Đậy nắp lại sau khi sử dụng.
Dung tích: 210ml
Thương hiệu: Skin1004','1213','360000',N'Hàn Quốc','LSP01'
EXEC sp_AddSP N'Nước Hoa Hồng Dưỡng Ẩm, Làm Dịu Da AHC Premium Hydra B5 Toner 140ml',N'Nước Hoa Hồng Dưỡng Ẩm, Làm Dịu Da AHC Premium Hydra B5 Toner có thành phần chính là các chiết xuất từ thiên nhiên cùng hàm lượng vitamin B5 cao giúp cung cấp độ ẩm mạnh mẽ, phục hồi và tái tạo làn da, làm dịu và trẻ hóa làn da, mang đến một làn da tràn đầy sức sống
• Thành phần chính: Dioscorea Japonica Root Extract, Arctium Lappa Root Extract, Phellinus Linteus Extract, Piper Methysticum Leaf/Root/Stem Extract, Camellia Sinensis Leaf Extract, Juniperus Communis Fruit Extract, Oryza Sativa (Rice) Extract, Salvia Hispanica Seed Extract, Portulaca Oleracea Extract, Pueraria Mirifica Root Extract, Glycyrrhiza Glabra (Licorice) Leaf Extract, Pisum Sativum (Pea) Extract, Cnidium Officinale Root Extract, Acer Saccharum (Sugar Maple) Extract, Houttuynia Cordata Extract, Leontopodium Alpinum Extract, Tilia Vulgaris Flower Extract, Jasminum Officinale (Jasmine) Extract, Chamaecyparis Obtusa Leaf Extract, Linum Usitatissimum (Linseed) Seed Extract, Aloe Barbadensis Leaf Extract, Hibiscus Esculentus Fruit Extract, Sodium Hyaluronate, Soluble Collagen, Betula Platyphylla Japonica Juice, Aloe Barbadensis Leaf Juice, Cananga Odorata (Ylang Ylang) Flower Water, Panthenol.
• Đối tượng khuyên dùng:
- Dành cho mọi loại da.
- Dành cho làn da khô ráp, thiếu ẩm.
- Dành cho làn da mất sức sống.
• Hướng dẫn sử dụng: Sử dụng sau bước rửa mặt, lấy một lượng nước hoa hồng vừa đủ thấm lên bông tẩy trang, rồi nhẹ nhàng lau theo chiều kết cấu da.
• Dung tích: 140ml
• Hạn sử dụng: 3 năm kể từ ngày sản xuất/NSX xem trên bao bì.','3243','672000',N'Hàn Quốc','LSP01'
EXEC sp_AddSP N'Nước hoa hồng chống viêm không mùi Klairs Supple Preparation Unscented Toner 180ml',N'Công dụng:
+ Cung cấp và duy trì độ ẩm cho da nhờ Hyaluronic acid, giữ cho da căng mọng mịn màng. Đồng thời có khả năng kháng viêm và ngừa mụn hiệu quả, giúp da chống lại các tác động xấu từ môi trường và luôn khỏe mạnh.
+ Giúp cân bằng độ PH của da với chiết xuất từ thực vật, không chứa hương liệu và tinh dầu, phù hợp với cả những làn da nhạy cảm nhất.
+ Loại da: mọi loại da, kể cả da nhạy cảm.
Thành phần: Water, Butylene Glycol, Dimethyl Sulfone, Betaine, Caprylic/Capric Triglyceride, Natto Gum, Sodium Hyaluronate, Disodium EDTA, Centella Asiatica Extract, Glycyrrhiza Glabra (Licorice) Root Extract, Polyquaternium-51, Chlorphenesin, Tocopheryl Acetate, Carbomer, Panthenol, Arginine, Luffa Cylindrica Fruit/Leaf/Stem Extract, Beta-Glucan, Althaea Rosea Flower Extract, Aloe Barbadensis Leaf Extract, Hydroxyethylcellulose, Portulaca, Oleracea Extract, Lysine HCL, Proline, Sodium Ascorbyl Phosphate, Acetyl Methionine, Theanine, Copper Tripeptide-1
Hướng dẫn sử dụng: Dùng sau sữa rửa mặt, lấy một lượng vừa đủ, vỗ nhẹ lên mặt cho đến khi thẩm thấu hoặc thấm ra bông tẩy trang lau nhẹ nhàng toàn mặt.
HSD: 3 năm từ NSX
Xem trên sản phẩm (năm.tháng.ngày)','754','299000',N'Hàn Quốc','LSP01'

select * from SANPHAM WHERE MALSP='LSP01'

--BẢNG SP
-- dưỡng da
--Serum
EXEC sp_AddSP N'Serum Cấp Nước Dưỡng Ẩm Balance Active Formula Hyaluronic Deep Moisture Serum 30ml',N'Thành phần: AQUA / WATER, CHONDRUS CRISPUS (CARRAGEENAN) POWDER, DISODIUM EDTA, ETHYLHEXYLGLYCERIN, GLYCERIN, PANTHENOL, PHENOXYETHANOL, SODIUM HYALURONATE
Công dụng :Serum Cấp Nước Dưỡng Ẩm Balance Active Formula với dạng tinh chất thẩm thấu cực nhanh, thấm vào da trong tích tắc. Bản sẽ có cảm giác da nhẹ tênh không nhờn không dính không bóng chút nào, nhưng lại như được phủ một lượng nước khổng lồ giữ cho bề mặt da luôn đủ ẩm, mướt mịn, mềm mại và tươi mới dù trong bất kì thời tiết nắng mưa sương gió nào.
công dụng:
- Serum Cấp Nước Dưỡng Ẩm Balance Active Formula chứa 5% Hylasome EG10 – một chất được chứng minh có khả năng giữ nước gấp 5 lần HA, cấp ẩm gấp 5 lần HA, chống oxy hóa cũng gấp 4 lần so với HA và hiệu quả đó có thể kéo dài lên đến 24 giờ đồng hồ.
- Syn-Ake – 1 loại peptide mô phỏng và “bắt chước” tác dụng của nọc rắn, giúp tăng độ đàn hồi cho da, làm đầy các nếp nhăn cũ và ngăn chặn sự hình thành các nếp nhăn mới, nuôi dưỡng làn da từ bên trong để da luôn “trẻ” mãi, bóng khỏe và căng đầy.
- Cung cấp và duy trì độ ẩm cho da, giúp kiềm dầu và hạn chế tiết dầu trên da.
- Loại bỏ làn da sần sùi, khô ráp đem lại làn da ẩm mượt, mềm mại., da luôn mọng nước.','909','210000',N'Anh Quốc','LSP02'
EXEC sp_AddSP N'Serum Vi Dưỡng Tái Tạo Da Olay Regenerist (Chai 50ml)',N'Serum dưỡng da Chống lão hoá Olay Regenerist Advanced Anti-Aging Moisturize 50ml của Mỹ là serum dưỡng ẩm ban đêm giúp cung cấp độ ẩm, làm sáng da, làm mờ nếp nhăn, tái tạo và giảm hiện tượng chảy xệ của da. Đây là sản phẩm được ưa chuộng hàng đầu trong các loại serum dưỡng da chất lượng cao nhưng mức giá vừa phải.
Thành phần và công dụng:
- Tinh Chất Vi Dưỡng Olay Regenerist 50ml sử dụng công thức chứa hỗn hợp Amino Peptide và hỗn hợp Vitamin B3 có tác dụng trẻ hóa lớp bề mặt da, tăng cường độ săn chắc cho da.
- Sản phẩm giúp lấy lại vẻ tươi trẻ, rạng rỡ cho khuôn mặt, cung cấp các thành phần chống lão hóa cho da, làm đầy các đường rãnh, nếp nhăn sâu trên da, đào thải độc tố, giúp da mềm mịn, tươi trẻ dài lâu.
- Tinh Chất Vi Dưỡng Olay Regenerist có kết cấu cực kỳ mỏng nhẹ nên thẩm thấu dễ dàng vào da, không để lại cảm giác nhờn dính, khó chịu khi sử dụng.
- Sản phẩm không chứa chất tạo màu và mùi hương nhân tạo.
HDSD:
Lấy một lượng vừa đủ và dùng đầu ngón tay thoa đều lên da mặt, cổ. Có thể thoa trước khi dùng kem dưỡng ẩm hoặc kem nền trang điểm
.Sử dụng 1 2 lần/ngày','767','549000',N'Mỹ','LSP02'
EXEC sp_AddSP N'Tinh Chất Dưỡng Trắng Da Some By Mi Yuja Niacin 30 Days Blemish Care Serum 50ml',N'THÀNH PHẦN và CÔNG DỤNG:
Giúp cải thiện tông da, làm đều màu da và tăng cường độ ẩm giúp da tràn đầy sức sống sau khi ngủ dậy.
Chứa 82% thành phần chiết xuất quả Yuja, Niacinamide, Glutathione, Arbutin và thành phần dưỡng trắng Resmelin được cấp bằng sáng chế giúp dưỡng trắng hiệu quả.
Chứa thành phần Aquaxyl FructanTM giúp tăng cường độ ẩm cũng 12 loại Vitamin nuôi dưỡng làn da khỏe mạnh.
Không chứa 20 thành phần gây hại cho da
HƯỚNG DẪN SỬ DỤNG:
1. Tẩy trang và làm sạch mặt, sử dụng nước hoa hồng để cân bằng độ pH trên da. 
2. Lấy một lượng Tinh Chất Dưỡng Trắng Some By Mi Yuja Niacin 30 Days Blemish Care Serum vừa đủ chấm đều các điểm trên mặt. 
3. Thoa đều khắp mặt và massage nhẹ nhàng để tinh chất thẩm thấu tốt hơn. 
4. Sử dụng 2 lần/ ngày vào sáng và tối.
LƯU Ý:
- Bao bì sản phẩm có thể thay đổi theo từng lô hàng từ nhà sản xuất.
- Khách hàng cần hiểu và lưu ý rằng không có một sản phẩm nào có thể đảm bảo phù hợp với 100% tất cả các làn da. Vậy nên bạn cần thử sản phẩm ở một vùng nhỏ trên da trước khi sử dụng cho toàn khuôn mặt.
- Thời gian sản phẩm có tác dụng trên da sẽ khác nhau, tùy cơ địa mỗi người
HSD: 3 năm từ NSX','12','326000',N'Hàn Quốc','LSP02'
EXEC sp_AddSP N'Tinh chất chuyên biệt cho da mụn Acnes 25+ Facial Serum 20ml',N'ĐẶC TÍNH 
Việc chọn một sản phẩm chuyên biệt để điều trị, chăm sóc hiệu quả vùng da mụn từ tuổi 25 đã khó; duy trì, cải thiện và nuôi dưỡng vùng da còn lại cùng lúc với việc điều trị mụn càng khó hơn. Hãy tự tin chọn ACNES 25+ FACIAL SERUM - Tinh chất trong suốt, không nhờn rít, với công thức vượt trội C+ SYSTEM kết hợp với Salicylic Acid thẩm thấu nhanh vào tế bào da giúp:
- SẠCH MỤN, GIẢM SƯNG ĐỎ, 
- LÀM SÁNG VẾT THÂM VÀ CẢI THIỆN BỀ MẶT DA, 
- NGĂN NGỪA DẤU HIỆU LÃO HÓA DA 
CÔNG DỤNG Tinh chất chăm sóc da mụn tuổi trưởng thành, giúp sạch mụn, sáng vết thâm và dưỡng da, ngăn ngừa dấu hiệu lão hóa. 
THÀNH PHẦN C+ System (Alpiniawhite, Dipotassium Glycyrrhizate, Pure Vitamin C, Derivative Vitamin C), Salicylic Acid 
CÁCH DÙNG 
Bước 1: ACNES 25+ FACIAL WASH 
Bước 2: ACNES 25+ FACIAL SERUM Thoa tinh chất lên khắp mặt, sử dụng lượng nhiều hơn đối với vùng da mụn. Dùng 2 lần/ ngày. 
HSD: 3 năm từ ngày sản xuất','211','105000',N'Việt Nam ','LSP02'
EXEC sp_AddSP N'Tinh Chất 9 Wishes Miracle White Ampule Serum Phục Hồi Da Và Dưỡng Trắng 25ml',N'Tính năng sản phẩm:
9 WISHES MIRACLE WHITE AMPULE SERUM là một dạng tinh chất dưỡng trắng da cô đặc đến từ hãng mỹ phẩm 9 Wishes. Sản phẩm với kết cấu nhẹ dễ dàng thấm sâu vào da, nuôi dưỡng, phục hồi làn da mệt mỏi, xạm đen vì nắng và các tia UV có hại. Với thành phần gồm các yếu tố mang lại hiệu quả cao như Glutathione, Niacinamide và Allantoin có khả năng làm giảm số lượng và cường độ của đốm đen trên da, 9 WISHES MIRACLE WHITE AMPULE SERUM mang lại làn da mềm mại, đủ ẩm và trắng sáng dần từ bên trong.
Thành phần chính: Carbonated Water, Water, Glycerin, Butylene Glycol, Galactomyces Ferment Filtrate, Beta-Glucan, Bifida Fermet Filtrate, Snail Secretion Filtrate, Centella Asiatica Extract, Aqua, Glycerin, Butylene Glycol, Betain, Niacinamide, Paeony Root Extract.
Hướng dẫn sử dụng: Sau bước toner lấy vài giọt tinh chất massage đều trên da mặt.','12','236000',N'Hàn Quốc','LSP02'

select * from SANPHAM WHERE MALSP='LSP02'

--Kem dưỡng
EXEC sp_AddSP N'Bioderma Kem Chăm Sóc Se Lỗ Chân Lông 30ml',N'Bioderma Sébium Pore Refiner là một sản phẩm kem dưỡng da nằm trong bộ các loại sản phẩm chăm sóc da của hãng dược mỹ phẩm hàng đầu tại Pháp mang tên Bioderma. Với những tác động ưu việt, nhanh chóng thẩm thấu vào sâu trong da giúp làm sạch sâu, ngăn ngừa bụi bẩn từ môi trường xung quanh...từ đó giúp se khít lỗ chân lông một cách hiệu quả và an toàn. Đặc biệt, sản phẩm còn giúp kiểm soát bã nhờn rất hiệu quả mà không hề gây kích ứng cho da.
Công dụng:
- Bảo vệ da khỏi những tác nhân gây giãn nở lỗ chân lông như bụi bẩn, vi khuẩn...
- Ngăn chặn hoàn toàn việc lỗ chân lông bị bít nhờ khả năng làm sạch lỗ chân lông bằng Salycilic Acid (BHA)
- Giảm da bóng nhờn, điều tiết lượng dầu thừa trên da.
- Làm mờ các lỗ chân lông, đóng vai trò như một lớp lót che khuyết điểm trước lớp nền.
- Thành phần chống oxy hóa từ Gingko biloba và nấm giúp nuỗi dưỡng làn da mịn màng và tươi sáng.
- Hương thơm nhẹ nhàng, thấm vào da nhanh chóng.
- Không làm bí da, gây tắc nghẽn lỗ chân lông hay nặng mặt, cho da thông thoáng dễ chịu cả ngày.
Hướng dẫn sử dụng:
- Lấy một lượng kem vừa đủ thoa đều trên mặt và/hoặc trên cả cơ thể sau khi đã làm sạch da.
- Thoa đều theo chuyển động xoắn ốc để các tinh chất thấm sâu và được hấp thu hoàn toàn vào bên trong da.
- Thực hiện mỗi ngày 1 – 2 lần, vào buổi sáng sau khi thức dậy hoặc tối trước khi ngủ để có kết quả tốt nhất.
Thành phần:
Aqua/water/eau, methyl methacrylate crosspolymer, dipropylene glycol, cyclopentasiloxane, cyclohexasiloxane, dimethicone, glycerin, butylene glycol, fomes officinalis (mushroom) extract, sodium polyacrylate, salicylic acid, dodecyl gallate, ginkgo biloba leaf extract, mannitol, xylitol, rhamnose, fructooligosaccharides, laminaria ochroleuca extract, silica, trideceth-6, c30-45 alkyl cetearyl dimethicone crosspolymer, lauryl peg/ppg-18/18 methicone, caprylic/capric triglyceride, mineral oil (paraffinum liquidum), pentylene glycol, 1,2-hexanediol. Caprylyl glycol, propylene glycol, sodium hydroxide, citric acid, disodium edta.','578','460000',N'Pháp','LSP03'
EXEC sp_AddSP N'Cell Fusion C Kem Làm Dịu % Mát Da Khẩn Cấp 50ml',N'Công Dụng - Kích hoạt Aquaporin - kênh dẫn nước trong da làm mát dịu và giảm các triệu chứng ửng đỏ, châm chích tức thì. - Cấp ẩm và giữ ẩm vượt trội sau 2 - 4 tuần sử dụng - Sản phẩm phù hợp cho mọi loại da có tình trạng thiếu ẩm, kích ứng, cháy nắng và sưng do nhiệt Thành Phần - Glyceryl Glucoside: kích hoạt AQUAPORINS để cung cấp độ ẩm cho toàn làn da - Phospho lipid + ceramide + Nam châm nước (water magnet) + acid amin thiết yếu: tạo môi trường giữ ẩm cho da - Độ ẩm nền 68% và hệ thống giữ ẩm phân tầng cùng 7 dạng HA mang đến hiệu quả giữ ẩm dài lâu Hướng Dẫn Sử Dụng Sau bước làm sạch và toner, tinh chất (serum/ ampoule), dùng một lượng vừa đủ vỗ nhẹ lên da và massage theo chiều cấu tạo da','7676','780000',N'Pháp','LSP03'
EXEC sp_AddSP N'Cetaphil kem dưỡng ẩm 50g',N'Công dụng, đặc điểm sản phẩm: Dưỡng ẩm cho da. Sản phẩm có công thức đặc biệt dành cho da khô mãn tính, nhạy cảm và dị ứng, giúp da trở nên mềm, mượt có độ ẩm. Kể cả da đang bị bệnh như chàm, nám da, mụn trứng cá, da đang bị dị ứng Thành phần sản phẩm: Aqua, polyglyceryl-methacrylate và propylene glycol, dầu khoáng nhẹ, diocttyl ether, PEG-5 glyceryl stearate, glycerin, dimethicone và dimethiconol, cetyl alcohol, dầu hạnh đào, benzyl alcohol, sodium hydroxide, tocophenryl acelate, acrylate/ C10-30 alkyl acrylate, crosspo-tymer disodium edetate, alpha tocopherol,... Hướng dẫn sử dụng: Thoa lên vùng da khô khi cần. Thoa sau khi làm ướt da, rửa tay hoặc sau khi tắm. Thông tin bảo quản: Bảo quản Nơi khô ráo, thoáng mát, tránh ánh nắng trực tiếp HSD: 30 tháng kể từ NSX ghi trên bao bì. ','988','215000',N'Pháp','LSP03'
EXEC sp_AddSP N'Kem dưỡng ẩm da NIVEA Crème 30ml',N'Được bổ sung hoạt chất Eucerit giúp cung cấp độ ẩm đến cả ngày dài; bảo vệ và nuôi dưỡng làn da mềm mại, mịn màng, đặc biệt cho da khô ráp.
Bảo quản: Nơi khô ráo, tránh nhiệt độ cao, ánh nắng trực tiếp.
Hướng dẫn sử dụng: Thoa kem lên vùng da cần dưỡng ẩm mỗi ngày.
Hạn sử dụng: 30 tháng từ ngày sản xuất
Thành phần chính
Chiết xuất từ sữa, hoạt chất Eucerit.','22','31000',N'Thái Lan','LSP03'
EXEC sp_AddSP N'Kem Dưỡng Ẩm Eucerin Cho Da Thường Đến Da Hỗn Hợp Eucerin Aquaporin Active Moisturising Cream Light (50ml)',N'Kem dưỡng ẩm cho da thường, hỗn hợp mất nước Eucerin Aquaporin Active 50ml giúp ngăn chặn sự mất nước và đảm bảo giữ ẩm lâu dài, giúp tăng cường độ ẩm suốt 24h từ khi sử dụng, mang lại cho làn da vẻ mịn màng và tươi sáng
Hướng dẫn sử dụng
Làm sạch da
Lấy một lượng sản phẩm vừa đủ bôi toàn mặt, cổ
Dùng 2 lần sáng và tối
Thành phần Aqua, Glycerine, Glyceryl Glucoside, Dicaprylyl Carbonate, Distarch Phosphate,..
Hạn sử dụng 3 năm kể từ ngày sản xuất
Hướng dẫn bảo quản Nơi khô ráo, thoáng mát','12','590000',N'Đức','LSP03'

select * from SANPHAM WHERE MALSP='LSP03'

--Kem mắt
EXEC sp_AddSP N'Gel chống nhăn và thâm quầng mắt Rebirth Anti-wrinkle Eye Gel (30ml)',N'Gel nhẹ bao gồm chiết xuất lô hội (xem thêm lợi ích lô hội ở bên dưới) làm dịu mát các vùng mắt nhạy cảm, làm giảm sưng phồng và vết đen. Vitamin E giúp làm giảm vết nhăn, thích hợp cho sử dụng hàng ngày.
Công dụng: Làm giảm vết nhăn quanh vùng mắt. Chống thâm vùng mắt. Gel nhẹ, dễ dàng hấp thụ. Làm giảm vết đen xung quanh mắt. 
Hướng dẫn sử dụng: 
- Dùng ngón đeo nhẫn chấm gel dưỡng lên trên bầu mắt. Nên dùng ngón đeo nhẫn vì lực ở ngón đeo nhẫn là yếu nhất trong số 5 đầu ngón tay, như vậy mới không làm tổn thương đến mắt đồng thời làm gel thâm nhập sâu hơn vào da. 
- Dùng đầu ngón đeo nhẫn tản đều gel . Bạn nên tản gel theo hướng từ đầu mắt đến cuối mắt, có thể miết nhẹ. 
- Khi đến điểm đuôi mắt, bạn massage hướng lên phía trên, đây là thủ thuật hết sức đơn giản và hiệu quả để ngăn ngừa nếp nhăn cho mắt.
- Sau đó dùng ngón đeo nhẫn chấm lên bầu mắt dưới. 
- Sau đó cũng tản kem từ đầu mắt đến đuôi mắt theo hướng lên trên. 
- Nếu vị trí này đã xuất hiện nếp nhăn, hãy thoa gel theo hướng ngược với nếp nhăn, có thể từ đuôi mắt ngược về phía đầu mắt. 
Hạn sử dụng: 3 năm kể từ ngày sản xuất 
Ngày sản xuất: xem trên bao bì sản phẩm ','6','155000',N'Úc','LSP04'
EXEC sp_AddSP N'GEL DƯỠNG NGĂN NGỪA NẾP NHĂN VÙNG MẮT NARIS WRINKLE PLUS 20G',N'Công dụng: Gel dưỡng mắt nuôi dưỡng sự đàn hồi, ngăn ngừa và giảm thiểu nếp nhăn vùng mắt. Giảm stress cho vùng da mắt khi làm việc trong thời gian dài. Duy trì tế bào da khỏe mạnh và sáng đẹp.
Cách dùng: Sử dụng ngón đeo nhẫn bôi đều gel dưỡng và massage nhẹ nhàng theo chỉ dẫn trên vỏ hộp lên xung quanh khu vực mắt. Nên sử dụng hàng ngày để mang đến hiệu quả ngăn ngừa và cải thiện nếp nhăn cao.
Thành phần chính: Water, Butylene Glycol, Carbomer, Jobs Tears (Coix Lacryma-Jobi) Extract, Potassium Hydroxide, Sodium Hyaluronate, Disodium Edta, D-Δ-Tocopherol, Soluble Collagen,...
Không gây kích ứng da
Thể tích thực: 20g
Bảo quản: Để sản phẩm nơi khô ráo, tránh ánh nắng trực tiếp.
Ngày sản xuất: (Căn cứ CA) 
Hạn sử dụng: 05 năm kể từ ngày sản xuất','8','295000',N'Nhật Bản','LSP04'
EXEC sp_AddSP N'Kem Dưỡng Giảm Thâm Quầng Mắt Himalaya (15ml)',N'Kem Dưỡng Giảm Thâm Quầng Mắt Himalaya 15g chứa cipadessa Baccifera ngăn ngừa nếp nhăn và làm sáng quầng thâm mắt.
Winter Begonia là một loại thảo mộc với nhiều lợi ích trong dược phẩm, được sử dụng trong kem mắt với tác dụng làm sáng vùng da dưới mắt
Dầu mầm lúa mì có nguồn Vitamin E tự nhiên phong phú, giúp bảo vệ làn da của bạn khỏi tác hại do ô nhiễm, tia UV, nuôi dưỡng da.
Kem dưỡng mắt đã được kiểm nghiệm lâm sàng có hiệu quả giảm thâm quầng mắt sau 6 tuần sử dụng.
Thành phần an toàn từ các loại thảo mộc giúp chăm sóc vùng da mỏng manh quanh mắt, làm giảm quầng thâm, bảo vệ khỏi tác hại của ánh sáng mặt trời, dưỡng ẩm, ngừa nếp nhăn mắt.
Hướng dẫn sử dụng: dùng lượng vừa đủ thoa lên da vùng da mắt, cách mi mắt 1cm, massage thật nhẹ nhàng cho kem thấm hết, ngày dùng 2 lần.
HSD: 3 năm từ NSX','21','155000',N'Ấn Độ','LSP04'
EXEC sp_AddSP N'Kem Dưỡng Ngăn Lão Hóa Vùng Mắt EUCERIN Anti-Age Hyaluron Filler Eye Treatment 15ml',N'Kem dưỡng làm mờ vết nhăn vùng mắt
Làm giảm rõ rệt và ngăn ngừa các vết nhăn quanh vùng mắt
Thành phần Aqua, Glycerin, Butylene Glycol Dicaprylate/Dicaprate, Diethylamino Hydroxybenzoyl Hexyl Benzoate Methylpropanediol, Synthetic Beeswax, Behenyl Alcohol, Ethylhexyl Triazone, Hydrogenated Coco-Glycerides Octyldodecanol, Stearyl Alcohol,...
Hướng dẫn sử dụng Thoa lên vùng da quanh mắt và dùng đầu ngón tay xoa nhẹ. Tránh tiếp xúc trực tiếp với mắt
Hướng dẫn bảo quản Bảo quản nơi khô ráo, thoáng mát, tránh ánh nắng trực tiếp và nhiệt độ cao
Hạn sử dụng: 30 tháng kể từ ngày sản xuất','22','1080000',N'Đức','LSP04'

select * from SANPHAM WHERE MALSP='LSP04'

--Nước cân bằng
EXEC sp_AddSP N'Vichy Nước Cân Bằng Cấp Ẩm Dành Cho Da Nhạy Cảm 200ml',N'THÔNG TIN SẢN PHẨM
1. NƯỚC KHOÁNG NÚI LỬA VICHY: củng cố và phục hồi hàng rào bảo vệ da
2. GLYCERIN: giàu bọt mịn, độ ẩm cao giảm khô căng da
Công dụng:
Nước cân bằng với công thức gồm nước khoáng VICHY chứa 15 khoáng chất quý hiếm và Glycerin giúp:
Cấp ẩm hiệu quả: +27% sau 1h sử dụng, +18% sau 4h sử dụng
Giảm tình trạng khô da giúp cho da trông khỏe hơn, và sáng hơn trong lần đầu sử dụng.
Cách dùng:
Sử dụng 2 lần/ngày với bông tẩy trang trên vùng da mặt và cổ
Miết nhẹ từ vùng trong mặt ra ngoài, và từ dưới cổ lên trên.
Áp dụng sau khi sử dụng nước/sữa tẩy trang VICHY để hiệu quả được tốt nhất
Bảo quản: Nhiệt độ phòng, nơi khô ráo, thoáng mát, tránh ánh nắng trực tiếp, tránh xa tầm với trẻ em.
Cảnh báo an toàn cho sức khỏe: Không paraben, không alcohol. An toàn cho da nhạy cảm. Kích ứng: không đáng kể
Thể tích thực: 200ml','19','545000',N'Pháp','LSP05'
EXEC sp_AddSP N'Nước cân bằng DermaDS 180ML',N'Nước cân bằng kiểm soát dầu – Clarifying Toner ngoài giúp cân bằng ẩm còn đóng vai trò như một sản phẩm đặc trị. Với công thức dịu nhẹ và độ thẩm thấu nhanh, sản phẩm này giúp cung cấp chất dinh dưỡng đồng thời ngăn ngừa mụn và đào thải độc tố cho da hiệu quả. Dung tích: Chai 180ml
Thành phần chính: Panthenol, Sodium Hyaluronate, Lactobionic Acid, Glycolic Acid, Salicylic Acid,…
Công dụng: Loại bỏ tế bào chết, đẩy lùi và ngăn ngừa mụn xuất hiện. Cân bằng pH của da, cấp nước cho da mềm mại và khỏe mạnh hơn. Tác dụng làm mát da, thải độc, nhanh chóng làm xẹp mụn, giảm đau và tiêu viêm. Kháng viêm, kháng khuẩn và cung cấp dưỡng chất nuôi dưỡng làn da.
Cách sử dụng: Sau bước làm sạch da, dùng khăn giấy thấm nhẹ cho khô da mặt. Đổ lượng vừa đủ toner vào lớp bông tẩy trang, lau nhẹ nhàng lớp bông khắp mặt tránh vùng mắt và môi. Tuyệt đối không chà xát quá mạnh tay trên bề mặt da.
Đối tượng khuyên dùng: Dành cho da dầu, da hỗn hợp thiên dầu, da mụn.
HSD: 3 năm kể từ NSX','26','481000',N'Hàn Quốc','LSP05'
EXEC sp_AddSP N'Nước Cân Bằng Dạng Phun Sương ( Sukin Original Hydrating Mist Toner ) 125ml',N'THÀNH PHẦN CHÍNH VÀ CÔNG DỤNG
Chiết xuất hoa cúc La Mã làm dịu, đem lại sự tươi mới, làm sạch và dưỡng ẩm da.
Nước hoa hồng đem lại sự tươi mới và làm dịu làn da.
Sukin Hydrating Mist Toner không chứa cồn với sự kết hợp chiết xuất hoa cúc La Mã và Nước hoa hồng để giúp làm dịu, thanh lọc và làm mát làn da mệt mỏi của bạn.
Sản phẩm dành cho mọi loại da.
BẢNG THÀNH PHẦN CHI TIẾT
Water (Aqua), Rosa Damascena Flower Water (Rose), Glycerin, Chamomilla Recutita (Matricaria) Flower Extract (Chamomile), Citric Acid, Phenoxyethanol.
HƯỚNG DẪN SỬ DỤNG
– Đặt sản phẩm xách da mặt khoảng cách 15cm hơi hướng từ trên xuống 45 độ và hơi ngước mặt lên.
– Nhắm mặt lại, xịt vào mặt và cổ để cảm nhận hơi mát từ sản phẩm.
– Bạn hãy sử dụng xuyên suốt trong ngày để có làn da khỏe và đủ độ ẩm.
– Bạn có thể sử dụng toner sau khi rửa mặt, trước và sau khi trang điểm hoặc bất cứ lúc nào da bạn cảm thấy mệt mỏi, căng thẳng hoặc quá nóng rát.','15','209000',N'Úc','LSP05'
EXEC sp_AddSP N'Nước cân bằng dịu da dưỡng ẩm hoa cúc Calendula Purite 150ml',N'CÔNG DỤNG, ĐẶC ĐIỂM SẢN PHẨM
Hoa Cúc Calendula từ Pháp nổi tiếng trong ngành làm đẹp nhờ đặc tính làm dịu và dưỡng ẩm mà không gây bít da.
Purité mang bí quyết làm đẹp từ Hoa Cúc Calendula vào dòng sản phẩm Real Calendula Gentle Hydrating, gói trọn sức mạnh dưỡng ẩm từ những cánh hoa. Kết hợp cùng hoạt chất Hyaluronic Acid để mang lại hiệu quả cấp ẩm tức thì và dưỡng ẩm dài lâu.
Từng cánh hoa calendula thật được tuyển chọn kỹ lưỡng đưa vào sản phẩm, cùng nano HA với kích thước siêu nhỏ giúp làm dịu làn da và cân bằng độ ẩm, cho da sẵn sàng hấp thụ tốt nhất khoáng chất từ các bước dưỡng sau.','66','129000',N'Pháp','LSP05'
EXEC sp_AddSP N'Nước cân bằng cho da khô DProgram 125ml',N'Mô tả sản phẩm:
D PROGRAM NƯỚC CÂN BẰNG CHO DA KHÔ làm mềm da cải thiện tình trạng khô da, gồ ghề và tình trạng dị ứng đồng thời cải thiện bề mặt da nhờ giảm áp lực máu bên trong da
- Ngăn chặn tình trạng khô da, cải thiện sự gồ ghề của da giúp cho kết cấu da giảm bong tróc và nứt nẻ.
- Ổn định lưu lượng máu dưới da và ngăn chặn giãn mạch cho làn da đẹp không còn nỗi lo.
- H-STABILIZING A tăng cường hàng rào bảo vệ da bởi làm vững mạnh lớp sừng để chống lại những tác nhân có hại từ môi trường
- GLYCYRRHIZINATE: kháng viêm
- PHỨC HỢP XR: những TP ổn định hệ vi sinh thông thường trên bề mặt da
- Chiết xuất LÁ NGẢI CỨU: chống & giảm ngứa
- Thành phần tăng cường khả năng thẩm thấu
- M-TRANEXAMIC ACID (cải thiện da gồ ghề & dưỡng sáng)','4','790000',N'Pháp','LSP05'

select * from SANPHAM WHERE MALSP='LSP05'

--BẢNG SP
-- dưỡng da
-- Xịt khoáng
EXEC sp_AddSP N'Bộ 2 Chai Xịt Dưỡng Ẩm Eucerin Hyaluron Mist Spray (150mlx2)',N'Xịt Khoáng Eucerin Hyaluron Mist Spray Dưỡng Ẩm Da là nước xịt khoáng đến từ thương hiệu mỹ phẩm Eucerin của Đức, với công thức chứa Hyaluronic Acid trọng lượng tối ưu cho hiệu quả dưỡng ẩm vượt trội, không chỉ mang đến sự tươi mát tức tức thì cho làn da mà còn làm giảm nếp nhăn hiệu quả, cho làn da trẻ trung và căng mịn, rạng rỡ suốt cả ngày dài.
Bảo quản:
Nơi khô ráo, thoáng mát.
Tránh ánh nắng trực tiếp, nơi có nhiệt độ cao hoặc ẩm ướt.
Đậy nắp kín sau khi sử dụng.
Dung tích: 50ml / 150ml
Thương hiệu: Eucerin','99','349000',N'Đức','LSP06'
EXEC sp_AddSP N'Nước khoáng La Roche Posay giúp làm sạch và làm dịu da Serozinc 50ml',N'Mô tả
Xịt khoáng là bước chăm sóc cần thiết cho mọi loại da. Nước xịt khoáng làm dịu da & giảm bóng nhờn cho da dầu mụn La Roche-Posay Serozinc 50ml dành riêng cho da dầu, da mụn chứa thành phần Kẽm (Zinc) có tác dụng chống oxy hóa, điều tiết bã nhờn, giảm sưng viêm, làm dịu và phục hồi da. Với nước xịt khoáng này bạn có thể dùng khi da bị kích ứng, khi vừa cạo râu hay có thể dùng trên da của em bé.
Loại sản phẩm
- Nước xịt khoáng chống oxy hóa, giảm sưng viêm, giảm bóng nhờn đến hơn 70%.
Loại da phù hợp
- Dành riêng cho da dầu, da mụn.
- Dùng được cho mặt & cơ thể.
- Dùng được cho người lớn, trẻ em & trẻ sơ sinh.
Độ an toàn
- Không gây kích ứng.
- An toàn cho mọi loại da, đặc biệt là da nhạy cảm.
Công dụng
- Chống oxy hóa, giảm sưng viêm, giảm bóng nhờn đến hơn 70%.
- Các giọt nước siêu nhỏ thẩm thấu sâu vào da cho hiệu quả làm dịu tức thì.
Hướng dẫn bảo quản
- Nơi thoáng mát, tránh ánh nắng mặt trời trực tiếp.
Hạn sử dụng: 3 năm kể từ ngày sản xuất
Ngày sản xuất: In trên bao bì sản phẩm','777','170000',N'Pháp','LSP06'

select * from SANPHAM WHERE MALSP='LSP06'

--BẢNG SP
-- dưỡng da
-- Mặt nạ
EXEC sp_AddSP N'Bộ 4 mặt nạ Matrix Ngọc Trai gói 30g + tặng kèm 1 mặt nạ nha đam 30g',N'Mô tả
Công dụng và đặc điểm của sản phẩm: Mặt nạ Matrix Lecos Ngọc Trai được kết hợp từ các dưỡng chất thiên nhiên giúp tăng cường khả năng dưỡng trắng và làm sáng da, giúp cân bằng ẩm, bảo vệ da khỏi tác động của môi trường.
Thành phần sản phẩm : Water, Glycerin, Sodium PCA, Hydroxyethylcellulose, Clam Pearl Sac Secretion, Phenoxyethanol, Polysorbate 20, Parfum.
Hướng dẫn sử dụng :
1. Sau khi rửa mặt sạch, lấy mặt nạ ra khỏi bao bì. 
2. Đắp mặt nạ lên mặt bắt đầu từ vùng trán. 
3. Thư giãn trong khoảng 15-20 phút. 
4. Lột mặt nạ ra rồi dùng tay vỗ nhẹ hoặc massage mặt ở hai bên má để dưỡng chất thẩm thấu vào da sau đó rửa lại với nước. 
Dùng mặt nạ dưỡng da thường xuyên từ 2 đến 3 lần trong một tuần để mang lại kết quả tốt nhất.

Thông tin bảo quản : Nơi khô ráo thoáng mát','43','32000',N'Việt Nam','LSP07'
EXEC sp_AddSP N'Bộ 4 mặt nạ Matrix Dừa Tươi gói 30g + tặng kèm 1 mặt nạ nha đam 30g',N'Mô tả
Công dụng và đặc điểm của sản phẩm: Mặt nạ Matrix Lecos Dừa Tươi được kết hợp từ chiết xuất dừa tươi và các dưỡng chất giúp dưỡng ẩm, làm dịu da và duy trì làn da khỏe mạnh, trắng sáng.
Thành phần sản phẩm : Water, Glycerin, Sodium PCA, Hydroxyethylcellulose, Cocos Nucifera Fruit Extract, Phenoxyethanol, Polysorbate 20, Parfum.
Hướng dẫn sử dụng :
1. Sau khi rửa mặt sạch, lấy mặt nạ ra khỏi bao bì. 
2. Đắp mặt nạ lên mặt bắt đầu từ vùng trán. 
3. Thư giãn trong khoảng 15-20 phút. 
4. Lột mặt nạ ra rồi dùng tay vỗ nhẹ hoặc massage mặt ở hai bên má để dưỡng chất thẩm thấu vào da sau đó rửa lại với nước. 
Dùng mặt nạ dưỡng da thường xuyên từ 2 đến 3 lần trong một tuần để mang lại kết quả tốt nhất.
Thông tin bảo quản : Nơi khô ráo thoáng mát','11','32000',N'Việt Nam','LSP07'
EXEC sp_AddSP N'Mặt nạ giấy Holika Holika chiết xuất trà xanh Pure Essence Mask sheet 23ml',N'Công dụng:
Chiết xuất từ lá trà xanh giúp cung cấp độ ẩm, làm se khít lỗ chân lông, giúp bạn có được một làn da mịn màng, tươi trẻ.
Sản phẩm dành riêng cho làn da dầu mụn nên vô cùng dịu nhẹ và an toàn với làn da.
Thành phần:
Water, Glycerin, Dipropylene Glycol, Betaine, Polyglyceryl-10 Laurate, Butylene Glycol, Centella Asiatica Extract, Paeonia Suffruticosa Root Extract, 1,2-Hexanediol, Allantoin, Panthenol, Chamomilla Recutita (Matricaria) Flower Extract, Camellia Sinensis Leaf Extract, Arginine, Carbomer, Glyceryl Caprylate, Xanthan Gum, Ethylhexylglycerin, Citrus Aurantium Bergamia (Bergamot) Peel Oil, Artemisia Vulgaris Extract, Viola Tricolor Extract Lavandula Angustifolia (Lavender) Flower Extract, Centaurea Cyanus Flower Extract, Madecassoside, Disodium EDTA...
Hướng dẫn sử dụng:
Làm sạch da với sữa rửa mặt và toner.
Tách nhẹ mép bao gói và lấy miếng mặt nạ đắp lên mặt. Sau đó cân chỉnh sao cho vừa khớp với khuôn mặt.
Thư giãn trong 10 – 20 phút và gỡ bỏ mặt nạ.
Vỗ nhẹ da để các tinh chất còn thừa thấm đều vào da.
Sử dụng đều đặn mỗi tuần ít nhất từ 3-4 lần để đạt hiệu quả như mong muốn.
Tránh bôi vào mắt, vết thương hở.
Bảo quản nơi khô ráo, thoáng mát, tránh ánh nắng trực tiếp và nhiệt độ cao.
Để xa tầm tay trẻ em.','3','25000',N'Hàn Quốc','LSP07'
EXEC sp_AddSP N'Mặt nạ JMSolution Active Golden Caviar Nourishing Mask 30ml',N'Thành phần:
- Với thành phần chính chiết xuất trứng cá tằm giàu dưỡng chất như khoáng chất, Acid Amin và các Axit béo cần thiết có tác dụng như cấp ẩm chuyên sâu, dưỡng ẩm khắc phục da khô ráp.
- Ngoài ra, mặt nạ còn có chứa thánh phần Vàng 24k giúp phục hồi, tái tạo da, nuôi dưỡng làn da tràn đầy sức sống.
Công dụng:
- Có hiệu quả cải thiện độ săn chắc và đàn hồi của da, tăng cường độ săn chắc và duy trì làn da khỏe mạnh.
- Chống lão hóa, nuôi dưỡng làn da khỏe mạnh, loại bỏ các tế bào da khô, bong tróc và giảm cảm giác căng cứng.
- Chiết xuất Caviar - kích hoạt sản xuất collagen, số lượng giảm dần theo tuổi tác, phục hồi độ đàn hồi và độ săn chắc của da. 
- Cung cấp dinh dưỡng tích cực và trẻ hóa da, làm mờ nếp nhăn trên khuôn mặt.
- Vàng 24K có đặc tính bảo vệ da khỏi bức xạ cực tím, làm sáng sắc tố, tăng cường mức độ thâm nhập của các thành phần hoạt động vào các lớp sâu.','77','39000',N'Hàn Quốc','LSP07'
EXEC sp_AddSP N'Mặt nạ tinh chất Sakura & Hyarulon căng mướt da Garnier Hydra Bomb Sarura White Serum Mask 28g',N'Thương hiệu: Garnier
Sức mạnh của 1 tuần sử dụng tinh chất dưỡng da (serum) trong 1 chiếc mặt nạ. 
Sự kết hợp của tinh chất Hoa anh đào Nhật Bản giúp sáng rạng rỡ & Hyaluronic Acid
Hoa anh đào  giúp da lấy lại vẻ sáng hồng rạng rỡ và căng mướt.
Thiết kế mặt nạ riêng biệt ôm trọn đường nét trên khuôn mặt người châu Á.
Khuyến khích sử dụng 3 lần mỗi tuần.
Kiểm nghiệm bởi chuyên gia da liễu. An toàn trên da nhạy cảm.','9','25000',N'Nhật Bản','LSP07'

select * from SANPHAM WHERE MALSP='LSP07'

--BẢNG SP
-- dưỡng da
-- Kem chống nắng
EXEC sp_AddSP N'Gel chống nắng cho da nhạy cảm Sunplay Skin Aqua Mild Care Gel',N'Đặc tính
- Chống nắng hiệu quả với SPF50 +, PA +++ giúp ngăn ngừa đen sạm, nám, tàn nhang, cháy nắng... Giữ ẩm tối ưu cho da khi tiếp xúc với ánh nắng. Công thức chuyên biệt, được kiểm nghiệm an toàn & dịu nhẹ cho da nhạy cảm, dễ bị mụn.
- Công thức 5 KHÔNG: cồn, dầu khoáng, paraben, hương liệu, chất tạo màu.
- Phù hợp mọi loại da, đặc biệt da nhạy cảm
Công dụng
- Công nghệ Watery Capsule: chứa các hạt chống nắng được bao bọc trong lớp màng nước giúp phản xạ đa chiều tia UVA/UVB.
- Ceramide: tăng cường hàng rào bảo vệ da & hạn chế mất nước.
- Alpha-Glucan Oligosaccharide: Làm dịu và cải thiện hàng rào sinh học bảo vệ da','7','119000',N'Nhật Bản','LSP08'
EXEC sp_AddSP N'Kem Chống Nắng NATURAL SUN ECO SUPER PERFECT SUN CREAM SPF50+ PA+++ 80ml',N'Sử dụng kem chống nắng Natural Sun Eco Super Perfect Sun Cream 50+PA+++ sẽ tạo nên lớp bảo vệ da tối ưu trước ánh nắng mặt trời, ngăn ngừa sự lão hóa sớm và những tổn thương bên trong da. Đây là sản phẩm mới toanh của Natural Sun Eco, sử dụng tinh chất từ mầm hoa Hướng dương, quả mọng Acai berry và hạt Argan làm thành phần chính với khả năng chăm sóc và bảo vệ sinh khí của làn da.
Chất kem Natural Sun Eco Super Perfect Sun Cream 50+PA+++ rất mỏng mịn, khi lên da sẽ tiệp vào trong rất nhanh, tránh sự bết rít đồng thời ngăn ngừa dầu tiết ra. Nhờ đó, sản phẩm ứng dụng thoải mái trên da mà không gây tình trạng tắc lỗ chân lông. Bên cạnh đó, kem chống nắng Super Perfect Sun Cream 50+PA+++ được ứng dụng như một kem lót, vừa bổ sung dưỡng chất ngăn ngừa lão hóa da, vừa tạo lớp nền mỏng mịn cho làn da thêm sức sống.
Natural Sun Eco là thương hiệu ứng dụng quy định nghiêm ngặt về mỹ phẩm hữu cơ thiên nhiên và quy trình sản xuất, đóng gói bởi ECOCERT – một tổ chức nổi tiếng tại Pháp. Do đó, sản phẩm an toàn cho mọi làn da, kể cả da nhờn hay da nhạy cảm khó tính','787','559000',N'Hàn Quốc','LSP08'
EXEC sp_AddSP N'Kem Chống Nắng Kiểm Soát Nhờn NATURAL SUN ECO NO SHINE HYDRATING SUN CREAM SPF50 PA+++ 100ml',N'Sản phẩm chống nắng dạng gel tươi mát, mang đến cảm giác nhẹ và mát cho làn da, hoàn toàn không gây cảm giác dính rít suốt ngày dài. Đây là sản phẩm đa chức năng, vừa sử dụng để chống nắng bảo vệ da, ngăn ngừa tình trạng sạm đen và lão hóa sớm cho da, vừa có thể sử dụng làm bước lót trang điểm tiện dụng
Chỉ số chống nắng SPF40 PA+++ bảo vệ da an toàn khỏi tác hại của tia UV lên đến 90%. Công thức kiểm soát nhờn mạnh mẽ nhờ thành phần hạt chia và hạt bông cotton. Tăng khả năng giữ ẩm gấp 10 lần, đồng thời song song vừa cân bằng nước vừa hấp thu dầu nhờn dư thừa. Mang đến bề mặt da khô ráo, mịn lỗ chân lông, sáng tông da nhưng không mang lại cảm giác nặng mặt. Kết hợp thành phần dưỡng da chống oxy hóa từ mầm hoa hướng dương giúp tăng cường rào chắn bảo vệ da khỏi tác hại của ánh nắng mặt trời và môi trường.','3','889000',N'Hàn Quốc','LSP08'
EXEC sp_AddSP N'Sữa Chống Nắng Hạ Nhiệt Làn Da NATURAL SUN ECO ICE AIR PUFF SUN SPF50+PA+++ 100ml',N'Kem chống nắng natural sun Eco ice air puff sun SPF50+ PA+++ đã được kiểm nghiệm về an toàn chất lượng bởi ECO-CERT – một tổ chức quy định nghiêm ngặt về mỹ phẩm hữu cơ thiên nhiên. Sản phẩm không chứa PABA, không có Paraben, không hương liệu, an toàn cho làn da.
Công thức dạng sữa mịn, dịu nhẹ dễ dàng thoa lên da nhờ bông mút. Tạo lớp kem chống nắng mỏng nhẹ tự nhiên nhất. Thành phần giàu dưỡng ẩm, giảm nguy cơ mất độ ẩm trong da khi tiếp xúc với nhiệt độ nóng.','7','715000',N'Hàn Quốc','LSP08'
EXEC sp_AddSP N'Kem Chống Nắng Lâu Trôi NATURAL SUN ECO POWER LONG-LASTING SUN CREAM SPF50+ PA+++ 50ml',N'Natural sun ECO power long-lasting sun cream SPF50+ PA+++ là kem chống nắng đa năng được yêu thích nhất tại The Face Shop. Sản phẩm vừa chống nắng bảo vệ da tối đa nhờ chỉ số SPF50+, vừa có thể sử dụng làm kem lót trang điểm hoàn hảo.Hơn thế, kem chống nắng natural sun có khả năng chống thấm nước, thách thức mồ hôi, dầu nhờn. Ngăn ngừa được cả 2 tia UVA và UVB, giảm thiểu tình trạng sạm da, chống lão hóa sớm. Kem có kết cấu nhẹ, màu da tự nhiên, thoa lên da thẩm thấu nhanh không gây bết dính. Đồng thời giúp điều chỉnh sắc màu da sáng mịn tự nhiê','9','655000',N'Hàn Quốc','LSP08'

select * from SANPHAM WHERE MALSP='LSP08'

--BẢNG SP
-- chăm sóc cơ thể
--Sữa tắm
EXEC sp_AddSP N'Sữa Tắm Dưỡng Ẩm Hương Nước Hoa On The Body Secret Jade 500G',N'Lấy nguồn cảm hứng từ nước hoa với hương thơm bất tận thương hiệu ON THE BODY tạo ra bộ sưu tập Sữa tắm hương nước hoa cao cấp
Sữa tắm hương nước hoa ON THE BODY là sự hòa quyện tinh tế giữa các tầng hương tinh dầu, thảo mộc, trái cây, gỗ, xạ hương cùng các hương hoa, mang lại cho một làn da mềm mại, mịn màng với hương thơm ngây ngất thật quyến rũ
Sữa tắm On The body Secret Jade nhẹ nhàng làm sạch da với bọt kem mịn, duy trì độ ẩm tối ưu cho làn da. Hương hoa hồng Bulgaria nồng nàn hòa quyện cùng hương trà trắng thanh khiết xen lẫn hương gỗ tuyết tùng ấm áp lan tỏa cảm giác thư giãn trong làn hương nước hoa dịu ngọt , tao nhã.
Hướng dẫn sử dụng
• Làm ướt cơ thể với nước
• Lấy 1 lượng sữa tắm vừa đủ, tạo bọt với bông tắm hoặc thoa trực tiếp lên cơ thể
• Massage nhẹ nhàng để lấy đi bụi bẩn và tế bào chết
• Xả sạch với nước sạch','78','150000',N'Hàn Quốc','LSP09'
EXEC sp_AddSP N'SỮA TẮM GUARDIAN BƯỞI TƯƠI MÁT 1000ML',N'Công dụng: Công thức đặc biệt từ chiết xuất từ Bưởi và Glycerin tự nhiên, sữa tắm giúp nhẹ nhàng làm sạch da toàn thân, mang lại làn da sạch sẽ và mịn màng, mềm mại cùng hương thơm thư thái.
Công thức không chứa xà phòng, lành tính với da.
Hướng dẫn sử dụng: Lấy một lượng nhỏ vào lòng bàn tay hoặc bông tắm, tạo bọt với nước và mát-xa nhẹ nhàng trên da, sau đó rửa sạch
Lưu ý: Chỉ sử dụng ngoài da. Tránh xa tầm tay trẻ em. Tránh tiếp xúc với mắt và vùng da bị tổn thương. Nếu dính vào mắt, hãy rửa sạch với nước. Ngưng sử dụng và tham khảo ý kiến của bác sĩ nếu da bị kích ứng.
Bảo quản: Bảo quản nơi khô ráo, thoáng mát, tránh ánh nắng mặt trời.
Thành phần cấu tạo: AQUA, SODIUM LAURETH SULFATE, COCAMIDOPROPYL BETAINE, SODIUM CHLORIDE, PALM KERNELAMIDE DEA, GLYCERIN, POLYQUATERNIUM-7, PARFUM, CITRIC ACID, TETRASODIUM EDTA, BENZOPHENONE-4, CITRUS GRANDIS (GRAPEFRUIT) FRUIT EXTRACT, METHYLCHLOROISOTHIAZOLINONE,  METHYLISOTHIAZOLINONE, POTASSIUM SORBATE, SODIUM BENZOATE, CI 14700, CI 14720.
Dung tích: 1000ml','8','59000',N'Malaysia','LSP09'
EXEC sp_AddSP N'Sữa Tắm Purite Hoa Mẫu Đơn 850ml',N'Chi tiết sản phẩm:
Sữa tắm thiên nhiên Purite Hoa Mẫu Đơn nhẹ nhàng làm sạch và dưỡng ẩm da nhờ bơ đậu mỡ
- Hương tinh dầu từ hoa mẫu đơn, thuần khiết & lưu hương từ 2-4h.
- Chiết xuất bơ đậu mỡ: nuôi dưỡng, cấp ẩm cho làn da.
Cách sử dụng: - Cho sữa tắm lên bàn tay hay bông tắm, tạo bọt, thoa đều nhẹ nhàng toàn thân rồi tắm lại bằng nước sạch.','41','178000',N'Pháp','LSP09'
EXEC sp_AddSP N'Gel tắm và làm sạch dịu nhẹ cho da thường và da khô nhạy cảm Bioderma Atoderm Gel Douche - 500ml',N'ĐỘ AN TOÀN: 
Không gây kích ứng
Không chứa cồn và paraben 
Không chứa xà phòng và chất tạo màu
Dung nạp tối ưu cho da
HƯỚNG DẪN SỬ DỤNG:
- Dùng 1 - 2 lần mỗi ngày
- Lấy một lượng vừa đủ ra tay hoặc bông tắm, massage nhẹ nhàng trên da ẩm sau đó làm sạch với nước rồi nhẹ nhàng lau khô da. 
- Dùng để làm sạch cho mặt và vùng da cơ thể.
THÀNH PHẦN: 
AQUA/WATER/EAU, SODIUM LAURETH SULFATE , COCO-BETAINE, SODIUM LAUROYL SARCOSINATE, GLYCERIN, METHYLPROPANEDIOL, MANNITOL, XYLITOL, RHAMNOSE, FRUCTOOLIGOSACCHARIDES, COPPER SULFATE, SODIUM CHLORIDE, COCO-GLUCOSIDE, GLYCERYL OLEATE, DISODIUM EDTA, CAPRYLOYL GLYCINE, CITRIC ACID, SODIUM HYDROXIDE, FRAGRANCE (PARFUM). 
Dung tích: 500ml','12','490000',N'Pháp','LSP09'
EXEC sp_AddSP N'Sữa Tắm Monsavon Dịu Nhẹ Chiết Xuất Từ Sữa Và Hoa Vani 1000ml',N'Công dụng:
Chiết xuất từ sữa và hoa vani
Cung cấp độ ẩm, nuôi dưỡng làn da mềm mịn
Tạo bọt nhẹ nhàng lấy đi bụi bẩn, tế bào chết
Hương thơm dịu nhẹ, thư giãn lưu hương lâu
Không tạo cảm giác nhờn rít, an toàn khi sử dụng','11','178000',N'Pháp','LSP09'

select * from SANPHAM WHERE MALSP='LSP09'

--BẢNG SP
-- chăm sóc cơ thể
--Tẩy tế bào chết cơ thể
EXEC sp_AddSP N'Tẩy tế bào chết Felina muối hồng Himalaya chiết xuất bơ 500g',N'Thành phần: Sodium Chloride, Aqua, Sodium Laureth Sulfate, Cocamidopropyl Betaine, Glycerin, Allantoin, PEG-40 Hydrogenated Castor Oil, Tocopheryl Acetate, Persea Gratissima Fruit Extract, Butylene Glycol, Parfum, Disodium EDTA, CI 19140, CI 45100
Công dụng: Hạt muối hồng Himalaya tự nhiên giúp làm sạch tế bào da chết khô sần, còn được dưỡng ẩm tăng cường nhờ chiết xuất quả bơ mang lại một làn da sáng mịn và mềm mại như được chăm sóc tại Spa.','78','76000',N'Việt Nam','LSP10'
EXEC sp_AddSP N'Tẩy tế bào chết Felina da nhạy cảm tinh chất sữa 300ml',N'Thành phần: Aqua, Prunus Armeniaca (Apricot) Kernel Powder, Trithanolamine, Sodium Laureth Sulfate, Cocamidopropyl Betaine, Glycerin, DMDM Hydantoin, Carbomer, Sodium Lactate, PEG-40 Hydrogenated Castor Oil, Tocopheryl Acetate, Lactis Serum Proteinum, Parfum.
Công dụng: Hạt mơ nghiền tự nhiên giúp tẩy tế bào da chết khô sần, còn được dưỡng ẩm tăng cường nhờ tinh chất sữa mang lại một làn da sáng mịn và mềm mại như được chăm sóc tại Spa.','7','92000',N'Việt Nam','LSP10'
EXEC sp_AddSP N'YOKO GOLD MUỐI TẨY TẾ BÀO CHẾT CÀ PHÊ HỘP 240G',N'Công dụng: Muối tắm Spa Lấy Tế Bào Chết Cà Phê Yoko Gold là sản phẩm kết hợp các Vitamin và dưỡng chất từ hạt cà phê tự nhiên giúp lấy đi da chết và làm liền các vết rạn da giúp da của bạn trở nên mềm mại và mịn màng. Ngoài ra sản phẩm còn bổ sung thêm Vitamin E và dầu Olive giúp làn da của bạn được dưỡng ẩm và trắng sáng sau khi sử dụng.
Hướng dẫn sử dụng: Làm ướt cơ thể với nước sạch, cho một lượng vừa đủ sản phẩm vào lòng bàn tay và thoa đều nhẹ nhàng theo hình vòng tròn lên cơ thể thư giãn khoảng 3 phút, sau đó làm sạch lại cơ thể với nước sạch.
Thành phần: Sodium Chloride, Aqua (Water), Sodium Laureth Sulfate, Glycerin, Sodium Lauryl Sulfate, Coffea Arabica Seed Powder, Parfum (Fragrance), Cocamide DEA, Olea Europaea Fruit Oil, Phenoxyethanol, Citric Acid, Niacinamide, Soluble Collagen, Ethylhexylglycerin, Tocopheryl Acetate, Butylene Glycol, CI 19140, CI 14720, Glycyrrhiza Glabra Root Extract, CI 42090.','14','79000',N'Thái Lan','LSP10'
EXEC sp_AddSP N'YOKO GOLD MUỐI TẨY TẾ BÀO CHẾT SỮA VÀ DƯA HẤU 350G',N'Công dụng: Muối tắm Spa tẩy tế bào chết Sữa + Dưa Hấu Yoko Gold là sự kết hợp giữa Sữa Hokkaido siêu đậm đặc và Dưa hấu, giàu Vitamin C và E giúp cho làn da thêm rạng rỡ, giảm sự xuất hiện của nếp nhăn do quá trình lão hóa. Ngoài ra, sản phẩm còn có tinh chất hạt Óc chó 100% tự nhiên giúp nhẹ nhàng lấy đi da chết hiệu quả cho làn da của bạn trở nên tươi trẻ và khỏe khoắn.
Hướng dẫn sử dụng: Làm ướt cơ thể với nước sạch, cho một lượng vừa đủ sản phẩm vào lòng bàn tay và thoa đều nhẹ nhàng theo hình tròn lên cơ thể, để trên da khoảng 3 phút , sau đó làm sạch lại cơ thể với nước sạch.
Thành phần: Sodium Chloride, Glycerin, Aqua (Water), Parfum (Fragrance), Juglans Regia Shell Powder, Sodium Laureth Sulfate, Sodium Lauryl Sulfate, Olea Europaea Fruit Oil, Propylene Glycol, Tocopheryl Acetate, Magnesium Ascorbyl Phosphate, CI 16035, Butylene Glycol, Milk Extract, Sodium Ferrocyanide, Benzyl Alcohol, Dehydroacetic Acid, Benzoic Acid, Sorbic Acid, Citrullus Lanatus Fruit Extract.','44','89000',N'Thái Lan','LSP10'
EXEC sp_AddSP N'YOKO GOLD MUỐI TẤM SPA DÂU TÂY DÂU TẰM 300G',N'Công dụng: Muối tắm Spa Dâu tây Dâu tằm Yoko là sự pha trộn của chiết xuất dâu tây và dâu tằm tự nhiên đặc tính làm săn chắc da và tẩy tế bào chết cho da. Ngoài ra, sản phẩm còn bổ sung Vitamin E và B3 có tác dụng làm sạch da, cho bạn một làm da đầy sức sống và rạng rỡ cùng hương thơm dễ chịu.
Hướng dẫn sử dụng: Làm ướt cơ thể với nước sạch, cho một lượng vừa đủ sản phẩm vào lòng bàn tay và thoa đều nhẹ nhàng theo hình tròn lên cơ thể, để trên da khoảng 3 phút , sau đó làm sạch lại cơ thể với nước sạch.
Thành phần: Sodium Chloride, Glycerin, Sodium Laureth Sulfate, Parfum (Fragrance), Olea Europaea (Olive) Fruit Oil, Niacinamide, Aqua (Water), CI 60730, Butylene Glycol, Tocopheryl Acetate, CI 16035, Fragaria Chiloensis Fruit Extract, Morus Alba Root Extract.','36','72000',N'Thái Lan','LSP10'

select * from SANPHAM WHERE MALSP='LSP10'

--BẢNG SP
-- chăm sóc cơ thể
--Sữa dưỡng thể
EXEC sp_AddSP N'COCOON BƠ DƯỠNG THỂ CÀ PHÊ ĐẮK LẮK 200ML',N'Những hạt cà phê Đắk Lắk đậm vị và thơm nồng nàn là nguồn cảm hứng, nguồn nguyên liệu dồi dào để Cocoon cho ra mắt các sản phẩm làm sạch da chết. Nhưng không dừng lại ở đó, chúng tôi còn khám phá ra, dầu được ép hạt cà phê thật sự là một nguồn “năng lượng vàng” rất giàu dưỡng chất giúp làm mềm, cung cấp năng lượng cho làn da đang mỏi mệt.
Dầu cà phê Đắk Lắk rất giàu cafein giúp chống oxy hoá, phục hồi và giữ lại độ ẩm cho làn da, làm đều màu da và mang đến sự khỏe khoắn, tươi mới. 
Kết hợp nguồn nguyên liệu đặc biệt này với các loại chất béo thực vật khác từ thiên nhiên Việt Nam như: bơ ca cao Tiền Giang, dầu mù u, chúng tôi mang đến cho bạn một phiên bản dưỡng thể “đậm đặc”: • MỀM MỊN NHƯ BƠ • KẾT CẤU MỎNG NHẸ THẤM NHANH • PHÙ HỢP VỚI KHÍ HẬU NÓNG ẨM TẠI VIỆT NAM','12','195000',N'Việt Nam','LSP11'
EXEC sp_AddSP N'Kem dưỡng Tesori dOriente Lotus Hoa sen 300ml',N'Kem dưỡng thể hoa sen - Lotus Flowers không chỉ giúp bảo vệ da, bổ sung độ ẩm, tăng độ đàn hồi, mà còn mang lại hương thơm tinh khiết gợi cảm và sẽ lưu lại trên làn da bạn suốt cả ngày. Công thức dạng kem, giúp thẩm thấu và thấm sâu, bổ sung độ ẩm, tái tạo da, giúp da mềm mại và tươi mát. Phòng ngừa lão hóa da, làm mờ các vết nhăn, sạm, nám.
Sản phẩm được sản xuất tại Italy bởi thương hiệu nổi tiếng thế giới Tesori DOriente được đóng hộp với dung tích 300ml. Dùng 1 lần chắc bạn sẽ nhớ mãi với mùi hương quyến rũ và công hiệu của sản phẩm. 
Cách dùng kem dưỡng da đúng cách:
- Nên dùng kem dưỡng da tốt nhất vào buổi sáng và buổi tối sau khi tắm với 
- Dùng kem dưỡng da ngay từ tuổi dậy thì.
- Hạn chế sử dụng kem dưỡng da vùng cánh tay và ngực.
- Dùng đúng lượng kem vừa đủ.
- Cho một lượng nhỏ vào mu bàn tay, thoa nhẹ và đều khắp cơ thể.
- Không thoa kem dưỡng da với động tác vuốt, vỗ, kéo, miết mạnh, không sử dụng các động tác xoay tròn vì sẽ làm cho các sợi đàn hồi và sợi co giãn bị tác động mạnh, da dễ bị chùng, chảy xệ và xuất hiện nếp nhăn sớm.
- Khi thoa kem dưỡng, nên dùng ngón áp út thao tác áp- ấn nhẹ nhàng, vuốt hướng tâm để kích thích vòng tuần hoàn máu và hệ bạch huyết hoạt động hiệu quả giúp thải độc tố, bảo vệ da, giúp da thẩm thấu nhanh kem dưỡng.','12','300000',N'Italia','LSP11'
EXEC sp_AddSP N'Sữa Dưỡng Thể Purité Thư Giãn Hoa Mẫu Đơn 250ml',N'SẢN PHẨM VỚI CÁC THÀNH PHẦN VÀ CÔNG DỤNG:
+ Mang lại cho bạn làn da đẹp tự nhiên với hương thơm Hoa mẫu đơn phảng phất đầy quyến rũ đồng thời làm mềm và giữ ẩm cho da.
+ Thành phần dầu Oliu giúp nuôi dưỡng làn da ẩm mịn từ bên trong.
+ Arbutin giúp tác động làm hỗ trợ tái cấu trúc tế bào, da căng mịn và khỏa mạnh hơn.
THIẾT KẾ SẢN PHẨM:
+ Dạng chai có vòi ấn, dung tích 250ml.
LOẠI DA PHÙ HỢP:
+ Phù hợp cho mọi loại da.
HƯỚNG DẪN SỬ DỤNG:
+ Lấy một lượng sản phẩm vừa đủ và thoa đều lên cơ thể. Nên sử dụng ngay sau khi tắm, khi da vừa được lau khô và còn đang ẩm để sản phẩm được hấp thu tốt nhất.
HƯỚNG DẪN BẢO QUẢN:
+ Bảo quản: nơi khô ráo thoáng mát.
+ Không bảo quản nơi có nhiệt độ quá cao hoặc quá thấp, tránh ánh sáng trực tiếp.
HẠN SỬ DỤNG:
+ 3 năm kể từ ngày sản xuất.
NGÀY SẢN XUẤT:
+ Xem trên bao bì sản phẩm.','36','115000',N'Việt Nam','LSP11'
EXEC sp_AddSP N'Gel dưỡng toàn thân chiết xuất lô hội 99% Holika Holika 250ml',N'Thành phần chính: Chiết xuất lô hội (99%), Ammonium Lauryl Sulfate, Cocamidopropyl, Betaine, Phenoxyethanol, Fragance, Soduim Chloride, Nelumbium Speciosum Flower Extract, Butylene Glycol, Nước, Ammonium Lảueth Sulfate…
Công dụng: Dưỡng ẩm cho da mặt và cơ thể: Dùng như kem dưỡng ẩm hoặc dùng làm mặt nạ ngủ; bôi lên vùng da cơ thể cần dưỡng ẩm như đầu gối, khuỷu tay, bàn tay,…
Làm dịu da: Làm dịu da bị cháy nắng, giảm sưng bọng và quầng thâm mắt.
Dưỡng tóc: Dùng như một loại kem xả giúp tóc suôn mượt hơn.
HDSD: Dưỡng ẩm cho da mặt và cơ thể: Lấy một lượng vừa đủ thoa trực tiếp lên vùng da cần dưỡng ẩm. Hoặc dùng như mặt nạ ngủ bằng cách lấy một lượng vừa đủ lên bông sau đó đắp lên toàn bộ khuôn mặt.
Làm dịu da: Lấy một lượng vừa đủ thoa lên vùng da bị cháy nắng. Hiệu quả hơn nếu để trong tủ lạnh.
Dưỡng tóc: Sau khi làm sạch tóc với dầu gội, trộn gel dưỡng lô hội với kem xả, thoa đều lên tóc rồi xả sạch với nước.
Chỉ tiêu kích ứng: Không gây kích ứng','54','160000',N'Hàn Quốc','LSP11'
EXEC sp_AddSP N'GEL NHA ĐAM MILAGANICS 300ML',N'THÀNH PHẦN
Gel Nha Đam Milaganics với thành phần là 98% tinh chất nha đam hoàn toàn tự nhiên, được bổ sung thêm các hoạt chất dưỡng da lành tính.
THÀNH PHẦN KHOA HỌC: 
Aloe Barbadensis Leaf Fruit Juice, Aqua, Glycerin, Tocopherol (vitamin E), Carbomer 940, Propylene Glycol, Triethanolamine,  Fragrance Alcohol, Citric Acid, Disodium EDTA, Potasium Sorbate, Diazolidinyl Urea.
CÔNG DỤNG
– Cung cấp độ ẩm cho da, làm mát da và bảo vệ da khỏi khô ráp: tinh chất Nha đam thiên nhiên có tác dụng giữ ẩm tuyệt vời, sẽ bảo vệ da khỏi tình trạng khô ráp, bong tróc vẩy da chết, đảm bảo làn da luôn được cấp nước đầy đủ
– Đẩy lùi lão hóa, giảm thiểu nếp nhăn và các dấu hiệu tuổi tác: vitamin E có trong Gel Nha đam Milaganics chính là tác nhân chống lão hóa tuyệt vời, sẽ mang lại cho bạn làn da căng tràn, tươi trẻ, đẩy lùi nếp nhăn cùng các dấu hiệu lão hóa.
– Dưỡng ẩm cho tóc và móng hiệu quả: Gel Nha đam Milaganics còn cung cấp cho tóc và móng độ ẩm nhất định, giúp móng và tóc luôn khỏe mạnh.
CÁCH DÙNG
– Dưỡng da: rửa mặt sạch, thoa một ít Gel Nha Đam lên mặt như kem dưỡng, không cần rửa lại
– Dưỡng thể: thoa đều lên da, và để tinh chất Gel Nha Đam thẩm thấu từ từ.
– Dưỡng móng: thoa Gel Nha Đam lên móng tay để giúp móng tay luôn khỏe mạnh
– Có thể bôi trực tiếp Gel Nha đam lên vùng da bị bỏng hoặc cháy nắng để làm dịu nhanh chóng','5','129000',N'Hàn Quốc','LSP11'

select * from SANPHAM WHERE MALSP='LSP11'

--BẢNG SP
-- chăm sóc cơ thể
--Kem dưỡng da tay
EXEC sp_AddSP N'KEM DƯỠNG DA TAY PALMERS MỀM MỊN 60G',N'Dưỡng ẩm sâu và cung cấp dưỡng chất giúp da mềm mịn, giúp loại bỏ và ngăn ngừa tình trạng da khô sần, nứt nẻ và bong tróc, giúp phục hồi da hư tổn ở bàn tay, khuỷu tay, đầu gối, bàn chân và gót chân. Với chiết xuất chính từ Bơ Cacao, dầu dừa, dầu cọ Châu Phi, Vitamin E giúp làm mềm những vùng da dễ khô nứt, phục hồi da hư tổn.
Thành phần chính: Theobroma Cacao Extract, Elaeis Guineensis Oil, Helianthus Annuus Sunflower Seed Oil, Cocos Nucifera Oil, Tocopherol.
HDSD: Dùng thoa dưỡng da 2 lần/ngày hoặc nhiều hơn khi thấy da tay quá khô. 
Chỉ tiêu kích ứng da: không đáng kể.
Bảo quản: Trong điều kiện nhiệt độ mát hoặc thông thường. Tránh ánh nắng trực tiếp.','54','112000',N'Mỹ','LSP12'
EXEC sp_AddSP N'Kem Dưỡng Tay Cung Cấp Ẩm DAILY PERFUMED HAND CREAM 10 SNOW COTTON',N'Công dụng
Món quà hằng ngày cho đôi tay thơm và mềm mại. Vận dụng khái niệm dưỡng da và nước hoa tạo ra sự mới mẻ với kết cấu mỏng nhẹ giúp đem lại đôi tay mềm và ẩm mịn.
Sản phẩm đa dạng mùi hương mang đến cho bạn nhiều sự lựa chọn: Trà xanh, mâm xôi và việt quất, hoa Tiare, Olive, hoa hồng, táo đỏ và lựu.
Thành phần
Được chiết xuất từ hoa cotton giúp làn da tay mềm mại và mịn màng kết hợp hyluronic acid giúp phục hồi độ ẩm hiệu quả cho làn da.
Hướng dẫn sử dụng
Thoa đều khắp tay và mát xa nhẹ nhàng để sản phẩm thẩm thấu vào da.','54','109000',N'Hàn Quốc','LSP12'
EXEC sp_AddSP N'Kem Dưỡng Tay Cung Cấp Ẩm RICH HAND V SOFT TOUCH HAND LOTION',N'CÔNG DỤNG CỦA RICH HAND V SOFT TOUCH HAND LOTION
Kem dưỡng da tay với khả năng dưỡng ẩm sâu và phục hồi độ mềm mượt cho đôi tay. Bên cạnh đó, sản phẩm còn mang đến hiệu quả chống oxi hoá để giúp đôi tay sáng mịn và hỗ trợ chống lão hóa cho làn da tay.
THÀNH PHẦN CHÍNH TRONG KEM DƯỠNG TAY CUNG CẤP ẨM
Kem dưỡng tay với thành phần chiết xuất từ tinh dầu hạnh nhân, dầu marula và dầu bơ chắc chắn sẽ mang lại đôi bàn tay mềm mại, mịn màng.
HƯỚNG DẪN SỬ DỤNG RICH HAND V SOFT TOUCH HAND LOTION
Bạn cho một lượng kem dưỡng vừa đủ vào lòng bàn tay rồi massage nhẹ nhàng 2 bàn tay với nhau. Có thể sử dụng nhiều lần trong ngày.','25','229000',N'Hàn Quốc','LSP12'
EXEC sp_AddSP N'Kem Dưỡng Da Tay Hương Nước Hoa THEFACESHOP PERFUMABLE HAND CREAM #01 GREEN MUSK ( Xạ Hương Thanh Mát)',N'CÔNG DỤNG
- Giúp làm ẩm và mềm da, cho bạn là da mềm mại, mịn màng, không bị khô ráp hay bong tróc.
- Ngăn ngừa nếp nhăn da, nuôi dưỡng làn da khỏe mạnh.
- Mùi hương nhẹ nhàng nữ tính, quyến rũ và cuốn hút.
- Thẩm thấu nhanh, không gây bết dính khó chịu.
- Cung cấp độ ẩm, cải thiện những vùng da khô một cách rõ rệt.
- Bảo vệ da khỏi các tia cực tím của ánh nắng mặt trời.
HƯỚNG DẪN SỬ DỤNG
- Lấy 1 lượng kem dưỡng da tay vừa đủ và thoa đều 2 tay
- Massage nhẹ nhàng để dưỡng chất được thẩm thấu tốt hơn.
- Có thể sử dụng nhiều lần trong ngày, đặc biệt là vào mùa đông da sẽ hay bị khô và thiếu ẩm.
- Sử dụng sau khi tay được được lau khô.','37','429000',N'Hàn Quốc','LSP12'
EXEC sp_AddSP N'Kem dưỡng da tay DAILY PERFUMED HAND LOTION ORCHID 300ml',N'CÔNG DỤNG CỦA KEM DƯỠNG DA TAY DAILY PERFUMED HAND LOTION ORCHID
Kem dưỡng da tay hương nước hoa, chứa chiết xuất từ hoa lan giàu chất chống oxi hóa và hiệu quả trong việc cung cấp và phục hồi độ ẩm, cho làn da tay mềm mại và mịn màng.
Kem dưỡng với chất kem mềm mịn mỏng nhẹ giúp kem thẩm thấu và hấp thụ nhanh hơn. Thành phần an toàn với hương thơm dịu nhẹ.
THÀNH PHẦN TRONG DAILY PERFUMED HAND LOTION ORCHID
Kem dưỡng da tay hương nước hoa, chứa chiết xuất từ hoa lan giàu chất chống oxi hóa.
HƯỚNG DẪN SỬ DỤNG KEM DƯỠNG DA TAY
Bạn rửa tay sạch bằng nước ấm, sau đó lau khô với khăn mềm.
Bạn lấy một lượng kem vừa đủ ra lòng bàn tay, thoa đều khắp tay và nhẹ nhàng massage để các dưỡng chất thấm đều trong da.','61','289000',N'Hàn Quốc','LSP12'

select * from SANPHAM WHERE MALSP='LSP12'

--BẢNG SP
-- chăm sóc cơ thể
--Sản phẩm khử mùi
EXEC sp_AddSP N'Lăn khử mùi Hồng Mộc Lan Refre Natural Rosa Magnolia 40ml',N'Đặc điểm sản phẩm: 
- Khử mùi cơ thể và giảm tiết mồ hôi hiệu quả.
- Thích hợp cho dàn da nhạy cảm nhờ thành phần không chứa cồn, không Paraben và không muối nhôm.
- Refre Whitening Natural - Lăn khử mùi chiết xuất từ thiên nhiên gồm mùi: Rosa Magnolia – Hồng mộc lan 
Công dụng:  Chiết xuất Cỏ Đuôi Ngựa và Tinh dầu Xô Thơm giúp kháng khuẩn, khử mùi cơ thể và giảm tiết mồ hôi hiệu quả.
• Các hạt phấn cực mịn từ Cây Tre giúp hút ẩm liên tục, giữ cho vùng da dưới cánh tay luôn mềm mịn, khô thoáng.
• Chiết xuất Aloe Vera (Nha Đam) và Licorice (Cam Thảo) giúp dưỡng trắng da.
• Đặc biệt thích hợp cho làn da nhạy cảm nhờ thành phần không chứa cồn,  không Paraben, không muối Nhôm, không gây kích ứng da.','12','46000',N' Việt nam','LSP13'
EXEC sp_AddSP N'Nivea Lăn Khử Mùi Nữ Serum Hồng Hokaido 50ml',N'Với dẫn xuất Vitamin C (Sodium Ascorbyl Phosphate) kết hợp chiết xuất hoa anh đào, tạo nên công thức cấp ẩm chuyên biệt, CHO CẢM NHẬN LÀN DA SĂN CHẮC TỐI ƯU. Giúp ngăn mùi & giảm tiết mồ hôi đến 48h. Không chứa cồn.
Hướng dẫn sử dụng: Lăn lên vùng da dưới cánh tay mỗi ngày. Sử dụng tốt nhất ngay sau khi tắm. Đợi cho sản phẩm khô rồi mặc áo.
Điều kiện bảo quản:
Bảo quản ở nhiệt độ thấp hơn 35°C (nơi thoáng mát), tránh tiếp xúc trực tiếp với ánh mặt trời.
Luôn đọc kỹ thông tin in trên bao bì. Sử dụng theo hướng dẫn.','120','93000',N'Việt Nam','LSP13'
EXEC sp_AddSP N'Nivea Xịt Khử Mùi Nữ Serum Sakura 150ml',N'Với dẫn xuất Vitamin C (Sodium Ascorbyl Phosphate) kết hợp chiết xuất hoa anh đào, tạo nên công thức cấp ẩm chuyên biệt, CHO CẢM NHẬN LÀN DA SĂN CHẮC TỐI ƯU. Giúp ngăn mùi & giảm tiết mồ hôi đến 48h. Không chứa cồn.
Hướng dẫn sử dụng: Lắc kỹ trước khi sử dụng. Giữ đầu xịt cách vùng da dưới cánh tay 15 cm và xịt đều nhẹ.
Điều kiện bảo quản:
Bảo quản ở nhiệt độ thấp hơn 35°C (nơi thoáng mát), tránh tiếp xúc trực tiếp với ánh mặt trời.
Luôn đọc kỹ thông tin in trên bao bì. Sử dụng theo hướng dẫn.','24','90000',N'Việt Nam','LSP13'
EXEC sp_AddSP N'Gel Khử Mùi Gillette Arctic Ice 107G',N'Thành phần : Xem mục Active ingredients và Inactive ingredients trên sản phẩm
Công dụng: Ngăn mùi cơ thể và giảm tiết mồ hôi, đem lại mùi thơm dễ chịu cho vùng da dưới cánh tay .Hướng dẫn sử dụng : Mở nắp, gỡ miếng chặn bên trong, xoay nắm vặn ở đáy chai để lấy sáp, lăn nhẹ trên vùng da dưới cánh tay, 
Lưu ý : Chỉ dùng ngoài da, tránh dùng trên vết thương hở, tránh xa tầm tay trẻ em.
Điều kiện bảo quản: bảo quản nơi khô mát, tránh để trực tiếp dưới ánh sáng mặt trời.','14','106000',N'Mỹ','LSP13'
EXEC sp_AddSP N'Lăn Khử Mùi Nam Ngăn Mồ Hôi Adidas 6 In 1 40ml',N'Công dụng: Lăn khử mùi nam ngăn mồ hôi Adidas 6 in 1 với công thức độc đáo 6 trong 1 khô thoáng, kháng khuẩn, chống ẩm ướt, tươi mát, sáng da, giảm ố vàng giúp bảo vệ làn da suốt 48 giờ. Sản phẩm không chứa cồn, an toàn cho làn da.
Thành phần: Aqua/Water, Aluminum Zirconium Octachlorohydrex Gly, Hydroxypropyl Starch Phosphate, Parfum/Fragrance, Disodium EDTA, Limonene, Hexyl Cinnamal, Butylphenyl Methylpropional, Benzyl Salicylate, Caesalpinia Spinosa Gum, Hydrogenated Polydecene, Citronellol, Linalool, Sorbitan Sesquioleate, Alpha-Isomethyl Ionone, Sucrose Stearate, Geraniol, Amyl Cinnamal, Silica, Ethylhexylglycerin, Gossypium Herbaceum (Cotton) Powder, BHT
HDSD: Lắc đều, lăn đều vào vùng da duới cánh tay
Cách bảo quản: Để nơi khô ráo, thóang mát, tránh ắng nắng, tránh xa tầm taytrẻ em','57','69000',N'Mỹ','LSP13'

select * from SANPHAM WHERE MALSP='LSP13'

--BẢNG SP
-- chăm sóc cơ thể
--Nước hoa
EXEC sp_AddSP N'Lovilla Nước Hoa Hương Trái Cây 100ml',N'Đặc điểm nổi bật:
- Nươc hoa Lovillea với sự kết hợp của hương đào tự nhiên tươi mát cùng hương thơm hoa hồng quyến rũ dịu dàng
Cách dùng:
- Cho một ít vào lòng bàn tay rồi thoa đều lên các vùng cơ thể mà bạn muốn
Lưu ý:
-Để xa tầm tay trẻ em.
-Chỉ sử dụng ngoài da.
-Tránh tiếp xúc với mắt, nếu dính vào mắt, rửa sạch ngay bằng nước sạch.
-Không sử dụng trên các vùng da bị tổn thương hoặc khi da bị kích ứng.
-Bảo quản: nơi thoáng mát, tránh ánh nắng trực tiếp.','21','19000',N'Việt Nam','LSP14'
EXEC sp_AddSP N'Nước hoa Cindy Bloom Aroma Flower 50ml',N'Nước hoa Cindy Bloom - Aroma Flower
Hướng dẫn sử dụng: Giữ cách thân khoảng 8cm và xịt lên cơ thể
Lưu ý: Dễ cháy. Không được xịt vào mắt. Không để nơi có nhiệt độ cao. Tránh ánh sáng trực tiếp. Để ngoài tầm tay trẻ em.
Thành phần: Alcohol, Fragrance, Water, Cl 16255
Hạn sử dụng: 3 năm kể từ NSX in trên bao bì
Phong cách: Ngọt ngào, nữ tính
Độ lưu hương: 6 - 8 tiếng
Các tầng hương:
Hương đầu: dưa hấu, đào
Hương giữa: hoa anh thảo, táo gai, hồng nhung
Hương cuối: hổ phách, tuyết tùng, xạ hương trắng
"Cô gái thành thị luôn khao khát hòa mình vào thiên nhiên để tìm ra nguồn cảm hứng sống mới thì không thể bỏ qua hương 
thơm ngọt lành từ trái cây mọng nước kết hợp với hương hoa anh thảo, táo gai và hồng nhung, dịu dàng pha chút năng động. 
Đọng lại ấm áp, ngọt ngào bởi hổ phách, tuyết tùng và xạ hương trắng"','14','189000',N'Nhật Bản','LSP14'
EXEC sp_AddSP N'Nước hoa Cindy Bloom Fresh Ocean 50ml',N'Nước hoa Cindy Bloom - Fresh Ocean
Hướng dẫn sử dụng: Giữ cách thân khoảng 8cm và xịt lên cơ thể
Lưu ý: Dễ cháy. Không được xịt vào mắt. Không để nơi có nhiệt độ cao. Tránh ánh sáng trực tiếp. Để ngoài tầm tay trẻ em.
Thành phần: Alcohol, Fragrance, Water, Cl 42090.
Hạn sử dụng: 3 năm kể từ NSX in trên bao bì
Phong cách: Trẻ trung, năng động
Độ lưu hương: 6 - 8 tiếng
Các tầng hương:
Hương đầu: cam biagrade, chanh
Hương giữa: hoa hồng, linh lan, hoa nhài
Hương cuối: hoa diên vĩ, gỗ tuyết tùng, gỗ đàn hương
"Nước hoa Cindy Bloom – Fresh Ocean dành cho những ai thích sự tự do, khám phá thiên nhiên và vẻ đẹp của biển, gây ấn tượng bởi hương thơm tươi mát của cam quýt. Lớp hương giữa phảng phất hương thơm nhẹ nhàng từ hoa hồng, linh lan và hoa nhài. Hương cuối kết hợp ăn ý với hương gỗ tuyết tùng, tạo nên mùi hương êm ái, lãng mạn, đầy nhịp điệu của biển cả"','7','189000',N'Nhật Bản','LSP14'
EXEC sp_AddSP N'Nước hoa Cindy Bloom Urban Vibes 50ml',N'Nước hoa Cindy Bloom - Urban Vibes
Hướng dẫn sử dụng: Giữ cách thân khoảng 8cm và xịt lên cơ thể.
Lưu ý: Dễ cháy. Không được xịt vào mắt. Không để nơi có nhiệt độ cao. Tránh ánh sáng trực tiếp. Để ngoài tầm tay trẻ em.
Thành phần: Alcohol, Fragrance, Water.
Hạn sử dụng: 3 năm kể từ NSX in trên bao bì
Phong cách: Tự tin, cuốn hút
Độ lưu hương: 6 - 8 tiếng
Các tầng hương:
Hương đầu: cam bergamot, quả lý chua
Hương giữa: hoắc hương, hoa hồng
Hương cuối: xạ hương, vani
Nước hoa Cindy Bloom – Urban Vibes kết hợp nhịp nhàng hương thơm của hương trái cây, hoa hồng và hoắc hương, mang đậm chất thành thị, 
hiện đại và quen thuộc. Vương lại da diết bởi xạ hương kết hợp ngọt ngào của vani mang đến cảm xúc nồng nàn, mạnh mẽ cho những cô gái thành thị 
đầy sôi nổi, toát lên vẻ đẹp tự tin, quyến rũ, bí ẩn, cuốn hút.','6','189000',N'Nhật Bản','LSP14'
EXEC sp_AddSP N'Combo Làm Sạch Da & Nước Hoa SOUL SCRET BLOSSOM',N'**Combo gồm 03 sản phẩm:
01 Nước Hoa SOUL SCRET BLOSSOM
01 Bộ Sản Phẩm Làm Sạch Sâu YEHWADAM DEEP CLEANSING KIT (50ml x2)
01 Túi Đựng Sản Phẩm Trang Điểm FMGT SIGMOISTURIZINGNATURE POUCH','6','499000',N'Hàn Quốc','LSP14'

select * from SANPHAM WHERE MALSP='LSP14'

--BẢNG SP
-- Chăm sóc tóc
--Dầu gội
EXEC sp_AddSP N'DẦU GỘI KERASYS ADVANCE VOLUME LÀM SẠCH GÀU 600ML',N' Dầu gội giúp chăm sóc chuyên sâu với thành phần dưỡng chất đậm đặc: Bổ sung collagen giúp liên kết cấu trúc tóc đã bị hư tổn, giúp sợi tóc dai, chắc khỏe trở lại, giảm gãy rụng.
- Collagen giúp lấy lại sức sống cho mái tóc, giúp tóc trở nên mềm mại, suôn mượt và bồng bềnh hơn.
- Thích hợp cho tóc yếu, dễ gãy rụng.
Thành phần: water, sodium laureth sulfate, cocamidopropyl betaine, sodium chloride, sodium benzoate, sodium cocoyl alaninate, lauryl hydroxysultaine, sodium laureth-6 carboxylate, sodium cocoamphoacetate, hexylene glycol, fragrance, cocamide mea, laureth-10, benzoic acid, sodium laureth sulfate, glycol distearate, dimethicone, laureth-23, laureth-3, xanthan gum, phenoxyethanol, sodium benzoate, guar hydroxypropyltrimonium chloride, trihydroxystearin, cetyl alcohol, synthetic fluorphlogopite, titanium dioxide (ci 77891), cocamide mea, amodimethicone, trideceth-10, propylene glycol, butylene glycol, panthenol, cetrimonium chloride, collagen (from pork derived skin source), dodecenyl oleylsuccinamide, hydrolyzed soy protein, citric acid, ammonium chloride.
HDSD: làm ướt tóc, lấy một lượng dầu vừa đủ ra lòng bàn tay và thoa đều lên tóc, massage từ da đầu
cho đến ngọn tóc trong vài phút sau đó xả sạch bằng nước
Lưu ý: Để xa tầm tay trẻ em. Tránh tiếp xúc với mắt. Nếu dính vào mắt, không được chà xát, phải rửa ngay
với nước. Ngưng sử dụng ngay khi có các biểu hiện kích ứng. Bảo quản: nơi khô mát, tránh tiếp xúc trực
tiếp ánh sáng mặt trời và nơi có nhiệt độ cao','32','230000',N'Nhật Bản','LSP15'
EXEC sp_AddSP N'Dầu Gội Pantene Micellar Làm Sạch Và Dưỡng Ẩm Chiết Xuất Hoa Súng 530ml',N'Chứa chiết xuất hoa súng cùng công nghệ chăm sóc da Micellar làm sạch nhẹ nhàng mà không làm khô da
Không chứa Silicone - nhân tố tạo bọt, giúp tóc suôn mượt nhưng dễ gây kích ứng da đầu, khiến tóc yếu và dễ gãy rụng
Hướng dẫn sử dụng 
Làm ướt tóc, lấy một lượng dầu gội vừa đủ tạo bọt và xoa nhẹ nhàng lên tóc và da đầu, mát-xa và xả sạch lại với nước','63','159000',N'Việt Nam','LSP15'
EXEC sp_AddSP N'Dầu gội X-Men Go DETOX Than Tre Hoạt Tính 630G',N'Dầu gội Detox dành cho nam giới đầu tiên tại Việt Nam.
Với Than tre hoạt tính không chỉ giúp sạch gàu hiệu quả, loại bỏ 5 tác nhân ô nhiễm gây ra gàu, ngứa mà còn nuôi dưỡng để tóc bồng bềnh mạnh mẽ.
Thành phần: Water, Sodium Laureth Sulfate, Dimethiconol, Cocamidopropyl Betaine, Perfume, Glycol Distearate, Cocamide MEA, Climbazole, Menthol, Carbomer, TEA-Dodecylbenzenesulfonate, Charcoal Powder (Bamboo charcoal), Charcoal Powder, Guar Hydroxypropyltrimonium Chloride, Trideceth - 10, Polyquaternium-7, Polyquaternium-10, Sodium Chloride, Sodium Cumenesulfonate, Tetrasodium EDTA, Citric Acid, Sodium Hydroxide, Methylchloroisothiazolinone , Methylisothiazolinone.','171','171000',N'Việt Nam','LSP15'
EXEC sp_AddSP N'Dầu Gội Romano Attitude -380G',N'Với công thức cải tiến với thành phần Pro Vitamin B5 cho mái tóc mềm mại và chắc khỏe hơn. Cùng với hương thơm độc đáo, sản phẩm mang đến cho phái mạnh phong cách lịch lãm và đầy quyến rũ.
Dầu gội cao cấp Romano Attitude có công thức chuyên biệt dành cho nam với thành phần Pro Vitamin B5 cho mái tóc sạch, mát và chắc khỏe.
Dầu Gội Cao Cấp Cho Nam Romano Attitude là sự kết hợp của các thành phần tự nhiên, lấy cảm hứng từ hương nước hoa cho hương thơm sảng khoái, mát lạnh và nam tính sẽ cho bạn phong cách trẻ trung, năng động và quyến rũ.
Dầu gội được chứng minh là không hại da đầu, bạn chỉ cần nhẹ nhàng gội Romano với nước, massage nhẹ nhàng rồi xả lại là đã có một mái tóc chắc khỏe, đầy sức sống.
Sản phẩm thích hợp cho mọi loại tóc.','21','109000',N'Việt Nam','LSP15'
EXEC sp_AddSP N'Set Gội Xả Hairburst Kích Thích Mọc Tóc For Longer Stronger Hair (Dầu Gội 350ml + Dầu xả 350ml)',N'1. Dầu Gội Hairburst For Longer Stronger Shampoo Hair Kích Thích Mọc Tóc 350ml
Với hơn 95% thành phần từ thiên nhiên, dầu gội Hairburst chiết xuất từ trái bơ và dừa giúp kích thích tóc mọc nhanh, dài và dày hơn. Tóc chắc khỏe hơn từ bên trong nhờ những dưỡng chất cung cấp độ ẩm giúp rửa trôi bã nhờn dư thừa trên da đầu và ngăn gãy rụng tóc. 
Thành phần: Nước cất (Aqua), Cetearyl Alcohol, Glycerin, Behentrimonium Chloride, Citric Acid, Stearamidopropyl, Dimethylamin, Hydrolyzed Wheat Protein, Palmitamidopropyltrimonium Chloride, Phenoxyethanol, Panthenol, Parfum (Fragrance), Sodium Benzoate
2. Dầu Xả Hairburst For Longer Stronger Hair Conditioner Kích Thích Mọc Tóc 350ml
Với hơn 95% thành phần từ thiên nhiên, dầu xả Hairburst giúp kích thích tóc mọc nhanh, dài và dày hơn. Tóc sẽ mềm mượt và bồng bềnh hơn nhờ vào sự kết hợp từ trái bơ và dừa.
- Bộ sản phẩm không chứa chất tạo bọt SLS và Parabens, không gây khô tóc và da đầu.
- Amino Acids và Pathenol (vitamin B5) giúp tăng khả năng giữ nước ở chân tóc, nuôi dưỡng tóc bồng bềnh, chắc khỏe.
- Bao bì tái sử dụng, thân thiện với môi trường.
Thành phần: Nước cất (Aqua), Cetearyl Alcohol, Glycerin, Behentrimonium Chloride, Citric Acid, Stearamidopropyl, Dimethylamin, Hydrolyzed Wheat Protein, Palmitamidopropyltrimonium Chloride, Phenoxyethanol, Panthenol, Parfum (Fragrance), Sodium Benzoate','211','679000',N'Anh Quốc','LSP15'

select * from SANPHAM WHERE MALSP='LSP15'

--Dầu xả
EXEC sp_AddSP N'Dầu Xả Pantene Micellar Làm Sạch Và Dưỡng Ẩm Chiết Xuất Hoa Súng 530ml',N'Dầu xả Pantene Micellar làm sạch & dưỡng ẩm chiết xuất hoa súng 530ml có chứa công nghệ chăm sóc da Micellar giúp nhẹ nhàng thấm sâu vào tóc, giúp chăm sóc tóc suôn mượt.','45','159000',N'Việt Nam','LSP16'
EXEC sp_AddSP N'DẦU XẢ MOIST DIANE NIGHT REPAIR DƯỠNG CHẤT BAN ĐÊM 450ML',N'Thành phần: argania spinosa sprout cell extract, pouteria sapota seed oil, chenopodium quinoa seed oil, cetearamidoethyl diethonium hydrolyzed rice protein, glycerin, tocopherol, prunus domestica seed extract, sclerocarya birrea seed oil, theobroma grandi orum seed butter, mangifera indica (mango) seed oil, carapa guaianensis seed oilhydro, adansonia digitata seed oil, schinziophyton rautanenii kernel oil, hydroxypropyltrimonium hydrolyzed keratin, laurdimonium hydroxypropyl hydrolyzed keratin, cholesterol, behentrimonium chloride, citric acid, isostearoyl hydrolyzed collagen, hydrolyzed silk pg-propyl methylsilanediol, glycine soja (soybean) sterols, hydrolyzed quina, hydrolyzed adansonia digitata seed extract, argania spinosa kernel oil, alcohol denat, isomalt, terminalia perdinandiana fruit extract, keratin, ceramide ag, cellulose gum, hydrolyzed conchiolin protein, malus domestica fruit cell culture extract, potassium sorbate, lecithin, ceramide eop, gold, collagen, silver, water, cetearyl alcohol, dimethicone, steartrimonium chloride, ethylhexyl palmitate, fragrance, hydrogenated castor oil isostearate, ricinus communt (castor) seed oil, caramel, glucose, dimethiconol, hydroxyethylcellulose, lactic acid, sodium lactate, oenocarpus bataua fruit oil, hydrolzed keratin.','77','250000',N'Nhật Bản','LSP16'
EXEC sp_AddSP N'DẦU XẢ KERASYS ADVANCE MOISURE DƯỠNG ẨM SUÔN MƯỢT 600ML',N'Thành phần: water, cetearyl alcohol, stearyl alcohol, cyclopentasiloxane, dimethicone, behentrimonium chloride, isopropyl alcohol, laureth-23, hydroxyethylcellulose, phenoxyethanol, ethylhexylglycerin, ceteardimonium chloride, propylene glycol, amodimethicone, trideceth-10, dicaprylyl carbonate, fragrance, benzyl alcohol, amodimethicone, c12-14 sec-pareth-7, propylene glycol, panthenyl ethyl ether, ceteareth-20, keratin, hydrolysed keratin, lactic acid, potassium lactate, butylene glycol, panthenol, cetrimonium chloride, ceramide np, dodecenyl oleylsuccinamide, tocopherol. ','8','230000',N'Nhật Bản','LSP16'
EXEC sp_AddSP N'TRESEMME DẦU XẢ VÀO NẾP SUÔN MƯỢT 7G',N'Tinh Chất Tresemmé Keratin Smooth Tinh Dầu Argan Và Keratin Vào Nếp Suôn Mượt với công thức cô đọng với dưỡng chất Keratin & Oleo Serum - hệ 5 tinh dầu quý sẽ bảo vệ mái tóc bạn tránh những hư tổn và kéo dài vẻ đẹp cho mái tóc. 
Suôn Mượt giúp nuôi dưỡng chuyên sâu & cấp ẩm cho tóc, loại bỏ khô - xơ - rối cho mái tóc thật sự suôn mượt.
Sản phẩm dành cho tóc khô xơ, không vào nếp. Mái tóc sẽ dành cho cải thiện tình trạng khô và trở nên mềm mại, mượt mà. Đặc biệt, sản phẩm còn thích hợp dành cho tóc nhuộm.
Hướng dẫn sử dụng: Sau khi gội, dùng Tresemmé Keratin Smooth Tinh Dầu Argan Và Keratin Vào Nếp Suôn Mượt từ thân đến ngọn, matxa trong vòng 2-3 phút để dưỡng chất thấm sâu vào sợi tóc. Sau đó xả sạch với nước.','67','2000',N'Anh Quốc','LSP16'
EXEC sp_AddSP N'SUNSILK DẦU XẢ VÀNG MỀM MƯỢT DIỆU KỲ 6GX',N' ','555','1000',N'Việt Nam','LSP16'

select * from SANPHAM WHERE MALSP='LSP16'

--Kem ủ tóc
EXEC sp_AddSP N'Hấp dầu Gamma Ovlie dưỡng tóc 500ml',N'Kem hấp dầu dưỡng tóc Olive với công thức đặc chế gồm các hoạt chất tối ưu cung cấp dưỡng chất cho mái tóc. Nên sử dụng thường xuyên như một loại dầu xả để giữ cho bạn mái tóc óng mượt và mềm mại.
Sở hữu mái tóc bóng khỏe là mong muốn của mọi chị em. Chính vì thế, nhiều người đã không tiếc thời gian và công sức đến các trung tâm spa, salon chăm sóc tóc để có được mái tóc như ý. Nhưng giờ đây, với hấp dầu tinh chất trái Olive , bạn sẽ nhanh chóng có được mái tóc khỏe mạnh mà không còn phải tốn kém quá nhiều nữa.
Hấp dầu tinh chất trái Olive 500 ml với công thức chứa độ dưỡng chất đậm đặc phù hợp cho nhiều loại tóc, giúp tóc bổ sung cho mái tóc những dưỡng chất cần thiết.
Đối với tóc hư tổn, tóc đã tiếp xúc với hóa chất nhiều lần, hấp dầu tinh chất trái Olive sẽ giúp tăng khả năng giữ ẩm, tái tạo lại độ đàn hồi, bóng mượt và tạo sức sống mới cho mái tóc.
Hấp dầu tinh chất trái Olive 500 ml còn có tác dụng cải thiện cấu trúc tóc, giảm hư tổn và phục hồi từ gốc đến ngọn.
An toàn cho da đầu, không gây kích ứng da và phù hợp với nhiều loại tóc.
Hương thơm nhẹ nhàng, dễ chịu.','89','190000',N'Việt Nam','LSP17'
EXEC sp_AddSP N'Kem hấp dầu trái bơ M.Pros 500ml',N'Mang đến hương vị và sức sống mới cho mái tóc của bạn, kem hấp dầu Mpros chứa nhiều dưỡng chất nuôi dưỡng, bảo vệ và chăm sóc toàn diện giúp mái tóc trở nên mượt mà, óng ả . Cùng với hương thơm tinh khiết, dịu nhẹ từ trái bơ cho bạn sức quyến rũ, ấn tượng khó phai. Sử dụng HẤP DẦU PHỤC HỒI VÀ NUÔI DƯỠNG TÓC KHÔ, HƯ TỔN – TRÁI BƠ – Cảm nhận mái tóc mượt mà quyến rũ như bạn hằng mong đợi.','7','34000',N'Việt Nam','LSP17'
EXEC sp_AddSP N'Kem ủ phục hồi LOREAL ELSEVE 200ML',N'Thành phần: "AQUA / WATER, CETEARYL ALCOHOL, PARAFFINUM LIQUIDUM / MINERAL OIL, DIPALMITOYLETHYL HYDROXYETHYLMONIUM METHOSULFATE, CETYL ESTERS, LACTIC ACID, PHENOXYETHANOL, TRIDECETH-6, CHLORHEXIDINE DIGLUCONATE, AMODIMETHICONE, 2-OLEAMIDO-1,3-OCTADECANEDIOL, PARFUM / FRAGRANCE, CETRIMONIUM CHLORIDE
Công dụng: Kem ủ phục hồi tóc hư tổn
Hướng dẫn sử dụng: Thoa trực tiếp lên tóc,masage xả lại với nước sạch','7','110000',N'INDONESIA','LSP17'
EXEC sp_AddSP N'Kem ủ tóc - Hấp dầu Xcute Me XTRA MOIST 450G',N'Công dụng:
- Bổ sung độ ẩm cho tóc mềm mượt dễ vào nếp.
- Dưỡng chất chiết xuất từ hoa Tsubaki và Hyaluron. 
- Phục hồi tình trạng tóc khô yếu trở nên chắc khỏe một cách rõ rệt.
Thành phần sản phẩm: 
AQUA, STEARYL ALCOHOL, CETYL ALCOHOL, BEHENTRIMONIUM CHLORIDE, DIMETHICONE, AMODIMETHICONE, LAURETH-23, POLYQUATERNIUM-10, LAURETH-4, CYCLOPENTASILOXANE, DIMETHICONE, FRAGRANCE, BENZYL ALCOHOL, ETHYLHEXYLGLYCERIN, TOCOPHEROL, GUAR HYDROXYPROPYLTRIMONIUM CHLORIDE, DISODIUM EDTA, PANTHENOL, SODIUM HYALURONATE, CAMELLIA JAPONICA SEED OIL','18','72000',N'Thái Lan','LSP17'
EXEC sp_AddSP N'Kem ủ TRESemmé Keratin Smooth Vào nếp mềm mượt 180ml',N'Bạn phải "đánh vật" với mái tóc xơ rối mỗi ngày? Nay đã có bí quyết từ chuyên gia tạo mẫu tóc! Dòng sản phẩm TRESemmé Keratine Smooth mới, cho tóc vào nếp suôn mượt suốt đến 48 giờ để bạn tự tin làm chủ mái tóc của mình.
- Kem ủ tóc TRESemmé Keratin dành cho tóc khô xơ, không vào nếp. Với công thức chứa keratin và vitamin, giúp thấm sâu phục hồi keratin cho mái tóc bạn óng ả và vào nếp mượt mà đến 48 giờ*. Đặc biệt, sản phẩm còn thích hợp với cả tóc nhuộm.
- Hướng dẫn sử dụng:
• Thoa đều dầu gội TRESemmé Keratin Smooth lên tóc ướt, xoa bóp nhẹ nhàng để tạo bọt.
• Vuốt nhẹ từ chân tóc đến ngọn tóc, đồng thời massage da đầu nhiều lần giúp các mạch máu lưu thông, nuôi dưỡng tóc tốt hơn; sau đó gội sạch với nước.
• Luôn dùng dầu xả TRESemmé Keratin Smooth sau mỗi lần gội, tập trung vào phần thân và ngọn tóc, tránh tiếp xúc với da đầu.
• Xả sạch bằng nước lạnh để cố định lớp biểu bì và cho tóc thêm phần sáng bóng.
• Sử dụng kem ủ tóc TRESemmé Keratin Smooth ít nhất 1 - 2 lần một tuần sau khi gội và xả để đạt hiệu quả tốt nhất.
- Hãy chia sẻ cảm nhận về mái tóc của bạn sau khi sử dụng sản phẩm TRESemmé Keratin Smooth tại đây nhé.','7','123000',N'Anh','LSP17'

select * from SANPHAM WHERE MALSP='LSP17'

--Dầu dưỡng tóc
EXEC sp_AddSP N'DẦU DƯỠNG TÓC DCASH HORSE OIL HEALTHY ELEGANT 80ML',N'Công dụng: 
- Thẩm thấu vào sợi tóc khô do hư tổn.
- Giảm tóc rụng.
- Phục hồi tóc hư tổn trở lại như được chăm sóc tại salon. 
- Giúp chân tóc chắc khỏe hơn, nhanh dài và bóng mượt.
Thành phần sản phẩm: 
CYCLOPENTASILOXANE, DIMETHICONOL, CYCLOHEXASILOXANE, DIMETHICONE/VINYL DIMETHICONE CROSSPOLYMER, C12-15 ALKYL BENZOATE, DIMETHICONE, ARGENIA SPINOSA KERNEL OIL, DEPHENYLSILOXY PHENYL TRIMETHICONE, BIS-HYDROXY/METHOXY AMODIMETHICONE, FRAGRANCE, HORSE FAT, TOCOPHERYL ACETATE, ETHYLHEXYL METHOXYCINNAMATE.','44','70000',N'Thái Lan','LSP18'
EXEC sp_AddSP N'DƯỠNG ẨM TÓC SIÊU MỀM MƯỢT LAVOX 180ML',N'Công dụng: nuôi dưỡng và bảo vệ tóc gấp hai lần,duy trì mái tóc óng mượt đầy sức sống suốt ngày dài.Ưu việt của sản phẩm là khả năng phủ đều và thẩm thấu nhanh,cung cấp độ ẩm cho tóc luôn mềm mượt,sáng bóng,đàn hồi mà không gây bết dính.Tăng cường tái tạo các lớp biểu bì bị hư tổn,bảo vệ tóc chống nhiệt,dưỡng tóc chắc dày,suôn mượt theo thời gian.','7','119000',N'Việt Nam','LSP18'
EXEC sp_AddSP N'Ellips Bộ 2*6V Keratin Dưỡng Tóc Suôn Mượt',N'Ngoài ra, các thành phần vitamin A,C,E, Pro B5 và chiết xuất dầu Morrocan giúp mái tóc mềm mại và bảo vệ tóc khỏi các tác nhân ánh nắng, nhiệt độ, bụi bẩn, Serum dưỡng tóc ELLIPS Pro-Keratin Complex Silky Black bổ sung chiết xuất dầu Candlenut làm tăng khả năng bảo vệ tóc khỏi tác hại của ánh nắng, giữ tóc vào nếp suôn mượt và giữ màu tóc. Sản phẩm tác động từ gốc đến ngọn tóc, giúp điều trị cấu trúc tóc hư tổn, làm giảm chẻ ngọn hiệu quả giúp bạn thêm tự tin với mái tóc đẹp mỗi ngày.
Serum dưỡng tóc ELLIPS Pro-Keratin Complex Silky Black được thiết kế nhỏ gọn tiện dụng dưới dạng viên. Một set gồm 6 viên các bạn có thể sử dụng được 6 lần. Đây là một sản phẩm vô cùng tiện dụng vì các bạn có thể bỏ túi mang theo để chăm sóc tóc trong những chuyến du lịch dài ngày.','7','23000',N'Anh','LSP18'
EXEC sp_AddSP N'DƯỠNG TÓC NHUỘM & UỐN LAVOX 180ML',N'hành phần: DEIONIZED WATER,DIMETHICONE,DIMETHICONE/VINYL DIMETHICONE CROSSPOLYMER,CAPRYLYL METHICONE,PHENYL TRIMETICONE,LAURYL PEG-10,TRIS(TRIMETHYLSILOXY)SILYLETHYL DIMETHICONE,SILICONE QUATERNIUM-16/GLYCIDOXY DIMETHICONE CROSSPOLY-MER,UNDECETH-11,PROPYLENE GLYCOL,UNDECETH-5,HYDROLYZED SILK,SODIUM CHLORIDE,PHENOXYETHANOL,ETHYLHEXYLGLYCERIN,PERFUME,CAPRYLIC/CAPRIC TRIGLYCERIDE,ARGANIA SPINOSA KERNEL OIL,LAURYL PEG/PPG-18/18 METHICONE,BUTYROSPERMIUM PARKII (SHEA) BUTTER,SQUALANE HYDROLYZED JOJOBA ESTERS,POLYQUATERNIUM-10.','7','248000',N'Việt Nam','LSP18'
EXEC sp_AddSP N'Nước dưỡng tóc Double Rich cho tóc khô xơ & hư tổn 250ml',N'Nước xịt dưỡng tóc chăm sóc tóc hư  Double Rich Nutrition V là sản phẩm chăm sóc cần thiết dành cho tóc khô và hư tổn. Với công thức kết hợp hệ PhytoCollagen và chiết xuất Shea Bulter độc đáo giúp bạn nhanh chóng có được mái tóc suôn mềm và óng mượt như ý. Sản phẩm bổ sung Protein và các Vitamin thiết yếu, giúp mái tóc phục hồi độ mượt mà và nuôi dưỡng cho tóc chắc khỏe. Chai xịt có hương phấn nhẹ nhàng, bên cạnh mái tóc suôn mượt, dễ chải và giảm gãy rụng.','8','72000',N'Anh','LSP18'

select * from SANPHAM WHERE MALSP='LSP18'

--Nhuộm tóc
EXEC sp_AddSP N'Kem nhuộm dưỡng tóc màu thời trang LOreal Paris Excellence Fashion 5.13',N'Kem Nhuộm Dưỡng Tóc Màu Thời Trang Loreal Excellence Fashion giúp bạn tự tin, nổi bật với mái tóc óng ả cùng tông màu nâu khói hiện đại.
Đồng thời, sản phẩm còn có tác dụng chăm sóc tóc chắc khỏe, suôn mượt hơn.
Loreal Excellence Fashion giúp nâng sáng màu tóc 2 - 3 cấp độ, giúp màu nhuộm lên chuẩn. Công thức kem nhuộm dưỡng tóc 3 tác động trước - trong - sau khi nhuộm, giúp dưỡng tóc khỏe và suôn mượt','22','149000',N'Anh','LSP19'
EXEC sp_AddSP N'Kem Nhuộm Tóc Số 4 Màu Nâu Nhạt Salon De Pro 80Gr',N'Kem nhuộm tóc thảo dược cao cấp dành cho quý bà lịch lãm, chuyên dùng cho TÓC BẠC
Không cay mắt, không mùi, không làm tóc bị khô xơ và hư tổn
Không chứa chất A-mô-ni-ắc, không gây khó chịu mỗi khi sử dụng','14','77000',N'Nhật Bản','LSP19'

select * from SANPHAM WHERE MALSP='LSP19'

--Phụ kiện
--Cọ trang điểm
EXEC sp_AddSP N'Cọ trang điểm 
Morphe Complexion 
Crew 5 Piece Brush',N'Cọ trang điểm Morphe Complexion Crew 5 Piece Brush là một trong những bộ có chất lượng, 
được đông đảo các bạn nữ lựa chọn cho việc trang điểm của mình có xuất xứ thương hiệu từ Mỹ 
gồm nhiều loại với các kích thước khác nhau, thiết kế cực kỳ sang trọng cùng chất liệu lông cao cấp, 
khả năng tạo dáng, định hình tốt, không bị rụng lông cũng như không khô cứng sau 
một thời gian sử dụng, dễ thao tác, sợi cọ được làm từ những sợi lông nhân tạo chất lượng 
giúp lên màu chuẩn khi trang điểm dành riêng cho những bạn muốn khuôn mặt mình trở nên 
thật sắc nét và đầy cuốn hút.',30,820000,N'Mỹ','LSP38'
EXEC sp_AddSP N'Cọ trang điểm Zoeva Complete Brush',N'Cọ trang điểm Zoeva Complete Brush là bộ cọ gồm 15 cây có đầy đủ các loại cọ thích hợp cho từng công đoạn trang điểm khác nhau với lông siêu mềm có một ưu thế lớn khi có rất nhiều cọ kể cả cọ đa năng complexion và cọ mắt mà không chứa thành phần độc hại cùng kiểu dáng đẹp, sang trọng để các bạn có thể dễ dàng mang theo ở bất cứ nơi đâu. Cọ trang điểm Zoeva Complete Brush sở hữu sợi cọ nhỏ có độ bền cao, siêu mềm mịn giúp tán đều các sản phẩm trang điểm trên da. Không những thế, Cọ trang điểm Zoeva Complete Brush có thân cọ bọc nhôm được mài nhẵn trơn, không hoen gỉ và gây trầy xước khi sử dụng, là sự lựa chọn hoàn hảo với các cô nàng tập make up và cả những cô nàng make up chuyên nghiệp.',40,820000,N'Đức','LSP38'
EXEC sp_AddSP N'Cọ trang điểm E.L.F Ultimate Blending Brush',N'Cọ trang điểm E.L.F Ultimate Blending Brush là loại cọ cao cấp của E.L.F được thiết kế đặc biệt dành cho kỹ thuật trang điểm chuyên nghiệp trở thành trợ thủ đắc lực với nhiều chức năng hỗ trợ phù hợp với các sản phẩm như kem nền, brozer, highlight, phấn má hồng, phấn phủ dạng bột, dạng lỏng hoặc mousse... cho bạn một lớp make up đẹp tuyệt hảo không thua kém chuyên viên trang điểm chuyên nghiệp.Cọ trang điểm E.L.F Ultimate Blending Brush có lông cọ nhân tạo mềm mại có đặc tính kháng khuẩn nên không gây kích ứng da, an toàn với da nhạy cảm với đầu cọ được thiết kế tròn, lông dày không hút kem giúp lấy lượng sản phẩm vừa đủ và tán đều, không để lại dấu vết, kem/ phấn dễ dàng bám vào cọ và được tán đều lên da một cách nhanh chóng mang đến cho bạn một lớp nền mỏng mịn, tự nhiên cùng đặc điểm cán cọ thon dài, cầm vừa tay cho bạn dễ dàng makeup.',20,135000,N'	Mỹ','LSP38'
EXEC sp_AddSP N'Cọ trang điểm Ofélia Brush',N'Cọ trang điểm Ofélia Brush là dụng cụ hỗ trợ đắc lực với 6 cây cọ cơ bản cho các bước trang điểm (4 cọ mặt và 2 cọ mắt) có lông cọ mềm mượt như nhung, không gây sót, kích ứng cho da, lông cọ được búi chắc chắn hạn chế rơi rụng và vẫn giữ nguyên hình dáng cùng thân cọ chắc chắn, được thiết kế vừa tay cầm nên thao tác makeup sẽ dễ dàng và nhanh chóng hơn. Cọ trang điểm Ofélia Brush với các loại cọ trang điểm được dùng cho từng mục đích khác nhau như cọ đánh nền, đánh khối, đánh màu mắt... nên bạn cần sử dụng đúng loại cọ để có được hiệu quả trang điểm đẹp và chất lượng nhất nhé!',15,450000,N'Mỹ','LSP38'
EXEC sp_AddSP N'Cọ trang điểm BH Cosmetics Pink Studded Elegance Brush',N'Cọ trang điểm BH Cosmetics Pink Studded Elegance Brush được phái đẹp tin yêu lựa chọn hỗ trợ cho hành trình làm đẹp mỗi ngày của bản thân cũng như được sử dụng vào công việc makeup chuyên nghiệp có 2 phiên bản gồm tone màu ngọt lịm Trắng Sữa và Hồng Pastel với bộ 1 cây hoàn thiện các bước trang điểm cho người sử dụng. Cọ trang điểm BH Cosmetics Pink Studded Elegance Brush sở hữu đầu lông siêu mịn cùng với đó là đầu cọ được bó rất chặt nên không cần bạn lo về tình trạng rụng lông giúp bạn có quy trình làm đẹp ưng ý và hiệu quả nhất.',25,420000,N'Mỹ','LSP38'

select * from SANPHAM WHERE MALSP='LSP38'

--mút trang điểm

EXEC sp_AddSP N'Mút trang điểm Ebelin Professional Make-up Ei',N'Mút trang điểm Ebelin Professional Make-up Ei hình trứng 3D hàng Ebelin Đức là phụ kiện trang điểm hiệu quả trong việc đánh phấn, tản kem nền. Giúp tiết kiệm thời gian make-up, không tốn phần và đạt hiệu quả cao. Sản phẩm có nhiều màu khác nhau để bạn lựa chọn.',50,130000,N'Đức','LSP39'
EXEC sp_AddSP N'Mút Trang Điểm True Professional Make-Up Blending Sponge',N'Mút trang điểm dạng có tay cầm như True professional là một lựa chọn hoàn hảo cho các nàng không muốn bị dây mỹ phẩm ra tay. Với hình dạng bé xinh này giúp bạn dễ dàng tán sản phẩm vào những vùng “ngõ ngách” như hốc mắt, vùng dưới mắt và cánh mũi. Bạn cũng có thể sử dụng chiếc mút tí hon này để tán các loại kem highlight lên vùng gò má, xương chân mày hay sống mũi. Sản phẩm có xuất xứ từ Anh Quốc.',20,180000,N'Anh','LSP39'
EXEC sp_AddSP N'Set Bông Mút Trang Điểm Hold Live Multifunctional Beauty Blender',N'Set Bông Mút Trang Điểm Hold Live Multifunctional Beauty Blender được làm từ chất liệu có độ xốp đặc biệt và mềm mại giúp tạo nên lớp nền và lớp phủ mịn màng thật đều màu và tự nhiên.',30,160000,N'Anh','LSP39'
EXEC sp_AddSP N'Bộ 3 Mút Tán Nền Etude House Make-Up Puff Big Cover Mini Blender Kit Pro',N'Những sản phẩm mút tán nền ngày càng phổ biến hơn bởi độ tiện dụng mà nó mang lại. Vừa giúp tán lớp nền đều mỏng mịn hơn, vừa vệ sinh và còn tiện hơn dùng tay rất là nhiều. Etude House là một hãng mỹ phẩm đến từ Hàn Quốc được biết đến với những sản phẩm có chất lượng và có bao bì thiết kế cực kì dễ thương, giá tiền hợp lí nên được nhiều người lựa chọn. Bộ 3 Mút Tán Nền Etude House Big Cover Mini Blender Kit Pro là bộ ba mút tán kem nền, che khuyết điểm với thiết kế đa diện cùng kích thước nhỏ gọn, nhiều kích cỡ khác nhau.',10,130000,N'Hàn Quốc','LSP39'
EXEC sp_AddSP N'Mút Tán Nền Real Techniques 2 Miracle Complexion Sponges',N'Bông Mút trang điểm sponge của Real Techniques được cho là Bản dupe hoàn hảo của Bông Mút trang điểm Beauty Blender đình đám trong thế giới làm đẹp. Thậm chí nhiều Beauty Blogger & Beauty Guru dành cho Bông trang điểm sponge của Real Technique những lời có cánh “là miếng bông mút còn tuyệt hơn cả bản chính”.',23,110000,N'Mỹ','LSP39'
EXEC sp_AddSP N'Mút trang điểm siêu mịn A Pieu Non-Latex Blending Puff',N'Mút trang điểm siêu mịn A Pieu Non-Latex Blending Puff là miếng mút “đa-zi-năng” có thể tán kem lót, kem nền, kem che khuyết điểm, má hồng & highlight, xứng đáng là bản dupe hoàn hảo của beauty blender mà cô nàng nào cũng “ưng” hết lòng. Nhiều beauty blogger đã thực sự bất ngờ khi lần đầu tiên sử dụng “em” mút trang điểm đến từ Hàn Quốc này. Với giá thành “cực yêu thương” nhưng Mút tán kem A’pieu Non-Latex Blending Puff lại mang đến chất lượng vô cùng kinh ngạc. Mút tán kem Apieu được thiết kế có kiểu dáng giống hình hồ lô có khả năng tán mọi góc cạnh trên khuân mặt như khóe mắt, cằm, cánh mũi…giúp bạn có lớp nền mềm mịn hơn. Dùng được cho rất nhiều loại phấn khác nhau như phấn nước, nén, kem nền, phấn khô đều được. Ngoài ra sản phẩm còn có tính chất mềm, mịn, đàn hồi cực tốt sẽ đem lại cho bạn lớp kem nền mỏng mịn, hoàn hảo cho những lớp trang điểm tiếp theo.',11,60000,N'Hàn Quốc','LSP39'

select * from SANPHAM WHERE MALSP='LSP39'

--kẹp bấm mi

EXEC sp_AddSP N'Kẹp Bấm Mi Shu Uemura Eyelash Curler',N'Kẹp Bấm Mi Shu Uemura Eyelash Curler đã trở thành món đồ quen thuộc được các cô nàng nghiện makeup xem như vật bất ly thân tài ba giúp định hình mi cong, mềm mại một cách tự nhiên mà không lo làm tổn thương mi hay “gãy”, đồng thời còn hỗ trợ cho bước chuốt mascara được hoàn hảo hơn. Thêm một điểm cộng to đùng nữa là em nó rất vừa vặn với khuôn lông mi của người châu Á, không có cảm giác bị kẹp vào da nên các nàng cứ vô tư mà sử dụng nhen, quan trọng nhất nó không làm tổn hại lông mi của bạn.
',34,400000,N'Nhật Bản','LSP40'
EXEC sp_AddSP N'Kẹp Bấm Mi The Face Shop Daily Beauty Tools Eyelash Curler',N'
Không giống như những loại kẹp nhựa hay kim loại xuất hiện nhiều trên thị trường hiện nay, Kẹp Bấm Mi The Face Shop Daily Beauty Tools Eyelash Curler có lớp đệm cao su cao cấp cùng khớp nối silicon êm ái, linh hoạt, kết hợp với tay cầm được thiết kế vừa vặn, chắc chắn sẽ giúp bạn có được làn mi cong quyến rũ chỉ trong vài giây.An toàn đối với mắt: Lớp đệm cao su và khớp nối silicon giúp kẹp mi cong hiệu quả và tuyệt đối an toàn cho mắt. Sản phẩm phù hợp với mọi đối tượng ngay cả những bạn trẻ mới tập trang điểm. Tay cầm vừa vặn: Thiết kế tay cầm vừa vặn và tiện lợi giúp bạn dễ dàng uốn cong đôi mi, giúp mi giữ độ cong lâu dài. Thiết kế trẻ trung: Thiết kế gọn nhẹ, trẻ trung, dễ dàng mang theo ngay cả khi bạn có một chuyến du lịch xa.',34,55000,N'Hàn Quốc','LSP40'
EXEC sp_AddSP N'Kẹp Bấm Mi W7 Groovy Curls Eyelash Curler',N'Khả năng làm cong mi cực kỳ nhanh chóng và chuẩn xác từ mọi góc nhìn, không cần phải kẹp đi kẹp lại nhiều lần mất thời gian. Cho dù bạn có vụng về hay mới tập tành bấm mi cũng sẽ dễ dàng thao tác với thiết kế tay cầm siêu nhẹ, siêu mềm chứ không nặng và cứng như một số loại kẹp mi khác.',34,95000,N'Anh','LSP40'
EXEC sp_AddSP N'Kẹp Bấm Mi Muji Eyelash Curler',N'Thiết kế tay cầm chắc chắn lại vừa vặn, các nàng có thể dễ dàng xác định đúng vị trí và tạo dáng cho lông mi một cách nhẹ nhàng. Hơn nữa em ấy có kích thước rất vừa vặn với khuôn lông mi của đa số người châu Á, nên không có cảm giác bị kẹp vào da hay gây tổn hại đến lông mi. Đặc biệt, khi mua sản phẩm các nàng còn được tặng kèm thêm miếng lót cao su “sơ-cua” để thay thế miếng cũ khi dùng nhiều lần, không quá tốn kém mua kẹp mới nên có thể thiết kiệm được một khoản kha khá trong quá trình làm đẹp đó.',34,190000,N'Nhật Bản','LSP40'
EXEC sp_AddSP N'Kẹp mi Innisfree Eyelash Culer',N'Kẹp mi Eyelash Curler của Innisfree được thiết kế với lớp đệm và khớp nối silicon êm ái giúp kẹp mi cong tự nhiên, cuốn hút và an toàn. Kết hợp cùng tay cầm màu đen thiết kế vừa vặn, dễ dàng tạo dáng cho đôi mi. Eyelash Curler là dụng cụ chuyên nghiệp sẽ giúp bạn dễ dàng tạo độ cong đẹp tự nhiên như ý nhưng vẫn cực kì thu hút cho làn mi mỗi ngày. Sản phẩm đem lại cảm giác nhẹ nhàng thoải mái cho mắt, sử dụng được trong thời gian dài.',34,85000,N'Hàn Quốc','LSP40'

select * from SANPHAM WHERE MALSP='LSP40'

--lông mi giả

EXEC sp_AddSP N'Lông mi giả Vacosi Style Love',N'Ngày nay trước nhu cầu làm đẹp của phụ nữ hiện đại, mi giả đã trở thành một “vũ khí” làm đẹp không thể thiếu. Mi giả được yêu thích bởi việc nhỏ, gọn và rất tiện lợi. Mi giả sẽ làm cho “cửa sổ tâm hồn” của bạn long lanh, sâu hút, có hồn và đẹp sắc nét hơn. Lông mi giả Vacosi Style Love, giúp bạn có thêm nhiều lựa chọn tiện lợi.',34,65000,N'Hàn Quốc','LSP41'
EXEC sp_AddSP N'Mi giả Hàn Quốc',N'Mi giả Hàn Quốc với chất liệu an toàn, chất lông mi mềm mại không gây cộm ngứa là một món đồ làm đẹp tuyệt vời cho đôi mắt của chị em phụ nữ. Chỉ với một cặp mi giả Hàn Quốc, bạn sẽ sở hữu ngay làn mi cong quyến rũ và đôi mắt long lanh, sâu thẳm, thu hút ánh hình người đối diện.',34,30000,N'Hàn Quốc','LSP41'
EXEC sp_AddSP N'Lông mi giả tự nhiên Ing lashTatoc No Glue Eyelash',N'Lông mi giả tự nhiên Ing lashTatoc No Glue Eyelash với chiều dài mi vừa phải và sáng đẹp phù hợp với đôi mắt châu Á mang đến sự lôi cuốn và ấn tượng cho đôi mắt thêm phần xinh đẹp.',34,160000,N'Hàn Quốc','LSP41'
EXEC sp_AddSP N'Lông mi giả Odbo Swan Eyelashes OD-849',N'Lông mi giả Odbo Swan Eyelashes OD-849 là dụng cụ hỗ trợ trang điểm cho đôi mắt của phái đẹp thêm phần long lanh, thu hút. Sản phẩm chính hãng Thái Lan có giá bán phải chăng và được ưa chuộng tại trường Việt Nam.',34,80000,N'Thái Lan','LSP41'

select * from SANPHAM WHERE MALSP='LSP41'

--kính áp tròng

EXEC sp_AddSP N'Áp tròng VICKYDINH',N'Thương hiệu kính áp tròng VICKYDINH khá nổi tiếng ở Việt Nam nhờ chất lượng kính rất tốt, không bị cộm, không gây khó chịu cho mắt, mẫu mã đa dạng. Có thể phân chia kính áp tròng của Vickydinh làm 3 loại: Kính áp tròng, kính giãn tròng và kính dành cho cosplay - các lễ hội.',34,400000,N'Việt Nam','LSP42'
EXEC sp_AddSP N'Seed',N'Seed là một thương hiệu kính áp tròng nổi tiếng đến từ Nhật Bản. Điểm khác của Seed với những thương hiệu khác chính là hạn sử dụng của kính không quá 1 tháng. Kính áp tròng rất mỏng, tạo cảm giác dễ chịu cho mắt, không bị cộm. Những sản phẩm của Seed bao gồm: kính áp tròng 1 ngày, kính áp tròng 2 tuần và kính áp tròng 1 tháng. Có lẽ sản phẩm được yêu thích nhất chính là kính áp tròng một ngày do sự tiện lợi cũng như chất lượng của kính. Đây là sản phẩm kính áp tròng một ngày mỏng nhất trên thị trường trong thời điểm hiện tại, đảm bảo sẽ làm vừa lòng người khó tính nhất.',34,100000,N'Nhật Bản','LSP42'
EXEC sp_AddSP N'Kính áp tròng Doll Eyes',N'Doll Eyes là một cái tên không còn xa lạ với những bạn nữ thích trang điểm, làm đẹp, hay theo dõi các video của các beauty blogger Việt Nam như Trinh Phạm, Changmakeup hay Chloe Nguyễn,...Không chỉ được chú ý vì nhận được rất nhiều nhận xét tốt từ những chuyên gia làm đẹp, người nổi tiếng như hoa hậu Kì Duyên, Á hậu Huyền My, mà Doll Eyes còn được chú ý chính bởi chính chất lượng. Hầu hết mọi người khi đã trải nghiệm với sản phẩm kính áp tròng của Doll Eyes đều đưa ra nhận xét rằng chất lượng kính rất tốt, không bị khô, không bị cộm, có đủ độ cận, khiến mắt rất dễ chịu.',34,300000,N'Việt Nam','LSP42'
EXEC sp_AddSP N'CARAS',N'Caras Lens là thương hiệu kính áp tròng uy tín sản xuất dựa trên công nghệ Mỹ. Với ưu điểm nổi bật là kính có sự mềm mại và dẻo dai, nên không tạo ra sự khó chịu cho người sử dụng, đồng thời hạn chế tối đa tình trạng khô mắt. CARAS có đẩy đủ các sản phẩm lens cận, kính áp tròng cận không màu, lens cận loạn, lens không độ, lens sử dụng 24h. Caras lens còn xuất hiện trên thị trường với nhiều mẫu mã khác nhau, nhiều kích thước khác nhau nên tạo ra nhiều sự lựa chọn hơn cho quý khách hàng.',34,800000,N'Mỹ','LSP42'

select * from SANPHAM WHERE MALSP='LSP42'

--LÀM SẠCH DA

--sửa rửa mặt

EXEC sp_AddSP N'Sữa rửa mặt dạng Gel COSRX Low pH Good Morning Gel Cleanser',N'Sữa rửa mặt dạng Gel COSRX Low pH Good Morning Gel Cleanser chính hãng đến từ Hàn Quốc là loại sữa rửa mặt tạo bọt dịu nhẹ chiết xuất từ các thành phần tự nhiên như tràm trà, hỗ trợ bạn làm sạch da mặt và mang lại cảm giác thư giãn, dễ chịu đặc biệt mà các sản phẩm thông thường khác không thể có.',34,245000,N'Hàn Quốc','LSP34'
EXEC sp_AddSP N'Sữa rửa mặt Innisfree Volcanic Pore Foam',N'Chiết xuất trà xanh và nước khoáng giúp làm sạch da và khô thoáng lỗ chân lông. Chỉ sau 2 tuần sử dụng, đảm bảo da mặt bạn sẽ hạn chế mụn và kiềm dầu hiệu quả.',34,170000,N'Hàn Quốc','LSP34'
EXEC sp_AddSP N'Sữa rửa mặt Clarisonic Daily Clarifying Cleanser',N'Sữa rửa mặt có chứa thành phần như vitamin C, E giúp ngăn chặn quá trình oxy hóa, giúp dưỡng trắng hiệu quả, công thức đặc biệt Nelumbo Nucifera Flower Extract và trà xanh giúp tăng cường xoa dịu làn da.
Ngoài ra thành phần Acid Salicylic còn giúp làm sạch dầu nhờn, bụi bẩn trên da, loại bỏ tế bào chết và ngăn mụn trứng cá hiệu quả.',34,890000,N'Mỹ','LSP34'
EXEC sp_AddSP N'Sữa rửa mặt Neostrata Clarifying Facial Cleanser',N'Neostrata Clarifying Facial Cleanser đặc trị mụn dành cho da nhờn với Polyhydroxy Acid Gluconolactone có khả năng ngăn ngừa mụn và chống lão hóa vô cùng hiệu quả. ',34,650000,N'Mỹ','LSP34'
EXEC sp_AddSP N'Sữa rửa mặt Shirochasou White Tea Foaming Wash',N'Sản phẩm có xuất xứ từ Nhật Bản rất được yêu thích chính là Shirochasou White Tea Foaming Wash, được chiết xuất từ trà trắng nguyên chất có khả năng diệt khuẩn, loại bỏ nếp nhăn, đốm mụn và sạm da, đem đến cho bạn làn da thoáng sạch, diệt khuẩn, giảm viêm cho da, se khít lỗ chân lông, chống lão hóa và dưỡng da trắng sáng.',34,170000,N'Nhật Bản','LSP34'

select * from SANPHAM WHERE MALSP='LSP34'

--tẩy trang mặt

EXEC sp_AddSP N'Dung dịch làm sạch và tẩy trang công nghệ Micellar Bioderma Sensibio H2O',N'Dung dịch làm sạch và tẩy trang công nghệ Micellar Bioderma Sensibio H2O là một trong những loại nước tẩy trang được yêu thích nhất hiện nay đến từ hãng mỹ phẩm nổi tiếng. Bioderma của Pháp. Với khả năng làm sạch sâu, lấy đi các lớp trang điểm, bụi bẩn mà vô cùng an toàn với da mặt. Nước tẩy trang Bioderma mang lại cảm giác sạch sẽ, thoáng mát cho mặt.',34,470000,N'Pháp','LSP35'
EXEC sp_AddSP N'Nước tẩy trang làm sạch da ngăn ngừa mụn Caryophy Smart Cleansing Water',N'Nước tẩy trang làm sạch da ngăn ngừa mụn Caryophy Smart Cleansing Water có đặc trưng cơ bản của dòng sản phẩm này là dịu nhẹ và an toàn cho làn da của bạn. Đây là sản phẩm nước tẩy trang không cồn Hàn Quốc tích hợp 5 công dụng vượt trội trong cùng một sản phẩm: tẩy trang làm sạch da, ngăn ngừa giảm mụn mờ thâm, cấp ẩm và cân bằng độ pH cho da và tẩy da chết hiệu quả.',34,280000,N'Hàn Quốc','LSP35'
EXEC sp_AddSP N'Nước Tẩy Trang Eucerin Dermatoclean Hyaluron 3In1 Dịu Nhẹ Cho Da',N'Nước Tẩy Trang Eucerin Dermato Clean Hyaluron Dịu Nhẹ Cho Da 400ml đến từ hãng Eucerin nổi tiếng tại Đức. Sản phẩmcó khả năng làm sạch sâu giúp loại bỏ nhanh chóng tàn dư mỹ phẩm makeup trên gương mặt giúp ngăn ngừa tình trạng tắt nghẽn lỗ chân lông gây mụn. Nước tẩy trang tẩy sạch sâu tất cả lớp trang điểm trên da mặt, ngay cả mắt và môi.',34,335000,N'Đức','LSP35'
EXEC sp_AddSP N'Dầu tẩy trang Olive DHC Deep Cleansing Oil',N'Dầu tẩy trang Olive DHC Deep Cleansing Oil do hãng mỹ phẩm nổi tiếng DHC của Nhật sản xuất với chiết xuất từ dầu olive, đây là một trong những loại dầu thực vật vừa có tác dụng tẩy trang vừa nuôi dưỡng da. Với công dụng vượt trội có thể tẩy sạch cả những loại trang điểm lâu trôi mà còn cung cấp ẩm cho da, dầu tẩy trang DHC là một trong những sản phẩm tẩy trang bán chạy nhất ở Nhật.',34,800000,N'Nhật Bản','LSP35'
EXEC sp_AddSP N'Nước tẩy trang Perfect Diary AMINO ACID MAKEUP',N'Nước tẩy trang Perfect Diary AMINO ACID MAKEUP giúp làm sạch nhẹ nhàng, tác dụng và hiệu quả vô cùng thần thánh, nó có khả năng làm sạch rất tốt mà không hề làm khô da, sau khi sử dụng xong da vẫn có một độ ẩm cần thiết, tẩy sạch cả những vùng makeup khó rửa như mắt. Về giá thành thì rất bình dân phù hợp với học sinh, sinh viên.',34,180000,N'Trung Quốc','LSP35'

select * from SANPHAM WHERE MALSP='LSP35'

--tẩy trang mắt môi

EXEC sp_AddSP N'Tẩy Trang Mắt Môi Neutrogena Oil Free Eye Makeup Remover',N'Tẩy trang mắt Neutrogena Oil-Free eye makeup remover với dung tích 162ml và có xuất xứ từ Mỹ giúp làm sạch được cho cả da có quá nhiều dầu khiến vùng mắt và môi hay bị lem khi tẩy trang. Sản phẩm không những có thể tẩy sạch được mascara chống lem, các sản phẩm makeup mắt dạng không trôi, mà còn giúp làm sạch lớp son môi tuyệt đối, giúp hạn chê gây thâm môi khi lớp son bám chặt trên môi.Đây là dòng sản phẩm tẩy trang mắt môi chất lượng được ưa chuộng nhất hiện nay.',34,250000,N'Mỹ','LSP36'
EXEC sp_AddSP N'Tẩy trang mắt môi Bifesta Eye Makeup Remover',N'Bifesta là thương hiệu thuộc tập đoàn Mandom của Nhật Bản. Tẩy trang mắt môi Bifesta Eye Makeup Remover là loại tẩy trang dạng nước (water-base) đầu tiên tại Nhật Bản, được ví như Bioderma. Với những ai thường xuyên trang điểm vùng mắt thì một loại tẩy trang riêng cho mắt là thứ không thể thiếu, và đây là một sự lựa chọn tuyệt vời không thể bỏ qua.',34,235000,N'Nhật Bản','LSP36'
EXEC sp_AddSP N'Tẩy Trang Mắt Môi Chia Seed Fresh Make Up Remover',N'Giúp làm sạch vùng mắt và môi sau khi trang điểm. Dễ sử dụng và không gây tổn hại cho da. Thích hợp dùng cho mọi loại da, kể cả da nhạy cảm và da dầu.',34,150000,N'Hàn Quốc','LSP36'
EXEC sp_AddSP N'Tẩy Trang Mắt Môi Nature Republic Hawaiian Deep Sea Lip&Eye Remover',N'Tẩy trang Nature Republic Hawaiian Deep Sea Lip&Eye Remover có xuất xứ từ Hàn Quốc là sản phẩm chăm sóc da giúp làm sạch các lớp bụi bẩn, trang điểm xung quanh mắt và môi – hai khu vực nhạy cảm nhất đòi hỏi kĩ thuật tẩy trang khéo léo. Sản phẩm được bào chế theo công thức hiện đại của Hàn Quốc.',34,280000,N'Hàn Quốc','LSP36'
EXEC sp_AddSP N'Lip & Eye Remover Waterproof EX của Laneige',N'Vùng mắt và môi là nơi da mềm và nhạy cảm nhất nên cần những sản phẩm dùng riêng biệt và an toàn khi tẩy trang. Đặc biệt, trong trường hợp dùng mascara hay kẻ mắt khó làm sạch. Hiểu được nhu cầu của khách hàng hãng Laneige đã cho ra đời dòng sản phẩm nước tẩy trang dành riêng cho mắt và môi: Lip & Eye Remover Waterproof EX',34,155000,N'Hàn Quốc','LSP36'

select * from SANPHAM WHERE MALSP='LSP36'

--tẩy tế bào chết

EXEC sp_AddSP N'Kem tẩy tế bào chết Sebamed Clear Face',N'Kem tẩy tế bào chết Sebamed Clear Face Sebamed Clear Face chứa các thành phần làm sạch nhẹ dịu, không gây kích ứng da, không chứa chất tẩy, nhẹ nhàng loại bỏ lớp da chết, giúp da trắng sáng tự nhiên đồng thời dễ dàng hấp thu các dưỡng chất của kem dưỡng. Sản phẩm cũng có công dụng điều trị và ngăn ngừa mụn hiệu quả.',34,390000,N'Đức','LSP36'
EXEC sp_AddSP N'Kem tẩy tế bào chết Andalou Naturals',N'Công nghệ tế bào gốc từ trái cây (Fruit Stem Cell Complex) tiên tiến của Andalou Naturals làm mới làn da ở cấp độ tế bào, kết hợp giữa tự nhiên và khoa học đem đến những hiệu quả làm sạch da có thể nhìn thấy được. Chiết xuất tế bào gốc từ hoa hồng Alpine (Alpine Rose Stem Cells), trái lựu và vỏ cây Magnolia sẽ nhẹ nhàng loại bỏ đi các lớp trang điểm, bụi bẩn, tạp chất và tế bào chết, mang lại làn da sáng khỏe và mịn màng. Bên cạnh đó, axit hyaluronic và tinh chất lô hội sẽ có tác động nhẹ nhàng giúp phục hồi lớp rào chắn hydro lipid của da.',34,400000,N'Mỹ','LSP37'
EXEC sp_AddSP N'Kem tẩy tế bào chết 3W Clinic Premium Placenta',N'Như chúng ta đã biết trong các bước làm đẹp làn da, thì việc tẩy đi các tế bào chết thì làn da cũng là một trong những bước quan trọng. Chính vì thế kem tẩy tế bào chết 3W Clinic Premium Placenta (Soft Peeling Gel) với chiết xuất nhau thai cừu không chỉ giúp tẩy tế bào chết, còn kích thích quá trình tuần hoàn tái tạo da, làm tăng quá trình dưỡng da, giúp các dưỡng chất thấm sâu vào da, mang lại cho bạn một làn da tươi sáng và mịn màng. Thành phần được bào chế từ protein chiết xuất từ nhau thai, chiết xuất lô hội và đu đủ tẩy sạch tế bào cũ chết và chất thải trên da,làm sạch các chất bẩn bám sâu trong lỗ chân lông một cách nhẹ nhàng, thu nhỏ lỗ chân lông.',34,90000,N'Hàn Quốc','LSP37'
EXEC sp_AddSP N'Kem tẩy tế bào chết 3 tác dụng Vichy Normaderm',N'Kem tẩy tế bào chết 3 tác dụng Vichy Normaderm được sử dụng như sữa rửa mặt để giảm sự xuất hiện của các bã nhờn và tạp chất hay như kem tể tế bào chết để giảm sự xuất hiện của lỗ chân lông hoặc như một mặt nạ giúp loại bà nhờn trên da. Sản phẩm được thử nghiệm và an toàn trên làn da nhạy cảm với 3 tác dụng: làm sạch sâu, tẩy tế bào chết và mặt nạ dưỡng. Sản phẩm dạng cát mềm, hạt nhỏ, làm mịn da và không gây căng khô.',34,410000,N'Pháp','LSP37'
EXEC sp_AddSP N'Tẩy tế bào chết Botani Exfoliating',N'Kem rửa mặt và mặt nạ tự nhiên Botani Exfoliating có xuất xứ tại Úc không gây mài mòn, loại bỏ các tế bào da chết và các tạp chất mà không làm bong tróc da dầu tự nhiên. Công thức độc đáo của Exfoliating 2 in 1 Scrub & Mask chứa hạt nhựa tự nhiên đáng chú ý của thiên nhiên cho microbeads – Jojoba Spheres. Bằng cách sử dụng dầu Jojoba huyền thoại, những hạt thực vật này giống như hình cầu được mịn màng và mang lại lợi ích tẩy tế bào chết nhẹ nhàng.',34,730000,N'Úc','LSP37'

select * from SANPHAM WHERE MALSP='LSP37'

--TRANG ĐIỂM

--kem lót

EXEC sp_AddSP N'3CE Back To Baby Pore Velvet Primer',N'3CE Back To Baby Pore Velvet Primer được tạo nên với phương châm giúp cho phái đẹp tỏa sáng, thời trang với phong cách makup thời thượng. 3CE Skin Tone Control Primer hướng tới khách hàng chủ yếu là các bạn gái trẻ trung, năng động. Kem lót 3CE Skin Tone Control Primer là sản phẩm kem lót đặc biệt có chức năng hiệu chỉnh màu da theo sắc tố da của bạn, được sử dụng trước lớp trang điểm giúp cải thiện tình trạng của da như không đều màu, sạm da.',34,320000,N'Hàn Quốc','LSP20'
EXEC sp_AddSP N'Laura Mercier Pure Canvas Primer',N'Laura Mercier Pure Canvas Primer đến từ hãng Laura Mercier nổi tiếng, là lớp kem lót giúp việc trang điểm lướt nhẹ và lâu trôi hơn làm cho lớp makeup của bạn vẫn tươi mới và giữ nguyên sắc màu trong nhiều giờ. Laura Mercier Primers là loại kem lót water-based và có chứa lượng cực nhỏ silicon và mỏng nhẹ nhất nen lớp kem lót cảm thấy tươi mới và mỏng nhẹ.',34,900000,N'Mỹ','LSP20'
EXEC sp_AddSP N'Make Up For Ever Step 1',N'Make Up For Ever Step 1 là dòng kem lót mới ra năm nay của MUFE, cải tiến từ dòng HD Primer trước, kết cấu lỏng nhưng hơi sánh, đặc biệt có mùi thơm đúng đẳng cấp rất chuyên nghiệp. Dòng sản phẩm MUFE Step 1 Equalizer này còn đang làm mưa làm gió khắp thế giới, suốt 5 châu 4 bể đấy ạ. Kem lót với 10 lựa chọn - làm đều màu da, trung hoà sắc đỏ, làm da tươi sáng mọng nước, dưỡng ẩm cao cấp,... Đây là một vũ khí thực sự tối tân, thực sự hiệu quả cho những cô gái da dầu đang đi tìm một chiếc chìa thần để khoá lớp make up được tươi, đẹp suốt cả ngày.',34,850000,N'Pháp','LSP20'
EXEC sp_AddSP N'Mac Prep + Prime Natural Radiance',N'Mac Prep + Prime Natural Radiance có dạng gel mượt, giàu thành phần tự nhiên, có hydrat và các số nguyên tố để cải thiện bảo vệ da, kiểm soát dầu. Sản phẩm có hai màu, màu vàng đặc biệt hiệu quả trên các tông màu da sậm và hồng cho tông màu da trắng. Kem lót giúp làm mềm, mịn, mượt da đáng kể, giúp dễ dàng tán kem nền. Mac Prep + Prime Natural Radiance che phủ, làm se nhỏ lỗ chân lông, dưỡng ẩm tốt cho da, thích hợp với kiểu trang điểm phủ sương, bóng khỏe và giữ lớp trang điểm lâu trôi.',34,975000,N'Mỹ','LSP20'
EXEC sp_AddSP N'Kem lót 3CE Back To Baby Glow Beam',N'3CE Back To Baby Glow Beam với khả năng tạo độ căng bóng, bắt sáng cho lớp kem nền mượt, tràn đầy sức sống.',34,310000,N'Hàn Quốc','LSP20'

select * from SANPHAM WHERE MALSP='LSP20'

--Kem nền

EXEC sp_AddSP N'Kem nền trang điểm cho lớp nền căng mịn lâu trôi MKUP Illuminating Dewy Star long wearing foundation',N'Kem nền trang điểm cho lớp nền căng mịn lâu trôi MKUP Illuminating Dewy Star long wearing foundation với độ che phủ hoàn hảo khi được kết hợp với làn da trung tính. Độ che phủ hoàn hảo, độ thấm hút nhanh, đem lại làn da mịn màng và không bết dính.',34,400000,N'Đài Loan','LSP21'
EXEC sp_AddSP N'Kem nền Eglips Blur Wearing Foundation (SPF30/ PA++)',N'Kem nền Eglips Blur Wearing Foundation (SPF30/ PA++) mang đến cho bạn một làn da mặt mềm mịn và sáng bóng mà chẳng cần phải nhờ tới lớp phấn phủ thêm dày. Đặc biệt, với thành phần chứa 4 loại tinh chất thực vật lành tính như Guaiazulene hoa cúc , tinh dầu quả rosehip, tinh chất ngọc trai, tinh chất bông sen, giúp cung cấp độ ẩm từ bên trong, duy trì lớp nền bóng khỏe suốt ngày dài.',34,280000,N'Hàn Quốc','LSP21'
EXEC sp_AddSP N'Kem Nền 3CE Lì Mịn Như Nhung',N'Kem Nền 3CE Lì Mịn Như Nhung có độ che phủ cao, giúp lớp trang điểm lâu trôi. Các hạt phấn li ti với kết cấu siêu nhỏ, hấp thụ hoàn toàn vào da, giúp làn da của bạn tỏa sáng xinh đẹp với vẻ tinh khiết, trong trẻo, không tì vết. Màu sắc vô cùng đa dạng nên bạn có thể dễ dàng tìm ra màu sắc phù hợp nhất với làn da của mình. Có khả năng chống lem, thích hợp để sử dụng trang điểm hàng ngày.',34,740000,N'Hàn Quốc','LSP21'

select * from SANPHAM WHERE MALSP='LSP21'

--kem che khuyết điểm

EXEC sp_AddSP N'Giorgio Armani Compact Cream Concealer',N'Giorgio Armani Compact Cream Concealer cũng sẽ là một sản phẩm không thể không nhắc tới trong danh sách này. Bởi với mọi cô gái, ai cũng muốn sở hữu cho mình một sản phẩm thuộc hãng mỹ phẩm cao cấp Giorgio Armani. Sản phẩm này đã chinh phục biết bao cô gái ngay từ cái nhìn đầu tiên với một thiết kế nhỏ gọn, tiện lợi mà vô cùng sang trọng',34,900000,N'Ý','LSP22'
EXEC sp_AddSP N'MAC Pro Longwear Concealer',N'Chất kem của MAC Pro Longwear Concealer là dạng lỏng rất dễ tán, bám lâu và có rất nhiều tone màu để chị em lựa chọn. Bên cạnh đó, kem còn có các dưỡng chất cần thiết cho da, cung cấp độ ẩm cho làn da khô, cần bằng và kiềm dầu cho da dầu và thiên dầu, tránh việc trang điểm hằng ngày gây mụn.',34,750000,N'Mỹ','LSP22'
EXEC sp_AddSP N'NARS Radiant Creamy Concealer',N'Là dòng sản phẩm được các chuyên gia trang điểm khuyên dùng, kem che khuyết điểm NARS Radiant Creamy Concealer chắc chắn sẽ có những ưu điểm khiến bạn phải tìm cách sở hữu nó. Với kết cấu kem sáng ngời và nhẹ tênh, sản phẩm có thể che lấp các khuyết điểm lớn nhỏ chẳng hạn như nếp nhăn hay những dấu hiệu của sự mệt mỏi.
Bên cạnh đó, NARS Radiant Creamy Concealer còn có khả năng cấp nước cho làn da thiếu độ ẩm và hoạt động như một sản phẩm dưỡng da đa chức năng, đồng thời còn có khả năng khuếch tán ánh sáng cho khuôn mặt rạng ngời, mượt mà không tì vết. Khiên vùng da xung quanh mắt thêm sáng hơn, tô điểm cho đôi mắt thêm long lanh.',34,650000,N'Mỹ','LSP22'

select * from SANPHAM WHERE MALSP='LSP22'

--phần phủ

EXEC sp_AddSP N'Estee Lauder',N'Phấn phủ với độ che phủ cao và khả năng thấm hút dầu thừa cực tốt, cho lớp nền mịn lì và lâu trôi lên đến 8 tiếng sau khi sử dụng. Thiết kế hộp phấn sang trọng, quý phái với vỏ màu vàng ánh kim giúp thu hút mọi ánh nhìn mỗi khi bạn sử dụng. Hạt phấn siêu nhỏ vô cùng mỏng mịn và mềm mượt khi apply lên da, cho lớp nền mịn lì nhưng không hề bị hiện tượng khô mốc hay loang lổ mặt...',34,1600000,N'Mỹ','LSP23'
EXEC sp_AddSP N'M.A.C Select Sheer Loose Powder',N'Phấn phủ của M.A.C cũng là một trong những loại phấn phủ cực kì tốt, đáng cho bạn sử dụng. Với thành phần dưỡng chất phấn phủ M.A.C vừa hỗ trợ quá trình trang điểm lại vừa chăm sóc làn da một cách chu đáo nhất. Đặc biệt phấn phủ của M.A.C che phủ tốt, màu phấn chuẩn, độ mịn cao và kiềm dầu tốt và thích hợp sử dụng cho mùa hè.
',34,760000,N'Mỹ','LSP23'
EXEC sp_AddSP N'Phấn phủ 3CE Blur Sebum Powder',N'Với khả năng kiềm dầu hiệu quả cùng công nghệ Blur tạo hiệu ứng mịn màng mang đến cho bạn một lớp phủ nền matte mịn mượt, trong suốt, cho lớp nền lâu trôi, mềm mại suốt cả ngày.',34,520000,N'Hàn Quốc','LSP23'

select * from SANPHAM WHERE MALSP='LSP23'

--tạo khổi

EXEC sp_AddSP N'Revlon',N'Tạo khối và highlight không thể tách rời, giúp bạn cải thiện nhược điểm làm cho khuôn mặt thanh thoát hơn. Revlon Photoready Insta-fix contour & Highlight Duo là bộ đôi tạo khối và highlight không thể thiếu để giúp bạn có được khuôn mặt xinh đẹp hoàn hảo.
',34,350000,N'Mỹ','LSP24'
EXEC sp_AddSP N'The Face Shop',N'Gương mặt bừng sáng, nổi bật một cách tự nhiên, bằng cách nào để tạo được hiệu ứng như vậy, rất dễ dàng các bạn nhé chỉ cần thao tác đơn giản với The Face Shop Signature Highlighter, làn da bạn sẽ tỏa sáng, căng mướt suốt cả ngày. Sản phẩm giúp mang đến lớp make up rạng rỡ, tươi sáng.',34,350000,N'Hàn Quốc','LSP24'
EXEC sp_AddSP N'3CE',N'3CE ngày càng được ưa chuộng ở thị trường Việt Nam, hãng có những sản phẩm trang điểm với mức giá tầm trung rất phù hợp với chi tiêu, trong đó phải kể đến dòng sản phẩm makeup tuyệt vời. Với phấn tạo khối 3CE Magic Touch Face Maker1 nữa là highlight và phần còn lại là shading, cực kỳ tiện dụng phải không? Tùy theo sở thích cá nhân, màu da mỗi người sẽ chọn 1 màu phù hợp cho mình nhé!',34,250000,N'Hàn Quốc','LSP24'

select * from SANPHAM WHERE MALSP='LSP24'

--kẻ chân mày

EXEC sp_AddSP N'Chì Kẻ Mày Tự Nhiên Laneige Natural Brow Liner Auto Pencil (0.3g)',N'Chì Kẻ Mày Tự Nhiên Laneige Natural Brow Liner Auto Pencil giúp người sử dụng vẽ những nét kẻ thanh mảnh đẹp tự nhiên một cách dễ dàng. Đặc biệt, với khả năng hấp thụ mồ hôi và bã nhờn, sản phẩm giúp lớp trang điểm ở lông mày không bị lem, giữ cho lớp trang điểm bền lâu.',34,200000,N'Hàn Quốc','LSP25'
EXEC sp_AddSP N'Chì kẻ mày siêu mảnh NYX Micro Brow Pencil',N'Bút kẻ mày siêu mảnh NYX Micro Brow Pencil là một trong những loại bút trang điểm không thể thiếu giúp định hình đôi chân mày của bạn theo những phong cách thịnh hành nhất hiện nay. Đặc biệt, với các thành phần từ thiên nhiên, sản phẩm không chỉ nhẹ dịu cho da mà còn đem đến một màu sắc thật tươi mới và khả năng bám màu tốt, giúp bạn trang điểm dễ dàng và nhanh chóng.',34,185000,N'Mỹ','LSP25'

select * from SANPHAM WHERE MALSP='LSP25'

--phấn mắt

EXEC sp_AddSP N'Bảng Phấn Mắt 3CE Mini Multi Eye Color Palette',N'phấn mắt 3CE Stylenanda Multi Eye Color Palette: nắm bắt xu hướng nên bảng phấn mắt của hãng đều cập nhật những màu thịnh hành nên bạn muốn hòa theo xu hướng thì đừng ngần ngại mà chọn 3CE nhé.',34,560000,N'Hàn Quốc','LSP26'
EXEC sp_AddSP N'Bảng Phấn Mắt Maybelline New York',N'Thương hiệu Maybelline thành lập bởi T.L. Williams, kết hợp hai từ Mabel và Vaseline, Maybelline đã trở thành một trong những ông lớn của ngành công nghiệp mỹ phẩm. Năm 1917, Maybelline đã cho ra mắt dòng sản phẩm Maybelline Cake Mascara làm nên cuộc cách mạng cho ngành mỹ phẩm và mascara hiện đại đầu tiên đã được ra mắt. Kể từ đó đến nay, thương hiệu đã thành công trong việc chinh phục chị em phụ nữ trên toàn thế giới. Năm 2007 Maybelline chính thức xuất hiện tại Việt Nam, nhanh chóng trở thành thương hiệu trang điểm hàng đầu, là thương hiệu được nhiều người biết đến ở dòng sản phẩm mặt, môi trong đó phấn mắt cũng nổi không kém dòng Mascara.
',34,260000,N'Mỹ','LSP26'
EXEC sp_AddSP N'Bảng Phấn Mắt Perfect Diary',N'Bảng Phấn Mắt Perfect Diary dạng phấn nén với các hạt phấn siêu mịn, lên màu chuẩn và bền màu. Hộp phấn thiết kế đẹp mắt, nhiều màu với chủ đề những cong vật hoang dã giúp người dùng có thể dễ dàng kết hợp sử dụng, phối hợp trang điểm. Màu sắc bóng, ấn tượng, cho hiệu quả vẽ Line mắt cao. Thành phần bổ sung dưỡng chất chăm sóc da mắt. Thiết kế dạng hộp nhỏ gọn tiện lợi, đi cùng đầu cọ mềm mại, dễ dàng đánh và tán mỏng phấn với đầu cọ mịn.',34,340000,N'Trung Quốc','LSP26'

select * from SANPHAM WHERE MALSP='LSP26'

--kẻ mắt

EXEC sp_AddSP N'Bút kẻ mắt Canmake Strong Eyes Liner',N'Bút kẻ mắt Canmake nổi tiếng chiều lòng tất cả “cô chủ” với màu sắc tự nhiên như chính đôi mắt bạn toát ra. Bạn đừng lo tay chân lọng cọng vì bàn chải được làm từ chất liệu thiên nhiên nên mềm mịn cho bút có mức độ linh hoạt đi theo hướng đúng ý người dùng. Canmake Strong Eyes Liner Nhật còn cho phép người dùng chạy theo xu hướng vẽ mắt thay đổi liên tục trong suốt thời gian qua với dạng ngòi bút thanh mảnh vừa phải thích hợp mọi khuôn mẫu trào lưu.',34,420000,N'Nhật Bản','LSP27'
EXEC sp_AddSP N'Bút kẻ mắt nước cao cấp Shiseido Maquillage Secret Shading Liner 0.4ml',N'Bút kẻ mắt nước ShiseidoMaquillage Secret Shading Liner với đầu lông mảnh giúp bạn dễ dàng kẻ được đường kẻ từ mảnh đến đậm, cho đôi mắt ấn tượng và quyến rũ. Kẻ mắt nước nhanh khô, kết hợp cùng công thức chống trôi bảo vệ màu mực khỏi mồ hôi, nước, dầu và cọ xát, cho đôi mắt đẹp suốt cả ngày. Đặc biệt, màu mực được bảo quản riêng để đảm bảo mực luôn mới và không bị khô.
',34,590000,N'Nhật Bản','LSP27'
EXEC sp_AddSP N'Bút Kẻ Mắt 3CE',N'Bút Kẻ Mắt 3CE là kẻ mắt dạng nước có thể kẻ được nhiều loại make-up khác nhau từ nhẹ nhàng, tự nhiên đến sâu sắc và quyến rũ với đầu chổi nhỏ và dài, dễ dàng kẻ các đường viền mảnh sát mí cùng chiết xuất thành phần giúp cung cấp thêm độ ẩm cho phần da nhạy cảm nhất là phần viền mắt và đuôi mắt. Đặc biệt, Bút Kẻ Mắt 3CE sở hữu đầu bút kẻ mảnh, mềm mại giúp dễ dàng tạo viền mắt mà không gây lem, không trôi, cùng với chất collagen hỗ trợ nuôi dưỡng mi mắt, tăng cường độ đàn hồi, an toàn cho da.',34,300000,N'Hàn Quốc','LSP27'

select * from SANPHAM WHERE MALSP='LSP27'

--mascara

EXEC sp_AddSP N'Guerlain',N'Một cây mascara như dưới đây hẳn cũng là niềm ao ước và yêu thích của không ít những cô nàng hay make up. Sản phẩm mascara của Guerlain toát lên vẻ ngoài bắt mắt và sang trọng. Với thiết kế chi tiết và tỉ mỉ, cây mascara của hãng này thách thức cả những sợi mi mỏng manh và ngắn nhất, cho bạn hàng mi cong mềm mại, nữ tính. Những hạt mascara siêu mịn, nhanh khô, và có thể dễ dàng tẩy trang bằng nước ấm.',34,580000,N'Pháp','LSP28'
EXEC sp_AddSP N'Yves Rocher',N'Yves Rocher Volume Elixir được yêu thích không bởi thương hiệu mà chất lượng sản phẩm này cũng rất tuyệt vời. Với thành phần được chiết xuất từ hoa dâm bụt không những giúp dưỡng mi mà còn giúp hàng mi dày lên rõ rệt chỉ sau một lần chải. Công thức sản phẩm của mascara không tạo cảm giác nặng nề và đảm bảo đôi mi dày hoàn hảo cả ngày. Đầu chải hình nón với sợi chải kép và mềm cho phép chải các sợi mi phía trong một cách dễ dàng, mang đến ánh nhìn cuốn hút và quyến rũ.',34,400000,N'Pháp','LSP28'
EXEC sp_AddSP N'The Face Shop',N'The Face Shop Mega proof Mascara khiến đôi mắt của bạn trở nên đẹp và thu hút hơn bởi hàng mi cong và đen nhánh, hàng lông mi cong vút tạo điểm nhấn. Nó khiến cho đôi mắt của bạn có thêm chiều sâu, thu hút hơn. Sản phẩm chiết xuất hoàn toàn từ thiên nhiên giúp bổ sung thêm dưỡng chất mà không gây kích ứng mắt kể cả mắt nhạy cảm nhất.- Sản phẩm sẽ giúp mi dài và cong hơn gấp 2 lần so với mi tự nhiên cho bạn hàng mi đen nhánh, tự nhiên và thu hút',34,230000,N'Hàn Quốc','LSP28'
select * from SANPHAM WHERE MALSP='LSP28'

--má hồng

EXEC sp_AddSP N'Phấn Má Hồng Mịn Lì 3CE',N'Phấn Má Hồng Mịn Lì 3CE mang đến khuôn mặt hồng hào, tươi tắn. Phấn có kết cấu phấn lên da mịn màng và tự nhiên hơn, đem đến hiệu quả trang điểm hoàn hảo. Phấn má hồng 3CE là bí quyết để có được gương mặt luôn tự tin, rạng ngời, là chìa khóa thần kì giúp mang đến vẻ đẹp nhẹ nhàng, ngọt ngào cho bạn gái.',34,400000,N'Hàn Quốc','LSP29'
EXEC sp_AddSP N'Phấn má MAC Powder Blush Burnt Pepper',N'Dòng phấn má Mac Burnt Pepper này với chất phấn mịn, đậm màu hay gọi là pigmented cùng với độ bám siêu tốt trên da. Dễ dàng mang tới đôi má hồng tự nhiên, nhẹ nhàng thu hút. Hơn nữa package em này siêu xinh cũng như siêu chắc chắn nhờ thiết kế hộp tròn sang trọng với nắp bật trong suốt, cho phép bạn có thể nhìn thấy màu phấn bên trong được in logo của thương hiệu MAC.',34,670000,N'Mỹ','LSP29'
EXEC sp_AddSP N'Phấn Má Hồng MISSHA Cotton Mix Blusher',N'Phấn má hồng Missha Cotton Mix Blusher có kết cầu lì, mềm, không bết bột tăng hiệu quả pha trộn màu sắc mềm mại. Màu sắc sống động, tự nhiên, trong suốt. Kết cấu phấn mỏng nhẹ thực sự hoàn hảo cho công đoạn highlight, tạo khối cho gương mặt. Sự kết hợp độc đáo trong màu sắc và chất bóng, lì của phấn giúp bạn dễ dàng tạo ra muôn vàn phong cách trang điểm ấn tượng. Có thể sử dụng như phấn highlight để tạo ấn tượng khuôn mặt thon gọn và nổi bật như ý muốn.',34,190000,N'Hàn Quốc','LSP29'

select * from SANPHAM WHERE MALSP='LSP29'

--son thỏi

EXEC sp_AddSP N'Son Hermès Matte Màu 68 Rouge Bleu',N'Son Hermes 68 Rouge Bleu một trong những màu son đỏ được yêu thích nhất của quý cô hiện nay và Hermès 68 Rouge Bleu cũng là màu son hứa hẹn bán chạy nhất của bộ sưu tập son môi Rouge Hermès. Son Hermès Matte 68 có chất son lì giữ màu tốt nên các Nàng yên tâm ăn uống không lo phai màu nhé.',34,2050000,N'Ý','LSP30'
EXEC sp_AddSP N'Son Lì Christian Louboutin Màu Triluna 006M Vừa Ra Mắt',N'Son Louboutin 006M Triluna Vừa Ra Mắt với tông cam tươi pha đỏ, màu son tươi trẻ, thu hút, là thỏi son đẳng cấp của thương hiệu Christian Louboutin. Thuộc dòng son lì lên môi chuẩn màu, đặc biệt son không chứa chì không gây khô môi Christian Louboutin 006M hứa hẹn sẽ đem đến cho các bạn gái một bờ môi căng mọng trẻ trung.',34,2850000,N'Mỹ','LSP30'
EXEC sp_AddSP N'Son Lì Chanel Rouge Allure Velvet Extreme Màu 116 Extreme ',N'Son Chanel 116 Extreme màu đỏ rượu mang đến cho phái đẹp một diện mạo cá tính, bí ẩn và quyến rũ trong những buổi tiệc đêm. Đặc biệt hơn, son Chanel 116 Extreme còn được chiết xuất từ các thành phần tự nhiên giúp bảo vệ làn môi dưới những tác nhân có hại từ môi trường, có khả năng chống oxi hóa mang đến môi làn môi mềm mịn, căng mọng suốt ngày dài năng động.',34,850000,N'Pháp','LSP30'
EXEC sp_AddSP N'Son Dior Rouge 666 Matte Kisss Golden Nights Xmas 2020 ( Phiên Bản Đặc Biệt)
',N' Dior 666 Matte Kisss Golden Nights màu son mới nhất trong bảng màu của Dior. Chất son mịn, lì mà lại không quá khô môi. Son Dior 666 Limited là một lựa chọn hoàn hảo cho một đôi môi nổi bật với màu sắc đỏ pha hồng chân thực rực rỡ đầy lôi cuốn dành cho các Nàng năm nay',34,850000,N'Pháp','LSP30'
EXEC sp_AddSP N'Son Lì Gucci Rouge À Lèvres Voile Mat Màu 25 Goldie Red',N'Son Lì Gucci 25 Goldie Red là một màu mới nhất của hãng Gucci, với màu đỏ nữ tính đáng yêu. Màu son vô cùng đáng yêu trẻ trung, với tông màu này các bạn gái sẽ thấy mình trẻ trung gợi cảm hơn rất nhiều các Nàng nhé',34,900000,N'Ý','LSP30'

select * from SANPHAM WHERE MALSP='LSP30'

--son kem

EXEC sp_AddSP N'Son YSL Kem Màu 204 Beige Underground ( New 2020 )',N'Son YSL kem 204 Beige Underground với một tông màu hồng đào mơ mộng mới toanh, cho bờ môi ngọt ngào một cách tự nhiên nhất.',34,850000,N'Pháp','LSP31'
EXEC sp_AddSP N'Christian Louboutin Loubilaque',N'chất son của Loubilaque mịn màng đem đến cho người sử dụng một làm môi căng mọng, đầy gợi cảm. Son kem Louboutin ghi điểm nhờ màu son tươi tắn, rạng rỡ cộng thêm dưỡng chất siêu mềm mịn nhưng không quá bóng sáng giúp các nàng hay cả tô son trong mùa hè cũng không tạo cảm giác nóng bức.',34,1900000,N'Mỹ','LSP31'
EXEC sp_AddSP N'Chanel Rouge Allure Ink',N'Chanel Rouge Allure Ink có thiết kế sang trọng, thời thượng với thân son được thiết kế nhỏ nhắn, lớp vỏ bên ngoài đường vân lì mờ ảo giúp bạn gái có thể dễ dàng nhìn được màu son, logo thương hiệu màu trắng in nổi trên nền đen của nắp son càng làm phái đẹp thêm say đắm. Son kem Chanel có cọ quét son hình tam giác, vát bằng phẳng ở 4 mặt giúp son bám đều trên bờ môi, phần đầu cọ quét hơi nhọn giúp bạn gái tô viền môi chuẩn xác hơn, không bị lem ra ngoài.',34,850000,N'Pháp','LSP31'
EXEC sp_AddSP N'Dior Diorific Matte Fluid',N'Diorific Matte Fluid mang sắc màu nổi bật phù hợp với từng cá tính của bạn. Bên cạnh đó, công nghệ mới nhất mang đến màu sắc đầy quyến rũ và độ bền màu cao, nhưng vẫn giữ sự mượt mà và ẩm độ cho đôi môi.',34,1000000,N'Pháp','LSP31'
EXEC sp_AddSP N'Burberry Liquid Lip Velvet',N'Burberry luôn mang họa tiết caro đặc trưng, sang trọng. Chất son mới là tiêu chí đánh giá giúp Burberry lại một lần nữa khẳng định chất lượng son của mình. Là loại son kem lì, ngoài việc khi đánh lên môi sẽ giữ màu được lâu, son còn giúp môi bạn trở nên mềm mịn hơn, không gây khô môi, bong tróc. Hơn nữa, son sẽ giúp bạn che được khuyết điểm trên môi để bạn có thể tự tin tỏa sáng.',34,730000,N'Anh','LSP31'

select * from SANPHAM WHERE MALSP='LSP31'

--son dưỡng

EXEC sp_AddSP N'Innisfree',N'Son dưỡng môi Canola Honey là son dưỡng dạng thỏi chiết xuất từ mật ong và tinh dầu hoa cải từ thiên nhiên đảo Jeju. Mật ong có độ dưỡng ẩm cao, kháng khuẩn, bảo vệ da khỏi các tác hại của môi trường sống xung quanh.',34,160000,N'Hàn Quốc','LSP32'
EXEC sp_AddSP N'MAC',N'son dưỡng TenderTalk Lip Balm. Son MAC TenderTalk Lip Balm là dòng son dưỡng có màu chắc chắn sẽ khiến các tín đồ của MAC không thể rời mắt từ cái nhìn đầu tiên. Nhờ tác dụng dưỡng ẩm nên môi bạn sẽ luôn ẩm mượt, màu son thể hiện sắc độ vừa đủ.',34,600000,N'Mỹ','LSP32'
EXEC sp_AddSP N'Laneige',N'Son môi dưỡng ẩm Moisture. Son môi dưỡng ẩm Moisture tạo độ ẩm xoa dịu làn môi và nuôi dưỡng môi khô, nứt nẻ. Bằng công nghệ mới ổn định polyol của nước nên có khả năng duy trì dưỡng ẩm và tạo độ ẩm dồi dào cho đôi môi.',34,600000,N'Hàn Quốc','LSP32'
EXEC sp_AddSP N'Son Dưỡng Môi DHC ',N'Son Dưỡng Môi DHC có hợp chất dưỡng ẩm chiết xuất từ thực vật thiên nhiên: dầu olive nguyên chất, lô hội, cam thảo, vitamin E…giúp dưỡng ẩm cho môi mềm mại, mịn màng trong thời gian dài, phòng tránh khô, nứt nẻ môi. Sản phẩm không màu, không mùi, không paraben, đặc biệt không gây bóng nhờn.',34,350000,N'Nhật Bản','LSP32'
EXEC sp_AddSP N'Son Dưỡng Môi Dior Addict Lip Glow',N'Son Dior Addict Lip Glow chứa thành phần giúp “tươi màu môi” và “tươi màu son môi” đồng thời bảo vệ môi khỏi tia UV từ mặt trời. Chứa thành phần chống nắng SPF 10 bảo vệ môi khỏi tác hại từ tia UV, Dior Addict Lip Glow sẽ giúp cho môi tránh bị thâm và lão hóa.',34,760000,N'Pháp','LSP32'

select * from SANPHAM WHERE MALSP='LSP32'

--phấn nước cushion

EXEC sp_AddSP N'Phấn Nước April Skin Black Magic Snow Cushion Galaxy Edition',N'Phấn Nước April Skin Black Magic Snow Cushion Galaxy Edition với tone màu tự nhiên, nhiều chất dưỡng ẩm, có khả năng che phủ tốt, sản phẩm thích hợp với các nàng da khô, da nhờn và da thường, tích hợp 3 tác dụng chỉ trong 1 sản phẩm đó là: vừa là phấn phủ, vừa là kem nền và vừa là kem che khuyết điểm với lớp nền mỏng nhẹ nhưng độ che phủ khá tốt đem đến cho bạn làn da luôn mịn màng tươi sáng cùng khả năng giữ tone khá tốt giúp bạn luôn rạng ngời.',34,380000,N'Hàn Quốc','LSP33'
EXEC sp_AddSP N'Phấn Nước Che Phủ Mịn Lì 24H LANEIGE NEO Cushion Matte',N'Phấn Nước Che Phủ Mịn Lì 24H LANEIGE NEO Cushion Matte chứa những hạt phấn nhỏ, bám sâu dễ nhàng lắp đầy các khuyết điểm không đáng có trên khuôn mặt, các khuyết điểm này sẽ được che phủ đem lại cho bạn một làn da trắng mịn và không tỳ vết lại vô cùng nhẹ nhàng, tự nhiên, thoải mái. Không những thế, thiết kế bao bì của Neo cushion dựa trên công nghệ kéo sợi một lần chạm có thể dễ dàng thay đổi refill cushion bên trong. Do vây, phái đẹp có thể hoàn thoàn tự tin xuống phố cùng bạn bè và đồng nghiệp nhé!',34,950000,N'Hàn Quốc','LSP33'
EXEC sp_AddSP N'Phấn nước trang điểm mỏng nhẹ innisfree Light Fit Cushion SPF33 PA++',N'Phấn nước trang điểm mỏng nhẹ innisfree Light Fit Cushion SPF33 PA++ là sản phẩm đã có mặt khá lâu của thương hiệu mỹ phẩm Hàn Quốc Innisfree nhưng phiên bản mới được ra mắt có vỏ hộp với khoảng 100 lựa chọn xinh xắn và nữ tính khác nhau đã nhanh chóng chiếm được cảm tình của các tín đồ làm đẹp.',34,400000,N'Hàn Quốc','LSP33'

select * from SANPHAM WHERE MALSP='LSP33'


select TENSP,TENLOAI from SANPHAM,LOAISP WHERE SANPHAM.MALSP=LOAISP.MALSP

--BẢNG HÓA ĐƠN
	CREATE PROC sp_AddHD(
    @NGTAO DATETIME, -- NGÀY TẠO HÓA ĐƠN
    @THANHTOAN FLOAT, -- TỔNG (SỐ LƯỢNG * ĐƠN GIÁ)
    @TINHTRANG NVARCHAR(50), -- CHƯA THANHTOAN,DATT
    @MAKH VARCHAR(11),
    @MANV VARCHAR(10) )

AS 
	--THÊM MỚI DỮ LIỆU
	INSERT INTO HOADON(NGTAO,THANHTOAN,TINHTRANG,MAKH,MANV)
	VALUES(@NGTAO,@THANHTOAN,@TINHTRANG,@MAKH,@MANV)
GO

EXEC sp_AddHD '08/12/2020','1',N'Đã thanh toán','0934214565','NV01'
EXEC sp_AddHD '08/01/2020','1',N'Đã thanh toán','0948571923','NV02'
EXEC sp_AddHD '08/05/2020','1',N'Đã thanh toán','0968492918','NV03'

--BẢNG CHI TIẾT HÓA ĐƠN
CREATE PROC sp_AddCTHD(
    @MAHD INT,
    @MASP INT,
    @SOLUONG INT)

AS 
	--KIỂM TRA TRÙNG KHÓA CHÍNH
	IF EXISTS(SELECT * FROM CHITIETHD WHERE MASP=@MASP AND MAHD=@MAHD)
		RETURN 0
	--THÊM MỚI DỮ LIỆU
	INSERT INTO CHITIETHD(MAHD,MASP,SOLUONG)
	VALUES(@MAHD,@MASP,@SOLUONG)
GO

EXEC sp_AddCTHD '1','97','1'
EXEC sp_AddCTHD '1','99','1'
EXEC sp_AddCTHD '1','7','1'
EXEC sp_AddCTHD '1','32','1'
EXEC sp_AddCTHD '1','180','1'
EXEC sp_AddCTHD '1','69','1'

EXEC sp_AddCTHD '2','4','1'
EXEC sp_AddCTHD '2','99','1'
EXEC sp_AddCTHD '2','97','1'
EXEC sp_AddCTHD '2','1','1'
EXEC sp_AddCTHD '2','2','1'
EXEC sp_AddCTHD '2','3','1'


EXEC sp_AddCTHD '3','97','1'
EXEC sp_AddCTHD '3','99','1'
EXEC sp_AddCTHD '3','8','1'
EXEC sp_AddCTHD '3','9','1'
EXEC sp_AddCTHD '3','3','1'
EXEC sp_AddCTHD '3','5','1'

SELECT * FROM CHITIETHD
SELECT * FROM HOADON
SELECT * FROM SANPHAM

--Thống kê doanh thu
CREATE PROC up_RevenueStatistics @From Date, @To Date
AS
	SELECT CONVERT(DATE,NGTAO) 'NgayXuatHD', SUM(THANHTOAN) 'ThanhTien'
	FROM HOADON 
	WHERE CONVERT(DATE,NGTAO) BETWEEN @From AND @To 
	GROUP BY CONVERT(DATE,NGTAO)
GO

--Thống kê sản phẩm
CREATE PROC up_ProductStatistics @From Date, @To Date
AS
	SELECT TENSP, SUM(CHITIETHD.SOLUONG) 'TongSoLuong'
	FROM HOADON, CHITIETHD, SANPHAM
	WHERE HOADON.MAHD=CHITIETHD.MAHD AND CHITIETHD.MASP=SANPHAM.MASP
	AND CONVERT(DATE, NGTAO) BETWEEN @From AND @To
	GROUP BY SANPHAM.MASP, TENSP
GO


CREATE PROC sp_ChartSanPham
@nam int
AS
	SELECT TOP 10 TENSP, SUM(CTHD.SOLUONG) SOLUONGBANRA
	FROM HOADON HD JOIN CHITIETHD CTHD 
		ON HD.MAHD = CTHD.MAHD JOIN SANPHAM SP
		ON SP.MASP = CTHD.MASP
	WHERE YEAR(NGTAO) = @nam
	GROUP BY TENSP
	ORDER BY SOLUONGBANRA DESC
GO

CREATE PROC sp_ChartNhanVien
@nam int
AS
	SELECT HOTEN, SUM(THANHTOAN) DOANHTHU
	FROM HOADON HD JOIN NHANVIEN NV
		ON NV.MANV = HD.MANV JOIN THONGTINTAIKHOAN TTTK
		ON TTTK.MATK = NV.MATK
	WHERE YEAR(HD.NGTAO) = @nam
	GROUP BY HOTEN
	ORDER BY DOANHTHU DESC
GO

CREATE PROC sp_ChartDoanhThu
@nam int
AS
	DECLARE @tbDoanhThu TABLE (THANG INT, DOANHTHU FLOAT)
	INSERT @tbDoanhThu
		SELECT m, SUM(THANHTOAN) DOANHTHU
		FROM (SELECT * FROM HOADON WHERE YEAR(NGTAO) = @nam) dtn RIGHT JOIN (
			SELECT m
			FROM (VALUES (1), (2), (3), (4), (5), (6), (7), (8), (9), (10), (11), (12))
			[1 to 12](m)
		) listMonth
			ON MONTH(NGTAO)=listMonth.m
		GROUP BY m
		ORDER BY m
	
	UPDATE @tbDoanhThu SET DOANHTHU = 0 WHERE DOANHTHU IS NULL
	SELECT * FROM @tbDoanhThu
GO
