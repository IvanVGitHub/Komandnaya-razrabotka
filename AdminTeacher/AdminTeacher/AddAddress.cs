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
    public partial class AddAddress : Form
    {
        private string[] data;
        private bool isAdd = true;
        private AddressAdded adAdded;


        public AddAddress(string[] data, AddressAdded i)
        {
            InitializeComponent();
            adAdded = i;
            if (data != null)
            {
                this.data = data;
                isAdd = false;
                button1.Text = "Изменить";
                textBox1.Text = data[0];
                textBox2.Text = data[1];
                textBox3.Text = data[2];
            }
        }

        public AddAddress(AddressAdded i)
        {
            InitializeComponent();
            adAdded = i;
            data = new string[3];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data[0] = textBox1.Text;
            data[1] = textBox2.Text;
            data[2] = textBox3.Text;
            if (isAdd)
            {
                addRecord();
            }
            else
            {
                editRecord();
            }
            adAdded.addressAdded();
            this.Close();
           
        }

        private void addRecord()
        {
            Program.getDBHelper().addAddress(data);
        }

        private void editRecord()
        {
            Program.getDBHelper().editAddress(data);
        }
    }
}
