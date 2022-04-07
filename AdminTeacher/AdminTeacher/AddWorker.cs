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
    public partial class AddWorker : Form
    {
        private string[] data;
        private bool isAdd = true;
        private WorkerAdded wAdded;

        public AddWorker(string[] data, WorkerAdded i)
        {
            InitializeComponent();
            wAdded = i;
            if (data != null)
            {
                this.data = data;
                isAdd = false;
                textBox1.Text = data[3];
                textBox2.Text = data[0];
                textBox3.Text = data[1];
                textBox4.Text = data[2];
                textBox5.Text = data[4];
                textBox6.Text = data[5];
                textBox7.Text = data[7];
                button1.Text = "Изменить";
            }
        }

        public AddWorker(WorkerAdded i)
        {
            InitializeComponent();
            wAdded = i;
            data = new string[7];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data[3] = textBox1.Text;
            data[0] = textBox2.Text;
            data[1] = textBox3.Text;
            data[2] = textBox4.Text;
            data[4] = textBox5.Text;
            data[5] = textBox6.Text;
            if (isAdd)
            {
                data[6] = textBox7.Text;
                addUser();
            }
            else
            {
                data[7] = textBox7.Text;
                editUser();
            }
            wAdded.workerAdded();
            this.Close();
        }

        private void addUser()
        {
            Program.getDBHelper().addWorker(data);
        }

        private void editUser()
        {
            Program.getDBHelper().editWorker(data);
        }
    }
}
