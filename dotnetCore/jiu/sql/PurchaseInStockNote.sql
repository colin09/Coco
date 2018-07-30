/*
DECLARE @MyList TABLE (Value INT)
INSERT INTO @MyList VALUES (1)
INSERT INTO @MyList VALUES (2)
INSERT INTO @MyList VALUES (3)
INSERT INTO @MyList VALUES (4)

SELECT *
FROM MyTable
WHERE MyColumn IN (SELECT Value FROM @MyList)
*/

---------------------------------------------------------------------------------------------------------------------


SELECT * FROM OrgUser WHERE Id = '8263';

--City_Id
SELECT * FROM PurchaseRequisitionAuditSettings WHERE FirstAuditUserMobileNos like '%18621800271%';
SELECT * FROM PurchaseRequisitionAuditSettings WHERE SecondAuditUserMobileNos like '%18621800271%';

--UserMobile
SELECT * FROM PurchaseRequisitionAuditMobileSettings WHERE FirstAuditUserMobileNos like '%18621800271%';
SELECT * FROM PurchaseRequisitionAuditMobileSettings WHERE SecondAuditUserMobileNos like '%18621800271%';

---------------------------------------------------------------------------------------------------------------------
------/InStockAudit/AuditSearchOfExceptionNote 

DECLARE @firstAuMobiles VARCHAR(20)
DECLARE @firstAuCitys VARCHAR(20)

DECLARE @secondAuMobiles VARCHAR(20)
DECLARE @secondAuCitys VARCHAR(20)

DECLARE @Mobile VARCHAR(20)

SET @secondAuMobiles = '18621800271'
SET @Mobile = '%18621800271%'

SELECT PurchaseInStockNote_Id,dbo.AuditTrace.CreateTime,TraceState 
FROM dbo.PurchaseInStockNoteAuditTrace WITH(NOLOCK)
INNER JOIN dbo.AuditTrace WITH(NOLOCK) ON AuditTrace.Id=dbo.PurchaseInStockNoteAuditTrace.Id
INNER JOIN dbo.PurchaseInStockNotes WITH(NOLOCK) ON dbo.PurchaseInStockNotes.Id=dbo.PurchaseInStockNoteAuditTrace.PurchaseInStockNote_Id 
												AND dbo.PurchaseInStockNotes.NeedAuditUserRole= dbo.AuditTrace.UserRoleInfo_UserRole
WHERE PurchaseInStockNotes.AuditState = 5 --AND TraceState = 1
and ( 
		(UserName is not null and NeedAuditUserRole = 'FirstAuditUser' and UserRoleInfo_UserRole = 'FirstAuditUser' and (PurchaseMobileNo in (@firstAuMobiles) or City_Id in (@firstAuCitys) ) )
		or (UserName is not null and NeedAuditUserRole = 'SecondAuditUser' and UserRoleInfo_UserRole = 'SecondAuditUser' and (PurchaseMobileNo in (@secondAuMobiles) or City_Id in (@secondAuCitys)) )
		or (UserName like @Mobile)
	)
;

SELECT * FROM PurchaseInStockNotes WHERE Id in ('bb06e90e70f84f9399f3f626ef6508d3','b92cac2824084fe7aa041d0c7c84c497');



---------------------------------------------------------------------------------------------------------------------
------/PurchaseRequisition/GetPurchaseRequisitionTraces


SELECT * from PurchaseRequisitions WHERE Id = 'fbdac3f7191d47a19cf0f4aad5566a59';

SELECT * from PurchaseRequisitionAuditTrace WHERE PurchaseRequisition_Id = 'fbdac3f7191d47a19cf0f4aad5566a59';

SELECT AuditTrace.Id,SequenceNo,TraceState,UserName,MobileNo,Remark,AuditTime,CreateTime,LastUpdateTime,UserId,ImgIds,FileIds,PurchaseRequisition_Id,UserRoleInfo_Level Level,UserRoleInfo_UserRoleName UserRoleName,UserRoleInfo_UserRole UserRole
FROM dbo.PurchaseRequisitionAuditTrace WITH(NOLOCK)
INNER JOIN dbo.AuditTrace WITH(NOLOCK) ON AuditTrace.Id = PurchaseRequisitionAuditTrace.Id
WHERE PurchaseRequisition_Id='fbdac3f7191d47a19cf0f4aad5566a59'
ORDER BY CreateTime DESC
;




---------------------------------------------------------------------------------------------------------------------
------/PurchaseInStockNotes


SELECT top 100 * FROM PurchaseInStockNotes;

SELECT TOp 100 * FROM City;
SELECT TOp 100 * FROM Orgs;

select TOp 100 i.Id,pf.Desc_BrandName brandName,pf.Desc_OldCategory category,i.GoodsPosition,i.Num _num,i.Unit,
    pf.Desc_Specifications specification,i.OrdinaryStockCount _OrdinaryStockCount,i.BulkProductStockCount,pf.Desc_PackageQuantity packageQuantity,
    i.Remark,i.IsGiveaway,i.Product_Id productId,pf.Desc_ProductName productName,pis.StoreHouse_Id storeHouseId,
    s.[Name] storeHouseName,i.PurchaseRequisitionItem_Id purchaseRequisitionItemId,i.AllocationNoteItem_Id allocationNoteItemId,
    i.ProductionDate,i.ExpirationDate,I.Note_Id,i.imgIds
    from [dbo].[InStockItems] i WITH(NOLOCK)
    inner join [dbo].[PurchaseInStockNotes] pis WITH(NOLOCK) on pis.id=i.Note_Id
    inner join dbo.Product p WITH(NOLOCK) on i.Product_Id=p.Id
    inner join dbo.ProductInfo pf WITH(NOLOCK) on pf.Id=p.Info_Id
    inner join dbo.Orgs s WITH(NOLOCK) on s.id=pis.StoreHouse_Id


