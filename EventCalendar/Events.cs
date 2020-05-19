using System;

namespace EventCalendar
{
    /// <summary>
    /// An event has an invitor, a title, a date and an array of
    /// participants in a fixed size depending on the specified
    /// maximum number of participants.
    /// Only title and date of the event can be changed, after an
    /// event has been created.
    /// It is possible to register and unregister persons and to
    /// cancel an event.
    /// </summary>
    public class Event
    {
        private Person _invitor;
        private string _title;
        private DateTime _date;
        private Person[] _participants;
        private int _nrRegistered;
        private bool _isCancelled;

        /// <summary>
        /// Read-only property invitor
        /// </summary>
        public Person Invitor
        {
            get
            {
                return _invitor;
            }
        }

        /// <summary>
        /// Property to set and read the event title
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        /// <summary>
        /// Property to read and set the event date
        /// </summary>
        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
            }
        }

        /// <summary>
        /// Read-only property max numbers of participants
        /// </summary>
        public int MaxParticipants
        {
            get
            {
                return _participants.Length;
            }
        }

        /// <summary>
        /// An event is created with a fixed invitor and maximum
        /// number of participants. Title and date can also be changed
        /// after construction.
        /// </summary>
        /// <param name="invitor"></param>
        /// <param name="title"></param>
        /// <param name="date"></param>
        /// <param name="maxParticipants"></param>
        public Event(Person invitor,
                     string title,
                     DateTime date,
                     int maxParticipants)
        {
            //Event Heuriger = new Event(MaxMusterWirt,"Heurigenschmaus 2020", 20.05.2020, 100)
            _invitor = invitor;
            _title = title;
            _date = date;
            _participants = new Person[maxParticipants];
        }

        /// <summary>
        /// It is only possible to register a participant,
        /// - which is not registered already,
        /// - as long as the maximum number of participants is not reached yet,
        /// - if the participant is not already registered to 10 other events
        /// </summary>
        /// <param name="newParticipant"></param>
        /// <returns>true, if registration was successful</returns>
        public bool Register(Person newParticipant)
        {
            if (newParticipant.NrEventsRegistered < Person.MAX_EVENTS)
            {
                if (IsRegistered(newParticipant) == false)
                {
                    for (int i = 0; i < _participants.Length; i++)
                    {
                        if (_participants[i] == null)
                        {
                            newParticipant.NrEventsRegistered = newParticipant.NrEventsRegistered + 1;
                            _participants[i] = newParticipant;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// It is only possible to unregister a participant
        /// who is actually registered. After unregister,
        /// the number of events registered of the person
        /// is decremented by one.
        /// </summary>
        /// <param name="participant"></param>
        /// <returns>true, if unregister was successful</returns>
        public bool Unregister(Person participant)
        {
            if (IsRegistered(participant))
            {
                _participants[FindRegistration(participant)] = null;
                participant.NrEventsRegistered = participant.NrEventsRegistered - 1;
                return true;
            }
            return false;
        }

        /// <summary>
        /// If an event is cancelled, all participants
        /// are notified and finally the number of
        /// participants is set to 0;
        /// </summary>
        public void Cancel()
        {
            _isCancelled = true;
            for (int i = 0; i < _participants.Length; i++)
            {
                if (_participants[i] != null)
                {
                    _participants[i].NrEventsRegistered = _participants[i].NrEventsRegistered - 1;
                }
            }
            _participants = new Person[0];
        }

        private bool IsRegistered(Person p)
        {
            for (int i = 0; i < _participants.Length; i++)
            {
                if (_participants[i] != null)
                {
                    if (_participants[i].Equals(p))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private int FindRegistration(Person p)
        {
            for (int i = 0; i < _participants.Length; i++)
            {
                if (_participants[i] != null)
                {
                    if (_participants[i].Equals(p))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}