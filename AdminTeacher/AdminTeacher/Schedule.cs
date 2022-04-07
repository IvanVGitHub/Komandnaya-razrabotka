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
    public partial class Schedule : Form, ScheduleAdded
    {
        private string userId;
        public Schedule(string id)
        {
            InitializeComponent();
            userId = id;
            initSchedule(userId);
        }

        private void initSchedule(string id)
        {
            List<UserSchedule> sch = Program.getDBHelper().getUserSchedule(id);
            if (sch.Count > 0)
            {
                fillTable(sch);
            }
        }

        private void fillTable(List<UserSchedule> list)
        {
            DataTable table = new DataTable("Расписание");
            DataColumn c0 = new DataColumn("День");
            DataColumn c1 = new DataColumn("Предмет");
            DataColumn c2 = new DataColumn("№ пары");
            DataColumn c3 = new DataColumn("Корпус");
            DataColumn c4 = new DataColumn("Аудитория");
            DataColumn c5 = new DataColumn("Группа");
            DataColumn c6 = new DataColumn("Неделя");
            DataColumn c7 = new DataColumn("ID");
            DataColumn c8 = new DataColumn("UID");
            table.Columns.Add(c0);
            table.Columns.Add(c1);
            table.Columns.Add(c2);
            table.Columns.Add(c3);
            table.Columns.Add(c4);
            table.Columns.Add(c5);
            table.Columns.Add(c6);
            table.Columns.Add(c7);
            table.Columns.Add(c8);
            foreach (UserSchedule item in list)
            {
                DataRow row = table.NewRow();
                row["День"] = getDay(item.day);
                row["Предмет"] = item.subject;
                row["№ пары"] = item.order;
                row["Корпус"] = item.building;
                row["Аудитория"] = item.room;
                row["Группа"] = item.group;
                row["Аудитория"] = item.room;
                row["Неделя"] = item.week.Equals("1") ? "А" : "Б";
                row["ID"] = item.id;
                row["UID"] = item.userId;
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = false;
            dataGridView1.Columns[dataGridView1.Columns.Count - 2].Visible = false;
        }

        private string getDay(string d)
        {
            switch (d)
            { 
                case "1":
                    return "пн";
                case "2":
                    return "вт";
                case "3":
                    return "ср";
                case "4":
                    return "чт";
                case "5":
                    return "пт";
                case "6":
                    return "сб";
                default:
                    return "";
                    
            }
        }

        private string getDayNumber(string d)
        {
            switch (d.ToLower())
            {
                case "пн":
                    return "1";
                case "вт":
                    return "2";
                case "ср":
                    return "3";
                case "чт":
                    return "4";
                case "пт":
                    return "5";
                case "сб":
                    return "6";
                default:
                    return "";

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] data = getCells(dataGridView1);
            Program.getDBHelper().deleteSchedule(data[data.Length - 2]);
            initSchedule(userId);
        }

        private string[] getCells(DataGridView dataGrid)
        {
            int index = dataGrid.CurrentRow.Index;
            string[] result = new string[dataGrid.ColumnCount];
            for (int i = 0; i < result.Length; i++)
            {
                if (dataGrid.Rows[index].Cells[i].Value != null)
                    result[i] = dataGrid.Rows[index].Cells[i].Value.ToString();
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] data = getCells(dataGridView1);
            data[0] = getDayNumber(data[0]);
            AddSchedule f = new AddSchedule(new UserSchedule(data), this);
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] data;
            AddSchedule f;
            try
            {
                data = getCells(dataGridView1);
                f = new AddSchedule(userId, this);
                f.Show();
            }
            catch (Exception ex) {
                f = new AddSchedule(userId, this);
                f.Show();
            }
        }

        void ScheduleAdded.scheduleAdded()
        {
            initSchedule(userId);
        }
    }
}
