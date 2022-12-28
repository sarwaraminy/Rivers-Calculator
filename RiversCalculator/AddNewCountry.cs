using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RiversCalculator
{
    public partial class AddNewCountry : Form
    {
        //create the instance for connection string and login user path
        ConnectionString ConString = new ConnectionString();
        String loginuser;
        RiversCalculator form1 = new RiversCalculator();

        public AddNewCountry()
        {
            InitializeComponent();
        }

        private void btnAddCntry_Click(object sender, EventArgs e)
        {
            loginuser = File.ReadAllText(ConString.loginUser).Trim();
            if (txtNewCntry.Text == "") { MessageBox.Show("Please enter country ID"); txtNewCntry.Focus(); }
            else if (txtCntryName.Text == "") { MessageBox.Show("Please enter country Name"); txtCntryName.Focus(); }
            else if (txtCntryISO.Text == "") { MessageBox.Show("Please enter country ISO"); txtCntryISO.Focus(); }
            else
            {
                try
                {
                    String cntryQAdd = "INSERT INTO COUNTRYTBL(COUNTRY,USER_ID,ISO,CNAME) VALUES  ('" +txtNewCntry.Text+"','"+loginuser+"','"+txtCntryISO.Text+"','"+txtCntryName.Text+"')";
                    ConString.excuteMyQuery(cntryQAdd);
                    lblMessage.Text = "A record is added to Country table!";
                    txtCntryISO.Text = "";
                    txtCntryName.Text = "";
                    txtNewCntry.Text = "";
                    this.Dispose();
                    Application.Restart();
                }
                catch { lblMessage.Text = "Record is not inserted!"; }
            }
        }

        private void AddNewCountry_Load(object sender, EventArgs e)
        {
            txtNewCntry.MaxLength = 2;
            txtCntryISO.MaxLength = 3;
            txtCntryName.MaxLength = 50;
            lblBRight.Text = ConString.copyRight;
        }

        private void lblBRight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/sarwar.aminy");
        }
    }
}
