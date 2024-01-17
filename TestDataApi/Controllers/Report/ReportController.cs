using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestDataApi.Contracts.Report;
using TestDataApi.Contracts.Test;
using TestDataApi.Models;
using TestDataApi.Services.Reports;

namespace TestDataApi.Controllers;

[ApiController]
[Route("report")]
public class ReportController : ApiController
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost]
    public IActionResult CreateReport(CreateReportRequest request)
    {
        ErrorOr<Report> requestToReportResult = Report.From(request);

        if (requestToReportResult.IsError)
        {
            return Problem(requestToReportResult.Errors);
        }

        var report = requestToReportResult.Value;
        ErrorOr<Created> createReportResult = _reportService.CreateReport(report);

        return createReportResult.Match(
            created => CreatedAtGetReport(report),
            errors => Problem(errors));

    }

    [HttpGet("{id:guid}")]
    public IActionResult GetReport(Guid id)
    {
        ErrorOr<Report> getReportResult = _reportService.GetReport(id);

        return getReportResult.Match(
            report => Ok(MapReportResponse(report)),
            errors => Problem(errors)
            );
    }

    [HttpGet]
    public List<Report> GetAllReports()
    {
        List<Report> getAllReportResult = _reportService.GetAllReports();

        return getAllReportResult;
    }

    private static ReportResponse MapReportResponse(Report report)
    {
        List<TestResponse> testResponses = new List<TestResponse>();
        
        foreach (Test test in report.Tests)
        {
            testResponses.Add(new TestResponse(test.Id, test.Name, test.Description, test.Successful, test.TestDurationInMilliseconds));
        }

        return new ReportResponse(report.Id, report.TcmId, report.TestDate, testResponses);
    }
    
    private CreatedAtActionResult CreatedAtGetReport(Report report)
    {
        return CreatedAtAction(
            nameof(GetReport),
            new { id = report.Id },
            MapReportResponse(report));
    }
}