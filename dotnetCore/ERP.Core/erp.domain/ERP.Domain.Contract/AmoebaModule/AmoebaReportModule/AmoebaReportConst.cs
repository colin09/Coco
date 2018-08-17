using System;
using System.Collections.Generic;
using System.Linq;
using ERP.Domain.Enums.AmoebaModule;

namespace ERP.Domain.Contract.AmoebaModule.AmoebaReportModule
{
    public class AmoebaReportConst
    {

        /// <summary>
        /// Excel 表数据对应的列顺序
        /// </summary>
        public static string[] ColumnMapping =  {
                    "CityName",
                "InSum",

                    "SaleCount_Jiu",
                    "SaleCount_Yinliao",
                    "SaleCount_liangyou",
                    "SaleCount_Rihua",
                    "SaleCount_Qita",
                    "SaleCount_Sum_Other",
                    "SaleCount_Sum_All",

                    "GrossMargin_Jiu",
                    "GrossMargin_Yinliao",
                    "GrossMargin_liangyou",
                    "GrossMargin_Rihua",
                    "GrossMargin_Qita",
                    "GrossMargin_Sum_Other",
                    "GrossMargin_Sum_All",

                    "GrossMargin_Unit_Jiu",
                    "GrossMargin_Unit_Yinliao",
                    "GrossMargin_Unit_liangyou",
                    "GrossMargin_Unit_Rihua",
                    "GrossMargin_Unit_Qita",
                    "GrossMargin_Unit_Avg_Other",
                    "GrossMargin_Unit_Avg_All",

                    "OtherRevenue_Diaohuo",
                    "OtherRevenue_Zhuanpeisong",
                    "OtherRevenue_Weijiudai",
                    "OtherRevenue_Yingyewai",

                "OutSum",

                    "FixedCharge_Fangzu",
                    "FixedCharge_Cangguan",
                    "FixedCharge_Gongzi",
                    "FixedCharge_Zhejiu",
                    "FixedCharge_Bangong",
                    "FixedCharge_Sum",

                    "FixedCharge_Unit_Fangzu",
                    "FixedCharge_Unit_Cangguan",
                    "FixedCharge_Unit_Gongzi",
                    "FixedCharge_Unit_Zhejiu",
                    "FixedCharge_Unit_Bangong",
                    "FixedCharge_Unit_Avg",

                    "VeriableCharge_Ticheng",
                    "VeriableCharge_Wuliu",
                    "VeriableCharge_Sum",

                    "VeriableCharge_Unit_Ticheng",
                    "VeriableCharge_Unit_Wuliu",
                    "VeriableCharge_Unit_Avg",

                    "OtherCharge",

                "SplitSum",
                "NetProfit",

                    "UniformProcure_SaleAmount",
                    "UniformProcure_GrossMargin",
                    "UniformProcure_SaleCount",
                    "UniformProcure_Uint_GrossMargin",
                    "UniformProcure_Commissions",
                    "UniformProcure_Split_Sum",
                    "UniformProcure_NetProfit",

                    "Area_jinglirun",
                    "Area_Feixiaobi",
                    "Area_Feijianbi",
                    "Area_Zhankuanlixi",
                    "Area_Chuzhankuan_jinglirun",
                    "Area_Zhouzhuanlv",
                    "Area_Jingxianjinliu",
                    "Area_WaibuZhankuanlv",
                };

        /// <summary>
        /// 数据对比分组
        /// </summary>
        public static List<AmoebaRepostDataItem> AmoebaRepostComparisonGroup = new List<AmoebaRepostDataItem> {
            new AmoebaRepostDataItem{ Id=1,Name="批发入驻-销售件数"},
            new AmoebaRepostDataItem{ Id=2,Name="批发入驻-毛利"},
            new AmoebaRepostDataItem{ Id=3,Name="批发入驻-单件毛利"},
            new AmoebaRepostDataItem{ Id=4,Name="批发入驻-其他收入"},
            new AmoebaRepostDataItem{ Id=5,Name="批发入驻-费用支出"},
            new AmoebaRepostDataItem{ Id=6,Name="统采-收入"},
            new AmoebaRepostDataItem{ Id=7,Name="统采-支出"},
            new AmoebaRepostDataItem{ Id=8,Name="区域指标"},
        };

