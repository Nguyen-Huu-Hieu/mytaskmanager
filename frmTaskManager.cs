using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;                 // thư viện để sử dụng lớp Process
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTaskManager
{
    public partial class frmTaskManager : Form
    {
        public frmTaskManager()
        {
            InitializeComponent();
            GetAllProcess();
        }
        /// <summary>
        /// Mảng để lưu lại danh sách process
        /// </summary>
        Process[] process;
        /// <summary>
        /// Hàm lấy tất cả process đang chạy 
        /// </summary>
        void GetAllProcess()
        {
            process = Process.GetProcesses();    // lấy danh sách tiến trình
            livDisplayProcess.Items.Clear();     // xóa danh sách tiến trình cũ
            foreach (var item in process)
            {
                ListViewItem _Item = new ListViewItem() { };
                _Item.Text = item.ProcessName;          // lấy tên process
                _Item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = item.Id.ToString() });       // lấy id process
                _Item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = (item.Responding == true) ? "Responding" : "Not Responding" });  //lấy trạng thái process
                _Item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = ((int)(item.PrivateMemorySize64 / Math.Pow(2, 20))).ToString() + " MB" });  //lấy bộ nhớ mà process đang sử dụng theo MB
                livDisplayProcess.Items.Add(_Item);     // thêm vào ListView
            }
            lblSoTienTrinh.Text = process.Length.ToString(); // hiển thị tổng số tiến trình đang chạy
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // nếu tổng số tiến trình thay đổi, cập nhật lại danh sách tiến trình
            if (process.Length != Process.GetProcesses().Length) 
            {
                GetAllProcess();
            }
        }

        private void btnEndTask_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                ListViewItem _item = livDisplayProcess.SelectedItems[0];            // lấy thông tin tiến trình đang được chọn tại ListView 
                foreach (var item in process)                                       // vòng lặp để tìm kiếm tiến trình đó trong mảng process
                {
                    if (item.Id.ToString().CompareTo(_item.SubItems[1].Text) == 0)  // so sánh id của từng tiến trình với id của tiến trình cần xóa
                    {
                        index = process.ToList().IndexOf(item);                     // lấy số thứ tự tiến trình cần xóa trong mảng process
                        break;
                    }
                }
                process[index].Kill();                                              // xóa tiến trình
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuRunNewTask_Click(object sender, EventArgs e)
        {
            using (frmRunNewTask frm = new frmRunNewTask())
            {
                if (frm.ShowDialog() == DialogResult.OK)            // gọi Form Run New Task, chạy xong cập nhật lại danh sách tiến trình

                {
                    GetAllProcess();                                
                }
            };
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();                                     // Thoát Form
        }
    }
}
