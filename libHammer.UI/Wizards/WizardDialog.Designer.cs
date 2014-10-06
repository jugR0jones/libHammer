namespace Codoxide.Common.Windows.Controls.Wizards
{
	partial class WizardDialog
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
			this.contentPanel = new System.Windows.Forms.Panel();
			this.buttonContainer = new System.Windows.Forms.FlowLayoutPanel();
			this.DismissButton = new System.Windows.Forms.Button();
			this.FinishButton = new System.Windows.Forms.Button();
			this.NextButton = new System.Windows.Forms.Button();
			this.BackButton = new System.Windows.Forms.Button();
			this.lbl3D = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.additionalButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonContainer.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contentPanel
			// 
			this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contentPanel.Location = new System.Drawing.Point(0, 0);
			this.contentPanel.Name = "contentPanel";
			this.contentPanel.Size = new System.Drawing.Size(648, 315);
			this.contentPanel.TabIndex = 0;
			// 
			// buttonContainer
			// 
			this.buttonContainer.Controls.Add(this.DismissButton);
			this.buttonContainer.Controls.Add(this.FinishButton);
			this.buttonContainer.Controls.Add(this.NextButton);
			this.buttonContainer.Controls.Add(this.BackButton);
			this.buttonContainer.Dock = System.Windows.Forms.DockStyle.Right;
			this.buttonContainer.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.buttonContainer.Location = new System.Drawing.Point(302, 2);
			this.buttonContainer.Name = "buttonContainer";
			this.buttonContainer.Size = new System.Drawing.Size(346, 32);
			this.buttonContainer.TabIndex = 1;
			// 
			// DismissButton
			// 
			this.DismissButton.Location = new System.Drawing.Point(268, 3);
			this.DismissButton.Name = "DismissButton";
			this.DismissButton.Size = new System.Drawing.Size(75, 23);
			this.DismissButton.TabIndex = 2;
			this.DismissButton.Text = "Cancel";
			this.DismissButton.UseVisualStyleBackColor = true;
			// 
			// FinishButton
			// 
			this.FinishButton.Enabled = false;
			this.FinishButton.Location = new System.Drawing.Point(187, 3);
			this.FinishButton.Name = "FinishButton";
			this.FinishButton.Size = new System.Drawing.Size(75, 23);
			this.FinishButton.TabIndex = 3;
			this.FinishButton.Text = "Finish";
			this.FinishButton.UseVisualStyleBackColor = true;
			this.FinishButton.Click += new System.EventHandler(this.FinishButton_Click);
			// 
			// NextButton
			// 
			this.NextButton.Enabled = false;
			this.NextButton.Location = new System.Drawing.Point(106, 3);
			this.NextButton.Name = "NextButton";
			this.NextButton.Size = new System.Drawing.Size(75, 23);
			this.NextButton.TabIndex = 1;
			this.NextButton.Text = "Next >";
			this.NextButton.UseVisualStyleBackColor = true;
			this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
			// 
			// BackButton
			// 
			this.BackButton.Enabled = false;
			this.BackButton.Location = new System.Drawing.Point(25, 3);
			this.BackButton.Name = "BackButton";
			this.BackButton.Size = new System.Drawing.Size(75, 23);
			this.BackButton.TabIndex = 0;
			this.BackButton.Text = "< Back";
			this.BackButton.UseVisualStyleBackColor = true;
			this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
			// 
			// lbl3D
			// 
			this.lbl3D.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl3D.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl3D.Location = new System.Drawing.Point(0, 0);
			this.lbl3D.Name = "lbl3D";
			this.lbl3D.Size = new System.Drawing.Size(648, 2);
			this.lbl3D.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.additionalButtonsPanel);
			this.panel1.Controls.Add(this.buttonContainer);
			this.panel1.Controls.Add(this.lbl3D);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 315);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(648, 34);
			this.panel1.TabIndex = 0;
			// 
			// additionalButtonsPanel
			// 
			this.additionalButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.additionalButtonsPanel.Location = new System.Drawing.Point(0, 2);
			this.additionalButtonsPanel.Name = "additionalButtonsPanel";
			this.additionalButtonsPanel.Size = new System.Drawing.Size(302, 32);
			this.additionalButtonsPanel.TabIndex = 3;
			// 
			// WizardDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(648, 349);
			this.Controls.Add(this.contentPanel);
			this.Controls.Add(this.panel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WizardDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WizardDialog";
			this.buttonContainer.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel contentPanel;
		private System.Windows.Forms.FlowLayoutPanel buttonContainer;
		private System.Windows.Forms.Label lbl3D;
		public System.Windows.Forms.Button DismissButton;
		public System.Windows.Forms.Button NextButton;
		public System.Windows.Forms.Button BackButton;
		public System.Windows.Forms.Button FinishButton;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FlowLayoutPanel additionalButtonsPanel;
	}
}