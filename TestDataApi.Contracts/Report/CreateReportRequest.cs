using TestDataApi.Contracts.Test;

namespace TestDataApi.Contracts.Report;

public record CreateReportRequest(
    Guid TcmId,
    List<CreateTestRequest> Tests
);