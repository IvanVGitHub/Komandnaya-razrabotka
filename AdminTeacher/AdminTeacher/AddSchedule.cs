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
    public partial class AddSchedule : Form
    {
        private UserSchedule us;
        private bool isAdd = false;
        private ScheduleAdded sa;

        public AddSchedule(UserSchedule s, ScheduleAdded sa)
        {
            InitializeComponent();
            us = s;
            this.sa = sa;
            isAdd = false;
            comboBox1.Items.Add("ПН");
            comboBox1.Items.Add("ВТ");
            comboBox1.Items.Add("СР");
            comboBox1.Items.Add("ЧТ");
            comboBox1.Items.Add("ПТ");
            comboBox1.Items.Add("СБ");
            comboBox2.Items.Add("А");
            comboBox2.Items.Add("Б");

            textBox1.Text = us.order;
            textBox2.Text = us.subject;
            textBox3.Text = us.building;
            textBox4.Text = us.room;
            textBox5.Text = us.group;
            comboBox1.SelectedIndex = Int32.Parse(us.day) - 1;
            comboBox2.SelectedIndex = getWeekNumber(us.week);

            button1.Text = "Изменить";
        }

        public AddSchedule(string userId, ScheduleAdded sa)
        {
            InitializeComponent();
            isAdd = true;
            this.sa = sa;
            us = new UserSchedule();
            us.userId = userId;
            comboBox1.Items.Add("ПН");
            comboBox1.Items.Add("ВТ");
            comboBox1.Items.Add("СР");
            comboBox1.Items.Add("ЧТ");
            comboBox1.Items.Add("ПТ");
            comboBox1.Items.Add("СБ");
            comboBox2.Items.Add("А");
            comboBox2.Items.Add("Б");
            button1.Text = "Добавить";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int getWeekNumber(string w)
        {
            if (w.Equals("А"))
            {
                return 0;
            }
            else 
            {
                return 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            us.order = textBox1.Text;
            us.subject = textBox2.Text;
            us.building = textBox3.Text;
            us.room = textBox4.Text;
            us.group = textBox5.Text;
            us.day = (comboBox1.SelectedIndex + 1) + "";
            us.week = comboBox2.SelectedIndex == 0 ? "А" : "Б";
            if (isRoomFree())
            {
                if (isAdd)
                {
                    addSchedule();
                }
                else
                {
                    updateSchedule();
                }
                sa.scheduleAdded();
                this.Close();
            }
            else 
            {
                MessageBox.Show(this, "Аудитория в это время занята.");
            }
        }

        private void updateSchedule()
        {
            Program.getDBHelper().updateSchedule(us);
        }

        private void addSchedule()
        {
            Program.getDBHelper().addSchedule(us);
        }

        private bool isRoomFree()
        {
            return Program.getDBHelper().checkRoom(us);
        }

    }
}
