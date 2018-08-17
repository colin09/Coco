


namespace ERP.Domain.Contract.AmoebaModule.AmoebaReportModule
{


    public class AmoebaReportSimpleVO
    {

        public string Id { set; get; }
        public string CityId { set; get; }
        public string CityName { set; get; }
        public string DataMonth { set; get; }


        #region  区域指标

        public decimal Area_jinglirun { set; get; }
        public decimal Area_Feixiaobi { set; get; }
        public decimal Area_Feijianbi { set; get; }
        public decimal Area_Zhankuanlixi { set; get; }
        public decimal Area_Chuzhankuan_jinglirun { set; get; }
        public decimal Area_Zhouzhuanlv { set; get; }
        public decimal Area_Jingxianjinliu { set; get; }
        public decimal Area_WaibuZhankuanlv { set; get; }

        #endregion

    }

    public class AmoebaReportVO : AmoebaReportSimpleVO
    {
        /*
        public string Id { set; get; }
        public string CityId { set; get; }
        public string CityName { set; get; }
        public string DataMonth { set; get; }
        */


        #region  销售件数

        public decimal SaleCount_Jiu { set; get; }
        public decimal SaleCount_Yinliao { set; get; }
        public decimal SaleCount_liangyou { set; get; }
        public decimal SaleCount_Rihua { set; get; }
        public decimal SaleCount_Qita { set; get; }
        /// <summary>
        /// 件数小计[饮料+食品粮油+家具日化+其他类]
        /// </summary>
        public decimal SaleCount_Sum_Other { set; get; }
        /// <summary>
        /// 件数合计 [酒类 + 饮料+食品粮油+家具日化+其他类]
        /// </summary>
        public decimal SaleCount_Sum_All { set; get; }

        #endregion

        #region  毛利总额

        public decimal GrossMargin_Jiu { set; get; }
        public decimal GrossMargin_Yinliao { set; get; }
        public decimal GrossMargin_liangyou { set; get; }
        public decimal GrossMargin_Rihua { set; get; }
        public decimal GrossMargin_Qita { set; get; }
        /// <summary>
        /// 毛利小计[饮料+食品粮油+家具日化+其他类]
        /// </summary>
        public decimal GrossMargin_Sum_Other { set; get; }
        /// <summary>
        /// 毛利合计 [酒类 + 饮料+食品粮油+家具日化+其他类]
        /// </summary>
        public decimal GrossMargin_Sum_All { set; get; }

        #endregion

        #region  单件毛利

        public decimal GrossMargin_Unit_Jiu { set; get; }
        public decimal GrossMargin_Unit_Yinliao { set; get; }
        public decimal GrossMargin_Unit_liangyou { set; get; }
        public decimal GrossMargin_Unit_Rihua { set; get; }
        public decimal GrossMargin_Unit_Qita { set; get; }
        /// <summary>
        /// 单件毛利小计 [毛利小计/件数小计]
        /// </summary>
        public decimal GrossMargin_Unit_Avg_Other { set; get; }
        /// <summary>
        /// 单件毛利 [毛利合计/件数合计]
        /// </summary>
        public decimal GrossMargin_Unit_Avg_All { set; get; }

        #endregion

        #region  其他收入

        public decimal OtherRevenue_Diaohuo { set; get; }
        public decimal OtherRevenue_Zhuanpeisong { set; get; }
        public decimal OtherRevenue_Weijiudai { set; get; }
        public decimal OtherRevenue_Yingyewai { set; get; }

        #endregion

        #region  固定费用

        public decimal FixedCharge_Fangzu { set; get; }
        public decimal FixedCharge_Cangguan { set; get; }
        public decimal FixedCharge_Gongzi { set; get; }
        public decimal FixedCharge_Zhejiu { set; get; }
        public decimal FixedCharge_Bangong { set; get; }
        public decimal FixedCharge_Sum { set; get; }

        #endregion

        #region  批发单件固定费用

        public decimal FixedCharge_Unit_Fangzu { set; get; }
        public decimal FixedCharge_Unit_Cangguan { set; get; }
        public decimal FixedCharge_Unit_Gongzi { set; get; }
        public decimal FixedCharge_Unit_Zhejiu { set; get; }
        public decimal FixedCharge_Unit_Bangong { set; get; }
        /// <summary>
        /// 单件固定费用
        /// </summary>
        public decimal UnitFixedCharge_Unit_Avg { set; get; }

        #endregion

        #region  变动费用

        public decimal VeriableCharge_Ticheng { set; get; }
        public decimal VeriableCharge_Wuliu { set; get; }
        public decimal VeriableCharge_Sum { set; get; }

        #endregion

        #region  批发单件变动费用

        public decimal VeriableCharge_Unit_Ticheng { set; get; }
        public decimal VeriableCharge_Unit_Wuliu { set; get; }
        public decimal VeriableCharge_Unit_Avg { set; get; }

        #endregion

        /// <summary>
        /// 其他费用 - 营业外支出
        /// </summary>
        public decimal OtherCharge { set; get; }


        /// <summary>
        /// 收入合计
        /// </summary>
        public decimal InSum { set; get; }
        /// <summary>
        /// 费用合计
        /// </summary>
        public decimal OutSum { set; get; }
        /// <summary>
        /// 批发分摊费用合计
        /// </summary>
        public decimal SplitSum { set; get; }
        /// <summary>
        /// 批发净利润
        /// </summary>
        public decimal NetProfit { set; get; }

        #region  统采

        public decimal UniformProcure_SaleAmount { set; get; }
        public decimal UniformProcure_GrossMargin { set; get; }
        public decimal UniformProcure_SaleCount { set; get; }
        public decimal UniformProcure_Uint_GrossMargin { set; get; }


        public decimal UniformProcure_Commissions { set; get; }
        public decimal UniformProcure_Split_Sum { set; get; }
        public decimal UniformProcure_NetProfit { set; get; }


        #endregion





    }
}
