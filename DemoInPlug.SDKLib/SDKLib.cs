using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoInPlug.SDKLib
{
    public interface IPlugIn
    {
        string action(string text);
        string name();
    }

    public class Plug
    {
        public string pName;
        public string pPath;
        public string pFName;
    }

    public class Kit
    {
        public static List<Plug> getAllPlugIns(string path)
        {
            List<Plug> myPlugs = new List<Plug>();
            if (!Directory.Exists(path))
            {
                return myPlugs;
            }
            string[] dlls = Directory.GetFiles(path, "*.dll");

            foreach (string dll in dlls)
            {
                Assembly asm = Assembly.LoadFile(dll);
                Type[] tipler = asm.GetTypes();
                foreach (Type tip in tipler)
                {
                    if (tip.GetInterface("IPlugIn") != null)
                    {
                        Plug p = new Plug();
                        p.pFName = tip.FullName;
                        p.pPath = dll;
                        object obj = asm.CreateInstance(tip.FullName);
                        p.pName = obj.GetType().InvokeMember("name", BindingFlags.InvokeMethod, null, obj, null).ToString();
                        myPlugs.Add(p);
                    }
                }
            }
            return myPlugs;
        }
         
        public static object createObject(Plug p)
        {
            Assembly asm = Assembly.LoadFile(p.pPath);
            object obj = asm.CreateInstance(p.pFName);
            return obj;
        }
    }
}
