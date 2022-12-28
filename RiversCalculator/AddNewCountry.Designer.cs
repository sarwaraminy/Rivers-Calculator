namespace RiversCalculator
{
    partial class AddNewCountry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewCountry));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAddCntry = new System.Windows.Forms.Button();
            this.txtCntryISO = new System.Windows.Forms.TextBox();
            this.txtCntryName = new System.Windows.Forms.TextBox();
            this.txtNewCntry = new System.Windows.Forms.TextBox();
            this.lblCntryISO = new System.Windows.Forms.Label();
            this.lblCntryName = new System.Windows.Forms.Label();
            this.lblCntryID = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblBRight = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnAddCntry);
            this.panel1.Controls.Add(this.txtCntryISO);
            this.panel1.Controls.Add(this.txtCntryName);
            this.panel1.Controls.Add(this.txtNewCntry);
            this.panel1.Controls.Add(this.lblCntryISO);
            this.panel1.Controls.Add(this.lblCntryName);
            this.panel1.Controls.Add(this.lblCntryID);
            this.panel1.Location = new System.Drawing.Point(2, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 93);
            this.panel1.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.Location = new System.Drawing.Point(11, 55);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(71, 35);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnAddCntry
            // 
            this.btnAddCntry.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnAddCntry.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCntry.Image")));
            this.btnAddCntry.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddCntry.Location = new System.Drawing.Point(230, 52);
            this.btnAddCntry.Name = "btnAddCntry";
            this.btnAddCntry.Size = new System.Drawing.Size(100, 37);
            this.btnAddCntry.TabIndex = 4;
            this.btnAddCntry.Text = "Add";
            this.btnAddCntry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCntry.UseVisualStyleBackColor = false;
            this.btnAddCntry.Click += new System.EventHandler(this.btnAddCntry_Click);
            // 
            // txtCntryISO
            // 
            this.txtCntryISO.Location = new System.Drawing.Point(230, 26);
            this.txtCntryISO.Name = "txtCntryISO";
            this.txtCntryISO.Size = new System.Drawing.Size(100, 20);
            this.txtCntryISO.TabIndex = 3;
            // 
            // txtCntryName
            // 
            this.txtCntryName.Location = new System.Drawing.Point(71, 26);
            this.txtCntryName.Name = "txtCntryName";
            this.txtCntryName.Size = new System.Drawing.Size(153, 20);
            this.txtCntryName.TabIndex = 2;
            // 
            // txtNewCntry
            // 
            this.txtNewCntry.Location = new System.Drawing.Point(11, 26);
            this.txtNewCntry.Name = "txtNewCntry";
            this.txtNewCntry.Size = new System.Drawing.Size(54, 20);
            this.txtNewCntry.TabIndex = 1;
            // 
            // lblCntryISO
            // 
            this.lblCntryISO.AutoSize = true;
            this.lblCntryISO.Location = new System.Drawing.Point(227, 9);
            this.lblCntryISO.Name = "lblCntryISO";
            this.lblCntryISO.Size = new System.Drawing.Size(25, 13);
            this.lblCntryISO.TabIndex = 0;
            this.lblCntryISO.Text = "ISO";
            // 
            // lblCntryName
            // 
            this.lblCntryName.AutoSize = true;
            this.lblCntryName.Location = new System.Drawing.Point(71, 10);
            this.lblCntryName.Name = "lblCntryName";
            this.lblCntryName.Size = new System.Drawing.Size(74, 13);
            this.lblCntryName.TabIndex = 0;
            this.lblCntryName.Text = "Country Name";
            // 
            // lblCntryID
            // 
            this.lblCntryID.AutoSize = true;
            this.lblCntryID.Location = new System.Drawing.Point(8, 9);
            this.lblCntryID.Name = "lblCntryID";
            this.lblCntryID.Size = new System.Drawing.Size(57, 13);
            this.lblCntryID.TabIndex = 0;
            this.lblCntryID.Text = "Country ID";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MintCream;
            this.panel2.Controls.Add(this.lblMessage);
            this.panel2.Location = new System.Drawing.Point(2, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(339, 33);
            this.panel2.TabIndex = 1;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMessage.Location = new System.Drawing.Point(4, 5);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(328, 23);
            this.lblMessage.TabIndex = 0;
            // 
            // lblBRight
            // 
            this.lblBRight.LinkColor = System.Drawing.Color.Navy;
            this.lblBRight.Location = new System.Drawing.Point(1, 144);
            this.lblBRight.Name = "lblBRight";
            this.lblBRight.Size = new System.Drawing.Size(340, 21);
            this.lblBRight.TabIndex = 75;
            this.lblBRight.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblBRight_LinkClicked);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 21);
            this.label1.TabIndex = 74;
            this.label1.Text = "_________________________________________________________________________________" +
                "____";
            // 
            // AddNewCountry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 162);
            this.Controls.Add(this.lblBRight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(358, 201);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(358, 201);
            this.Name = "AddNewCountry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddNewCountry";
            this.Load += new System.EventHandler(this.AddNewCountry_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtNewCntry;
        private System.Windows.Forms.Label lblCntryID;
        private System.Windows.Forms.TextBox txtCntryISO;
        private System.Windows.Forms.TextBox txtCntryName;
        private System.Windows.Forms.Label lblCntryISO;
        private System.Windows.Forms.Label lblCntryName;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAddCntry;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.LinkLabel lblBRight;
        private System.Windows.Forms.Label label1;
    }
}