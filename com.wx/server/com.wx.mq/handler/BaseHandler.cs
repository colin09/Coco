using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.mq.handler
{
    interface BaseHandler
    {


        bool Action(string message);

    }
}
