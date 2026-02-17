namespace MementoPattern
{
    /// <summary>
    /// Marker interface for Memento objects.
    /// The Caretaker (History) only knows about this interface, 
    /// preventing it from accessing the state internal to the memento.
    /// </summary>
    public interface IMemento
    {
        DateTime GetDate();
        string GetName();
    }
}
