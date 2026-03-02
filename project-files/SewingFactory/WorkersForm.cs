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

namespace SewingFactoryManagementSystem
{
    public partial class WorkersForm : Form
    {
        public WorkersForm()
        {
            InitializeComponent();
        }

        CURDQueryClass curdClass= new CURDQueryClass();
        int totalWorker = 0;
        string WorkerID = "";

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Workers_Load(object sender, EventArgs e)
        {
            getDataFromTheDB();
            buttonToGetBlackAndWhite();
            comboBox1.SelectedIndex = 0;

            getWorkerTotalFinal();
        }

        public void getDataFromTheDB()
        {
            DataTable data = new DataTable();
            data.Columns.Add("ID сотрудника");
            data.Columns.Add("Имя");
            data.Columns.Add("Фамилия");
            data.Columns.Add("Адрес");
            data.Columns.Add("Номер телефона");
            data.Columns.Add("Удостоверение личности");
            data.Columns.Add("Номер рабочего удостоверения");
            data.Columns.Add("Пол");
            data.Columns.Add("Статус");

            string querySelect = "SELECT * FROM worker";

            MySqlDataReader datas = curdClass.SelectQuery(querySelect);

            while (datas.Read())
            {
                data.Rows.Add(
                    datas.IsDBNull(0) ? "" : datas.GetString(0), // workerId
                    datas.IsDBNull(1) ? "" : datas.GetString(1), // workerFname
                    datas.IsDBNull(2) ? "" : datas.GetString(2), // workerLname
                    datas.IsDBNull(3) ? "" : datas.GetString(3), // workerAddress
                    datas.IsDBNull(4) ? "" : datas.GetInt32(4).ToString(), // workerPhoneNo
                    datas.IsDBNull(5) ? "" : datas.GetString(5), // workerIdentityCard
                    datas.IsDBNull(6) ? "" : datas.GetString(6), // workerLicence
                    datas.IsDBNull(7) ? "" : datas.GetString(7), // workerGender
                    datas.IsDBNull(8) ? "" : datas.GetString(8)  // workerStatus
                );
            }

            // Закрытие DataReader и освобождение ресурсов
            datas.Close();

            bunifuCustomDataGrid1.DataSource = data;
        }


        public void buttonToGetBlackAndWhite()
        {

            bunifuFlatButton3.Enabled = false;
            bunifuFlatButton4.Enabled = false;

        }

        public void getWorkerTotal()
        {
            string qu = "SELECT COUNT(*) FROM worker";
            MySqlDataReader cusQu = curdClass.SelectQuery(qu);

            while (cusQu.Read())
            {
                totalWorker = cusQu.GetInt32(0);
            }
        }

        public void getWorkerTotalFinal()
        {
            getWorkerTotal();

            int cusdeleteTotal = 0;

            string sqlcodeToSelectDeleteCus = "SELECT deleteDriver FROM systemcontroller";
            MySqlDataReader dataDe = curdClass.SelectQueryOutMsg(sqlcodeToSelectDeleteCus);

            while (dataDe.Read())
            {
                cusdeleteTotal = dataDe.GetInt32(0);
            }

            string cusStr = "";

            int totalCus = totalWorker + cusdeleteTotal + 1;

            if (totalCus < 10)
            {
                cusStr = "DRI-00";
            }
            else if (totalCus >= 10 && totalCus < 100)
            {
                cusStr = "DRI-0";
            }
            else if (totalCus >= 100)
            {
                cusStr = "DRI-";
            }

            bunifuCustomTextbox1.Text = cusStr + totalCus.ToString();
        }

        private void bunifuCustomTextbox1_Click(object sender, EventArgs e)
        {
            getWorkerTotalFinal();
        }

        private void monoFlat_TextBox8_TextChanged(object sender, EventArgs e)
        {
            if (monoFlat_TextBox8.Text.Length == 0)
            {
                buttonToGetBlackAndWhite();
                cleanTextBoxes();
            }
        }

