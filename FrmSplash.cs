using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shortcut
{
    delegate void ProgressDelegate(int i);
    delegate void CloseDelegate();

    public partial class FrmSplash : Form
    {
        private int numOfMaxCnt = 0;

        public FrmSplash(int maxCnt = 0)
        {
            InitializeComponent();
            numOfMaxCnt = maxCnt;
        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
        }

        public void Step(int i)
        {
            progressBar.Value = i;
#if DEBUG
            lblCnt.Text = i.ToString() + " / " + numOfMaxCnt;
#else
            lblCnt.Text = i.ToString() + "%";
#endif
            lblCnt.Refresh();
        }
    }
}
