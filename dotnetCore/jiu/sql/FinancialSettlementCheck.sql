

DECLARE @City_Id VARCHAR(32)
DECLARE @BeginTime VARCHAR(32)
DECLARE @EndTime VARCHAR(32)

SET @City_Id = '5074486180318214227'
SET @BeginTime = '2018-09-01 00:00:00'
SET @EndTime = '2018-10-01 00:00:00'

--订单
SELECT COUNT(1) FROM dbo.Orders o WITH(NOLOCK) WHERE o.City_Id = @City_Id AND o.CompleteTime BETWEEN @BeginTime and @EndTime and HasCreateNote = 0;
--退货单
SELECT COUNT(1) FROM dbo.ReturnOrders ro WITH(NOLOCK) LEFT JOIN dbo.Orders o  WITH(NOLOCK) ON ro.Order_Id = o.Id WHERE o.City_Id = @City_Id  AND ReturnOrderTime_CompleteTime BETWEEN @BeginTime and @EndTime and HasCreateReturnNote=0;
--采购入库单
SELECT COUNT(1) FROM dbo.PurchaseInStockNotes WITH(NOLOCK)  WHERE City_Id = @City_Id AND IsDeleted=0 AND  AuditState<>9 AND InStockTime BETWEEN @BeginTime and @EndTime;
--采购退货单
SELECT COUNT(1) FROM dbo.PurchaseOutStockNotes WITH(NOLOCK) WHERE City_Id = @City_Id AND IsDeleted=0 AND  AuditState<>9 AND OutStockTime BETWEEN @BeginTime and @EndTime;
--销售出库单
SELECT COUNT(1) FROM dbo.OrderOutStockNote n  WITH(NOLOCK) WHERE n.City_Id=@City_Id AND IsDeleted=0 AND  AuditState <> 9 AND BusinessTime BETWEEN @BeginTime and @EndTime;
--退货入库单
SELECT COUNT(1) FROM dbo.ReturnOrderInStockNotes n  WITH(NOLOCK) WHERE  n.City_Id=@City_Id AND IsDeleted=0 AND AuditState <> 9 AND BusinessTime BETWEEN @BeginTime and @EndTime;
--未下推采购入库单
SELECT COUNT(1) FROM dbo.PurchaseInStockNotes  WITH(NOLOCK)  WHERE City_Id = @City_Id AND IsDeleted=0 AND AuditState = 9 AND IsDownReason =0 AND InStockTime BETWEEN @BeginTime and @EndTime AND   IsNotCreateFinance = 0;
--未下推采购退货单
SELECT COUNT(1) FROM dbo.PurchaseOutStockNotes  WITH(NOLOCK) WHERE IsDeleted=0 AND City_Id = @City_Id AND AuditState = 9 AND IsDownReason =0 AND OutStockTime BETWEEN @BeginTime and @EndTime AND  IsNotCreateFinance = 0;
--未下推销售出库单
SELECT COUNT(1) FROM dbo.OrderOutStockNote n  WITH(NOLOCK) WHERE IsDeleted=0 AND n.City_Id=@City_Id AND  AuditState = 9 AND IsDownReason =0 AND BusinessTime BETWEEN @BeginTime and @EndTime AND  IsNotCreateFinance = 0 ;
--未下推退货入库单
SELECT COUNT(1) FROM dbo.ReturnOrderInStockNotes n  WITH(NOLOCK) WHERE IsDeleted=0 AND n.City_Id=@City_Id AND AuditState = 9 AND IsDownReason =0 AND BusinessTime BETWEEN @BeginTime and @EndTime AND   IsNotCreateFinance = 0;
--库存盘点
SELECT COUNT(1) FROM dbo.BaseNotes n WITH(NOLOCK) LEFT JOIN dbo.StoreHouse sh WITH(NOLOCK)  ON n.StoreHouse_Id = sh.Id WHERE IsDeleted=0 AND sh.City_Id=@City_Id AND OperationType=4 AND AuditState <> 9 AND BusinessTime BETWEEN @BeginTime and @EndTime;
--破损出库单
SELECT COUNT(1) FROM dbo.BaseNotes n WITH(NOLOCK) LEFT JOIN dbo.StoreHouse sh WITH(NOLOCK)  ON n.StoreHouse_Id = sh.Id WHERE IsDeleted=0 AND sh.City_Id=@City_Id AND OperationType=6 AND AuditState <> 9 AND BusinessTime BETWEEN @BeginTime and @EndTime;
--其他盘点单
SELECT COUNT(1) FROM dbo.BaseNotes n WITH(NOLOCK) LEFT JOIN dbo.StoreHouse sh  WITH(NOLOCK) ON n.StoreHouse_Id = sh.Id WHERE IsDeleted=0 AND sh.City_Id=@City_Id AND OperationType IN (7,8,9)AND AuditState <> 9 AND BusinessTime BETWEEN @BeginTime and @EndTime;
--物料调拨单
SELECT COUNT(1) FROM dbo.ProductAllocations WITH(NOLOCK) WHERE City_Id = @City_Id AND IsDeleted = 0 AND AuditState <> 9 AND BusinessTime BETWEEN @BeginTime and @EndTime;
--应收单
SELECT COUNT(1) FROM dbo.ReceivableBills WITH(NOLOCK) WHERE City_Id = @City_Id AND IsDeleted = 0 AND AuditState <> 2 AND CreateTime BETWEEN @BeginTime and @EndTime;
--收款单
SELECT COUNT(1) FROM dbo.ReceiptBills WITH(NOLOCK) WHERE City_Id = @City_Id AND IsDeleted = 0 AND AuditState <> 2 AND CreateTime BETWEEN @BeginTime and @EndTime;
--预收单
SELECT COUNT(1) FROM dbo.PreReceiptBills WITH(NOLOCK) WHERE City_Id = @City_Id AND IsDeleted = 0 AND AuditState <> 2 AND CreateTime BETWEEN @BeginTime and @EndTime;
--预收退款单
SELECT COUNT(1) FROM dbo.PreReceiptRefundBills WITH(NOLOCK) WHERE City_Id = @City_Id AND IsDeleted = 0 AND AuditState <> 2 AND CreateTime BETWEEN @BeginTime and @EndTime;
--应付单
SELECT COUNT(1) FROM dbo.PayableBills WITH(NOLOCK) WHERE City_Id = @City_Id AND IsDeleted = 0 AND AuditState <> 2 AND CreateTime BETWEEN @BeginTime and @EndTime;
--付款单
SELECT COUNT(1) FROM dbo.PaymentBills WITH(NOLOCK) WHERE City_Id = @City_Id AND IsDeleted = 0 AND AuditState <> 2 AND CreateTime BETWEEN @BeginTime and @EndTime;
--预付单
SELECT COUNT(1) FROM dbo.PrepayBills WITH(NOLOCK) WHERE City_Id = @City_Id AND IsDeleted = 0 AND AuditState <> 2 AND CreateTime BETWEEN @BeginTime and @EndTime;
--预付退款单
SELECT COUNT(1) FROM dbo.PrepayRefundBills WITH(NOLOCK) WHERE City_Id = @City_Id AND IsDeleted = 0 AND AuditState <> 2 AND CreateTime BETWEEN @BeginTime and @EndTime;
--成本调整单
SELECT COUNT(1) FROM dbo.CostAdjustNotes n WITH(NOLOCK) LEFT JOIN  dbo.StoreHouse sh ON n.StoreHouse_Id = sh.Id WHERE sh.City_Id = @City_Id AND IsDeleted = 0  AND AuditState <> 9 AND CreateTime BETWEEN @BeginTime and @EndTime;	
--兑奖入库单
SELECT COUNT(1) FROM dbo.RaffleInTickets WITH(NOLOCK) WHERE City_Id=@City_Id AND AuditState=0 AND CreateTime BETWEEN @BeginTime and @EndTime;
--返利采购入库单未创建返利应收单
SELECT COUNT(1) FROM dbo.PurchaseInStockNotes WITH(NOLOCK) WHERE  City_Id= @City_Id AND IsDeleted=0 AND RebateTotalAmount>0 AND IsCreateRebateBill=0 AND CreateTime BETWEEN @BeginTime and @EndTime;
--返利采购退货单未创建返利应收单
SELECT COUNT(1) FROM dbo.PurchaseOutStockNotes WITH(NOLOCK) WHERE  City_Id= @City_Id AND IsDeleted=0 AND RebateTotalAmount>0 AND IsCreateRebateBill=0 AND CreateTime BETWEEN @BeginTime and @EndTime;
--未审核兑奖出库单
SELECT COUNT(1) FROM dbo.RaffleOutTickets WITH(NOLOCK) WHERE  City_Id= @City_Id AND Type=1 AND AuditState<>9 AND CreateTime BETWEEN @BeginTime and @EndTime;