        /// <summary>
        /// 填写改进方法项分组
        /// </summary>
        public static List<AmoebaRepostDataItem> AmoebaRepostTargetGroup = new List<AmoebaRepostDataItem> {
            new AmoebaRepostDataItem{ Id=1,Name="批发入驻-销售件数"},
            //new AmoebaRepostDataItem{ Id=2,Name="批发入驻-毛利"},
            new AmoebaRepostDataItem{ Id=3,Name="批发入驻-单件毛利"},
            //new AmoebaRepostDataItem{ Id=4,Name="批发入驻-其他收入"},
            new AmoebaRepostDataItem{ Id=5,Name="批发入驻-费用支出"},
            new AmoebaRepostDataItem{ Id=6,Name="统采-收入"},
            //new AmoebaRepostDataItem{ Id=7,Name="统采-支出"},
            new AmoebaRepostDataItem{ Id=8,Name="区域指标"},
        };

        public static List<AmoebaRepostDataItem> AmoebaReportDataList = new List<AmoebaRepostDataItem>
        {
            new AmoebaRepostDataItem{Id=101,ParentId=1, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=false,Percent=true, ValuePercent=false, Unit="件数", FirstName="批发入驻-销售件数",SecondName="酒类",ThirdName="", Name="酒类",Alias="SaleCount_Jiu"},
            new AmoebaRepostDataItem{Id=102,ParentId=1, HasTarget=true, TargetType=AmoebaTargetType.Calculate,  EndNode=false,Percent=true, ValuePercent=false, Unit="件数", FirstName="批发入驻-销售件数",SecondName="非酒类",ThirdName="", Name="非酒类",Alias="SaleCount_Sum_Other"},
            new AmoebaRepostDataItem{Id=103,ParentId=1, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=true, ValuePercent=false, Unit="件数", FirstName="批发入驻-销售件数",SecondName="非酒类",ThirdName="饮料", Name="饮料",Alias="SaleCount_Yinliao"},
            new AmoebaRepostDataItem{Id=104,ParentId=1, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=true, ValuePercent=false, Unit="件数", FirstName="批发入驻-销售件数",SecondName="非酒类",ThirdName="食品粮油", Name="食品粮油",Alias="SaleCount_liangyou"},
            new AmoebaRepostDataItem{Id=105,ParentId=1, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=true, ValuePercent=false, Unit="件数", FirstName="批发入驻-销售件数",SecondName="非酒类",ThirdName="家居日化", Name="家居日化",Alias="SaleCount_Rihua"},
            new AmoebaRepostDataItem{Id=106,ParentId=1, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=true, ValuePercent=false, Unit="件数", FirstName="批发入驻-销售件数",SecondName="非酒类",ThirdName="其他类", Name="其他类",Alias="SaleCount_Qita"},
            new AmoebaRepostDataItem{Id=107,ParentId=1, HasTarget=true, TargetType=AmoebaTargetType.Calculate,  EndNode=false,Percent=true, ValuePercent=false, Unit="件数", FirstName="批发入驻-销售件数",SecondName="销售件数合计",ThirdName="", Name="销售件数合计",Alias="SaleCount_Sum_All"},
                                                                                                                                           
            new AmoebaRepostDataItem{Id=201,ParentId=2, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=false,Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-毛利",SecondName="酒类",ThirdName="", Name="酒类",Alias="GrossMargin_Jiu"},
            new AmoebaRepostDataItem{Id=202,ParentId=2, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=false,Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-毛利",SecondName="非酒类",ThirdName="", Name="非酒类",Alias="GrossMargin_Sum_Other"},
            new AmoebaRepostDataItem{Id=203,ParentId=2, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-毛利",SecondName="非酒类",ThirdName="饮料", Name="饮料",Alias="GrossMargin_Yinliao"},
            new AmoebaRepostDataItem{Id=204,ParentId=2, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-毛利",SecondName="非酒类",ThirdName="食品粮油", Name="食品粮油",Alias="GrossMargin_liangyou"},
            new AmoebaRepostDataItem{Id=205,ParentId=2, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-毛利",SecondName="非酒类",ThirdName="家居日化", Name="家居日化",Alias="GrossMargin_Rihua"},
            new AmoebaRepostDataItem{Id=206,ParentId=2, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-毛利",SecondName="非酒类",ThirdName="其他类", Name="其他类",Alias="GrossMargin_Qita"},
            new AmoebaRepostDataItem{Id=207,ParentId=2, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=false,Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-毛利",SecondName="毛利总额",ThirdName="", Name="毛利总额",Alias="GrossMargin_Sum_All"},
                                                                                                                                            
            new AmoebaRepostDataItem{Id=301,ParentId=3, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=false,Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-单件毛利",SecondName="酒类",ThirdName="", Name="酒类",Alias="GrossMargin_Unit_Jiu"},
            new AmoebaRepostDataItem{Id=302,ParentId=3, HasTarget=true, TargetType=AmoebaTargetType.Calculate,  EndNode=false,Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-单件毛利",SecondName="非酒类",ThirdName="", Name="非酒类",Alias="GrossMargin_Unit_Avg_Other"},
            new AmoebaRepostDataItem{Id=303,ParentId=3, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-单件毛利",SecondName="非酒类",ThirdName="饮料", Name="饮料",Alias="GrossMargin_Unit_Yinliao"},
            new AmoebaRepostDataItem{Id=304,ParentId=3, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-单件毛利",SecondName="非酒类",ThirdName="食品粮油", Name="食品粮油",Alias="GrossMargin_Unit_liangyou"},
            new AmoebaRepostDataItem{Id=305,ParentId=3, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-单件毛利",SecondName="非酒类",ThirdName="家居日化", Name="家居日化",Alias="GrossMargin_Unit_Rihua"},
            new AmoebaRepostDataItem{Id=306,ParentId=3, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-单件毛利",SecondName="非酒类",ThirdName="其他类", Name="其他类",Alias="GrossMargin_Unit_Qita"},
            new AmoebaRepostDataItem{Id=307,ParentId=3, HasTarget=true, TargetType=AmoebaTargetType.Calculate,  EndNode=false,Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-单件毛利",SecondName="平均单件毛利",ThirdName="", Name="平均单件毛利",Alias="GrossMargin_Unit_Avg_All"},
                                                                                                                                            
            new AmoebaRepostDataItem{Id=402,ParentId=4, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-其他收入",SecondName="区域间调货",ThirdName="", Name="区域间调货",Alias="OtherRevenue_Diaohuo"},
            new AmoebaRepostDataItem{Id=403,ParentId=4, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-其他收入",SecondName="转配送收入",ThirdName="", Name="转配送收入",Alias="OtherRevenue_Zhuanpeisong"},
            new AmoebaRepostDataItem{Id=404,ParentId=4, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-其他收入",SecondName="微酒贷收入",ThirdName="", Name="微酒贷收入",Alias="OtherRevenue_Weijiudai"},
            new AmoebaRepostDataItem{Id=405,ParentId=4, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-其他收入",SecondName="营业外收入",ThirdName="", Name="营业外收入",Alias="OtherRevenue_Yingyewai"},
                                                                                                                                          
            new AmoebaRepostDataItem{Id=406,ParentId=4, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=false,Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-其他收入",SecondName="收入合计",ThirdName="", Name="收入合计",Alias="InSum"},
                                                                                                                                          
            new AmoebaRepostDataItem{Id=501,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=false,Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="费用支出合计",ThirdName="", Name="费用支出合计",Alias="OutSum"},
                                                                                                                                           
            new AmoebaRepostDataItem{Id=502,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=false,Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="固定费用合计",ThirdName="", Name="固定费用合计",Alias="FixedCharge_Sum"},
            new AmoebaRepostDataItem{Id=503,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="固定费用",ThirdName="房租及水电物业", Name="房租及水电物业",Alias="FixedCharge_Fangzu"},
            new AmoebaRepostDataItem{Id=504,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="固定费用",ThirdName="仓管+搬运工薪酬", Name="仓管+搬运工薪酬",Alias="FixedCharge_Cangguan"},
            new AmoebaRepostDataItem{Id=505,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="固定费用",ThirdName="其他人员固定工资", Name="其他人员固定工资",Alias="FixedCharge_Gongzi"},
            new AmoebaRepostDataItem{Id=506,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="固定费用",ThirdName="折旧费用", Name="折旧费用",Alias="FixedCharge_Zhejiu"},
            new AmoebaRepostDataItem{Id=507,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="固定费用",ThirdName="办公费用", Name="办公费用",Alias="FixedCharge_Bangong"},
                                                                                                                                                               
            new AmoebaRepostDataItem{Id=508,ParentId=5, HasTarget=true, TargetType=AmoebaTargetType.None,       EndNode=false,Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="批发单件固定费用",ThirdName="", Name="批发单件固定费用",Alias="FixedCharge_Unit_Avg"},
            new AmoebaRepostDataItem{Id=509,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="批发单件固定费用",ThirdName="房租及水电物业", Name="房租及水电物业",Alias="FixedCharge_Unit_Fangzu"},
            new AmoebaRepostDataItem{Id=510,ParentId=5, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="批发单件固定费用",ThirdName="仓管+搬运工薪酬", Name="仓管+搬运工薪酬",Alias="FixedCharge_Unit_Cangguan"},
            new AmoebaRepostDataItem{Id=511,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="批发单件固定费用",ThirdName="其他人员固定工资", Name="其他人员固定工资",Alias="FixedCharge_Unit_Gongzi"},
            new AmoebaRepostDataItem{Id=512,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="批发单件固定费用",ThirdName="折旧费用", Name="折旧费用",Alias="FixedCharge_Unit_Zhejiu"},
            new AmoebaRepostDataItem{Id=513,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="批发单件固定费用",ThirdName="办公费用", Name="办公费用",Alias="FixedCharge_Unit_Bangong"},
                                                                                                                                                                   
            new AmoebaRepostDataItem{Id=514,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=false,Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="变动费用小计",ThirdName="", Name="变动费用小计",Alias="VeriableCharge_Sum"},
            new AmoebaRepostDataItem{Id=515,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="变动费用",ThirdName="销售提成", Name="销售提成",Alias="VeriableCharge_Ticheng"},
            new AmoebaRepostDataItem{Id=516,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="变动费用",ThirdName="物流费用", Name="物流费用",Alias="VeriableCharge_Wuliu"},
                                                                                                                                                                   
            new AmoebaRepostDataItem{Id=517,ParentId=5, HasTarget=true, TargetType=AmoebaTargetType.None,       EndNode=false,Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="批发单件变动费用",ThirdName="", Name="批发单件变动费用",Alias="VeriableCharge_Unit_Avg"},
            new AmoebaRepostDataItem{Id=518,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="批发单件变动费用",ThirdName="销售提成", Name="销售提成",Alias="VeriableCharge_Unit_Ticheng"},
            new AmoebaRepostDataItem{Id=519,ParentId=5, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=false,ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="批发单件变动费用",ThirdName="物流费用", Name="物流费用",Alias="VeriableCharge_Unit_Wuliu"},
                                                                                                                                                                       
            new AmoebaRepostDataItem{Id=520,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=false,Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="其他费用-营业外支出",ThirdName="", Name="其他费用-营业外支出",Alias="OtherCharge"},
                                                                                                                                           
            new AmoebaRepostDataItem{Id=521,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=false,Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="批发分摊费用合计",ThirdName="", Name="批发分摊费用合计",Alias="SplitSum"},
            new AmoebaRepostDataItem{Id=522,ParentId=5, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=false,Percent=true, ValuePercent=false, Unit="费用", FirstName="批发入驻-费用支出",SecondName="批发净利润",ThirdName="", Name="批发净利润",Alias="NetProfit"},
                                                                                                                                            
            new AmoebaRepostDataItem{Id=601,ParentId=6, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="统采-收入",SecondName="销售额",ThirdName="", Name="销售额",Alias="UniformProcure_SaleAmount"},
            new AmoebaRepostDataItem{Id=602,ParentId=6, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="统采-收入",SecondName="毛利总额",ThirdName="", Name="毛利总额",Alias="UniformProcure_GrossMargin"},
            new AmoebaRepostDataItem{Id=603,ParentId=6, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="统采-收入",SecondName="销售件数",ThirdName="", Name="销售件数",Alias="UniformProcure_SaleCount"},
            new AmoebaRepostDataItem{Id=604,ParentId=6, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="统采-收入",SecondName="单件毛利",ThirdName="", Name="单件毛利",Alias="UniformProcure_Uint_GrossMargin"},
                                                                                                                                          
            new AmoebaRepostDataItem{Id=705,ParentId=7, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="统采-支出",SecondName="统采销售提成",ThirdName="", Name="统采销售提成",Alias="UniformProcure_Commissions"},
            new AmoebaRepostDataItem{Id=706,ParentId=7, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="统采-支出",SecondName="统采分摊费用合计",ThirdName="", Name="统采分摊费用合计",Alias="UniformProcure_Split_Sum"},
            new AmoebaRepostDataItem{Id=707,ParentId=7, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="统采-支出",SecondName="统采净利润",ThirdName="", Name="统采净利润",Alias="UniformProcure_NetProfit"},
                                                                                                                                           
            new AmoebaRepostDataItem{Id=801,ParentId=8, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="区域指标",SecondName="区域净利润",ThirdName="", Name="区域净利润",Alias="Area_jinglirun"},
            new AmoebaRepostDataItem{Id=802,ParentId=8, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="区域指标",SecondName="占款利息",ThirdName="", Name="占款利息",Alias="Area_Zhankuanlixi"},
            new AmoebaRepostDataItem{Id=803,ParentId=8, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="区域指标",SecondName="扣除占款利息净利润",ThirdName="", Name="扣除占款利息净利润",Alias="Area_Chuzhankuan_jinglirun"},
            new AmoebaRepostDataItem{Id=804,ParentId=8, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=true, ValuePercent=false, Unit="数据", FirstName="区域指标",SecondName="费销比（批发销售）",ThirdName="", Name="费销比（批发销售）",Alias="Area_Feixiaobi"},
            new AmoebaRepostDataItem{Id=805,ParentId=8, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=true, ValuePercent=false, Unit="数据", FirstName="区域指标",SecondName="费件比（批发销售）",ThirdName="", Name="费件比（批发销售）",Alias="Area_Feijianbi"},
            new AmoebaRepostDataItem{Id=806,ParentId=8, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=true, ValuePercent=false, Unit="数据", FirstName="区域指标",SecondName="资金周转率",ThirdName="", Name="资金周转率",Alias="Area_Zhouzhuanlv"},
            new AmoebaRepostDataItem{Id=807,ParentId=8, HasTarget=false,TargetType=AmoebaTargetType.None,       EndNode=true, Percent=true, ValuePercent=false, Unit="费用", FirstName="区域指标",SecondName="经营活动净现金流",ThirdName="", Name="经营活动净现金流",Alias="Area_Jingxianjinliu"},
            new AmoebaRepostDataItem{Id=808,ParentId=8, HasTarget=true, TargetType=AmoebaTargetType.Edit,       EndNode=true, Percent=true, ValuePercent=true,  Unit="数据", FirstName="区域指标",SecondName="外部占款率",ThirdName="", Name="外部占款率",Alias="Area_WaibuZhankuanlv"},

        };


    }
    /* 
    public enum AmoebaTargetType
    {
        None = 1,
        Calculate,
        Edit,
    }*/



    public class AmoebaRepostDataItem
    {
        public int Id { set; get; }
        public int ParentId { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public bool EndNode { set; get; }
        /// <summary>
        /// 涨幅为百分比
        /// </summary>
        public bool Percent { set; get; }

        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string Unit { set; get; }

        /// <summary>
        /// 数据值百分比
        /// </summary>
        public bool ValuePercent { set; get; }

        public bool HasTarget { set; get; }
        //public bool Calculate { set; get; }
        public AmoebaTargetType TargetType { set; get; }
    }
}
