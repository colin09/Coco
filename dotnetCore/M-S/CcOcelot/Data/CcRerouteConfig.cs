using System.ComponentModel.DataAnnotations;

namespace CcOcelot.Data {
    public class CcRerouteConfig {
        [Key]
        public int Id { set; get; }

        public int RouteId { set; get; }
        public int RerouteId { set; get; }

    }
}