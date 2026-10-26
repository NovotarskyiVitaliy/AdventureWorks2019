namespace AdventureWorks2019.Application;

public class JwtOptions
{
    public string SecretKey { get; set; } = "";
    public int ExpireMinutes { get; set; } = 5;
}
