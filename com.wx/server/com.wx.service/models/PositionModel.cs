using com.wx.sqldb.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.service.models
{
    public class PositionModel
    {
        public int Id { set; get; }
        public int ParentId { set; get; }
        public string Name { set; get; }
        public string EnName { set; get; }

        public string Type { set; get; }
        public int Deep { set; get; }
        public int Sort { set; get; }
    }



    public class PositionItemModel
    {
        public int Id { set; get; }
        public int Code { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public string EnName { set; get; }
        public string Domain { set; get; }
        public string Image { set; get; }
        public string Desc { set; get; }
        public int RelationId { set; get; }
        public RelationType RelationType { set; get; }
        /// <summary>
        /// 首页、主页、详细页
        /// </summary>
        public PageType Type { set; get; }
        public int Sort { set; get; }
    }

}
