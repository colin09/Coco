using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

using com.mh.common.Logger;
using com.mh.common.ioc;
using com.mh.mongo.iservice;

namespace com.mh.dataengine.common.iservice
{
    public abstract class BaseDataEngine
    {
        public int GroupId { set; get; }

        public int StoreId { set; get; }

        public Dictionary<string, string> Config { set; get; }

        public Dictionary<string, string> Mapping { set; get; }


        public string EMails { set; get; }
        public double LastId { set; get; }

        protected string MailAddress
        {
            get
            {
                if (!string.IsNullOrEmpty(EMails))
                {
                    var list = EMails.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (list.Length > 0)
                        return list[0];
                }
                return "";
            }
        }
        protected List<string> MailCopyAddress
        {
            get
            {
                if (!string.IsNullOrEmpty(EMails))
                {
                    if (EMails.IndexOf(',') > 0)
                    {
                        var mails = EMails.Substring(EMails.IndexOf(',') + 1);
                        if (!string.IsNullOrEmpty(mails.Trim()))
                            return new List<string>(mails.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries));
                    }
                }
                return null;
            }
        }


        public abstract int RuleId { get; }

        public abstract string Operate { get; }


        protected static ILog log => IocProvider.GetService<ILog>();
        protected static  IPluginDataService pluginDataService => IocProvider.GetService<IPluginDataService>();


        

    }
}
