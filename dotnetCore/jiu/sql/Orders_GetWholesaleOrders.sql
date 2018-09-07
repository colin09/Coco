
declare @OrderNO varchar(50)
declare	@ProductName nvarchar(200)
declare	@CityId NVARCHAR(64)--查询的组织机构ID
declare	@BeginCompleteTime datetime
declare	@EndCompleteTime datetime
declare @IsAgencySaleOrder BIT--是否招商订单
declare	@HasCreateNote BIT
declare	@PageIndex int
declare	@PageSize int
declare	@TotalCount int --OUTPUT

set	@OrderNO ='100816800094'
set	@ProductName =''
set	@CityId ='5450635494528712791'
set	@BeginCompleteTime ='2018-06-10'
set	@EndCompleteTime ='2018-07-09'
set @IsAgencySaleOrder=0
set	@HasCreateNote=1
set	@PageIndex=0
set	@PageSize=20
set	@TotalCount=0

BEGIN

    -- 除了精确查询只能查一个月以内的下单或者完成订单， 避免数据量过大
    IF(ISNULL(@OrderNO,'')='' )
	BEGIN
        IF(@BeginCompleteTime IS NULL)
			SET @BeginCompleteTime = (CONVERT(varchar(7), getdate() , 120) + '-1')

        IF(@EndCompleteTime IS NULL OR @EndCompleteTime > DATEADD(MONTH, 1, @BeginCompleteTime))
			SET @EndCompleteTime = DATEADD(MONTH, 1, DATEADD(DAY, 1, @BeginCompleteTime))
    END

    SELECT @TotalCount = 100
    declare @OrderTable table(Id varchar(100),RowNumber int)

    IF(@OrderNO IS NOT NULL AND @OrderNO <>'')
        BEGIN
             ;WITH
                OrderIds
                AS
                (
                    SELECT DISTINCT o.Id, o.LastUpdateTime 
                    FROM dbo.Orders o WITH (NOLOCK)
                    INNER JOIN dbo.OrderItem oi WITH (NOLOCK) ON oi.Order_Id = o.Id AND o.City_Id = @CityId AND (@ProductName IS NULL OR @ProductName ='' OR oi.ProductName like '%'+@ProductName +'%')
                    WHERE oi.Id IS NOT NULL AND o.OrderNo = @OrderNO
                        AND o.CompleteTime BETWEEN @BeginCompleteTime AND @EndCompleteTime
                        AND (@HasCreateNote is NULL OR @HasCreateNote =o.HasCreateNote)
                        AND (@IsAgencySaleOrder is NULL OR @IsAgencySaleOrder =o.IsAgencySaleOrder)
                ) INSERT INTO @OrderTable
            SELECT Id, ROW_NUMBER() OVER (ORDER BY o.LastUpdateTime desc) RowNumber
            FROM OrderIds o
        END
    ELSE IF(@ProductName IS NOT NULL AND @ProductName <>'')
		BEGIN
            ;WITH
                OrderIds
                AS
                (
                    SELECT DISTINCT o.Id, o.LastUpdateTime 
                    FROM dbo.Orders o WITH (NOLOCK)
                    INNER JOIN dbo.OrderItem oi WITH (NOLOCK) ON oi.Order_Id = o.Id AND o.City_Id = @CityId AND oi.ProductName like '%'+@ProductName +'%'
                    WHERE oi.Id IS NOT NULL
                        AND o.CompleteTime BETWEEN @BeginCompleteTime AND @EndCompleteTime
                        AND (@HasCreateNote is NULL OR @HasCreateNote =o.HasCreateNote)
                        AND (@IsAgencySaleOrder is NULL OR @IsAgencySaleOrder =o.IsAgencySaleOrder)
                ) INSERT INTO @OrderTable
            SELECT Id, ROW_NUMBER() OVER (ORDER BY o.LastUpdateTime desc) RowNumber
            FROM OrderIds o
        END
	ELSE
		BEGIN
            ;WITH
                OrderIds
                AS
                (
                    SELECT o.Id, o.LastUpdateTime
                    FROM dbo.Orders o WITH (NOLOCK)
                    WHERE o.City_Id = @CityId
                        AND o.CompleteTime BETWEEN @BeginCompleteTime AND @EndCompleteTime
                        AND (@HasCreateNote is NULL OR @HasCreateNote =o.HasCreateNote)
                        AND (@IsAgencySaleOrder is NULL OR @IsAgencySaleOrder =o.IsAgencySaleOrder)
                ) INSERT INTO @OrderTable
            SELECT Id, ROW_NUMBER() OVER (ORDER BY o.LastUpdateTime desc) RowNumber
            FROM OrderIds o
        END


    SELECT @TotalCount = COUNT(Id) FROM @OrderTable
    PRINT('TotalCount:') PRINT(@TotalCount)

    SELECT o.Id OrderId,
        o.OrderType,
        o.OrderNO OrderNo,
        o.ProductAmount,
        o.OrderAmount,
        o.PayableAmount,
        o.Bonus,
        o.Coupons,
        o.User_Id UserId,
        ou.TrueName BrokerUserName,
        ou.MobileNO BrokerMobileNO,
        o.CreateTime CreateTime,
        o.CompleteTime CompleteTime,
        o.StoreHouse_Id,
        o.LastUpdateTime,
        o.LastUpdateTime,
        o.City_Id CityId,
        o.HasCreateNote,
        o.IsAgencySaleOrder
    FROM @OrderTable o1
    INNER JOIN dbo.Orders o WITH (NOLOCK) ON o1.Id = o.Id
    INNER JOIN dbo.[USER] u WITH (NOLOCK) ON u.Id = o.User_Id
    LEFT JOIN dbo.OrgUser ou WITH (NOLOCK) ON ou.Id = o.BrokerUser_Id
    WHERE @PageSize <=0 OR  (RowNumber >= @PageSize * @PageIndex + 1 AND RowNumber <= @PageSize * (@PageIndex+1))
    ORDER BY o.CompleteTime DESC
END
;

