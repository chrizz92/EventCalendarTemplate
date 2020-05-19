using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventCalendar;

namespace EventCalender.Test
{
    [TestClass()]
    public class PersonTests
    {
        [TestMethod()]
        public void P01_Constructor_AllOk_ShouldCreatePersonWithFullName()
        {
            Person p = new Person("Max", "Müller");
            Assert.AreEqual("Max", p.FirstName);
            Assert.AreEqual("Müller", p.LastName);
        }

        [TestMethod()]
        public void P02_Constructor_AllOk_OptionalFields()
        {
            Person p = new Person("Max", "Müller");
            Assert.AreEqual(null, p.EmailAdress);
            Assert.AreEqual(null, p.PhoneNumber);
            p.PhoneNumber = "123456";
            Assert.AreEqual("123456", p.PhoneNumber);
            p.EmailAdress = "m.mueller@hotmail.de";
            Assert.AreEqual("m.mueller@hotmail.de", p.EmailAdress);
        }
    }
}
