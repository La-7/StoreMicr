namespace EventBus.Messages.Events
{
    public class BaseEvent
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }

        public BaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public BaseEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }
    }
}
