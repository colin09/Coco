using System.ComponentModel.DataAnnotations;

namespace CcOcelot.Data {
    public class CcReroute {
        [Key]
        public int Id { set; get; }
        public int ItemId { set; get; }

        [MaxLength(256)]
        public string UpstreamPathTemplate { set; get; }
        [MaxLength(256)]
        public string UpstreamHttpMethod { set; get; }
        [MaxLength(256)]
        public string UpstreamHost { set; get; }

        [MaxLength(256)]
        public string DownstreamScheme { set; get; }
        [MaxLength(256)]
        public string DownstreamPathTemplate { set; get; }
        [MaxLength(256)]
        public string DownstreamHostAndPorts { set; get; }

        [MaxLength(256)]
        public string AuthenticationOptions { set; get; }
        [MaxLength(256)]
        public string RequestIdKey { set; get; }
        [MaxLength(256)]
        public string CacheOptions { set; get; }
        [MaxLength(256)]
        public string ServiceName { set; get; }
        [MaxLength(256)]
        public string LoadBalancerOptions { set; get; }
        [MaxLength(256)]
        public string QosOptions { set; get; }
        [MaxLength(256)]
        public string DelegatingHandlers { set; get; }
        public int Priority { set; get; }
        public int InfoStatus { set; get; }

    }
}