namespace Game_of_Life
{
    partial class ImportFileFail
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ImportFileFailbutton1 = new System.Windows.Forms.Button();
            this.ImportFileFailLabel1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ImportFileFailLabel1);
            this.panel1.Controls.Add(this.ImportFileFailbutton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 187);
            this.panel1.TabIndex = 0;
            // 
            // ImportFileFailbutton1
            // 
            this.ImportFileFailbutton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ImportFileFailbutton1.Location = new System.Drawing.Point(157, 140);
            this.ImportFileFailbutton1.Name = "ImportFileFailbutton1";
            this.ImportFileFailbutton1.Size = new System.Drawing.Size(87, 30);
            this.ImportFileFailbutton1.TabIndex = 0;
            this.ImportFileFailbutton1.Text = "Ok";
            this.ImportFileFailbutton1.UseVisualStyleBackColor = true;
            // 
            // ImportFileFailLabel1
            // 
            this.ImportFileFailLabel1.AutoSize = true;
            this.ImportFileFailLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportFileFailLabel1.Location = new System.Drawing.Point(10, 17);
            this.ImportFileFailLabel1.Name = "ImportFileFailLabel1";
            this.ImportFileFailLabel1.Size = new System.Drawing.Size(380, 100);
            this.ImportFileFailLabel1.TabIndex = 1;
            this.ImportFileFailLabel1.Text = "Error opening file.\r\n\r\nTry re-sizing the grid larger.\r\n\r\nGo to settings, options," +
    " width and height cells.";
            // 
            // ImportFileFail
            // 
            this.AcceptButton = this.ImportFileFailbutton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ImportFileFailbutton1;
            this.ClientSize = new System.Drawing.Size(400, 187);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportFileFail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ImportFileFail";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ImportFileFailLabel1;
        private System.Windows.Forms.Button ImportFileFailbutton1;
    }
}