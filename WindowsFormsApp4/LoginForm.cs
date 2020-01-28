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

namespace WindowsFormsApp4 {
	public partial class LoginForm : Form {
		public LoginForm() {
			InitializeComponent();
			this.Password.Size = new Size(this.Password.Size.Width, this.Login.Size.Height);
		}

		private void CloseButton_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		private void CloseButton_MouseEnter(object sender, EventArgs e) {
			this.CloseButton.BackColor = Color.Green;
		}

		private void CloseButton_MouseLeave(object sender, EventArgs e) {
			this.CloseButton.BackColor = Color.Red;
		}

		Point lastpoint;
		private void Panel1_MouseMove(object sender, MouseEventArgs e) {
			/*if (e.Button == MouseButtons.Left) {
				this.Left += e.X - lastpoint.X;
				this.Top += e.Y - lastpoint.Y;
			}*/
		}

		private void Panel1_MouseClick(object sender, MouseEventArgs e) {
			//lastpoint = new Point(e.X, e.Y);
		}

		private void LoginButton_Click(object sender, EventArgs e) {
			string login = this.Login.Text;
			string password = this.Password.Text;

			DataBase db = new DataBase();

			DataTable table = new DataTable();

			MySqlDataAdapter dataAdapter = new MySqlDataAdapter();

			MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = login AND `password` = password", db.GetConnection());

			dataAdapter.SelectCommand = command;
			dataAdapter.Fill(table);

			if (table.Rows.Count > 0) {
				this.Hide();
				MainForm main = new MainForm();
				main.Show();
			}
			else {
				MessageBox.Show("Noo");
			}
		}

		private void label2_Click(object sender, EventArgs e) {
			this.Hide();
			RegisterForm reg = new RegisterForm();
			reg.Show();
		}
	}
}