SELECT top 100 * FROM InStockItems where Id = '280d1b143ed74eeda5cde3dc37777290';

SELECT top 100 * FROM InStockItems where Note_Id = 'be587ceba6324bb18acd4a88b1233c3e';

--update InStockItems SET imgIds='["SSSSSSSSSSSSSSSSSSSS0001"]' where Id = '280d1b143ed74eeda5cde3dc37777290';
--update InStockItems SET imgIds='["SSSSSSSSSSSSSSSSSSSS0001"]' where Id = 'f2c2d83026fc4943b80df0cac60eca8d';

SELECT top 100 [Id],[AuditState],[InStockTime],[IsDownReason],[CreateTime],[LastUpdateTime],[City_Id],[OrgUser_Id],[Supplier_Id],[AuditRemark],[IsDeleted],[IsInternal],[SupplierCity_Id]
    ,[Remark],[Version],[IsFromAgencySaleNote],[IsFromCityAllocation],[BatchNo],[PurchaseRequisitionNo],[PurchaseName],[PurchaseMobileNo],[WhetherSmsNotify],[PurchaseRequisition_Id]
    ,[PurchaseUserId],[NoteNo],[Operators],[IsHidden],[PurchaseBusinessType],[AllocationNoteNo],[AllocationNote_Id],[CannotEditAndDelete],[IsNotSyncStock],[HasCreateRelatedNote],[NeedPurchaseAudit]
    ,[HandlingCost],[LogisticsFee],[NeedAuditUserRole],[AuditTime],[StoreHouse_Id],[VerificationDate],[RebateTotalAmount],[IsCreateRebateBill]  FROM [dbo].[PurchaseInStockNotes] WITH(NOLOCK)

SELECT top 10 [City].[Id],[City].[CityMode],[Orgs].[Name],[Orgs].[Id],[Orgs].[Enable],[Orgs].[CreateTime],[Orgs].[LastUpdateTime],[Orgs].[NewId],[Orgs].[OrgType],
    [Orgs].[Address_Province] Province,[Orgs].[Address_City] City,[Orgs].[Address_County] County,[Orgs].[Address_DetailAddress] DetailAddress,
    [Orgs].[Address_Latitude] Latitude,[Orgs].[Address_Longitude] Longitude
    FROM [dbo].[City] WITH(NOLOCK)
    INNER JOIN dbo.Orgs WITH(NOLOCK) ON Orgs.Id = City.Id



	
    SELECT [Id],[State],[AuditTime],[RequisitionNo],[TotalAmount],[PayType],[PrepayAmount],[HasCreatePrepayBill],[HasPaymentAmount],[PurchaseUserId],[PurchaseName],[PurchaseMobileNo]
    ,[ExpiredDays],[NeedAuditUserRole],[IsNewProvider],[Remark],[CheckInfoJsonString],[CreateTime],[LastUpdateTime],[City_Id],[Provider_Id],[StoreHouse_Id],[RequistitionTime],[PurchaseUserRoleName]
    ,[NotSkuGiftString],[NeedAuditUserRoleName],[RequisitionType],[SpecificationsCount],[UnitCount],[ProviderState],[BatchNo],[AuditSetting_Id],[SeparateBillType],[CCOrgUserIds],[CallGoodsState]
    ,[WhetherRebate]
    ,[VerificationDate]
    ,[RebateTotalAmount]
    ,[PurchaseReason]
    ,[PickupAmount]
    ,[UnWriteOffAmount]
    ,[WriteOffDays]
    ,[PayDays]
    FROM [dbo].[PurchaseRequisitions] WITH(NOLOCK)
   where PurchaseRequisitions.Id='34d1f6b9ae9a4a4886e9f62c3f222b31' order by CreateTime desc


SELECT top 100 * FROM InStockItems order by CreateTime desc;


SELECT top 100 AuditTrace.Id,SequenceNo,TraceState,UserName,MobileNo,Remark,AuditTime,CreateTime,LastUpdateTime,UserId,ImgIds,PurchaseRequisitionAuditTrace.Id,FileIds,PurchaseRequisition_Id
    ,UserRoleInfo_Level Level,UserRoleInfo_UserRoleName UserRoleName,UserRoleInfo_UserRole UserRole
    FROM dbo.PurchaseRequisitionAuditTrace WITH(NOLOCK)
    INNER JOIN dbo.AuditTrace WITH(NOLOCK) ON AuditTrace.Id = PurchaseRequisitionAuditTrace.Id
	ORDER BY AuditTrace.CreateTime DESC
    WHERE PurchaseRequisition_Id=@Id



DECLARE @cityId varchar(32)
DECLARE @storeHouseId varchar(32)
DECLARE @productIds varchar(32)
SELECT Product_Id, MAX(ProductionDate) AS ProductionDate FROM ProductLots WITH(NOLOCK) WHERE City_Id=@cityId AND StoreHouse_Id=@storeHouseId AND Product_Id IN (@productIds)
GROUP BY Product_Id



SELECT TOp 100 * FROM ProductLots where StoreHouse_Id = '8987';



