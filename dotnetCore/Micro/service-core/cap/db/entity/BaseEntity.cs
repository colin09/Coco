using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cap.db.entity {

    public class BaseEntity {

        public BaseEntity () {
            Id = Guid.NewGuid ().ToString ("N");
        }

        [Key]
        [MaxLength (64)]
        public string Id { set; get; }
        public DataStatus Status { set; get; } = DataStatus.Successed;
        public DateTime CreateDate { set; get; } = DateTime.Now;
        public int CreateUser { set; get; } = 0;
        public DateTime UpdateDate { set; get; } = DateTime.Now;
        public int UpdateUser { set; get; } = 0;

    }

    public enum DataStatus {
        Deleted = 0,
        Scheduled = 1,
        Enqueued = 2,
        Processing = 3,
        Failed = 4,
        Successed = 5
    }
}