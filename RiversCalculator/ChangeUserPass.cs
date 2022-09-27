using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace RiversCalculator
{
    public partial class ChangeUserPass : Form
    {
        //create the instance for connection string and login user path
        ConnectionString ConString = new ConnectionString();

        public ChangeUserPass()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Check the updated fields are not empty
            if (txtUserid.Text == "") { MessageBox.Show("Please enter the User ID"); txtUserid.Focus(); }
            else if (txtOldPassword.Text == "") { MessageBox.Show("Please enter the old password"); txtOldPassword.Focus(); }
            else if (txtNewPassword.Text == "") { MessageBox.Show("Please enter the new password"); txtNewPassword.Focus(); }
            else if (txtRePassword.Text == "") { MessageBox.Show("Please re type the new password"); txtRePassword.Focus(); }
            else if (txtNewPassword.Text != txtRePassword.Text) { MessageBox.Show("The new password and re-type password is not same"); txtNewPassword.Text = ""; txtRePassword.Text = ""; txtNewPassword.Focus(); }
            // else proced the new task
            else
            {
                // ----------------------pass current date without time--------------------------
                // - at the first get each portion(Year, Month, Day) -
                // - then make the format of the date as MM/DD/YYYY -
                // - if the number of month or day is less than 10 then concatnate a 0 in the start
                var dateAndTime = DateTime.Now;
                var year = dateAndTime.Year;
                var month = dateAndTime.Month;
                var day = dateAndTime.Day;
                string day0 = "";
                string month0 = "";
                if (day < 10)
                    day0 = '0' + day.ToString();
                else
                    day0 = day.ToString();
                if (month < 10)
                    month0 = "0" + month.ToString();
                else
                    month0 = month.ToString();
                string editDate = month0.ToString() + "/" + day0.ToString() + "/" + year.ToString();
                // firs appear a confirm box for ensuring of user
                // check the selected user name is in the database or not
                OleDbConnection conS = new OleDbConnection(ConString.dbConString);
                conS.Open();

                OleDbCommand cmdS = new OleDbCommand();

                // - Get the totals and put it to the total box and recieved box
                string totValS = "SELECT USER_ID, PASS FROM USERINFO " +
                   " WHERE USER_ID='" + txtUserid.Text + "'";

                OleDbDataAdapter oledbAdapter = new OleDbDataAdapter(totValS, conS);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                oledbAdapter.Fill(ds, "USERINFO");
                dt = ds.Tables["USERINFO"];

                // decrypt the password
                string decryptPass = Encoding.Unicode.GetString(Convert.FromBase64String(dt.Rows[0]["PASS"].ToString()));
                string encryptPass = Convert.ToBase64String(Encoding.Unicode.GetBytes(txtOldPassword.Text));
                string encryptPassNew = Convert.ToBase64String(Encoding.Unicode.GetBytes(txtNewPassword.Text));
                string userid = dt.Rows[0]["USER_ID"].ToString();
                try
                {
                    if (userid != "" && decryptPass == txtOldPassword.Text)
                    {
                        //------------------------------------end check username-----------------------------------
                        if (MessageBox.Show(" Are you sure that your password change?", "Confirm changes",
                            MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {

                            // Stablish connection
                            OleDbConnection con = new OleDbConnection(ConString.dbConString);
                            con.Open();
                            OleDbCommand cmd = new OleDbCommand();

                            string cmdUpdate = "UPDATE USERINFO " +
                                               " SET USER_ID='" + txtUserid.Text + "'" +
                                               ",PASS='" + encryptPassNew + "'" +
                                               " WHERE USER_ID='" + txtUserid.Text + "' AND PASS='" + encryptPass + "'";

                            cmd.CommandText = cmdUpdate;
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Your password successfully changed!", "Edit User ID ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Dispose();
                        }
                    }
                }
                catch { MessageBox.Show("The user ID you entered deosn't exist in the system: "+txtUserid.Text); }
            }
        }

        // clear the boxes
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserid.Text = "";
            txtNewPassword.Text = "";
            txtOldPassword.Text = "";
            txtRePassword.Text = "";
        }

        private void lblBRight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/sarwar.aminy");
        }

    }
}
