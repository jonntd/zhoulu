namespace EventDispatcher
{
    public interface IEvent
    {
        string type { get; set; }

        object target { get; set; }
    }
}