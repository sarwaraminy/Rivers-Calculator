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
    public partial class EditRiverInforRpt : Form
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
        public EditRiverInforRpt()
        {
            InitializeComponent();
        }

        private void EditRiverInforRpt_Load(object sender, EventArgs e)
        {
            //get the login user id
            loginuser = File.ReadAllText(loginUserFile.loginUser).Trim();
            //load the country name to combo box
            loadCountryTbl();
            //setup the year calender
            editAdd_yearRpt.Format = DateTimePickerFormat.Custom;
            editAdd_yearRpt.CustomFormat = "yyyy";
            editAdd_yearRpt.ShowUpDown = true;
            //load Provence/district name for the selected country
            loadProvencesRpt();
            loadDistricRpt();
            //disable the text boxes until the find button is clicked
            editAdd_yearRpt.Enabled = false;
            txtEditRnameRpt.Enabled = false;
            txtEditStationRpt.Enabled = false;
            txtEditStationIDRpt.Enabled = false;
            txtEditCapacityRpt.Enabled = false;
            txtEditM3PerSRpt.Enabled = false;
        }
        //load countries
        private void loadCountryTbl()
        {
            cmbEditCntryRpt.DataSource = null;
            cmbEditCntryRpt.Items.Clear();
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
                cmbEditCntryRpt.DataSource = dt;
                cmbEditCntryRpt.DisplayMember = "CNAME";
                cmbEditCntryRpt.ValueMember = "COUNTRY";
                con.Close();
            }
            catch (Exception exobj)
            {
                MessageBox.Show("Error Location(loadCountryTbl())\n" + exobj.Message);
            }
        }
        //load district informatin to box 
        private void loadProvencesRpt()
        {
            //clear district combo box
            cmbEditRProvences.DataSource = null;
            cmbEditRProvences.Items.Clear();
            try
            {
                string selCntry = cmbEditCntryRpt.SelectedValue.ToString();
                con = new OleDbConnection(ConString.dbConString);
                con.Open();
                // First find the index and add the value of index
                string selectIndx = "SELECT DISTINCT PNAME, PCODE FROM RIVERSINFO WHERE COUNTRY='" + selCntry + "' AND USER_ID='" + loginuser + "'";
                oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "RIVERSINFO");
                dt = ds.Tables["RIVERSINFO"];
                cmbEditRProvences.DataSource = dt;
                cmbEditRProvences.DisplayMember = "PNAME";
                cmbEditRProvences.ValueMember = "PCODE";
                con.Close();
            }
            catch (Exception exobj)
            {
                MessageBox.Show(exobj.Message);
            }

        }

        //load district informatin to box 
        private void loadDistricRpt()
        {
            //clear district combo box
            cmbEditDistricRpt.DataSource = null;
            cmbEditDistricRpt.Items.Clear();
            try
            {
                string selCntry, selProve;
                if (cmbEditCntryRpt.SelectedIndex != -1) selCntry = cmbEditCntryRpt.SelectedValue.ToString();
                else selCntry = "";
                if (cmbEditRProvences.SelectedIndex != -1) selProve = cmbEditRProvences.SelectedValue.ToString();
                else selProve = "";
                con = new OleDbConnection(ConString.dbConString);
                con.Open();
                // First find the index and add the value of index
                string selectIndx = "SELECT DNAME,DCODE FROM RIVERSINFO WHERE COUNTRY='" + selCntry + "' AND USER_ID='" + loginuser 
                    + "' AND PCODE='" + selProve + "'";
                oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "RIVERSINFO");
                dt = ds.Tables["RIVERSINFO"];
                cmbEditDistricRpt.DataSource = dt;
                cmbEditDistricRpt.DisplayMember = "DNAME";
                cmbEditDistricRpt.ValueMember = "DCODE";
                con.Close();
            }
            catch (Exception exobj)
            {
                MessageBox.Show(exobj.Message);
            }

        }

        private void cmbEditCntryRpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load provences to combo box
            loadProvencesRpt();
        }

        private void cmbEditRProvences_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load districts to combo box
            loadDistricRpt();
        }

        private void cmbEditDistricRpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load rivers info
            loadRivers();
        }
        //load district informatin to box 
        private void loadRivers()
        {
            //clear district combo box
            cmbEditRiverRpt.DataSource = null;
            cmbEditRiverRpt.Items.Clear();
            try
            {
                string selCntry, selProve, selDist;
                if (cmbEditCntryRpt.SelectedIndex != -1) selCntry = cmbEditCntryRpt.SelectedValue.ToString();
                else selCntry = "";
                if (cmbEditRProvences.SelectedIndex != -1) selProve = cmbEditRProvences.SelectedValue.ToString();
                else selProve = "";
                if (cmbEditDistricRpt.SelectedIndex != -1) selDist = cmbEditDistricRpt.SelectedValue.ToString();
                else selDist = "";
                con = new OleDbConnection(ConString.dbConString);
                con.Open();
                // First find the index and add the value of index
                string selectIndx = "SELECT RIVER_ID, RNAME FROM RIVERSINFO WHERE DCODE='" + selDist + "' AND USER_ID='" + loginuser 
                    + "' AND PCODE='"+selProve+"' AND COUNTRY='"+selCntry+"'";
                oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "RIVERSINFO");
                dt = ds.Tables["RIVERSINFO"];
                cmbEditRiverRpt.DataSource = dt;
                cmbEditRiverRpt.DisplayMember = "RNAME";
                cmbEditRiverRpt.ValueMember = "RIVER_ID";
                con.Close();
            }
            catch (Exception exobj) { MessageBox.Show(exobj.Message); }

        }

        private void cmbEditRiverRpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load year from report table
            loadYearRpt();
        }
        //load year reports informatin to box 
        private void loadYearRpt()
        {
            //clear district combo box
            cmbEditRptYear.DataSource = null;
            cmbEditRptYear.Items.Clear();
            try
            {
                string selRiverID;
                if (cmbEditRiverRpt.SelectedIndex != -1)
                    selRiverID = cmbEditRiverRpt.SelectedValue.ToString();
                else
                    selRiverID = "";
                con = new OleDbConnection(ConString.dbConString);
                // First find the index and add the value of index
                string selectIndx = "SELECT YEAR FROM RIVERS WHERE RIVER_ID='" + selRiverID + "' AND USER_ID='" +loginuser+"'";
                oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "RIVERS");
                dt = ds.Tables["RIVERS"];
                cmbEditRptYear.DataSource = dt;
                cmbEditRptYear.DisplayMember = "YEAR";
                cmbEditRptYear.ValueMember = "YEAR";
            }
            catch (Exception exobj) { MessageBox.Show(exobj.Message); }

        }

        private void btnFindRpt_Click(object sender, EventArgs e)
        {
            //Enable the text boxes until the find button is clicked and the boxes is not null
            if (cmbEditDistricRpt.SelectedIndex != -1 && cmbEditRiverRpt.SelectedIndex != -1 && cmbEditRptYear.SelectedIndex != -1)
            {
                editAdd_yearRpt.Enabled = true;
                txtEditRnameRpt.Enabled = true;
                txtEditStationRpt.Enabled = true;
                txtEditStationIDRpt.Enabled = true;
                txtEditCapacityRpt.Enabled = true;
                txtEditM3PerSRpt.Enabled = true;
                try
                {
                    string selRiver = cmbEditRiverRpt.SelectedValue.ToString();
                    string selYear = cmbEditRptYear.SelectedValue.ToString();
                    con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    // First find the index and add the value of index
                    string selectIndx = "SELECT RIVER_ID, YEAR, RIVER_NAME, STATION_NAME, STATION_ID, CA, M3_PER_SECOND FROM RIVERS"+
                        " WHERE RIVER_ID='" + selRiver + "' AND YEAR='" + selYear + "' AND USER_ID='" + loginuser + "'";
                    oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                    dt = new DataTable();
                    ds = new DataSet();
                    oledbAdapter.Fill(ds, "RIVERSINFO");
                    dt = ds.Tables["RIVERSINFO"];
                    //conver the string to date
                    string sYearDate = dt.Rows[0]["YEAR"].ToString()+"/01/01";
                    DateTime sYear = DateTime.Parse(sYearDate);
                    editAdd_yearRpt.Text = sYear.ToString();
                    txtEditRnameRpt.Text = dt.Rows[0]["RIVER_NAME"].ToString();
                    txtEditStationRpt.Text = dt.Rows[0]["STATION_NAME"].ToString();
                    txtEditStationRpt.Text = dt.Rows[0]["STATION_NAME"].ToString();
                    txtEditStationIDRpt.Text = dt.Rows[0]["STATION_ID"].ToString();
                    txtEditCapacityRpt.Text = dt.Rows[0]["CA"].ToString();
                    txtEditM3PerSRpt.Text = dt.Rows[0]["M3_PER_SECOND"].ToString();
                    con.Close();
                }
                catch (Exception exobj) { MessageBox.Show(exobj.Message); }
            }
            else { MessageBox.Show("No data found!"); }
            
        }

        private void txtEditM3PerSRpt_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtEditM3PerSRpt.Text, "[^0-9]"))
            {
                txtEditM3PerSRpt.Text = txtEditM3PerSRpt.Text.Remove(txtEditM3PerSRpt.Text.Length - 1);
            }
            else {  }
        }

        private void txtEditCapacityRpt_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtEditCapacityRpt.Text, "[^0-9]"))
            {
                txtEditCapacityRpt.Text = txtEditCapacityRpt.Text.Remove(txtEditCapacityRpt.Text.Length - 1);
            }
            else { }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //update the selected record
            // Check the updated fields are not empty
            if (editAdd_yearRpt.Text == "") { MessageBox.Show("Please enter a year"); editAdd_yearRpt.Focus(); }
            else if (txtEditRnameRpt.Text == "") { MessageBox.Show("Please enter river name"); txtEditRnameRpt.Focus(); }
            else if (txtEditStationRpt.Text == "") { MessageBox.Show("Please enter station name"); txtEditStationRpt.Focus(); }
            else if (txtEditStationIDRpt.Text == "") { MessageBox.Show("Please enter station ID"); txtEditStationIDRpt.Focus(); }
            else if (txtEditCapacityRpt.Text == "") { MessageBox.Show("Please enter a capacity "); txtEditCapacityRpt.Focus(); }
            else if (txtEditM3PerSRpt.Text == "") { MessageBox.Show("Please enter a m3/sec"); txtEditM3PerSRpt.Focus(); }
            // else proced the new task
            else
            {
                string selCntry = cmbEditCntryRpt.SelectedValue.ToString();
                string selProve = cmbEditRProvences.SelectedValue.ToString();
                string selDist = cmbEditDistricRpt.SelectedValue.ToString();
                string selRivID = cmbEditRiverRpt.SelectedValue.ToString();
                string selYear = editAdd_yearRpt.Value.Date.Year.ToString();
                try
                {
                    //validate that the user is entered valid river Name
                    con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    // First find the index and add the value of index
                    string selectIndx = "SELECT RIVER_ID FROM RIVERSINFO" +
                        " WHERE COUNTRY='" + selCntry + "' AND DCODE='" + selDist + "' AND RIVER_ID='" + selRivID +
                        "' AND USER_ID='" + loginuser + "' AND PCODE='" + selProve + "'";
                    oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                    dt = new DataTable();
                    ds = new DataSet();
                    oledbAdapter.Fill(ds, "RIVERSINFO");
                    dt = ds.Tables["RIVERSINFO"];
                    if (dt.Rows.Count > 0)
                    {
                        //Update the records
                        con = new OleDbConnection(ConString.dbConString);
                        con.Open();
                        cmd = new OleDbCommand();

                        string cmdUpdate = "UPDATE RIVERS " +
                                           " SET RIVER_NAME='" + txtEditRnameRpt.Text + "'" +
                                           ",STATION_NAME='" + txtEditStationRpt.Text + "'" +
                                           ",STATION_ID='" + txtEditStationIDRpt.Text + "'" +
                                           ",CA='" + txtEditCapacityRpt.Text + "'" +
                                           ",M3_PER_SECOND='" + txtEditM3PerSRpt.Text + "'" +
                                           " WHERE RIVER_ID='" + selRivID + "' AND YEAR='" + selYear + "' AND USER_ID='" + loginuser + "'";

                        cmd.CommandText = cmdUpdate;
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Record updated successfully!", "Update a record ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        con.Close();
                    }
                    else { MessageBox.Show("Update unsuccessful!"); }
                }
                catch (Exception exobj) { MessageBox.Show(exobj.Message); }
                

            }
        }

        private void btnReLogin_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // first check if the user make sures that they want to delete.
            // firs appear a confirm box for ensuring of user
            // get the selected item from list box
            string selRiver = cmbEditRiverRpt.SelectedValue.ToString();
            string selYear = cmbEditRptYear.Text;
            try
            {
                if (MessageBox.Show(" Are you sure the record for year: '"+selYear +"' and for river: '"+cmbEditRiverRpt.Text+"' to be delete?", "Confirm Delete",
                      MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    cmd = new OleDbCommand();
                    string cmdDelete = "DELETE FROM RIVERS WHERE RIVER_ID='" + selRiver +
                        "' AND YEAR='" + selYear + "' AND USER_ID='" + loginuser + "'";
                    cmd.CommandText = cmdDelete;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("A record for year '"+selYear+"' and for river: '"+cmbEditRiverRpt.Text+"' Deleted.", "Delete Information"
                               , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //re-load the combo boxes
                    loadRivers();
                }
            }
            catch (Exception exobj) { MessageBox.Show(exobj.Message); }
        }
    }
}
