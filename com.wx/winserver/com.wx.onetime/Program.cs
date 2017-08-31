using CLAP;
using com.wx.onetime.commond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.onetime
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = Console.ReadLine();

            while (true)
            {
                args = cmd.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Parser.RunConsole<OT_Command>(args);

                cmd = Console.ReadLine();
            }
        }
    }
}
