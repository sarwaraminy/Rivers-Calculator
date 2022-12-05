using System;
using System.Windows.Forms;
using System.Data.OleDb;

namespace RiversCalculator
{
    class ConnectionString
    {
        public string dbName;
        public string excelName;
        string loginFileName = "loginuser.dat";
        string folderName = "Rivers Calculator";
        public string folderPath; 
        public string dbConString;
        public string xlsxConString;
        public string loginUser;
        public string superUser;
        public ConnectionString()
        {
            folderPath = @Application.StartupPath.Substring(0,3) + @folderName;
            dbName = "Rivers_Calculator.mdb";
            excelName = "Rivers_Calculator.xlsx";
            dbConString= "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                      "Data Source=" + folderPath + "\\" + dbName + "; Jet OLEDB:Database Password=aaAA11!!;";
            xlsxConString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + folderPath + "\\" + excelName + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            loginUser = folderPath + "\\" + loginFileName;
            superUser = "admin";
        }

        //This methode excute the sql commond
        public void excuteMyQuery(string myQuery)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(dbConString);
                OleDbCommand cmd = new OleDbCommand(myQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exobj) { MessageBox.Show(exobj.Message); }
        }

    }
}
