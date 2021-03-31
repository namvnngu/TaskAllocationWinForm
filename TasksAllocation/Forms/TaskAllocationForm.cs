using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TasksAllocation.Files;
using TasksAllocation.Forms;
using TasksAllocation.Utils.Validation;

namespace TasksAllocation
{
    public partial class TaskAllocationForm : Form
    {
        ErrorsForm errorsForm = new ErrorsForm();
        TaskAllocation taskAllocation = new TaskAllocation();
        Configuration configuration = new Configuration();
        ErrorManager errorManager = new ErrorManager();

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
                bool cffFileNameExits = taskAllocation.GetCffFilename(taffFileName, ref errorManager);

                if (cffFileNameExits)
                {
                    // Validate task allocation file
                    bool validTaskAllocation = taskAllocation.Validate(taffFileName, ref errorManager);

                    if (validTaskAllocation)
                    {
                        // Validate configuration file
                        string cffFilename = taskAllocation.CffFilename;
                        bool validaConfiguration = configuration.Validate(cffFilename);

                        if (validaConfiguration) {
                            // Display files
                        };
                    }
                }

                // Display Error
                foreach (string[] error in errorManager.Errors)
                {
                    Console.WriteLine($"Message: {error[0]}");
                    Console.WriteLine($"Error: {error[1]}");
                    Console.WriteLine($"Expected: {error[2]}");
                }

                // Display the allocations

            }
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Dispose();
        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            AboutBoxForm aboutBox = new AboutBoxForm();

            aboutBox.ShowDialog();
        }

        private void errorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            errorsForm.Show();
        }
    }
}
