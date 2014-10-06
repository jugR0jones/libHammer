namespace Codoxide.Common.Windows.Controls.Wizards
{
	partial class WizardPage
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.wizardPageTitlePanel = new System.Windows.Forms.Panel();
			this.wizardPageDescControl = new System.Windows.Forms.Label();
			this.wizardPageTitleControl = new System.Windows.Forms.Label();
			this.wizardPage3DSeperator = new System.Windows.Forms.Label();
			this.wizardPageTitlePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// wizardPageTitlePanel
			// 
			this.wizardPageTitlePanel.BackColor = System.Drawing.SystemColors.Window;
			this.wizardPageTitlePanel.Controls.Add(this.wizardPageDescControl);
			this.wizardPageTitlePanel.Controls.Add(this.wizardPageTitleControl);
			this.wizardPageTitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.wizardPageTitlePanel.Location = new System.Drawing.Point(0, 0);
			this.wizardPageTitlePanel.Name = "wizardPageTitlePanel";
			this.wizardPageTitlePanel.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.wizardPageTitlePanel.Size = new System.Drawing.Size(630, 60);
			this.wizardPageTitlePanel.TabIndex = 0;
			// 
			// wizardPageDescControl
			// 
			this.wizardPageDescControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wizardPageDescControl.Location = new System.Drawing.Point(10, 25);
			this.wizardPageDescControl.Name = "wizardPageDescControl";
			this.wizardPageDescControl.Size = new System.Drawing.Size(610, 30);
			this.wizardPageDescControl.TabIndex = 1;
			// 
			// wizardPageTitleControl
			// 
			this.wizardPageTitleControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.wizardPageTitleControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.wizardPageTitleControl.Location = new System.Drawing.Point(10, 5);
			this.wizardPageTitleControl.Name = "wizardPageTitleControl";
			this.wizardPageTitleControl.Size = new System.Drawing.Size(610, 20);
			this.wizardPageTitleControl.TabIndex = 0;
			// 
			// wizardPage3DSeperator
			// 
			this.wizardPage3DSeperator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.wizardPage3DSeperator.Dock = System.Windows.Forms.DockStyle.Top;
			this.wizardPage3DSeperator.Location = new System.Drawing.Point(0, 60);
			this.wizardPage3DSeperator.Name = "wizardPage3DSeperator";
			this.wizardPage3DSeperator.Size = new System.Drawing.Size(630, 2);
			this.wizardPage3DSeperator.TabIndex = 1;
			// 
			// WizardPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.wizardPage3DSeperator);
			this.Controls.Add(this.wizardPageTitlePanel);
			this.Name = "WizardPage";
			this.Size = new System.Drawing.Size(630, 298);
			this.wizardPageTitlePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel wizardPageTitlePanel;
		private System.Windows.Forms.Label wizardPage3DSeperator;
		private System.Windows.Forms.Label wizardPageTitleControl;
		private System.Windows.Forms.Label wizardPageDescControl;
	}
}
