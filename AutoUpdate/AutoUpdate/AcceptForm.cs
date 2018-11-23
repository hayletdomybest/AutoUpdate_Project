using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoUpdate
{
    public partial class AcceptForm : Form
    {
        internal AcceptForm(UpdateInfo server)
        {
            InitializeComponent();
            lab_title_lastest.Text = string.Format
                (lab_title_lastest.Text, server._Version.ToString());           
        }


        private void btn_update_Yes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
        private void btn_update_No_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }


    }
}
