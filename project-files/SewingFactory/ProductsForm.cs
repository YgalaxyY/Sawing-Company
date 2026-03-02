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
using System.IO;
using System.Text.RegularExpressions;

namespace SewingFactoryManagementSystem
{
    public partial class ProductsForm : Form
    {
        public ProductsForm()
        {
            InitializeComponent();

        }

        public string cleanTextboxes{

            set { monoFlat_TextBox1.Text = value; }
        }

        CURDQueryClass curdClass = new CURDQueryClass(); //Class for Curd function.



        private void label2_Click(object sender, EventArgs e)
        {
            this.Close(); //Closing mark X.
        }

     

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            getDataFromTheDB();             //load data into DataGridView.
            buttonToGetBlackAndWhite();    //Change the Update and Delete Button to be black and white and Disabled.          
            comboBox1.SelectedIndex = 0;  //comboBox1 must have the first value as default when form loads.
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            //Insertion
            //If non of them empty do let to insert.    

            string meterType = "";

            if (radioButton1.Checked == true)
            {

                meterType = "Normal";

            }
            else if (radioButton2.Checked == true) {


                meterType = "Digital";
            }
                    
            if (monoFlat_TextBox1.Text == string.Empty || monoFlat_TextBox2.Text == string.Empty || monoFlat_TextBox3.Text == string.Empty || monoFlat_TextBox4.Text == string.Empty || monoFlat_TextBox5.Text == string.Empty || monoFlat_TextBox6.Text == string.Empty || monoFlat_TextBox7.Text == string.Empty || monoFlat_TextBox8.Text == string.Empty)
            {

                ErrorMsgBox er = new ErrorMsgBox("Please Fill all the columns");
                er.Show();

            }
            else {

                string canInsert = "yes";

                if (monoFlat_TextBox8.Text.Length < 5)
                {

                    ErrorMsgBox er = new ErrorMsgBox("Счетчик количества должен иметь пять цифр");
                    er.Show();
                    canInsert = "no";

                }
               
           
                 if (canInsert == "yes")
                {

                    string InsertQuery = "INSERT INTO product VALUES ('" + monoFlat_TextBox1.Text + "','" + monoFlat_TextBox2.Text + "','" + monoFlat_TextBox3.Text + "','" + monoFlat_TextBox4.Text + "','" + monoFlat_TextBox5.Text + "','" + monoFlat_TextBox6.Text + "','" + monoFlat_TextBox7.Text + "','IN','" + monoFlat_TextBox8.Text + "','"+meterType+"')";
                    RegisterCarMethod(InsertQuery);
                   
                }
                
            }
            
        }


        string productIdForBug1 = "";
        string meterType = "";
        
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

            //Selection
            string query = "SELECT * FROM product WHERE productId='" + monoFlat_TextBox9.Text + "'";
            string dataComeChecker = "no";
            //The data returns back as MySqlDataReader format. That's why we can loop it.
            MySqlDataReader returnData = curdClass.SelectQuery(query);


            while (returnData.Read()) {

                monoFlat_TextBox1.Text = returnData.GetString(0);
                monoFlat_TextBox2.Text = returnData.GetString(1);
                monoFlat_TextBox3.Text = returnData.GetString(2);
                monoFlat_TextBox4.Text = returnData.GetString(3);
                monoFlat_TextBox5.Text = returnData.GetString(4);
                monoFlat_TextBox6.Text = returnData.GetString(5);
                monoFlat_TextBox7.Text = returnData.GetString(6);
                monoFlat_TextBox8.Text = returnData.GetString("stock");
                meterType = returnData.GetString("unitmeter_type");

                dataComeChecker = monoFlat_TextBox8.Text;
                productIdForBug1 = returnData.GetString(0); 
            }

            if (dataComeChecker == "no") {

                ErrorMsgBox ee = new ErrorMsgBox("Вы ввели неверный ID изделия");
                ee.Show();

            }

            if (meterType == "Normal")
            {

                radioButton1.Checked = true;
            }
            else if (meterType == "Digital") {

                radioButton2.Checked = true;

            }

