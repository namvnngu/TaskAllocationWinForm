
namespace TasksAllocation.Forms
{
    partial class ErrorsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.errorWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // errorWebBrowser
            // 
            this.errorWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.errorWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.errorWebBrowser.Name = "errorWebBrowser";
            this.errorWebBrowser.Size = new System.Drawing.Size(1600, 865);
            this.errorWebBrowser.TabIndex = 0;
            // 
            // ErrorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 865);
            this.Controls.Add(this.errorWebBrowser);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "ErrorsForm";
            this.Text = "Errors";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser errorWebBrowser;
    }
}