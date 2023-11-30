namespace Publicador;

public class HelloWorld
{
    private readonly List<IDomainEvent> _events = new List<IDomainEvent>();

    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();   

    public void ClearDomainEvents() => _events.Clear();    

    public string Name { get; init; }

    public HelloWorld(string name)
    {
        Name = name;

        Raise(new HelloWorldCreatedDomainEvent(name));
    }

    private void Raise(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }
}