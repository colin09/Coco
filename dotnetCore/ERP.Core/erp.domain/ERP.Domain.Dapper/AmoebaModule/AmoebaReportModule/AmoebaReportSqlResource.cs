using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dapper.AmoebaModule.AmoebaReportModule
{
    public class AmoebaReportSqlResource : SqlResourceBase
    {
        public override string FileName
        {
            get { return "AmoebaReportSqlResource"; }
        }

        public string Select { get { return GetSql("Select"); } }
        public string SelectAll { get { return GetSql("SelectAll"); } }
        public string Insert { get { return GetSql("Insert"); } }
        public string Update { get { return GetSql("Update"); } }
        public string Delete { get { return GetSql("Delete"); } }


        public string SelectMonthCitys { get { return GetSql("SelectMonthCitys"); } }
        public string SelectSimpleInfo { get { return GetSql("SelectSimpleInfo"); } }
        public string SelectImportDate { get { return GetSql("SelectImportDate"); } }
        public string SelectLastMonth { get { return GetSql("SelectLastMonth"); } }
        public string SelectRanking { get { return GetSql("SelectRanking"); } }




    }
}
