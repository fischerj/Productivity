﻿namespace Productivity
{
    partial class ProductivityView
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
            System.Windows.Forms.ToolStrip toolStrip;
            System.Windows.Forms.ToolStripButton manageRulesButton;
            System.Windows.Forms.ToolStripContainer toolStripContainer;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductivityView));
            toolStrip = new System.Windows.Forms.ToolStrip();
            manageRulesButton = new System.Windows.Forms.ToolStripButton();
            toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            toolStrip.SuspendLayout();
            toolStripContainer.TopToolStripPanel.SuspendLayout();
            toolStripContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            manageRulesButton});
            toolStrip.Location = new System.Drawing.Point(3, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new System.Drawing.Size(35, 25);
            toolStrip.TabIndex = 0;
            // 
            // manageRulesButton
            // 
            manageRulesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            manageRulesButton.Image = global::Productivity.Properties.Resources.Settings;
            manageRulesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            manageRulesButton.Name = "manageRulesButton";
            manageRulesButton.Size = new System.Drawing.Size(23, 22);
            manageRulesButton.Text = "Manage Rules";
            manageRulesButton.Click += new System.EventHandler(this.manageRulesButton_Click);
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            toolStripContainer.ContentPanel.Size = new System.Drawing.Size(284, 237);
            toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            toolStripContainer.Location = new System.Drawing.Point(0, 0);
            toolStripContainer.Name = "toolStripContainer";
            toolStripContainer.Size = new System.Drawing.Size(284, 262);
            toolStripContainer.TabIndex = 0;
            toolStripContainer.Text = "toolStripContainer";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            toolStripContainer.TopToolStripPanel.Controls.Add(toolStrip);
            // 
            // ProductivityView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(toolStripContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductivityView";
            this.Text = "ProductivityView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductivityView_FormClosing);
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.PerformLayout();
            toolStripContainer.ResumeLayout(false);
            toolStripContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

    }
}