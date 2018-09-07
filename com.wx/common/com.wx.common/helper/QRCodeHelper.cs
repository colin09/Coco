using System;
using System.Drawing.Imaging;
using System.IO;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace com.wx.common.helper
{
    public class QRCodeHelper
    {

        public static string GetQRCode(string content)
        {
            ErrorCorrectionLevel Ecl = ErrorCorrectionLevel.M; //误差校正水平   
            string Content = content;//待编码内容  
            QuietZoneModules QuietZones = QuietZoneModules.Two;  //空白区域   
            int ModuleSize = 22;//大小  
            var encoder = new QrEncoder(Ecl);
            QrCode qr;

            MemoryStream ms = new MemoryStream();
            if (encoder.TryEncode(Content, out qr))//对内容进行编码，并保存生成的矩阵  
            {
                var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones));
                render.WriteToStream(qr.Matrix, ImageFormat.Png, ms);
            }

            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
