using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoInPlug.SDKLib;

namespace DemoInPlug.Buyuk
{
    public class Buyuk : IPlugIn
    {
        public string action(string text)
        {
            return text.ToUpper();
        }

        public string name()
        {
            return "Buyuk Harf";
        }
    }
}
