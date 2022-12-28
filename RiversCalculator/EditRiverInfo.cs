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
    public partial class EditRiverInfo : Form
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

        public EditRiverInfo()
        {
            InitializeComponent();
        }

        private void EditRiverInfo_Load(object sender, EventArgs e)
        {
            //get the login user id
            loginuser = File.ReadAllText(loginUserFile.loginUser).Trim();
            lblBRight.Text = ConString.copyRight;
            //load the country name to combo box
            loadCountryTbl();
            //load Provence/district name for the selected country
            loadProvences();
            loadDistric();
            //disable the text boxes until the find button is clicked
            txtEditDcode.Enabled = false;
            txtEditDname.Enabled = false;
            txtEditRcode.Enabled = false;
            txtEditRname.Enabled = false;
        }
        //load countries
        private void loadCountryTbl()
        {
            cmbEditCntry.DataSource = null;
            cmbEditCntry.Items.Clear();
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
                cmbEditCntry.DataSource = dt;
                cmbEditCntry.DisplayMember = "CNAME";
                cmbEditCntry.ValueMember = "COUNTRY";
                con.Close();
            }
            catch (Exception exobj)
            {
                MessageBox.Show("Error Location(loadCountryTbl())\n" + exobj.Message);
            }
        }

        private void cmbEditCntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load the related country Provence to Provence combo box
            loadProvences();
        }

        //load district informatin to box 
        private void loadProvences()
        {
            //clear district combo box
            cmbEditProvence.DataSource = null;
            cmbEditProvence.Items.Clear();
            try
            {
                string selCntry = cmbEditCntry.SelectedValue.ToString();
                con = new OleDbConnection(ConString.dbConString);
                // First find the index and add the value of index
                string selectIndx = "SELECT DISTINCT PNAME, PCODE FROM RIVERSINFO WHERE COUNTRY='" + selCntry + "'";
                oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "RIVERSINFO");
                dt = ds.Tables["RIVERSINFO"];
                cmbEditProvence.DataSource = dt;
                cmbEditProvence.DisplayMember = "PNAME";
                cmbEditProvence.ValueMember = "PCODE";
            }
            catch (Exception exobj)
            {
                MessageBox.Show(exobj.Message);
            }

        }

        //load district informatin to box 
        private void loadDistric()
        {
            //clear district combo box
            cmbEditDistric.DataSource = null;
            cmbEditDistric.Items.Clear();
            try
            {
                string selCntry, selProve;
                if (cmbEditCntry.SelectedIndex != -1) selCntry = cmbEditCntry.SelectedValue.ToString();
                else selCntry = "";
                if (cmbEditProvence.SelectedIndex != -1) selProve = cmbEditProvence.SelectedValue.ToString();
                else selProve = "";
                con = new OleDbConnection(ConString.dbConString);
                // First find the index and add the value of index
                string selectIndx = "SELECT DNAME,DCODE FROM RIVERSINFO WHERE COUNTRY='" + selCntry + "' AND PCODE='" + selProve + "'";
                oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "RIVERSINFO");
                dt = ds.Tables["RIVERSINFO"];
                cmbEditDistric.DataSource = dt;
                cmbEditDistric.DisplayMember = "DNAME";
                cmbEditDistric.ValueMember = "DCODE";
            }
            catch (Exception exobj)
            {
                MessageBox.Show(exobj.Message);
            }

        }

        private void cmbEditProvence_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDistric();
        }

        private void cmbEditDistric_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load rivers info
            loadRivers();
        }
        //load district informatin to box 
        private void loadRivers()
        {
            //clear district combo box
            cmbEditRiver.DataSource = null;
            cmbEditRiver.Items.Clear();
            try
            {
                string selDist;
                if (cmbEditDistric.SelectedIndex != -1)
                    selDist = cmbEditDistric.SelectedValue.ToString();
                else
                    selDist = "";
                con = new OleDbConnection(ConString.dbConString);
                // First find the index and add the value of index
                string selectIndx = "SELECT RIVER_ID, RNAME FROM RIVERSINFO WHERE DCODE='" + selDist + "'";
                oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "RIVERSINFO");
                dt = ds.Tables["RIVERSINFO"];
                cmbEditRiver.DataSource = dt;
                cmbEditRiver.DisplayMember = "RNAME";
                cmbEditRiver.ValueMember = "RIVER_ID";
            }
            catch (Exception exobj)
            {
                MessageBox.Show(exobj.Message);
            }

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //Enable the text boxes until the find button is clicked
            txtEditDcode.Enabled = true;
            txtEditDname.Enabled = true;
            txtEditRcode.Enabled = true;
            txtEditRname.Enabled = true;
            try
            {
                string selCntry = cmbEditCntry.SelectedValue.ToString();
                string selProve = cmbEditProvence.SelectedValue.ToString();
                string selDist = cmbEditDistric.SelectedValue.ToString();
                string selRiver = cmbEditRiver.SelectedValue.ToString();
                con = new OleDbConnection(ConString.dbConString);
                // First find the index and add the value of index
                string selectIndx = "SELECT DCODE, DNAME, RIVER_ID, RNAME FROM RIVERSINFO WHERE COUNTRY='" + selCntry +
                    "' AND DCODE='" + selDist + "' AND RIVER_ID='" + selRiver + "' AND PCODE='" + selProve + "' AND USER_ID='" + loginuser+"'";
                oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                con.Open();
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "RIVERSINFO");
                dt = ds.Tables["RIVERSINFO"];
                txtEditDcode.Text = dt.Rows[0]["DCODE"].ToString();
                txtEditDname.Text = dt.Rows[0]["DNAME"].ToString();
                txtEditRcode.Text = dt.Rows[0]["RIVER_ID"].ToString();
                txtEditRname.Text = dt.Rows[0]["RNAME"].ToString();
                con.Close();
            }
            catch (Exception exobj) { MessageBox.Show(exobj.Message); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //update the selected record
            // Check the updated fields are not empty
            if (txtEditDcode.Text == "") { MessageBox.Show("Please enter the District ID"); txtEditDcode.Focus(); }
            else if (txtEditDname.Text == "") { MessageBox.Show("Please Enter the District name"); txtEditDname.Focus(); }
            else if (txtEditRcode.Text == "") { MessageBox.Show("Please enter the river ID"); txtEditRcode.Focus(); }
            else if (txtEditRname.Text == "") { MessageBox.Show("Please enter the river Name"); txtEditRname.Focus(); }
            // else proced the new task
            else
            {
                string selCntry = cmbEditCntry.SelectedValue.ToString();
                string selProve = cmbEditProvence.SelectedValue.ToString();
                string selDist = cmbEditDistric.SelectedValue.ToString();
                string selRiver = cmbEditRiver.SelectedValue.ToString();
                try
                {
                    con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    cmd = new OleDbCommand();

                    string cmdUpdate = "UPDATE RIVERSINFO " +
                                       " SET DCODE='" + txtEditDcode.Text + "'" +
                                       ",RIVER_ID='" + txtEditRcode.Text + "'" +
                                       ",DNAME='" + txtEditDname.Text + "'" +
                                       ",RNAME='" + txtEditRname.Text + "'" +
                                       " WHERE COUNTRY='" + selCntry + "' AND DCODE='" + selDist + "' AND RIVER_ID='"+
                                       selRiver+"' AND PCODE='"+selProve+"' AND USER_ID='"+loginuser+"'";

                    cmd.CommandText = cmdUpdate;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("River information updated successfully", "Update a record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                    //re-load the combo boxes
                    loadProvences();
                    loadDistric();
                    loadRivers();
                }
                catch (Exception exobj) { MessageBox.Show(exobj.Message); }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // first check if the user make sures that they want to delete.
            // firs appear a confirm box for ensuring of user
            // get the selected item from list box
            string selCntry = cmbEditCntry.SelectedValue.ToString();
            string selProve = cmbEditProvence.SelectedValue.ToString();
            string selDist = cmbEditDistric.SelectedValue.ToString();
            string selRiver = cmbEditRiver.SelectedValue.ToString();
            string selRiverText = cmbEditRiver.Text;
            try
            {
                if (MessageBox.Show("Are you sure the record: '" + selRiverText + "' to be delete?", "Confirm Delete",
                      MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    cmd = new OleDbCommand();
                    string cmdDelete = "DELETE FROM RIVERSINFO WHERE COUNTRY='" + selCntry + 
                        "' AND DCODE='" + selDist + "' AND RIVER_ID='"+selRiver+"' AND PCODE='"+selProve+"' AND USER_ID='"+loginuser+"'";
                    cmd.CommandText = cmdDelete;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("The river record '" + selRiverText + "' Successfully deleted!", "Information for delete"
                               , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //re-load the combo boxes
                    loadDistric();
                    loadRivers();
                }
            }
            catch (Exception exobj) { MessageBox.Show(exobj.Message); }
        }

        private void btnReLogin_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void lblBRight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/sarwar.aminy");
        }
    }
}
