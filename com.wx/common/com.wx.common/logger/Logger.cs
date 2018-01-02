using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.common.logger
{
   public class Logger
   {
       private static readonly ILog log;

       static Logger()
       {
           log=new FileLog();
       }

       public static ILog Current()
       {
           return log;
       }

   }
}
