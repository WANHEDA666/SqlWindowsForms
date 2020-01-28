using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            this.UserName.Text = "Name";
            this.UserLastname.Text = "Lastname";
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UserName_Enter(object sender, EventArgs e) {
            if (this.UserName.Text == "Name") {
                this.UserName.Text = "";
            }
        }

        private void UserName_Leave(object sender, EventArgs e) {
            if (this.UserName.Text == "") {
                this.UserName.Text = "Name";
            }
        }

        private void UserLastname_Enter(object sender, EventArgs e) {
            if (this.UserLastname.Text == "Lastname") {
                this.UserLastname.Text = "";
            }
        }

        private void UserLastname_Leave(object sender, EventArgs e) {
            if (this.UserLastname.Text == "") {
                this.UserLastname.Text = "Lastname";
            }
        }

        private void LoginButton_Click(object sender, EventArgs e) {
            string userrlogin = this.Login.Text;
            string userrpass = this.Password.Text;
            string userrname = this.UserName.Text;
            string userrLastname = this.UserLastname.Text;

            if ((userrlogin != "") && (userrpass != "") && (userrname != "") && (userrLastname != "") && (userrname != "Name") && (userrLastname != "Lastname") && !Check()) {
                DataBase db = new DataBase();

                MySqlCommand add = new MySqlCommand("INSERT INTO `users` (`id`, `login`, `password`, `name`, `lastname`) VALUES(NULL, @login, @pass, @name, @lastname)", db.GetConnection());
                add.Parameters.Add("@login", MySqlDbType.VarChar).Value = userrlogin;
                add.Parameters.Add("@pass", MySqlDbType.VarChar).Value = userrpass;
                add.Parameters.Add("@name", MySqlDbType.VarChar).Value = userrname;
                add.Parameters.Add("@lastname", MySqlDbType.VarChar).Value = userrLastname;

                db.OpenConnection();

                if (add.ExecuteNonQuery() == 1) {
                    this.Hide();
                    MainForm main = new MainForm();
                    main.Show();
                }
                else {
                    MessageBox.Show("Account not created");
                }

                db.CloseConnection();
            }
            else {
                MessageBox.Show("Enter all values");
            }
        }

        private Boolean Check() {
            string userrlogin = this.Login.Text;
            DataBase db = new DataBase();

            DataTable table = new DataTable();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @login", db.GetConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = userrlogin;

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(table);

            if (table.Rows.Count > 0) {
                MessageBox.Show("There's already such login. Use another.");
                return true;
            }
            else {
                return false;
            }
        }
    }
}
