--销售利润表

DECLARE	@Category varchar(100)--产品类目
DECLARE	@SaleMode INT --产品类型   99：统采产品  自营 = 1,合作 = 2,寄售 = 3,大商转自营 = 4,大商转配送 = 5,代营 = 6,入驻 = 7,总部寄售 = 8,
DECLARE	@BeginOrderTime datetime--统计开始时间
DECLARE	@EndOrderTime datetime--统计结束时间
DECLARE	@CityId NVARCHAR(64)--查询的城市ID
DECLARE	@SaleCityId NVARCHAR(64)--销售城市ID
DECLARE	@ProductName VARCHAR(100)--某个物料的毛利
DECLARE	@IsInternal BIT  -- 是否内部往来
DECLARE	@PageIndex int
DECLARE	@PageSize int
-- OUTPUT
DECLARE	@TotalCount int 
DECLARE	@TotalDiscountAmount DECIMAL(38,6) 
DECLARE	@TotalPayableAmount DECIMAL(38,6) 
DECLARE	@TotalCostAmount DECIMAL(38,6) 
DECLARE	@TotalAdditionalProfit DECIMAL(38,6) 

SET	@Category='饮料'
SET @SaleMode=6
SET @CityId='5083889334477646579'
SET @SaleCityId='5083889334477646579'
SET @PageIndex=0
SET @PageSize=20

BEGIN

--默认统计一个月的数据
    IF(@BeginOrderTime IS NULL) SET @BeginOrderTime = DATEADD(MONTH,-1,GETDATE())
    IF(@EndOrderTime IS NULL) SET @EndOrderTime = GETDATE()

SELECT @TotalCount = (
        (SELECT 
            COUNT(1)
        FROM dbo.NoteItems ni WITH (NOLOCK)
            INNER JOIN dbo.OrderOutStockNote o  WITH (NOLOCK) ON ni.OrderOutStockNote_Id = o.Id AND o.City_Id=@CityId AND o.AuditState=9 AND (@SaleCityId IS NULL OR @SaleCityId=o.SaleCity_Id)  --统计已核算单据
                AND o.IsDeleted =0 AND o.IsHidden = 0 AND (@BeginOrderTime IS NULL OR o.BusinessTime>=@BeginOrderTime) AND (@EndOrderTime IS NULL OR o.BusinessTime<=@EndOrderTime) 
                AND (@IsInternal IS NULL OR o.IsInternal = @IsInternal) AND (@SaleMode IS NULL OR (@SaleMode = 99 AND (ni.SaleMode IN (1,3,9))) OR (@SaleMode=ni.SaleMode))
            INNER JOIN dbo.Product p  WITH (NOLOCK) ON ni.Product_Id = p.Id
            INNER JOIN dbo.ProductInfo [pi]  WITH (NOLOCK) ON [pi].Id = p.Info_Id 
                AND (@ProductName IS NULL OR [pi].Desc_ProductName LIKE '%'+@ProductName+'%') AND (@Category IS NULL OR pi.Desc_OldCategory LIKE '%'+@Category+'%')
        )
	+
        (SELECT 
            COUNT(1)
        FROM dbo.ReturnNoteItems ni WITH (NOLOCK)
            INNER JOIN dbo.ReturnOrderInStockNotes o WITH (NOLOCK)  ON ni.ReturnOrderInStockNote_Id = o.Id AND o.City_Id=@CityId AND (@SaleCityId IS NULL OR @SaleCityId=o.SaleCity_Id)
                AND (@BeginOrderTime IS NULL OR o.BusinessTime>=@BeginOrderTime) AND (@EndOrderTime IS NULL OR o.BusinessTime<=@EndOrderTime) AND o.AuditState=9   --统计已核算单据
                AND o.IsDeleted =0 AND (@SaleMode IS NULL OR (@SaleMode = 99 AND (ni.SaleMode IN (1,3,9))) OR (@SaleMode=ni.SaleMode)) AND (@IsInternal IS NULL OR o.IsInternal = @IsInternal)
            INNER JOIN dbo.Product p WITH (NOLOCK)  ON ni.Product_Id = p.Id
            INNER JOIN dbo.ProductInfo [pi] WITH (NOLOCK)  ON [pi].Id = p.Info_Id AND (@ProductName IS NULL OR [pi].Desc_ProductName LIKE '%'+@ProductName+'%') AND (@Category IS NULL OR pi.Desc_OldCategory LIKE '%'+@Category+'%')
        )
    )
