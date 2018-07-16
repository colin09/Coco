
DECLARE	@UserIds varchar(MAX)
DECLARE	@MobileNO varchar(100)
DECLARE	@State int
DECLARE	@UserName varchar(100)
DECLARE	@TrueName nvarchar(200)
DECLARE	@Province nvarchar(100)
DECLARE	@Cities nvarchar(MAX)
DECLARE	@OrgIds NVARCHAR(max)
DECLARE	@County nvarchar(50)
DECLARE	@BrokerUserId varchar(100)
DECLARE	@StartTime datetime
DECLARE	@EndTime datetime
DECLARE	@AuditStartTime datetime
DECLARE	@AuditEndTime datetime
DECLARE	@IsContainsUpdate bit
DECLARE	@LowestIntegral int
DECLARE	@MostIntegral int
DECLARE	@CompanyName nvarchar(100)
DECLARE	@EnableOrDongJie bit
DECLARE	@ExpProvience nvarchar(1000)
DECLARE	@IsSyncUser bit
DECLARE	@PageIndex int
DECLARE	@PageSize int
-- OUTPUT
DECLARE	@TotalCount int 	

SET @PageIndex=0
SET @PageSize=20

BEGIN
	SELECT @TotalCount = 0
	declare @UserTable table(Id varchar(100), RowNumber int)

	if(@UserIds IS NULL)
	BEGIN
		;WITH Cities AS(
			SELECT o.Address_City,o.Id
			FROM dbo.City c			
			INNER JOIN dbo.Orgs o ON c.Id = o.Id
			LEFT JOIN dbo.f_split(@Cities,',') c1 ON c1.col = o.Address_City
			LEFT JOIN dbo.f_split(@OrgIds,',') c2 ON c2.col=o.Id
			WHERE (@Province IS NULL OR o.Address_Province = @Province) AND (@Cities IS NULL OR c1.col IS NOT NULL) AND (@OrgIds IS NULL OR c2.col IS NOT NULL)
		),
		UserIds AS(
			SELECT u.Id, u.LastUpdateTime
			FROM dbo.[User] u WITH (NOLOCK)
            /*
			INNER JOIN dbo.[CompanyUser] cu WITH (NOLOCK) ON u.Id = cu.Id 
				AND (@BrokerUserId IS NULL OR cu.BrokerUser_Id = @BrokerUserId OR (@BrokerUserId ='' AND cu.BrokerUser_Id IS NULL))
				AND (@State IS NULL OR @State < 1 OR cu.State = @State)
				AND (@EnableOrDongJie IS NULL OR @EnableOrDongJie = 0 OR (@EnableOrDongJie = 1 AND cu.State in (3,4))) --只查询启用或冻结用户
				AND (@UserName IS NULL OR u.UserName = @UserName)
				AND (@TrueName IS NULL OR u.TrueName like '%'+@TrueName+'%')
				AND (@MobileNO IS NULL OR u.MobileNo = @MobileNO)
				AND (@StartTime IS NULL OR (u.CreateTime >= @StartTime or (@IsContainsUpdate =1 AND u.LastUpdateTime >=@StartTime)))
				AND (@EndTime IS NULL OR (u.CreateTime <= @EndTime or (@IsContainsUpdate =1 AND u.LastUpdateTime <=@EndTime)))
				AND (@AuditStartTime IS NULL OR cu.AuditTime >= @AuditStartTime)
				AND (@AuditEndTime IS NULL OR cu.AuditTime <= @AuditEndTime)
				AND (@County IS NULL OR @County ='' OR u.County = @County)
				AND (@CompanyName IS  NULL OR cu.CompanyName like '%'+@CompanyName+'%')
			LEFT JOIN Cities cc ON u.City = cc.Address_City AND cc.id=u.CityOrg_Id */
            LEFT JOIN Cities cc ON u.City_Id = cc.Id 
            LEFT JOIN Orgs o ON u.City_Id = o.Id 
			WHERE (cc.Address_City IS NOT NULL OR (@Province IS NULL AND  @Cities IS NULL)) AND(cc.Id IS NOT NULL OR @OrgIds IS NULL)
                AND (@State IS NULL OR @State < 1 OR u.State = @State)
				AND (@EnableOrDongJie IS NULL OR @EnableOrDongJie = 0 OR (@EnableOrDongJie = 1 AND u.State in (3,4))) --只查询启用或冻结用户
				AND (@UserName IS NULL OR u.UserName = @UserName)
				AND (@TrueName IS NULL OR u.TrueName like '%'+@TrueName+'%')
				AND (@MobileNO IS NULL OR u.MobileNo = @MobileNO)
				AND (@StartTime IS NULL OR (u.CreateTime >= @StartTime or (@IsContainsUpdate =1 AND u.LastUpdateTime >=@StartTime)))
				AND (@EndTime IS NULL OR (u.CreateTime <= @EndTime or (@IsContainsUpdate =1 AND u.LastUpdateTime <=@EndTime)))
				AND (@County IS NULL OR @County ='' OR o.Address_County = @County)
				AND (@CompanyName IS  NULL OR u.CompanyName like '%'+@CompanyName+'%')
		)
		INSERT INTO @UserTable 
		SELECT Id, ROW_NUMBER() OVER (ORDER BY LastUpdateTime desc) RowNumber
		FROM UserIds 

	END
	ELSE
	BEGIN
		;WITH UserIds AS(
			SELECT col Id  FROM dbo.f_split(@UserIds,',') 
		)
		INSERT INTO @UserTable 
		SELECT Id, ROW_NUMBER() OVER (ORDER BY Id ASC) RowNumber
		FROM UserIds 
	END


	SELECT @TotalCount = COUNT(Id) FROM @UserTable
    PRINT('TotalCount:') PRINT(@TotalCount) 

	SELECT u.Id, u.UserName, u.TrueName, u.MobileNO, u.CreateTime,u.City_Id,u.[State],u.CompanyName,u.TrueName,
            o.Address_Province,o.Address_City,o.Address_County,o.Address_DetailAddress
        --u.IDCard, u.NickName, u.Email, u.Gender, u.BirthDay, cu.AuditTime,cu.BrokerUser_Id,
		--u.Province, u.City, u.County, u.DetailAddress,  		  
        --cu.State,cu.CompanyName,ou.TrueName BrokerName,  u.CityOrg_Id CityId
	FROM @UserTable u1
	INNER JOIN dbo.[User] u WITH (NOLOCK) ON u1.Id = u.Id
	--INNER JOIN dbo.[CompanyUser] cu WITH (NOLOCK) ON u.Id = cu.Id
    LEFT JOIN dbo.Orgs o ON o.Id = u.City_Id
	--LEFT JOIN dbo.OrgUser ou WITH (NOLOCK) ON ou.Id = cu.BrokerUser_Id
	WHERE @PageSize <=0 OR (RowNumber >= @PageSize * @PageIndex + 1 AND RowNumber <= @PageSize * (@PageIndex+1))
	ORDER BY u.LastUpdateTime DESC
END