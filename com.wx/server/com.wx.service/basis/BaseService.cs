using com.wx.common.logger;
using com.wx.service.models;
using com.wx.sqldb;
using com.wx.sqldb.factory;
using System;

namespace com.wx.service.basis
{
    public class BaseService
    {
        protected HuiDbSession DbSession
        {
            get
            {
                return new HuiDbSession();
            }
        }

        protected ILog Log => Logger.Current();







        protected ServiceResult Error(Action<ServiceResult> callback) 
        {
            var result = new ServiceResult
            {
                IsSuccess = false,
                StateCode = StatusCode.InternalServerError,
                Message = "操作失败！"
            };

            if (callback != null)
                callback(result);
            return result ;
        }



        protected ServiceResult<T> Success<T>(Action<ServiceResult<T>> callback) where T : class
        {
            var result = new ServiceResult<T>
            {
                IsSuccess = true,
                StateCode = StatusCode.Success,
                Message = "操作成功！"
            };

            callback?.Invoke(result);
            return result;
        }



    }
}
