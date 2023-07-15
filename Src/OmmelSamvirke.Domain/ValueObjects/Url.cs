namespace OmmelSamvirke.Domain.ValueObjects;

public class Url
{
    public string Address { get; set; }

    public Url(string address)
    {
        Address = address;
    }
}
