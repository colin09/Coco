--------------------------------------------------------------------------------------------------------------------------------------------------------------------
/**
    group by product+month
*/

SELECT 
ISNULL(t_purchase.DateMonth, t_product.DateMonth) DateMonth,ISNULL(t_purchase.CityName, t_product.CityName) CityName,ISNULL(t_purchase.ProductName, t_product.ProductName) ProductName,
ISNULL(t_purchase.PackageQuantity, t_product.PackageQuantity) PackageQuantity,ISNULL(t_purchase.Unit, t_product.Unit) Unit,ISNULL(t_purchase.Specifications, t_product.Specifications) Specifications,
t_purchase.PurchaseCount,t_purchase.PurchaseAmount,t_purchase.PurchaseBareAmount,t_product.SaleCount,t_product.RebateAmount,t_product.SaleAmount,t_purchase.SurplusCount,t_purchase.SurplusAmount,t_purchase.SurplusBareAmount
FROM
(SELECT 
CONVERT(varchar(7),Purchase.CreateTime,120) DateMonth,MAX(Orgs.Id) CityId, MAX(Orgs.Name) CityName,Product_Id,MAX(Info_Id) Info_Id,
SUM([Count]) PurchaseCount,SUM([Count]*Price) PurchaseAmount,SUM([Count]*BarePrice) PurchaseBareAmount,
SUM([Count]-SettlementCount) SurplusCount,SUM(([Count]-SettlementCount)*Price) SurplusAmount,SUM(([Count]-SettlementCount)*BarePrice) SurplusBareAmount,
Max(Purchase.ProductName) ProductName,Max(Purchase.PackageQuantity) PackageQuantity,Max(Purchase.Unit) Unit,Max(Purchase.Specifications) Specifications
FROM AgentPurchaseSettlementNotes Purchase
JOIN Product ON product.Id = Purchase.Product_Id
INNER JOIN dbo.Orgs WITH(NOLOCK) ON Orgs.Id = Purchase.SettlementCity_Id
WHERE AgentCity_Id =  '897' --AND Purchase.CreateTime >= @startM AND Purchase.CreateTime <= @endM
GROUP BY Product_Id,CONVERT(varchar(7),Purchase.CreateTime,120)) t_purchase
FULL JOIN
(SELECT 
CONVERT(varchar(7),ProductSettlement.CreateTime,120) DateMonth,Product_Id,MAX(Info_Id) Info_Id, MAX(Orgs.Id) CityId,MAX(Orgs.Name) CityName,
SUM(ProductSettlement.SaleCount) SaleCount,SUM(ProductSettlement.SettlementRebateAmount) RebateAmount,SUM(ProductSettlement.SettlementSaleAmount) SaleAmount,
Max(ProductSettlement.ProductName) ProductName,Max(ProductSettlement.PackageQuantity) PackageQuantity,Max(ProductSettlement.Unit) Unit,Max(ProductSettlement.Specifications) Specifications
FROM AgentProductSettlementNotes ProductSettlement WITH(NOLOCK)
JOIN Product ON product.Id = ProductSettlement.Product_Id
INNER JOIN dbo.Orgs WITH(NOLOCK) ON Orgs.Id = ProductSettlement.SettlementCity_Id
where AgentCity_Id = '897' --AND ProductSettlement.CreateTime >= @startM AND ProductSettlement.CreateTime <= @endM
GROUP BY Product_Id,CONVERT(varchar(7),ProductSettlement.CreateTime,120)) t_product
ON t_product.Info_Id = t_purchase.Info_Id AND t_product.CityId = t_purchase.CityId
ORDER BY DateMonth,CityName
OFFSET 0 ROWS FETCH NEXT 20 ROWS ONLY

--------------------------------------------------------------------------------------------------------------------------------------------------------------------

SELECT Product_Id,
SUM([Count]-SettlementCount) SurplusCount,SUM(([Count]-SettlementCount)*Price) SurplusAmount,SUM(([Count]-SettlementCount)*BarePrice) SurplusBareAmount
FROM AgentPurchaseSettlementNotes Purchase
WHERE AgentCity_Id =  '897' --AND Purchase.CreateTime <= @endM
GROUP BY Product_Id


--------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------------------------------------


