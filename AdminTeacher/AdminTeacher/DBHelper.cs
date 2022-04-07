using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data.SqlClient;

using System.Data;
namespace AdminTeacher
{
    public class DBHelper
    {
        private  OleDbCommand cmd = new OleDbCommand();
        private  OleDbConnection cn = new OleDbConnection();
        private string dbPath = @"C:\Users\Иван\YandexDisk\Инста\8 семестр\8 семестр. Технол. команд. разр. прогр. сист\Контрольная\AdminTeacher\MyDB2.accdb";

        public DBHelper()
        {
            cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbPath;
            cmd.Connection = cn;
        }

        public List<String[]> getWorkers()
        {
            List<String[]> result = new List<String[]>();
            string[] cels;
            string q = "select * from Users where isadmin=False";
            OleDbCommand cm = new OleDbCommand(q, cn);
            cn.Open();
            OleDbDataReader reader = cm.ExecuteReader();
            while (reader.Read())
            {
                cels = new string[8];
                cels[0] = reader[1].ToString();
                cels[1] = reader[4].ToString();
                cels[2] = reader[5].ToString();
                cels[3] = reader[6].ToString();
                cels[4] = reader[7].ToString();
                cels[5] = reader[8].ToString();
                cels[6] = reader[2].ToString();
                cels[7] = reader[0].ToString();
                result.Add(cels);
            }
            reader.Close();
            cn.Close();
            return result;
        }

        public bool addAddress(string[] data)
        {
            string q = "insert into [Addresses] ([aname], [address], [phone])" 
                + " values ('" + data[0]+ "', '" + data[1] + "', '" + data[2] + "')";
            return executeCommand(q);

        }

        public bool deleteAddress(string id)
        {
            string q = "delete from Addresses where Код=" + id + "";
            return executeCommand(q);

        }

        public bool deleteWorker(string id)
        {
            string q = "delete from Users where Код=" + id + "";
            return executeCommand(q);

        }

        public bool deleteSchedule(string id)
        {
            string q = "delete from Schedule where Код=" + id + "";
            return executeCommand(q);

        }

        public bool editAddress(string[] data)
        {
            string q = "update [Addresses] set [aname]='" + data[0] + "', [address]='" +
                data[1] + "', [phone]='" + data[2] + "' where Код=" + data[3];
            return executeCommand(q);

        }

        public bool editWorker(string[] data)
        {
            string q = "update [Users] set [email]='" + data[3] + "', [fio]='" +
                data[0] + "', [position]='" + data[1] + "', [zvanie]='" + data[2] + "', "
                + "[phone]='" + data[4] + "', " + "[address]='," + data[5]
                + "', [password]='" + data[7] + "' where Код=" + data[6];
            return executeCommand(q);

        }

        public bool updateSchedule(UserSchedule us)
        {
            string q = "update [Schedule] set [sorder]=" + us.order + ", [mday]=" + us.day +
                ", [subject]='" + us.subject + "', [building]='" + us.building +
                "', [room]='" + us.room + "', [mgroup]='" + us.group + "', [week]=" + getWeek(us.week) +
                " where Код=" + us.id;
            return executeCommand(q);
        }

        public bool addWorker(string[] data)
        {
            string q = "insert into [Users] ([email], [password], [isadmin], [fio], [position], [zvanie], [phone], [address])"
                + " values ('" + data[3] + "', '" + data[6] + "', 0, '" + data[0] + "', '" + data[1] +
                "', '" + data[2] + "', '" + data[4] + "', '" + data[5]
                +"')";
            return executeCommand(q);

        }

        private string getWeek(string week)
        {
            return week.Equals("А") ? "1" : "2";
        }

        public bool addSchedule(UserSchedule us)
        {
            string q = "insert into [Schedule] ([userId], [sorder], [mday], [subject], [building], [room], [mgroup], [week]) values " +
                "(" + us.userId + ", " + us.order + ", " + us.day + ", '" + us.subject + "', '" + us.building + "', '" + us.room + "', '" +
                us.group + "', " + getWeek(us.week) + ")";
            return executeCommand(q);
        }

        public bool checkRoom(UserSchedule us)
        {
            string q = "select * from Schedule where mday=" + us.day + " and room='" + us.room + "' and week=" + getWeek(us.week) +
                " and building='" + us.building + "' and sorder=" + us.order;
            OleDbCommand cm = new OleDbCommand(q, cn);
            bool result = true;
            cn.Open();
            OleDbDataReader reader = cm.ExecuteReader();
            while (reader.Read())
            {
                if (reader[0] == null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            reader.Close();
            cn.Close();
           return result;
        }

        public List<string> getAllGroups()
        {
            List<string> result = new List<string>();
            string q = "SELECT DISTINCT  mgroup from Schedule";
            OleDbCommand cm = new OleDbCommand(q, cn);
            cn.Open();
            OleDbDataReader reader = cm.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader[0].ToString());
            }

            reader.Close();
            cn.Close();
            return result;
        }

