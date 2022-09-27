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
using System.Runtime.InteropServices;

namespace RiversCalculator
{
    public partial class Login : Form
    {
        //create the instance for connection string and login user path
        ConnectionString ConString = new ConnectionString();
        ConnectionString loginUserFile = new ConnectionString();

        string connectionString;
        public Login()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        { 
            if (txtUserID.Text == "")
            {
                MessageBox.Show("Please enter a valid User ID");
                txtUserID.Focus();
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter a valid Password");
                txtPassword.Focus();
            }
            else
            {
                connectionString = ConString.dbConString;
                OleDbConnection con = new OleDbConnection(connectionString);
                con.Open();

                OleDbCommand cmd = new OleDbCommand();

                // - Get the totals and put it to the total box and recieved box

                string totVal = "SELECT USER_ID,PASS,FIRST_NAME,LAST_NAME FROM USERINFO " +
                          " WHERE USER_ID='" + txtUserID.Text + "'";

                OleDbDataAdapter oledbAdapter = new OleDbDataAdapter(totVal, con);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                oledbAdapter.Fill(ds, "USERINFO");
                dt = ds.Tables["USERINFO"];
                // check if the user is typed right or wrong userid and password
                try
                {
                    // decrypt the password
                    string decryptPass = Encoding.Unicode.GetString(Convert.FromBase64String(dt.Rows[0]["PASS"].ToString()));
                    string firstName = dt.Rows[0]["FIRST_NAME"].ToString();
                    string lastName = dt.Rows[0]["LAST_NAME"].ToString();

                    if (decryptPass == txtPassword.Text)
                    {
                        // Capture the login user id
                        string DbTypeSelection = loginUserFile.loginUser;
                        string delimiter = "";

                        // - -------------------create the cuptured text-------------------
                        string[][] Houtput = new string[][]{
                            new string[]{txtUserID.Text.ToLower()}
                            };
                        int length = Houtput.GetLength(0);
                        StringBuilder sb = new StringBuilder();
                        for (int index = 0; index < length; index++)
                            sb.AppendLine(string.Join(delimiter, Houtput[index]));

                        // - ---------------------Create the files------------------------
                        System.IO.File.WriteAllText(DbTypeSelection, sb.ToString());
                        // --------------------------------------------------------

                        MessageBox.Show("Welcome "+firstName+" "+ lastName, "Login Success!!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose(); // close the form
                    }
                    else
                    {
                        MessageBox.Show("Your User ID or Password is incorrect", "Login Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserID.Text = "";
                        txtPassword.Text = "";
                        txtUserID.Focus();
                    }
                }
                catch
                {
                    MessageBox.Show("Your User ID or Password is incorrect", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserID.Text = "";
                    txtPassword.Text = "";
                    txtUserID.Focus();
                }
            }
        }

        private void lblBRight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/sarwar.aminy");
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.txtPassword.KeyPress += new KeyPressEventHandler(KeyPress);
        }
        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)13) && (txtPassword.Text != "") && (txtUserID.Text != ""))
                btnSubmit.PerformClick();
        }

        // check is the window is open or not
        public static Form IsFormAlreadyOpen(Type FormType)
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == FormType)
                    return OpenForm;
            }

            return null;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            ChangeUserPass change = null;
            if ((change = (ChangeUserPass)IsFormAlreadyOpen(typeof(ChangeUserPass))) == null)
            {
                change = new ChangeUserPass();
                change.Show();
            }
            else
            {
                MessageBox.Show("the Change password dialog is already open!");
            }
        }

        private void btnCreateNewAccount_Click(object sender, EventArgs e)
        {
            UserInfo usrInfo = null;
            if ((usrInfo = (UserInfo)IsFormAlreadyOpen(typeof(UserInfo))) == null)
            {
                usrInfo = new UserInfo();
                usrInfo.Show();
            }
            else
            {
                MessageBox.Show("The New User ID dialog is already open!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // disable the colosing x button
        private const uint SC_CLOSE = 0xF060;
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, bool revert);
        [DllImport("user32.dll")]
        private static extern bool DeleteMenu(IntPtr hMenu, uint position, uint flags);

        private void Login_Load(object sender, EventArgs e)
        {
            IntPtr hwnd = GetSystemMenu(this.Handle, false);

            DeleteMenu(hwnd, SC_CLOSE, 0); 
        }


    }
}
