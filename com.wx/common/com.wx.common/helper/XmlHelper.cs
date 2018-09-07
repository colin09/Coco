using com.wx.common.config;
using com.wx.common.logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace com.wx.common.helper
{
    public class XmlHelper
    {
        private static string xmlPath = $"{AppSettingConfig.WxDataPath}\\wxData.xml";

        private static ILog log = Logger.Current();

        public static string[] ReadCache()
        {
            if (!File.Exists(xmlPath))
                CreateFile();

            var doc = XElement.Load(xmlPath);
            var list = from ele in doc.Elements("data")
                       where ele.Attribute("state").Value == "1"
                       select ele;
            if (list.Count() > 0)
            {
                var item = list.First();
                var expires = item.Attribute("expires").Value;
                if (Convert.ToDateTime(expires) > DateTime.Now)
                {
                    item.SetAttributeValue("state", 0);
                    doc.Save(xmlPath);
                    return null;
                }

                var token = item.Attribute("accessToken").Value;
                var ticket = item.Attribute("jsapiTicket").Value;
                return new string[] { token, ticket };
            }
            return null;
        }
        public static bool WriteCache(string token, string ticket, DateTime expires)
        {
            if (!File.Exists(xmlPath))
                CreateFile();

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
           var root = doc.SelectSingleNode("Redis-Server");

            var item =doc.CreateElement("data");
            item.SetAttribute("accessToken", token);
            item.SetAttribute("jsapiTicket", ticket);
            item.SetAttribute("expires", expires.ToString());
            item.SetAttribute("state", "0");

            root.AppendChild(item);
            doc.Save(xmlPath);
            return true;

        }



        private static void CreateFile()
        {
            var xmldoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            var xmldecl = xmldoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmldoc.AppendChild(xmldecl);
            //加入一个根元素
            var xmlelem = xmldoc.CreateElement("", "Redis-Server", "");
            xmldoc.AppendChild(xmlelem);
            xmldoc.Save(xmlPath);
            log.Info($"save xml to {xmlPath}");
        }

















        public void CreateDat()
        {
            var xmldoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            //XmlDeclaration xmldecl;
            var xmldecl = xmldoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmldoc.AppendChild(xmldecl);

            //加入一个根元素
            var xmlelem = xmldoc.CreateElement("", "Employees", "");
            xmldoc.AppendChild(xmlelem);
            //加入另外一个元素
            for (int i = 1; i < 3; i++)
            {

                XmlNode root = xmldoc.SelectSingleNode("Employees");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("Node");//创建一个<Node>节点 
                xe1.SetAttribute("genre", "DouCube");//设置该节点genre属性 
                xe1.SetAttribute("ISBN", "2-3631-4");//设置该节点ISBN属性 

                XmlElement xesub1 = xmldoc.CreateElement("title");
                xesub1.InnerText = "CS从入门到精通";//设置文本节点 
                xe1.AppendChild(xesub1);//添加到<Node>节点中 
                XmlElement xesub2 = xmldoc.CreateElement("author");
                xesub2.InnerText = "候捷";
                xe1.AppendChild(xesub2);
                XmlElement xesub3 = xmldoc.CreateElement("price");
                xesub3.InnerText = "58.3";
                xe1.AppendChild(xesub3);

                root.AppendChild(xe1);//添加到<Employees>节点中 
            }
            //保存创建好的XML文档
            xmldoc.Save($"{AppDomain.CurrentDomain.BaseDirectory}\\wxData.xml");
        }
    }
}
