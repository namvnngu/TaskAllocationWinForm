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
using TasksAllocation.Utils.Display;

namespace TasksAllocation
{
    public partial class TaskAllocationForm : Form
    {
        ErrorsForm errorsForm;
        TaskAllocation taskAllocation = new TaskAllocation();
        Configuration configuration = new Configuration();
        Validations validations = new Validations();

        string errorText;

        public TaskAllocationForm()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Reset
            validations = new Validations();
            taskAllocation = new TaskAllocation();
            configuration = new Configuration();

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string taffFileName = openFileDialog.FileName;
                string cffFilename;
                bool validTaskAllocation, validaConfiguration;
                bool allValidFiles = false;

                urlTextBox.Text = taffFileName;

                // Validate task allocation file and configuration file
                validTaskAllocation = taskAllocation.ValidateFile(taffFileName, validations);
                cffFilename = taskAllocation.CffFilename;
                validaConfiguration = configuration.Validate(cffFilename, validations);

                if (validTaskAllocation && validaConfiguration)
                {
                    allValidFiles = true;
                }


                // Display Error


                // Display the allocations
                if (allValidFiles)
                {

                }
                else
                {
                    errorsForm = new ErrorsForm();
                    errorText = ErrorDisplay.DisplayText(validations.ErrorValidationManager);
                    errorsForm.errorWebBrowser.DocumentText = errorText;
                    errorsForm.Show();
                }
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

        private void ErrorsToolStripMenuItemClick(object sender, EventArgs e)
        {
            errorsForm = new ErrorsForm();
            errorsForm.errorWebBrowser.DocumentText = errorText;
            errorsForm.Show();
        }

        private void MainWebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void ValidateButtonClick(object sender, EventArgs e)
        {

        }
    }
}
