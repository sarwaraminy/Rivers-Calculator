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
    public partial class RiversInfo : Form
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
        public RiversInfo()
        {
            InitializeComponent();
        }

        private void RiversInfo_Load(object sender, EventArgs e)
        {
            loginuser = File.ReadAllText(loginUserFile.loginUser).Trim();
            //load current date to the text box
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

            //load the country name to combo box
            loadCountryTbl();
            
        }
        //load countries
        private void loadCountryTbl()
        {
            cmbAddCntry.DataSource = null;
            cmbAddCntry.Items.Clear();
            try
            {
                con = new OleDbConnection(ConString.dbConString);
                // First find the index and add the value of index
                string loadRiverQ = "SELECT COUNTRY, CNAME FROM COUNTRYTBL WHERE USER_ID='" + loginuser + "'";
                con.Open();
                oledbAdapter = new OleDbDataAdapter(loadRiverQ, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "COUNTRYTBL");
                dt = ds.Tables["COUNTRYTBL"];
                cmbAddCntry.DataSource = dt;
                cmbAddCntry.DisplayMember = "CNAME";
                cmbAddCntry.ValueMember = "COUNTRY";
                con.Close();
            }
            catch (Exception exobj)
            {
                MessageBox.Show("Error Location(loadCountryTbl())\n" + exobj.Message);
            }
        }

        private void lblBRight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/sarwar.aminy");
        }

        private void btnAddRiver_Click(object sender, EventArgs e)
        {
            //check there is no empty record is inserted
            if (txtProvID.Text == "") { MessageBox.Show("Please Enter Provence Code"); txtProvID.Focus(); }
            else if (txtProvName.Text == "") { MessageBox.Show("Please Enter Provence name"); txtProvName.Focus(); }
            else if (txtAdcode.Text == "") { MessageBox.Show("Please Enter District Code"); txtAdcode.Focus(); }
            else if (txtAdname.Text == "") { MessageBox.Show("Please enter the Distict Name"); txtAdname.Focus(); }
            else if (txtArcode.Text == "") { MessageBox.Show("Please enter the river code"); txtArcode.Focus(); }
            else if (txtArname.Text == "") { MessageBox.Show("Please enter the river name"); txtArname.Focus(); }
            else
            {
                //Get the country information
                string cntry_code = cmbAddCntry.SelectedValue.ToString();
                string cname = cmbAddCntry.Text;
                try
                {
                    //-----------------------------check for duplicate record
                    string dupQ = "SELECT COUNTRY,PCODE,DCODE,RIVER_ID FROM RIVERSINFO WHERE COUNTRY='" + cntry_code +
                        "' AND DCODE='" + txtAdcode.Text + "' AND USER_ID='" + loginuser + "' AND RIVER_ID='"+txtArcode.Text+
                        "' AND PCODE='" + txtProvID.Text + "'";
                    con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    cmd = new OleDbCommand();
                    oledbAdapter = new OleDbDataAdapter(dupQ, con);
                    ds = new DataSet();
                    dt = new DataTable();
                    oledbAdapter.Fill(ds, "RIVERSINFO");
                    dt = ds.Tables["RIVERSINFO"];
                    if (dt.Rows.Count == 0)
                    {
                        // - Insert the value to the database
                        string insertRiverInfoQ = "INSERT INTO RIVERSINFO VALUES('" + cntry_code + "','" + txtProvID.Text+"','" + txtAdcode.Text + "','" +
                                      txtArcode.Text + "','" + cname + "','" + txtProvName.Text + "','" + txtAdname.Text + "','" + txtArname.Text + "','" +
                                      loginuser + "','" + txtRegisterDate.Text + "');";

                        con = new OleDbConnection(ConString.dbConString);
                        con.Open();
                        cmd = new OleDbCommand();
                        cmd.Connection = con;
                        cmd.CommandText = insertRiverInfoQ;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("A record is successfully added!");
                        //Clear the boxes
                        txtAdcode.Text = "";
                        txtAdname.Text = "";
                        txtArcode.Text = "";
                        txtArname.Text = "";
                        txtProvName.Text = "";
                        txtProvID.Text = "";
                    }
                    else { MessageBox.Show("The Record is already exist with the value of: \n"+"Country: "+cname+"\n District code: "+txtAdcode.Text+"\n River ID: "+txtArcode.Text); }
                }
                catch (Exception exob) { MessageBox.Show(exob.Message); }
            }
            
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            //Clear the boxes
            txtAdcode.Text = "";
            txtAdname.Text = "";
            txtArcode.Text = "";
            txtArname.Text = "";
        }

        private void btnRestartApp_Click(object sender, System.EventArgs e)
        {
            Application.Restart();
        }
    }
}
