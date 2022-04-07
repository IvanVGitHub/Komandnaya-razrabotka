using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdminTeacherTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void getUserScheduleTest()
        {
            AdminTeacher.DBHelper h = new AdminTeacher.DBHelper();

            //assert
            Assert.IsNotNull(h.getWorkers());
        }

        [TestMethod]
        public void addAddressTest()
        {
            AdminTeacher.DBHelper h = new AdminTeacher.DBHelper();
            string[] s = new string[3];
            s[0] = "a";
            s[1] = "b";
            s[2] = "c";
            //assert
            Assert.IsTrue(h.addAddress(s));
        }

        [TestMethod]
        public void deleteAddressTest()
        {
            AdminTeacher.DBHelper h = new AdminTeacher.DBHelper();
            //assert
            Assert.IsTrue(h.deleteAddress("a"));
        }
    }
}
