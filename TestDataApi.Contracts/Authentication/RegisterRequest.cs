namespace TestDataApi.Contracts.Authentication;

public record RegisterRequest(
    string Username,
    string Password);