using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventCalendar;

namespace EventCalender.Test
{
    [TestClass()]
    public class EventTests
    {
        [TestMethod()]
        public void E01_Constructor_AllOk_ShouldCreateSimpleEvent()
        {
            // Arrange
            Person invitor = new Person("Max", "Müller");
            DateTime date = new DateTime(2016, 10, 25);

            // Act
            Event ev = new Event(invitor, "Party", date, 2);

            // Assert
            Assert.AreEqual(invitor, ev.Invitor);
            Assert.AreEqual(date, ev.Date);
            Assert.AreEqual("Party", ev.Title);
            Assert.AreEqual(2, ev.MaxParticipants);
        }

        [TestMethod()]
        public void E02_Properties_Change_ShouldBePossible()
        {
            // Arrange
            Person invitor = new Person("Max", "Müller");
            DateTime date = new DateTime(2016, 10, 25);
            Event ev = new Event(invitor, "Party", date, 2);

            // Act
            ev.Date = ev.Date.AddDays(1);
            ev.Title = "Aufräumparty";

            // Assert
            Assert.AreNotEqual(date, ev.Date);
            Assert.AreEqual("26.10.2016", ev.Date.ToShortDateString());
            Assert.AreEqual("Aufräumparty", ev.Title);
        }

        [TestMethod()]
        public void E03_Register_NotFullyBooked_ShouldBePossible()
        {
            // Arrange
            Person invitor = new Person("Max", "Müller");
            DateTime date = new DateTime(2016, 10, 25);
            Event ev = new Event(invitor, "Party", date, 2);
            Person participant1 = new Person("Susi", "Huber");
            Person participant2 = new Person("Gretl", "Sauerkraut");

            // Act
            bool participant1Registered = ev.Register(participant1);
            bool participant2Registered = ev.Register(participant2);

            // Assert
            Assert.IsTrue(participant1Registered);
            Assert.IsTrue(participant2Registered);
        }

        [TestMethod()]
        public void E04_Register_RegisterTwice_ShouldReturnFalse()
        {
            // Arrange
            Person invitor = new Person("Max", "Müller");
            DateTime date = new DateTime(2016, 10, 25);
            Event ev = new Event(invitor, "Party", date, 2);
            Person participant1 = new Person("Susi", "Huber");

            // Act
            bool participant1Registered = ev.Register(participant1);
            bool participant1RegisteredTwice = ev.Register(participant1);

            // Assert
            Assert.IsTrue(participant1Registered);
            Assert.IsFalse(participant1RegisteredTwice);
        }

        [TestMethod()]
        public void E05_Register_FullyBooked_ShouldReturnFalse()
        {
            // Arrange
            Person invitor = new Person("Max", "Müller");
            DateTime date = new DateTime(2016, 10, 25);
            Event ev = new Event(invitor, "Party", date, 1);
            Person participant1 = new Person("Susi", "Huber");
            Person participant2 = new Person("Gretl", "Sauerkraut");

            // Act
            bool participant1Registered = ev.Register(participant1);
            bool participant2Registered = ev.Register(participant2);

            // Assert
            Assert.IsTrue(participant1Registered);
            Assert.IsFalse(participant2Registered);
        }

        [TestMethod()]
        public void E06_Register_MoreThan10Events_ShouldReturnFalse()
        {
            // Arrange
            Person invitor = new Person("Max", "Müller");
            DateTime date = new DateTime(2016, 10, 25);
            Event[] events = new Event[11];
            Person participant = new Person("Susi", "Huber");
            for (int i = 0; i < 10; i++)
            {
                events[i] = new Event(invitor, "Title-" + (i + 1), date, 2);
                events[i].Register(participant);
            }
            events[10] = new Event(invitor, "Title-11", date, 2);

            // Act
            bool participantRegistered = events[10].Register(participant);

            // Assert
            Assert.IsFalse(participantRegistered);
        }

        [TestMethod()]
        public void E07_Unregister_Registered_ShouldBeOK()
        {
            // Arrange
            Person invitor = new Person("Max", "Müller");
            DateTime date = new DateTime(2016, 10, 25);
            Event ev = new Event(invitor, "Title-1", date, 2);
            Person participant = new Person("Susi", "Huber");
            bool participantRegistered = ev.Register(participant);

            // Act
            bool participantUnregistered = ev.Unregister(participant);

            // Assert
            Assert.IsTrue(participantUnregistered);
        }

        [TestMethod()]
        public void E08_Unregister_NotRegistered_ShouldReturnFalse()
        {
            // Arrange
            Person invitor = new Person("Max", "Müller");
            DateTime date = new DateTime(2016, 10, 25);
            Event ev = new Event(invitor, "Title-1", date, 2);
            Person participant1 = new Person("Susi", "Huber");
            Person participant2 = new Person("Max", "Maier");

            // Act
            bool participantUnregistered = ev.Unregister(participant1);
            bool participantRegistered = ev.Register(participant1);
            ev.Register(participant2);
            bool participantUnregisteredAfterRegister = ev.Unregister(participant1);
            bool participantUnregisteredAfterUnegister = ev.Unregister(participant1);

            // Assert
            Assert.IsFalse(participantUnregistered, "Nicht registrierte Person kann sich nicht abmelden");
            Assert.IsTrue(participantRegistered, "Anmelden nicht OK");
            Assert.IsTrue(participantUnregisteredAfterRegister, "Abmelden nach Anmelden nicht OK");
            Assert.IsFalse(participantUnregisteredAfterUnegister, "Abmelden nach An- und Abmelden geht");
        }

        [TestMethod()]
        public void E09_Cancel_Registered_ShouldBeOK()
        {
            // Arrange
            Person invitor = new Person("Max", "Müller");
            DateTime date = new DateTime(2016, 10, 25);
            Event ev = new Event(invitor, "Title-1", date, 2);
            Person participant = new Person("Susi", "Huber");
            bool participantRegistered = ev.Register(participant);
            int nrRegistered = participant.NrEventsRegistered;

            // Act
            ev.Cancel();

            // Assert
            Assert.AreEqual((nrRegistered - 1), participant.NrEventsRegistered);
        }

        [TestMethod()]
        public void E10_Cancel_Register_ShouldNotBePossible()
        {
            // Arrange
            Person invitor = new Person("Max", "Müller");
            DateTime date = new DateTime(2016, 10, 25);
            Event ev = new Event(invitor, "Title-1", date, 2);
            Person participant = new Person("Susi", "Huber");

            // Act
            ev.Cancel();
            bool participantRegistered = ev.Register(participant);

            // Assert
            Assert.IsFalse(participantRegistered);
        }
    }
}