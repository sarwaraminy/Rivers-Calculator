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
using System.Data.SqlClient;
using System.Threading;

namespace RiversCalculator
{
    public partial class ExcelUpload : Form
    {
        ConnectionString sa_conCls = new ConnectionString();
        //Create some variables
        OleDbConnection con;
        OleDbCommand cmd;
        string loginuser;

        //create the connectionClass
        ConnectionString ConString = new ConnectionString();

        public ExcelUpload()
        {
            InitializeComponent();
        }


        private void ExcelUpload_Load(object sender, EventArgs e)
        {
            loginuser = File.ReadAllText(ConString.loginUser).Trim();
        }

        private void lblBRight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/sarwar.aminy");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                openXlsxFileDialog.InitialDirectory = @Application.StartupPath.Substring(0, 3);
                openXlsxFileDialog.DefaultExt = "xlsx";
                openXlsxFileDialog.Filter = "Excel workbook (*.xlsx)|*.xlsx";
                openXlsxFileDialog.FilterIndex = 1;
                openXlsxFileDialog.RestoreDirectory = true;
                openXlsxFileDialog.ShowReadOnly = true;
                openXlsxFileDialog.ShowReadOnly = true;
                if (openXlsxFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // then copy the database into this folder
                    string SourceDir = @ConString.folderPath;
                    string fname = ConString.excelName;
                    string folderPath = @Path.GetDirectoryName(openXlsxFileDialog.FileName);
                    System.IO.File.Copy(Path.Combine(folderPath, openXlsxFileDialog.FileName), Path.Combine(SourceDir, fname), true);
                    lblMessage.Text = "Excel File successfully uploaded!";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch (IOException ioex)
            {
                MessageBox.Show(ioex.Message);
            }
        }

 
        private void btnLoadToDb_Click(object sender, EventArgs e)
        {
            try
            {
                ptcrBox1.Show();
                //Clear box
                lblMessage.Text = "";
                //check the excel file is exists
                if (File.Exists(@ConString.folderPath + "\\" + ConString.excelName))
                {
                    //----------------------------------------------------------countrytbl
                    //First clear our tem table - TMP_COUNTRYTBL
                    //delete privios data
                    string clearCntry = "DELETE FROM TMP_COUNTRYTBL";
                    sa_conCls.excuteMyQuery(clearCntry);
                    //Load data from excel to tmp_country table
                    xlsxLoadTMP_CountryTBL();
                    //now load data from TMP_COUNTRYTBL to countrytbl
                    loadFromTMPtoCNTRY();
                    //------End-------------------------------------------------countrytbl

                    //----------------------------------------------------------rivers
                    //First clear our tem table - TMP_RIVERS
                    //delete privios data
                    string clearRiversD = "DELETE FROM TMP_RIVERS";
                    sa_conCls.excuteMyQuery(clearRiversD);
                    //Load data from excel to tmp_rivers table
                    xlsxLoadTMP_Rivers();
                    //now load data from TMP_RIVERS to RIVERS
                    loadFromTMPtoRiver();
                    //------End-------------------------------------------------RIVERS

                    //----------------------------------------------------------riversinfo
                    //First clear our tem table - TMP_RIVERSINFO
                    //delete privios data
                    string clearRiversI = "DELETE FROM TMP_RIVERSINFO";
                    sa_conCls.excuteMyQuery(clearRiversI);
                    //Load data from excel to tmp_rivers table
                    xlsxLoadTMP_Riversinfo();
                    //now load data from TMP_RIVERS to RIVERS
                    loadFromTMPtoRiverinfo();
                    //------End-------------------------------------------------RIVERSINFO
                    
                    lblMessage.Text = "Data has been Imported successfully.";
                    ptcrBox1.Hide();
                }
                else { lblMessage.Text = "Excel File is missing"; }
            }
            catch (Exception exobj)
            {
                MessageBox.Show(string.Format("Data has not been Imported due to :{0}", exobj.Message), "Not Imported", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        //This methode excute the sql commond
        /*private void excuteMyQuery(string myQuery)
        {
            try
            {
                con = new OleDbConnection(ConString.dbConString);
                cmd = new OleDbCommand(myQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exobj) { MessageBox.Show(exobj.Message); }
        }*/

        //Load data from excel.Country sheet to tmp_countrytbl
        private void xlsxLoadTMP_CountryTBL()
        {
            try
            {
                string xlsxQ = "SELECT * from [country_table$]";
                //connect to database and load
                OleDbConnection conXLS = new OleDbConnection(ConString.xlsxConString);
                //Connect to access database
                con = new OleDbConnection(ConString.dbConString);
                //command excel
                OleDbCommand cmdXLSX = conXLS.CreateCommand();
                cmdXLSX.CommandType = CommandType.Text;
                cmdXLSX.CommandText = xlsxQ;
                //Create the access insert Query
                string accessQ = "INSERT INTO TMP_COUNTRYTBL(COUNTRY, USER_ID, ISO, CNAME) VALUES(@CID, @USERID, @ISO, @CNAME)";
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = accessQ;
                //Create parameters
                OleDbParameter CID = new OleDbParameter("@CID", OleDbType.VarChar);
                cmd.Parameters.Add(CID);
                OleDbParameter USERID = new OleDbParameter("@USERID", OleDbType.VarChar);
                cmd.Parameters.Add(USERID);
                OleDbParameter ISO = new OleDbParameter("@ISO", OleDbType.VarChar);
                cmd.Parameters.Add(ISO);
                OleDbParameter CNAME = new OleDbParameter("@CNAME", OleDbType.VarChar);
                cmd.Parameters.Add(CNAME);
                conXLS.Open();
                con.Open();
                //Read Excel
                OleDbDataReader drXLS = cmdXLSX.ExecuteReader();
                while (drXLS.Read())
                {
                    CID.Value = drXLS[0].ToString();
                    USERID.Value = loginuser;
                    ISO.Value = drXLS[2].ToString();
                    CNAME.Value = drXLS[1].ToString();
                    //insert values to database
                    cmd.ExecuteNonQuery();
                }
                // close connections
                conXLS.Close();
                con.Close();
            }
            catch (Exception exobj)
            {
                MessageBox.Show(string.Format("Data has not been imported due to :{0}", exobj.Message), "Not Imported", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }

        //now insert data from TMP_COUNTRYTBL to COUNTRYTBL
        private void loadFromTMPtoCNTRY()
        {
            try
            {
                string CntryQ = "INSERT INTO COUNTRYTBL (COUNTRY, USER_ID, ISO, CNAME) SELECT t1.COUNTRY, t1.USER_ID, t1.ISO, t1.CNAME FROM "+
                    "TMP_COUNTRYTBL t1 WHERE NOT EXISTS (SELECT COUNTRY, USER_ID FROM COUNTRYTBL t2 WHERE t2.COUNTRY=t1.COUNTRY AND t2.USER_ID=t1.USER_ID)";
                //Now excute the query
                sa_conCls.excuteMyQuery(CntryQ); 
            }
            catch (Exception exobj)
            {
                MessageBox.Show(string.Format("Data has not been imported due to :{0}", exobj.Message), "Not Imported", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Load data from excel.Rivers sheet to tmp_rivers
        private void xlsxLoadTMP_Rivers()
        {
            try
            {
                string xlsxQ = "SELECT * from [rivers_table$]";
                //connect to database and load
                OleDbConnection conXLS = new OleDbConnection(ConString.xlsxConString);
                //Connect to access database
                con = new OleDbConnection(ConString.dbConString);
                //command excel
                OleDbCommand cmdXLSX = conXLS.CreateCommand();
                cmdXLSX.CommandType = CommandType.Text;
                cmdXLSX.CommandText = xlsxQ;
                //Create the access insert Query
                string accessQ = "INSERT INTO TMP_RIVERS(RIVER_ID,[YEAR],USER_ID,RIVER_NAME,STATION_NAME,STATION_ID,CA,M3_PER_SECOND,[YEAR_DATE])"
                    + " VALUES(@RID,@YEAR,@UID,@RNAME,@SNAME,@SID,@CA,@M3,@YDATE)";
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = accessQ;
                //Create parameters
                OleDbParameter RID = new OleDbParameter("@RID", OleDbType.VarChar);
                cmd.Parameters.Add(RID);
                OleDbParameter YEAR = new OleDbParameter("@YEAR", OleDbType.VarChar);
                cmd.Parameters.Add(YEAR);
                OleDbParameter UID = new OleDbParameter("@UID", OleDbType.VarChar);
                cmd.Parameters.Add(UID);
                OleDbParameter RNAME = new OleDbParameter("@RNAME", OleDbType.VarChar);
                cmd.Parameters.Add(RNAME);
                OleDbParameter SNAME = new OleDbParameter("@SNAME", OleDbType.VarChar);
                cmd.Parameters.Add(SNAME);
                OleDbParameter SID = new OleDbParameter("@SID", OleDbType.VarChar);
                cmd.Parameters.Add(SID);
                OleDbParameter CA = new OleDbParameter("@CA", OleDbType.VarChar);
                cmd.Parameters.Add(CA);
                OleDbParameter M3 = new OleDbParameter("@M3", OleDbType.VarChar);
                cmd.Parameters.Add(M3);
                OleDbParameter YDATE = new OleDbParameter("@YDATE", OleDbType.VarChar);
                cmd.Parameters.Add(YDATE);
                conXLS.Open();
                con.Open();
                //Read Excel
                OleDbDataReader drXLS = cmdXLSX.ExecuteReader();
                while (drXLS.Read())
                {
                    RID.Value = drXLS[0].ToString();
                    YEAR.Value = drXLS[1].ToString().Substring(0,4);
                    UID.Value = loginuser;
                    RNAME.Value = drXLS[2].ToString();
                    SNAME.Value = drXLS[3].ToString();
                    SID.Value = drXLS[4].ToString();
                    CA.Value = drXLS[5].ToString();
                    M3.Value = drXLS[6].ToString();
                    YDATE.Value = "01/01/" + drXLS[1].ToString().Substring(0, 4);
                    //insert values to database
                    cmd.ExecuteNonQuery();
                }
                // close connections
                conXLS.Close();
                con.Close();
            }
            catch (Exception exobj)
            {
                MessageBox.Show(string.Format("Data has not been imported due to :{0}", exobj.Message), "Not Imported", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //now insert data from TMP_RIVERS to RIVERS
        private void loadFromTMPtoRiver()
        {
            try
            {
                string CntryQ = "INSERT INTO RIVERS (RIVER_ID,[YEAR], USER_ID, RIVER_NAME, STATION_NAME, STATION_ID,CA,M3_PER_SECOND,[YEAR_DATE]) "+ 
                    " SELECT t1.RIVER_ID, t1.YEAR, t1.USER_ID, t1.RIVER_NAME, t1.STATION_NAME, t1.STATION_ID, t1.CA, t1.M3_PER_SECOND,t1.YEAR_DATE FROM " +
                    "TMP_RIVERS t1 WHERE NOT EXISTS (SELECT RIVER_ID, YEAR, USER_ID FROM RIVERS t2 WHERE t2.RIVER_ID=t1.RIVER_ID AND t2.YEAR=t1.YEAR AND t2.USER_ID=t1.USER_ID)";
                //Now excute the query
                sa_conCls.excuteMyQuery(CntryQ);
            }
            catch (Exception exobj)
            {
                MessageBox.Show(string.Format("Data has not been imported due to :{0}", exobj.Message), "Not Imported", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Load data from excel.Riversdata sheet to tmp_riversinfo
        private void xlsxLoadTMP_Riversinfo()
        {
            try
            {
                string xlsxQ = "SELECT * from [riversdata_table$]";
                //connect to database and load
                OleDbConnection conXLS = new OleDbConnection(ConString.xlsxConString);
                //Connect to access database
                con = new OleDbConnection(ConString.dbConString);
                //command excel
                OleDbCommand cmdXLSX = conXLS.CreateCommand();
                cmdXLSX.CommandType = CommandType.Text;
                cmdXLSX.CommandText = xlsxQ;
                //Create the access insert Query
                string accessQ = "INSERT INTO TMP_RIVERSINFO (COUNTRY,PCODE,DCODE,RIVER_ID,CNAME,PNAME,DNAME,RNAME,USER_ID,REG_DATE)"
                    + " VALUES(@CID,@PID,@DID,@RID,@CNAME,@PNAME,@DNAME,@RNAME,@UID,@RDATE)";
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = accessQ;
                //Create parameters
                OleDbParameter CID = new OleDbParameter("@CID", OleDbType.VarChar);
                cmd.Parameters.Add(CID);
                OleDbParameter PID = new OleDbParameter("@PID", OleDbType.VarChar);
                cmd.Parameters.Add(PID);
                OleDbParameter DID = new OleDbParameter("@DID", OleDbType.VarChar);
                cmd.Parameters.Add(DID);
                OleDbParameter RID = new OleDbParameter("@RID", OleDbType.VarChar);
                cmd.Parameters.Add(RID);
                OleDbParameter CNAME = new OleDbParameter("@CNAME", OleDbType.VarChar);
                cmd.Parameters.Add(CNAME);
                OleDbParameter PNAME = new OleDbParameter("@PNAME", OleDbType.VarChar);
                cmd.Parameters.Add(PNAME);
                OleDbParameter DNAME = new OleDbParameter("@DNAME", OleDbType.VarChar);
                cmd.Parameters.Add(DNAME);
                OleDbParameter RNAME = new OleDbParameter("@RNAME", OleDbType.VarChar);
                cmd.Parameters.Add(RNAME);
                OleDbParameter UID = new OleDbParameter("@UID", OleDbType.VarChar);
                cmd.Parameters.Add(UID);
                OleDbParameter RDATE = new OleDbParameter("@RDATE", OleDbType.Date);
                cmd.Parameters.Add(RDATE);
                conXLS.Open();
                con.Open();
                //Read Excel
                OleDbDataReader drXLS = cmdXLSX.ExecuteReader();
                while (drXLS.Read())
                {
                    CID.Value = drXLS[0].ToString();
                    PID.Value = drXLS[1].ToString();
                    DID.Value = drXLS[2].ToString();
                    RID.Value = drXLS[3].ToString();
                    CNAME.Value = drXLS[4].ToString();
                    PNAME.Value = drXLS[5].ToString();
                    DNAME.Value = drXLS[6].ToString();
                    RNAME.Value = drXLS[7].ToString();
                    UID.Value = loginuser;
                    RDATE.Value = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();

                    //insert values to database
                    cmd.ExecuteNonQuery();
                }
                // close connections
                conXLS.Close();
                con.Close();
            }
            catch (Exception exobj)
            {
                MessageBox.Show(string.Format("Data has not been imported due to :{0}", exobj.Message), "Not Imported", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //now insert data from TMP_RIVERSINFO to RIVERSINFO
        private void loadFromTMPtoRiverinfo()
        {
            try
            {
                string CntryQ = "INSERT INTO RIVERSINFO (COUNTRY,PCODE,DCODE,RIVER_ID,CNAME,PNAME,DNAME,RNAME,USER_ID,REG_DATE) " +
                    " SELECT t1.COUNTRY, t1.PCODE, t1.DCODE, t1.RIVER_ID, t1.CNAME, t1.PNAME, t1.DNAME, t1.RNAME,t1.USER_ID,t1.REG_DATE FROM " +
                    " TMP_RIVERSINFO t1 WHERE NOT EXISTS (SELECT COUNTRY, PCODE, DCODE, RIVER_ID FROM RIVERSINFO t2 WHERE t2.COUNTRY=t1.COUNTRY " +
                    " AND t2.PCODE=t1.PCODE AND t2.DCODE=t1.DCODE AND t2.RIVER_ID=t1.RIVER_ID )";
                //Now excute the query
                sa_conCls.excuteMyQuery(CntryQ);
            }
            catch (Exception exobj)
            {
                MessageBox.Show(string.Format("Data has not been imported due to :{0}", exobj.Message), "Not Imported", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
