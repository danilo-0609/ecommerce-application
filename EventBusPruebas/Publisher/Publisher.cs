namespace Publisher;

public sealed class Publisher
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Pato { get; set; } = "Pato";

    public Publisher(string name, string description, string pato = "pato")
    {
        Name = name;
        Description = description;
        Pato = pato;
    }
}
