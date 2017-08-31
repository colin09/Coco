using com.wx.common.helper;
using com.wx.service.basis;
using com.wx.service.iservice;
using com.wx.sqldb.data;
using System.Linq;
using com.wx.service.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace com.wx.service.service
{
    public class UserService : BaseService, IUserService
    {


        public ServiceResult Login(string mobile, string pwd)
        {
            var crptyPwd = CryptoHelper.MD5Encrypt(pwd);
            var query =
                DbSession.UserRepository.Where(
                    u => u.Status == DataStatue.Normal && u.Mobile == mobile && u.Password == crptyPwd);
            if (!query.Any())
                return Error(e => e.Message = "用户名或密码错误！");

            var user = query.ToList().Select(q => new
            {
                id = q.Id,
                name = q.Name,
                nickName = q.NickName,
                logo = q.Logo,
                openId = "",
                mobile = q.Mobile,
                userLevel = q.UserLever
            }).FirstOrDefault();
            if (user == null)
                return this.Error(e => e.Message = "用户名或密码错误！!");
            Log.Info(user.ToJson());
            return Success<dynamic>(s => s.Data = user);
        }


        public ServiceResult SaveOAuthUser(string userInfo)
        {
            if (userInfo.IsEmpty())
                return Error(e => e.Message = "用户信息不能为空！");

            JObject obj = (JObject)JsonConvert.DeserializeObject(userInfo);
            if (obj != null && obj["openid"] != null)
            {
                var openid = obj["openid"].ToString();
                var nickname = obj["nickname"].ToString();
                var sex = (int)obj["sex"];
                var headimgurl = obj["headimgurl"].ToString();

                var country = obj["country"].ToString();
                var province = obj["province"].ToString();
                var city = obj["city"].ToString();


                var user = new UserEntity();
                var outUser = DbSession.OutsiteUserRepository.Where(o => o.OpenId == openid && o.Status == DataStatue.Normal)
                        .FirstOrDefault();
                if (outUser != null && outUser.UserId > 0)
                {
                    //是否更新本地 user ？
                    user = DbSession.UserRepository.ReadOne(outUser.UserId);
                }
                else
                {
                    user = new UserEntity()
                    {
                        UserLever = UserLevel.Customer,
                        Name = nickname,
                        NickName = nickname,
                        Password = CryptoHelper.MD5Encrypt("ziyoufeng.tw"),
                        Logo = headimgurl,
                        Gender = sex,
                        Country = country,
                        Province = province,
                        City = city
                    };
                    DbSession.UserRepository.Create(user);

                    if (outUser != null)
                    {
                        outUser.UserId = user.Id;
                        DbSession.OutsiteUserRepository.Update(outUser);
                    }
                    else
                        DbSession.OutsiteUserRepository.Create(new OutsiteUserEntity()
                        {
                            UserId = user.Id,
                            OpenId = openid,
                            OutSiteType = OutsiteType.Weixin,
                            AccountNO = "0",
                            IsOauthed = 1,
                        });

                    DbSession.SaveChange();
                }
                var response = new
                {
                    id = user.Id,
                    name = user.Name,
                    nickName = user.NickName,
                    logo = user.Logo,
                    openId = openid,
                    mobile = user.Mobile,
                    userLevel = user.UserLever
                };

                return Success<dynamic>(s => s.Data = response); ;
            }
            return Error(e => e.Message = "用户信息解析失败！");
        }


        public ServiceResult GetUserInfo(int userId)
        {
            var user = DbSession.UserRepository.ReadOne(userId);
            if (user == null)
                return Error(e => e.Message = "没有找到用户信息。");

            return Success<UserEntity>(s => s.Data = user);
        }

    }
}
