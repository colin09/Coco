using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Models
{
    public class Pager
    {
        public Pager()
        {
            this.TotalCount = 0;
            this.PageSize = 20;
            this.PageIndex = 1;
            this.IsGetTotalCount = true;
        }

        public virtual int TotalCount { get; set; }
        public virtual int PageCount
        {
            get { if (this.PageSize >= 0) { return (this.TotalCount - 1) / this.PageSize + 1; } return 1; }
        }
        public virtual int PageSize { get; set; }
        public virtual int PageIndex { get; set; }
        public bool IsGetTotalCount { get; set; }

        public override string ToString()
        {
            string format = "<PagerCondition PageSize=\"{0}\" PageIndex=\"{1}\" IsGetTotalCount=\"{2}\" TotalCount=\"{3}\" PageCount=\"{4}\" />";
            return string.Format(format, new object[]
			{
				this.PageSize,
				this.PageIndex,
				this.IsGetTotalCount,
				this.TotalCount,
				this.PageCount
			});
        }
    }
}
