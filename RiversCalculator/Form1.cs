using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace RiversCalculator
{
    public partial class RiversCalculator : Form
    {
        //create the instance of RiversInfo
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
        //current date setup

        public RiversCalculator()
        {
            InitializeComponent();
        }

        private void RiversCalculator_Load(object sender, EventArgs e)
        {
            //add the footting text
            lblBRight.Text = ConString.copyRight;
            //create the database
            // get the user information from database
            if (!File.Exists(@ConString.folderPath + "\\" + ConString.dbName))
            {
                // - First create the Folder
                string newDir = @ConString.folderPath;
                Directory.CreateDirectory(newDir);
                // then copy the database into this folder
                string SourceDir = @ConString.folderPath;
                string fname = @ConString.dbName;
                string folderPath = "";
                folderPath = Application.StartupPath;
                System.IO.File.Copy(Path.Combine(folderPath, fname), Path.Combine(SourceDir, fname), true);
            }
            //now show the login page
            Login loginForm = new Login();
            loginForm.ShowDialog();
            loginuser = File.ReadAllText(loginUserFile.loginUser).Trim();
          
            //after a success login check the user license expiration
            licensCheck();
            //disable the user account management menu for none admin
            if (loginuser != ConString.superUser)
            {
                manageUserAccount.Visible = false;
                mLicenseUpdate.Visible = false;
            }
            else
            {
                manageUserAccount.Visible = true;
                mLicenseUpdate.Visible = true;
            }

            //setup the year calender
            add_yearDT.Format = DateTimePickerFormat.Custom;
            add_yearDT.CustomFormat = "yyyy";
            add_yearDT.ShowUpDown = true;
            //load the country box
            loadCountryTbl("P");
            //load the country to report country box
            loadCountryTbl("R");
            //check if there is no river then disable the buton save
            if (cmbAddRiver.SelectedIndex != -1) btnSave.Enabled = true;
            else btnSave.Enabled = false;
            //change the from date
            ChangeFromDate();
            //load the report
            TheRiverRpt();
            //Calculate the year
            CalculateTheTyears();
            //calculate the Dcreasion
            TheRiverRptDecreation();
            //load our time period chart
            PercentageChart("R");
        }
        
        //load countries
        private void loadCountryTbl(string cType)
        {
            //clear Privences combo box
            if (cType == "P")
            {
                cmbCountry.DataSource = null;
                cmbCountry.Items.Clear();
            }
            //clear the report
            else
            {
                cmbRptCountry.DataSource = null;
                cmbRptCountry.Items.Clear();
            }
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
                if (cType == "P")
                {
                    cmbCountry.DataSource = dt;
                    cmbCountry.DisplayMember = "CNAME";
                    cmbCountry.ValueMember = "COUNTRY";
                    //first load provences
                    loadProvences("P");
                    //load district based on selected country
                    loadDistric("P");
                }
                else
                {
                    cmbRptCountry.DataSource = dt;
                    cmbRptCountry.DisplayMember = "CNAME";
                    cmbRptCountry.ValueMember = "COUNTRY";
                    //first load provences
                    loadProvences("R");
                    //load district based on selected country
                    loadDistric("R");
                }
                con.Close();
            }
            catch (Exception exobj)
            {
                MessageBox.Show("Error Location(loadCountryTbl())\n" + exobj.Message);
            }
        }

        //load Provence informatin to box 
        private void loadProvences(string cType)
        {
            string selCntry = "";
            //clear Privences combo box
            if (cType == "P")
            {
                cmbProvence.DataSource = null;
                cmbProvence.Items.Clear();
                if (cmbCountry.SelectedIndex != -1) selCntry = cmbCountry.SelectedValue.ToString();
                else selCntry = "";
            }
            //clear the report
            else
            {
                cmbRptProvence.DataSource = null;
                cmbRptProvence.Items.Clear();
                if (cmbRptCountry.SelectedIndex != -1) selCntry = cmbRptCountry.SelectedValue.ToString();
                else selCntry = "";
            }
            try
            {
                con = new OleDbConnection(ConString.dbConString);
                // First find the index and add the value of index
                string loadRiverQ = "SELECT DISTINCT PCODE, PNAME FROM RIVERSINFO WHERE COUNTRY='" + selCntry + "' AND USER_ID='" + loginuser + "'";
                con.Open();
                oledbAdapter = new OleDbDataAdapter(loadRiverQ, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "RIVERSINFO");
                dt = ds.Tables["RIVERSINFO"];
                if (cType == "P")
                {
                    cmbProvence.DataSource = dt;
                    cmbProvence.DisplayMember = "PNAME";
                    cmbProvence.ValueMember = "PCODE";
                }
                else
                {
                    cmbRptProvence.DataSource = dt;
                    cmbRptProvence.DisplayMember = "PNAME";
                    cmbRptProvence.ValueMember = "PCODE";
                }
                con.Close();
            }
            catch (Exception exobj)
            {
                MessageBox.Show("Error Location(loadProvences())\n" + exobj.Message);
            }
        }

        //load district informatin to box 
        private void loadDistric(string cType)
        {
            string selCntry, selProve;
            //clear district combo box
            if (cType == "P")
            {
                cmbDistrict.DataSource = null;
                cmbDistrict.Items.Clear();
                if (cmbCountry.SelectedIndex != -1) selCntry = cmbCountry.SelectedValue.ToString();
                else selCntry = "";
                if (cmbProvence.SelectedIndex != -1) selProve = cmbProvence.SelectedValue.ToString();
                else selProve = "";
            }
            //clear the report
            else
            {
                cmbRptDistric.DataSource = null;
                cmbRptDistric.Items.Clear();
                if (cmbRptCountry.SelectedIndex != -1) selCntry = cmbRptCountry.SelectedValue.ToString();
                else selCntry = "";
                if (cmbRptProvence.SelectedIndex != -1) selProve = cmbRptProvence.SelectedValue.ToString();
                else selProve = "";
            }
            try
            {
                con = new OleDbConnection(ConString.dbConString);
                // First find the index and add the value of index
                string loadRiverQ = "SELECT DNAME,DCODE FROM RIVERSINFO WHERE COUNTRY='" + selCntry + "' AND USER_ID='"+loginuser
                    +"' AND PCODE='" + selProve + "'";
                con.Open();
                oledbAdapter = new OleDbDataAdapter(loadRiverQ, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "RIVERSINFO");
                dt = ds.Tables["RIVERSINFO"];
                if (cType == "P")
                {
                    cmbDistrict.DataSource = dt;
                    cmbDistrict.DisplayMember = "DNAME";
                    cmbDistrict.ValueMember = "DCODE";
                }
                else
                {
                    cmbRptDistric.DataSource = dt;
                    cmbRptDistric.DisplayMember = "DNAME";
                    cmbRptDistric.ValueMember = "DCODE";
                }
                con.Close();
            }
            catch (Exception exobj)
            {
                MessageBox.Show("Error Location(loadDistric())\n" + exobj.Message);
            }

        }
        //this medthod will change the from date
        private void ChangeFromDate()
        {
            var dateAndTime = DateTime.Now;
            var year = dateAndTime.Year;
            var month = dateAndTime.Month;
            var day = dateAndTime.Day;
            string day0 = "", month0 = "";
            if (day < 10) { day0 = '0' + day.ToString(); }
            else { day0 = day.ToString(); }
            if (month < 10) { month0 = "0" + month.ToString(); }
            else { month0 = month.ToString(); }
            //change the date of from date
            var fromYearMinus10 = year - 10;
            string fromDateS = month0.ToString() + "/" + day0.ToString() + "/" + fromYearMinus10.ToString();
            //fromDate.Format = DateTimePickerFormat.Custom;
            //fromDate.CustomFormat = fromDateS;
        }
        //check if the license is not valid
        private void licensCheck()
        {
            var dateAndTime = DateTime.Now;
            var year = dateAndTime.Year;
            var month = dateAndTime.Month;
            var day = dateAndTime.Day;
            string day0 = "", month0 = "";
            if (day < 10) { day0 = '0' + day.ToString(); }
            else { day0 = day.ToString(); }
            if (month < 10) { month0 = "0" + month.ToString(); }
            else { month0 = month.ToString(); }
            string CurrDate = month0.ToString() + "/" + day0.ToString() + "/" + year.ToString();
            //change the date of from date
            var fromYearMinus10 = year - 10;
            string fromDateS = month0.ToString() + "/" + day0.ToString() + "/" + fromYearMinus10.ToString();
            try
            {
                con = new OleDbConnection(ConString.dbConString);
                // First find the index and add the value of index
                string selectIndx = "SELECT START_DATE, END_DATE FROM LICENSETBL WHERE USER_ID= '" + loginuser +
                    "' AND ( END_DATE >=#" + CurrDate + "#)";
                oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "LICENSETBL");
                dt = ds.Tables["LICENSETBL"];
                if (dt.Rows.Count == 0)
                {
                    LicenseUpdateDirection liceOpen = new LicenseUpdateDirection();
                    liceOpen.ShowDialog();
                }
                else { }
            }
            catch (Exception exobj) { MessageBox.Show("Error Location(licensCheck())\n" + exobj.Message); }
        }
        //load country into 
        private void loadRiverCmb(string dType)
        {
            //clear the cmb box
            if (dType == "P")
            {
                cmbAddRiver.DataSource = null;
                cmbAddRiver.Items.Clear();
            }
            else
            {
                cmbRiverSel.DataSource = null;
                cmbRiverSel.Items.Clear();
            }
            try
            {
                string dcode;
                if (dType == "P")
                {
                    if (cmbDistrict.SelectedIndex != -1) dcode = cmbDistrict.SelectedValue.ToString();
                    else dcode = "";
                }
                else
                {
                    if (cmbRptDistric.SelectedIndex != -1) dcode = cmbRptDistric.SelectedValue.ToString();
                    else dcode = "";
                }
                con = new OleDbConnection(ConString.dbConString);
                // First find the index and add the value of index
                string selectIndx = "SELECT RNAME,RIVER_ID FROM RIVERSINFO WHERE USER_ID='" + loginuser + "' AND DCODE='" + dcode+"'";
                oledbAdapter = new OleDbDataAdapter(selectIndx, con);
                dt = new DataTable();
                ds = new DataSet();
                oledbAdapter.Fill(ds, "RIVERSINFO");
                dt = ds.Tables["RIVERSINFO"];
                if (dt.Rows.Count > 0)
                {
                    if (dType == "P")
                    {
                        cmbAddRiver.DataSource = dt;
                        cmbAddRiver.DisplayMember = "RNAME";
                        cmbAddRiver.ValueMember = "RIVER_ID";
                    }
                    else
                    {
                        cmbRiverSel.DataSource = dt;
                        cmbRiverSel.DisplayMember = "RNAME";
                        cmbRiverSel.ValueMember = "RIVER_ID";
                    }
                }
            }
            catch (Exception exobj)
            {
                MessageBox.Show("Error Location(loadRiverCmb())\n" + exobj.Message);
            }
            
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtStation.Text == "") { MessageBox.Show("Please enter station name!"); txtStation.Focus(); }
            else if (txtStationID.Text == "") { MessageBox.Show("Please enter station id"); txtStationID.Focus(); }
            else if (txtCA.Text == "") { MessageBox.Show("Please enter the CA"); txtCA.Focus(); }
            else if (txtM3.Text == "") { MessageBox.Show("Please enter the m3/sec value"); txtM3.Focus(); }
            else
            {
                // - get the current login user id
                string SelectedRiver = cmbAddRiver.SelectedValue.ToString();
                string SelectedRiverN = cmbAddRiver.Text;
                string YearSel = add_yearDT.Value.Date.Year.ToString();
                string yearDate = "01/01/" + YearSel;
                try
                {
                    //-----------------------------check for duplicate record
                    string dupQ = "SELECT RIVER_ID FROM RIVERS WHERE RIVER_ID='" + SelectedRiver + 
                        "' AND YEAR='" + YearSel+"' AND USER_ID='"+loginuser+"'";
                    con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    cmd = new OleDbCommand();
                    oledbAdapter = new OleDbDataAdapter(dupQ, con);
                    ds = new DataSet();
                    dt = new DataTable();
                    oledbAdapter.Fill(ds, "RIVERS");
                    dt = ds.Tables["RIVERS"];
                    if (dt.Rows.Count == 0)
                    {
                        string insertQ = "INSERT INTO RIVERS VALUES('" + SelectedRiver + "','" + YearSel + "','" + loginuser + "','" +
                            SelectedRiverN + "','" + txtStation.Text + "','" + txtStationID.Text + "','" + txtCA.Text + "','" +
                            txtM3.Text + "','" + yearDate + "');";
                        cmd.Connection = con;
                        cmd.CommandText = insertQ;
                        cmd.ExecuteNonQuery();
                        lblMessage.Text = "A record successfully added into table!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        //clear the boxes
                        txtCA.Text = "";
                        txtM3.Text = "";
                        txtStation.Text = "";
                        txtStationID.Text = "";
                        //re-load the report
                        TheRiverRpt();
                        //calculate the time periods 
                        CalculateTheTyears();
                        //calculate the Dcreasion
                        TheRiverRptDecreation();
                        //load our time period chart
                        PercentageChart("R");
                    }
                    else { MessageBox.Show("the record is already exist!"); }
                }
                catch (Exception exob) { MessageBox.Show("Error Location(btnSave_Click())\n" + exob.Message); }

            }
        }

        private void txtM3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtM3.Text, "[^0-9]"))
            {
                lblMessage.Text = "only numbers allowed!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                txtM3.Text = txtM3.Text.Remove(txtM3.Text.Length - 1);
            }
            else { lblMessage.Text = ""; }
        }

        private void txtCA_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtCA.Text, "[^0-9]"))
            {
                txtCA.Text = txtCA.Text.Remove(txtCA.Text.Length - 1);
            }
            else { lblMessage.Text = ""; }
        }

        //create the report for gridview
        private void TheRiverRpt()
        {
            //-------------- get the From date and To date--------------
            var Fmonth0 = "";
            var Fday0 = "";
            var frmYear = fromDate.Value.Date.Year;
            var frmMonth = fromDate.Value.Date.Month;
            var frmDay = fromDate.Value.Date.Day;
            if (frmMonth < 10) { Fmonth0 = "0" + frmMonth.ToString(); }
            else { Fmonth0 = frmMonth.ToString(); }
            if (frmDay < 10) { Fday0 = "0" + frmDay.ToString(); }
            else { Fday0 = frmDay.ToString(); }
            // put the format date to the datetime picker
            string frmDateS = Fmonth0 + "/" + Fday0 + "/" + frmYear.ToString();

            // Format the to date and put it in an string
            var toMonth0 = "";
            var toDay0 = "";
            var toYear = toDate.Value.Date.Year;
            var toMonth = toDate.Value.Date.Month;
            var toDay = toDate.Value.Date.Day;
            if (toMonth < 10) { toMonth0 = "0" + toMonth.ToString(); }
            else { toMonth0 = toMonth.ToString(); }
            if (toDay < 10) { toDay0 = "0" + toDay.ToString(); }
            else { toDay0 = toDay.ToString(); }
            // put the toDate all together
            string toDateS = toMonth0 + "/" + toDay0 + "/" + toYear.ToString();
            try
            {
                //check eighter there is a river is exist or not
                if (cmbRiverSel.SelectedIndex != -1)
                {
                    //fill the info
                    string riverID = cmbRiverSel.SelectedValue.ToString();
                    // - Get the river information
                    string riverInfoQ = "SELECT TOP 1 RIVER_NAME, STATION_NAME, STATION_ID, CA FROM RIVERS " +
                       " WHERE USER_ID='" + loginuser + "' AND RIVER_ID='" + riverID + "'" +
                       " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)";
                    //Get total m3/s
                    string riverM3sQ = "SELECT Round(Avg(M3_PER_SECOND), 0) AS AVERAGE, COUNT(*) AS TOT_ROWS, Round(stDev(M3_PER_SECOND),0) AS STDEV FROM RIVERS " +
                       " WHERE USER_ID='" + loginuser + "' AND RIVER_ID='" + riverID + "'" +
                       " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)";

                    oledbAdapter = new OleDbDataAdapter(riverM3sQ, con);
                    ds = new DataSet();
                    dt = new DataTable();
                    oledbAdapter.Fill(ds, "RIVERS");
                    dt = ds.Tables["RIVERS"];

                    int avg;
                    bool boolpr = int.TryParse(dt.Rows[0]["AVERAGE"].ToString(), out avg);
                    if (boolpr)
                    {
                        int totRec = Int32.Parse(dt.Rows[0]["TOT_ROWS"].ToString());
                        txtMean.Text = dt.Rows[0]["AVERAGE"].ToString();
                        txtN.Text = totRec.ToString();
                        txtTimePerN.Text = totRec.ToString();

                        double std;
                        bool boolstd = double.TryParse(dt.Rows[0]["STDEV"].ToString(), out std);
                        if (boolstd)
                        {
                            txtStd.Text = dt.Rows[0]["STDEV"].ToString();
                        }
                        else { txtStd.Text = ""; }
                    }
                    else
                    {
                        txtMean.Text = "";
                        txtN.Text = "";
                        txtTimePerN.Text = "";
                        txtStd.Text = "";
                    }
                    con.Close();
                    con.Open();
                    oledbAdapter = new OleDbDataAdapter(riverInfoQ, con);
                    ds = new DataSet();
                    dt = new DataTable();
                    oledbAdapter.Fill(ds, "RIVERS");
                    dt = ds.Tables["RIVERS"];
                    if (dt.Rows.Count > 0)
                    {
                        txtStNameDisp.Text = dt.Rows[0]["STATION_NAME"].ToString();
                        txtStNumDisp.Text = dt.Rows[0]["STATION_ID"].ToString();
                        txtCADisp.Text = dt.Rows[0]["CA"].ToString() + " km2";

                        string riverQ = "SELECT YEAR AS 'Years', M3_PER_SECOND AS 'm3/Sec' FROM RIVERS WHERE USER_ID='" + loginuser
                            + "' AND RIVER_ID='" + riverID + "'" +
                       " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)";
                        oledbAdapter = new OleDbDataAdapter(riverQ, con);
                        dt = new DataTable();
                        oledbAdapter.Fill(dt);
                        dataGridRiver.DataSource = dt;
                    }
                    else
                    {
                        txtStNameDisp.Text = "";
                        txtStNumDisp.Text = "";
                        txtCADisp.Text = "";
                        dataGridRiver.DataSource = null;
                        txtMean.Text = "";
                    }
                }
            }
            catch (Exception exobj) { MessageBox.Show("Error Location(TheRiverRpt())\n" + exobj.Message); }
        }

        private void cmbRiverSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRiverSel.SelectedIndex != -1)
            {
                //the river report with m3
                TheRiverRpt();
                //calculate the time periods 
                CalculateTheTyears();
                //calculate the Dcreasion
                TheRiverRptDecreation();
                //load our time period chart
                PercentageChart("R");
            }
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

        private void HelpOption_Click(object sender, EventArgs e)
        {
            string helpPath = Application.StartupPath + "\\Help.pdf";
            System.Diagnostics.Process.Start(helpPath);
        }

        private void manageUserAccount_Click(object sender, EventArgs e)
        {
            DeletAuser DAUSR = null;
            if ((DAUSR = (DeletAuser)IsFormAlreadyOpen(typeof(DeletAuser))) == null)
            {
                DAUSR = new DeletAuser();
                DAUSR.Show();
            }
            else
            {
                MessageBox.Show("The delete user page is already open!");
            }
        }

        //back up a copy of database to user selected place
        private void mgmtBackup_Click(object sender, EventArgs e)
        {
            var frmYear = DateTime.Now.Year;
            var frmMonth = DateTime.Now.Month;
            var frmDay = DateTime.Now.Day;

            var month0 = "";
            var day0 = "";
            if (frmMonth < 10) { month0 = "0" + frmMonth.ToString(); }
            else { month0 = frmMonth.ToString(); }
            if (frmDay < 10) { day0 = "0" + frmDay.ToString(); }
            else { day0 = frmDay.ToString(); }

            string currTime = DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            string currDt = DateTime.Now.Year + "_" + month0 + "_" + day0;
            FolderBrowserDialog fld = new FolderBrowserDialog();
            string SourceDir = ConString.folderPath;
            string fname = ConString.dbName;
            try
            {
                if (fldBrowser.ShowDialog() == DialogResult.OK)
                {
                    string fldpath = fldBrowser.SelectedPath + "\\RIVER_INFO_backup" + currDt + "_" + currTime;
                    // - first create the above directory
                    System.IO.Directory.CreateDirectory(fldpath);

                    System.IO.File.Copy(Path.Combine(SourceDir, fname), Path.Combine(fldpath, fname));
                    MessageBox.Show("A backup of your database is created in the following path \n" + fldpath);
                }
                else
                    MessageBox.Show("Backup faild!");
            }
            catch (Exception exobj) { MessageBox.Show(exobj.Message); }
        }
        //restor the database from selected backup
        private void mgmtRestor_Click(object sender, EventArgs e)
        {
            string SourceDir = ConString.folderPath;
            string fname = ConString.dbName;
            string folderPath = "";
            try
            {
                if (MessageBox.Show("Are you sure you want to restore your database, because you will lost your old data",
                "Confirm Dialog", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (fldBrowser.ShowDialog() == DialogResult.OK)
                    {
                        folderPath = fldBrowser.SelectedPath;
                        System.IO.File.Copy(Path.Combine(folderPath, fname), Path.Combine(SourceDir, fname), true);
                        MessageBox.Show("Your database successfully restored! ");
                        //Load country to river combo box
                        loadRiverCmb("R");
                        //load the report
                        TheRiverRpt();
                        //calculate the Dcreasion
                        TheRiverRptDecreation();
                        //load our time period chart
                        PercentageChart("R");
                    }
                    else
                        MessageBox.Show("Faild to restor your database! ");
                }
            }
            catch (Exception exobj) { MessageBox.Show("Error Location(mgmtRestor_Click())\n" + exobj.Message); }
        }

        private void mEditRiverInfo_Click(object sender, EventArgs e)
        {
            EditRiverInfo DAUSR = null;
            if ((DAUSR = (EditRiverInfo)IsFormAlreadyOpen(typeof(EditRiverInfo))) == null)
            {
                DAUSR = new EditRiverInfo();
                DAUSR.Show();
            }
            else
            {
                MessageBox.Show("The Edit River page is already open!");
            }
        }

        private void mEditRiverRpt_Click(object sender, EventArgs e)
        {
            EditRiverInforRpt DAUSR = null;
            if ((DAUSR = (EditRiverInforRpt)IsFormAlreadyOpen(typeof(EditRiverInforRpt))) == null)
            {
                DAUSR = new EditRiverInforRpt();
                DAUSR.Show();
            }
            else
            {
                MessageBox.Show("Edit page for river is already open!");
            }
        }

        private void btnTimePer_Click(object sender, EventArgs e)
        {
            //calculate the time periods 
            CalculateTheTyears();
            //show the graph
            PercentageChart("R");
        }

        //calculate the values
        private void CalculateTheTyears()
        {
            try
            {
                //clear the tyears table first
                string delTyear = "DELETE FROM TYEARS ";
                ConString.excuteMyQuery(delTyear);
                //Calculate the year
                //--------------------Time Period 1
                double tp1Val;
                double YnVal, SnVal;
                if (txtYn.Text == "") YnVal = 0.00;
                else YnVal = double.Parse(txtYn.Text);
                if (txtSn.Text == "") SnVal = 0.00;
                else SnVal = double.Parse(txtSn.Text);
                //calculate Yt
                if (txtTP1.Text == "") { tp1Val = 0; txtYtTP1.Text = ""; txtKTP1.Text = ""; }
                else
                {
                    tp1Val = double.Parse(txtTP1.Text);
                    double YtTP1C = -(Math.Log(Math.Log((tp1Val / (tp1Val - 1)))));
                    txtYtTP1.Text = YtTP1C.ToString("0.##");
                    //Calculate the K
                    double tp1YnC = (YtTP1C - YnVal) / SnVal;
                    txtKTP1.Text = tp1YnC.ToString("0.##");
                    //calculate the Xt records
                    double tp1XtC = double.Parse(txtMean.Text) + tp1YnC * double.Parse(txtStd.Text);
                    txtXtTP1.Text = Math.Truncate(tp1XtC).ToString();
                }
                //--------------------Time Period 2
                double tp2Val;
                if (txtTP2.Text == "") { tp2Val = 0; txtYtTP2.Text = ""; txtKTP2.Text = ""; }
                else
                {
                    tp2Val = double.Parse(txtTP2.Text);
                    double YtTP2C = -(Math.Log(Math.Log((tp2Val / (tp2Val - 1)))));
                    txtYtTP2.Text = YtTP2C.ToString("0.##");
                    //Calculate the K
                    double tp2YnC = (YtTP2C - YnVal) / SnVal;
                    txtKTP2.Text = tp2YnC.ToString("0.##");
                    //calculate the Xt records
                    double tp2XtC = double.Parse(txtMean.Text) + tp2YnC * double.Parse(txtStd.Text);
                    txtXtTP2.Text = Math.Truncate(tp2XtC).ToString();
                }
                //--------------------Time Period 3
                double tp3Val;
                if (txtTP3.Text == "") { tp3Val = 0; txtYtTP3.Text = ""; txtKTP3.Text = ""; }
                else
                {
                    tp3Val = double.Parse(txtTP3.Text);
                    double YtTP3C = -(Math.Log(Math.Log((tp3Val / (tp3Val - 1)))));
                    txtYtTP3.Text = YtTP3C.ToString("0.##");
                    //Calculate the K
                    double tp3YnC = (YtTP3C - YnVal) / SnVal;
                    txtKTP3.Text = tp3YnC.ToString("0.##");
                    //calculate the Xt records
                    double tp3XtC = double.Parse(txtMean.Text) + tp3YnC * double.Parse(txtStd.Text);
                    txtXtTP3.Text = Math.Truncate(tp3XtC).ToString();
                }
                //--------------------Time Period 4
                double tp4Val;
                if (txtTP4.Text == "") { tp4Val = 0; txtYtTP4.Text = ""; txtKTP4.Text = ""; }
                else
                {
                    tp4Val = double.Parse(txtTP4.Text);
                    double YtTP4C = -(Math.Log(Math.Log((tp4Val / (tp4Val - 1)))));
                    txtYtTP4.Text = YtTP4C.ToString("0.##");
                    //Calculate the K
                    double tp4YnC = (YtTP4C - YnVal) / SnVal;
                    txtKTP4.Text = tp4YnC.ToString("0.##");
                    //calculate the Xt records
                    double tp4XtC = double.Parse(txtMean.Text) + tp4YnC * double.Parse(txtStd.Text);
                    txtXtTP4.Text = Math.Truncate(tp4XtC).ToString();
                }
                //--------------------Time Period 5
                double tp5Val;
                if (txtTP5.Text == "") { tp5Val = 0; txtYtTP5.Text = ""; txtKTP5.Text = ""; }
                else
                {
                    tp5Val = double.Parse(txtTP5.Text);
                    double YtTP5C = -(Math.Log(Math.Log((tp5Val / (tp5Val - 1)))));
                    txtYtTP5.Text = YtTP5C.ToString("0.##");
                    //Calculate the K
                    double tp5YnC = (YtTP5C - YnVal) / SnVal;
                    txtKTP5.Text = tp5YnC.ToString("0.##");
                    //calculate the Xt records
                    double tp5XtC = double.Parse(txtMean.Text) + tp5YnC * double.Parse(txtStd.Text);
                    txtXtTP5.Text = Math.Truncate(tp5XtC).ToString();
                }
                //--------------------Time Period 6
                double tp6Val;
                if (txtTP6.Text == "") { tp6Val = 0; txtYtTP6.Text = ""; txtKTP6.Text = ""; }
                else
                {
                    tp6Val = double.Parse(txtTP6.Text);
                    double YtTP6C = -(Math.Log(Math.Log((tp6Val / (tp6Val - 1)))));
                    txtYtTP6.Text = YtTP6C.ToString("0.##");
                    //Calculate the K
                    double tp6YnC = (YtTP6C - YnVal) / SnVal;
                    txtKTP6.Text = tp6YnC.ToString("0.##");
                    //calculate the Xt records
                    double tp6XtC = double.Parse(txtMean.Text) + tp6YnC * double.Parse(txtStd.Text);
                    txtXtTP6.Text = Math.Truncate(tp6XtC).ToString();
                }
                //--------------------Time Period 7
                double tp7Val;
                if (txtTP7.Text == "") { tp7Val = 0; txtYtTP7.Text = ""; txtKTP7.Text = ""; }
                else
                {
                    tp7Val = double.Parse(txtTP7.Text);
                    double YtTP7C = -(Math.Log(Math.Log((tp7Val / (tp7Val - 1)))));
                    txtYtTP7.Text = YtTP7C.ToString("0.##");
                    //Calculate the K
                    double tp7YnC = (YtTP7C - YnVal) / SnVal;
                    txtKTP7.Text = tp7YnC.ToString("0.##");
                    //calculate the Xt records
                    double tp7XtC = double.Parse(txtMean.Text) + tp7YnC * double.Parse(txtStd.Text);
                    txtXtTP7.Text = Math.Truncate(tp7XtC).ToString();
                }
                //--------------------Time Period 8
                double tp8Val;
                if (txtTP8.Text == "") { tp8Val = 0; txtYtTP8.Text = ""; txtKTP8.Text = ""; }
                else
                {
                    tp8Val = double.Parse(txtTP8.Text);
                    double YtTP8C = -(Math.Log(Math.Log((tp8Val / (tp8Val - 1)))));
                    txtYtTP8.Text = YtTP8C.ToString("0.##");
                    //Calculate the K
                    double tp8YnC = (YtTP8C - YnVal) / SnVal;
                    txtKTP8.Text = tp8YnC.ToString("0.##");
                    //calculate the Xt records
                    double tp8XtC = double.Parse(txtMean.Text) + tp8YnC * double.Parse(txtStd.Text);
                    txtXtTP8.Text = Math.Truncate(tp8XtC).ToString();
                }
                //now insert the calculated data into tyears table
                string tQ1 = "INSERT INTO TYEARS VALUES ('"+lblTyears.Text+"',"+txtTP1.Text+","+txtTP2.Text+","+txtTP3.Text+","+txtTP4.Text+
                    ","+txtTP5.Text+","+txtTP6.Text+","+txtTP7.Text+","+txtTP8.Text+")";
                ConString.excuteMyQuery(tQ1);
                string tQ2 = "INSERT INTO TYEARS VALUES ('"+lblYt.Text+"',"+txtYtTP1.Text+","+txtYtTP2.Text+","+txtYtTP3.Text+","+txtYtTP4.Text+
                    ","+txtYtTP5.Text+","+txtYtTP6.Text+","+txtYtTP7.Text+","+txtYtTP8.Text+")";
                ConString.excuteMyQuery(tQ2);
                string tQ3 = "INSERT INTO TYEARS VALUES ('"+lblK.Text+"',"+txtKTP1.Text+","+txtKTP2.Text+","+txtKTP3.Text+","+txtKTP4.Text+","+
                    txtKTP5.Text+","+txtKTP6.Text+","+txtKTP7.Text+","+txtKTP8.Text+")";
                ConString.excuteMyQuery(tQ3);
                string tQ4 = "INSERT INTO TYEARS VALUES ('"+lblXt.Text+"',"+txtXtTP1.Text+","+txtXtTP2.Text+","+txtXtTP3.Text+","+
                    txtXtTP4.Text+","+txtXtTP5.Text+","+txtXtTP6.Text+","+txtXtTP7.Text+","+txtXtTP8.Text+")";
                ConString.excuteMyQuery(tQ4);
            }
            catch { }
        }
        //load data into valtable
        private void loadToValTbl()
        {
            //Now insert the value of some screen options
            //clear first the table
            string vtQclr = "DELETE FROM VALTABLE";
            ConString.excuteMyQuery(vtQclr);
            string vTQ1 = "INSERT INTO VALTABLE VALUES('Country','" + cmbRptCountry.Text + "','1')";
            ConString.excuteMyQuery(vTQ1);
            string vTQ2 = "INSERT INTO VALTABLE VALUES('Provence','" + cmbRptProvence.Text + "','1')";
            ConString.excuteMyQuery(vTQ2);
            string vTQ3 = "INSERT INTO VALTABLE VALUES('District','" + cmbRptDistric.Text + "','1')";
            ConString.excuteMyQuery(vTQ3);
            string vTQ4 = "INSERT INTO VALTABLE VALUES('River','" + cmbRiverSel.Text + "','1')";
            ConString.excuteMyQuery(vTQ4);
            string vTQ5 = "INSERT INTO VALTABLE VALUES('Station','" + txtStNameDisp.Text + "','1')";
            ConString.excuteMyQuery(vTQ5);
            string vTQ6 = "INSERT INTO VALTABLE VALUES('Station ID','" + txtStNumDisp.Text + "','1')";
            ConString.excuteMyQuery(vTQ6);
            string vTQ7 = "INSERT INTO VALTABLE VALUES('Capacity','" + txtCADisp.Text + "','1')";
            ConString.excuteMyQuery(vTQ7);
            //add the bottom part to database
            string vTbQ1 = "INSERT INTO VALTABLE VALUES('Average','" + txtMean.Text + "','2')";
            ConString.excuteMyQuery(vTbQ1);
            string vTbQ2 = "INSERT INTO VALTABLE VALUES('Dividence','" + txtStd.Text + "','2')";
            ConString.excuteMyQuery(vTbQ2);
            string vTbQ3 = "INSERT INTO VALTABLE VALUES('Count','" + txtN.Text + "','2')";
            ConString.excuteMyQuery(vTbQ3);
        }
        //add the above data into data grid dataGridRptOptsT
        private void loadTopOptsToDG()
        {
            dataGridRptOptsT.DataSource = null;
            try
            {
                string riverQ = "SELECT NAME_VAL, VAL FROM VALTABLE WHERE MARK='1'";
                con = new OleDbConnection(ConString.dbConString);
                con.Open();
                oledbAdapter = new OleDbDataAdapter(riverQ, con);
                dt = new DataTable();
                oledbAdapter.Fill(dt);
                dataGridRptOptsT.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        //add the above data into data grid dataGridRptOptsB
        private void loadBottomOptsToDG()
        {
            dataGridRptOptsB.DataSource = null;
            try
            {
                string riverQ = "SELECT NAME_VAL, VAL FROM VALTABLE WHERE MARK='2'";
                con = new OleDbConnection(ConString.dbConString);
                con.Open();
                oledbAdapter = new OleDbDataAdapter(riverQ, con);
                dt = new DataTable();
                oledbAdapter.Fill(dt);
                dataGridRptOptsB.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //add the above data into data grid dataGridRptOptsT
        private void loadTopRightOptsToDG()
        {
            dataGridRptOptsTR.DataSource = null;
            try
            {
                string riverQ = "SELECT * FROM TYEARS";
                con = new OleDbConnection(ConString.dbConString);
                con.Open();
                oledbAdapter = new OleDbDataAdapter(riverQ, con);
                dt = new DataTable();
                oledbAdapter.Fill(dt);
                dataGridRptOptsTR.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void txtTP1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTP1.Text, "[^0-9]"))
            {
                txtTP1.Text = txtTP1.Text.Remove(txtTP1.Text.Length - 1);
            }
            else { }
        }

        private void txtTP2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTP2.Text, "[^0-9]"))
            {
                txtTP2.Text = txtTP2.Text.Remove(txtTP2.Text.Length - 1);
            }
            else { }
        }

        private void txtTP3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTP3.Text, "[^0-9]"))
            {
                txtTP3.Text = txtTP3.Text.Remove(txtTP3.Text.Length - 1);
            }
            else { }
        }

        private void txtTP4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTP4.Text, "[^0-9]"))
            {
                txtTP4.Text = txtTP4.Text.Remove(txtTP4.Text.Length - 1);
            }
            else { }
        }

        private void txtTP5_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTP5.Text, "[^0-9]"))
            {
                txtTP5.Text = txtTP5.Text.Remove(txtTP5.Text.Length - 1);
            }
            else { }
        }

        private void txtTP6_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTP6.Text, "[^0-9]"))
            {
                txtTP6.Text = txtTP6.Text.Remove(txtTP6.Text.Length - 1);
            }
            else { }
        }

        private void txtTP7_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTP7.Text, "[^0-9]"))
            {
                txtTP7.Text = txtTP7.Text.Remove(txtTP7.Text.Length - 1);
            }
            else { }
        }

        private void txtTP8_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTP8.Text, "[^0-9]"))
            {
                txtTP8.Text = txtTP8.Text.Remove(txtTP8.Text.Length - 1);
            }
            else { }
        }

        //create the Decreation report for gridview decreation
        private void TheRiverRptDecreation()
        {
            //-------------- get the From date and To date--------------
            var Fmonth0 = "";
            var Fday0 = "";
            var frmYear = fromDate.Value.Date.Year;
            var frmMonth = fromDate.Value.Date.Month;
            var frmDay = fromDate.Value.Date.Day;
            if (frmMonth < 10) { Fmonth0 = "0" + frmMonth.ToString(); }
            else { Fmonth0 = frmMonth.ToString(); }
            if (frmDay < 10) { Fday0 = "0" + frmDay.ToString(); }
            else { Fday0 = frmDay.ToString(); }
            // put the format date to the datetime picker
            string frmDateS = Fmonth0 + "/" + Fday0 + "/" + frmYear.ToString();

            // Format the to date and put it in an string
            var toMonth0 = "";
            var toDay0 = "";
            var toYear = toDate.Value.Date.Year;
            var toMonth = toDate.Value.Date.Month;
            var toDay = toDate.Value.Date.Day;
            if (toMonth < 10) { toMonth0 = "0" + toMonth.ToString(); }
            else { toMonth0 = toMonth.ToString(); }
            if (toDay < 10) { toDay0 = "0" + toDay.ToString(); }
            else { toDay0 = toDay.ToString(); }
            // put the toDate all together
            string toDateS = toMonth0 + "/" + toDay0 + "/" + toYear.ToString();
            try
            {
                //check eighter there is a river or not
                if (cmbRiverSel.SelectedIndex != -1)
                {
                    //Get river id
                    string riverID = cmbRiverSel.SelectedValue.ToString();
                    //Get total m3/s
                    string riverM3sQ = "SELECT COUNT(*) AS TOT_ROWS FROM RIVERS " +
                       " WHERE USER_ID='" + loginuser + "' AND RIVER_ID='" + riverID + "'" +
                       " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)";
                    con = new OleDbConnection(ConString.dbConString);
                    con.Open();
                    oledbAdapter = new OleDbDataAdapter(riverM3sQ, con);
                    ds = new DataSet();
                    dt = new DataTable();
                    oledbAdapter.Fill(ds, "RIVERS");
                    dt = ds.Tables["RIVERS"];
                    if (dt.Rows.Count > 0)
                    {
                        string riverQ = "SELECT BRS.M3_PER_SECOND AS 'M3s', (SELECT COUNT(*) FROM RIVERS " +
                            "AS B WHERE B.M3_PER_SECOND >= BRS.M3_PER_SECOND AND RIVER_ID='" + riverID + "' AND USER_ID='" + loginuser + "'" +
                            " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)" +
                            ") AS Row, Round(Row/((SELECT COUNT(*) FROM RIVERS WHERE USER_ID='" + loginuser + "' AND RIVER_ID='" + riverID + "'" +
                            " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)" +
                            ") +1 ), 2) AS P, Round(1/p, 1) AS T1P, " +
                            "Round(Row/((SELECT COUNT(*) FROM RIVERS WHERE USER_ID='" + loginuser + "' AND RIVER_ID='" + riverID + "'" +
                            " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)" +
                            ") +1 )*100, 1) AS PP " +
                            "FROM RIVERS AS BRS WHERE USER_ID='" + loginuser + "'" +
                            " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)" +
                            " AND RIVER_ID='" + riverID + "' ORDER BY BRS.M3_PER_SECOND DESC; ";

                        con.Close();
                        con = new OleDbConnection(ConString.dbConString);
                        con.Open();
                        oledbAdapter = new OleDbDataAdapter(riverQ, con);
                        dt = new DataTable();
                        oledbAdapter.Fill(dt);
                        dataGridDecreation.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        dataGridDecreation.DataSource = null;
                    }
                }
            }
            catch (Exception exobj) { MessageBox.Show("Error Location(TheRiverRptDecreation())\n" + exobj.Message); }
        }

        //Percentage cahrt method  
        private void PercentageChart(string gType)
        {
            try
            {
                this.pctChart.ChartAreas.Clear();
                this.pctChart.Series.Clear();
                this.pctChart.Legends.Clear();
            }
            catch { }
            if (gType == "P") firstSeries(gType);
            else { firstSeries(gType); secondSeries(); }
        }
        //first Line Graph
        private void firstSeries(string gType)
        {

            //-------------- get the From date and To date--------------
            var Fmonth0 = "";
            var Fday0 = "";
            var frmYear = fromDate.Value.Date.Year;
            var frmMonth = fromDate.Value.Date.Month;
            var frmDay = fromDate.Value.Date.Day;
            if (frmMonth < 10) { Fmonth0 = "0" + frmMonth.ToString(); }
            else { Fmonth0 = frmMonth.ToString(); }
            if (frmDay < 10) { Fday0 = "0" + frmDay.ToString(); }
            else { Fday0 = frmDay.ToString(); }
            // put the format date to the datetime picker
            string frmDateS = Fmonth0 + "/" + Fday0 + "/" + frmYear.ToString();

            // Format the to date and put it in an string
            var toMonth0 = "";
            var toDay0 = "";
            var toYear = toDate.Value.Date.Year;
            var toMonth = toDate.Value.Date.Month;
            var toDay = toDate.Value.Date.Day;
            if (toMonth < 10) { toMonth0 = "0" + toMonth.ToString(); }
            else { toMonth0 = toMonth.ToString(); }
            if (toDay < 10) { toDay0 = "0" + toDay.ToString(); }
            else { toDay0 = toDay.ToString(); }
            // put the toDate all together
            string toDateS = toMonth0 + "/" + toDay0 + "/" + toYear.ToString();
            try
            {
                //check eighter the river is exist or not
                if (cmbRiverSel.SelectedIndex != -1)
                {
                    //Get river id
                    string riverID = cmbRiverSel.SelectedValue.ToString();
                    pctChart.DataSource = null;
                    foreach (var series in pctChart.Series)
                    {
                        series.Points.Clear();
                    }
                    string rivChartQ = "SELECT BRS.M3_PER_SECOND, (SELECT COUNT(*) FROM RIVERS " +
                            "AS B WHERE B.M3_PER_SECOND >= BRS.M3_PER_SECOND AND RIVER_ID='" + riverID + "' AND USER_ID='" + loginuser + "'" +
                            " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)" +
                            ") AS Row, Round(Row/((SELECT COUNT(*) FROM RIVERS WHERE USER_ID='" + loginuser + "' AND RIVER_ID='" + riverID + "'" +
                            " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)" +
                            ") +1 ), 2) AS P, Round(1/p, 1) AS T1P, " +
                            "Round(Row/((SELECT COUNT(*) FROM RIVERS WHERE USER_ID='" + loginuser + "' AND RIVER_ID='" + riverID + "'" +
                            " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)" +
                            ") +1 )*100, 1) AS PP " +
                            "FROM RIVERS AS BRS WHERE USER_ID='" + loginuser + "'" +
                            " AND ( YEAR_DATE >= #" + frmDateS + "#) AND ( YEAR_DATE <= #" + toDateS + "#)" +
                            " AND RIVER_ID='" + riverID + "' ORDER BY BRS.M3_PER_SECOND DESC; ";
                    con = new OleDbConnection(ConString.dbConString);
                    ds = new DataSet();
                    con.Open();
                    oledbAdapter = new OleDbDataAdapter(rivChartQ, con);
                    oledbAdapter.Fill(ds);
                    pctChart.DataSource = ds;
                    //set the member of the chart data source used to data bind to the X-values of the series  
                    string xCol;
                    string xTitle, yTitle, legendText;
                    if (gType == "P")
                    {
                        xCol = "PP";
                        xTitle = "Percentage of Time Discharge";
                        yTitle = "Discharge(m3/sec)";
                        legendText = "Flow duration curve";
                    }

                    else
                    {
                        xCol = "T1P";
                        xTitle = "Return Period";
                        yTitle = "Maximum Discharge(m3/sec)";
                        legendText = "Annual observed maximum Discharge";
                    }
                    /*pctChart.Series["TimePeriod"].XValueMember = xCol;
                    //set the member columns of the chart data source used to data bind to the X-values of the series  
                    pctChart.Series["TimePeriod"].YValueMembers = "M3_PER_SECOND";
                    pctChart.Series[0].BorderWidth = 1;
                    pctChart.Series[0].ChartType = SeriesChartType.Line;
                    pctChart.ChartAreas[0].AxisX.Title = xTitle;*/

                    //setup chart area
                    ChartArea chartArea = new ChartArea();
                    chartArea.Name = "First Area";
                    pctChart.ChartAreas.Add(chartArea);
                    chartArea.BackColor = Color.Azure;
                    chartArea.BackGradientStyle = GradientStyle.HorizontalCenter;
                    chartArea.BackHatchStyle = ChartHatchStyle.LargeGrid;
                    chartArea.BorderDashStyle = ChartDashStyle.Solid;
                    chartArea.BorderWidth = 1;
                    //chartArea.BorderColor = Color.Red;
                    //chartArea.ShadowColor = Color.Purple;
                    chartArea.ShadowOffset = 0;
                    pctChart.ChartAreas[0].Axes[0].MajorGrid.Enabled = true;//x axis
                    pctChart.ChartAreas[0].Axes[1].MajorGrid.Enabled = true;//y axis

                    //Cursor: only apply the top area
                    chartArea.CursorX.IsUserEnabled = true;
                    //chartArea.CursorX.AxisType = AxisType.Primary;
                    chartArea.CursorX.Interval = 1;
                    chartArea.CursorX.LineWidth = 1;
                    chartArea.CursorX.LineDashStyle = ChartDashStyle.Dash;
                    chartArea.CursorX.IsUserSelectionEnabled = true;
                    chartArea.CursorX.SelectionColor = Color.Yellow;
                    chartArea.CursorX.AutoScroll = true;

                    chartArea.CursorY.IsUserEnabled = true;
                    chartArea.CursorY.AxisType = AxisType.Primary;
                    chartArea.CursorY.Interval = 1;
                    chartArea.CursorY.LineWidth = 1;
                    chartArea.CursorY.LineDashStyle = ChartDashStyle.Dash;
                    chartArea.CursorY.IsUserSelectionEnabled = true;
                    chartArea.CursorY.SelectionColor = Color.Yellow;
                    chartArea.CursorY.AutoScroll = true;

                    //Axis
                    chartArea.AxisY.Minimum = 0d;// Y axis Minimum value
                    chartArea.AxisY.Title = @yTitle;
                    chartArea.AxisX.Minimum = 0d;//X axis Minimum value
                    //chartArea.AxisX.Maximum = 12d;
                    chartArea.AxisX.IsLabelAutoFit = true;
                    chartArea.AxisX.LabelAutoFitMaxFontSize = 5;
                    chartArea.AxisX.LabelStyle.Angle = -20;
                    chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;//show the last label
                    //chartArea.AxisX.Interval = 100;
                    if (gType == "R")
                    {
                        chartArea.AxisX.Minimum = 1.0;
                        chartArea.AxisX.Maximum = 1000.0;
                        chartArea.AxisX.MajorGrid.Interval = 10.0;
                        chartArea.AxisX.MinorGrid.Interval = 10.0;
                        chartArea.AxisX.IsLogarithmic = true;
                        chartArea.AxisX.LogarithmBase = 10;
                    }
                    chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
                    chartArea.AxisX.IntervalType = DateTimeIntervalType.NotSet;
                    chartArea.AxisX.Title = @xTitle;
                    chartArea.AxisX.TextOrientation = TextOrientation.Auto;
                    chartArea.AxisX.LineWidth = 2;
                    chartArea.AxisX.LineColor = Color.DarkOrchid;

                    //this.pctChart.Series.Clear();
                    Series series1 = new Series("");
                    series1.ChartArea = "First Area";
                    if (pctChart.Series.IsUniqueName(series1.ChartArea)) pctChart.Series.Add(series1);
                    //Series style
                    series1.Name = "";
                    series1.ChartType = SeriesChartType.Line;//type
                    series1.BorderWidth = 1;
                    series1.BorderColor = Color.Blue;
                    series1.XValueType = ChartValueType.Int32;
                    series1.YValueType = ChartValueType.Int32;
                    
                    //marker
                    if (gType == "R")
                    {
                        series1.MarkerStyle = MarkerStyle.Diamond;
                        series1.MarkerSize = 10;
                        series1.MarkerStep = 1;
                        series1.MarkerColor = Color.Blue;
                        series1.ToolTip = @legendText+" Points (#VALX, #VALY)";
                    }
                    else
                    {
                        series1.ToolTip = @legendText + " Points (#VALX, #VALY)";
                    }

                    //Label
                    /*series1.IsValueShownAsLabel = true;
                    series1.SmartLabelStyle.Enabled = false;
                    series1.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
                    series1.LabelForeColor = Color.Gray;
                    series1.LabelToolTip = @"SeriesToolTip";*/
                    //Empty Point Style 
                    DataPointCustomProperties p = new DataPointCustomProperties();
                    p.Color = Color.Green;
                    series1.EmptyPointStyle = p;
                    //Legend
                    series1.LegendText = @legendText;
                    this.pctChart.Legends.Add("Annual observed maximum Discharge");
                    this.pctChart.Legends["Annual observed maximum Discharge"].Docking = Docking.Top;

                    series1.YValueMembers = "M3_PER_SECOND";
                    series1.XValueMember = xCol;
                    series1.BorderWidth = 1;
                    con.Close();
                }
            }
            catch (Exception exobj) { MessageBox.Show("Error Location(firstSeries())\n" + exobj.Message); }
        }
        //second series
        private void secondSeries()
        {
            try
            {
                //Series
                Series series1 = new Series("");
                pctChart.Series.Add(series1);
                //pctChart.Series[1].YAxisType = AxisType.Secondary;

                series1.Name = "Second Area";
                series1.ChartType = SeriesChartType.Line;
                series1.BorderWidth = 2;
                series1.Color = Color.Red;
                series1.XValueType = ChartValueType.Int32;
                series1.YValueType = ChartValueType.Int32;

                //Marker
                series1.MarkerStyle = MarkerStyle.None;
                series1.MarkerSize = 10;
                series1.MarkerStep = 1;
                series1.MarkerColor = Color.Red;
                series1.ToolTip = @"Gumble Distribution: points(#VALX, #VALY)";

                //Label:
                /*series1.IsValueShownAsLabel = true;
                series1.SmartLabelStyle.Enabled = false;
                series1.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
                series1.LabelForeColor = Color.Gray;
                series1.LabelToolTip = @"LabelToolTip";*/

                //Legend
                series1.LegendText = "Gumble Distribution";
                this.pctChart.Legends.Add("Gumble Distribution");
                this.pctChart.Legends["Gumble Distribution"].Docking = Docking.Top;
                //convert the calculated value 
                int xtp1, xtp2, xtp3, xtp4, xtp5, xtp6, xtp7, xtp8;
                if (int.TryParse(txtXtTP1.Text, out xtp1)) { xtp1 = int.Parse(txtXtTP1.Text); }
                if (int.TryParse(txtXtTP2.Text, out xtp2)) { xtp2 = int.Parse(txtXtTP2.Text); }
                if (int.TryParse(txtXtTP3.Text, out xtp3)) { xtp3 = int.Parse(txtXtTP3.Text); }
                if (int.TryParse(txtXtTP4.Text, out xtp4)) { xtp4 = int.Parse(txtXtTP4.Text); }
                if (int.TryParse(txtXtTP5.Text, out xtp5)) { xtp5 = int.Parse(txtXtTP5.Text); }
                if (int.TryParse(txtXtTP6.Text, out xtp6)) { xtp6 = int.Parse(txtXtTP6.Text); }
                if (int.TryParse(txtXtTP7.Text, out xtp7)) { xtp7 = int.Parse(txtXtTP7.Text); }
                if (int.TryParse(txtXtTP8.Text, out xtp8)) { xtp8 = int.Parse(txtXtTP8.Text); }
                List<int> yVal = new List<int> { xtp1, xtp2, xtp3, xtp4, xtp5, xtp6, xtp7, xtp8 };
                //convert and get the value of TP1
                int tp1, tp2, tp3, tp4, tp5, tp6, tp7, tp8;
                if (int.TryParse(txtTP1.Text, out tp1)) { tp1 = int.Parse(txtTP1.Text); }
                if (int.TryParse(txtTP2.Text, out tp2)) { tp2 = int.Parse(txtTP2.Text); }
                if (int.TryParse(txtTP3.Text, out tp3)) { tp3 = int.Parse(txtTP3.Text); }
                if (int.TryParse(txtTP4.Text, out tp4)) { tp4 = int.Parse(txtTP4.Text); }
                if (int.TryParse(txtTP5.Text, out tp5)) { tp5 = int.Parse(txtTP5.Text); }
                if (int.TryParse(txtTP6.Text, out tp6)) { tp6 = int.Parse(txtTP6.Text); }
                if (int.TryParse(txtTP7.Text, out tp7)) { tp7 = int.Parse(txtTP7.Text); }
                if (int.TryParse(txtTP8.Text, out tp8)) { tp8 = int.Parse(txtTP8.Text); }

                List<int> xVal = new List<int> { tp1, tp2, tp3, tp4, tp5, tp6, tp7, tp8 };
                for (int i = 0; i < yVal.Count; i++)
                {
                    series1.Points.AddXY(xVal[i], yVal[i]);
                }
            }
            catch (Exception exobj) { MessageBox.Show(exobj.Message); }
        }


        private void mLicenseUpdate_Click(object sender, EventArgs e)
        {
            LicenseUpdate DAUSR = null;
            if ((DAUSR = (LicenseUpdate)IsFormAlreadyOpen(typeof(LicenseUpdate))) == null)
            {
                DAUSR = new LicenseUpdate();
                DAUSR.Show();
            }
            else
            {
                MessageBox.Show("Page is already open!");
            }
        }

        //generate the reports based on selected date range
        private void btnReportDate_Click(object sender, EventArgs e)
        {
            //check eighter the from value is not greater than todate
            if (toDate.Value.Date <= fromDate.Value.Date)
            {
                MessageBox.Show("From Date can't be greater than to date!");
            }
            else
            {
                //load the reports
                TheRiverRpt();
                TheRiverRptDecreation();
                CalculateTheTyears();
                //show the graph
                PercentageChart("R");
            }
        }

        private void lblBRight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/sarwar.aminy");
        }

        private void logoutSystemMenue_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void mExitAPP_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void returnPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PercentageChart("R");
        }

        private void percentageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PercentageChart("P");
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadProvences("P");
        }

        private void cmbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRiverCmb("P");
        }
        private void cmbRptCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadProvences("R");
        }

        private void mAddRiverInfo_Click(object sender, EventArgs e)
        {
            RiversInfo rvrinfo = null;
            if ((rvrinfo = (RiversInfo)IsFormAlreadyOpen(typeof(RiversInfo))) == null)
            {
                rvrinfo = new RiversInfo();
                rvrinfo.Show();
            }
            else
            {
                MessageBox.Show("The River information page is already opepn!");
            }
        }

        private void cmbRptCountry_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            loadProvences("R");
        }

        private void cmbRptDistric_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRiverCmb("R");
            loadToValTbl();
        }

        private void btnImportToPDF_Click(object sender, EventArgs e)
        {
            if (dataGridRiver.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = cmbRiverSel.Text.ToString() + " Calculation.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk. " + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        //load data first to datagirds
                        loadBottomOptsToDG();
                        loadTopOptsToDG();
                        loadTopRightOptsToDG();
                        try
                        {
                            //yearly datagrid
                            PdfPTable pdfTable = new PdfPTable(dataGridRiver.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 80;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridRiver.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridRiver.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            //top options datagrid
                            PdfPTable pdfTable2 = new PdfPTable(2);
                            pdfTable2.DefaultCell.Padding = 3;
                            pdfTable2.WidthPercentage = 80;
                            pdfTable2.HorizontalAlignment = Element.ALIGN_LEFT;
                            //get rows
                            foreach (DataGridViewRow row2 in dataGridRptOptsT.Rows)
                            {
                                foreach (DataGridViewCell cell2 in row2.Cells)
                                {
                                    pdfTable2.AddCell(cell2.Value.ToString());
                                }
                            }

                            //bottom options datagrid
                            PdfPTable pdfTable3 = new PdfPTable(2);
                            pdfTable3.DefaultCell.Padding = 3;
                            pdfTable3.WidthPercentage = 80;
                            pdfTable3.HorizontalAlignment = Element.ALIGN_LEFT;
                            
                            //get rows
                            foreach (DataGridViewRow row3 in dataGridRptOptsB.Rows)
                            {
                                foreach (DataGridViewCell cell3 in row3.Cells)
                                {
                                    pdfTable3.AddCell(cell3.Value.ToString());
                                }
                            }

                            //Top right options datagrid
                            PdfPTable pdfTable4 = new PdfPTable(dataGridRptOptsTR.Columns.Count);
                            pdfTable4.DefaultCell.Padding = 3;
                            pdfTable4.WidthPercentage = 100;
                            pdfTable4.HorizontalAlignment = Element.ALIGN_RIGHT;
                            //get rows
                            foreach (DataGridViewRow row4 in dataGridRptOptsTR.Rows)
                            {
                                foreach (DataGridViewCell cell4 in row4.Cells)
                                {
                                    pdfTable4.AddCell(cell4.Value.ToString());
                                }
                            }

                            //add some static table
                            PdfPTable pdfS1 = new PdfPTable(2);
                            pdfS1.DefaultCell.Padding = 3;
                            pdfS1.WidthPercentage = 22;
                            pdfS1.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfS1.AddCell(new Phrase(lblN.Text));
                            pdfS1.AddCell(new Phrase(txtTimePerN.Text));
                            pdfS1.AddCell(new Phrase(lblYn.Text));
                            pdfS1.AddCell(new Phrase(txtYn.Text));
                            pdfS1.AddCell(new Phrase(lblSn.Text));
                            pdfS1.AddCell(new Phrase(txtSn.Text));

                            //decreation datagrid
                            PdfPTable pdfTable1 = new PdfPTable(dataGridDecreation.Columns.Count);
                            pdfTable1.DefaultCell.Padding = 3;
                            pdfTable1.WidthPercentage = 100;
                            pdfTable1.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column1 in dataGridDecreation.Columns)
                            {
                                PdfPCell cell1 = new PdfPCell(new Phrase(column1.HeaderText));
                                pdfTable1.AddCell(cell1);
                            }

                            foreach (DataGridViewRow row1 in dataGridDecreation.Rows)
                            {
                                foreach (DataGridViewCell cell1 in row1.Cells)
                                {
                                    pdfTable1.AddCell(cell1.Value.ToString());
                                }
                            }

                            
                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                //
                                pdfDoc.Add(pdfS1);
                                //add our first table
                                pdfDoc.Add(pdfTable4);
                                //line break
                                pdfDoc.Add(new iTextSharp.text.Paragraph("\n"));
                                //graph report--------------------------------------------------------------------------
                                //Call first Graph to be created
                                PercentageChart("R");
                                MemoryStream memoryStream = new MemoryStream();
                                pctChart.SaveImage(memoryStream, ChartImageFormat.Png);
                                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(memoryStream.GetBuffer());
                                //img.ScalePercent(77f);
                                img.ScaleToFit(370f, 370f);
                                img.Alignment = iTextSharp.text.Image.TEXTWRAP | iTextSharp.text.Image.ALIGN_RIGHT;
                                img.IndentationLeft = 1;
                                img.SpacingAfter = 1;
                                
                                pdfDoc.Add(img);
                                
                                //--------------------------------------------------------------------------------------
                                //add new boxed value
                                pdfDoc.Add(pdfTable2);
                                //pdfDoc.Add(new iTextSharp.text.Paragraph("\n"));
                                pdfDoc.Add(pdfTable);
                                //pdfDoc.Add(new iTextSharp.text.Paragraph("\n"));
                                pdfDoc.Add(pdfTable3);
                                pdfDoc.NewPage();
                                //call the graph2 to be created and then import it to the pdf
                                PercentageChart("P");
                                //graph report 2--------------------------------------------------------------------------
                                MemoryStream memoryStream1 = new MemoryStream();
                                pctChart.SaveImage(memoryStream1, ChartImageFormat.Png);
                                iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(memoryStream1.GetBuffer());
                                img1.ScalePercent(90f);
                                img1.Alignment = Right;
                                pdfDoc.Add(img1);
                                //--------------------------------------------------------------------------------------
                                pdfDoc.Add(pdfTable1);
                                pdfDoc.Close();
                                stream.Close();
                                //Call back the return graph
                                PercentageChart("R");
                            }

                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex) { MessageBox.Show("Error :" + ex.Message); }
                    }
                }
            }
            else { MessageBox.Show("No Record To Export !!!", "Info"); }
        }

        private void cmbProvence_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDistric("P");
        }

        private void cmbRptProvence_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDistric("R");
        }

        private void mmExportXlsx_Click(object sender, EventArgs e)
        {
            try
            {
                //Initial and Restore Directories
                saveExcelFileDialog.InitialDirectory = @"C:\";
                saveExcelFileDialog.RestoreDirectory = true;
                //Title
                saveExcelFileDialog.Title = "Export a sample of Rivers Calculator excel file";
                //Default Extension
                saveExcelFileDialog.DefaultExt = "xlsx";
                //Filter and Filter Index
                saveExcelFileDialog.Filter = "Excel workbook (*.xlsx)|*.xlsx";
                saveExcelFileDialog.FilterIndex = 1;
                saveExcelFileDialog.FileName = ConString.excelName;
                if (saveExcelFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // then copy the database into this folder
                    string SourceDir = @Path.GetDirectoryName(saveExcelFileDialog.FileName);
                    string fname = @ConString.excelName;
                    string folderPath = "";
                    folderPath = Application.StartupPath;
                    System.IO.File.Copy(Path.Combine(folderPath, fname), Path.Combine(SourceDir, saveExcelFileDialog.FileName), true);
                }
            }
            catch (IOException ioex)
            {
                MessageBox.Show(ioex.Message);
            }
        }

        private void mmUploadXlsx_Click(object sender, EventArgs e)
        {
            ExcelUpload xlsUpload = null;
            if ((xlsUpload = (ExcelUpload)IsFormAlreadyOpen(typeof(ExcelUpload))) == null)
            {
                xlsUpload = new ExcelUpload();
                xlsUpload.Show();
            }
            else
            {
                MessageBox.Show("The excel uplad page is already opepn!");
            }
        }

        private void addCountryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewCountry addCntry = null;
            if ((addCntry = (AddNewCountry)IsFormAlreadyOpen(typeof(AddNewCountry))) == null)
            {
                addCntry = new AddNewCountry();
                addCntry.Show();
            }
            else
            {
                MessageBox.Show("The add new Country page is already opepn!");
            }
        }

    }
}
