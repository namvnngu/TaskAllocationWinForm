using System;
using System.Windows.Forms;
using TasksAllocation.Files;
using TasksAllocation.Forms;
using TasksAllocation.Utils.Display;
using TasksAllocation.Utils.Validation;

namespace TasksAllocation
{
    public partial class TaskAllocationForm : Form
    {
        ErrorsForm ErrorsDisplayForm;
        TaskAllocation TaskAllocationController = new TaskAllocation();
        Configuration ConfigurationController = new Configuration();
        Validations ValdationsController = new Validations();
        string RenderedMainDisplayText = "", RenderedErrorText;
        string TaffFilename, CffFilename;
        bool ValidTaskAllocation, ValidConfiguration;

        public TaskAllocationForm()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            int errorCount;

            // Reset
            ValdationsController = new Validations();
            TaskAllocationController = new TaskAllocation();
            ConfigurationController = new Configuration();

            errorsToolStripMenuItem.Enabled = false;
            allocationToolStripMenuItem.Enabled = false;
            validateButton.Enabled = false;
            RenderedMainDisplayText = "";

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                TaffFilename = openFileDialog.FileName;
                urlTextBox.Text = TaffFilename;

                // Validate task allocation file and configuration file
                ValidTaskAllocation = TaskAllocationController.ValidateFile(TaffFilename, ValdationsController);
                CffFilename = TaskAllocationController.CffFilename;
                ValidConfiguration = ConfigurationController.ValidateFile(CffFilename, ValdationsController);
                TaskAllocationController.CalculateAllocationValues(ConfigurationController);
                errorCount = ValdationsController.ErrorValidationManager.Errors.Count;

                if (ValidTaskAllocation && ValidConfiguration)
                { 
                    allocationToolStripMenuItem.Enabled = true;
                    validateButton.Enabled = true;
                } else
                {
                    TaskAllocationController.AllocationDisplays = AllocationsDisplay.DisplayInvalidAllocations(TaskAllocationController.AllocationDisplays, TaskAllocationController.AllocationInFileCount);
                }

                if (errorCount != 0)
                {
                    RenderedErrorText = ErrorDisplay.DisplayText(ValdationsController.ErrorValidationManager);
                    errorsToolStripMenuItem.Enabled = true;
                }

                // Display a summary of validations and a set of allocations
                RenderedMainDisplayText += ValidationSummaryDisplay.ValidAllocationFile(TaffFilename, ValidTaskAllocation);
                RenderedMainDisplayText += ValidationSummaryDisplay.ValidConfigurationFile(CffFilename, ValidConfiguration);
                RenderedMainDisplayText += AllocationsDisplay.Display(TaskAllocationController.AllocationDisplays, ConfigurationController);

                mainWebBrowser.DocumentText = RenderedMainDisplayText;
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
            ErrorsDisplayForm = new ErrorsForm();
            ErrorsDisplayForm.errorWebBrowser.DocumentText = RenderedErrorText;
            ErrorsDisplayForm.Show();
        }

        private void MainWebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void ValidateButtonClick(object sender, EventArgs e)
        {
            ValidateAllocation();
        }

        private void AllocationToolStripMenuItemClick(object sender, EventArgs e)
        {
            ValidateAllocation();
        }

        private void ValidateAllocation()
        {
            int errorCount;

            TaskAllocationController.ValidateAllocations(ConfigurationController, ValdationsController);
            errorCount = ValdationsController.ErrorValidationManager.Errors.Count;

            if (errorCount != 0)
            {
                RenderedErrorText = ErrorDisplay.DisplayText(ValdationsController.ErrorValidationManager);
                errorsToolStripMenuItem.Enabled = true;
            }

            allocationToolStripMenuItem.Enabled = false;
            validateButton.Enabled = false;

            // Display a summary of validations and a set of allocations
            RenderedMainDisplayText = "";
            RenderedMainDisplayText += ValidationSummaryDisplay.ValidAllocationFile(TaffFilename, ValidTaskAllocation);
            RenderedMainDisplayText += ValidationSummaryDisplay.ValidConfigurationFile(CffFilename, ValidConfiguration);
            RenderedMainDisplayText += ValidationSummaryDisplay.ValidAllocations(errorCount);
            RenderedMainDisplayText += AllocationsDisplay.Display(TaskAllocationController.AllocationDisplays, ConfigurationController);

            mainWebBrowser.DocumentText = RenderedMainDisplayText;
        }
    }
}
