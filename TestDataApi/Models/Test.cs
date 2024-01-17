namespace TestDataApi.Models;

public class Test
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Successful { get; private set; }
    public long TestDurationInMilliseconds { get; private set; }

    public Test(string name, string description, bool successful, long testDurationInMilliseconds)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Successful = successful;
        TestDurationInMilliseconds = testDurationInMilliseconds;
    }

    public Test()
    {
        
    }
}