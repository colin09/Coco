using ERP.Domain.Model.OrgModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.Data.Entity;
using System.Linq;

namespace ERP.Domain.Model.AmoebaReportModule
{
    [Table("AmoebaReport")]
    public class AmoebaReport : BaseEntity, IAggregationRoot
    {
        #region  基本属性
     
        [ForeignKey("City")]
        [MaxLength(64)]
        public string CityId { set; get; }
        [MaxLength(32)]
        public string DataMonth { set; get; }


        #region  销售件数

        public decimal? SaleCount_Jiu { set; get; }
        public decimal? SaleCount_Yinliao { set; get; }
        public decimal? SaleCount_liangyou { set; get; }
        public decimal? SaleCount_Rihua { set; get; }
        public decimal? SaleCount_Qita { set; get; }
        /// <summary>
        /// 件数小计[饮料+食品粮油+家具日化+其他类]
        /// </summary>
        public decimal? SaleCount_Sum_Other { set; get; }
        /// <summary>
        /// 件数合计 [酒类 + 饮料+食品粮油+家具日化+其他类]
        /// </summary>
        public decimal? SaleCount_Sum_All { set; get; }

        #endregion

        #region  毛利总额

        public decimal? GrossMargin_Jiu { set; get; }
        public decimal? GrossMargin_Yinliao { set; get; }
        public decimal? GrossMargin_liangyou { set; get; }
        public decimal? GrossMargin_Rihua { set; get; }
        public decimal? GrossMargin_Qita { set; get; }
        /// <summary>
        /// 毛利小计[饮料+食品粮油+家具日化+其他类]
        /// </summary>
        public decimal? GrossMargin_Sum_Other { set; get; }
        /// <summary>
        /// 毛利合计 [酒类 + 饮料+食品粮油+家具日化+其他类]
        /// </summary>
        public decimal? GrossMargin_Sum_All { set; get; }

        #endregion

        #region  单件毛利

        public decimal? GrossMargin_Unit_Jiu { set; get; }
        public decimal? GrossMargin_Unit_Yinliao { set; get; }
        public decimal? GrossMargin_Unit_liangyou { set; get; }
        public decimal? GrossMargin_Unit_Rihua { set; get; }
        public decimal? GrossMargin_Unit_Qita { set; get; }
        /// <summary>
        /// 单件毛利小计 [毛利小计/件数小计]
        /// </summary>
        public decimal? GrossMargin_Unit_Avg_Other { set; get; }
        /// <summary>
        /// 单件毛利 [毛利合计/件数合计]
        /// </summary>
        public decimal? GrossMargin_Unit_Avg_All { set; get; }

        #endregion

        #region  其他收入

        public decimal? OtherRevenue_Diaohuo { set; get; }
        public decimal? OtherRevenue_Zhuanpeisong { set; get; }
        public decimal? OtherRevenue_Weijiudai { set; get; }
        public decimal? OtherRevenue_Yingyewai { set; get; }

        #endregion

        #region  固定费用

        public decimal? FixedCharge_Fangzu { set; get; }
        public decimal? FixedCharge_Cangguan { set; get; }
        public decimal? FixedCharge_Gongzi { set; get; }
        public decimal? FixedCharge_Zhejiu { set; get; }
        public decimal? FixedCharge_Bangong { set; get; }
        public decimal? FixedCharge_Sum { set; get; }

        #endregion

        #region  批发单件固定费用

        public decimal? FixedCharge_Unit_Fangzu { set; get; }
        public decimal? FixedCharge_Unit_Cangguan { set; get; }
        public decimal? FixedCharge_Unit_Gongzi { set; get; }
        public decimal? FixedCharge_Unit_Zhejiu { set; get; }
        public decimal? FixedCharge_Unit_Bangong { set; get; }
        /// <summary>
        /// 单件固定费用
        /// </summary>
        public decimal? FixedCharge_Unit_Avg { set; get; }

        #endregion

        #region  变动费用

        public decimal? VeriableCharge_Ticheng { set; get; }
        public decimal? VeriableCharge_Wuliu { set; get; }
        public decimal? VeriableCharge_Sum { set; get; }

        #endregion

        #region  批发单件变动费用

        public decimal? VeriableCharge_Unit_Ticheng { set; get; }
        public decimal? VeriableCharge_Unit_Wuliu { set; get; }
        public decimal? VeriableCharge_Unit_Avg { set; get; }

        #endregion

        /// <summary>
        /// 其他费用 - 营业外支出
        /// </summary>
        public decimal? OtherCharge { set; get; }


