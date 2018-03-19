using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using com.mh.model.enums;


namespace com.mh.model.mysql.entity
{
    /// <summary>
    /// 员工特权优惠券
    /// </summary>
    public class Fifteen_Mins_Privilege_CouponEntity : BaseEntity
    {
        //public int Id { get; set; }

        /// <summary>
        /// 集团Id
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 标记是哪个店发送的这张券
        /// </summary>
        public int SendSectionId { get; set; }

        /// <summary>
        /// 主表id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 券号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 券验证码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 业务范围
        /// </summary>
        public string ScopeOfBusiness { get; set; }
        /// <summary>
        /// 券类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 优惠类型
        /// </summary>
        public string Class_s { get; set; }
        /// <summary>
        /// 枚举 优惠类型
        /// </summary>
        public int Class { get; set; }
        /// <summary>
        /// 券定义编号
        /// </summary>
        public string DefineNum { get; set; }
        /// <summary>
        /// 券名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 仅限绑定会员使用
        /// </summary>
        public bool IsBindMember { get; set; }

        /// <summary>
        /// 满足条件值
        /// </summary>
        public decimal FullAmount { get; set; }
        /// <summary>
        /// 面额 100 or 8.5 分别代表金额和折扣。折扣减为：1-8.5/10  
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 折扣 % 无用  
        /// </summary>
        public decimal Discount2 { get; set; }
        /// <summary>
        /// 优惠比率
        /// </summary>
        public decimal DiscountRate
        {
            get
            {
                var rate = 0m;
                if (Class == (int)CouponClass.FullDiscount)
                {
                    rate = Discount2;
                }
                else if (Class == (int)CouponClass.FullMinus)
                {
                    rate = FullAmount == 0m ? 1m : Math.Round(Discount / FullAmount, 2, MidpointRounding.AwayFromZero);
                }
                return rate;
            }
            set { }
        }
        /// <summary>
        /// 折扣最大额度
        /// </summary>
        public decimal MaxDiscountAmount { get; set; }
        /// <summary>
        /// 抵扣类型
        /// </summary>
        public string DiscountType { get; set; }
        /// <summary>
        /// 发行机构
        /// </summary>

        public string PublishOrg { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlementMethod { get; set; }
        /// <summary>
        /// 状态
        /// </summary>

        public string InnerStatus { get; set; }
        /// <summary>
        /// 已收款登记 是、否       
        /// </summary>
        public bool IsUsed { get; set; }
        /// <summary>
        /// 生效起始日 凌晨   
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 生效截止日 晚上  
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 使用说明
        /// </summary>
        public string Instructions { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string AuditPerson { get; set; }
        ///// <summary>
        ///// 审核日期 
        ///// </summary>
        //public DateTime AuditDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string CouponUpdatePerson { get; set; }

        /// <summary>
        /// 修改日期    
        /// </summary>
        public DateTime? CouponUpdateDate { get; set; }

        /// <summary>
        /// 券创建时间     
        /// </summary>
        public DateTime? CouponCreateDate { get; set; }

        /// <summary>
        /// 状态 -1 删除 0 禁用 1 可用 2 已发送 3 已核销。            
        /// </summary>
        public FifteenMinCouponType CouponStatus { get; set; }

        /// <summary>
        /// 数据导入时间（包括导入覆盖。）
        /// </summary>
        //public DateTime CreateDate { get; set; }

        /// <summary>
        /// 优惠券持有人。
        /// </summary>
        public int BuyerUserId { get; set; }


        #region Overrides of BaseEntity

        /// <summary>
        /// KeyMemberId
        /// </summary>
        // public override object EntityId
        // {
        //     get { return Id; }

        // }


        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 用户编号 --作废
        /// </summary>
        public int UserId { get; set; } = 0;

        /// <summary>
        /// 会员编号
        /// </summary>
        public string MemberId { get; set; }

        /// <summary>
        /// 核销时间
        /// </summary>
        public DateTime? VerifyTime { get; set; }

        #region 活动
        /// <summary>
        /// 活动id
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 子模块编号
        /// </summary>
        public string SubModuleId { get; set; }

        /// <summary>
        /// 子模块名称
        /// </summary>
        public string SubModuleName { get; set; }

        /// <summary>
        /// 执行策略Id
        /// </summary>
        public string StrategyActionId { get; set; }

        /// <summary>
        /// 策略名称
        /// </summary>
        public string StrategyActionName { get; set; }
        #endregion

        #endregion


          public static void BuildMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fifteen_Mins_Privilege_CouponEntity>().ToTable("15Mins_Privilege_Coupon");
            modelBuilder.Entity<Fifteen_Mins_Privilege_CouponEntity>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.CouponStatus).HasColumnName("Status").IsRequired();
                entity.HasKey(e => e.Id);
                entity.Ignore("CreateUser");
                //entity.Ignore("CreateDate");
                entity.Ignore("UpdateUser");
                entity.Ignore("UpdateDate");
            });
        }
    }
}
