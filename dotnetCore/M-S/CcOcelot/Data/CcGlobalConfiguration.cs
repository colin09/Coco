using System.ComponentModel.DataAnnotations;

namespace CcOcelot.Data
{
    public class CcGlobalConfiguration
    {
        [Key]
        public int Id{set;get;}

        [MaxLength(256)]
        public string GatewayName{set;get;}
        [MaxLength(256)]
        public string RequestIdKey{set;get;}
        [MaxLength(256)]
        public string BaseUrl{set;get;}
        [MaxLength(256)]
        public string DownstreamSchem{set;get;}
        [MaxLength(256)]
        public string ServiceDiscoveryProvider{set;get;}
        [MaxLength(256)]
        public string LoadBalancerOptions{set;get;}
        [MaxLength(256)]
        public string HttpHandlerOptions{set;get;}
        [MaxLength(256)]
        public string QosOptions{set;get;}


        public int IsDefault{set;get;}
        public int InfoStatus{set;get;}
    }
}