using com.wx.common.helper;
using com.wx.common.mvc;
using com.wx.domain.wx;
using com.wx.mongo.data;
using com.wx.mongo.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.wx.web.Areas.wp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly MgQuestionAnswerService _quesService;

        public HomeController()
        {
            _quesService = new MgQuestionAnswerService();
        }


        // GET: wp/Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Questionnaire()
        {
            return View();
        }


        public ActionResult QuestionnaireAnwser(MgQuestionAnswer request,string[] channel)
        {
            log.Info(request.ToJson());
            request.createTime = DateTime.Now;
            request.channel = string.Join(",", channel);
            _quesService.Insert(request);

            return View("Thank");
        }


    }
}