        private void monoFlat_TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox2.Text, @"[^a-zA-Z \s]"))
            {
                char[] chars = monoFlat_TextBox2.Text.ToCharArray();
                string charsToStr = new string(chars);
                ErrorMsgBox er = new ErrorMsgBox("The first name must be in text format.");
                er.Show();
                monoFlat_TextBox2.Text = charsToStr.Remove(charsToStr.Length - 1);
            }
        }

        private void monoFlat_TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox3.Text, @"[^a-zA-Z \s]"))
            {
                char[] chars = monoFlat_TextBox3.Text.ToCharArray();
                string charsToStr = new string(chars);
                ErrorMsgBox er = new ErrorMsgBox("The last name must be in text format.");
                er.Show();
                monoFlat_TextBox3.Text = charsToStr.Remove(charsToStr.Length - 1);
            }
        }

        private void monoFlat_TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox5.Text, "[^0-9+]"))
            {
                char[] chars = monoFlat_TextBox5.Text.ToCharArray();
                string charsToStr = new string(chars);
                ErrorMsgBox er = new ErrorMsgBox("The phone number must be in digit format.");
                er.Show();
                monoFlat_TextBox5.Text = charsToStr.Remove(charsToStr.Length - 1);
            }
        }

        private void monoFlat_TextBox6_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox6.Text, @"[!@#$%^&*]"))
            {
                char[] chars = monoFlat_TextBox6.Text.ToCharArray();
                ErrorMsgBox er = new ErrorMsgBox("You cannot enter symbols.");
                er.Show();
                string charsToStr = new string(chars);
                monoFlat_TextBox6.Text = charsToStr.Remove(charsToStr.Length - 1);
            }
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string position;
            if (bunifuCustomDataGrid1.CurrentCell.ColumnIndex.Equals(0))
            {
                position = bunifuCustomDataGrid1.CurrentCell.Value.ToString();
                string query = "SELECT * FROM worker WHERE workerId='" + position + "'";
                MySqlDataReader data = curdClass.SelectQuery(query);
                string RadioVal;
                while (data.Read())
                {
                    bunifuCustomTextbox1.Text = data.GetString(0);
                    monoFlat_TextBox2.Text = data.GetString(1);
                    monoFlat_TextBox3.Text = data.GetString(2);
                    monoFlat_TextBox4.Text = data.GetString(3);
                    monoFlat_TextBox5.Text = data.GetString(4);
                    monoFlat_TextBox6.Text = data.GetString(5);
                    monoFlat_TextBox7.Text = data.GetString(6);
                    RadioVal = data.GetString(7);
                    monoFlat_TextBox8.Text = data.GetString(0);
                    WorkerID = data.GetString(0);
                    if (RadioVal == "M")
                        radioButton1.Checked = true;
                    else if (RadioVal == "F")
                        radioButton2.Checked = true;
                }
                buttonToGetColor();
            }
        }

        public void buttonToGetColor()
        {
            bunifuFlatButton3.Enabled = true;
            bunifuFlatButton4.Enabled = true;
            this.bunifuFlatButton4.Iconimage = global::SewingFactoryManagementSystem.Properties.Resources.if_Remove_27874;
            this.bunifuFlatButton3.Iconimage = global::SewingFactoryManagementSystem.Properties.Resources.if_db_update_32131;
        }

        public void cleanTextBoxes()
        {
            bunifuCustomTextbox1.Text = "";
            monoFlat_TextBox2.Text = "";
            monoFlat_TextBox3.Text = "";
            monoFlat_TextBox4.Text = "";
            monoFlat_TextBox5.Text = "";
            monoFlat_TextBox6.Text = "";
            monoFlat_TextBox7.Text = "";
            monoFlat_TextBox8.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxSearch(comboBox1.Text);
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            ComboBoxSearch(bunifuTextbox1.text);
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            // Поиск
            string query = "SELECT * FROM worker WHERE workerId='" + monoFlat_TextBox8.Text + "'";
            MySqlDataReader data = curdClass.SelectQuery(query);
            while (data.Read())
            {
                bunifuCustomTextbox1.Text = data.GetString(0);
                monoFlat_TextBox2.Text = data.GetString(1);
                monoFlat_TextBox3.Text = data.GetString(2);
                monoFlat_TextBox4.Text = data.GetString(3);
                monoFlat_TextBox5.Text = data.GetString(4);
                monoFlat_TextBox6.Text = data.GetString(5);
                monoFlat_TextBox7.Text = data.GetString(6);
                string radioVal = data.GetString(7);
                if (radioVal == "M")
                    radioButton1.Checked = true;
                else if (radioVal == "F")
                    radioButton2.Checked = true;
                buttonToGetColor();
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            // Обновление
            string sqlValue = radioButton1.Checked ? "M" : "F";
            string query = "UPDATE worker SET workerFname='" + monoFlat_TextBox2.Text + "',workerLname='" + monoFlat_TextBox3.Text + "',workerAddress='" + monoFlat_TextBox4.Text + "',workerPhoneNo='" + monoFlat_TextBox5.Text + "',workerIdentityCard='" + monoFlat_TextBox6.Text + "',workerLicence='" + monoFlat_TextBox7.Text + "',workerGender='" + sqlValue + "' WHERE workerId='" + monoFlat_TextBox8.Text + "'";
            curdClass.CUDOutMsg(query);
            SuccessMSGBox s = new SuccessMSGBox("Успешно обновлено.");
            s.Show();
            getDataFromTheDB();
            cleanTextBoxes();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            // Удаление
            if (monoFlat_TextBox8.Text == WorkerID)
            {
                ComformFormDelete cfd = new ComformFormDelete("Вы хотите удалить сотрудника ID '" + monoFlat_TextBox8.Text + "'", "DELETE FROM worker WHERE workerId='" + monoFlat_TextBox8.Text + "'", monoFlat_TextBox8.Text, "workerDeletion");
                cfd.ShowDialog();
                getDataFromTheDB();
                cleanTextBoxes();
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            // Добавление
            SuccessMSGBox s = new SuccessMSGBox("Сотрудник добавлен");
            s.Show();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            cleanTextBoxes();
        }

        private void monoFlat_TextBox7_TextChanged(object sender, EventArgs e)
        {
            // Валидация
        }

        public void ComboBoxSearch(string status)
        {
            getDataFromTheDB();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
            // Обработчик входа в groupBox4
        }

        private void Drivers_Load(object sender, EventArgs e)
        {
            getDataFromTheDB();
            buttonToGetBlackAndWhite();
            comboBox1.SelectedIndex = 0;
            getWorkerTotalFinal();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        // Остальные методы...
    }
}
