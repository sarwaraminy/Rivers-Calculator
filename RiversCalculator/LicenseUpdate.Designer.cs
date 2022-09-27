namespace RiversCalculator
{
    partial class LicenseUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseUpdate));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.endDateLice = new System.Windows.Forms.DateTimePicker();
            this.fromDateLice = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbUserIDLice = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBRight = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.endDateLice);
            this.groupBox1.Controls.Add(this.fromDateLice);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbUserIDLice);
            this.groupBox1.Location = new System.Drawing.Point(-1, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(321, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extend a user license";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnUpdate.Location = new System.Drawing.Point(13, 65);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(297, 23);
            this.btnUpdate.TabIndex = 20;
            this.btnUpdate.Text = "Extend";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // endDateLice
            // 
            this.endDateLice.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.endDateLice.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDateLice.Location = new System.Drawing.Point(230, 38);
            this.endDateLice.Name = "endDateLice";
            this.endDateLice.RightToLeftLayout = true;
            this.endDateLice.Size = new System.Drawing.Size(80, 20);
            this.endDateLice.TabIndex = 19;
            // 
            // fromDateLice
            // 
            this.fromDateLice.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.fromDateLice.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDateLice.Location = new System.Drawing.Point(137, 39);
            this.fromDateLice.Name = "fromDateLice";
            this.fromDateLice.RightToLeftLayout = true;
            this.fromDateLice.Size = new System.Drawing.Size(79, 20);
            this.fromDateLice.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "End Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Start Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "List of User IDs";
            // 
            // cmbUserIDLice
            // 
            this.cmbUserIDLice.FormattingEnabled = true;
            this.cmbUserIDLice.Location = new System.Drawing.Point(14, 38);
            this.cmbUserIDLice.Name = "cmbUserIDLice";
            this.cmbUserIDLice.Size = new System.Drawing.Size(115, 21);
            this.cmbUserIDLice.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(-1, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(331, 21);
            this.label4.TabIndex = 71;
            this.label4.Text = "_________________________________________________________________________________" +
                "______________________________";
            // 
            // lblBRight
            // 
            this.lblBRight.AutoSize = true;
            this.lblBRight.LinkColor = System.Drawing.Color.Navy;
            this.lblBRight.Location = new System.Drawing.Point(2, 123);
            this.lblBRight.Name = "lblBRight";
            this.lblBRight.Size = new System.Drawing.Size(260, 13);
            this.lblBRight.TabIndex = 72;
            this.lblBRight.TabStop = true;
            this.lblBRight.Text = "Copyright: Mohammad Sarwar Amini ->+93706254517";
            // 
            // LicenseUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 142);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblBRight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseUpdate";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Extend a user license";
            this.Load += new System.EventHandler(this.LicenseUpdate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbUserIDLice;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DateTimePicker endDateLice;
        private System.Windows.Forms.DateTimePicker fromDateLice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel lblBRight;
    }
}