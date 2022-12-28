namespace RiversCalculator
{
    partial class DeletAuser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeletAuser));
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.btnReLogin = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtCurrentDate = new System.Windows.Forms.DateTimePicker();
            this.grpdeleteuser = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblRegisterDate = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblBRight = new System.Windows.Forms.LinkLabel();
            this.lblDeleteUserMessage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpdeleteuser.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.Location = new System.Drawing.Point(391, 32);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(120, 160);
            this.lstUsers.TabIndex = 34;
            // 
            // btnReLogin
            // 
            this.btnReLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReLogin.FlatAppearance.BorderSize = 0;
            this.btnReLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnReLogin.Image")));
            this.btnReLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReLogin.Location = new System.Drawing.Point(28, 151);
            this.btnReLogin.Name = "btnReLogin";
            this.btnReLogin.Size = new System.Drawing.Size(159, 43);
            this.btnReLogin.TabIndex = 33;
            this.btnReLogin.Text = "Refresh Application";
            this.btnReLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReLogin.UseVisualStyleBackColor = true;
            this.btnReLogin.Click += new System.EventHandler(this.btnReLogin_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.FlatAppearance.BorderSize = 0;
            this.btnDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAll.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteAll.Image")));
            this.btnDeleteAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAll.Location = new System.Drawing.Point(266, 150);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(119, 49);
            this.btnDeleteAll.TabIndex = 32;
            this.btnDeleteAll.Text = "Delete All ID";
            this.btnDeleteAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnFind
            // 
            this.btnFind.FlatAppearance.BorderSize = 0;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Image = ((System.Drawing.Image)(resources.GetObject("btnFind.Image")));
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFind.Location = new System.Drawing.Point(268, 99);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(96, 43);
            this.btnFind.TabIndex = 31;
            this.btnFind.Text = "Search";
            this.btnFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtCurrentDate
            // 
            this.txtCurrentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCurrentDate.Location = new System.Drawing.Point(266, 73);
            this.txtCurrentDate.Name = "txtCurrentDate";
            this.txtCurrentDate.RightToLeftLayout = true;
            this.txtCurrentDate.Size = new System.Drawing.Size(115, 20);
            this.txtCurrentDate.TabIndex = 30;
            // 
            // grpdeleteuser
            // 
            this.grpdeleteuser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpdeleteuser.BackColor = System.Drawing.Color.Cornsilk;
            this.grpdeleteuser.Controls.Add(this.lstUsers);
            this.grpdeleteuser.Controls.Add(this.btnReLogin);
            this.grpdeleteuser.Controls.Add(this.btnDeleteAll);
            this.grpdeleteuser.Controls.Add(this.btnFind);
            this.grpdeleteuser.Controls.Add(this.txtCurrentDate);
            this.grpdeleteuser.Controls.Add(this.btnEdit);
            this.grpdeleteuser.Controls.Add(this.lblRegisterDate);
            this.grpdeleteuser.Controls.Add(this.btnDelete);
            this.grpdeleteuser.Controls.Add(this.lblRePassword);
            this.grpdeleteuser.Controls.Add(this.txtRePassword);
            this.grpdeleteuser.Controls.Add(this.lblPassword);
            this.grpdeleteuser.Controls.Add(this.txtPassword);
            this.grpdeleteuser.Controls.Add(this.lblUserid);
            this.grpdeleteuser.Controls.Add(this.txtUserid);
            this.grpdeleteuser.Controls.Add(this.lblLastname);
            this.grpdeleteuser.Controls.Add(this.txtLastname);
            this.grpdeleteuser.Controls.Add(this.lblfirstname);
            this.grpdeleteuser.Controls.Add(this.txtfirstname);
            this.grpdeleteuser.Controls.Add(this.label1);
            this.grpdeleteuser.Location = new System.Drawing.Point(8, 35);
            this.grpdeleteuser.Name = "grpdeleteuser";
            this.grpdeleteuser.Size = new System.Drawing.Size(520, 205);
            this.grpdeleteuser.TabIndex = 63;
            this.grpdeleteuser.TabStop = false;
            // 
            // btnEdit
            // 
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(142, 99);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(106, 43);
            this.btnEdit.TabIndex = 29;
            this.btnEdit.Text = "Update";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblRegisterDate
            // 
            this.lblRegisterDate.AutoSize = true;
            this.lblRegisterDate.Location = new System.Drawing.Point(268, 57);
            this.lblRegisterDate.Name = "lblRegisterDate";
            this.lblRegisterDate.Size = new System.Drawing.Size(67, 13);
            this.lblRegisterDate.TabIndex = 28;
            this.lblRegisterDate.Text = "Current Date";
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(26, 99);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(91, 43);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblRePassword
            // 
            this.lblRePassword.AutoSize = true;
            this.lblRePassword.Location = new System.Drawing.Point(264, 13);
            this.lblRePassword.Name = "lblRePassword";
            this.lblRePassword.Size = new System.Drawing.Size(97, 13);
            this.lblRePassword.TabIndex = 27;
            this.lblRePassword.Tag = "";
            this.lblRePassword.Text = "Re-Type Password";
            // 
            // txtRePassword
            // 
            this.txtRePassword.Location = new System.Drawing.Point(267, 29);
            this.txtRePassword.Name = "txtRePassword";
            this.txtRePassword.PasswordChar = '*';
            this.txtRePassword.Size = new System.Drawing.Size(114, 20);
            this.txtRePassword.TabIndex = 18;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(149, 13);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 25;
            this.lblPassword.Tag = "";
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(138, 31);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(122, 20);
            this.txtPassword.TabIndex = 17;
            // 
            // lblUserid
            // 
            this.lblUserid.AutoSize = true;
            this.lblUserid.Location = new System.Drawing.Point(22, 12);
            this.lblUserid.Name = "lblUserid";
            this.lblUserid.Size = new System.Drawing.Size(43, 13);
            this.lblUserid.TabIndex = 21;
            this.lblUserid.Tag = "";
            this.lblUserid.Text = "User ID";
            // 
            // txtUserid
            // 
            this.txtUserid.Location = new System.Drawing.Point(22, 29);
            this.txtUserid.Name = "txtUserid";
            this.txtUserid.Size = new System.Drawing.Size(110, 20);
            this.txtUserid.TabIndex = 15;
            // 
            // lblLastname
            // 
            this.lblLastname.AutoSize = true;
            this.lblLastname.Location = new System.Drawing.Point(142, 57);
            this.lblLastname.Name = "lblLastname";
            this.lblLastname.Size = new System.Drawing.Size(58, 13);
            this.lblLastname.TabIndex = 19;
            this.lblLastname.Text = "Last Name";
            // 
            // txtLastname
            // 
            this.txtLastname.Location = new System.Drawing.Point(138, 73);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(122, 20);
            this.txtLastname.TabIndex = 22;
            // 
            // lblfirstname
            // 
            this.lblfirstname.AutoSize = true;
            this.lblfirstname.Location = new System.Drawing.Point(22, 57);
            this.lblfirstname.Name = "lblfirstname";
            this.lblfirstname.Size = new System.Drawing.Size(57, 13);
            this.lblfirstname.TabIndex = 16;
            this.lblfirstname.Text = "First Name";
            // 
            // txtfirstname
            // 
            this.txtfirstname.Location = new System.Drawing.Point(22, 73);
            this.txtfirstname.Name = "txtfirstname";
            this.txtfirstname.Size = new System.Drawing.Size(108, 20);
            this.txtfirstname.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(393, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select a user ID";
            // 
            // lblBRight
            // 
            this.lblBRight.LinkColor = System.Drawing.Color.Navy;
            this.lblBRight.Location = new System.Drawing.Point(7, 259);
            this.lblBRight.Name = "lblBRight";
            this.lblBRight.Size = new System.Drawing.Size(337, 22);
            this.lblBRight.TabIndex = 65;
            this.lblBRight.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblBRight_LinkClicked);
            // 
            // lblDeleteUserMessage
            // 
            this.lblDeleteUserMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDeleteUserMessage.BackColor = System.Drawing.Color.MediumTurquoise;
            this.lblDeleteUserMessage.Location = new System.Drawing.Point(13, 9);
            this.lblDeleteUserMessage.Name = "lblDeleteUserMessage";
            this.lblDeleteUserMessage.Size = new System.Drawing.Size(324, 18);
            this.lblDeleteUserMessage.TabIndex = 62;
            this.lblDeleteUserMessage.Text = "Only Admin can change the User Informations";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(531, 21);
            this.label2.TabIndex = 64;
            this.label2.Text = "_________________________________________________________________________________" +
                "________";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel1.Location = new System.Drawing.Point(10, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(519, 36);
            this.panel1.TabIndex = 66;
            // 
            // DeletAuser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 280);
            this.Controls.Add(this.grpdeleteuser);
            this.Controls.Add(this.lblBRight);
            this.Controls.Add(this.lblDeleteUserMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(556, 319);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(556, 319);
            this.Name = "DeletAuser";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delete/Edit selected User ID";
            this.Load += new System.EventHandler(this.DeletAuser_Load);
            this.grpdeleteuser.ResumeLayout(false);
            this.grpdeleteuser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.Button btnReLogin;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.DateTimePicker txtCurrentDate;
        private System.Windows.Forms.GroupBox grpdeleteuser;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label lblRegisterDate;
        private System.Windows.Forms.Button btnDelete;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lblBRight;
        private System.Windows.Forms.Label lblDeleteUserMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
    }
}