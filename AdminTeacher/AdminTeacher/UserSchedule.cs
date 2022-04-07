using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminTeacher
{
    public class UserSchedule
    {
        public string id;
        public string userId;
        public string order;
        public string day;
        public string subject;
        public string building;
        public string room;
        public string group;
        public string week;

        public UserSchedule(string id, string uid, string order, string d, string sub,
            string b, string r, string g, string w)
        {
            this.id = id;
            userId = uid;
            this.order = order;
            day = d;
            subject = sub;
            building = b;
            room = r;
            group = g;
            week = w;
        }

        public UserSchedule()
        { 
        }

        public UserSchedule(string[] data)
        {
            id = data[7];
            userId = data[8];
            order = data[2];
            day = data[0];
            subject = data[1];
            building = data[3];
            room = data[4];
            group = data[5];
            week = data[6];
        }
    }
}
