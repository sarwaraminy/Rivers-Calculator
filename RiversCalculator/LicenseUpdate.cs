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
    public partial class LicenseUpdate : Form
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

        public LicenseUpdate()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // - at the first get each portion(Year, Month, Day) -
            // - then make the format of the date as MM/DD/YYYY -
            // - if the number of month or day is less than 10 then concatnate a 0 in the start
            var year = fromDateLice.Value.Year;
            var month = fromDateLice.Value.Month;
            var day = fromDateLice.Value.Day;
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
            string fromDateload = month0.ToString() + "/" + day0.ToString() + "/" + year.ToString();

            var yearT = endDateLice.Value.Year;
            var monthT = endDateLice.Value.Month;
            var dayT = endDateLice.Value.Day;
            string dayT0 = "";
            string monthT0 = "";
            if (dayT < 10)
                dayT0 = '0' + dayT.ToString();
            else
                dayT0 = dayT.ToString();
            if (monthT < 10)
                monthT0 = "0" + monthT.ToString();
            else
                monthT0 = monthT.ToString();
            string endDateload = monthT0.ToString() + "/" + dayT0.ToString() + "/" + yearT.ToString();
            string selUserCmb = cmbUserIDLice.SelectedValue.ToString();
            try
            {
                // Stablish connection
                con = new OleDbConnection(ConString.dbConString);
                con.Open();
                OleDbCommand cmd = new OleDbCommand();

                string cmdUpdate = "UPDATE USERINFO " +
                                   " SET START_DATE='" + fromDateload + "'" +
                                   ",END_DATE='" + endDateload + "'" +
                                   " WHERE USER_ID='" + selUserCmb + "'";

                cmd.CommandText = cmdUpdate;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
            catch (Exception exobj) { MessageBox.Show(exobj.Message); }
        }
        //load Users into 
        private void loadUserIDs()
        {
            try
            {
                if (loginuser == ConString.superUser)
                {
                    con = new OleDbConnection(ConString.dbConString);
                    // First find the index and add the value of index
                    string selectIndx = "SELECT USER_ID, FIRST_NAME FROM USERINFO WHERE USER_ID <> '" + ConString.superUser +"'";
                    oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                    dt = new DataTable();
                    ds = new DataSet();
                    oledbAdapter.Fill(ds, "USERINFO");
                    dt = ds.Tables["USERINFO"];
                    cmbUserIDLice.DataSource = dt;
                    cmbUserIDLice.DisplayMember = "FIRST_NAME";
                    cmbUserIDLice.ValueMember = "USER_ID";
                }
                else { MessageBox.Show("You are not eligiable to Extend License"); }
                
            }
            catch (Exception exobj)
            {
                MessageBox.Show(exobj.Message);
            }

        }

        private void LicenseUpdate_Load(object sender, EventArgs e)
        {
            loginuser = File.ReadAllText(loginUserFile.loginUser).Trim();
            //load users into combo box
            loadUserIDs();

        }
    }
}
