using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wx.service.models;

namespace com.wx.service.iservice
{
   public interface IUserService
    {


        ServiceResult Login(string mobile, string pwd);

        ServiceResult SaveOAuthUser(string userInfo);

       ServiceResult GetUserInfo(int userId);
    }
}
