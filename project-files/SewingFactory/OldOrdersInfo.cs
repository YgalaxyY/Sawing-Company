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
    public partial class OldOrdersInfo : Form
    {
        public OldOrdersInfo()
        {
            InitializeComponent();
        }

        CURDQueryClass curdFunction = new CURDQueryClass();

        private void OldOrdersInfo_Load(object sender, EventArgs e)
        {

            getDataFromTheDBForReturned();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Returned")
            {


                getDataFromTheDBForReturned();


            }
            else if (comboBox1.Text == "Pendding")
            {


                getDataFromTheDBForPendding();
            }

        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Returned")
            {

                if (bunifuTextbox1.text == string.Empty)
                {

                    getDataFromTheDBForReturned();

                }
                else if (comboBox2.Text == "Order ID" && bunifuTextbox1.text != string.Empty)
                {

                    string query = "SELECT * FROM oldorderdata WHERE oderId LIKE '" + bunifuTextbox1.text + "%'";

                    getDataFromTheDBForReturnedSearching(query);

                }
                else if (comboBox2.Text == "Customer ID")
                {

                    string query = "SELECT * FROM oldorderdata WHERE cusId LIKE '" + bunifuTextbox1.text + "%'";
                    getDataFromTheDBForReturnedSearching(query);

                }
                else if (comboBox2.Text == "Product ID")
                {

                    string query = "SELECT * FROM oldorderdata WHERE productId LIKE '" + bunifuTextbox1.text + "%'";
                    getDataFromTheDBForReturnedSearching(query);

                }

            }



            if (comboBox1.Text == "Pendding")
            {

                if (bunifuTextbox1.text == string.Empty)
                {

                    getDataFromTheDBForPendding();

                }
                else if (comboBox2.Text == "Order ID" && bunifuTextbox1.text != string.Empty)
                {

                    string query = "SELECT * FROM orders WHERE orderId LIKE'" + bunifuTextbox1.text + "%'";
                    getDataFromTheDBForPenddingSerching(query);


                }
                else if (comboBox2.Text == "Customer ID" && bunifuTextbox1.text != string.Empty)
                {

                    string query = "SELECT * FROM orders  WHERE cusId LIKE '" + bunifuTextbox1.text + "%'";
                    getDataFromTheDBForPenddingSerching(query);

                }
                else if (comboBox2.Text == "Product ID" && bunifuTextbox1.text != string.Empty)
                {


                    string query = "SELECT * FROM orders WHERE productId LIKE '" + bunifuTextbox1.text + "%'";
                    getDataFromTheDBForPenddingSerching(query);

                }

            }

        }

        public void getDataFromTheDBForPendding()
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
            dTable1.Columns.Add("Discount Amount");
            dTable1.Columns.Add("Discount Percentage");



            string querySelect = "SELECT * FROM orders";

            MySqlDataReader datas = curdFunction.SelectQuery(querySelect);

            while (datas.Read())
            {

                dTable1.Rows.Add(datas.GetString("OrderId"), datas.GetString("productId"), datas.GetString("cusId"), datas.GetString("workerId"), datas.GetString("orderDate"), datas.GetString("completionDate"), datas.GetString("basis"), datas.GetString("duration"), datas.GetString("advancedAmount"), datas.GetString("blance"), datas.GetString("workerAmount"), datas.GetString("totalAmount"), datas.GetString("startQuantity"), datas.GetString("totalQuantity"), datas.GetString("DiscountAmount"), datas.GetString("DiscountPer"));

            }

            bunifuCustomDataGrid1.DataSource = dTable1;

        }

        public void getDataFromTheDBForReturned()
        {
            DataTable data = new DataTable();
            data.Columns.Add("Номер заказа");
            data.Columns.Add("ID изделия");
            data.Columns.Add("ID клиента");
            data.Columns.Add("ID закройщика");
            data.Columns.Add("Дата заказа");
            data.Columns.Add("Дата выполнения");
            data.Columns.Add("Продолжительность");
            data.Columns.Add("Авансовый платеж");
            data.Columns.Add("Остаток");
            data.Columns.Add("Дополнительные дни");
            data.Columns.Add("Сумма за дополнительные дни");
            data.Columns.Add("Дополнительная зарплата закройщика");
            data.Columns.Add("Дополнительное количество");
            data.Columns.Add("Сумма за дополнительное количество");
            data.Columns.Add("Общая сумма");
            data.Columns.Add("Сумма скидки");
            data.Columns.Add("Процент скидки");
            data.Columns.Add("Основание");
            data.Columns.Add("Зарплата закройщика");

            string querySelect = "SELECT * FROM oldorderdata";

            MySqlDataReader datas = curdFunction.SelectQuery(querySelect);

            while (datas.Read())
            {
                data.Rows.Add(
                    datas.IsDBNull(0) ? "" : datas.GetString(0), // oderId
                    datas.IsDBNull(1) ? "" : datas.GetString(1), // productId
                    datas.IsDBNull(2) ? "" : datas.GetString(2), // cusId
                    datas.IsDBNull(3) ? "" : datas.GetString(3), // workerId
                    datas.IsDBNull(4) ? "" : datas.GetString(4), // orderDate
                    datas.IsDBNull(5) ? "" : datas.GetString(5), // completionDate
                    datas.IsDBNull(6) ? "" : datas.GetInt32(6).ToString(), // duration
                    datas.IsDBNull(7) ? "" : datas.GetDouble(7).ToString(), // advancedAmount
                    datas.IsDBNull(8) ? "" : datas.GetDouble(8).ToString(), // balanceAmount
                    datas.IsDBNull(9) ? "" : datas.GetInt32(9).ToString(), // extraDay
                    datas.IsDBNull(10) ? "" : datas.GetDouble(10).ToString(), // extraDayAmount
                    datas.IsDBNull(11) ? "" : datas.GetDouble(11).ToString(), // extraworkerSalary
                    datas.IsDBNull(12) ? "" : datas.GetInt32(12).ToString(), // extraQuantity
                    datas.IsDBNull(13) ? "" : datas.GetDouble(13).ToString(), // extraMileageAmount
                    datas.IsDBNull(14) ? "" : datas.GetDouble(14).ToString(), // TotalAmount
                    datas.IsDBNull(15) ? "" : datas.GetDouble(15).ToString(), // DiscountAmount
                    datas.IsDBNull(16) ? "" : datas.GetString(16), // DiscountPer
                    datas.IsDBNull(17) ? "" : datas.GetDouble(17).ToString(), // basis
                    datas.IsDBNull(18) ? "" : datas.GetString(18) // workersalary
                );
            }

            // Закрытие DataReader и освобождение ресурсов
            datas.Close();

            bunifuCustomDataGrid1.DataSource = data;
        }


        public void getDataFromTheDBForReturnedSearching(string query)
        {

            DataTable data = new DataTable();
            data.Columns.Add("Order ID");
            data.Columns.Add("Product ID");
            data.Columns.Add("Customer ID");
            data.Columns.Add("Worker ID");
            data.Columns.Add("Order Date");
            data.Columns.Add("Completion Date");
            data.Columns.Add("Duration");
            data.Columns.Add("Advanced Amount");
            data.Columns.Add("Balance Amount");
            data.Columns.Add("Extra Days");
            data.Columns.Add("Extra Days Amount");
            data.Columns.Add("Extra Worker Salary");
            data.Columns.Add("Extra Quantity");
            data.Columns.Add("Extra Quantity Amount");
            data.Columns.Add("Total Amount");
            data.Columns.Add("Discount Amount");
            data.Columns.Add("Discount Percentage");
            data.Columns.Add("Basis");
            data.Columns.Add("Worker Salary");


            MySqlDataReader datas = curdFunction.SelectQuery(query);

            while (datas.Read())
            {

                data.Rows.Add(datas.GetString("oderId"), datas.GetString("productId"), datas.GetString("cusId"), datas.GetString("workerId"), datas.GetString("orderDate"), datas.GetString("completionDate"), datas.GetString("Duration"), datas.GetString("advancedAmount"), datas.GetString("balanceAmount"), datas.GetString("extraDay"), datas.GetString("extraDayAmount"), datas.GetString("extraworkerSalary"), datas.GetString("extraQuantity"), datas.GetString("ExtraMileageAmount"), datas.GetString("TotalAmount"), datas.GetString("DiscountAmount"), datas.GetString("DiscountPer"), datas.GetString("basis"), datas.GetString("workersalary"));

            }

            bunifuCustomDataGrid1.DataSource = data;


        }

        public void getDataFromTheDBForPenddingSerching(string query)
        {

            DataTable data = new DataTable();
            data.Columns.Add("Order ID");
            data.Columns.Add("Product ID");
            data.Columns.Add("Customer ID");
            data.Columns.Add("Worker ID");
            data.Columns.Add("Order Date");
            data.Columns.Add("Completion Date");
            data.Columns.Add("Basis");
            data.Columns.Add("Duration");
            data.Columns.Add("Ad.Paid.Amount");
            data.Columns.Add("Blance");
            data.Columns.Add("Worker Salary");
            data.Columns.Add("Total Amount");
            data.Columns.Add("Stock Value");
            data.Columns.Add("Total Given Quantity");
            data.Columns.Add("Discount Amount");
            data.Columns.Add("Discount Percentage");


            MySqlDataReader datas = curdFunction.SelectQuery(query);

            while (datas.Read())
            {

                data.Rows.Add(datas.GetString("OrderId"), datas.GetString("productId"), datas.GetString("cusId"), datas.GetString("workerId"), datas.GetString("orderDate"), datas.GetString("completionDate"), datas.GetString("basis"), datas.GetString("duration"), datas.GetString("advancedAmount"), datas.GetString("blance"), datas.GetString("workerAmount"), datas.GetString("totalAmount"), datas.GetString("startQuantity"), datas.GetString("totalQuantity"), datas.GetString("DiscountAmount"), datas.GetString("DiscountPer"));

            }

            bunifuCustomDataGrid1.DataSource = data;

        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void bunifuCustomDataGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void bunifuCustomDataGrid1_CellContextMenuStripChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        int rowIndex = 0;
        private void bunifuCustomDataGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Показать детали изделия", MenuItemNew_Click));
                m.MenuItems.Add(new MenuItem("Show Customer Details", MenuItemNew2_Click));
                m.MenuItems.Add(new MenuItem("Показать детали закройщика", MenuItemNew3_Click));


                rowIndex = e.RowIndex;

                m.Show(bunifuCustomDataGrid1, bunifuCustomDataGrid1.PointToClient(Cursor.Position));

            }
        }
        private void MenuItemNew_Click(Object sender, System.EventArgs e)
        {

            try
            {
                string indexPosition = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Product ID"].Value.ToString();
                string orderId = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Order ID"].Value.ToString();


                ProductDetailsForBook cus = new ProductDetailsForBook(indexPosition, orderId);
                cus.Show();

            }
            catch (Exception)
            {
                ErrorMsgBox r = new ErrorMsgBox("Please select a record");
                r.Show();
            }
        }

        private void MenuItemNew2_Click(Object sender, System.EventArgs e)
        {

            try
            {
                string indexPosition = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Customer ID"].Value.ToString();
                string orderId = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Order ID"].Value.ToString();


                CustomerDetailsForBookForm cus = new CustomerDetailsForBookForm(indexPosition, orderId);
                cus.Show();

            }
            catch (Exception)
            {
                ErrorMsgBox r = new ErrorMsgBox("Please select a record");
                r.Show();
            }

        }
        private void MenuItemNew3_Click(Object sender, System.EventArgs e)
        {
            try
            {
                string indexPosition = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Worker ID"].Value.ToString();
                string orderId = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Order ID"].Value.ToString();

                if (indexPosition == "No-Worker")
                {

                    ErrorMsgBox er = new ErrorMsgBox("Нет закройщика.");
                    er.Show();

                }
                else
                {

                    WorkerDetails dri = new WorkerDetails(indexPosition, orderId);
                    dri.Show();
                }

            }
            catch (Exception)
            {
                ErrorMsgBox r = new ErrorMsgBox("Please select a record");
                r.Show();
            }

        }
    }
}
