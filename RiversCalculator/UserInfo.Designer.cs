namespace RiversCalculator
{
    partial class UserInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInfo));
            this.lblBRight = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblRegisterDate = new System.Windows.Forms.Label();
            this.txtRegisterDate = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblRePassword = new System.Windows.Forms.Label();
            this.txtRePassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUserid = new System.Windows.Forms.Label();
            this.txtUserid = new System.Windows.Forms.TextBox();
            this.lblLastname = new System.Windows.Forms.Label();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.lblfirstname = new System.Windows.Forms.Label();
            this.txtfirstname = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBRight
            // 
            this.lblBRight.AutoSize = true;
            this.lblBRight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBRight.LinkColor = System.Drawing.Color.Navy;
            this.lblBRight.Location = new System.Drawing.Point(-4, 148);
            this.lblBRight.Name = "lblBRight";
            this.lblBRight.Size = new System.Drawing.Size(260, 13);
            this.lblBRight.TabIndex = 77;
            this.lblBRight.TabStop = true;
            this.lblBRight.Text = "Copyright: Mohammad Sarwar Amini ->+93706254517";
            this.lblBRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBRight.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblBRight_LinkClicked);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(-6, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 21);
            this.label1.TabIndex = 76;
            this.label1.Text = "_________________________________________________________________________________" +
                "____";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(195, 94);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(179, 32);
            this.btnClear.TabIndex = 75;
            this.btnClear.Text = "Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblRegisterDate
            // 
            this.lblRegisterDate.AutoSize = true;
            this.lblRegisterDate.BackColor = System.Drawing.Color.MediumTurquoise;
            this.lblRegisterDate.Location = new System.Drawing.Point(262, 53);
            this.lblRegisterDate.Name = "lblRegisterDate";
            this.lblRegisterDate.Size = new System.Drawing.Size(67, 13);
            this.lblRegisterDate.TabIndex = 74;
            this.lblRegisterDate.Text = "Current Date";
            // 
            // txtRegisterDate
            // 
            this.txtRegisterDate.Location = new System.Drawing.Point(262, 69);
            this.txtRegisterDate.Name = "txtRegisterDate";
            this.txtRegisterDate.ReadOnly = true;
            this.txtRegisterDate.Size = new System.Drawing.Size(111, 20);
            this.txtRegisterDate.TabIndex = 70;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Image = ((System.Drawing.Image)(resources.GetObject("btnRegister.Image")));
            this.btnRegister.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegister.Location = new System.Drawing.Point(11, 95);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(160, 32);
            this.btnRegister.TabIndex = 72;
            this.btnRegister.Text = "Register";
            this.btnRegister.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblRePassword
            // 
            this.lblRePassword.AutoSize = true;
            this.lblRePassword.BackColor = System.Drawing.Color.MediumTurquoise;
            this.lblRePassword.Location = new System.Drawing.Point(140, 50);
            this.lblRePassword.Name = "lblRePassword";
            this.lblRePassword.Size = new System.Drawing.Size(97, 13);
            this.lblRePassword.TabIndex = 73;
            this.lblRePassword.Tag = "";
            this.lblRePassword.Text = "Re-Type Password";
            // 
            // txtRePassword
            // 
            this.txtRePassword.Location = new System.Drawing.Point(137, 68);
            this.txtRePassword.Name = "txtRePassword";
            this.txtRePassword.PasswordChar = '*';
            this.txtRePassword.Size = new System.Drawing.Size(116, 20);
            this.txtRePassword.TabIndex = 65;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.MediumTurquoise;
            this.lblPassword.Location = new System.Drawing.Point(15, 50);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 71;
            this.lblPassword.Tag = "";
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(12, 68);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(115, 20);
            this.txtPassword.TabIndex = 64;
            // 
            // lblUserid
            // 
            this.lblUserid.AutoSize = true;
            this.lblUserid.BackColor = System.Drawing.Color.MediumTurquoise;
            this.lblUserid.Location = new System.Drawing.Point(264, 6);
            this.lblUserid.Name = "lblUserid";
            this.lblUserid.Size = new System.Drawing.Size(68, 13);
            this.lblUserid.TabIndex = 69;
            this.lblUserid.Tag = "";
            this.lblUserid.Text = "New User ID";
            // 
            // txtUserid
            // 
            this.txtUserid.Location = new System.Drawing.Point(264, 23);
            this.txtUserid.Name = "txtUserid";
            this.txtUserid.Size = new System.Drawing.Size(115, 20);
            this.txtUserid.TabIndex = 62;
            // 
            // lblLastname
            // 
            this.lblLastname.AutoSize = true;
            this.lblLastname.BackColor = System.Drawing.Color.MediumTurquoise;
            this.lblLastname.Location = new System.Drawing.Point(134, 7);
            this.lblLastname.Name = "lblLastname";
            this.lblLastname.Size = new System.Drawing.Size(58, 13);
            this.lblLastname.TabIndex = 66;
            this.lblLastname.Text = "Last Name";
            // 
            // txtLastname
            // 
            this.txtLastname.Location = new System.Drawing.Point(136, 23);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(122, 20);
            this.txtLastname.TabIndex = 68;
            this.txtLastname.TextChanged += new System.EventHandler(this.txtLastname_TextChanged);
            // 
            // lblfirstname
            // 
            this.lblfirstname.AutoSize = true;
            this.lblfirstname.BackColor = System.Drawing.Color.MediumTurquoise;
            this.lblfirstname.Location = new System.Drawing.Point(8, 7);
            this.lblfirstname.Name = "lblfirstname";
            this.lblfirstname.Size = new System.Drawing.Size(57, 13);
            this.lblfirstname.TabIndex = 63;
            this.lblfirstname.Text = "First Name";
            // 
            // txtfirstname
            // 
            this.txtfirstname.Location = new System.Drawing.Point(8, 23);
            this.txtfirstname.Name = "txtfirstname";
            this.txtfirstname.Size = new System.Drawing.Size(122, 20);
            this.txtfirstname.TabIndex = 67;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel1.Controls.Add(this.txtRePassword);
            this.panel1.Controls.Add(this.lblRePassword);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Location = new System.Drawing.Point(-1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 132);
            this.panel1.TabIndex = 78;
            // 
            // UserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 165);
            this.Controls.Add(this.lblBRight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRegisterDate);
            this.Controls.Add(this.txtRegisterDate);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.lblUserid);
            this.Controls.Add(this.txtUserid);
            this.Controls.Add(this.lblLastname);
            this.Controls.Add(this.txtLastname);
            this.Controls.Add(this.lblfirstname);
            this.Controls.Add(this.txtfirstname);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserInfo";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Register a new User ID";
            this.Load += new System.EventHandler(this.UserInfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lblBRight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblRegisterDate;
        private System.Windows.Forms.TextBox txtRegisterDate;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblRePassword;
        private System.Windows.Forms.TextBox txtRePassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUserid;
        private System.Windows.Forms.TextBox txtUserid;
        private System.Windows.Forms.Label lblLastname;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.Label lblfirstname;
        private System.Windows.Forms.TextBox txtfirstname;
        private System.Windows.Forms.Panel panel1;
    }
}