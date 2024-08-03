namespace DomainDrivenDesign.Domain.Options;
public class StmpOptions
{
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;

}
