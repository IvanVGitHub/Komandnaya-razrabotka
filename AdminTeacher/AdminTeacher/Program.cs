using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminTeacher
{
    static class Program
    {
        private static UserInfo ui;
        private static DBHelper dbHelper;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ui = new UserInfo();
            dbHelper = new DBHelper();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        public static UserInfo getUserInfo()
        {
            if (ui == null) {
                ui = new UserInfo();
            }
            return ui;
        }

        public static void logout()
        {
            ui = null;
        }

        public static DBHelper getDBHelper()
        {
            if (dbHelper == null)
            {
                dbHelper = new DBHelper();
            }
            return dbHelper;
        }
    }
}
