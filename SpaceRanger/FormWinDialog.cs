using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceRanger
{
    public partial class FormWinDialog : Form
    {
        public FormWinDialog(string time)
        {
            InitializeComponent();
            labelTime.Text += " " + time;
        }

        public string getName()
        {
            return textBoxName.Text;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            //this.Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            //this.Dispose();
        }

        private void FormWinDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
