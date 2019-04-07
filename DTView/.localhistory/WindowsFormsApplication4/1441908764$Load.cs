using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextGen
{
    public partial class Load : Form
    {
        public Load()
        {
            InitializeComponent();
            Show();
        }

        public void setStatus(String update, int prog)
        {
            Status.Text = update;
            progressBar1.Value = prog;
            Application.DoEvents();
        }
    }
}
