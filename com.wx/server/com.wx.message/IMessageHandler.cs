﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.message
{

   public interface IMessageHandler
    {
        bool Action(string message);

    }
}
