namespace TestDataApi.Contracts.Test;

public record CreateTestRequest(
    string Name,
    string Description,
    bool Successful,
    long TestDurationInMilliseconds
);