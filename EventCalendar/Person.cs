using System;

namespace EventCalendar
{
    /// <summary>
    /// A person has a mandatory full name and
    /// an optional email address and phone number.
    /// </summary>
    public class Person
    {
        public const int MAX_EVENTS = 10;
        private string _firstName;
        private string _lastName;
        private string _emailAdress;
        private string _phoneNumber;
        private int _nrEventsRegistered = 0;

        /// <summary>
        /// A person must only be created with a
        /// full name specified.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public Person(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public Person(string firstName, string lastName, string email, string phone) : this(firstName, lastName)
        {
            _emailAdress = email;
            _phoneNumber = phone;
        }

        /// <summary>
        /// Read-only property first name
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
        }

        /// <summary>
        /// Read-only property last name
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
        }

        /// <summary>
        /// Optional property email address
        /// </summary>
        public string EmailAdress
        {
            get
            {
                //string mailadresse = Harald.EmailAdress
                return _emailAdress;
            }

            set
            {
                // Harald.EmailAdress = "harald@example.com";
                _emailAdress = value;
            }
        }

        /// <summary>
        /// Optional property phone number
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }

            set
            {
                _phoneNumber = value;
            }
        }

        /// <summary>
        /// Number of events this person is registered to.
        /// </summary>
        public int NrEventsRegistered
        {
            get
            {
                return _nrEventsRegistered;
            }

            set
            {
                // haraldSchmitt.NrEventsRegistred = haraldSchmitt.NrEventsRegistred++;
                if (value <= MAX_EVENTS && value >= 0)
                {
                    _nrEventsRegistered = value;
                }
            }
        }
    }
}