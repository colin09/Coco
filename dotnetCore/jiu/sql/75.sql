
SELECT top 100 * FROM PurchaseInStockNotes;

SELECT TOp 100 * FROM City;
SELECT TOp 100 * FROM Orgs;

SELECT TOp 100 * FROM ProductLots;



DECLARE @cityId varchar(32)
DECLARE @storeHouseId varchar(32)
DECLARE @productId varchar(32)

SELECT TOP 1 ProductionDate FROM ProductLots WHERE City_Id = @cityId AND StoreHouse_Id = @storeHouseId AND Product_Id = @productId ORDER BY ProductionDate DESC;

