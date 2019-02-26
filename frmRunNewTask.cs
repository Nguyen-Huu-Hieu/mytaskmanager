using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyTaskManager
{
    public partial class frmRunNewTask : Form
    {
        public frmRunNewTask()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtOpen.Text))                     // xử lý nếu chuỗi không trống
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = txtOpen.Text;          // lấy tên tiến trình cần chạy
                    process.Start();                                    // chạy tiến trình
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
