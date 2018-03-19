using System;
using System.IO;
using Microsoft.Extensions.Configuration;

using com.mh.common.extension;

namespace com.mh.common.configuration
{

    public class ConfigManager
    {
        private static IConfigurationRoot Configuration = new ConfigBuilder().Configuration;

        private static Random random = new Random(DateTime.Now.Millisecond);


        #region - test -
        public static string TestJson
        {
            get
            {
                var jsonS = "";
                jsonS += $"option1 : {Configuration["option1"]} \n";
                jsonS += $"option2 : {Configuration["option2"]} \n";

                jsonS += $"subsection.suboption1 : {Configuration["subsection:suboption1"]} \n";

                jsonS += $"wizards[1].Name : {Configuration["wizards:0:Name"]} \n";
                jsonS += $"wizards[1].Age : {Configuration["wizards:0:Age"]} \n";

                jsonS += $"wizards[2].Name : {Configuration["wizards:1:Name"]} \n";
                jsonS += $"wizards[2].Age : {Configuration["wizards:1:Age"]} \n";

                return jsonS;
            }
        }
        #endregion


        private static string _resourcedomain => Configuration["resourcedomain"];
        private static string _resourcedomain2 => Configuration["_resourcedomain2"];
        private static string _resourcedomain3 => Configuration["_resourcedomain3"];
        private static string _imageUpload => Configuration["_imageUpload"];




        public static string GetHttpImagePath()
        {
            var domains = new string[] { _resourcedomain, _resourcedomain2, _resourcedomain3 };

            var index = random.Next(0, domains.Length);
            return domains[index] + _imageUpload;
        }


        /**** MongoDB *********************************************************************/
        public static string MongoDBConnectionString => Configuration["MongoDB:ConnectionString"];
        public static string MongoChatServer => Configuration["MongoDB:ChatServer"];
        public static string MongoMagichorse => Configuration["MongoDB:Magichorse"];
        public static string MongoMagicalHorseStat => Configuration["MongoDB:MagicalHorseStat:dbName"];
        public static string MongoMagicalHorseStatDataVersion => Configuration["MongoDB:MagicalHorseStat:dataVersion"];
        public static string MongoDataSource => Configuration["MongoDB:DataSource"];



        /**** MySql *********************************************************************/

        public static string HuiConnStr => Configuration["MySql:HuiConnStr"];
        public static string MySqlWriteConn => Configuration["MySql:WriteConnStr"];
        public static string MySqlReadConn => Configuration["MySql:ReadConnStr"];

        public static string MagicHorseConnStr => Configuration["MySql:MagicHorseConnStr"];


        /**** E-Mail *********************************************************************/
        public static string EMailSenderSmtp => Configuration["EMail:sender:smtp"];
        public static int EMailSenderPort => Configuration["EMail:sender:port"].ToInt32();
        public static string EMailSenderAddress => Configuration["EMail:sender:address"];
        public static string EMailSenderPwd => Configuration["EMail:sender:password"];
        public static string EMailSenderName => Configuration["EMail:sender:name"];


        /**** rabbit MQ *********************************************************************/
        public static string RabbitHost => Configuration["Rabbit:Host"];
        public static string RabbitUserName => Configuration["Rabbit:UserName"];
        public static string RabbitPassword => Configuration["Rabbit:Password"];

        public static string RabbitQuenuName => Configuration["Rabbit:QuenuName"];
        public static string RabbitDataGatherQuenuName => Configuration["Rabbit:DataGatherQuenuName"];
        public static string RabbitExecuteQuenuName => Configuration["Rabbit:ExecuteQuenuName"];
        public static string RabbitWxQuenuName => Configuration["Rabbit:WxQuenuName"];




        public static string Get(string key) => Configuration[key];
    }
}