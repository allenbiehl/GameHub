namespace GameHub.Core.Event
{
    public interface IEventListener<T>
    {
        public void OnEvent( T eventType );
    }
}