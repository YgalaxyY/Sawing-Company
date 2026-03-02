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
    public partial class Notification : Form
    {

        string date = "";
        public Notification(string d)
        {
            InitializeComponent();
            date = d;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            getDataFromTheDB();
        }

        CURDQueryClass curdFunction = new CURDQueryClass();

        public void getDataFromTheDB()
        {

            DataTable data = new DataTable();
            data.Columns.Add("Order ID");
            data.Columns.Add("ID изделия");
            data.Columns.Add("ID клиента");
            data.Columns.Add("ID сотрудника");
            data.Columns.Add("Дата заказа");
            data.Columns.Add("Дата выполнения");
            data.Columns.Add("Basis");
            data.Columns.Add("Duration");
            data.Columns.Add("Ad.Paid.Amount");
            data.Columns.Add("Blance");
            data.Columns.Add("Зарплата сотрудника");
            data.Columns.Add("Общая сумма");
            data.Columns.Add("Значение остатка");
            data.Columns.Add("Общее количество");
            data.Columns.Add("Сумма скидки");
            data.Columns.Add("Процент скидки");



            string querySelect = "SELECT * FROM orders WHERE completionDate='" + date+"'";

            MySqlDataReader datas = curdFunction.SelectQuery(querySelect);



            while (datas.Read())
            {

                data.Rows.Add(datas.GetString("OrderId"), datas.GetString("productId"), datas.GetString("cusId"), datas.GetString("workerId"), datas.GetString("orderDate"), datas.GetString("completionDate"), datas.GetString("basis"), datas.GetString("duration"), datas.GetString("advancedAmount"), datas.GetString("blance"), datas.GetString("workerAmount"), datas.GetString("totalAmount"), datas.GetString("startQuantity"), datas.GetString("totalQuantity"), datas.GetString("DiscountAmount"), datas.GetString("DiscountPer"));

            }

            bunifuCustomDataGrid1.DataSource = data;

        }
        int rowIndex = 0;
        private void bunifuCustomDataGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Показать детали изделия", MenuItemNew_Click));
                m.MenuItems.Add(new MenuItem("Показать детали клиента", MenuItemNew2_Click));
                m.MenuItems.Add(new MenuItem("Показать детали сотрудника", MenuItemNew3_Click));


                rowIndex = e.RowIndex;

                m.Show(bunifuCustomDataGrid1, bunifuCustomDataGrid1.PointToClient(Cursor.Position));

            }
        }

        private void MenuItemNew_Click(Object sender, System.EventArgs e)
        {

            try
            {
                string indexPosition = bunifuCustomDataGrid1.Rows[rowIndex].Cells["ID изделия"].Value.ToString();
                string orderId = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Order ID"].Value.ToString();


                ProductDetailsForBook carDet = new ProductDetailsForBook(indexPosition, orderId);
                carDet.Show();

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
                string indexPosition = bunifuCustomDataGrid1.Rows[rowIndex].Cells["ID сотрудника"].Value.ToString();
                string orderId = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Order ID"].Value.ToString();

                if (indexPosition == "No-Worker")
                {

                    ErrorMsgBox er = new ErrorMsgBox("Нет назначенного сотрудника.");
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

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