        public List<UserSchedule> getGroupsSchedule(string group)
        {
            List<UserSchedule> result = new List<UserSchedule>();
            string q = "SELECT Schedule.userId, Schedule.sorder, Schedule.mday, Schedule.subject, Schedule.building, Schedule.room, Schedule.week, Users.fio " +
                "FROM [Schedule], [Users] WHERE (([Schedule].[mgroup]='" +group + "') AND ([Schedule].[userId]=[Users].[Код])) order by week, mday, sorder";
            OleDbCommand cm = new OleDbCommand(q, cn);
            cn.Open();
            OleDbDataReader reader = cm.ExecuteReader();
            while (reader.Read())
            {
                UserSchedule u = new UserSchedule();
                u.order = reader[1].ToString();
                u.day = reader[2].ToString();
                u.subject = reader[3].ToString();
                u.building = reader[4].ToString();
                u.room = reader[5].ToString();
                u.week = reader[6].ToString();
                u.userId = reader[7].ToString();
                result.Add(u);
            }
            reader.Close();
            cn.Close();
            return result;
        }

        public List<UserSchedule> getUserSchedule(String id)
        {
            List<UserSchedule> result = new List<UserSchedule>();
            string q = "select * from Schedule where userid=" + id + " order by week, mday, sorder";
            OleDbCommand cm = new OleDbCommand(q, cn);
            cn.Open();
            OleDbDataReader reader = cm.ExecuteReader();
            while (reader.Read())
            {
                UserSchedule u = new UserSchedule(
                    reader[0].ToString(), reader[1].ToString(),
                    reader[2].ToString(), reader[3].ToString(),
                    reader[4].ToString(), reader[5].ToString(),
                    reader[6].ToString(), reader[7].ToString(),
                    reader[8].ToString());

                result.Add(u);
            }
            reader.Close();
            cn.Close();
            return result;
        }

        private bool executeCommand(string command)
        {
            try
            {
                cn.Open();
                cmd.CommandText = command;
                cmd.ExecuteNonQuery();
                cn.Close();
             
            }
            catch (Exception er)
            {
                cn.Close();
                return false;
            }
            return true;
        }

        public List<String[]> getAddresses()
        {
            List<String[]> result = new List<String[]>();
            string[] cels;
            string q = "select * from Addresses";
            OleDbCommand cm = new OleDbCommand(q, cn);
            cn.Open();
            OleDbDataReader reader = cm.ExecuteReader();
            while (reader.Read())
            {
                cels = new string[4];
                cels[0] = reader[1].ToString();
                cels[1] = reader[2].ToString();
                cels[2] = reader[3].ToString();
                cels[3] = reader[0].ToString();
                result.Add(cels);
            }
            reader.Close();
            cn.Close();
            return result;
        }

        public bool loginUser(string email, string pass)
        {
            string[] cels = {"", "", "", "", "", "", "", "", ""};
            string q = "select * from Users where email='" + email + "' and password='" + pass + "'";
            OleDbCommand cm = new OleDbCommand(q, cn);
            cn.Open();
            OleDbDataReader reader = cm.ExecuteReader();
            while (reader.Read())
            {
                cels[0] = reader[1].ToString();
                cels[1] = reader[2].ToString();
                cels[2] = reader[3].ToString();
                cels[3] = reader[4].ToString();
                cels[4] = reader[5].ToString();
                cels[5] = reader[6].ToString();
                cels[6] = reader[7].ToString();
                cels[7] = reader[8].ToString();
                cels[8] = reader[0].ToString();
               
            }
            reader.Close();
            cn.Close();
            if (cels[2].Length > 0)
            {
                Program.getUserInfo().isAdmin = Boolean.Parse(cels[2]);
                if (!Program.getUserInfo().isAdmin)
                {
                    Program.getUserInfo().email = cels[0];
                    Program.getUserInfo().fio = cels[3];
                    Program.getUserInfo().position = cels[4];
                    Program.getUserInfo().zvanie = cels[5];
                    Program.getUserInfo().phone = cels[6];
                    Program.getUserInfo().address = cels[7];
                    Program.getUserInfo().id = cels[8];

                }
                return true;
            }
            return false;
        }
    }
}
