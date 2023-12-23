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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.EXTRAEXIT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(778, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 24);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // EXTRAEXIT
            // 
            this.EXTRAEXIT.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.EXTRAEXIT.BackColor = System.Drawing.Color.DimGray;
            this.EXTRAEXIT.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.EXTRAEXIT.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EXTRAEXIT.Location = new System.Drawing.Point(322, -1);
            this.EXTRAEXIT.Name = "EXTRAEXIT";
            this.EXTRAEXIT.Size = new System.Drawing.Size(116, 37);
            this.EXTRAEXIT.TabIndex = 5;
            this.EXTRAEXIT.Text = "EXTRAEXIT";
            this.EXTRAEXIT.UseVisualStyleBackColor = false;
            this.EXTRAEXIT.Click += new System.EventHandler(this.EXTRAEXIT_Click);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(816, 489);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.EXTRAEXIT);
            this.Controls.Add(this.centralWidget);
            this.Controls.Add(this.centeralWidget);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "BaseForm";
            this.Text = "GigaChat";
            this.Load += new System.EventHandler(this.BaseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel centeralWidget;
        private System.Windows.Forms.Panel centralWidget;
        private System.Windows.Forms.Button EXTRAEXIT;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}