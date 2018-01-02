using com.wx.ioc;
using com.wx.ioc.IOCAdapter;
using com.wx.ioc.IOCUnity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.consumer
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
