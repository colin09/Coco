using System;
using System.ComponentModel;
using com.mh.model.enums;

namespace com.mh.model.mysql
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
        public DataStatus Status { set; get; } = DataStatus.Normal;
        public DateTime CreateDate { set; get; } = DateTime.Now;
        public int CreateUser { set; get; } = 0;
        public DateTime UpdateDate { set; get; } = DateTime.Now;
        public int UpdateUser { set; get; } = 0;

    }



}