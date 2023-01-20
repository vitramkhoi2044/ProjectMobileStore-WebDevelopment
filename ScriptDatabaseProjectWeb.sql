use ProjectWeb
Go

INSERT INTO BrandCategories(BrandName,BrandDescription) VALUES(N'Apple',N'abc')
Go
INSERT INTO BrandCategories(BrandName,BrandDescription) VALUES(N'Samsung',N'abcd')
Go
INSERT INTO BrandCategories(BrandName,BrandDescription) VALUES(N'Oppo',N'bcd')
Go
INSERT INTO BrandCategories(BrandName,BrandDescription) VALUES(N'Xiaomi',N'abcde')
Go
INSERT INTO BrandCategories(BrandName,BrandDescription) VALUES(N'Vivo',N'cde')
Go

INSERT INTO Mobiles(MobileName,Color,TotalImage,MobileType,Price,BrandID) VALUES(N'iPhone 14 Pro Max',N'Tím',4,N'Smartphone',33990000,1)
Go
INSERT INTO Mobiles(MobileName,Color,TotalImage,MobileType,Price,BrandID) VALUES(N'iPad Pro M1 WiFi',N'Đen',4,N'Tablet',25490000,1)
Go
INSERT INTO Mobiles(MobileName,Color,TotalImage,MobileType,Price,BrandID) VALUES(N'Galaxy S22 Ultra 5G',N'Đỏ',4,N'Smartphone',25990000,2)
Go
INSERT INTO Mobiles(MobileName,Color,TotalImage,MobileType,Price,BrandID) VALUES(N'Galaxy Tab S8 Ultra',N'Xám',4,N'Tablet',27990000,2)
Go
INSERT INTO Mobiles(MobileName,Color,TotalImage,MobileType,Price,BrandID) VALUES(N'Reno8 Pro 5G',N'Xanh ngọc',4,N'Smartphone',18990000,3)
Go
INSERT INTO Mobiles(MobileName,Color,TotalImage,MobileType,Price,BrandID) VALUES(N'iPad Air 5 M1 Wifi',N'Đen',4,N'Tablet',20490000,1)
Go
INSERT INTO Mobiles(MobileName,Color,TotalImage,MobileType,Price,BrandID) VALUES(N'Xiaomi Redmi Note 11S 5G',N'Xanh dương nhạt',4,N'Smartphone',6490000,4)
Go
INSERT INTO Mobiles(MobileName,Color,TotalImage,MobileType,Price,BrandID) VALUES(N'Xiaomi Pad 5',N'Trắng',4,N'Tablet',10490000,4)
Go
INSERT INTO Mobiles(MobileName,Color,TotalImage,MobileType,Price,BrandID) VALUES(N'Vivo V25 5G',N'Vàng',4,N'Smartphone',10490000,5)
Go
INSERT INTO Mobiles(MobileName,Color,TotalImage,MobileType,Price,BrandID) VALUES(N'Galaxy Tab A8',N'Bạc',4,N'Tablet',7490000,2)
Go

INSERT INTO Promotions(DiscountCode,PercentDiscount,StartDate,EndDate) VALUES('SALE15',15,'2022-12-13','2022-12-15')
Go

Select * From Mobiles
Select * From BrandCategories
Select * From Promotions