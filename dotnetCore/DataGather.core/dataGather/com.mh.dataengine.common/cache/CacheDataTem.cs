

namespace com.mh.dataengine.common.cache
{

    public class SectionTem
    {
        public int SectionId { set; get; }
        public string SectionCode { set; get; }
        public int StoreId { set; get; }
        public int GroupId { set; get; }

        public string BrandCode { set; get; }

        public string Msg { set; get; }
    }


    public class BuyerTem
    {
        public int BuyerUserId { set; get; }
        public string BuyerCode { set; get; }
    }

}