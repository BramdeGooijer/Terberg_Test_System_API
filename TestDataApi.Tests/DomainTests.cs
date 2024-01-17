using ErrorOr;
using TestDataApi.Contracts.Report;
using TestDataApi.Contracts.Test;
using TestDataApi.Models;

namespace TestDataApi.Tests;

public class Tests
{
    [Test]
    public void DomainTest()
    {
        CreateReportRequest reportRequest = new CreateReportRequest(Guid.NewGuid(), new List<CreateTestRequest>());
        ErrorOr<Report> reportFromRequest = Report.From(reportRequest);

        if (reportFromRequest.IsError)
        {
            return;
        }

        Report report = reportFromRequest.Value;
        
        report.AddTest("Test 1", "dit was test 1 voor gpio pin 1", true, 5);
        Assert.AreEqual(1, report.Tests.Count);
    }
}