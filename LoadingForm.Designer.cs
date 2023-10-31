namespace GigaChat
{
    partial class LoadingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingForm));
            this.LoadingLabel = new System.Windows.Forms.Label();
            this.loadingBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // LoadingLabel
            // 
            this.LoadingLabel.AutoSize = true;
            this.LoadingLabel.Font = new System.Drawing.Font("Comic Sans MS", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadingLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LoadingLabel.Location = new System.Drawing.Point(309, 151);
            this.LoadingLabel.Name = "LoadingLabel";
            this.LoadingLabel.Size = new System.Drawing.Size(187, 48);
            this.LoadingLabel.TabIndex = 0;
            this.LoadingLabel.Text = "Loading...";
            // 
            // loadingBar
            // 
            this.loadingBar.ForeColor = System.Drawing.Color.Lime;
            this.loadingBar.Location = new System.Drawing.Point(294, 214);
            this.loadingBar.Name = "loadingBar";
            this.loadingBar.Size = new System.Drawing.Size(221, 23);
            this.loadingBar.TabIndex = 1;
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.loadingBar);
            this.Controls.Add(this.LoadingLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoadingForm";
            this.Text = "LoadingForm";
            this.Load += new System.EventHandler(this.LoadingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LoadingLabel;
        private System.Windows.Forms.ProgressBar loadingBar;
    }
}