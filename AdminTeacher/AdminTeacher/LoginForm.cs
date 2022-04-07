using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminTeacher
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                if (Program.getDBHelper().loginUser(textBox1.Text, textBox2.Text))
                {
                    this.Hide();
                    var form2 = new MainForm();
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                }
                else 
                {
                    MessageBox.Show(this, "Пользователь не найден.");
                }
            }
            else
            {
                MessageBox.Show(this, "Заполните все поля");
            }
        }
    }
}
