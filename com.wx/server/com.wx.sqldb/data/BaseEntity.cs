using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    public class BaseEntity
    {
        public BaseEntity()
        {
        }

        /*
        public BaseEntity(bool ifDefault)
        {
            this.Status = 1;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUser = this.UpdateUser = 0;
        }*/

        public int Id { set; get; }
        public DataStatue Status { set; get; } = DataStatue.Normal;
        public DateTime CreateDate { set; get; } = DateTime.Now;
        public int CreateUser { set; get; } = 0;
        public DateTime UpdateDate { set; get; } = DateTime.Now;
        public int UpdateUser { set; get; } = 0;

    }


    public enum DataStatue
    {
        /// <summary>
        /// 已删除（逻辑删除）
        /// </summary>
        [Description("删除")]
        Deleted = -1,
        /// <summary>
        /// 默认状态
        /// </summary>
        [Description("默认")]
        Default = 0,
        /// <summary>
        /// 正常状态
        /// </summary>
        [Description("正常")]
        Normal = 1,
    }




}
