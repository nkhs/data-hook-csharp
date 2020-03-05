using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotaHook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KeyHook.textBox = labelSearch;
            KeyHook.frmMain = this;
            KeyHook.start();
        }
        int i;
        private void button1_Click(object sender, EventArgs e)
        {
            CFireBase.AddLogAsync("test " + i);
            i++;
        }

        public void onMessage(string text)
        {
            //labelSearch.Text = text;
            CFireBase.AddLogAsync(text);
        }
    }
}
