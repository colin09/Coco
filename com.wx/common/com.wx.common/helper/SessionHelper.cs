using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.common.helper
{
    public class SessionHelper
    {

        public static string Encrypt(int userId)
        {
            var data = new SessionData
            {
                UserId = userId.ToString(),
                IssueDate = DateTime.Now
            };

            return CryptoHelper.Encode(data.ToJson());
        }

        public static int Decrypt(string token)
        {
            var json = CryptoHelper.Decode(token);

            var data = json.DesJson<SessionData>();
            var userId = 0;
            if (data != null && data.Expired)
                userId = Convert.ToInt32(data.UserId);
            else if (data != null && !data.Expired)
                userId = -1;
            return userId;
        }

    }



    public class SessionData
    {
        public string UserId { get; set; }

        public DateTime IssueDate { get; set; }

        public bool Expired
        {
            get
            {
                return DateTime.Now > IssueDate.AddDays(30);
            }
        }
    }
}
