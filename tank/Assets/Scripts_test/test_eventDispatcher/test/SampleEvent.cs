namespace EventDispatcher
{
    /// <summary>
    /// Test event.
    /// </summary>
    public class SampleEvent : Event
    {
        // GETTER / SETTER
        /// <summary>
        /// An example of event-specific data you can add in.
        /// </summary>
        private string _custom_value_string;
        public string customValue
        {
            get
            {
                return _custom_value_string;
            }
            set
            {
                _custom_value_string = value;
            }
        }

        /// <summary>
        /// The Event Type Name
        /// </summary>
        public static string SAMPLE_EVENT = "SAMPLE_EVENT";

        /// <summary>
        /// Initializes a new instance of the <see cref="com.rmc.projects.event_dispatcher.SampleEvent"/> class.
        /// </summary>
        /// <param name="aType_str">A type_str.</param>
        public SampleEvent(string aType_str)
            : base(aType_str)
        {

        }
    }
}