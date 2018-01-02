using com.wx.common.helper;
using com.wx.common.mvc;
using com.wx.mongo.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.wx.web.Areas.mgr.Controllers
{
    public class OtherController : ManagerController
    {
        private readonly MgQuestionAnswerService _quesService;

        public OtherController()
        {
            _quesService = new MgQuestionAnswerService();
        }




        // GET: mgr/Other
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QesAnswers()
        {
            log.Info($"qesanswers...");
            
            var list = _quesService.GetSimpleList();
            log.Info($"list: {list.ToJson()}");
            return View(list);
        }

        public ActionResult GetOneAnswer(string id)
        {
            log.Info($"answer id : {id}");
            var m = _quesService.GetOneById(id);

            m.channel = GetChannels(m.channel);
            log.Info($"m :  {m.ToJson()}");

            return Json(m,JsonRequestBehavior.AllowGet);
        }



        public string GetChannels(string channel)
        {
            if (string.IsNullOrWhiteSpace(channel))
                return "";
            var items = channel.Split(',');
            var channels = "";
            foreach(var item in items)
            {
                switch (item)
                {
                    case "1": channels += " / 大众点评"; break;
                    case "2": channels += " / 百度搜索"; break;
                    case "3": channels += " / 婚礼纪"; break;
                    case "4": channels += " / 亲友推荐"; break;
                    case "5": channels += " / 微信推广"; break;
                    case "6": channels += " / 其他"; break;
                };
            }
            return channels;
        }

    }
}