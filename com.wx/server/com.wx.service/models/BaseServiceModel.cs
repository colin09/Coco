using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.service.models
{


    [DataContract(Name = "result")]
    public class ServiceResult
    {


        [DataMember(Name = "isSuccessful", Order = 1)]
        public bool IsSuccess { set; get; }

        [DataMember(Name = "statusCode", Order = 2)]
        public StatusCode StateCode { set; get; }

        [DataMember(Name = "message", Order = 3)]
        public string Message { set; get; }

    }

    [DataContract(Name = "result")]
    public class ServiceResult<T>: ServiceResult
    {
        public ServiceResult() : this(default(T))
        {
        }

        public ServiceResult(T data)
        {
            Data = data;
        }

        [DataMember(Name = "data")]
        public T Data { set; get; }
    }






    public enum StatusCode
    {
        UnKnow = 0,
        Success = 200,
        ClientError = 400,
        Unauthorized = 401,
        NotFound = 404,
        RequestTimeout = 408,
        BlacklistUser = 409,
        PriceBigger = 410,
        InternalServerError = 500,
        ServiceUnavailable = 503
    }


}
