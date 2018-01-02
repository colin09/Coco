using System;
using System.Configuration;
using com.wx.redis;
using com.wx.service.service;
using com.wx.sqldb;
using com.wx.sqldb.data;
using com.wx.weixin.api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wx.api.weixin;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            UnityBootStrapper.Init();
        }


        [TestMethod]
        public void UpdateWxMenu()
        {
            var menu = new Api_Menu();
            menu.CreateMenu();
        }



        [TestMethod]
        public void TestRedis()
        {

            //var redisConfig = RedisConfig.GetConfig();

            RedisHelper.Set("key1", "00000000000000", DateTime.Now.AddHours(1));

            Console.WriteLine(RedisHelper.getValueString("key1"));
        }



        [TestMethod]
        public void TestMysql()
        {
            var db = new HuiDbSession();

            db.StoreRepository.Create(new StoreEntity()
            {
                Name = "store============",
                Address = "address....",
                EMail = "",
                Logo = "",

            });
        }

        [TestMethod]
        public void Store()
        {
            var service = new StoreService();
            service.Modify(new StoreEntity()
            {
                Name = "store001",
                Address = "address ---"
            }, true);
        }


        [TestMethod]
        public void StoreTicket()
        {
            AccountMg.CreateTicketFoyStore(TokenMg.Param.AccessToken, 1);
        }



        }
}
