using System.ComponentModel.DataAnnotations;

namespace CcOcelot.Data {
    public class CcRerouteGroup {
        [Key]
        public int Id { set; get; }
        [MaxLength(256)]
        public string Name { set; get; }
        [MaxLength(256)]
        public string Detail { set; get; }
        public int ParentId { set; get; }
        public int InfoStatus { set; get; }
    }
}