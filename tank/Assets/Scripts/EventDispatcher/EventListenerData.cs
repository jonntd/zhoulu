namespace EventDispatcher
{
    public class EventListenerData
    {
        private object _eventListener;
        public object eventListener
        {
            get
            {
                return _eventListener;
            }
            set
            {
                _eventListener = value;

            }
        }


        private string _eventName_string;
        public string eventName
        {
            get
            {
                return _eventName_string;
            }
            set
            {
                _eventName_string = value;

            }
        }


        private EventDelegate _eventDelegate;
        public EventDelegate eventDelegate
        {
            get
            {
                return _eventDelegate;
            }
            set
            {
                _eventDelegate = value;

            }
        }

        private EventDispatcherAddMode _eventListeningMode;
        public EventDispatcherAddMode eventListeningMode
        {
            get
            {
                return _eventListeningMode;
            }
            set
            {
                _eventListeningMode = value;

            }
        }

        public EventListenerData(object aEventListener, string aEventName_string, EventDelegate aEventDelegate, EventDispatcherAddMode eventListentInModel)
        {
            _eventListener = aEventListener;
            _eventName_string = aEventName_string;
            _eventDelegate = aEventDelegate;
            _eventListeningMode = eventListentInModel;
        }

    }
}