PRINT('TotalCount:') PRINT(@TotalCount)

		
	SELECT 
		@TotalDiscountAmount =ISNULL(SUM(ISNULL(t.DiscountAmount,0)),0),
	    @TotalPayableAmount = ISNULL(SUM(ISNULL(t.AdValoremAmount,0)),0),
		@TotalCostAmount = ISNULL(SUM(ISNULL(t.costAmount,0)),0),
		@TotalAdditionalProfit = ISNULL(SUM(ISNULL(t.AdditionalProfit,0)),0)
	FROM 
		(SELECT 
		    ni.DiscountAmount,ni.AdValoremAmount,ni.CostPrice*ni.Num costAmount,ni.AdditionalProfit
        FROM dbo.NoteItems ni WITH (NOLOCK)
            INNER JOIN dbo.OrderOutStockNote o  WITH (NOLOCK) ON ni.OrderOutStockNote_Id = o.Id AND o.City_Id=@CityId AND o.AuditState=9 AND (@SaleCityId IS NULL OR @SaleCityId=o.SaleCity_Id)   --统计已核算单据
            AND o.IsDeleted =0 AND o.IsHidden = 0 AND (@BeginOrderTime IS NULL OR o.BusinessTime>=@BeginOrderTime)
            AND (@EndOrderTime IS NULL OR o.BusinessTime<=@EndOrderTime) AND (@IsInternal IS NULL OR o.IsInternal = @IsInternal) AND (@SaleMode IS NULL OR (@SaleMode = 99 AND (ni.SaleMode IN (1,3,9))) OR (@SaleMode=ni.SaleMode))
            INNER JOIN dbo.Product p  WITH (NOLOCK) ON ni.Product_Id = p.Id
            INNER JOIN dbo.ProductInfo [pi]  WITH (NOLOCK) ON [pi].Id = p.Info_Id 
            AND (@ProductName IS NULL OR [pi].Desc_ProductName LIKE '%'+@ProductName+'%')
            AND (@Category IS NULL OR pi.Desc_OldCategory LIKE '%'+@Category+'%')
		
        UNION ALL 
        
        SELECT 
            0-ni.DiscountAmount,0-ni.AdValoremAmount,0-ni.CostPrice*ni.Num costAmount,0-ni.AdditionalProfit
        FROM dbo.ReturnNoteItems ni WITH (NOLOCK)
            INNER JOIN dbo.ReturnOrderInStockNotes o WITH (NOLOCK)  ON ni.ReturnOrderInStockNote_Id = o.Id AND o.City_Id=@CityId AND (@SaleCityId IS NULL OR @SaleCityId=o.SaleCity_Id)
            AND (@BeginOrderTime IS NULL OR o.BusinessTime>=@BeginOrderTime)
            AND (@EndOrderTime IS NULL OR o.BusinessTime<=@EndOrderTime)
            AND o.AuditState=9   --统计已核算单据
            AND o.IsDeleted =0 AND (@SaleMode IS NULL OR (@SaleMode = 99 AND (ni.SaleMode IN (1,3,9))) OR (@SaleMode=ni.SaleMode))
            AND (@IsInternal IS NULL OR o.IsInternal = @IsInternal)
            INNER JOIN dbo.Product p WITH (NOLOCK)  ON ni.Product_Id = p.Id
            INNER JOIN dbo.ProductInfo [pi] WITH (NOLOCK)  ON [pi].Id = p.Info_Id AND (@ProductName IS NULL OR [pi].Desc_ProductName LIKE '%'+@ProductName+'%')
            AND (@Category IS NULL OR pi.Desc_OldCategory LIKE '%'+@Category+'%')
        ) t


	SELECT t.Id,t.NoteId,t.NoteNo,t.City,t.SaleCity,t.BusinessTime,t.CompanyUserName,t.ProductName,t.Category,
		t.SubCategory,t.Desc_PackageQuantity,t.Unit,t.Desc_PackageUnit,t.Desc_Specifications,t.Count,
		t.DiscountAmount,t.PayableAmount,t.CostAmount,t.AdditionalProfit,UseRewardBonusAmount 
    FROM (
        SELECT 
            t.Id,t.NoteId,t.NoteNo,t.City,t.SaleCity,t.BusinessTime,t.CompanyUserName,t.ProductName,t.Category,
            t.SubCategory,t.Desc_PackageQuantity,t.Unit,t.Desc_PackageUnit,t.Desc_Specifications,t.Count,
            t.DiscountAmount,t.PayableAmount,t.CostAmount,t.AdditionalProfit,UseRewardBonusAmount,
            ROW_NUMBER() OVER (ORDER BY  BusinessTime DESC,Id) RowNumber
        FROM (
            SELECT 
                ni.Id,
                o.Id NoteId,
                o.NoteNo,
                c.Name City,
                sc.Name SaleCity,
                o.BusinessTime,
                (CASE o.IsInternal WHEN 1 THEN org.Name ELSE ISNULL(u.CompanyName,'')+'-'+u.MobileNO END) AS CompanyUserName,
                (CASE WHEN p.ProductName IS NULL OR p.ProductName = '' THEN	[pi].Desc_ProductName 
                ELSE p.ProductName END) AS ProductName,
                pi.Desc_OldCategory AS Category,
                pi.Desc_SubCategory AS SubCategory,
                pi.Desc_PackageQuantity,
                ni.Unit,
                pi.Desc_PackageUnit,
                pi.Desc_Specifications,
                ni.Num AS Count,
                ni.DiscountAmount,
                ni.AdValoremAmount	AS PayableAmount, 
                ni.CostPrice*ni.Num AS CostAmount,
                ni.AdditionalProfit,
                ni.UseRewardBonusAmount AS UseRewardBonusAmount
            FROM dbo.NoteItems ni WITH (NOLOCK)
                INNER JOIN dbo.OrderOutStockNote o  WITH (NOLOCK) ON ni.OrderOutStockNote_Id = o.Id AND o.City_Id=@CityId AND (@SaleCityId IS NULL OR @SaleCityId=o.SaleCity_Id)
                    AND (@BeginOrderTime IS NULL OR o.BusinessTime>=@BeginOrderTime)
                    AND (@EndOrderTime IS NULL OR o.BusinessTime<=@EndOrderTime)
                    and o.AuditState=9   --统计已核算单据
                    AND o.IsDeleted =0 AND o.IsHidden = 0
                    AND (@SaleMode IS NULL OR (@SaleMode = 99 AND (ni.SaleMode IN (1,3,9))) OR (@SaleMode=ni.SaleMode))
                    AND (@IsInternal IS NULL OR o.IsInternal = @IsInternal)
                INNER JOIN dbo.Orgs c ON o.City_Id=c.Id
                LEFT JOIN dbo.Orgs sc WITH(NOLOCK) ON o.SaleCity_Id=sc.Id
                INNER JOIN dbo.Product p  WITH (NOLOCK) ON ni.Product_Id = p.Id
                INNER JOIN dbo.ProductInfo [pi]  WITH (NOLOCK) ON [pi].Id = p.Info_Id
                    AND (@ProductName IS NULL OR [pi].Desc_ProductName LIKE '%'+@ProductName+'%')
                    AND (@Category IS NULL OR pi.Desc_OldCategory LIKE '%'+@Category+'%')
                LEFT JOIN dbo.Orgs org WITH (NOLOCK) ON o.CompanyUserCity_Id=org.Id
                --LEFT JOIN dbo.CompanyUser cu WITH (NOLOCK) ON o.User_Id=cu.Id
                LEFT JOIN dbo.[User] u WITH (NOLOCK) ON u.Id = o.User_Id
                
            UNION ALL 
            
            SELECT 
                ni.Id,
                o.Id NoteId,
                o.NoteNo,
                c.Name City,
                sc.Name SaleCity,
                o.BusinessTime,
                (CASE o.IsInternal WHEN 1 THEN org.Name ELSE ISNULL(u.CompanyName,'')+'-'+u.MobileNO END) AS CompanyUserName,
                (CASE WHEN p.ProductName IS NULL OR p.ProductName = '' THEN	[pi].Desc_ProductName 
                ELSE p.ProductName END) AS ProductName,
                pi.Desc_OldCategory AS Category,
                pi.Desc_SubCategory AS SubCategory,
                pi.Desc_PackageQuantity,
                ni.Unit,
                pi.Desc_PackageUnit,
                pi.Desc_Specifications,
                0-ni.Num AS Count,
                0-ni.DiscountAmount,
                0-ni.AdValoremAmount	AS PayableAmount, 
                0-ni.CostPrice*ni.Num AS CostAmount,
                0-ni.AdditionalProfit AS AdditionalProfit,
                0 AS UseRewardBonusAmount
            FROM dbo.ReturnNoteItems ni WITH (NOLOCK)
                INNER JOIN dbo.ReturnOrderInStockNotes o WITH (NOLOCK)  ON ni.ReturnOrderInStockNote_Id = o.Id AND o.City_Id=@CityId AND (@SaleCityId IS NULL OR @SaleCityId=o.SaleCity_Id)
                    AND (@BeginOrderTime IS NULL OR o.BusinessTime>=@BeginOrderTime) AND (@EndOrderTime IS NULL OR o.BusinessTime<=@EndOrderTime)
                    AND o.AuditState=9   --统计已核算单据
                    AND o.IsDeleted =0 AND (@SaleMode IS NULL OR (@SaleMode = 99 AND (ni.SaleMode IN (1,3,9))) OR (@SaleMode=ni.SaleMode))
                    AND (@IsInternal IS NULL OR o.IsInternal = @IsInternal)
                INNER JOIN dbo.Orgs c WITH(NOLOCK) ON o.City_Id=c.Id
                LEFT JOIN dbo.Orgs sc WITH(NOLOCK) ON o.SaleCity_Id=sc.Id
                INNER JOIN dbo.Product p WITH (NOLOCK)  ON ni.Product_Id = p.Id
                INNER JOIN dbo.ProductInfo [pi] WITH (NOLOCK)  ON [pi].Id = p.Info_Id
                    AND  (@ProductName IS NULL OR [pi].Desc_ProductName LIKE '%'+@ProductName+'%')
                    AND (@Category IS NULL OR pi.Desc_OldCategory LIKE '%'+@Category+'%')
                LEFT JOIN dbo.Orgs org WITH (NOLOCK) ON o.CompanyUserCity_Id=org.Id
                --LEFT JOIN dbo.CompanyUser cu WITH (NOLOCK) ON o.User_Id=cu.Id
                LEFT JOIN dbo.[User] u WITH (NOLOCK) ON u.Id = o.User_Id
            ) t
        ) t
	WHERE @PageSize <=0 OR (RowNumber >= @PageSize * @PageIndex + 1 AND RowNumber <= @PageSize * (@PageIndex+1))
END