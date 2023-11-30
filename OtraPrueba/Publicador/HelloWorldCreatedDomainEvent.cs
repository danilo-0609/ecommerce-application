namespace Publicador;

public sealed class HelloWorldCreatedDomainEvent : IDomainEvent
{
    public string Text { get; init; } = string.Empty;

    public HelloWorldCreatedDomainEvent(string text)
    {
        Text = text;
    }
}
