using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.weixin.data
{
   public class MessageReply
    {

        /// <summary>
        /// 普通文本消息
        /// </summary>
        public static string Message_Text
        {
            get
            {
                return @"<xml>
                        <ToUserName><![CDATA[{0}]]></ToUserName>
                        <FromUserName><![CDATA[{1}]]></FromUserName>
                        <CreateTime>{2}</CreateTime>
                        <MsgType><![CDATA[text]]></MsgType>
                        <Content><![CDATA[{3}]]></Content>
                        </xml>";
            }
        }
        /// <summary>
        /// 图文消息主体
        /// </summary>
        public static string Message_News_Main
        {
            get
            {
                return @"<xml>
                        <ToUserName><![CDATA[{0}]]></ToUserName>
                        <FromUserName><![CDATA[{1}]]></FromUserName>
                        <CreateTime>{2}</CreateTime>
                        <MsgType><![CDATA[news]]></MsgType>
                        <ArticleCount>{3}</ArticleCount>
                        <Articles>
                        {4}
                        </Articles>
                        </xml> ";
            }
        }
        /// <summary>
        /// 图文消息项
        /// </summary>
        public static string Message_News_Item
        {
            get
            {
                return @"<item>
                        <Title><![CDATA[{0}]]></Title> 
                        <Description><![CDATA[{1}]]></Description>
                        <PicUrl><![CDATA[{2}]]></PicUrl>
                        <Url><![CDATA[{3}]]></Url>
                        </item>";
            }
        }


        public static string Message_Help
        {
            get
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append("您好，我是小q，请回复数字选择服务：").Append("\n\n");
                buffer.Append("1  天气预报").Append("\n");
                buffer.Append("2  公交查询").Append("\n");
                buffer.Append("3  周边搜索").Append("\n");
                buffer.Append("4  歌曲点播").Append("\n");
                buffer.Append("5  经典游戏").Append("\n");
                buffer.Append("6  美女电台").Append("\n");
                buffer.Append("7  人脸识别").Append("\n");
                buffer.Append("8  聊天唠嗑").Append("\n\n");
                buffer.Append("回复'?'显示此帮助菜单");
                return buffer.ToString();
            }
        }

    }
}
