using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace SewingFactoryManagementSystem
{
    public partial class LoginForm : Form
    {
        // Реалистичные учетные данные для швейной фабрики
        // Администратор: admin / FabricaAdmin2024!
        // Оператор: operator.ivanova / IvanoVa2024!
        // Мастер: master.petrov / PetrovMaster2024!
        
        public LoginForm()
        {
            InitializeComponent();
        }
      
        public LoginForm(string type)
        {
            InitializeComponent();
            label3.Text = type;
        }

        int canMove;
        int Xval;
        int Yval;
        string accountType = "";
        
        // Строка подключения к БД
        string connectionString = "SERVER=localhost;DATABASE=sewingfactory;UID=root;PASSWORD=1234";

        private void LoginForm_Load(object sender, EventArgs e)
        {
            bunifuFlatButton1.Enabled = false;
            this.bunifuFlatButton1.Iconimage = global::SewingFactoryManagementSystem.Properties.Resources.if_login_173049_ConvertImage;
        }



        private void monoFlat_TextBox1_TextChanged(object sender, EventArgs e)
        {


            if (monoFlat_TextBox1.Text == string.Empty)
            {

                monoFlat_TrackBar1.Value = 0;

            }
            else
            {

                monoFlat_TrackBar1.Value = 5;

            }
        }

        private void monoFlat_TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (monoFlat_TextBox1.Text == string.Empty)
            {

                monoFlat_TrackBar1.Value = 5;

            }
            else
            {

                monoFlat_TrackBar1.Value = 10;

            }

        }

        private void monoFlat_TrackBar1_ValueChanged()
        {
            if (monoFlat_TrackBar1.Value == 10)
            {

                bunifuFlatButton1.Enabled = true;
                this.bunifuFlatButton1.Iconimage = global::SewingFactoryManagementSystem.Properties.Resources.if_login_173049;
            }
            else {

                bunifuFlatButton1.Enabled = false;
                this.bunifuFlatButton1.Iconimage = global::SewingFactoryManagementSystem.Properties.Resources.if_login_173049_ConvertImage;

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string username = monoFlat_TextBox1.Text;
            string password = monoFlat_TextBox2.Text;
            
            // Хешируем пароль для сравнения с БД
            string passwordHash = HashPassword(password);
            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Проверка учетных данных в БД
                    string query = "SELECT role, full_name FROM users WHERE username = @username AND password_hash = @password AND is_active = TRUE";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", passwordHash);
                    
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader.GetString("role");
                            string fullName = reader.GetString("full_name");
                            
                            // Успешная аутентификация
                            LogActivity($"Пользователь {fullName} ({username}) вошел в систему как {role}");
                            
                            MainForm main = new MainForm();
                            main.Show();
                            this.Hide();
                        }
                        else
                        {
                            ErrorMsgBox err = new ErrorMsgBox("Неверный логин или пароль.");
                            err.Show();
                            monoFlat_TextBox1.Text = "";
                            monoFlat_TextBox2.Text = "";
                            monoFlat_TrackBar1.Value = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsgBox err = new ErrorMsgBox($"Ошибка подключения к базе данных: {ex.Message}");
                err.Show();
            }
        }
        
        // Метод хеширования пароля (SHA-256)
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        
        // Метод логирования активности
        private void LogActivity(string message)
        {
            try
            {
                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string logPath = Path.Combine(pathforLog, "SFMS");
                
                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);
                    
                string logFile = Path.Combine(logPath, "ActivityLog.txt");
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";
                
                File.AppendAllText(logFile, logEntry);
            }
            catch { }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            Xval = e.X;
            Yval = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - Xval, MousePosition.Y - Yval);
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            accountType ac = new accountType();
            ac.Show();
            this.Hide();
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 400, MousePosition.Y - 17);
            }
           
        }

        private void label2_MouseUp(object sender, MouseEventArgs e)
        {
            panel1_MouseUp(sender,e);
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            panel1_MouseDown(sender, e);
        }
    }
}
