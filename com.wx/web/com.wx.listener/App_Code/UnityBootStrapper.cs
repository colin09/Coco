﻿using com.wx.ioc.IOCAdapter;
using com.wx.ioc.IOCUnity;

namespace com.wx.listener
{
     class UnityBootStrapper
    {
        public static void Init()
        {
            if (LocatorFacade.Current == null)
            {
                LocatorFacade.SetLocatorProvider(new UnityLocator());
            }
        }
    }
}