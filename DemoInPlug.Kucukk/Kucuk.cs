using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoInPlug.SDKLib;

namespace DemoInPlug.Kucukk
{
    public class Kucuk : IPlugIn
    {
        public string action(string text)
        {
            return text.ToLower();
        }

        public string name()
        {
            return "Kucuk Harf";
        }
    }
}
