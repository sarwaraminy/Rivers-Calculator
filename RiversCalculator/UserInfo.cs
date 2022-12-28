using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace RiversCalculator
{
    public partial class UserInfo : Form
    {
        //create the instance for connection string and login user path
        ConnectionString ConString = new ConnectionString();

        public UserInfo()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
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
            string StartDateReg = month0.ToString() + "/" + day0.ToString() + "/" + year.ToString();
            var eYear = dateAndTime.Year+1;
            string endDateReg = month0.ToString() + "/" + day0.ToString() + "/" + eYear.ToString();
            // first check the boxes are not empty
            if (txtfirstname.Text == "") { MessageBox.Show("Please enter your first name"); txtfirstname.Focus(); }
            else if (txtUserid.Text == "") { MessageBox.Show("Please enter a user id"); txtUserid.Focus(); }
            else if (txtPassword.Text == "") { MessageBox.Show("Please enter a password"); txtPassword.Focus(); }
            else if (txtRePassword.Text == "") { MessageBox.Show("Please re-type your password"); txtRePassword.Focus(); }
            // Check either the password and repassoer are same
            else if (txtPassword.Text != txtRePassword.Text)
            {
                MessageBox.Show(" Password is not matched!");
                txtPassword.Text = "";
                txtRePassword.Text = "";
                txtPassword.Focus();
            }
            // else register the user information to the database
            else
            {
                try
                {
                    // - Insert the value to the database
                    // encript the password
                    string encryptPass = Convert.ToBase64String(Encoding.Unicode.GetBytes(txtPassword.Text));
                    string InsertHome = "INSERT INTO USERINFO VALUES('" + txtUserid.Text + "','" + encryptPass + "','" +
                                  txtfirstname.Text + "','" + txtLastname.Text + "','" + txtRegisterDate.Text + "');";

                    OleDbConnection con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = con;
                    cmd.CommandText = InsertHome;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //-----------------
                    string registerQ = "INSERT INTO LICENSETBL VALUES('" + txtUserid.Text + "','" + StartDateReg + "','" +
                                  endDateReg + "');";

                    con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    cmd = new OleDbCommand();
                    cmd.Connection = con;
                    cmd.CommandText = registerQ;
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("A user id successfully registerd to system");
                    this.Dispose();
                }
                catch
                {
                    MessageBox.Show("The user id already exist!");
                    txtUserid.Text = "";
                    txtPassword.Text = "";
                    txtRePassword.Text = "";
                    txtfirstname.Text = "";
                    txtLastname.Text = "";
                    txtUserid.Focus();
                }
            }
        }

        private void UserInfo_Load(object sender, EventArgs e)
        {
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
            txtRegisterDate.Text = month0.ToString() + "/" + day0.ToString() + "/" + year.ToString();
            lblBRight.Text = ConString.copyRight;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // clear the boxes
            txtUserid.Text = "";
            txtPassword.Text = "";
            txtRePassword.Text = "";
            txtfirstname.Text = "";
            txtLastname.Text = "";
            txtUserid.Focus();
        }
        // submit when the user is pressing the enter key
        private void txtLastname_TextChanged(object sender, EventArgs e)
        {
            this.txtLastname.KeyPress += new KeyPressEventHandler(KeyPress);
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)13) && (txtUserid.Text != "") && (txtPassword.Text != ""))
                btnRegister.PerformClick();
        }

        private void lblBRight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/sarwar.aminy");
        }
    }
}
