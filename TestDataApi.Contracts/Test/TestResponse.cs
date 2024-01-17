namespace TestDataApi.Contracts.Test;

public record TestResponse(
    Guid Id,
    string Name,
    string Description,
    bool Successfull,
    long TestDurationInMilliseconds
    );