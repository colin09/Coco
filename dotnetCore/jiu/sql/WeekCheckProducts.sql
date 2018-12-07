DECLARE @BeginTime DATETIME =DATEADD(d,-7, DATEADD(wk, datediff(wk,0,getdate()), 0))  --本周第一天

INSERT INTO [dbo].[WeekCheckProducts]
	 ([ProductId]
	 ,[ProductName]
	 ,[Specification]
	 ,[Unit]
	 ,[PackageQuantity]
	 ,[Category]
	 ,[BrandName]
	 ,[CityId]
	 ,[CityName]
	 ,[StoreHouseId]
	 ,[StoreHouseName]
	 ,[StoreHouseNewId]
	 ,[ProductNewId]
	 ,[IsTakeNote])
SELECT p.Id ProductId,info.Desc_ProductName ProductName,info.Desc_Specifications Specification,
	info.Desc_PackageUnit Unit,info.Desc_PackageQuantity PackageQuantity,info.Desc_OldCategory Category,
	info.Desc_BrandName BrandName,city.Id,city.Name CityName,o.Id,o.Name StoreHouseName,o.NewId,p.NewId,0
FROM (
	SELECT t.Product_Id,t.StoreHouse_Id FROM (
			SELECT t.Product_id,t.StoreHouse_Id,row_number() OVER (PARTITION BY t.StoreHouse_Id ORDER BY t.saleCount desc) rownumber FROM (
					SELECT i.Product_Id,o.StoreHouse_Id,SUM((CASE WHEN i.SaleUnit = i.Specifications THEN i.Count*i.PackageQuantity ELSE i.count end)) saleCount  FROM
					dbo.OrderItem i WITH(NOLOCK)
					INNER JOIN dbo.Orders o WITH(NOLOCK) ON i.Order_Id =o.Id AND o.CompleteTime > '2018-05-15' where o.StoreHouse_Id='9991'
					GROUP BY i.Product_Id,o.StoreHouse_Id) t
			) t WHERE t.rownumber <= 100
			UNION
			SELECT Product_Id,StoreHouse_Id FROM dbo.ProductStocks WITH(NOLOCK) WHERE CostPrice >=100  and StoreHouse_Id='9991'
	) t
INNER JOIN dbo.Product p  WITH(NOLOCK) ON t.Product_Id = p.Id AND p.ProductType IN (2,4,6,8,9)
INNER JOIN dbo.ProductInfo info WITH(NOLOCK)  ON p.Info_Id = info.Id
INNER JOIN dbo.StoreHouse sh WITH(NOLOCK) ON t.StoreHouse_Id=sh.id AND sh.StoreHouseType IN (0,1)
INNER JOIN dbo.Orgs o WITH(NOLOCK) ON sh.Id = o.Id AND o.Enable =1
INNER JOIN dbo.Orgs city WITH(NOLOCK) ON sh.City_Id=city.Id





