using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoInPlug.SDKLib;
using System.Reflection;

namespace DemoInPlug.UIPlug
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Plug> myList = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            myList = Kit.getAllPlugIns(Application.StartupPath + "//Plugs");
            foreach (Plug p in myList)
            {
                Button btn = new Button();
                btn.Text = p.pName;
                btn.Click += btn_Click;
                flowLayoutPanel1.Controls.Add(btn);
            }
            List<string> str = new List<string>();
            foreach (Plug p in myList)
            {
                Assembly asm = Assembly.LoadFile(p.pPath);
                Type attrib1 = typeof(AssemblyDescriptionAttribute);
                Type attrib2 = typeof(AssemblyProductAttribute);

                object[] objattr1 = asm.GetCustomAttributes(attrib1, false);
                object[] objattr2 = asm.GetCustomAttributes(attrib2, false);
                if (objattr1.Length > 0)
                {
                    AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)objattr1[0];
                    str.Add(p.pName + " : Description : " + desc.Description);
                }
                if (objattr2.Length > 0)
                {
                    AssemblyProductAttribute prod = (AssemblyProductAttribute)objattr2[0];
                    str.Add(p.pName + " : Product : " + prod.Product);
                }
            }
            str.ToList().ForEach(x =>
            {
                listBox1.Items.Add(x.ToString());
            });
        }

        void btn_Click(object sender, EventArgs e)
        {
            foreach (Plug p in myList)
            {
                if (p.pName == (sender as Button).Text)
                {
                    run(p);
                }
            }
        }
        void run(Plug p)
        {
            IPlugIn obj = (IPlugIn)Kit.createObject(p);
            textBox1.Text = obj.action(textBox1.Text);

        }

    }
}
