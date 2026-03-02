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
    public partial class OrderAvaliable : Form
    {
        public OrderAvaliable()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            monoFlat_TextBox1.Text = "";
            this.Hide();
        }

        int canMove;
        int xCo;
        int yCo;

        CURDQueryClass curdFunction = new CURDQueryClass();

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            canMove = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1) {

                this.SetDesktopLocation(MousePosition.X - xCo, MousePosition.Y - yCo);

            }

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            xCo = e.X;
            yCo = e.Y;
        }

        private void OrderAvaliable_Load(object sender, EventArgs e)
        {
            bunifuFlatButton1.Enabled = false;
            getDataFromTheDB();
        }

        public void getDataFromTheDB()
        {
            DataTable data = new DataTable();
            data.Columns.Add("Номер заказа");
            data.Columns.Add("ID клиента");
            data.Columns.Add("ID сотрудника");
            data.Columns.Add("Дата заказа");
            data.Columns.Add("Дата выполнения");
            data.Columns.Add("Зарплата сотрудника");
            data.Columns.Add("Значение остатка");
            data.Columns.Add("Общее количество");
            data.Columns.Add("Остаток");
            data.Columns.Add("Зарплата закройщика");
            data.Columns.Add("Общая сумма");
            data.Columns.Add("Значение счетчика");
            data.Columns.Add("Общее количество");
            data.Columns.Add("Сумма скидки");
            data.Columns.Add("Процент скидки");

            string querySelect = "SELECT * FROM orders";

            MySqlDataReader datas = curdFunction.SelectQuery(querySelect);

            while (datas.Read())
            {
                data.Rows.Add(
                    datas.IsDBNull(0) ? "" : datas.GetString(0), // OrderId
                    datas.IsDBNull(1) ? "" : datas.GetString(1), // productId
                    datas.IsDBNull(2) ? "" : datas.GetString(2), // cusId
                    datas.IsDBNull(3) ? "" : datas.GetString(3), // workerId
                    datas.IsDBNull(4) ? "" : datas.GetString(4), // orderDate
                    datas.IsDBNull(5) ? "" : datas.GetString(5), // completionDate
                    datas.IsDBNull(6) ? "" : datas.GetString(6), // basis
                    datas.IsDBNull(7) ? "" : datas.GetInt32(7).ToString(), // duration
                    datas.IsDBNull(8) ? "" : datas.GetDouble(8).ToString(), // advancedAmount
                    datas.IsDBNull(9) ? "" : datas.GetDouble(9).ToString(), // balance
                    datas.IsDBNull(10) ? "" : datas.GetDouble(10).ToString(), // workerAmount
                    datas.IsDBNull(11) ? "" : datas.GetDouble(11).ToString(), // totalAmount
                    datas.IsDBNull(12) ? "" : datas.GetString(12), // startQuantity
                    datas.IsDBNull(13) ? "" : datas.GetString(13), // totalQuantity
                    datas.IsDBNull(14) ? "" : datas.GetDouble(14).ToString(), // discountAmount
                    datas.IsDBNull(15) ? "" : datas.GetDouble(15).ToString()  // discountPer
                );
            }

            // Закрытие DataReader и освобождение ресурсов
            datas.Close();

            bunifuCustomDataGrid1.DataSource = data;
        }



        private void bunifuCustomDataGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          

            
           
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {

            if (bunifuTextbox1.text != string.Empty)
            {

                ComboBoxSearch(bunifuTextbox1.text);
            }
            else {


                getDataFromTheDB();
            }
                 
        }

        public void ComboBoxSearch(string status)
        {

            DataTable dTable1 = new DataTable();
            dTable1.Columns.Add("Order ID");
            dTable1.Columns.Add("Product ID");
            dTable1.Columns.Add("ID изделия");
            dTable1.Columns.Add("Worker ID");
            dTable1.Columns.Add("Дата заказа");
            dTable1.Columns.Add("Дата выполнения");
            dTable1.Columns.Add("Зарплата сотрудника");
            dTable1.Columns.Add("Значение остатка");
            dTable1.Columns.Add("Общее количество");
            dTable1.Columns.Add("Blance");
            dTable1.Columns.Add("Worker Salary");
            dTable1.Columns.Add("Total Amount");
            dTable1.Columns.Add("Stock Value");
            dTable1.Columns.Add("Total Given Quantity");
            dTable1.Columns.Add("Discount Percentage");

            
            string queryForBrand = "SELECT * FROM orders WHERE orderId like'" + status + "%'";
            MySqlDataReader dataBrand = curdFunction.SelectQuery(queryForBrand);

                while (dataBrand.Read())
                {

                dTable1.Rows.Add(dataBrand.GetString("OrderId"), dataBrand.GetString("productId"), dataBrand.GetString("cusId"), dataBrand.GetString("workerId"), dataBrand.GetString("orderDate"), dataBrand.GetString("completionDate"), dataBrand.GetString("basis"), dataBrand.GetString("duration"), dataBrand.GetString("advancedAmount"), dataBrand.GetString("blance"), dataBrand.GetString("workerAmount"), dataBrand.GetString("totalAmount"), dataBrand.GetString("startQuantity"), dataBrand.GetString("totalQuantity"), dataBrand.GetString("DiscountAmount"), dataBrand.GetString("DiscountPer"));

            }

           

            bunifuCustomDataGrid1.DataSource = dTable1;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 455, MousePosition.Y - 17);

            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1_MouseDown(sender, e);
        }

        public string current = "";

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bunifuCustomDataGrid1.CurrentCell.ColumnIndex.Equals(0))
            {

                current = bunifuCustomDataGrid1.CurrentCell.Value.ToString();

                monoFlat_TextBox1.Text = current;

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            monoFlat_TextBox1.Text = "";
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void monoFlat_TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (monoFlat_TextBox1.Text != "")
            {

                bunifuFlatButton1.Enabled = true;

            }
            else {

                bunifuFlatButton1.Enabled = false;

            }
        }
    }
}
