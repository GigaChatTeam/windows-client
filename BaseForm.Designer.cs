namespace GigaChat
{
    partial class BaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.centeralWidget = new System.Windows.Forms.Panel();
            this.centralWidget = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // centeralWidget
            // 
            this.centeralWidget.AutoScroll = true;
            this.centeralWidget.AutoScrollMinSize = new System.Drawing.Size(729, 399);
            this.centeralWidget.AutoSize = true;
            this.centeralWidget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.centeralWidget.Cursor = System.Windows.Forms.Cursors.Default;
            this.centeralWidget.Dock = System.Windows.Forms.DockStyle.Right;
            this.centeralWidget.Location = new System.Drawing.Point(816, 0);
            this.centeralWidget.Name = "centeralWidget";
            this.centeralWidget.Size = new System.Drawing.Size(0, 489);
            this.centeralWidget.TabIndex = 3;
            // 
            // centralWidget
            // 
            this.centralWidget.AutoSize = true;
            this.centralWidget.Dock = System.Windows.Forms.DockStyle.Right;
            this.centralWidget.Location = new System.Drawing.Point(816, 0);
            this.centralWidget.Name = "centralWidget";
            this.centralWidget.Size = new System.Drawing.Size(0, 489);
            this.centralWidget.TabIndex = 4;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(816, 489);
            this.Controls.Add(this.centralWidget);
            this.Controls.Add(this.centeralWidget);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "BaseForm";
            this.Text = "GigaChat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel centeralWidget;
        private System.Windows.Forms.Panel centralWidget;
    }
}