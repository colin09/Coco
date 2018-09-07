using System;
using System.IO;
using com.wx.api.weixin;
using com.wx.sqldb;
using com.wx.sqldb.data;
using Newtonsoft.Json;
using com.wx.common.logger;
using com.wx.message.ImgServiceReference;
using com.wx.common.config;

namespace com.wx.message.messageHandler
{
    public class StoreQrHandler : IMessageHandler
    {
        private ILog _log = Logger.Current();
        private readonly HuiDbSession DbSession = new HuiDbSession();

        public bool Action(string message)
        {
            try
            {
                var array = JsonConvert.DeserializeObject<string[]>(message);
                if (array == null)
                    return false;
                var storeId = Convert.ToInt32(array[0]);
                var ticket = AccountMg.CreateTicketFoyStore(TokenMg.Param.AccessToken, storeId);
                _log.Info("StoreQr.image.ticket:{0}", ticket);

                var bytes = AccountMg.GetQrCodeByTicket(ticket);
                _log.Info("StoreQr.image.byte:{0}", bytes.ToString());

                UploadServiceSoapClient client = new UploadServiceSoapClient();
                var filePath = "store/qrcode/";
                var fileName = Guid.NewGuid().ToString();
                var extName = ".png";
                var result = client.Up(bytes, filePath, fileName, extName, (int)SourceType.StoreQrCode);

                /*
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                var fileName = $"store\\qrcode\\{Guid.NewGuid().ToString()}";
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}\\{fileName}.png";
                image.Save(path);
                */
                var m = new ResourceEntity()
                {
                    SourceId = storeId,
                    SourceType = SourceType.StoreQrCode,
                    Domain = AppSettingConfig.ImgDomain,
                    Name = filePath + fileName,
                    ExtName = extName,
                    IsDefault = true,
                };

                DbSession.ResourceRepository.Create(m);
                DbSession.SaveChange();
                return true;

            }
            catch (Exception ex)
            {
                _log.Info($"StoreQrHandler Error ==> {ex}");
                return true;
            }

        }
    }
}
