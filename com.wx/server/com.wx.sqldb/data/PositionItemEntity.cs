using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    public class PositionItemEntity : BaseEntity
    {

        public int Code { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public string EnName { set; get; }
        public string Image { set; get; }
        public string Desc { set; get; }
        /// <summary>
        /// 首页、主页、详细页
        /// </summary>
        public PageType Type { set; get; }
        public int Sort { set; get; }

        public int RelationId { set; get; }
        public RelationType RelationType { set; get; }
    }

    public enum PageType
    {
        Default = 0,
        Index = 1,
        Main,
        Detail
    }


    public enum RelationType
    {
        Image = 1,
        TextTop,
        TextBottom,
    }

}
