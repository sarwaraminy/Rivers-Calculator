using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace RiversCalculator
{
    public partial class DeletAuser : Form
    {
        //create the instance for connection string and login user path
        ConnectionString ConString = new ConnectionString();
        ConnectionString loginUserFile = new ConnectionString();
        string loginuser;

        //Create some variables
        OleDbConnection con;
        OleDbDataAdapter oledbAdapter;
        DataTable dt;
        DataSet ds;
        OleDbCommand cmd;

        public DeletAuser()
        {
            InitializeComponent();
        }

        private void DeletAuser_Load(object sender, EventArgs e)
        {
            loginuser = File.ReadAllText(loginUserFile.loginUser).Trim();
            // load the existence users during the loading window
            ExistenceUsers();
            // disable the boxes till the search button is clicked
            txtUserid.Enabled = false;
            txtPassword.Enabled = false;
            txtRePassword.Enabled = false;
            txtfirstname.Enabled = false;
            txtLastname.Enabled = false;
            txtCurrentDate.Enabled = false;
        }
        // load the existence users from user table
        private void ExistenceUsers()
        {
            try
            {
                lstUsers.Items.Clear();
                string allUsers = " SELECT USER_ID FROM USERINFO WHERE USER_ID <> 'admin';";
                con = new OleDbConnection(ConString.dbConString);
                oledbAdapter = new OleDbDataAdapter(allUsers, con);
                ds = new DataSet();
                dt = new DataTable();
                oledbAdapter.Fill(ds, "USERINFO");

                dt = ds.Tables["USERINFO"];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstUsers.Items.Add(dt.Rows[i]["USER_ID"].ToString());
                }


            }
            catch { }

        }

        private void btnFind_Click(object sender, System.EventArgs e)
        {
            try
            {
                con = new OleDbConnection(ConString.dbConString);
                con.Open();
                cmd = new OleDbCommand();

                // - Get the totals and put it to the total box and recieved box
                string totVal = "SELECT USER_ID,PASS,FIRST_NAME,LAST_NAME,Register_Date FROM USERINFO "
                          + " WHERE USER_ID='" + lstUsers.SelectedItem.ToString() + "'";

                oledbAdapter = new OleDbDataAdapter(totVal, con);
                ds = new DataSet();
                dt = new DataTable();
                oledbAdapter.Fill(ds, "USERINFO");
                dt = ds.Tables["USERINFO"];
                // decrypt the password
                string decryptPass = Encoding.Unicode.GetString(Convert.FromBase64String(dt.Rows[0]["PASS"].ToString()));

                txtUserid.Text = dt.Rows[0]["USER_ID"].ToString();
                txtPassword.Text = decryptPass;
                txtRePassword.Text = txtPassword.Text;
                txtfirstname.Text = dt.Rows[0]["FIRST_NAME"].ToString();
                txtLastname.Text = dt.Rows[0]["LAST_NAME"].ToString();
                txtCurrentDate.Text = dt.Rows[0]["Register_Date"].ToString();

                // enable the boxes for editing
                txtUserid.Enabled = true;
                txtPassword.Enabled = true;
                txtRePassword.Enabled = true;
                txtfirstname.Enabled = true;
                txtLastname.Enabled = true;
                txtCurrentDate.Enabled = true;
            }
            catch { MessageBox.Show("Please select a User ID"); }
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            var year = txtCurrentDate.Value.Date.Year;
            var month = txtCurrentDate.Value.Date.Month;
            var day = txtCurrentDate.Value.Date.Day;
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
            string InsertDate = month0 + "/" + day0 + "/" + year.ToString();
            // check the fields are not empty
            // first check the boxes are not empty
            if (txtUserid.Text == "") { MessageBox.Show("Please enter User ID"); txtUserid.Focus(); }
            else if (txtPassword.Text == "") { MessageBox.Show("Please give a new password"); txtPassword.Focus(); }
            else if (txtRePassword.Text == "") { MessageBox.Show("Please re-type the new password"); txtRePassword.Focus(); }
            else if (txtfirstname.Text == "") { MessageBox.Show("Please enter the First Name"); txtfirstname.Focus(); }
            // Check either the password and repassoer are same
            else if (txtPassword.Text != txtRePassword.Text)
            {
                MessageBox.Show("Passwords are not matched!");
                txtPassword.Text = "";
                txtRePassword.Text = "";
                txtPassword.Focus();
            }
            else
            {
                try
                {
                    // encript the password
                    string encryptPass = Convert.ToBase64String(Encoding.Unicode.GetBytes(txtPassword.Text));
                    // update the edited information about users by admin
                    string updateUsr = " UPDATE USERINFO SET USER_ID='" + txtUserid.Text + "'" +
                                       " ,PASS='" + encryptPass + "'" +
                                       " ,FIRST_NAME='" + txtfirstname.Text + "'" +
                                       " ,LAST_NAME='" + txtLastname.Text + "'" +
                                       " ,Register_Date='" + InsertDate + "'" +
                                       " WHERE USER_ID='" + lstUsers.SelectedItem.ToString() + "'";

                    con = new OleDbConnection(ConString.dbConString);
                    cmd = new OleDbCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = updateUsr;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("The selected User ID updated successfully", "Edit User ID",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // refresh the listbox
                    ExistenceUsers();
                }
                catch { MessageBox.Show("Please select a user ID"); }
            }
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            // first check if the user make sures that they want to delete.
            // firs appear a confirm box for ensuring of user
            // get the selected item from list box
            try
            {
                if (MessageBox.Show("Are you sure the User ID: '" + lstUsers.SelectedItem.ToString() + "' to be delete? ", "Confirm Delete",
                      MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    cmd = new OleDbCommand();
                    string cmdDelete = "DELETE FROM USERINFO WHERE USER_ID='" + lstUsers.SelectedItem.ToString() + "'";
                    cmd.CommandText = cmdDelete;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("The User ID '" + lstUsers.SelectedItem.ToString() + "' successfully delted!", "Delete Information"
                               , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // refresh the listbox
                    ExistenceUsers();
                }
            }
            catch { MessageBox.Show("No User ID is selected!"); }
        }

        private void btnDeleteAll_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete all user ID?", "Confirm All User ID Delete",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    cmd = new OleDbCommand();

                    string cmdDelete = "DELETE FROM USERINFO WHERE USER_ID <> 'admin';";
                    cmd.CommandText = cmdDelete;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("All user IDs deleted successfully", "All User Deletion information"
                               , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // reload the application
                    Application.Restart();
                }
            }
            catch { }
        }

        private void btnReLogin_Click(object sender, System.EventArgs e)
        {
            Application.Restart();
        }

        private void lblBRight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/sarwar.aminy");
        }
    }
}