        /// <summary>
        /// 收入合计
        /// </summary>
        public decimal? InSum { set; get; }
        /// <summary>
        /// 费用合计
        /// </summary>
        public decimal? OutSum { set; get; }
        /// <summary>
        /// 批发分摊费用合计
        /// </summary>
        public decimal? SplitSum { set; get; }
        /// <summary>
        /// 批发净利润
        /// </summary>
        public decimal? NetProfit { set; get; }

        #region  统采

        public decimal? UniformProcure_SaleAmount { set; get; }
        public decimal? UniformProcure_GrossMargin { set; get; }
        public decimal? UniformProcure_SaleCount { set; get; }
        public decimal? UniformProcure_Uint_GrossMargin { set; get; }


        public decimal? UniformProcure_Commissions { set; get; }
        public decimal? UniformProcure_Split_Sum { set; get; }
        public decimal? UniformProcure_NetProfit { set; get; }


        #endregion

        #region  区域指标

        public decimal? Area_jinglirun { set; get; }
        public decimal? Area_Feixiaobi { set; get; }
        public decimal? Area_Feijianbi { set; get; }
        public decimal? Area_Zhankuanlixi { set; get; }
        public decimal? Area_Chuzhankuan_jinglirun { set; get; }
        public decimal? Area_Zhouzhuanlv { set; get; }
        public decimal? Area_Jingxianjinliu { set; get; }
        public decimal? Area_WaibuZhankuanlv { set; get; }

        #endregion


        #endregion


        #region 聚合属性

        public City City { get; set; }

        #endregion

        #region  方法

        public object GetValue(string propertyName)
        {
            return this.GetType().GetProperty(propertyName).GetValue(this, null);
        }
        public void SetValue(string propertyName, object value)
        {
            var property = this.GetType().GetProperty(propertyName);
            property.SetValue(this, value, null);
        }

        public TType GetValue<TType>(string propertyName, TType defaultValue)
        {
            try
            {
                var value = this.GetType().GetProperty(propertyName).GetValue(this, null);
                //if (value == null)
                //    return defaultValue;
                //else
                return (TType)value;
            }
            catch (System.Exception)
            {
                return defaultValue;
            }
        }

        #endregion

        internal static void ModelConfiguration(ModelBuilder modelBuilder)
        {
            #region   保证财务的精度
            var properties = new[]
            {
               modelBuilder.Entity<AmoebaReport>().Property(s=> s.SaleCount_Jiu),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.SaleCount_Yinliao),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.SaleCount_liangyou),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.SaleCount_Rihua),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.SaleCount_Qita),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.SaleCount_Sum_Other),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.SaleCount_Sum_All),
                                          
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Jiu),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Yinliao),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_liangyou),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Rihua),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Qita),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Sum_Other),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Sum_All),
                                         
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Unit_Jiu),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Unit_Yinliao),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Unit_liangyou),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Unit_Rihua),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Unit_Qita),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Unit_Avg_Other),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.GrossMargin_Unit_Avg_All),
                                          
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.OtherRevenue_Diaohuo),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.OtherRevenue_Zhuanpeisong),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.OtherRevenue_Weijiudai),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.OtherRevenue_Yingyewai),
                                          
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Fangzu),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Cangguan),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Gongzi),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Zhejiu),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Bangong),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Sum),
                                         
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Unit_Fangzu),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Unit_Cangguan),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Unit_Gongzi),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Unit_Zhejiu),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Unit_Bangong),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.FixedCharge_Unit_Avg),
                                    
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.VeriableCharge_Ticheng),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.VeriableCharge_Wuliu),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.VeriableCharge_Sum),
                                         
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.VeriableCharge_Unit_Ticheng),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.VeriableCharge_Unit_Wuliu),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.VeriableCharge_Unit_Avg),
                                          
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.OtherCharge),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.InSum),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.OutSum),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.SplitSum),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.NetProfit),
                                         
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.UniformProcure_SaleAmount),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.UniformProcure_GrossMargin),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.UniformProcure_SaleCount),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.UniformProcure_Uint_GrossMargin),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.UniformProcure_Commissions),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.UniformProcure_Split_Sum),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.UniformProcure_NetProfit),
                                         
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.Area_jinglirun),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.Area_Feixiaobi),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.Area_Feijianbi),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.Area_Zhankuanlixi),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.Area_Chuzhankuan_jinglirun),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.Area_Zhouzhuanlv),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.Area_Jingxianjinliu),
                 modelBuilder.Entity<AmoebaReport>().Property(s=> s.Area_WaibuZhankuanlv),
            };

            properties.ToList().ForEach(property =>
            {
                //property.HasPrecision(38, 6);
            });
            #endregion
        }
    }
}