            //if the data comes back, Enable the update and delete buttons and change their colors from black and white to colors.
            if (monoFlat_TextBox1.Text.Length > 0 && monoFlat_TextBox6.Text.Length > 0 && monoFlat_TextBox5.Text.Length > 0) {

                buttonToGetColor();
            }
           
        }



        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            //Update

            string selectProduct = "SELECT productStatus FROM product WHERE productId='" + monoFlat_TextBox9.Text + "'";
            MySqlDataReader datas = curdClass.SelectQueryOutMsg(selectProduct);
            string productS = "";


            while (datas.Read())
            {

                productS = datas.GetString(0);

            }

            string meterType = "";

            if (radioButton1.Checked == true) {

                meterType = "Normal";
            }
            else
            {

                meterType = "Digital";

            }

            string selectProductforUpdate = "SELECT * FROM product WHERE productId='" + monoFlat_TextBox9.Text + "'";
            MySqlDataReader dataForselectProductforUpdate = curdClass.SelectQueryOutMsg(selectProductforUpdate);

            string productId = "";
            string model = "";
            string brand = "";
            string makeYear = "";
            string color = "";
            string dailyRate = "";
            string monthlyRate = "";
            string odameterValue = "";
            string meterTypeOld = "";
           

            while (dataForselectProductforUpdate.Read())
            {

                productId = dataForselectProductforUpdate.GetString("productId");
                model = dataForselectProductforUpdate.GetString("productModel");
                brand = dataForselectProductforUpdate.GetString("productBrand");
                makeYear = dataForselectProductforUpdate.GetString("productMakeYear");
                color = dataForselectProductforUpdate.GetString("productColor");
                dailyRate = dataForselectProductforUpdate.GetString("productDailyRate");
                monthlyRate = dataForselectProductforUpdate.GetString("productMonthlyRate");
                odameterValue = dataForselectProductforUpdate.GetString("stock");
                meterTypeOld = dataForselectProductforUpdate.GetString("unitmeter_type");



            }

            string canUpdate = "yes";


            if (monoFlat_TextBox8.Text.Length < 5 && productS != "OUT" && productIdForBug1 == monoFlat_TextBox9.Text)
            {

                ErrorMsgBox er = new ErrorMsgBox("Значение остатка должно иметь пять цифр");
                er.Show();
                canUpdate = "no";

            }


            if (productId == monoFlat_TextBox1.Text && model == monoFlat_TextBox2.Text && brand == monoFlat_TextBox3.Text && makeYear == monoFlat_TextBox4.Text && color == monoFlat_TextBox5.Text && dailyRate == monoFlat_TextBox6.Text && monthlyRate == monoFlat_TextBox7.Text && odameterValue == monoFlat_TextBox8.Text && productIdForBug1 == monoFlat_TextBox9.Text && productS != "OUT" && meterTypeOld==meterType)
            {

                ErrorMsgBox er = new ErrorMsgBox("You did not make any changes to update");
                er.Show();
                canUpdate = "no";

            }


            if (monoFlat_TextBox1.Text == string.Empty || monoFlat_TextBox2.Text == string.Empty || monoFlat_TextBox3.Text == string.Empty || monoFlat_TextBox4.Text == string.Empty || monoFlat_TextBox5.Text == string.Empty || monoFlat_TextBox6.Text == string.Empty || monoFlat_TextBox7.Text == string.Empty || monoFlat_TextBox8.Text == string.Empty)
            {

                ErrorMsgBox er = new ErrorMsgBox("Please Fill all the columns");
                er.Show();

            }
            else
 {



                if (productIdForBug1 == monoFlat_TextBox9.Text)
                {
                    if (productS != "OUT")
                    {

                        if (canUpdate == "yes")
                        {


                            string query = "UPDATE product SET productId='" + monoFlat_TextBox1.Text + "', productModel='" + monoFlat_TextBox2.Text + "',productBrand='" + monoFlat_TextBox3.Text + "',productMakeYear='" + monoFlat_TextBox4.Text + "',productColor='" + monoFlat_TextBox5.Text + "',productDailyRate='" + monoFlat_TextBox6.Text + "',productMonthlyRate='" + monoFlat_TextBox7.Text + "',stock='" + monoFlat_TextBox8.Text + "',unitmeter_type='" + meterType + "' WHERE productId='" + monoFlat_TextBox9.Text + "'";

                            try
                            {
                                MySqlConnection myConnect = new MySqlConnection("SERVER=localhost;DATABASE=sewingfactory;UID=root;PASSWORD=1234");
                                myConnect.Open();
                                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                                myCommand.ExecuteNonQuery();
                                myConnect.Clone();

                                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                string txt = "Изделие " + monoFlat_TextBox1.Text + " обновлено " + DateTime.Now.ToString("yyyy-MM-dd") + " в " + DateTime.Now.ToLongTimeString();
                                File.AppendAllText(pathforLog + "//SFMS//ActivityLog.txt", txt + Environment.NewLine);


                                if (comboBox1.Text == "All")
                                {

                                    ComboBoxSearch(comboBox1.Text);

                                }
                                else if (comboBox1.Text == "Hired")
                                {

                                    ComboBoxSearch(comboBox1.Text);

                                }
                                else if (comboBox1.Text == "Avaliable")
                                {

                                    ComboBoxSearch(comboBox1.Text);
                                }
                                else
                                {

                                    getDataFromTheDB();
                                }

                                SuccessMSGBox s = new SuccessMSGBox("Update Successfully");
                                s.Show();
                                cleanTextBoxes();  //Clear the Textboxes as empty.      

                            }
                            catch (Exception Ex)
                            {
                                string messageFoDuplicatePrimaryKey = Ex.Message;

                                if (messageFoDuplicatePrimaryKey.StartsWith("Duplicate"))
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("Изделие с ID " + monoFlat_TextBox1.Text + " уже существует.");
                                    r.Show();
                                }
                                else
                                {

                                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                        }
                    }
                    else
                    {

                        ErrorMsgBox es = new ErrorMsgBox("Изделие используется в заказе, невозможно изменить");
                        es.Show();

                    }
                }
                else
                {

                    ErrorMsgBox es = new ErrorMsgBox("You have entered a wrong car ID. \n" + "We automatically fixed the right ID (" + productIdForBug1 + ").");
                    es.Show();
                    monoFlat_TextBox9.Text = productIdForBug1;

                }

            }

        }
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            string selectProduct = "SELECT productStatus FROM product WHERE productId='" + monoFlat_TextBox9.Text + "'";
            MySqlDataReader datas = curdClass.SelectQueryOutMsg(selectProduct);
            string productS = "";

            while (datas.Read()) {

                productS = datas.GetString(0);
            }

            if (monoFlat_TextBox9.Text == productIdForBug1)
            {

                if (productS != "OUT")
                {
                    ComformFormDelete cfd = new ComformFormDelete("Вы хотите удалить изделие ID '" + monoFlat_TextBox9.Text + "'", "DELETE FROM product WHERE productId='" + monoFlat_TextBox9.Text + "'", monoFlat_TextBox9.Text, "productDeletion");
                    cfd.ShowDialog();



                    if (comboBox1.Text == "All")
                    {

                        ComboBoxSearch(comboBox1.Text);


                    }
                    else if (comboBox1.Text == "Hired")
                    {

                        ComboBoxSearch(comboBox1.Text);

                    }
                    else if (comboBox1.Text == "Avaliable")
                    {

                        ComboBoxSearch(comboBox1.Text);
                    }
                    else
                    {

                        getDataFromTheDB();

                    }

                    string selectProduct2 = "SELECT productId FROM product WHERE productId='" + monoFlat_TextBox9.Text + "'";
                    MySqlDataReader datas2 = curdClass.SelectQueryOutMsg(selectProduct2);
                    string productS2 = "";

                    while (datas2.Read())
                    {

                        productS2 = datas2.GetString(0);
                    }

                    if (productS2 == string.Empty)
                    {

                        cleanTextBoxes();
                    }

                }
                else
                {

                    ErrorMsgBox es = new ErrorMsgBox("Изделие используется в заказе, невозможно удалить");
                    es.Show();
                }
            }
            else {

                ErrorMsgBox es = new ErrorMsgBox("You have entered a wrong car ID. \n" + "We automatically fixed the right ID (" + productIdForBug1 + ").");
                es.Show();
                monoFlat_TextBox9.Text = productIdForBug1;
            }
        }


 

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Search the database bashed on comboBox1 Text like Hire All and Etc.

            if (comboBox1.Text == "All")
            {
                //in this case text All.
                ComboBoxSearch(comboBox1.Text);

            }
            else if (comboBox1.Text == "Hired")
            {
                //in this case text Hired.
                ComboBoxSearch(comboBox1.Text);

            }

            else if (comboBox1.Text == "Avaliable")
            {
                //in this case text Avaliable.
                ComboBoxSearch(comboBox1.Text);
            }
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //We are displaying data in the form from the DataGridView in a sense of cell click.

            string position;
            string meterType = "";
            
            if (bunifuCustomDataGrid1.CurrentCell.ColumnIndex.Equals(0)) {

                position = bunifuCustomDataGrid1.CurrentCell.Value.ToString();

                string query = "SELECT * FROM product WHERE productId='" + position + "'";
                MySqlDataReader data = curdClass.SelectQuery(query);

                while (data.Read()) {

                    monoFlat_TextBox1.Text = data.GetString(0);
                    monoFlat_TextBox2.Text = data.GetString(1);
                    monoFlat_TextBox3.Text = data.GetString(2);
                    monoFlat_TextBox4.Text = data.GetInt32(3).ToString();
                    monoFlat_TextBox5.Text = data.GetString(4);
                    monoFlat_TextBox6.Text = data.GetDouble(5).ToString();
                    monoFlat_TextBox7.Text = data.GetDouble(6).ToString();
                    monoFlat_TextBox8.Text = data.GetString("stock");
                    monoFlat_TextBox9.Text = data.GetString(0);
                    productIdForBug1 = data.GetString(0);
                    meterType = data.GetString("unitmeter_type");

                }

                if (meterType == "Normal") {

                    radioButton1.Checked = true;

                }else if (meterType == "Digital") {

                    radioButton2.Checked = true;

                }

                buttonToGetColor();
            }
        }

        //Update and Delete Buttons Enabled and Get Color Method.
        public void buttonToGetColor()
        {
            bunifuFlatButton3.Enabled = true;
            bunifuFlatButton4.Enabled = true;

            //Change the buttons back color and fore color to be like default when enabled.
            bunifuFlatButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(67)))), ((int)(((byte)(33)))));
            bunifuFlatButton3.Textcolor = System.Drawing.Color.White;

            bunifuFlatButton4.BackColor = System.Drawing.Color.Tan;
            bunifuFlatButton4.Textcolor = System.Drawing.Color.White;

        }

        //Update and Delete Buttons Disabled and Get Black and White Method.
        public void buttonToGetBlackAndWhite()
        {
            bunifuFlatButton3.Enabled = false;
            bunifuFlatButton4.Enabled = false;

            //Change the buttons back color and fore color to be black and white when disabled.
            bunifuFlatButton3.BackColor = System.Drawing.Color.DarkGray;
            bunifuFlatButton3.Textcolor = System.Drawing.Color.Black;

            bunifuFlatButton4.BackColor = System.Drawing.Color.DarkGray;
            bunifuFlatButton4.Textcolor = System.Drawing.Color.Black;
        }

        //Clear TextBoxes Method.
        public void cleanTextBoxes()
        {
            monoFlat_TextBox1.Text = "";
            monoFlat_TextBox2.Text = "";
            monoFlat_TextBox3.Text = "";
            monoFlat_TextBox4.Text = "";
            monoFlat_TextBox5.Text = "";
            monoFlat_TextBox6.Text = "";
            monoFlat_TextBox7.Text = "";
            monoFlat_TextBox8.Text = "";
            monoFlat_TextBox9.Text = "";

        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {

            DataTable dTable1 = new DataTable();
            dTable1.Columns.Add("ID изделия");
            dTable1.Columns.Add("Модель");
            dTable1.Columns.Add("Тип");
            dTable1.Columns.Add("Год выпуска");
            dTable1.Columns.Add("Цвет");
            dTable1.Columns.Add("Дневная ставка");
            dTable1.Columns.Add("Месячная ставка");
            dTable1.Columns.Add("Статус");
            dTable1.Columns.Add("Тип счетчика");

            string status = bunifuTextbox1.text;
            string query = "";

            if (comboBox1.Text == "Все")
            {

                query = "SELECT * FROM product WHERE productId like'" + status + "%'";

            }
            else if (comboBox1.Text == "Заказанные")
            {

                query = "SELECT * FROM product WHERE productId like'" + status + "%' AND productStatus='OUT'";

            }
            else if (comboBox1.Text == "Доступные")
            {

                query = "SELECT * FROM product WHERE productId like'" + status + "%' AND productStatus='IN'";

            }
            else
            {
                query = "SELECT * FROM product WHERE productId like'" + status + "%'";
            }

            MySqlDataReader dataBrand = curdClass.SelectQuery(query);

            while (dataBrand.Read())
            {

                dTable1.Rows.Add(dataBrand.GetString(0), dataBrand.GetString(1), dataBrand.GetString(2), dataBrand.GetString(3), dataBrand.GetString(4), dataBrand.GetString(5), dataBrand.GetString(6), dataBrand.GetString(8), dataBrand.GetString(7));

            }

            bunifuCustomDataGrid1.DataSource = dTable1;

        }

        public void getDataFromTheDB()
        {

            DataTable data = new DataTable();
            data.Columns.Add("ID изделия");
            data.Columns.Add("Модель");
            data.Columns.Add("Тип");
            data.Columns.Add("Год выпуска");
            data.Columns.Add("Цвет");
            data.Columns.Add("Дневная ставка");
            data.Columns.Add("Месячная ставка");
            data.Columns.Add("Статус");
            data.Columns.Add("Тип счетчика");

            string queryForAll = "SELECT * FROM product";

            MySqlDataReader reData = curdClass.SelectQuery(queryForAll);

            while (reData.Read())
            {

                data.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7), reData.GetString(8));

            }

            bunifuCustomDataGrid1.DataSource = data;


        }

        public void ComboBoxSearch(string status)
        {
            getDataByStatus(status);
        }

        public void getDataByStatus(string status)
        {

            DataTable dTable1 = new DataTable();
            dTable1.Columns.Add("ID изделия");
            dTable1.Columns.Add("Модель");
            dTable1.Columns.Add("Тип");
            dTable1.Columns.Add("Год выпуска");
            dTable1.Columns.Add("Цвет");
            dTable1.Columns.Add("Дневная ставка");
            dTable1.Columns.Add("Месячная ставка");
            dTable1.Columns.Add("Статус");
            dTable1.Columns.Add("Тип счетчика");

            if (status == "Все")
            {

                string queryForAll = "SELECT * FROM product";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAll);

                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7), reData.GetString(8));

                }

            }
            else if (status == "Заказанные")
            {

                string queryForHired = "SELECT * FROM product WHERE productStatus='OUT'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForHired);

                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7), reData.GetString(8));

                }

            }
            else if (status == "Доступные")
            {

                string queryForAvaliable = "SELECT * FROM product WHERE productStatus='IN'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAvaliable);

                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7), reData.GetString(8));

                }

            }

            bunifuCustomDataGrid1.DataSource = dTable1;

        }


        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            cleanTextBoxes();
            buttonToGetBlackAndWhite();
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void monoFlat_TextBox9_TextChanged(object sender, EventArgs e)
        {
            //If the TextBox9.length is 0 ,change the update and delete buttons to be disabled and black and white.
            //Becasue we don't need to be enabled when is not updating or deleting.
            if (monoFlat_TextBox9.Text.Length == 0)
            {

                buttonToGetBlackAndWhite();
                cleanTextBoxes();

            }

            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox9.Text, @"[!@#$%^&*\~{}.`,\\;=<|>/?+'""\]]"))
            {
                char[] chars = monoFlat_TextBox9.Text.ToCharArray();
                ErrorMsgBox er = new ErrorMsgBox("You cannot enter symbols like " + chars[chars.Length - 1]);
                er.Show();

                string charsToStr = new string(chars);

                monoFlat_TextBox9.Text = charsToStr.Remove(charsToStr.Length - 1);

            }
        }

        private void monoFlat_TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox4.Text, "[^0-9]")) {

                char[] chars = monoFlat_TextBox4.Text.ToCharArray();
                string charsToStr = new string(chars);
               
                ErrorMsgBox er = new ErrorMsgBox("The make year must be in digits format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox4.Text = charsToStr.Remove(charsToStr.Length - 1);

            }

        }

        private void monoFlat_TextBox5_TextChanged(object sender, EventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox5.Text, @"[^a-zA-Z \s]"))
            {
                char[] chars = monoFlat_TextBox5.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The color must be in text format.\nNumbers and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox5.Text = charsToStr.Remove(charsToStr.Length - 1);


            }

        }

        private void monoFlat_TextBox6_TextChanged(object sender, EventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox6.Text, "[^0-9.]"))
            {

                char[] chars = monoFlat_TextBox6.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The daily rate must be in digit format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox6.Text = charsToStr.Remove(charsToStr.Length - 1);


            }

        }

        private void monoFlat_TextBox7_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox7.Text, "[^0-9.]"))
            {
                char[] chars = monoFlat_TextBox7.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The monthly rate must be in digit format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox7.Text = charsToStr.Remove(charsToStr.Length - 1);

            }

        }

        private void monoFlat_TextBox8_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox8.Text, "[^0-9]"))
            {
                char[] chars = monoFlat_TextBox8.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("Значение остатка должно быть в цифровом формате.\nБуквы и символы не допускаются.");
                er.Show();
                monoFlat_TextBox8.Text = charsToStr.Remove(charsToStr.Length - 1);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        public void RegisterCarMethod(string query)
        {
            try
            {
                MySqlConnection myConnect = new MySqlConnection("SERVER=localhost;DATABASE=sewingfactory;UID=root;PASSWORD=1234");
                myConnect.Open();
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                myCommand.ExecuteNonQuery();
                myConnect.Clone();

                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string txt = "Новое изделие зарегистрировано " + DateTime.Now.ToString("yyyy-MM-dd") + " в " + DateTime.Now.ToLongTimeString() + " ID: " + monoFlat_TextBox1.Text;
                File.AppendAllText(pathforLog + "//SFMS//ActivityLog.txt", txt + Environment.NewLine);

                SuccessMSGBox s = new SuccessMSGBox("Успешно зарегистрировано.");
                s.Show();
                getDataFromTheDB();
                cleanTextBoxes();

            }
            catch (Exception Ex)
            {



                string messageFoDuplicatePrimaryKey = Ex.Message;

                if (messageFoDuplicatePrimaryKey.StartsWith("Duplicate"))
                {

                    ErrorMsgBox r = new ErrorMsgBox("Изделие с ID " + monoFlat_TextBox1.Text + " уже существует.");
                    r.Show();
                }
                else
                {

                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }

        private void monoFlat_TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox1.Text, @"[!@#$%^&*\~{}.`,\\;=<|>/?'" + "\"]"))
            {
               char[] chars=monoFlat_TextBox1.Text.ToCharArray(); 
                ErrorMsgBox er = new ErrorMsgBox("You cannot enter symbols like "+chars[chars.Length-1]);
                er.Show();

                string charsToStr = new string(chars);

                monoFlat_TextBox1.Text = charsToStr.Remove(charsToStr.Length - 1);

            }
        }

        private void monoFlat_TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox2.Text, @"[!@#$%^&*\~`{},\\;.=<|>/?'" + "\"]"))
            {
                char[] chars = monoFlat_TextBox2.Text.ToCharArray();
                ErrorMsgBox er = new ErrorMsgBox("You cannot enter symbols like " + chars[chars.Length - 1]);
                er.Show();

                string charsToStr = new string(chars);

                monoFlat_TextBox2.Text = charsToStr.Remove(charsToStr.Length - 1);

            }
        }

        private void monoFlat_TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox3.Text, @"[^a-zA-Z \s]"))
            {
                

                char[] chars = monoFlat_TextBox3.Text.ToCharArray();
                ErrorMsgBox er = new ErrorMsgBox("The brand name must be in text format.\nNumbers and symbols are not allowed.");
                er.Show();
                string charsToStr = new string(chars);
                monoFlat_TextBox3.Text = charsToStr.Remove(charsToStr.Length - 1);

            }
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
