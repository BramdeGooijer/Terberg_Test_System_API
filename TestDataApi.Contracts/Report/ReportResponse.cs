using TestDataApi.Contracts.Test;

namespace TestDataApi.Contracts.Report;

public record ReportResponse(
    Guid Id,
    Guid TcmId,
    DateTime TestDate,
    List<TestResponse> Tests);