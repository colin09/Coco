using System;
using System.Collections.Generic;

namespace ERP.Domain.Contract.AmoebaModule.AmoebaReportModule
{
    public class AmoebaTargetImproveDTO
    {
        public string CityId { set; get; }

        //public DateTime Month { set; get; } = DateTime.Now.AddMonths(1);
        public DateTime Month { get { return DateTime.Now; } }

        public List<AmoebaTargetImproveItem> Items { set; get; }

    }

    public class AmoebaTargetImproveItem
    {
        public int Id { set; get; }
        public decimal? Target { set; get; }
        public string Improvement { set; get; }
    }
}
