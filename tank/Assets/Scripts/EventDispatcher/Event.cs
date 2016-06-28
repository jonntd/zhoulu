namespace EventDispatcher
{
    public class Event : IEvent
    {
        private string _type_string;
        string IEvent.type
        {
            get
            {
                return _type_string;
            }
            set
            {
                _type_string = value;

            }
        }

        private object _target_object;
        object IEvent.target
        {
            get
            {
                return _target_object;
            }
            set
            {
                _target_object = value;

            }
        }

        public Event(string aType_str)
        {
            //
            _type_string = aType_str;
        }
    }
}