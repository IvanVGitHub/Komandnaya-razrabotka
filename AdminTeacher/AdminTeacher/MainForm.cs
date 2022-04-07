using ExcelLibrary.SpreadSheet;
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
    public partial class MainForm : Form, AddressAdded, WorkerAdded
    {
        public MainForm()
        {
            InitializeComponent();
            if (Program.getUserInfo().isAdmin)
            {
                initAdmin();
            }
            else
            {
                initTeacher();
            }
            
        }

        private void initAdmin()
        {
            label1.Text = "Здравствуйте, Администратор.";
            tabControl1.Visible = false;
            tabControl2.Visible = true;
            refreshAddress();
            refreshUsers(dataGridView3);
            showUsersForSchedule();

        }

        private void showUsersForSchedule()
        {
            refreshUsers(dataGridView5);
        }

        private void initTeacher()
        {
            label1.Text = "Здравствуйте, " + Program.getUserInfo().fio;
            tabControl1.Visible = true; 
            tabControl2.Visible = false;
            tabControl1.SelectedIndex = 0;

            UserInfo ui =  Program.getUserInfo();
            label14.Text = ui.fio;
            label13.Text = ui.position;
            label12.Text = ui.zvanie;
            label11.Text = ui.email;
            label10.Text = ui.phone;
            label9.Text = ui.address;
            List<String[]> addresses = Program.getDBHelper().getAddresses();
            if (addresses.Count > 0)
            {
                fillAddressTable(addresses, dataGridView1);
            }

            List<String[]> workers = Program.getDBHelper().getWorkers();
            if (workers.Count > 0)
            {
                fillWorkersTable(workers, dataGridView2);
            }
            initSchedule(ui.id);

        }

        private void initSchedule(string id)
        {
            List<UserSchedule> sch = Program.getDBHelper().getUserSchedule(id);
            if (sch.Count > 0)
            {
                fillScheduleTable(sch);
            }
        }

        private void fillScheduleTable(List<UserSchedule> list)
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
            dataGridView6.DataSource = table;
            dataGridView6.Columns[dataGridView6.Columns.Count - 1].Visible = false;
            dataGridView6.Columns[dataGridView6.Columns.Count - 2].Visible = false;
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

        private void fillWorkersTable(List<String[]> list, DataGridView grid)
        {
            DataTable table = new DataTable("Сотрудники");
            DataColumn c0 = new DataColumn("ФИО");
            DataColumn c1 = new DataColumn("Должность");
            DataColumn c2 = new DataColumn("Научное звание");
            DataColumn c3 = new DataColumn("Почта");
            DataColumn c4 = new DataColumn("Телефон");
            DataColumn c5 = new DataColumn("Адрес");
            DataColumn c6 = new DataColumn("ID");
            DataColumn c7 = new DataColumn("PAS");
            table.Columns.Add(c0);
            table.Columns.Add(c1);
            table.Columns.Add(c2);
            table.Columns.Add(c3);
            table.Columns.Add(c4);
            table.Columns.Add(c5);
            table.Columns.Add(c6);
            table.Columns.Add(c7);
            foreach (String[] item in list)
            {
                DataRow row = table.NewRow();
                row["ФИО"] = item[1];
                row["Должность"] = item[2];
                row["Научное звание"] = item[3];
                row["Почта"] = item[0];
                row["Телефон"] = item[4];
                row["Адрес"] = item[5];
                row["PAS"] = item[6];
                row["ID"] = item[7];
                table.Rows.Add(row);
            }

            grid.DataSource = table;
            grid.Columns[grid.Columns.Count - 1].Visible = false;
            grid.Columns[grid.Columns.Count - 2].Visible = false;
        }

        private void fillAddressTable(List<String[]> list, DataGridView grid)
        {
            DataTable table = new DataTable("Адреса");
            DataColumn c0 = new DataColumn("Название");
            DataColumn c1 = new DataColumn("Адрес");
            DataColumn c2 = new DataColumn("Телефон");
            DataColumn c3 = new DataColumn("ID");
            table.Columns.Add(c0);
            table.Columns.Add(c1);
            table.Columns.Add(c2);
            table.Columns.Add(c3);
            foreach (String[] item in list)
            {
                DataRow row = table.NewRow();
                row["Название"] = item[0];
                row["Адрес"] = item[1];
                row["Телефон"] = item[2];
                row["ID"] = item[3];
                table.Rows.Add(row);
            }
            grid.DataSource = table;
            grid.Columns[grid.Columns.Count - 1].Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.logout();
            this.Hide();
            var form2 = new LoginForm();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
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

        private void refreshAddress()
        {
            List<String[]> addresses = Program.getDBHelper().getAddresses();
            if (addresses.Count > 0)
            {
                fillAddressTable(addresses, dataGridView4);
            }
        }


        private void refreshUsers(DataGridView v)
        {
            List<String[]> workers = Program.getDBHelper().getWorkers();
            if (workers.Count > 0)
            {
                fillWorkersTable(workers, v);
            }
        }

        void AddressAdded.addressAdded()
        {
            refreshAddress();
        }

        void WorkerAdded.workerAdded()
        {
            refreshUsers(dataGridView3);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataGridView d = tabControl2.SelectedIndex == 0 ? dataGridView3 : dataGridView4;
            AddWorker f = new AddWorker(getCells(d), this);
            f.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DataGridView d = tabControl2.SelectedIndex == 0 ? dataGridView3 : dataGridView4;
            string[] data = getCells(d);
            AddWorker f = new AddWorker(this);
            f.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DataGridView d = tabControl2.SelectedIndex == 0 ? dataGridView3 : dataGridView4;
            string[] data = getCells(d);
            Program.getDBHelper().deleteWorker(data[data.Length - 2]);
            refreshUsers(dataGridView3);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string[] data = getCells(dataGridView5);
            Schedule f = new Schedule(data[data.Length - 2]);
            f.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            DataGridView d = tabControl2.SelectedIndex == 0 ? dataGridView3 : dataGridView4;
            AddAddress f = new AddAddress(getCells(d), this);
            f.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            DataGridView d = tabControl2.SelectedIndex == 0 ? dataGridView3 : dataGridView4;
            string[] data = getCells(d);
            AddAddress f = new AddAddress(this);
            f.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            DataGridView d = tabControl2.SelectedIndex == 0 ? dataGridView3 : dataGridView4;
            string[] data = getCells(d);
            Program.getDBHelper().deleteAddress(data[data.Length - 1]);
            refreshAddress();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Workbook workbook = new Workbook();
            Worksheet worksheet = new Worksheet("First Sheet");
            worksheet.Cells[0, 0] = new Cell("Неделя");
            worksheet.Cells[0, 1] = new Cell("День");
            worksheet.Cells[0, 2] = new Cell("№ Пары");
            worksheet.Cells[0, 3] = new Cell("Предмет");
            worksheet.Cells[0, 4] = new Cell("Группа");
            worksheet.Cells[0, 5] = new Cell("Аудитория");
            worksheet.Cells[0, 6] = new Cell("Корпус");


            List<UserSchedule> data = Program.getDBHelper().getUserSchedule(Program.getUserInfo().id);
            for (int i = 0; i < data.Count; i++)
            {
                UserSchedule item = data[i];
                worksheet.Cells[i + 1, 0] = new Cell(item.week.Equals("1") ? "А" : "Б");
                worksheet.Cells[i + 1, 1] = new Cell(getDay(item.day));
                worksheet.Cells[i + 1, 2] = new Cell(item.order);
                worksheet.Cells[i + 1, 3] = new Cell(item.subject);
                worksheet.Cells[i + 1, 4] = new Cell(item.group);
                worksheet.Cells[i + 1, 5] = new Cell(item.room);
                worksheet.Cells[i + 1, 6] = new Cell(item.building);
            }

            Worksheet worksheet2 = new Worksheet("Second Sheet");
            List<string> gps = Program.getDBHelper().getAllGroups();
            if (gps.Count > 0)
            {
                int j = 0;
                foreach (string s in gps)
                {
                    worksheet2.Cells[0, 10  + j] = new Cell("Неделя");
                    worksheet2.Cells[0, 11 + j] = new Cell("День");
                    worksheet2.Cells[0, 12 + j] = new Cell("№ Пары");
                    worksheet2.Cells[0, 13 + j] = new Cell("Предмет");
                    worksheet2.Cells[0, 14 + j] = new Cell("Группа");
                    worksheet2.Cells[0, 15 + j] = new Cell("Аудитория");
                    worksheet2.Cells[0, 16 + j] = new Cell("Корпус");
                    worksheet2.Cells[0, 17 + j] = new Cell("Преподаватель");
                    List<UserSchedule> us = Program.getDBHelper().getGroupsSchedule(s);
                    for (int i = 0; i < us.Count; i++)
                    {
                        UserSchedule item = us[i];
                        worksheet2.Cells[i + 1, 10 + j] = new Cell(item.week.Equals("1") ? "А" : "Б");
                        worksheet2.Cells[i + 1, 11 + j] = new Cell(getDay(item.day));
                        worksheet2.Cells[i + 1, 12 + j] = new Cell(item.order);
                        worksheet2.Cells[i + 1, 13 + j] = new Cell(item.subject);
                        worksheet2.Cells[i + 1, 14 + j] = new Cell(s);
                        worksheet2.Cells[i + 1, 15 + j] = new Cell(item.room);
                        worksheet2.Cells[i + 1, 16 + j] = new Cell(item.building);
                        worksheet2.Cells[i + 1, 17 + j] = new Cell(item.userId);
                    }
                    j+=10;
                }
            }

            workbook.Worksheets.Add(worksheet);
            workbook.Worksheets.Add(worksheet2);
            workbook.Save("MyFile2.xls");

        }

    }

}
