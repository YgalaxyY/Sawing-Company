using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SewingFactoryManagementSystem
{
    public partial class ProductDetailsForBook : Form
    {

        CURDQueryClass curd = new CURDQueryClass();
        string value = "";
        string orderID = "";

        public ProductDetailsForBook(string id,string orderId)
        {
            InitializeComponent();
            value = id;
            orderID = orderId;


        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        int canmo = 0;
        int xC = 0;
        int yC = 0;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canmo == 1) {

                this.SetDesktopLocation(MousePosition.X - xC, MousePosition.Y - yC);

            }   
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canmo = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canmo = 1;
            xC = e.X;
            yC = e.Y;

        }

        private void CardetailsForBook_Load(object sender, EventArgs e)
        {
            string sqlCode = "SELECT * FROM product WHERE productId='" + value + "'";
            MySqlDataReader da = curd.SelectQueryOutMsg(sqlCode);
            

            

            while (da.Read())
            {

                monoFlat_TextBox1.Text = da.GetString("productId");
                monoFlat_TextBox2.Text = da.GetString("productModel");
                monoFlat_TextBox3.Text = da.GetString("productBrand");
                monoFlat_TextBox4.Text = da.GetString("productMakeYear");
                monoFlat_TextBox5.Text = da.GetString("productColor");
                monoFlat_TextBox6.Text = da.GetString("productDailyRate");
                monoFlat_TextBox7.Text = da.GetString("productMonthlyRate");
                monoFlat_TextBox9.Text = da.GetString("stock");

               
            }


        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canmo == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 315, MousePosition.Y - 15);

            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canmo = 0;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            canmo = 1;
            xC = e.X;
            yC = e.Y;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
