using TestDataApi.Contracts.Report;
using ErrorOr;

namespace TestDataApi.Models;

public class Report
{
    public Guid Id { get; private set; }
    public Guid TcmId { get; private set; }
    public DateTime TestDate { get; private set; }
    public List<Test> Tests { get; }

    public Report(
        Guid id,
        Guid tcmId,
        List<Test> tests
    )
    {
        Id = id;
        TcmId = tcmId;
        TestDate = DateTime.UtcNow;
        Tests = tests;
    }

    public Report()
    {
        Tests = new List<Test>();
    }

    public static ErrorOr<Report> Create(
        Guid tcmId,
        Guid? id = null,
        List<Test>? tests = null
    )
    {
        List<Error> errors = new();

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Report(
            id ?? Guid.NewGuid(),
            tcmId,
            tests ?? new List<Test>());
    }

    public void AddTest(string name, string description, bool successful, long testDuration)
    {
        Test test = new Test(name, description, successful, testDuration);
        Tests.Add(test);
    }

    public static ErrorOr<Report> From(CreateReportRequest createReportRequest)
    {
        List<Test> tests = new List<Test>();

        foreach (var testRequest in createReportRequest.Tests)
        {
            tests.Add(new Test(testRequest.Name, testRequest.Description, testRequest.Successful, testRequest.TestDurationInMilliseconds));
        }
        
        return Create(
            tcmId: createReportRequest.TcmId,
            tests: tests);
    }
}