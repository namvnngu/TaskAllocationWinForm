using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TasksAllocation.Operations;
using TasksAllocation.Forms;

namespace TasksAllocation
{
    public partial class TaskAllocationForm : Form
    {
        public TaskAllocationForm()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string taffFileName = openFileDialog.FileName;
                TaskAllocation taskAllocation = new TaskAllocation();
                Configuration configuration = new Configuration();

                if (taskAllocation.GetCffFilename(taffFileName))
                {
                    if (taskAllocation.Validate(taffFileName) && configuration.Validate(taskAllocation.CffFilename))
                    {

                    }
                }
            }
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            AboutBoxForm aboutBox = new AboutBoxForm();

            aboutBox.ShowDialog();
        }
    }
}
