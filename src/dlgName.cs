using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoveSeat
{
    public partial class dlgName : Form
    {
        public string EnteredName { get { return txtServer.Text; } }


        public dlgName()
        {
            InitializeComponent();
        }

        public dlgName(string text, string caption): this()
        {
            label1.Text = text;
            Text = caption;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
