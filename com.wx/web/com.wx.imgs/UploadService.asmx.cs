using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace com.wx.imgs
{
    /// <summary>
    /// UploadService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class UploadService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }



        //上传文件至WebService所在服务器的方法，这里为了操作方法，文件都保存在UpDownFile服务所在文件夹下的File目录中
        [WebMethod]
        public bool Up(byte[] data,string filePath, string filename, string extName, int type)
        {
            try
            {
                if (!Directory.Exists(Server.MapPath(filePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(filePath));
                }
                FileStream fs = File.Create(Server.MapPath(filePath) + filename + extName);
                fs.Write(data, 0, data.Length);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
