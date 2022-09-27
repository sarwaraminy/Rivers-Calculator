namespace RiversCalculator
{
    partial class ExcelUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelUpload));
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnLoadToDb = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblBRight = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.openXlsxFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ptcrBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptcrBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(12, 13);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(173, 52);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Select Excel File";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnLoadToDb
            // 
            this.btnLoadToDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadToDb.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnLoadToDb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadToDb.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadToDb.Image")));
            this.btnLoadToDb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadToDb.Location = new System.Drawing.Point(234, 13);
            this.btnLoadToDb.Name = "btnLoadToDb";
            this.btnLoadToDb.Size = new System.Drawing.Size(173, 58);
            this.btnLoadToDb.TabIndex = 0;
            this.btnLoadToDb.Text = "Load to Database";
            this.btnLoadToDb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoadToDb.UseVisualStyleBackColor = false;
            this.btnLoadToDb.Click += new System.EventHandler(this.btnLoadToDb_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.btnLoadToDb);
            this.panel1.Location = new System.Drawing.Point(-6, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 84);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Yellow;
            this.panel2.Location = new System.Drawing.Point(5, -40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(378, 34);
            this.panel2.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.MistyRose;
            this.panel4.Controls.Add(this.ptcrBox1);
            this.panel4.Controls.Add(this.lblMessage);
            this.panel4.Location = new System.Drawing.Point(0, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(406, 36);
            this.panel4.TabIndex = 3;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMessage.Location = new System.Drawing.Point(5, 3);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(396, 31);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBRight
            // 
            this.lblBRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBRight.AutoSize = true;
            this.lblBRight.LinkColor = System.Drawing.Color.Navy;
            this.lblBRight.Location = new System.Drawing.Point(3, 153);
            this.lblBRight.Name = "lblBRight";
            this.lblBRight.Size = new System.Drawing.Size(260, 13);
            this.lblBRight.TabIndex = 67;
            this.lblBRight.TabStop = true;
            this.lblBRight.Text = "Copyright: Mohammad Sarwar Amini ->+93706254517";
            this.lblBRight.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblBRight_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(-5, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(411, 21);
            this.label2.TabIndex = 66;
            this.label2.Text = "_________________________________________________________________________________" +
                "________";
            // 
            // ptcrBox1
            // 
            this.ptcrBox1.Image = ((System.Drawing.Image)(resources.GetObject("ptcrBox1.Image")));
            this.ptcrBox1.Location = new System.Drawing.Point(0, 0);
            this.ptcrBox1.Name = "ptcrBox1";
            this.ptcrBox1.Size = new System.Drawing.Size(100, 36);
            this.ptcrBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ptcrBox1.TabIndex = 1;
            this.ptcrBox1.TabStop = false;
            // 
            // ExcelUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 170);
            this.Controls.Add(this.lblBRight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExcelUpload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExcelUpload";
            this.Load += new System.EventHandler(this.ExcelUpload_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptcrBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnLoadToDb;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.LinkLabel lblBRight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.OpenFileDialog openXlsxFileDialog;
        private System.Windows.Forms.PictureBox ptcrBox1;
    }
}