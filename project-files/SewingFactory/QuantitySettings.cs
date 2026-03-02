using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SewingFactoryManagementSystem
{
    public partial class QuantitySettings : Form
    {
        public QuantitySettings()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        int can = 0;
        int XC = 0;
        int yC = 0;

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {

            can = 0;


        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            can = 1;
            XC = e.X;
            yC = e.Y;

        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (can == 1) {


                this.SetDesktopLocation(MousePosition.X - XC, MousePosition.Y - yC);

            }

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) {


                monoFlat_TextBox2.Visible = false;
                label5.Visible = false;

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) {


                monoFlat_TextBox2.Visible = true;
                label5.Visible = true;
            }
        }

        private void label4_MouseUp(object sender, MouseEventArgs e)
        {
            can = 0;
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            can = 1;
            XC = e.X;
            yC = e.Y;
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if (can==1) {

                this.SetDesktopLocation(MousePosition.X - 360, MousePosition.Y - 18);

            }
        }

        private void QuantitySettings_Load(object sender, EventArgs e)
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            XmlDocument XD = new XmlDocument();
            XD.Load(path+"\\CRMS\\quantitySettings.xml");

            string radioVal = XD.SelectSingleNode("quantitySettings/quantityType").InnerText;

            if (radioVal == "both") {

                radioButton1.Checked = true;


            }else if (radioVal == "onlyDay")
            {

                radioButton2.Checked = true;

            }

            monoFlat_TextBox1.Text = XD.SelectSingleNode("quantitySettings/perday").InnerText;
            monoFlat_TextBox2.Text = XD.SelectSingleNode("quantitySettings/perMonth").InnerText;

            
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            XmlDocument XDoc = new XmlDocument();
            XDoc.Load(path + "\\CRMS\\quantitySettings.xml");

            if (radioButton1.Checked)
            {

                XDoc.SelectSingleNode("quantitySettings/quantityType").InnerText = "both";

            }
            else if (radioButton2.Checked) {


                XDoc.SelectSingleNode("quantitySettings/quantityType").InnerText = "onlyDay";

            }

            XDoc.SelectSingleNode("quantitySettings/perday").InnerText = monoFlat_TextBox1.Text;
            XDoc.SelectSingleNode("quantitySettings/perMonth").InnerText = monoFlat_TextBox2.Text;

            XDoc.Save(path+ "\\CRMS\\quantitySettings.xml");

            SuccessMSGBox su = new SuccessMSGBox("Changes have been made successfully.");
            su.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
