namespace Game_of_Life
{
    partial class FromSeedDialog
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
            this.SeedLabel = new System.Windows.Forms.Label();
            this.FromSeednumericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Seedbutton1 = new System.Windows.Forms.Button();
            this.FromSeedbuttonOk = new System.Windows.Forms.Button();
            this.FromSeedButtonCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FromSeednumericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FromSeedButtonCancel);
            this.panel1.Controls.Add(this.FromSeedbuttonOk);
            this.panel1.Controls.Add(this.Seedbutton1);
            this.panel1.Controls.Add(this.FromSeednumericUpDown1);
            this.panel1.Controls.Add(this.SeedLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(333, 128);
            this.panel1.TabIndex = 0;
            // 
            // SeedLabel
            // 
            this.SeedLabel.AutoSize = true;
            this.SeedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SeedLabel.Location = new System.Drawing.Point(38, 39);
            this.SeedLabel.Name = "SeedLabel";
            this.SeedLabel.Size = new System.Drawing.Size(47, 20);
            this.SeedLabel.TabIndex = 0;
            this.SeedLabel.Text = "Seed";
            // 
            // FromSeednumericUpDown1
            // 
            this.FromSeednumericUpDown1.Location = new System.Drawing.Point(91, 39);
            this.FromSeednumericUpDown1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.FromSeednumericUpDown1.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.FromSeednumericUpDown1.Name = "FromSeednumericUpDown1";
            this.FromSeednumericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.FromSeednumericUpDown1.TabIndex = 1;
            // 
            // Seedbutton1
            // 
            this.Seedbutton1.Location = new System.Drawing.Point(227, 39);
            this.Seedbutton1.Name = "Seedbutton1";
            this.Seedbutton1.Size = new System.Drawing.Size(75, 23);
            this.Seedbutton1.TabIndex = 2;
            this.Seedbutton1.Text = "Randomize";
            this.Seedbutton1.UseVisualStyleBackColor = true;
            this.Seedbutton1.Click += new System.EventHandler(this.Seedbutton1_Click);
            // 
            // FromSeedbuttonOk
            // 
            this.FromSeedbuttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.FromSeedbuttonOk.Location = new System.Drawing.Point(66, 90);
            this.FromSeedbuttonOk.Name = "FromSeedbuttonOk";
            this.FromSeedbuttonOk.Size = new System.Drawing.Size(75, 23);
            this.FromSeedbuttonOk.TabIndex = 3;
            this.FromSeedbuttonOk.Text = "Ok";
            this.FromSeedbuttonOk.UseVisualStyleBackColor = true;
            // 
            // FromSeedButtonCancel
            // 
            this.FromSeedButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.FromSeedButtonCancel.Location = new System.Drawing.Point(191, 90);
            this.FromSeedButtonCancel.Name = "FromSeedButtonCancel";
            this.FromSeedButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.FromSeedButtonCancel.TabIndex = 4;
            this.FromSeedButtonCancel.Text = "Cancel";
            this.FromSeedButtonCancel.UseVisualStyleBackColor = true;
            // 
            // FromSeedDialog
            // 
            this.AcceptButton = this.FromSeedbuttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.FromSeedButtonCancel;
            this.ClientSize = new System.Drawing.Size(333, 128);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FromSeedDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FromSeedDialog";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FromSeednumericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Seedbutton1;
        private System.Windows.Forms.NumericUpDown FromSeednumericUpDown1;
        private System.Windows.Forms.Label SeedLabel;
        private System.Windows.Forms.Button FromSeedButtonCancel;
        private System.Windows.Forms.Button FromSeedbuttonOk;
    }
}